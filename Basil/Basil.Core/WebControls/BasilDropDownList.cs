using Basil.Interfaces;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Basil.WebControls
{
    public class BasilDropDownList : DropDownList, IBasilWebControl
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

        public BasilDropDownList()
        {
            IsValid = true;
            Required = false;
        }

        public void Validate(BasilValidator validator = null)
        {
            Validator = validator;
            IsValid = true;

            if (!Required) return;

            IsValid = (SelectedIndex > 0 && !string.IsNullOrEmpty(SelectedValue));
        }

        protected override void Render(HtmlTextWriter writer)
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
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "control-label");
                writer.AddAttribute(HtmlTextWriterAttribute.For, ClientID);
                writer.RenderBeginTag(HtmlTextWriterTag.Label);
                writer.Write(Label);
                writer.RenderEndTag();// label control-label
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "controls");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            // Write the textfield
            base.Render(writer);

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