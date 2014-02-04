using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Basil.Helpers
{
    public static class ControlHelper
    {
        /// <summary>
        /// Can be used to apply error class to a controls parent if markup is all server side panels
        /// </summary>
        public static void ApplyErrorClassToControlGroup(Control control)
        {
            var pnlControls = FindImmediateParentOfType<Panel>(control);
            if (pnlControls == null) return;

            var pnlControlGroup = FindImmediateParentOfType<Panel>(pnlControls);
            if (pnlControlGroup != null)
            {
                pnlControlGroup.CssClass += " " + BootstrapHelper.FormGroupErrorClass;
            }
        }

        public static void RemoveErrorClassToControlGroup(Control control)
        {
            var pnlControls = FindImmediateParentOfType<Panel>(control);
            if (pnlControls == null) return;

            var pnlControlGroup = FindImmediateParentOfType<Panel>(pnlControls);
            if (pnlControlGroup != null)
            {
                pnlControlGroup.CssClass = BootstrapHelper.FormGroupClass;
            }
        }

        public static T FindImmediateParentOfType<T>(Control control) where T : Control
        {
            T retVal = default(T);
            var parentCtl = control.Parent;

            while (parentCtl != null)
            {
                if (parentCtl is T)
                {
                    retVal = (T)parentCtl;
                    break;
                }

                parentCtl = parentCtl.Parent;
            }

            return retVal;
        }

        public static Control FindControlById(Control containerControl, string controlId)
        {
            var queue = new Queue<Control>();
            queue.Enqueue(containerControl);

            while (queue.Count > 0)
            {
                var currentControl = queue.Dequeue();

                if (currentControl.ID == controlId)
                {
                    return currentControl;
                }

                foreach (Control childControl in currentControl.Controls)
                {
                    queue.Enqueue(childControl);
                }
            }

            return null;
        }

        public static IEnumerable<T> GetControlsOfType<T>(Control root) where T : Control
        {
            var t = root as T;
            if (t != null) yield return t;

            var container = root;
            if (container != null)
                foreach (var i in from Control c in container.Controls from i in GetControlsOfType<T>(c) select i)
                    yield return i;
        }
    }
}