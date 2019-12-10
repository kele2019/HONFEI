using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Ultimus.UWF.Form.ProcessControl;
using MyLib;

namespace Presale.Process.PurchaseOrder
{
	public partial class ManagerApproval : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{

			}
			((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
			((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
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
		protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
		{

		}
		protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
		{
			try
			{
				ApprovalHistory approvalHistory = Page.FindControl("ApprovalHistory1") as ApprovalHistory;
				int ActiontType = approvalHistory.ActionType;
				if (ActiontType != 0)//失败
				{
					string strinsert = string.Format("update PROC_Purchase set PurchaseOrdStatus = 0 where DOCUMENTNO='" + read_PurchaseOrderNo.Text + "'");
					DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert);
				}
			}
			catch
			{

			}
		}
	}
}