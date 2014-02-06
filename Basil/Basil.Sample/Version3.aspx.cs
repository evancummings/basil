using Basil.Enums;
using Basil.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basil.Sample
{
    public partial class Version3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BasilSettings.BootstrapVersion = BootstrapVersions.V3;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            pnlSuccess.Visible = false;
            pnlErrors.Visible = false;
            litErrors.Text = "";

            List<string> errors;

            var validator = new BasilValidator();
            validator.Validate(pnlForm, out errors);

            if (errors.Any())
            {
                pnlErrors.Visible = true;

                //litErrors.Text += "<h4>Group 1</h4>";
                litErrors.Text += string.Join("<br />", errors);
            }

            if (validator.IsValid)
            {
                // Validation has passed on all controls, do stuff
                pnlSuccess.Visible = true;
            }
        }
    }
}