using System;

namespace XCT.Popups.Prism.Sample
{
    public interface IPopupResult
    {
        bool LightDismissed { get; }
        Exception Exception { get; }
        IPopupParameters Parameters { get; }
    }
}
