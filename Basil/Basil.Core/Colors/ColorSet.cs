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

        #endregion Enumerations

        public string ForeColor { get; set; }

        public string BackColor { get; set; }

        public string BorderColor { get; set; }

        public BasilColorSet(ColorState state)
        {
            switch (state)
            {
                case ColorState.Valid:
                    ForeColor = DEFAULT_VALID_FORE_COLOR;
                    BackColor = DEFAULT_VALID_BACK_COLOR;
                    BorderColor = DEFAULT_VALID_BORDER_COLOR;
                    return;

                case ColorState.Invalid:
                    ForeColor = DEFAULT_INVALID_FORE_COLOR;
                    BackColor = DEFAULT_INVALID_BACK_COLOR;
                    BorderColor = DEFAULT_INVALID_BORDER_COLOR;
                    return;
            }
        }
    }
}