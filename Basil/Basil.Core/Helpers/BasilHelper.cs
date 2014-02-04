﻿using Basil.Interfaces;
using System.Linq;

namespace Basil.Helpers
{
    public static class BasilHelper
    {
        public static string GetCssClass(IBasilWebControl control)
        {
            var feedback = control.HasFeedback ? " has-feedback" : string.Empty;
            var cssClass = BootstrapHelper.FormGroupClass;

            if (!control.IsValid) cssClass += string.Format(" {0}{1}", BootstrapHelper.FormGroupErrorClass, feedback);
            if (control.IsWarning) cssClass += string.Format(" {0}{1}", BootstrapHelper.FormGroupWarningClass, feedback);
            if (control.IsInfo) cssClass += string.Format(" {0}{1}", BootstrapHelper.FormGroupInfoClass, feedback);
            if (control.IsSuccess) cssClass += string.Format(" {0}{1}", BootstrapHelper.FormGroupSuccessClass, feedback);

            return cssClass;
        }
    }
}