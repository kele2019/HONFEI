using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Workflow.Logic;

namespace Ultimus.UWF.Workflow.View.Maintenanc
{
    public partial class OpenForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) {
            string incident = Request["Incident"];
            string taskId = Request["TaskId"];
            string processName = Server.UrlDecode(Request["ProcessName"].Trim());

            //Server.Transfer(new TaskList().GetProcessStepUrl(taskId, "", Common.Logic.SessionLogic.GetLoginName()));
        }
    }
}