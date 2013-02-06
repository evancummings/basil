using Basil.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Basil
{
    public class BasilValidator
    {
        #region Instance Variables

        private BasilSettings _settings;


        #endregion

        #region Enumerations

        enum ControlState
        {
            Valid = 1,
            Invalid = 2
        }

        #endregion

        #region Properties

        public BasilSettings Settings { get { return _settings; } set { _settings = value; } }


        #endregion

        #region Control States
        private void SetControlState(CheckBox txt, ControlState state)
        {
            txt.Attributes["title"] = string.Empty;
            switch (state)
            {
                case ControlState.Valid:
                    txt.BackColor = System.Drawing.ColorTranslator.FromHtml(_settings.Checkbox.ValidColors.BackColor);
                    txt.ForeColor = System.Drawing.ColorTranslator.FromHtml(_settings.Checkbox.ValidColors.ForeColor);
                    return;
                case ControlState.Invalid:
                    txt.BackColor = System.Drawing.ColorTranslator.FromHtml(_settings.Checkbox.InvalidColors.BackColor);
                    txt.ForeColor = System.Drawing.ColorTranslator.FromHtml(_settings.Checkbox.InvalidColors.ForeColor);
                    return;
            }
        }

        private void SetControlState(RadioButtonList txt, ControlState state)
        {
            txt.Attributes["title"] = string.Empty;
            switch (state)
            {
                case ControlState.Valid:
                    txt.BackColor = System.Drawing.ColorTranslator.FromHtml(_settings.RadioButtonList.ValidColors.BackColor);
                    txt.ForeColor = System.Drawing.ColorTranslator.FromHtml(_settings.RadioButtonList.ValidColors.ForeColor);
                    return;
                case ControlState.Invalid:
                    txt.BackColor = System.Drawing.ColorTranslator.FromHtml(_settings.RadioButtonList.InvalidColors.BackColor);
                    txt.ForeColor = System.Drawing.ColorTranslator.FromHtml(_settings.RadioButtonList.InvalidColors.ForeColor);
                    return;
            }
        }

        private void SetControlState(DropDownList txt, ControlState state, string message)
        {
            //txt.Attributes("title") = message
            txt.ToolTip = message;
            switch (state)
            {
                case ControlState.Valid:
                    txt.BackColor = System.Drawing.ColorTranslator.FromHtml(_settings.DropDownList.ValidColors.BackColor);
                    txt.BorderColor = System.Drawing.ColorTranslator.FromHtml(_settings.DropDownList.ValidColors.BorderColor);
                    return;
                case ControlState.Invalid:
                    txt.BackColor = System.Drawing.ColorTranslator.FromHtml(_settings.DropDownList.InvalidColors.BackColor);
                    txt.BorderColor = System.Drawing.ColorTranslator.FromHtml(_settings.DropDownList.InvalidColors.BorderColor);
                    return;
            }
        }

        private void SetControlState(TextBox txt, ControlState state)
        {
            txt.Attributes["title"] = string.Empty;
            switch (state)
            {
                case ControlState.Valid:
                    txt.BackColor = System.Drawing.ColorTranslator.FromHtml(_settings.Textbox.ValidColors.BackColor);
                    txt.BorderColor = System.Drawing.ColorTranslator.FromHtml(_settings.Textbox.ValidColors.BorderColor);
                    return;
                case ControlState.Invalid:
                    txt.BackColor = System.Drawing.ColorTranslator.FromHtml(_settings.Textbox.InvalidColors.BackColor);
                    txt.BorderColor = System.Drawing.ColorTranslator.FromHtml(_settings.Textbox.InvalidColors.BorderColor);
                    return;
            }
        }

        private void SetControlState(TextBox txt, ControlState state, string message)
        {
            txt.Attributes["title"] = message;

            switch (state)
            {
                case ControlState.Valid:
                    txt.BackColor = System.Drawing.ColorTranslator.FromHtml(_settings.Textbox.ValidColors.BackColor);
                    txt.BorderColor = System.Drawing.ColorTranslator.FromHtml(_settings.Textbox.ValidColors.BorderColor);
                    return;
                case ControlState.Invalid:
                    txt.BackColor = System.Drawing.ColorTranslator.FromHtml(_settings.Textbox.InvalidColors.BackColor);
                    txt.BorderColor = System.Drawing.ColorTranslator.FromHtml(_settings.Textbox.InvalidColors.BorderColor);
                    return;
            }
        }
        #endregion

        #region Type Validators
        private bool IsNumeric(string num)
        {
            double myNum = 0;

            return Double.TryParse(num, out myNum);

        }

        private bool IsValidZipCode(string str_zip_code)
        {
            return IsNumeric(str_zip_code);
        }

        private bool IsValidPhoneNumber(string str_phone_no)
        {
            Regex matchRegex = new Regex(_settings.PhoneValidation.Regex);
            MatchCollection matches = matchRegex.Matches(str_phone_no);
            if (matches.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsValidEmailAddress(string email_id)
        {
            Regex matchRegex = new Regex(_settings.EmailValidation.Regex);
            MatchCollection matches = matchRegex.Matches(email_id);
            if (matches.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsValidSSN(string ssn)
        {
            //Return (ssn Like "###-##-####") Or (ssn Like "#########")
            Regex matchregex = new Regex(_settings.SocialSecurityNumberValidation.Regex);
            MatchCollection matches = matchregex.Matches(ssn);
            if (matches.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsValidNumber(string num)
        {
            return IsNumeric(num);
        }

        #endregion

        public BasilValidator()
        {
            _settings = new BasilSettings();
        }


        public bool Validate(Panel pnl)
        {
            bool isWizardValid = true;
            foreach (Control ctl in pnl.Controls)
            {
                bool isComponentValid = true;
                if ((ctl) is CheckBox)
                {
                    CheckBox tempCtl = (CheckBox)ctl;
                    if (tempCtl.Attributes["data-required"] == "true" & isComponentValid)
                    {
                        if (tempCtl.Checked == true)
                        {
                            SetControlState(tempCtl, ControlState.Valid);
                        }
                        else
                        {
                            SetControlState(tempCtl, ControlState.Invalid);
                            isWizardValid = false;
                            isComponentValid = false;
                        }
                    }
                }
                if ((ctl) is RadioButtonList)
                {
                    RadioButtonList tempCtl = (RadioButtonList)ctl;
                    if (tempCtl.Attributes["data-required"] == "true" & isComponentValid)
                    {
                        if (tempCtl.SelectedIndex >= 0)
                        {
                            SetControlState(tempCtl, ControlState.Valid);
                        }
                        else
                        {
                            SetControlState(tempCtl, ControlState.Invalid);
                            isWizardValid = false;
                            isComponentValid = false;
                        }
                    }
                }
                if ((ctl) is DropDownList)
                {
                    DropDownList tempCtl = (DropDownList)ctl;

                    if (tempCtl.Attributes["data-required"] == "true" & isComponentValid)
                    {
                        if (string.IsNullOrWhiteSpace(tempCtl.Text))
                        {
                            SetControlState(tempCtl, ControlState.Invalid, _settings.RequiredFieldValidation.Message);
                            isWizardValid = false;
                            isComponentValid = false;
                        }
                        else
                        {
                            SetControlState(tempCtl, ControlState.Valid, string.Empty);
                        }
                    }
                }
                if ((ctl) is TextBox)
                {
                    TextBox tempCtl = (TextBox)ctl;

                    if (tempCtl.Attributes["data-required"] == "true" & isComponentValid)
                    {
                        if (string.IsNullOrWhiteSpace(tempCtl.Text))
                        {
                            SetControlState(tempCtl, ControlState.Invalid, _settings.RequiredFieldValidation.Message);
                            isWizardValid = false;
                            isComponentValid = false;
                        }
                        else
                        {
                            SetControlState(tempCtl, ControlState.Valid);
                        }
                    }

                    if (tempCtl.Attributes["data-type"] == "zip" & isComponentValid)
                    {
                        if (IsValidZipCode(tempCtl.Text))
                        {
                            SetControlState(tempCtl, ControlState.Valid);
                        }
                        else
                        {
                            SetControlState(tempCtl, ControlState.Invalid, _settings.ZipValidation.Message);
                            isWizardValid = false;
                            isComponentValid = false;
                        }
                    }

                    if (tempCtl.Attributes["data-type"] == "phone" & isComponentValid)
                    {
                        if (IsValidPhoneNumber(tempCtl.Text))
                        {
                            SetControlState(tempCtl, ControlState.Valid);
                        }
                        else
                        {
                            SetControlState(tempCtl, ControlState.Invalid, _settings.PhoneValidation.Message);
                            isWizardValid = false;
                            isComponentValid = false;
                        }
                    }

                    if (tempCtl.Attributes["data-type"] == "email" & isComponentValid)
                    {
                        if (IsValidEmailAddress(tempCtl.Text))
                        {
                            SetControlState(tempCtl, ControlState.Valid);
                        }
                        else
                        {
                            SetControlState(tempCtl, ControlState.Invalid, _settings.EmailValidation.Message);
                            isWizardValid = false;
                            isComponentValid = false;
                        }
                    }

                    if (tempCtl.Attributes["data-type"] == "ssn" & isComponentValid)
                    {
                        if (IsValidSSN(tempCtl.Text))
                        {
                            SetControlState(tempCtl, ControlState.Valid);
                        }
                        else
                        {
                            SetControlState(tempCtl, ControlState.Invalid, _settings.SocialSecurityNumberValidation.Message);
                            isWizardValid = false;
                            isComponentValid = false;
                        }
                    }

                    if (tempCtl.Attributes["data-type"] == "number" & isComponentValid)
                    {
                        if (IsValidNumber(tempCtl.Text))
                        {
                            SetControlState(tempCtl, ControlState.Valid);
                        }
                        else
                        {
                            SetControlState(tempCtl, ControlState.Invalid, _settings.NumericEntryValidation.Message);
                            isWizardValid = false;
                            isComponentValid = false;
                        }
                    }
                }
            }

            return isWizardValid;
        }


    }
}
