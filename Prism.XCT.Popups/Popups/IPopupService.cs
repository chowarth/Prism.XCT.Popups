using System;

namespace Prism.XCT.Popups
{
    /// <summary>
    /// Defines a contract for displaying Xamarin.Community.Toolkit popups from ViewModels.
    /// </summary>
    public interface IPopupService
    {
        /// <summary>
        /// Displays a Xamarin.Community.Toolkit popup.
        /// </summary>
        /// <param name="name">The unique name of the popup to display. Must match an entry in the <see cref="IContainerRegistry"/>.</param>
        /// <param name="parameters">Parameters that the popup can use for custom functionality.</param>
        /// <param name="callback">The action to be invoked upon successful or failed completion of displaying the popup.</param>
        /// <example>
        /// This example shows how to display a popup with two parameters.
        /// <code>
        /// var parameters = new NavigationParametersParameters
        /// {
        ///     { "title", "Connection Lost!" },
        ///     { "message", "We seem to have lost network connectivity" }
        /// };
        /// _popupService.ShowPopup("DemoPopup", parameters, <paramref name="callback"/>: null);
        /// </code>
        /// </example>
        void ShowPopup(string name, IPopupParameters parameters, Action<IPopupResult> callback);
    }
}
