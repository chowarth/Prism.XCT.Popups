using Xamarin.Forms.Xaml;

namespace XCT.Popups.Prism.Sample.Views
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