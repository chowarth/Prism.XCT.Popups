using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;

namespace XCT.Popups.Prism.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IPopupService _popupService;

        public DelegateCommand ShowPopupCommand { get; }
        public DelegateCommand ShowPopupAsyncCommand { get; }

        public MainPageViewModel(INavigationService navigationService,
            IPopupService popupService)
            : base(navigationService)
        {
            Title = "Main Page";

            _popupService = popupService;

            ShowPopupCommand = new DelegateCommand(ShowPopupCommandExecuted);
            ShowPopupAsyncCommand = new DelegateCommand(async () => await ShowPopupCommandExecutedAsync());
        }

        private void ShowPopupCommandExecuted()
        {
            _popupService.ShowPopup("HelloWorldPopup", new PopupParameters() { { "test", "value" } }, (result) =>
            {
            });
        }

        private async Task ShowPopupCommandExecutedAsync()
        {
            var result = await _popupService.ShowPopupAsync("HelloWorldPopup");
        }
    }
}
