using Basil.Helpers;
using Basil.Interfaces;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Basil.WebControls.V3
{
    public class BasilCheckBoxList : CheckBoxList, IBasilWebControl, IRepeatInfoUser
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

        public BasilCheckBoxList()
        {
            IsValid = true;
            Required = false;
        }

        public void Validate(BasilValidator validator = null)
        {
            Validator = validator;
            IsValid = true;

            if (!Required) return;

            IsValid = Items.Cast<ListItem>().Any(x => x.Selected);

            if (!IsValid && Validator != null && !string.IsNullOrEmpty(Label))
            {
                Validator.Errors.Add(string.Format("<strong>{0}:</strong> {1}", Label, ErrorMessage));
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(CssClass) || CssClass != "list-unstyled") CssClass += " list-unstyled";

            var cssClass = BootstrapHelper.FormGroupClass;
            var feedback = HasFeedback ? " has-feedback" : string.Empty;

            if (!IsValid) cssClass += string.Format(" {0}{1}", Validator.Settings.Checkbox.ErrorCssClass, feedback);
            if (IsWarning) cssClass += string.Format(" {0}{1}", Validator.Settings.Checkbox.WarningCssClass, feedback);
            if (IsInfo) cssClass += string.Format(" {0}{1}", Validator.Settings.Checkbox.InfoCssClass, feedback);
            if (IsSuccess) cssClass += string.Format(" {0}{1}", Validator.Settings.Checkbox.SuccessCssClass, feedback);

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

            // Write the textfield
            base.Render(writer);

            if (!IsValid && Validator != null)
            {
                if (HasFeedback)
                {
                    writer.Write("<span class=\"glyphicon glyphicon-warning-sign form-control-feedback\"></span>");
                }

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "help-block");
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(ErrorMessage);
                writer.RenderEndTag();// span help-inline
            }

            writer.RenderEndTag(); // div from-group
        }

        void IRepeatInfoUser.RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "checkbox");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.RenderBeginTag(HtmlTextWriterTag.Label);

            writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
            writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID + IdSeparator + repeatIndex.ToString(NumberFormatInfo.InvariantInfo));
            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + ClientIDSeparator + repeatIndex.ToString(NumberFormatInfo.InvariantInfo));
            writer.AddAttribute(HtmlTextWriterAttribute.Value, Items[repeatIndex].Value);

            if (Items[repeatIndex].Selected) writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");

            var attrs = Items[repeatIndex].Attributes;
            foreach (string key in attrs.Keys)
            {
                writer.AddAttribute(key, attrs[key]);
            }

            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.Write(Items[repeatIndex].Text);
            writer.RenderEndTag();

            writer.RenderEndTag();// label
            writer.RenderEndTag();// div radio
        }
    }
}