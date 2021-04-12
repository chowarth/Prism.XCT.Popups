using System;

namespace Prism.XCT.Popups
{
    internal class PopupResult : IPopupResult
    {
        public bool LightDismissed { get; set; }

        public Exception Exception { get; set; }

        public IPopupParameters Parameters { get; set; }

    }
}
