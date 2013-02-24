namespace Basil.Validators
{
    public class BasilRequiredFieldValidator : BasilValidation
    {
        private const string DEFAULT_ERROR_MESSAGE = "This field is required";
        private const string DEFAULT_REGEX = "";

        public BasilRequiredFieldValidator()
        {
            this.Message = DEFAULT_ERROR_MESSAGE;
            this.Regex = DEFAULT_REGEX;
        }
    }
}