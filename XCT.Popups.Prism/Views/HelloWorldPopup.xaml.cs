using Xamarin.Forms.Xaml;

namespace XCT.Popups.Prism.Views
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