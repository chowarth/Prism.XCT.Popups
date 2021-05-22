using System;

namespace XCT.Popups.Prism
{
    internal class PopupResult : IPopupResult
    {
        public bool LightDismissed { get; set; }

        public Exception Exception { get; set; }

        public IPopupParameters Parameters { get; set; }

    }
}
