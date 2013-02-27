using Basil.Interfaces;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Basil.WebControls
{
    public class BasilCheckBox : CheckBox, IBasilWebControl
    {
        #region Properties

        public string Label { get; set; }

        public bool Required { get; set; }

        public bool IsValid { get; set; }

        public bool IsWarning { get; set; }

        public bool IsInfo { get; set; }

        public bool IsSuccess { get; set; }

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
        }

        protected override void Render(HtmlTextWriter writer)
        {
            var cssClass = "control-group";

            if (!IsValid) cssClass += string.Format(" {0}", Validator.Settings.Checkbox.ErrorCssClass);
            if (IsWarning) cssClass += string.Format(" {0}", Validator.Settings.Checkbox.WarningCssClass);
            if (IsInfo) cssClass += string.Format(" {0}", Validator.Settings.Checkbox.InfoCssClass);
            if (IsSuccess) cssClass += string.Format(" {0}", Validator.Settings.Checkbox.SuccessCssClass);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "controls");
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
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "help-inline");
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(Validator.Settings.RequiredFieldValidation.Message);
                writer.RenderEndTag();// span help-inline
            }

            writer.RenderEndTag();// div controls

            writer.RenderEndTag();// div control-group
        }
    }
}