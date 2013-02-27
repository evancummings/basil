using System;

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

            var validator = new BasilValidator();
            validator.Validate(pnlForm);

            if (validator.IsValid)
            {
                // Validation has passed on all controls, do stuff

                pnlSuccess.Visible = true;
            }
        }
    }
}