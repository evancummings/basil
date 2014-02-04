using Basil.Colors;
using Basil.Helpers;

namespace Basil.Controls
{
    public class BasilControl
    {
        [System.Obsolete("InvalidColors is obsolete. Use ErrorCssClass instead.")]
        public BasilColorSet InvalidColors { get; set; }

        [System.Obsolete("ValidColors is obsolete. Use SucessCssClass instead.")]
        public BasilColorSet ValidColors { get; set; }

        public string ErrorCssClass { get; set; }

        public string WarningCssClass { get; set; }

        public string InfoCssClass { get; set; }

        public string SuccessCssClass { get; set; }

        public BasilControl()
        {
            InvalidColors = new BasilColorSet(BasilColorSet.ColorState.Invalid);
            ValidColors = new BasilColorSet(BasilColorSet.ColorState.Valid);
            ErrorCssClass = BootstrapHelper.FormGroupErrorClass;
            WarningCssClass = BootstrapHelper.FormGroupWarningClass;
            InfoCssClass = BootstrapHelper.FormGroupInfoClass;
            SuccessCssClass = BootstrapHelper.FormGroupSuccessClass;
        }
    }
}