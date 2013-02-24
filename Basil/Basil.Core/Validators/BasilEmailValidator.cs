namespace Basil.Validators
{
    public class BasilEmailValidator : BasilValidation
    {
        private const string DEFAULT_ERROR_MESSAGE = "Email must be valid";
        private const string DEFAULT_REGEX = "[a-zA-Z0-9_\\-\\.]+@[a-zA-Z0-9_\\-\\.]+\\.[a-zA-Z]{2,5}";

        public BasilEmailValidator()
        {
            this.Message = DEFAULT_ERROR_MESSAGE;
            this.Regex = DEFAULT_REGEX;
        }
    }
}