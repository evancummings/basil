﻿using Basil.Enums;
using Basil.Validators;
using System.Configuration;
using System.Linq;
using System.Web;

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
                            return BootstrapVersions.V2;

                        case "3":
                            return BootstrapVersions.V3;
                    }
                }

                return BootstrapVersions.V2;
            }
        }

        public static bool IsPdfContext => (HttpContext.Current != null && HttpContext.Current.Request.Headers.AllKeys.Contains("EOPDF"));

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