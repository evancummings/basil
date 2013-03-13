using Basil.Enums;
using Basil.Interfaces;
using Basil.Validators;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Basil.WebControls
{
    public class BasilTextBox : TextBox, IBasilWebControl
    {
        #region Properties

        public string Label { get; set; }

        private string _errorMessage;

        public string ErrorMessage
        {
            get
            {
                if (string.IsNullOrEmpty(_errorMessage))
                {
                    switch (RequiredType)
                    {
                        case RequiredTypes.Text:
                            _errorMessage = Validator.Settings.RequiredFieldValidation.Message;
                            break;

                        case RequiredTypes.Zip:
                            _errorMessage = Validator.Settings.ZipValidation.Message;
                            break;

                        case RequiredTypes.Phone:
                            _errorMessage = Validator.Settings.PhoneValidation.Message;
                            break;

                        case RequiredTypes.Email:
                            _errorMessage = Validator.Settings.EmailValidation.Message;
                            break;

                        case RequiredTypes.SSN:
                            _errorMessage = Validator.Settings.SocialSecurityNumberValidation.Message;
                            break;

                        case RequiredTypes.Number:
                            _errorMessage = Validator.Settings.NumericEntryValidation.Message;
                            break;

                        case RequiredTypes.Date:
                            _errorMessage = Validator.Settings.DateValidation.Message;
                            break;
                    }
                }

                return _errorMessage;
            }
            set { _errorMessage = value; }
        }

        public RequiredTypes RequiredType { get; set; }

        public bool Required { get; set; }

        public bool IsValid { get; set; }

        public bool IsWarning { get; set; }

        public bool IsInfo { get; set; }

        public bool IsSuccess { get; set; }

        public bool RenderControlGroupMarkup { get; set; }

        public BasilValidator Validator { get; set; }

        #endregion Properties

        public BasilTextBox()
        {
            IsValid = true;
            Required = false;
            RenderControlGroupMarkup = true;
            RequiredType = RequiredTypes.Text;
        }

        public void Validate(BasilValidator validator = null)
        {
            Validator = validator;
            IsValid = true;

            if (!Required) return;

            switch (RequiredType)
            {
                case RequiredTypes.Text:
                    IsValid = !string.IsNullOrEmpty(Text);
                    break;

                case RequiredTypes.Zip:
                    IsValid = DataTypeValidator.IsValidZipCode(Text);
                    break;

                case RequiredTypes.Phone:
                    IsValid = DataTypeValidator.IsValidPhoneNumber(Validator, Text);
                    break;

                case RequiredTypes.Email:
                    IsValid = DataTypeValidator.IsValidEmailAddress(Text);
                    break;

                case RequiredTypes.SSN:
                    IsValid = DataTypeValidator.IsValidSSN(Validator, Text);
                    break;

                case RequiredTypes.Number:
                    IsValid = DataTypeValidator.IsValidNumber(Text);
                    break;

                case RequiredTypes.Date:
                    IsValid = DataTypeValidator.IsValidDate(Text);
                    break;
            }

            if (!IsValid && Validator != null && !string.IsNullOrEmpty(Label))
            {
                Validator.Errors.Add(string.Format("<strong>{0}:</strong> {1}", Label, ErrorMessage));
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (RenderControlGroupMarkup)
            {
                var cssClass = "control-group";

                if (!IsValid) cssClass += string.Format(" {0}", Validator.Settings.Textbox.ErrorCssClass);
                if (IsWarning) cssClass += string.Format(" {0}", Validator.Settings.Textbox.WarningCssClass);
                if (IsInfo) cssClass += string.Format(" {0}", Validator.Settings.Textbox.InfoCssClass);
                if (IsSuccess) cssClass += string.Format(" {0}", Validator.Settings.Textbox.SuccessCssClass);

                writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                if (!string.IsNullOrEmpty(Label))
                {
                    var labelCssClass = (Required) ? "control-label required" : "control-label";

                    writer.AddAttribute(HtmlTextWriterAttribute.Class, labelCssClass);
                    writer.AddAttribute(HtmlTextWriterAttribute.For, ClientID);
                    writer.RenderBeginTag(HtmlTextWriterTag.Label);
                    writer.Write(Label);
                    writer.RenderEndTag();// label control-label
                }

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "controls");
            }

            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            // Write the textfield
            base.Render(writer);

            if (!IsValid && Validator != null)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "help-inline");
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(ErrorMessage);
                writer.RenderEndTag();// span help-inline
            }

            writer.RenderEndTag();// div controls

            if (RenderControlGroupMarkup)
            {
                writer.RenderEndTag(); // div control-group
            }
        }
    }
}