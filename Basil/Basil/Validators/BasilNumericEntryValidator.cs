using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Basil.Validators
{
    public class BasilNumericEntryValidator : BasilValidation
    {
        private const string DEFAULT_ERROR_MESSAGE = "Entry must be numeric";
        private const string DEFAULT_REGEX = "";

        public BasilNumericEntryValidator()
        {
            this.Message = DEFAULT_ERROR_MESSAGE;
            this.Regex = DEFAULT_REGEX;
        }
    }
}
