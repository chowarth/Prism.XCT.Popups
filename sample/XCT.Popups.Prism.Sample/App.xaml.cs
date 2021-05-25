using Prism.Ioc;
using Prism.XCT.IoC;
using XCT.Popups.Prism.Sample.ViewModels;
using XCT.Popups.Prism.Sample.Views;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using Prism;

namespace XCT.Popups.Prism.Sample
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterSingleton<IPopupService, PopupService>();

            containerRegistry.RegisterPopup<HelloWorldPopup, HelloWorldPopupViewModel>();
        }
    }
}
