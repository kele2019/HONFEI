using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
//using MobileClient.MobileClientBackgroundRef;

namespace MobileClient
{
    public partial class OpenForm : BasePageClass.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string processName = Request.QueryString["ProcessName"];
            string incident = Request.QueryString["Incident"];
            string taskId = Request.QueryString["TaskID"];
            string stepName = Request.QueryString["StepName"];
            string YB = Request.QueryString["YB"];
            //WorkflowRef services = new WorkflowRef();
            int stepId = 0;// services.GetStepId(processName, stepName);
            int processId=ConvertUtil.ToInt32( DataAccess.Instance("BizDB").ExecuteScalar("select ID from  MOBILECLIENT_PROCESS where  PROCESSNAME='"+processName+"'"));
            stepId=ConvertUtil.ToInt32( DataAccess.Instance("BizDB").ExecuteScalar("select ID from  MOBILECLIENT_STEP where  FK_ID="+processId+" and stepname='"+stepName+"'"));
            Response.Redirect(string.Format("ProcessPage\\{0}.aspx?ProcessName={1}&Incident={2}&TaskID={3}&StepName={4}&YB={5}&StepId={0}", stepId
                ,Server.UrlEncode(processName),incident,taskId,Server.UrlEncode(stepName),YB));

        }
    }
}