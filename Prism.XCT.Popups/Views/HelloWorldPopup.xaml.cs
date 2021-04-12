using Xamarin.Forms.Xaml;

namespace Prism.XCT.Popups.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HelloWorldPopup : PrismPopup
    {
        public HelloWorldPopup()
        {
            InitializeComponent();
        }
    }
}