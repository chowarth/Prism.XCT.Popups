using Prism.Commands;
using Prism.Navigation;
using System;

namespace XCT.Popups.Prism.Sample.ViewModels
{
    class HelloWorldPopupViewModel : ViewModelBase, IPopupAware, IPopupLightDismissAware
    {
        public DelegateCommand DismissCommand { get; }

        public event Action<IPopupParameters> RequestDismiss;

        public HelloWorldPopupViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            DismissCommand = new DelegateCommand(DimsissCommandExecuted);
        }

        public void OnPopupOpened(IPopupParameters parameters)
        {
        }

        public void OnPopupDismissed()
        {
        }

        private void DimsissCommandExecuted()
        {
            RequestDismiss?.Invoke(new PopupParameters()
            {
                { "dismissedParam", "This was returned from the popup viewmodel" }
            });
        }

        public IPopupParameters OnPopupLightDismissed()
        {
            return new PopupParameters()
            {
                { "lightDismissedParam", "This was returned from the popup viewmodel when it was light dismissed" }
            };
        }
    }
}
