using System;

namespace XCT.Popups.Prism.Sample
{
    internal class PopupResult : IPopupResult
    {
        public bool LightDismissed { get; set; }

        public Exception Exception { get; set; }

        public IPopupParameters Parameters { get; set; }

    }
}
