using Basil.Colors;

namespace Basil.Controls
{
    public class BasilControl
    {
        [System.Obsolete("InvalidColors is obsolete. Use ErrorCssClass instead.")]
        public BasilColorSet InvalidColors { get; set; }

        [System.Obsolete("Valid Colors is obsolete. Use ErrorCssClass instead.")]
        public BasilColorSet ValidColors { get; set; }

        public string ErrorCssClass { get; set; }

        public string WarningCssClass { get; set; }

        public string InfoCssClass { get; set; }

        public string SuccessCssClass { get; set; }

        public BasilControl()
        {
            InvalidColors = new BasilColorSet(BasilColorSet.ColorState.Invalid);
            ValidColors = new BasilColorSet(BasilColorSet.ColorState.Valid);
            ErrorCssClass = "error";
            WarningCssClass = "warning";
            InfoCssClass = "info";
            SuccessCssClass = "success";
        }
    }
}