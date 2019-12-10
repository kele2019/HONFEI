using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Workflow.Interface;
using MyLib;
using Ultimus.UWF.Common.Logic;
using Ultimus.UWF.Workflow.Logic;
using Ultimus.UWF.Workflow.Entity;

namespace Ultimus.UWF.V7
{
    public partial class OpenForm : System.Web.UI.Page
    {
        ITask _task = ServiceContainer.Instance().GetService<ITask>();
        protected void Page_Load(object sender, EventArgs e)
        {
            string taskID = Request.QueryString["TaskID"];
            string taskUser = Request.QueryString["TaskUser"];
            string type = Request.QueryString["Type"];
            string processName = Request.QueryString["ProcessName"];
            string incident = Request.QueryString["Incident"];
            string stepName = Request.QueryString["StepName"];
            string formId = Request.QueryString["FormID"];
            string serverName = Request.QueryString["ServerName"];
            string userName = taskUser;
            

            string url = _task.GetTaskUrl(taskID, type, userName.Replace("\\", "/"));
            Response.Redirect( url);

        }
    }
}