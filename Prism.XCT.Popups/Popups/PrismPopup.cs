using System;
using Xamarin.CommunityToolkit.UI.Views;

namespace Prism.XCT.Popups
{
    public class PrismPopup : Popup
    {
        internal Action LightDismissed;

        protected override void LightDismiss()
        {
            base.LightDismiss();

            LightDismissed?.Invoke();
        }
    }
}
