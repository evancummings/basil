using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace ValidatorSample
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region "Validation"

        enum ControlState
        {
            Valid = 1,
            Invalid = 2
        }

        private void SetControlState(CheckBox txt, ControlState state)
        {
            txt.Attributes["title"] = string.Empty;
            switch (state)
            {
                case ControlState.Valid:
                    txt.BackColor = System.Drawing.ColorTranslator.FromHtml("");
                    txt.ForeColor = System.Drawing.ColorTranslator.FromHtml("#333333");
                    return;
                case ControlState.Invalid:
                    txt.BackColor = System.Drawing.ColorTranslator.FromHtml("#f8dbdb");
                    txt.ForeColor = System.Drawing.ColorTranslator.FromHtml("#b03535");
                    return;
            }
        }

        private void SetControlState(RadioButtonList txt, ControlState state)
        {
            txt.Attributes["title"] = string.Empty;
            switch (state)
            {
                case ControlState.Valid:
                    txt.BackColor = System.Drawing.ColorTranslator.FromHtml("");
                    txt.ForeColor = System.Drawing.ColorTranslator.FromHtml("#333333");
                    return;
                case ControlState.Invalid:
                    txt.BackColor = System.Drawing.ColorTranslator.FromHtml("#f8dbdb");
                    txt.ForeColor = System.Drawing.ColorTranslator.FromHtml("#b03535");
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
                    txt.BackColor = System.Drawing.Color.White;
                    txt.BorderColor = System.Drawing.ColorTranslator.FromHtml("");
                    return;
                case ControlState.Invalid:
                    txt.BackColor = System.Drawing.ColorTranslator.FromHtml("#f8dbdb");
                    txt.BorderColor = System.Drawing.ColorTranslator.FromHtml("#b03535");
                    return;
            }
        }

        private void SetControlState(TextBox txt, ControlState state)
        {
            txt.Attributes["title"] = string.Empty;
            switch (state)
            {
                case ControlState.Valid:
                    txt.BackColor = System.Drawing.Color.White;
                    txt.BorderColor = System.Drawing.ColorTranslator.FromHtml("");
                    return;
                case ControlState.Invalid:
                    txt.BackColor = System.Drawing.ColorTranslator.FromHtml("#f8dbdb");
                    txt.BorderColor = System.Drawing.ColorTranslator.FromHtml("#b03535");
                    return;
            }
        }
        
        private void SetControlState(TextBox txt, ControlState state, string message)
        {
            txt.Attributes["title"] = message;

            switch (state)
            {
                case ControlState.Valid:
                    txt.BackColor = System.Drawing.Color.White;
                    txt.BorderColor = System.Drawing.ColorTranslator.FromHtml("");
                    return;
                case ControlState.Invalid:
                    txt.BackColor = System.Drawing.ColorTranslator.FromHtml("#f8dbdb");
                    txt.BorderColor = System.Drawing.ColorTranslator.FromHtml("#b03535");
                    return;
            }
        }

        private bool ValidateWizardStep(Panel pnl)
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
                            SetControlState(tempCtl, ControlState.Invalid, "This field is required");
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
                            SetControlState(tempCtl, ControlState.Invalid, "This field is required");
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
                            SetControlState(tempCtl, ControlState.Invalid, "ZIP must be valid");
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
                            SetControlState(tempCtl, ControlState.Invalid, "Phone number must be valid");
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
                            SetControlState(tempCtl, ControlState.Invalid, "Email must be valid");
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
                            SetControlState(tempCtl, ControlState.Invalid, "SSN must be valid");
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
                            SetControlState(tempCtl, ControlState.Invalid, "Entry must be numeric");
                            isWizardValid = false;
                            isComponentValid = false;
                        }
                    }                    
                }
            }
            //Page.ClientScript.RegisterStartupScript(Me.GetType(), "asdasd", "ShowInvalidInput()", true)
            return isWizardValid;
        }

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
            Regex matchRegex = new Regex("\\(\\d{3}\\)\\s\\d{3}-\\d{4}$");
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
            Regex matchRegex = new Regex("[a-zA-Z0-9_\\-\\.]+@[a-zA-Z0-9_\\-\\.]+\\.[a-zA-Z]{2,5}");
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
            Regex matchregex = new Regex("^\\d{3}\\-?\\d{2}\\-?\\d{4}$");
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Basil.BasilValidator validator = new Basil.BasilValidator();

            validator.Validate(pnlForm);
            //ValidateWizardStep(pnlForm);
        }
    }
}

    
