using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Basil.Validators
{
    public static class DataTypeValidator
    {
        public static bool IsNumeric(string num)
        {
            double myNum = 0;

            return Double.TryParse(num, out myNum);
        }

        public static bool IsValidZipCode(string zip)
        {
            return IsNumeric(zip);
        }

        public static bool IsValidPhoneNumber(BasilValidator validator, string phone)
        {
            var matchRegex = new Regex(validator.Settings.PhoneValidation.Regex, RegexOptions.IgnorePatternWhitespace);
            var matches = matchRegex.Matches(phone);

            return matches.Count != 0;
        }

        public static bool IsValidEmailAddress(string email)
        {
            try
            {
                // This will throw an exception if an invalid email address is passed
                new MailAddress(email);

                return true;
            }
            catch
            {
            }

            return false;
        }

        public static bool IsValidSSN(BasilValidator validator, string ssn)
        {
            //Return (ssn Like "###-##-####") Or (ssn Like "#########")
            var matchregex = new Regex(validator.Settings.SocialSecurityNumberValidation.Regex);
            var matches = matchregex.Matches(ssn);

            return matches.Count != 0;
        }

        public static bool IsValidNumber(string num)
        {
            return IsNumeric(num);
        }

        public static bool IsValidDate(string text)
        {
            DateTime temp;

            return DateTime.TryParse(text, out temp);
        }
    }
}