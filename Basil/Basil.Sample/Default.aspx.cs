using System;
using System.Collections.Generic;
using System.Linq;

namespace Basil.Sample
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            pnlSuccess.Visible = false;
            pnlErrors.Visible = false;
            litErrors.Text = "";

            List<string> errors;
            List<string> errors2;

            var validator = new BasilValidator();
            validator.Validate(pnlForm);
            validator.Validate(pnlFormTwo);
            validator.Validate(pnlForm, out errors);
            validator.Validate(pnlFormTwo, out errors2);

            if (errors.Any())
            {
                pnlErrors.Visible = true;

                litErrors.Text += "<h4>Group 1</h4>";
                litErrors.Text += string.Join("<br />", errors);
            }

            if (errors2.Any())
            {
                pnlErrors.Visible = true;

                litErrors.Text += "<br />";
                litErrors.Text += "<h4>Group 2</h4>";
                litErrors.Text += string.Join("<br />", errors2);
            }

            if (validator.IsValid)
            {
                // Validation has passed on all controls, do stuff

                pnlSuccess.Visible = true;
            }
        }
    }
}