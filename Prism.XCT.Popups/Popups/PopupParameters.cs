using Prism.Common;

namespace Prism.XCT.Popups
{
    public class PopupParameters : ParametersBase, IPopupParameters
    {
        public PopupParameters()
        {
        }

        public PopupParameters(string query)
            : base(query)
        {
        }
    }
}
