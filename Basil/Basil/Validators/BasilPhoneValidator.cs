namespace Basil.Validators
{
    public class BasilPhoneValidator : BasilValidation
    {
        private const string DEFAULT_ERROR_MESSAGE = "Phone number must be valid";

        private const string DEFAULT_REGEX = @"
            ^                  # From Beginning of line
            (?:\(?)            # Match but don't capture optional (
            (?<AreaCode>\d{3}) # 3 digit area code
            (?:[).\s\-]*)      # Optional ) or .
            (?<Prefix>\d{3})   # Prefix
            (?:[-\.]?)         # optional - or .
            (?<Suffix>\d{4})   # Suffix
            (?!\d)             # Fail if eleventh number found";

        public BasilPhoneValidator()
        {
            // (555)555-5555 (ok)
            // (555) 555-5555 (ok)
            // 555-555-5555 (ok)
            // 5555555555 (ok)
            // 555.555.5555 (ok)
            // 55555555556 (fail)
            // 123.456.789 (fail)

            this.Message = DEFAULT_ERROR_MESSAGE;
            this.Regex = DEFAULT_REGEX;
        }
    }
}