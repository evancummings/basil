using Basil.Interfaces;

namespace Basil.Helpers
{
    public static class BasilHelper
    {
        public static string GetCssClass(IBasilWebControl control)
        {
            var feedback = control.HasFeedback ? " has-feedback" : string.Empty;
            var cssClass = BootstrapHelper.GetFormGroupClass();

            if (!control.IsValid) cssClass += string.Format(" {0}{1}", BootstrapHelper.GetFormGroupErrorClass(), feedback);
            if (control.IsWarning) cssClass += string.Format(" {0}{1}", BootstrapHelper.GetFormGroupWarningClass(), feedback);
            if (control.IsInfo) cssClass += string.Format(" {0}{1}", BootstrapHelper.GetFormGroupInfoClass(), feedback);
            if (control.IsSuccess) cssClass += string.Format(" {0}{1}", BootstrapHelper.GetFormGroupSuccessClass(), feedback);

            return cssClass;
        }
    }
}