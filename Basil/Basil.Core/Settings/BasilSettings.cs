﻿using Basil.Controls;
using Basil.Enums;
using Basil.Validators;
using System.Configuration;

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

        private static BootstrapVersions _bootstrapVersion;

        public static BootstrapVersions BootstrapVersion
        {
            get
            {
                var setting = ConfigurationManager.AppSettings["Basil.Bootstrap.Version"];
                if (setting != null)
                {
                    switch (setting)
                    {
                        case "2":
                            _bootstrapVersion = BootstrapVersions.V2;
                            break;

                        case "3":
                            _bootstrapVersion = BootstrapVersions.V3;
                            break;
                    }
                }
                else _bootstrapVersion = BootstrapVersions.V2;

                return _bootstrapVersion;
            }
            set { _bootstrapVersion = value; }
        }

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