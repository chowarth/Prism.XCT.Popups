using Prism.Common;

namespace XCT.Popups.Prism
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
