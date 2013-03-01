using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basil.Enums;
using Basil.Settings;

namespace Basil.Helpers
{
    public static class MessageHelper
    {

        public static string GetMessage(RequiredTypes type, BasilSettings settings)
        {

            if (settings == null) return String.Empty;

            switch (type)
            {
                case RequiredTypes.Text:
                    return settings.RequiredFieldValidation.Message;                   

                case RequiredTypes.Zip:
                    return settings.ZipValidation.Message;
                    

                case RequiredTypes.Phone:
                    return settings.PhoneValidation.Message;
                    

                case RequiredTypes.Email:
                    return settings.EmailValidation.Message;
                    

                case RequiredTypes.SSN:
                    return settings.SocialSecurityNumberValidation.Message;
                    

                case RequiredTypes.Number:
                    return settings.NumericEntryValidation.Message;
                    

                case RequiredTypes.Date:
                    return settings.DateValidation.Message;                    
            }

            return null;
        }
    }
}
