using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;

namespace Ultimus.UWF.Workflow
{
    public partial class History : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //传递 流程名 与 实例号 
                string process = Request["ProcessName"].ToString();
                string incident = Request["Incident"] == null ? "0" : Request["Incident"].ToString();
                if (process == null || process == "") return;
                getDateBind(process, incident);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 读取审批意见
        /// </summary>
        /// <param name="process"></param>
        /// <param name="incident"></param>
        private void getDateBind(string process, string incident)
        {
            try
            {
                if (string.IsNullOrEmpty(incident)) incident = "0";
                process = process.Replace("'", "");
                string cmdText = "select stepname,createdate,approvername,action,comments from wf_approvalhistory where processname='{0}' and incident={1} order by id ";
                cmdText = string.Format(cmdText, process, incident);
                DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(cmdText);
                gvApproval.DataSource = dt;
                gvApproval.DataBind();
            }
            catch (Exception e)
            {
                LogUtil.Error(e);
            }

        }

        protected void gvApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}