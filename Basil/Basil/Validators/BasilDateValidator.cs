namespace Basil.Validators
{
    public class BasilDateValidator : BasilValidation
    {
        private const string DEFAULT_ERROR_MESSAGE = "Date must be valid";

        public BasilDateValidator()
        {
            this.Message = DEFAULT_ERROR_MESSAGE;
        }
    }
}