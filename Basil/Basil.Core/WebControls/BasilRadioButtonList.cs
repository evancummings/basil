using Basil.Interfaces;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Basil.WebControls
{
    public class BasilRadioButtonList : RadioButtonList, IBasilWebControl, IRepeatInfoUser
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

        public BasilRadioButtonList()
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

            IsValid = (SelectedItem != null && !string.IsNullOrEmpty(SelectedValue));

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

                if (!IsValid) cssClass += string.Format(" {0}", Validator.Settings.RadioButtonList.ErrorCssClass);
                if (IsWarning) cssClass += string.Format(" {0}", Validator.Settings.RadioButtonList.WarningCssClass);
                if (IsInfo) cssClass += string.Format(" {0}", Validator.Settings.RadioButtonList.InfoCssClass);
                if (IsSuccess) cssClass += string.Format(" {0}", Validator.Settings.RadioButtonList.SuccessCssClass);

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

        void IRepeatInfoUser.RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "radio");
            writer.RenderBeginTag(HtmlTextWriterTag.Label);

            writer.AddAttribute(HtmlTextWriterAttribute.Type, "radio");
            writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID + IdSeparator);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + ClientIDSeparator);
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

            writer.RenderEndTag();// label radio
        }
    }
}