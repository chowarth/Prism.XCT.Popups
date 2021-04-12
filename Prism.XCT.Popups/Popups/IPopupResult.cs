using System;

namespace Prism.XCT.Popups
{
    public interface IPopupResult
    {
        bool LightDismissed { get; }
        Exception Exception { get; }
        IPopupParameters Parameters { get; }
    }
}
