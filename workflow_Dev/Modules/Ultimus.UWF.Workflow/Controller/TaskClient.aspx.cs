using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;

namespace Ultimus.UWF.Workflow.Controller
{
    public partial class TaskClient1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sessionid = "";// context.Request["SessionId"];
            sessionid = ConvertUtil.ToString(HttpContext.Current.Session["Ult_SessionId"]);
        }
    }
}