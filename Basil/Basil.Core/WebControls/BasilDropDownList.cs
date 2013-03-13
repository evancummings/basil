using Basil.Interfaces;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Basil.WebControls
{
    public class BasilDropDownList : DropDownList, IBasilWebControl
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

        public bool RenderControlGroupMarkup { get; set; }

        public BasilValidator Validator { get; set; }

        #endregion Properties

        public BasilDropDownList()
        {
            IsValid = true;
            Required = false;
            RenderControlGroupMarkup = true;
        }

        public void Validate(BasilValidator validator = null)
        {
            Validator = validator;
            IsValid = true;

            if (!Required) return;

            IsValid = (SelectedIndex > 0 && !string.IsNullOrEmpty(SelectedValue));

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

                if (!IsValid) cssClass += string.Format(" {0}", Validator.Settings.DropDownList.ErrorCssClass);
                if (IsWarning) cssClass += string.Format(" {0}", Validator.Settings.DropDownList.WarningCssClass);
                if (IsInfo) cssClass += string.Format(" {0}", Validator.Settings.DropDownList.InfoCssClass);
                if (IsSuccess) cssClass += string.Format(" {0}", Validator.Settings.DropDownList.SuccessCssClass);

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