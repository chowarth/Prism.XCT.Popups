using Prism.Common;
using Prism.Ioc;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;

namespace XCT.Popups.Prism.Sample
{
    /// <summary>
    /// Provides the ability to display Xamarin.Community.Toolkit popups from ViewModels.
    /// </summary>
    internal class PopupService : IPopupService
    {
        private readonly IApplicationProvider _applicationProvider;
        private readonly IContainerProvider _containerProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="PopupService"/> class.
        /// </summary>
        /// <param name="applicationProvider">An object that provides Application components.</param>
        /// <param name="containerProvider">An object that can resolve services.</param>
        public PopupService(IApplicationProvider applicationProvider, IContainerProvider containerProvider)
        {
            _applicationProvider = applicationProvider;
            _containerProvider = containerProvider;
        }

        /// <inheritdoc/>
        public void ShowPopup(string name, IPopupParameters parameters, Action<IPopupResult> callback)
        {
            try
            {
                parameters = GetSegmentParameters(name, parameters);

                var popup = CreatePopupFor(UriParsingHelper.GetSegmentName(name));
                var popupAware = InitializePopup(popup, parameters);

                popup.LightDismissed += Popup_LightDismissed;

                void Popup_LightDismissed()
                {
                    // May need an "is being dismissed" check because of UWP calling this when Dismiss is called

                    var lightDissmissAware = GetPopupControllerFor<IPopupLightDismissAware>(popup, PopupException.ImplementIPopupLightDismissAware);
                    var lightDismissParameters = lightDissmissAware.OnPopupLightDismissed();
                    var result = DismissPopup(popup, lightDismissParameters, true);

                    popupAware.RequestDismiss -= PopupAware_RequestDismiss;
                    popup.LightDismissed -= Popup_LightDismissed;
                    callback?.Invoke(result);
                }

                popupAware.RequestDismiss += PopupAware_RequestDismiss;

                void PopupAware_RequestDismiss(IPopupParameters dismissParameters)
                {
                    var result = DismissPopup(popup, dismissParameters, false);

                    popupAware.RequestDismiss -= PopupAware_RequestDismiss;
                    popup.LightDismissed -= Popup_LightDismissed;
                    callback?.Invoke(result);
                }

                _applicationProvider.MainPage.Navigation.ShowPopup(popup);
            }
            catch (PopupException pex)
            {
                callback?.Invoke(new PopupResult()
                {
                    Exception = pex
                });
            }
            catch (Exception ex)
            {
                callback?.Invoke(new PopupResult()
                {
                    Exception = new PopupException(PopupException.ShowPopup, ex)
                });
            }
        }

        private IPopupResult DismissPopup(Popup popup, IPopupParameters parameters, bool lightDismissed)
        {
            try
            {
                if (parameters is null)
                {
                    parameters = new PopupParameters();
                }

                // Only want to invoke Dismiss if the popup was not light dismissed
                if (!lightDismissed)
                    popup.Dismiss(default);

                var popupAware = GetPopupControllerFor<IPopupAware>(popup, PopupException.ImplementIPopupAware);
                popupAware.OnPopupDismissed();

                return new PopupResult()
                {
                    LightDismissed = lightDismissed,
                    Parameters = parameters
                };
            }
            catch(Exception ex)
            {
                return new PopupResult()
                {
                    Parameters = parameters,
                    LightDismissed = lightDismissed,
                    Exception = new PopupException(PopupException.DismissPopup, ex)
                };
            }
        }

        private T GetPopupControllerFor<T>(Popup popup, string notImplementedExceptionMessage)
            where T : class
        {
            if (popup.BindingContext is null)
            {
                throw new PopupException(PopupException.NoViewModel);
            }
            else if (popup.BindingContext is T viewmodelAsT)
            {
                return viewmodelAsT;
            }

            throw new PopupException(notImplementedExceptionMessage);
        }

        private IPopupAware InitializePopup(Popup popup, IPopupParameters parameters)
        {
            var popupAware = GetPopupControllerFor<IPopupAware>(popup, PopupException.ImplementIPopupAware);
            popupAware.OnPopupOpened(parameters);

            return popupAware;
        }

        private PrismPopup CreatePopupFor(string name)
        {
            var popup = (PrismPopup)_containerProvider.Resolve<object>(name);
            PageUtilities.SetAutowireViewModel(popup);

            return popup;
        }

        #region UriParsingHelper

        private IPopupParameters GetSegmentParameters(string uriSegment, IPopupParameters parameters)
        {
            var popupParameters = GetSegmentPopupParameters(uriSegment);

            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> navigationParameter in parameters)
                {
                    popupParameters.Add(navigationParameter.Key, navigationParameter.Value);
                }
            }

            return popupParameters;
        }

        private  IPopupParameters GetSegmentPopupParameters(string segment)
        {
            string query = string.Empty;

            if (string.IsNullOrWhiteSpace(segment))
            {
                return new PopupParameters(query);
            }

            var indexOfQuery = segment.IndexOf('?');
            if (indexOfQuery > 0)
                query = segment.Substring(indexOfQuery);

            return new PopupParameters(query);
        }

        #endregion UriParsingHelper
    }
}
