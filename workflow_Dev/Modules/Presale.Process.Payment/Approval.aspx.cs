using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presale.Process.Payment.Entity;
using MyLib;
using System.ComponentModel;
using Ultimus.UWF.Form.ProcessControl;
using Presale.Process.Common;
namespace Presale.Process.Payment
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
			string sql = "select EXT03 from dbo.org_user where loginname = '" + Page.User.Identity.Name + "'";
			ApprovalUsrePost user = DataAccess.Instance("BizDB").ExecuteEntity<ApprovalUsrePost>(sql);
			fld_ApplicantUserPost.Text = user.EXT03;

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
            }
		}
        protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
        {
            //string StepName = Request.QueryString["StepName"].ToString();
            //UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            //string FormID = userInfo.FormId;
            //ApprovalHistory approvalHistory = Page.FindControl("ApprovalHistory1") as ApprovalHistory;
            //int ActiontType = approvalHistory.ActionType;
            //if (ActiontType == 0 && StepName == "GM Approve")//成功
            //{
            //    string strinsert1 = string.Format("update PROC_Payment set status='2' where formid='" + FormID + "'");
            //    DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert1);
            //}
        }
	}
}