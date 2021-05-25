using System;

namespace XCT.Popups.Prism
{
    public interface IPopupAware
    {
        event Action<IPopupParameters> RequestDismiss;

        void OnPopupOpened(IPopupParameters parameters);

        void OnPopupDismissed();
    }
}
