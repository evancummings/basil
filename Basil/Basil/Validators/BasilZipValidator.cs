namespace Basil.Validators
{
    public class BasilZipValidator : BasilValidation
    {
        private const string DEFAULT_ERROR_MESSAGE = "Zip must be valid";
        private const string DEFAULT_REGEX = "";

        public BasilZipValidator()
        {
            this.Message = DEFAULT_ERROR_MESSAGE;
            this.Regex = DEFAULT_REGEX;
        }
    }
}