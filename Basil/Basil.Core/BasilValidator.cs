using Basil.Helpers;
using Basil.Settings;
using Basil.Validators;
using Basil.WebControls;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Basil
{
    public class BasilValidator
    {
        #region Enumerations

        private enum ControlState
        {
            Valid = 1,
            Invalid = 2
        }

        #endregion Enumerations

        #region Properties

        public BasilSettings Settings { get; set; }

        public bool IsValid { get; set; }

        public bool AddRelEqualsTooltip { get; set; }

        #endregion Properties

        #region Control States

        private void SetControlState(WebControl control, ControlState state, string message = "")
        {
            var backColorValid = "";
            var borderColorValid = "";
            var backColorInValid = "";
            var borderColorInValid = "";

            if (control is TextBox)
            {
                backColorValid = Settings.Textbox.ValidColors.BackColor;
                borderColorValid = Settings.Textbox.ValidColors.BorderColor;
                backColorInValid = Settings.Textbox.InvalidColors.BackColor;
                borderColorInValid = Settings.Textbox.InvalidColors.BorderColor;
            }
            else if (control is CheckBox)
            {
                backColorValid = Settings.Checkbox.ValidColors.BackColor;
                borderColorValid = Settings.Checkbox.ValidColors.BorderColor;
                backColorInValid = Settings.Checkbox.InvalidColors.BackColor;
                borderColorInValid = Settings.Checkbox.InvalidColors.BorderColor;
            }
            else if (control is RadioButtonList)
            {
                backColorValid = Settings.RadioButtonList.ValidColors.BackColor;
                borderColorValid = Settings.RadioButtonList.ValidColors.BorderColor;
                backColorInValid = Settings.RadioButtonList.InvalidColors.BackColor;
                borderColorInValid = Settings.RadioButtonList.InvalidColors.BorderColor;
            }
            else if (control is DropDownList)
            {
                backColorValid = Settings.DropDownList.ValidColors.BackColor;
                borderColorValid = Settings.DropDownList.ValidColors.BorderColor;
                backColorInValid = Settings.DropDownList.InvalidColors.BackColor;
                borderColorInValid = Settings.DropDownList.InvalidColors.BorderColor;
            }

            control.ToolTip = message;

            switch (state)
            {
                case ControlState.Valid:
                    control.BackColor = System.Drawing.ColorTranslator.FromHtml(backColorValid);
                    control.BorderColor = System.Drawing.ColorTranslator.FromHtml(borderColorValid);

                    return;

                case ControlState.Invalid:
                    control.BackColor = System.Drawing.ColorTranslator.FromHtml(backColorInValid);
                    control.BorderColor = System.Drawing.ColorTranslator.FromHtml(borderColorInValid);

                    if (AddRelEqualsTooltip)
                    {
                        control.Attributes["rel"] = "tooltip";
                    }

                    // If we find an invalid rule set the validation to return false
                    IsValid = false;

                    return;
            }
        }

        #endregion Control States

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addRelEqualsTooltip"></param>
        public BasilValidator(bool addRelEqualsTooltip = false)
        {
            Settings = new BasilSettings();

            AddRelEqualsTooltip = addRelEqualsTooltip;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="addRelEqualsTooltip"></param>
        public BasilValidator(BasilSettings settings, bool addRelEqualsTooltip = false)
        {
            Settings = settings;

            AddRelEqualsTooltip = addRelEqualsTooltip;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public bool Validate(Control control)
        {
            // Default to valid
            IsValid = true;

            #region Validate Basil Types

            // Validate all BasilTextBox controls
            var btbControls = ControlHelper.GetControlsOfType<BasilTextBox>(control).Where(x => x.Required).ToList();
            var bcbControls = ControlHelper.GetControlsOfType<BasilCheckBox>(control).Where(x => x.Required).ToList();
            var bcblControls = ControlHelper.GetControlsOfType<BasilCheckBoxList>(control).Where(x => x.Required).ToList();
            var brbControls = ControlHelper.GetControlsOfType<BasilRadioButtonList>(control).Where(x => x.Required).ToList();
            var bddlControls = ControlHelper.GetControlsOfType<BasilDropDownList>(control).Where(x => x.Required).ToList();

            foreach (var ctrl in btbControls)
            {
                ctrl.Validate(this);
            }

            foreach (var ctrl in bcbControls)
            {
                ctrl.Validate(this);
            }

            foreach (var ctrl in bcblControls)
            {
                ctrl.Validate(this);
            }

            foreach (var ctrl in brbControls)
            {
                ctrl.Validate(this);
            }

            foreach (var ctrl in bddlControls)
            {
                ctrl.Validate(this);
            }

            if (!btbControls.All(x => x.IsValid)) IsValid = false;
            if (!bcbControls.All(x => x.IsValid)) IsValid = false;
            if (!bcblControls.All(x => x.IsValid)) IsValid = false;
            if (!brbControls.All(x => x.IsValid)) IsValid = false;
            if (!bddlControls.All(x => x.IsValid)) IsValid = false;

            #endregion Validate Basil Types

            #region Validate .NET Types

            // Get the list of controls we want to validate
            var cbControls = ControlHelper.GetControlsOfType<CheckBox>(control).Where(x => x.Attributes["data-required"] != null).ToList();
            var rblControls = ControlHelper.GetControlsOfType<RadioButtonList>(control).Where(x => x.Attributes["data-required"] != null).ToList();
            var ddlControls = ControlHelper.GetControlsOfType<DropDownList>(control).Where(x => x.Attributes["data-required"] != null).ToList();
            var tbControls = ControlHelper.GetControlsOfType<TextBox>(control).Where(x => x.Attributes["data-required"] != null || x.Attributes["data-type"] != null).ToList();

            // Validate checkboxes
            foreach (var cbControl in cbControls.Where(x => x.Attributes["data-required"].Equals("true", StringComparison.OrdinalIgnoreCase)))
            {
                SetControlState(cbControl, cbControl.Checked ? ControlState.Valid : ControlState.Invalid);
            }

            // Validate radiobuttons
            foreach (var rblControl in rblControls.Where(x => x.Attributes["data-required"].Equals("true", StringComparison.OrdinalIgnoreCase)))
            {
                SetControlState(rblControl, rblControl.SelectedIndex >= 0 ? ControlState.Valid : ControlState.Invalid);
            }

            // Validate drop downs
            foreach (var ddlControl in ddlControls.Where(x => x.Attributes["data-required"].Equals("true", StringComparison.OrdinalIgnoreCase)))
            {
                if (string.IsNullOrWhiteSpace(ddlControl.Text)) SetControlState(ddlControl, ControlState.Invalid, Settings.RequiredFieldValidation.Message);
                else SetControlState(ddlControl, ControlState.Valid, string.Empty);
            }

            // Validate textboxes
            foreach (var tbControl in tbControls)
            {
                var dataRequired = tbControl.Attributes["data-required"];
                var dataType = tbControl.Attributes["data-type"];

                if (dataRequired != null && dataRequired.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrWhiteSpace(tbControl.Text)) SetControlState(tbControl, ControlState.Invalid, Settings.RequiredFieldValidation.Message);
                    else SetControlState(tbControl, ControlState.Valid);
                }

                if (dataType == null) continue;

                if (dataType.Equals("zip", StringComparison.OrdinalIgnoreCase))
                {
                    if (DataTypeValidator.IsValidZipCode(tbControl.Text)) SetControlState(tbControl, ControlState.Valid);
                    else SetControlState(tbControl, ControlState.Invalid, Settings.ZipValidation.Message);
                }

                if (dataType.Equals("phone", StringComparison.OrdinalIgnoreCase))
                {
                    if (DataTypeValidator.IsValidPhoneNumber(this, tbControl.Text)) SetControlState(tbControl, ControlState.Valid);
                    else SetControlState(tbControl, ControlState.Invalid, Settings.PhoneValidation.Message);
                }

                if (dataType.Equals("email", StringComparison.OrdinalIgnoreCase))
                {
                    if (DataTypeValidator.IsValidEmailAddress(tbControl.Text)) SetControlState(tbControl, ControlState.Valid);
                    else SetControlState(tbControl, ControlState.Invalid, Settings.EmailValidation.Message);
                }

                if (dataType.Equals("ssn", StringComparison.OrdinalIgnoreCase))
                {
                    if (DataTypeValidator.IsValidSSN(this, tbControl.Text)) SetControlState(tbControl, ControlState.Valid);
                    else SetControlState(tbControl, ControlState.Invalid, Settings.SocialSecurityNumberValidation.Message);
                }

                if (dataType.Equals("number", StringComparison.OrdinalIgnoreCase))
                {
                    if (DataTypeValidator.IsValidNumber(tbControl.Text)) SetControlState(tbControl, ControlState.Valid);
                    else SetControlState(tbControl, ControlState.Invalid, Settings.NumericEntryValidation.Message);
                }

                if (dataType.Equals("date", StringComparison.OrdinalIgnoreCase))
                {
                    if (DataTypeValidator.IsValidDate(tbControl.Text)) SetControlState(tbControl, ControlState.Valid);
                    else SetControlState(tbControl, ControlState.Invalid, Settings.DateValidation.Message);
                }
            }

            #endregion Validate .NET Types

            return IsValid;
        }
    }
}
