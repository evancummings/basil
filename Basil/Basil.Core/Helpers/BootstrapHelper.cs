using Basil.Enums;
using Basil.Settings;
using System.Linq;

namespace Basil.Helpers
{
    public static class BootstrapHelper
    {
        public static string FormGroupClass
        {
            get { return BasilSettings.BootstrapVersion == BootstrapVersions.V2 ? "control-group" : "form-group"; }
        }

        public static string FormGroupErrorClass
        {
            get { return BasilSettings.BootstrapVersion == BootstrapVersions.V2 ? "error" : "has-error"; }
        }

        public static string FormGroupWarningClass
        {
            get { return BasilSettings.BootstrapVersion == BootstrapVersions.V2 ? "warning" : "has-warning"; }
        }

        public static string FormGroupInfoClass
        {
            get { return BasilSettings.BootstrapVersion == BootstrapVersions.V2 ? "info" : "has-info"; }
        }

        public static string FormGroupSuccessClass
        {
            get { return BasilSettings.BootstrapVersion == BootstrapVersions.V2 ? "success" : "has-success"; }
        }
    }
}