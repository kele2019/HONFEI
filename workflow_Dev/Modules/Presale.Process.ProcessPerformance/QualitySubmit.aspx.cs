using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using System.Data;
using MyLib;
using System.ComponentModel;

namespace Presale.Process.ProcessPerformance
{
	public partial class QualitySubmit : System.Web.UI.Page
	{
		public string formId = "";
		public string dept = "";
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string stepName = Request.QueryString["StepName"].ToString();
				string Incident = Request.QueryString["Incident"].ToString();
				formId += DataAccess.Instance("BizDB").ExecuteScalar("select FORMID from PROC_ProcessPerformance where INCIDENT = '" + Incident + "'").ToString();
				switch (stepName)
				{
					case "Quality Submit2":
						dept += "'HFPD-02'";
						break;
					case "Quality Submit1":
						dept += "'HFPD-01'";
						break;
					case "Quality Submit5":
						dept += "'HFPD-06'";
						break;
					case "Quality Submit7":
						dept += "'HFPD-08'";
						break;
					case "Quality Submit6":
						dept += "'HFPD-07'";
						break;
					case "Quality Submit4":
						dept += "'HFPD-04'";
						break;
					case "Quality Submit3":
						dept += "'HFPD-03'";
						break;
					case "Quality Submit8":
						dept += "'HFPD-09','HFPD-10'";
						break;
                    case "Quality Submit9":
                        dept += "'FIN'";
                        break;
				}
				string sql = "select * from PROC_ProcessPerformance_Forth where DEPTMENTCODE in (" + dept + ") and FORMID = '" + formId + "' ORDER BY ROWID";
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
				for (int i = 0; i < RepeaterDetail.Items.Count; i++)
				{
					Label ROWID = (Label)RepeaterDetail.Items[i].FindControl("ROWID");
					string rowID = ROWID.Text;
					Label DEPTMENTCODE = (Label)RepeaterDetail.Items[i].FindControl("DEPTMENTCODE");
					string dept = DEPTMENTCODE.Text;
					string strinsert1 = "update PROC_ProcessPerformance_Forth set status='2' where formid='" + FormID + "' and DEPTMENTCODE = '" + dept + "' and  ROWID=" + int.Parse(rowID);
					DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert1);

				}
			}
		}
	}
}