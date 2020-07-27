using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presale.Process.CustomPurchaseorderRequest
{
    public partial class Approve : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string StepName = Request.QueryString["StepName"];
            hdStepname.Value = StepName;
        }
    }
}