using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Ultimus.UWF.Form.ProcessControl;
using System.ComponentModel;
using Presale.Process.Common;

namespace Presale.Process.FixedAssetApplication
{
	public partial class Approval2 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{

			}
			((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
			((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
		}
		protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
		{

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
                }
            }
		}
		protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
		{

		}
	}
}