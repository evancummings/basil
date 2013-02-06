using Basil.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Basil.Controls
{
    public class BasilControl
    {
        private BasilColorSet _invalidColors;
        public BasilColorSet InvalidColors { get { return _invalidColors; } set { _invalidColors = value; } }

        private BasilColorSet _validColors;
        public BasilColorSet ValidColors { get { return _validColors; } set { _validColors = value; } }

        public BasilControl()
        {
            _invalidColors = new BasilColorSet(BasilColorSet.ColorState.Invalid);
            _validColors = new BasilColorSet(BasilColorSet.ColorState.Valid);
        }
    }
}
