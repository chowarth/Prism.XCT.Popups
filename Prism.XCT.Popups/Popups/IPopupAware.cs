using System;

namespace Prism.XCT.Popups
{
    public interface IPopupAware
    {
        event Action<IPopupParameters> RequestDismiss;

        void OnPopupOpened(IPopupParameters parameters);

        void OnPopupDismissed();
    }
}
