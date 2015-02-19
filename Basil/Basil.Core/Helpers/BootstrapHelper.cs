using Basil.Enums;

namespace Basil.Helpers
{
    public static class BootstrapHelper
    {
        public static string GetFormGroupClass(BootstrapVersions? bsVersion)
        {
            switch (bsVersion)
            {
                case BootstrapVersions.V2:
                    return "control-group";
            }

            return "form-group";
        }

        public static string GetFormGroupErrorClass(BootstrapVersions? bsVersion)
        {
            switch (bsVersion)
            {
                case BootstrapVersions.V2:
                    return "error";
            }

            return "has-error";
        }

        public static string GetFormGroupWarningClass(BootstrapVersions? bsVersion)
        {
            switch (bsVersion)
            {
                case BootstrapVersions.V2:
                    return "warning";
            }

            return "has-warning";
        }

        public static string GetFormGroupInfoClass(BootstrapVersions? bsVersion)
        {
            switch (bsVersion)
            {
                case BootstrapVersions.V2:
                    return "info";
            }

            return "has-info";
        }

        public static string GetFormGroupSuccessClass(BootstrapVersions? bsVersion)
        {
            switch (bsVersion)
            {
                case BootstrapVersions.V2:
                    return "success";
            }

            return "has-success";
        }
    }
}