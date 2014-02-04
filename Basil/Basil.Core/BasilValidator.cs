using Basil.Helpers;
using Basil.Settings;
using Basil.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace Basil
{
    public class BasilValidator
    {
        #region Properties

        public BasilSettings Settings { get; set; }

        public bool IsValid { get; set; }

        public bool AddRelEqualsTooltip { get; set; }

        public List<string> Errors { get; set; }

        #endregion Properties

        public BasilValidator(bool addRelEqualsTooltip = false)
        {
            Settings = new BasilSettings();

            AddRelEqualsTooltip = addRelEqualsTooltip;
        }

        public BasilValidator(BasilSettings settings, bool addRelEqualsTooltip = false)
        {
            Settings = settings;

            AddRelEqualsTooltip = addRelEqualsTooltip;
        }

        /// <summary>
        /// Validate controls in container (Bootstrap 2)
        /// </summary>
        public bool Validate(Control control, out List<string> errors)
        {
            var validator = Validate(control);

            errors = Errors;

            IsValid = !errors.Any();

            return validator;
        }

        /// <summary>
        /// Validate controls in container (Bootstrap 2)
        /// </summary>
        public bool Validate(Control control)
        {
            // Default to valid
            IsValid = true;
            Errors = new List<string>();

            // Validate all BasilTextBox controls
            var btbControls = ControlHelper.GetControlsOfType<BasilTextBox>(control).Where(x => x.Required).ToList();
            var bcbControls = ControlHelper.GetControlsOfType<BasilCheckBox>(control).Where(x => x.Required).ToList();
            var bcblControls = ControlHelper.GetControlsOfType<BasilCheckBoxList>(control).Where(x => x.Required).ToList();
            var brbControls = ControlHelper.GetControlsOfType<BasilRadioButtonList>(control).Where(x => x.Required).ToList();
            var bddlControls = ControlHelper.GetControlsOfType<BasilDropDownList>(control).Where(x => x.Required).ToList();

            foreach (var ctrl in btbControls)
            {
                ctrl.Validate(this);
            }

            foreach (var ctrl in bcbControls)
            {
                ctrl.Validate(this);
            }

            foreach (var ctrl in bcblControls)
            {
                ctrl.Validate(this);
            }

            foreach (var ctrl in brbControls)
            {
                ctrl.Validate(this);
            }

            foreach (var ctrl in bddlControls)
            {
                ctrl.Validate(this);
            }

            if (!btbControls.All(x => x.IsValid)) IsValid = false;
            if (!bcbControls.All(x => x.IsValid)) IsValid = false;
            if (!bcblControls.All(x => x.IsValid)) IsValid = false;
            if (!brbControls.All(x => x.IsValid)) IsValid = false;
            if (!bddlControls.All(x => x.IsValid)) IsValid = false;

            return IsValid;
        }
    }
}