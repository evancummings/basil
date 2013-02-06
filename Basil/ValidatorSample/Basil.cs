using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public class Basil
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

    public Basil()
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

//public class BasilSettings
//{
//    private BasilTextbox _textbox;
//    public BasilTextbox Textbox { get { return _textbox; } set { _textbox = value; } }

//    private BasilDropDownList _dropDownList;
//    public BasilDropDownList DropDownList { get { return _dropDownList; } set { _dropDownList = value; } }

//    private BasilCheckbox _checkbox;
//    public BasilCheckbox Checkbox { get { return _checkbox; } set { _checkbox = value; } }

//    private BasilRadioButtonList _radionButtonList;
//    public BasilRadioButtonList RadioButtonList { get { return _radionButtonList; } set { _radionButtonList = value; } }

//    private BasilRequiredFieldValidator _requiredFieldValidation;
//    public BasilRequiredFieldValidator RequiredFieldValidation { get { return _requiredFieldValidation; } set { _requiredFieldValidation = value; } }

//    private BasilNumericEntryValidator _numericEntryValidation;
//    public BasilNumericEntryValidator NumericEntryValidation { get { return _numericEntryValidation; } set { _numericEntryValidation = value; } }

//    private BasilPhoneValidator _phoneValidation;
//    public BasilPhoneValidator PhoneValidation { get { return _phoneValidation; } set { _phoneValidation = value; } }

//    private BasilEmailValidator _emailValidation;
//    public BasilEmailValidator EmailValidation { get { return _emailValidation; } set { _emailValidation = value; } }

//    private BasilZipValidator _zipValidation;
//    public BasilZipValidator ZipValidation { get { return _zipValidation; } set { _zipValidation = value; } }

//    private BasilSocialSecurityNumberValidator _socialSecurityNumberValidation;
//    public BasilSocialSecurityNumberValidator SocialSecurityNumberValidation { get { return _socialSecurityNumberValidation; } set { _socialSecurityNumberValidation = value; } }


//    public BasilSettings()
//    {
//        _textbox = new BasilTextbox();
//        _dropDownList = new BasilDropDownList();
//        _checkbox = new BasilCheckbox();
//        _radionButtonList = new BasilRadioButtonList();

//        _requiredFieldValidation = new BasilRequiredFieldValidator();
//        _numericEntryValidation = new BasilNumericEntryValidator();
//        _phoneValidation = new BasilPhoneValidator();
//        _emailValidation = new BasilEmailValidator();
//        _zipValidation = new BasilZipValidator();
//        _socialSecurityNumberValidation = new BasilSocialSecurityNumberValidator();
//    }

//}

//public class BasilTextbox : BasilControl
//{
    
//}

//public class BasilDropDownList : BasilControl
//{

//}

//public class BasilCheckbox : BasilControl
//{

//}

//public class BasilRadioButtonList : BasilControl
//{

//}

//public class BasilControl
//{
//    private BasilColorSet _invalidColors;
//    public BasilColorSet InvalidColors { get { return _invalidColors; } set { _invalidColors = value; } }

//    private BasilColorSet _validColors;
//    public BasilColorSet ValidColors { get { return _validColors; } set { _validColors = value; } }    

//    public BasilControl()
//    {
//        _invalidColors = new BasilColorSet(BasilColorSet.ColorState.Invalid);
//        _validColors = new BasilColorSet(BasilColorSet.ColorState.Valid);        
//    }
//}

//public class BasilColorSet
//{
//    private const string DEFAULT_VALID_FORE_COLOR = "#333333";
//    private const string DEFAULT_VALID_BACK_COLOR = "#FFFFFF";
//    private const string DEFAULT_VALID_BORDER_COLOR = "#FFFFFF";

//    private const string DEFAULT_INVALID_FORE_COLOR = "#000000";
//    private const string DEFAULT_INVALID_BACK_COLOR = "#F8DBDB";
//    private const string DEFAULT_INVALID_BORDER_COLOR = "#B03535";

//    #region Enumerations

//    public enum ColorState
//    {
//        Valid = 1,
//        Invalid = 2
//    }

//    #endregion


//    private string _foreColor;
//    public string ForeColor { get { return _foreColor; } set { _foreColor = value; } }

//    private string _backColor;
//    public string BackColor { get { return _backColor; } set { _backColor = value; } }

//    private string _borderColor;
//    public string BorderColor { get { return _borderColor; } set { _borderColor = value; } }

//    public BasilColorSet(ColorState state)
//    {
//        switch (state)
//        {
//            case ColorState.Valid:
//                _foreColor = DEFAULT_VALID_FORE_COLOR;
//                _backColor = DEFAULT_VALID_BACK_COLOR;
//                _borderColor = DEFAULT_VALID_BORDER_COLOR;
//            return;

//            case ColorState.Invalid:
//                _foreColor = DEFAULT_INVALID_FORE_COLOR;
//                _backColor = DEFAULT_INVALID_BACK_COLOR;
//                _borderColor = DEFAULT_INVALID_BORDER_COLOR;
//            return;

//        }
//    }

//}


//public class BasilValidation
//{
//    private string _regex;
//    public string Regex { get { return _regex; } set { _regex = value; }}

//    private string _message;
//    public string Message { get { return _message; } set { _message = value; } }
//}

//public class BasilPhoneValidator : BasilValidation
//{
//    private const string DEFAULT_ERROR_MESSAGE = "Phone number must be valid";
//    private const string DEFAULT_REGEX = "\\(\\d{3}\\)\\s\\d{3}-\\d{4}$";

//    public BasilPhoneValidator()
//    {
//        this.Message = DEFAULT_ERROR_MESSAGE;
//        this.Regex = DEFAULT_REGEX;
//    }
//}

//public class BasilEmailValidator : BasilValidation
//{
//    private const string DEFAULT_ERROR_MESSAGE = "Email must be valid";
//    private const string DEFAULT_REGEX = "[a-zA-Z0-9_\\-\\.]+@[a-zA-Z0-9_\\-\\.]+\\.[a-zA-Z]{2,5}";

//    public BasilEmailValidator()
//    {
//        this.Message = DEFAULT_ERROR_MESSAGE;
//        this.Regex = DEFAULT_REGEX;
//    }
//}

//public class BasilZipValidator : BasilValidation
//{
//    private const string DEFAULT_ERROR_MESSAGE = "Zip must be valid";
//    private const string DEFAULT_REGEX = "";

//    public BasilZipValidator()
//    {
//        this.Message = DEFAULT_ERROR_MESSAGE;
//        this.Regex = DEFAULT_REGEX;
//    }
//}

//public class BasilSocialSecurityNumberValidator : BasilValidation
//{
//    private const string DEFAULT_ERROR_MESSAGE = "SSN must be valid";
//    private const string DEFAULT_REGEX = "^\\d{3}\\-?\\d{2}\\-?\\d{4}$";

//    public BasilSocialSecurityNumberValidator()
//    {
//        this.Message = DEFAULT_ERROR_MESSAGE;
//        this.Regex = DEFAULT_REGEX;
//    }
//}

//public class BasilRequiredFieldValidator : BasilValidation
//{
//    private const string DEFAULT_ERROR_MESSAGE = "This field is required";
//    private const string DEFAULT_REGEX = "";

//    public BasilRequiredFieldValidator()
//    {
//        this.Message = DEFAULT_ERROR_MESSAGE;
//        this.Regex = DEFAULT_REGEX;
//    }
//}

//public class BasilNumericEntryValidator : BasilValidation
//{
//    private const string DEFAULT_ERROR_MESSAGE = "Entry must be numeric";
//    private const string DEFAULT_REGEX = "";

//    public BasilNumericEntryValidator()
//    {
//        this.Message = DEFAULT_ERROR_MESSAGE;
//        this.Regex = DEFAULT_REGEX;
//    }
//}