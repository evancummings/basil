using Basil.Helpers;
using Basil.Interfaces;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Basil.WebControls.V3
{
    public class BasilCheckBox : CheckBox, IBasilWebControl
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
                    _errorMessage = Validator.Settings.RequiredFieldValidation.Message;
                }

                return _errorMessage;
            }
            set { _errorMessage = value; }
        }

        public bool Required { get; set; }

        public bool IsValid { get; set; }

        public bool IsWarning { get; set; }

        public bool IsInfo { get; set; }

        public bool IsSuccess { get; set; }

        public bool HasFeedback { get; set; }

        public BasilValidator Validator { get; set; }

        #endregion Properties

        public BasilCheckBox()
        {
            IsValid = true;
            Required = false;
        }

        public void Validate(BasilValidator validator = null)
        {
            Validator = validator;
            IsValid = true;

            if (!Required) return;

            IsValid = Checked;

            if (!IsValid && Validator != null && !string.IsNullOrEmpty(Label))
            {
                Validator.Errors.Add(string.Format("<strong>{0}:</strong> {1}", Label, ErrorMessage));
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            var cssClass = BootstrapHelper.FormGroupClass;
            var feedback = HasFeedback ? " has-feedback" : string.Empty;

            if (!IsValid) cssClass += string.Format(" {0}{1}", Validator.Settings.Checkbox.ErrorCssClass, feedback);
            if (IsWarning) cssClass += string.Format(" {0}{1}", Validator.Settings.Checkbox.WarningCssClass, feedback);
            if (IsInfo) cssClass += string.Format(" {0}{1}", Validator.Settings.Checkbox.InfoCssClass, feedback);
            if (IsSuccess) cssClass += string.Format(" {0}{1}", Validator.Settings.Checkbox.SuccessCssClass, feedback);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            // Write the checkbox
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "checkbox");
            writer.RenderBeginTag(HtmlTextWriterTag.Label);

            base.Render(writer);

            if (!string.IsNullOrEmpty(Label))
            {
                writer.Write(Label);
            }

            writer.RenderEndTag();// label

            if (!IsValid && Validator != null)
            {
                if (HasFeedback)
                {
                    writer.Write("<span class=\"glyphicon glyphicon-warning-sign form-control-feedback\"></span>");
                }

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "help-block");
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(ErrorMessage);
                writer.RenderEndTag();// span help-block
            }

            writer.RenderEndTag(); // div form-group
        }
    }
}