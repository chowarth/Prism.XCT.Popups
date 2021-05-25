using System;

namespace XCT.Popups.Prism
{
    public class PopupException : Exception
    {
        public const string ShowPopup = "Error while displaying popup";

        public const string DismissPopup = "Error while dismissing popup";

        public const string NoViewModel = "No ViewModel could be found";

        public const string ImplementIPopupAware = "The ViewModel does not implement IPopupAware";

        public const string ImplementIPopupLightDismissAware = "The ViewModel does not implement IPopupLightDismissAware";

        public PopupException(string message)
            : base(message)
        {
        }

        public PopupException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
