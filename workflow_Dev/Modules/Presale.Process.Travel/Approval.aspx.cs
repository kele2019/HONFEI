using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.ComponentModel;
using Ultimus.UWF.Form.ProcessControl;
using Presale.Process.Travel.Entity;
using Presale.Process.Common;

namespace Presale.Process.Travel
{
	public partial class Approval : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				//string sql = "select EXT03 from dbo.org_user where loginname = '" + Page.User.Identity.Name + "'";
				//ApprovalUserPost user = DataAccess.Instance("BizDB").ExecuteEntity<ApprovalUserPost>(sql);
				//fld_ApprovalUserPost.Text = user.EXT03;
			}
			((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
		}
		protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
		{
            //string sql = "select EXT03 from dbo.org_user where loginname = '" + Page.User.Identity.Name + "'";
            //ApprovalUserPost user = DataAccess.Instance("BizDB").ExecuteEntity<ApprovalUserPost>(sql);
            //fld_ApprovalUserPost.Text = user.EXT03;

            string StepName = Request.QueryString["StepName"];
            string ProcessName = Request.QueryString["ProcessName"].ToString();
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
            if (StepName == "GM Approve")
            {
                UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
                string FormID = userInfo.FormId;
                if (ActiontType == 0)//成功
                {

                    ApproveClass.UpdateReivewStatus(ProcessName, Incident, "CTO Review");
                    ApproveClass.UpdateReivewStatus(ProcessName, Incident, "DGM Review");
                    //string strinsert1 = string.Format("update PROC_Travel set status='2' where formid='" + FormID + "'");
                    //DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert1);
                }
            }

		}
	}
}