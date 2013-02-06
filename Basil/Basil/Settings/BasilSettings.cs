using Basil.Controls;
using Basil.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Basil.Settings
{
    public class BasilSettings
    {
        private BasilTextbox _textbox;
        public BasilTextbox Textbox { get { return _textbox; } set { _textbox = value; } }

        private BasilDropDownList _dropDownList;
        public BasilDropDownList DropDownList { get { return _dropDownList; } set { _dropDownList = value; } }

        private BasilCheckbox _checkbox;
        public BasilCheckbox Checkbox { get { return _checkbox; } set { _checkbox = value; } }

        private BasilRadioButtonList _radionButtonList;
        public BasilRadioButtonList RadioButtonList { get { return _radionButtonList; } set { _radionButtonList = value; } }

        private BasilRequiredFieldValidator _requiredFieldValidation;
        public BasilRequiredFieldValidator RequiredFieldValidation { get { return _requiredFieldValidation; } set { _requiredFieldValidation = value; } }

        private BasilNumericEntryValidator _numericEntryValidation;
        public BasilNumericEntryValidator NumericEntryValidation { get { return _numericEntryValidation; } set { _numericEntryValidation = value; } }

        private BasilPhoneValidator _phoneValidation;
        public BasilPhoneValidator PhoneValidation { get { return _phoneValidation; } set { _phoneValidation = value; } }

        private BasilEmailValidator _emailValidation;
        public BasilEmailValidator EmailValidation { get { return _emailValidation; } set { _emailValidation = value; } }

        private BasilZipValidator _zipValidation;
        public BasilZipValidator ZipValidation { get { return _zipValidation; } set { _zipValidation = value; } }

        private BasilSocialSecurityNumberValidator _socialSecurityNumberValidation;
        public BasilSocialSecurityNumberValidator SocialSecurityNumberValidation { get { return _socialSecurityNumberValidation; } set { _socialSecurityNumberValidation = value; } }


        public BasilSettings()
        {
            _textbox = new BasilTextbox();
            _dropDownList = new BasilDropDownList();
            _checkbox = new BasilCheckbox();
            _radionButtonList = new BasilRadioButtonList();

            _requiredFieldValidation = new BasilRequiredFieldValidator();
            _numericEntryValidation = new BasilNumericEntryValidator();
            _phoneValidation = new BasilPhoneValidator();
            _emailValidation = new BasilEmailValidator();
            _zipValidation = new BasilZipValidator();
            _socialSecurityNumberValidation = new BasilSocialSecurityNumberValidator();
        }

    }

}
