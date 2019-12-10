using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using System.ComponentModel;
using MyLib;
using Presale.Process.Common;

namespace Presale.Process.PurchaseOrder
{
	public partial class Approval : System.Web.UI.Page
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
                if (ActiontType == 0)
                {
                    ApproveClass.UpdateReivewStatus(ProcessName, Incident, "CTO  Reivew");
                    ApproveClass.UpdateReivewStatus(ProcessName, Incident, "DGM Review");
                }

            }
		}
		protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
		{
			try
			{
				UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
				string FormID = userInfo.FormId;
				ApprovalHistory approvalHistory = Page.FindControl("ApprovalHistory1") as ApprovalHistory;
				int ActiontType = approvalHistory.ActionType;
                string StepName = Request.QueryString["StepName"];

                if (ActiontType == 0 && StepName == "Supplier Finish")//成功
				{
					string strinsert = string.Format("update PROC_PurchaseOrder set status='2' where formid='" + FormID + "'");
					DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert);
					string strinsert1 = string.Format("update PROC_Purchase set PurchaseOrdStatus = 2 where DOCUMENTNO='" + read_PurchaseOrderNo.Text + "'");
					DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert1);
				}
				else {
					string strinsert = string.Format("update PROC_Purchase set PurchaseOrdStatus = 0 where DOCUMENTNO='" + read_PurchaseOrderNo.Text + "'");
					DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert);
				}
			}
			catch
			{

			}
		}

		protected void fld_detail_PROC_PurchaseOrder_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{

				string taxcode = ((Label)e.Item.FindControl("read_TAXCODE")).Text.ToString().Trim();
				if (taxcode == "J0")
				{
					((Label)e.Item.FindControl("latexcde")).Text = "进项税0%";
				}

				if (taxcode == "J11")
				{
					((Label)e.Item.FindControl("latexcde")).Text = "进项税11%";
				}

				if (taxcode == "J2")
				{
					((Label)e.Item.FindControl("latexcde")).Text = "进项税17%";
				}

				if (taxcode == "J4")
				{
					((Label)e.Item.FindControl("latexcde")).Text = "进项税4%";
				}

				if (taxcode == "J1")
				{
					((Label)e.Item.FindControl("latexcde")).Text = "进项税6%";
				}
			}
		}
	}
}