﻿using Basil.Enums;
using Basil.Helpers;
using Basil.Interfaces;
using Basil.Settings;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Basil.WebControls
{
    public class BasilCheckBox : CheckBox, IBasilWebControl
    {
        #region Properties

        public string Label { get; set; }

        public string HelpText { get; set; }

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
            set
            {
                _errorMessage = value;
            }
        }

        public bool Required { get; set; }

        public bool IsValid { get; set; }

        public bool IsWarning { get; set; }

        public bool IsInfo { get; set; }

        public bool IsSuccess { get; set; }

        public bool HasFeedback { get; set; }

        public bool RenderControlGroupMarkup { get; set; }

        public BasilValidator Validator { get; set; }

        public BootstrapVersions? BootstrapVersion { get; set; }

        #endregion Properties

        public BasilCheckBox()
        {
            IsValid = true;
            Required = false;
            RenderControlGroupMarkup = true;
            if (BootstrapVersion == null) BootstrapVersion = BasilSettings.BootstrapVersion;
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
            switch (BootstrapVersion)
            {
                case BootstrapVersions.V2:
                    RenderBoostrapV2(writer);
                    break;

                case BootstrapVersions.V3:
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

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "controls");

                writer.RenderBeginTag(HtmlTextWriterTag.Div);
            }

            // Write the checkbox
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "checkbox");
            writer.RenderBeginTag(HtmlTextWriterTag.Label);

            base.Render(writer);

            if (!string.IsNullOrEmpty(Label))
            {
                writer.Write(Label);
            }

            writer.RenderEndTag();// label

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

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "checkbox");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            // Write the checkbox
            writer.RenderBeginTag(HtmlTextWriterTag.Label);

            base.Render(writer);

            if (!string.IsNullOrEmpty(Label))
            {
                writer.Write(Label);
            }

            writer.RenderEndTag();// label

            if (!string.IsNullOrEmpty(HelpText))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "help-block");
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(HelpText);
                writer.RenderEndTag();// p help-block
            }

            writer.RenderEndTag();// div

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
    }
}