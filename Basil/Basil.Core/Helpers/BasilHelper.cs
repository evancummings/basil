using Basil.Enums;
using Basil.Interfaces;
using System.Linq;

namespace Basil.Helpers
{
    public static class BasilHelper
    {
        public static string GetCssClass(IBasilWebControl control, BootstrapVersions bsVersion)
        {
            var feedback = control.HasFeedback ? " has-feedback" : string.Empty;
            var cssClass = BootstrapHelper.GetFormGroupClass(bsVersion);

            if (!control.IsValid) cssClass += string.Format(" {0}{1}", BootstrapHelper.GetFormGroupErrorClass(bsVersion), feedback);
            if (control.IsWarning) cssClass += string.Format(" {0}{1}", BootstrapHelper.GetFormGroupWarningClass(bsVersion), feedback);
            if (control.IsInfo) cssClass += string.Format(" {0}{1}", BootstrapHelper.GetFormGroupInfoClass(bsVersion), feedback);
            if (control.IsSuccess) cssClass += string.Format(" {0}{1}", BootstrapHelper.GetFormGroupSuccessClass(bsVersion), feedback);

            return cssClass;
        }
    }
}