using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presale.Process.Common;
using Ultimus.UWF.Form.ProcessControl;
using MyLib;
using System.ComponentModel;

namespace Presale.Process.PurchaseApplication
{
	public partial class Approval : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            ((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
            ((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
		}
        protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
        {
            string ProcessName = Request.QueryString["ProcessName"].ToString();
            string StepName = Request.QueryString["StepName"].ToString();
            string Incident = Request.QueryString["Incident"].ToString();
            ApprovalHistory approvalHistory = Page.FindControl("ApprovalHistory1") as ApprovalHistory;
            int ActiontType = approvalHistory.ActionType;
            BusniessClass ApproveClass = new BusniessClass();
            if (StepName == "GM Approve")//成功
            {
                if (ApproveClass.CheckCFOApprove(ProcessName, Incident, "CFO Approve"))
                {
                    MessageBox.Show(this.Page, "CFO not approve");
                    g.Cancel = true;
                    return;
                }
                if (ActiontType == 0)
                {
                    ApproveClass.UpdateReivewStatus(ProcessName, Incident, "CTO Review");
                    ApproveClass.UpdateReivewStatus(ProcessName, Incident, "DGM Review");
                }
            }
        }
        protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
        {
            if (read_PRType.Text != "1")
            {
                string StepName = Request.QueryString["StepName"].ToString();
                UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
                string FormID = userInfo.FormId;
                ApprovalHistory approvalHistory = Page.FindControl("ApprovalHistory1") as ApprovalHistory;
                int ActiontType = approvalHistory.ActionType;
                if (ActiontType == 0 && StepName == "GM Approve")//成功
                {
                    string strinsert = string.Format("update PROC_Purchase set status='2' where formid='" + FormID + "'");
                    DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert);
                }
            }
        }
	}
}