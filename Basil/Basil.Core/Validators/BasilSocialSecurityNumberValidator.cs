namespace Basil.Validators
{
    public class BasilSocialSecurityNumberValidator : BasilValidation
    {
        private const string DEFAULT_ERROR_MESSAGE = "SSN must be valid";
        private const string DEFAULT_REGEX = "^\\d{3}\\-?\\d{2}\\-?\\d{4}$";

        public BasilSocialSecurityNumberValidator()
        {
            this.Message = DEFAULT_ERROR_MESSAGE;
            this.Regex = DEFAULT_REGEX;
        }
    }
}