using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Presale.Process.TravelExpenseReport.Entity;
using System.Data;

namespace Presale.Process.TravelExpenseReport
{
	public partial class TravelExpense : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string FormId = Request.QueryString["FormID"];
				string strsql1 = "select PurposeOfTrip,EmployeeName,Employee,CostCenterDisplay,";
				strsql1 += "RMB,PaidByCompanyRMB,PaidByEmployeeRMB,Rate from dbo.PROC_TravelExpense where FormID = '" + FormId + "'";
				TravelExpenseEntity List = DataAccess.Instance("BizDB").ExecuteEntity<TravelExpenseEntity>(strsql1);
				string strsql2 = "slelect ROWID,ItemValue,Meal,CurrencyMeal,BME,CurrencyBME,";
				strsql2 += "Air,CurrencyAir,Lau,CurrencyLau,TT,CurrencyTT,Others,CurrencyOther";
				strsql2 += " from dbo.PROC_TravelExpenseDetails_DT where FORMID = '" + FormId + "'";
				DataTable dtInfo = DataAccess.Instance("BizDB").ExecuteDataTable(strsql2);
				if (dtInfo.Rows.Count > 0)
				{
					travelDitail.DataSource = dtInfo;
					travelDitail.DataBind();
				}
			}
		}
	}
}