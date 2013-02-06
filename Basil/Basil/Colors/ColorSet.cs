using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Basil.Colors
{
    public class BasilColorSet
    {
        private const string DEFAULT_VALID_FORE_COLOR = "#333333";
        private const string DEFAULT_VALID_BACK_COLOR = "#FFFFFF";
        private const string DEFAULT_VALID_BORDER_COLOR = "#FFFFFF";

        private const string DEFAULT_INVALID_FORE_COLOR = "#000000";
        private const string DEFAULT_INVALID_BACK_COLOR = "#F8DBDB";
        private const string DEFAULT_INVALID_BORDER_COLOR = "#B03535";

        #region Enumerations

        public enum ColorState
        {
            Valid = 1,
            Invalid = 2
        }

        #endregion


        private string _foreColor;
        public string ForeColor { get { return _foreColor; } set { _foreColor = value; } }

        private string _backColor;
        public string BackColor { get { return _backColor; } set { _backColor = value; } }

        private string _borderColor;
        public string BorderColor { get { return _borderColor; } set { _borderColor = value; } }

        public BasilColorSet(ColorState state)
        {
            switch (state)
            {
                case ColorState.Valid:
                    _foreColor = DEFAULT_VALID_FORE_COLOR;
                    _backColor = DEFAULT_VALID_BACK_COLOR;
                    _borderColor = DEFAULT_VALID_BORDER_COLOR;
                    return;

                case ColorState.Invalid:
                    _foreColor = DEFAULT_INVALID_FORE_COLOR;
                    _backColor = DEFAULT_INVALID_BACK_COLOR;
                    _borderColor = DEFAULT_INVALID_BORDER_COLOR;
                    return;

            }
        }

    }
}
