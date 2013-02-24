using Basil.Colors;

namespace Basil.Controls
{
    public class BasilControl
    {
        public BasilColorSet InvalidColors { get; set; }

        public BasilColorSet ValidColors { get; set; }

        public BasilControl()
        {
            InvalidColors = new BasilColorSet(BasilColorSet.ColorState.Invalid);
            ValidColors = new BasilColorSet(BasilColorSet.ColorState.Valid);
        }
    }
}