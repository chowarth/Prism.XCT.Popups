using Prism.Common;

namespace XCT.Popups.Prism.Sample
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
