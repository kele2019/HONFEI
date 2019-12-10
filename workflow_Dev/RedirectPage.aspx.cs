using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;

namespace UWF
{
    public partial class RedirectPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect(ConfigurationManager.AppSettings["FromPortalGotoDefaultPageUrl"]);
        }
    }
}