using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Basil.Validators
{
    public class BasilPhoneValidator : BasilValidation
    {
        private const string DEFAULT_ERROR_MESSAGE = "Phone number must be valid";
        private const string DEFAULT_REGEX = "\\(\\d{3}\\)\\s\\d{3}-\\d{4}$";

        public BasilPhoneValidator()
        {
            this.Message = DEFAULT_ERROR_MESSAGE;
            this.Regex = DEFAULT_REGEX;
        }
    }
}
