using Prism.Navigation;
using System.Threading.Tasks;

namespace XCT.Popups.Prism.Sample
{
    public static class IPopupServiceExtensions
    {
        /// <summary>
        /// Displays a popup asynchronously.
        /// </summary>
        /// <param name="popupService">The popup service.</param>
        /// <param name="name">The unique name of the popup to display. Must match an entry in the <see cref="IContainerRegistry"/>.</param>
        /// <param name="parameters">Parameters that the popup can use for custom functionality.</param>
        /// <returns><see cref="IPopupResult"/> indicating whether the request was successful or if there was an encountered <see cref="PopupException"/>.</returns>
        public static Task<IPopupResult> ShowPopupAsync(this IPopupService popupService, string name, IPopupParameters parameters = null)
        {
            var tcs = new TaskCompletionSource<IPopupResult>();

            void PopupCallback(IPopupResult popupResult)
                => tcs.TrySetResult(popupResult);

            popupService.ShowPopup(name, parameters, PopupCallback);

            return tcs.Task;
        }
    }
}
