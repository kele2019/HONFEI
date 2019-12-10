using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Presale.Process.ProcessPerformance.Entity;
using Ultimus.UWF.Form.ProcessControl;
using System.Data;
using System.ComponentModel;

namespace Presale.Process.ProcessPerformance
{
	public partial class Approval : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string stepName = Request.QueryString["StepName"].ToString();
				string Incident = Request.QueryString["Incident"].ToString();
				string formId = DataAccess.Instance("BizDB").ExecuteScalar("select FORMID from PROC_ProcessPerformance where INCIDENT = '" + Incident + "'").ToString();
				string sql = "select * from PROC_ProcessPerformance_Forth where FORMID = '" + formId + "' ORDER BY DEPTMENTCODE,ROWID";
				DataTable dtInfo = DataAccess.Instance("BizDB").ExecuteDataTable(sql);
				if (dtInfo.Rows.Count > 0)
				{
					RepeaterDetail.DataSource = dtInfo;
					RepeaterDetail.DataBind();
				}
			}
			((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
			((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
		}
		protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
		{

		}
		protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			string FormID = userInfo.FormId;
			ApprovalHistory approvalHistory = Page.FindControl("ApprovalHistory1") as ApprovalHistory;
			int ActiontType = approvalHistory.ActionType;
			if (ActiontType == 0)//成功
			{
				string strinsert1 = "update PROC_ProcessPerformance set status='2' where formid='" + FormID + "'";
				DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert1);
			}
		}
	}
}