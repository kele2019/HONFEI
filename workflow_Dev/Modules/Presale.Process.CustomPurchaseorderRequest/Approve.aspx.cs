using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Ultimus.UWF.Form.ProcessControl;
using System.Data.SqlClient;
using MyLib;
using System.Collections;

namespace Presale.Process.CustomPurchaseorderRequest
{
    public partial class Approve : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string StepName = Request.QueryString["StepName"];
            hdStepname.Value = StepName;
            ((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
        }
        protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
        {
            ApprovalHistory approvalHistory = Page.FindControl("ApprovalHistory1") as ApprovalHistory;
            int ActiontType = approvalHistory.ActionType;
            string StepName = Request.QueryString["StepName"].ToString();
            string Incident = Request.QueryString["Incident"];
            Hashtable table = (Hashtable)sender;
            if (ActiontType == 0)//流程结束
            {
                if (CheckAllApprove("Customer Purchase Order Review", Incident, StepName))
                {
                    table.Add("IsCompleted", "isCompleted");
                }
            }
        }
        /// <summary>
        /// 检查指定审批人是否审批
        /// </summary>
        /// <param name="ProcessName"></param>
        /// <param name="Incident"></param>
        /// <param name="StepName"></param>
        /// <returns></returns>
        public bool CheckAllApprove(string ProcessName, string Incident, string StepName)
        {
            string Strsql = "select COUNT(1) from TASKS where PROCESSNAME=@PROCESSNAME and INCIDENT=@INCIDENT and STEPLABEL IN('Buyer and Planner','Program Engineering','Finance Manager','Quality Manager') AND STATUS=3";
            List<SqlParameter> listP = new List<SqlParameter>();
            listP.Add(new SqlParameter("@PROCESSNAME", ProcessName));
            listP.Add(new SqlParameter("@INCIDENT", Incident));
            listP.Add(new SqlParameter("@STEPLABEL", StepName));
            return Convert.ToInt32(DataAccess.Instance("UltDB").ExecuteScalar(Strsql, listP.ToArray())) >=3;
        }
    }
}