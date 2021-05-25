using Prism.Ioc;
using Prism.Mvvm;
using Xamarin.CommunityToolkit.UI.Views;

namespace XCT.Popups.Prism
{
    static class ContainerExtensions
    {
        public static IContainerRegistry RegisterPopup<TView>(this IContainerRegistry containerRegistry, string name = null)
            where TView : Popup
        {
            return containerRegistry.Register<object, TView>(name ?? typeof(TView).Name);
        }

        public static IContainerRegistry RegisterPopup<TView, TViewModel>(this IContainerRegistry containerRegistry, string name = null)
            where TView : Popup
        {
            ViewModelLocationProvider.Register<TView, TViewModel>();
            return containerRegistry.RegisterPopup<TView>(name);
        }
    }
}
