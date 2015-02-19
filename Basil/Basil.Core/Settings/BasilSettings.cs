using Basil.Validators;

namespace Basil.Settings
{
    public class BasilSettings
    {
        #region Properties

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