using Basil;
using System;
using System.Web.UI;

namespace ValidatorSample
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var validator = new BasilValidator(true);

            if (!validator.Validate(pnlForm))
            {
                return;
            }

            // Else continue processing
        }
    }
}