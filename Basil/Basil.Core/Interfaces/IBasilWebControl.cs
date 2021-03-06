﻿using Basil.Enums;
using System.Web.UI;

namespace Basil.Interfaces
{
    public interface IBasilWebControl
    {
        string Label { get; set; }

        string HelpText { get; set; }

        bool Required { get; set; }

        bool IsValid { get; set; }

        bool IsWarning { get; set; }

        bool IsInfo { get; set; }

        bool IsSuccess { get; set; }

        bool HasFeedback { get; set; }

        bool RenderControlGroupMarkup { get; set; }

        BasilValidator Validator { get; set; }

        BootstrapVersions? BootstrapVersion { get; set; }

        void RenderBoostrapV2(HtmlTextWriter writer);

        void RenderBoostrapV3(HtmlTextWriter writer);
    }
}