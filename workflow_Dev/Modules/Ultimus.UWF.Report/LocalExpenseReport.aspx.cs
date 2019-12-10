using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Ultimus.UWF.Report.Entity;
using System.Data;

namespace Ultimus.UWF.Report
{
	public partial class LocalExpenseReport : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string FormId = Request.QueryString["FormID"];
			formNumber.Text = FormId;
			string incidents = (DataAccess.Instance("BizDB").ExecuteScalar("select INCIDENT from dbo.PROC_LocalExpense where FormID = " + FormId)).ToString();
			string strsql1 = "select VendorName,VendorCode,RMB,CostCenterSubject,Project from dbo.PROC_LocalExpense where FormID = " + FormId;
			LocalExpenseEntity LocalExpense = DataAccess.Instance("BizDB").ExecuteEntity<LocalExpenseEntity>(strsql1);
			EmployeeName.Text = LocalExpense.VendorName;
			Employee.Text = LocalExpense.VendorCode;
			CostCenter.Text = LocalExpense.CostCenterSubject;
			Project.Text = LocalExpense.Project;
			Total.Text = LocalExpense.RMB.ToString();
			string strsql2 = "select ROWID,Date,SubjectName,Comments,RMB from dbo.PROC_LocalExpense_DT where FORMID = " + FormId;
			DataTable LocalExp = DataAccess.Instance("BizDB").ExecuteDataTable(strsql2);
			if (LocalExp.Rows.Count > 0)
			{
				LocalExpDitail.DataSource = LocalExp;
				LocalExpDitail.DataBind();
			}
			string strsql3 = "select b.EXT04,a.COMMENTS,a.CREATEDATE,b.EXT04 as Signature from dbo.WF_APPROVALHISTORY a left join ORG_USER b on a.EXT01 = b.LOGINNAME";
			strsql3 += " where a.PROCESSNAME = 'Local Expense' and a.INCIDENT = " + int.Parse(incidents) + " order by a.CREATEDATE";
			DataTable log = DataAccess.Instance("BizDB").ExecuteDataTable(strsql3);
			if (log.Rows.Count > 0)
			{
				logDetail.DataSource = log;
				logDetail.DataBind();
			}
		}
	}
}