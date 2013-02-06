using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Basil.Validators
{
    public class BasilValidation
    {
        private string _regex;
        public string Regex { get { return _regex; } set { _regex = value; } }

        private string _message;
        public string Message { get { return _message; } set { _message = value; } }
    }
}
