using Basil.Controls;
using Basil.Validators;

namespace Basil.Settings
{
    public class BasilSettings
    {
        #region Properties

        public BasilTextbox Textbox { get; set; }

        public BasilDropDownList DropDownList { get; set; }

        public BasilCheckbox Checkbox { get; set; }

        public BasilRadioButtonList RadioButtonList { get; set; }

        public BasilRequiredFieldValidator RequiredFieldValidation { get; set; }

        public BasilNumericEntryValidator NumericEntryValidation { get; set; }

        public BasilPhoneValidator PhoneValidation { get; set; }

        public BasilEmailValidator EmailValidation { get; set; }

        public BasilZipValidator ZipValidation { get; set; }

        public BasilSocialSecurityNumberValidator SocialSecurityNumberValidation { get; set; }

        public BasilDateValidator DateValidation { get; set; }

        #endregion Properties

        public BasilSettings()
        {
            Textbox = new BasilTextbox();
            DropDownList = new BasilDropDownList();
            Checkbox = new BasilCheckbox();
            RadioButtonList = new BasilRadioButtonList();

            RequiredFieldValidation = new BasilRequiredFieldValidator();
            NumericEntryValidation = new BasilNumericEntryValidator();
            PhoneValidation = new BasilPhoneValidator();
            EmailValidation = new BasilEmailValidator();
            ZipValidation = new BasilZipValidator();
            SocialSecurityNumberValidation = new BasilSocialSecurityNumberValidator();
            DateValidation = new BasilDateValidator();
        }
    }
}