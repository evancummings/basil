using Basil.Enums;
using Basil.Helpers;
using Basil.Interfaces;
using Basil.Settings;
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

        public bool HasFeedback { get; set; }

        public bool RenderControlGroupMarkup { get; set; }

        public BasilValidator Validator { get; set; }

        public BootstrapVersions BootstrapVersion { get; set; }

        #endregion Properties

        public BasilRadioButtonList()
        {
            IsValid = true;
            Required = false;
            RenderControlGroupMarkup = true;
            BootstrapVersion = BasilSettings.BootstrapVersion;
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
            switch (BootstrapVersion)
            {
                case BootstrapVersions.V2:
                    BasilSettings.BootstrapVersion = BootstrapVersions.V2;
                    RenderBoostrapV2(writer);
                    break;

                case BootstrapVersions.V3:
                    BasilSettings.BootstrapVersion = BootstrapVersions.V3;
                    RenderBoostrapV3(writer);
                    break;
            }
        }

        public void RenderBoostrapV2(HtmlTextWriter writer)
        {
            if (RenderControlGroupMarkup)
            {
                var cssClass = BasilHelper.GetCssClass(this, BootstrapVersion);

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
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
            }

            // Write the textfield
            base.Render(writer);

            if (!IsValid && Validator != null && !string.IsNullOrEmpty(ErrorMessage))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "help-inline");
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(ErrorMessage);
                writer.RenderEndTag();// span help-inline
            }

            if (RenderControlGroupMarkup)
            {
                writer.RenderEndTag();// div controls
                writer.RenderEndTag(); // div control-group
            }
        }

        public void RenderBoostrapV3(HtmlTextWriter writer)
        {
            var cssClass = BasilHelper.GetCssClass(this, BootstrapVersion);

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

            if (!IsValid && Validator != null && !string.IsNullOrEmpty(ErrorMessage))
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

        void IRepeatInfoUser.RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "radio");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

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

            writer.RenderEndTag();// label

            writer.RenderEndTag();// div radio
        }
    }
}