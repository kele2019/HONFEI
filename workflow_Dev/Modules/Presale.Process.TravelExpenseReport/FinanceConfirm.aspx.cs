using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using MyLib;
using System.ComponentModel;
using Presale.Process.TravelExpenseReport.Entity;

namespace Presale.Process.TravelExpenseReport
{
	public partial class FinanceConfirm : System.Web.UI.Page
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

		}
		protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
		{
			try
			{
				UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
				string FormID = userInfo.FormId;
				ApprovalHistory approvalHistory = Page.FindControl("ApprovalHistory1") as ApprovalHistory;
				int ActiontType = approvalHistory.ActionType;
				if (ActiontType == 0)//成功
				{
					string strinsert1 = string.Format("update PROC_TravelExpense set status='2' where formid='" + FormID + "'");
					DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert1);
					string strinsert = string.Format("update PROC_Travel set TravelExpStatus = 2 where DOCUMENTNO='" + read_TravelRequestNo.Text + "'");
					DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert);
				}
				else {
					string strinsert = string.Format("update PROC_Travel set TravelExpStatus = 0 where DOCUMENTNO='" + read_TravelRequestNo.Text + "'");
					DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert);
				}
			}
			catch
			{

			}
		}
	}
}