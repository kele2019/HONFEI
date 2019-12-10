using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ultimus.UWF.Home
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Exception"] != null)
            {
                Exception ex = (Exception)Session["Exception"];
                ltError.Text = ex.Message;
                ltStack.Text = ex.StackTrace;
            }
        }
    }
}