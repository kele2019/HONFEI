using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Report.Entity;
using MyLib;
using System.Data;
namespace Ultimus.UWF.Report
{
	public partial class TravelExpenseReport2 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string FormId = Request.QueryString["FormID"];
			formNumber.Text = FormId;
			string incidents = (DataAccess.Instance("BizDB").ExecuteScalar("select INCIDENT from dbo.PROC_TravelExpense where FormID = " + FormId)).ToString();
			
			string strsql1 = "select PurposeOfTrip,EmployeeName,Employee,CostCenterDisplay,";
			strsql1 += "RMB,PaidByCompanyRMB,PaidByEmployeeRMB,Rate,Project from dbo.PROC_TravelExpense where FormID = " + FormId;
			TravelExpenseEntity travelExpense = DataAccess.Instance("BizDB").ExecuteEntity<TravelExpenseEntity>(strsql1);
			BussinessPurpose.Text = travelExpense.PurposeOfTrip;
			RMB.Text = travelExpense.RMB;
			EmployeeName.Text = travelExpense.EmployeeName;
			PaidByCompany.Text = travelExpense.PaidByCompanyRMB;
			PaidByEmployee.Text = travelExpense.PaidByEmployeeRMB;
			Employee.Text = travelExpense.Employee;
			CostCenter.Text = travelExpense.CostCenterDisplay;
			Rate.Text = travelExpense.Rate;
			Project.Text = travelExpense.Project;
			//string strsql2 = "select a.ROWID,a.TravelFrom,a.ItemValue,a.Meal,a.CurrencyMeal,a.BME,a.CurrencyBME,";
			//strsql2 += "a.Air,a.CurrencyAir,a.TB,a.CurrencyTB,a.Hotel,a.CurrencyHotel,a.Lau,a.CurrencyLau,a.TT,a.CurrencyTT,a.Others,a.CurrencyOther";
			//strsql2 += " from dbo.PROC_TravelExpenseDetails_DT a where a.FORMID = " + FormId;
			string strsql2 = "select ROWID,ItemValue,TravelFrom,Meal,BME,Hotel,Air,TB,Lau,Others,TT,payMeal,payBME,payAir,payHotel,payTB,payLau,payTT,payOthers,CurrencyMeal,CurrencyBME,CurrencyAir,CurrencyHotel,CurrencyTB,CurrencyLau,CurrencyTT,CurrencyOther from dbo.PROC_TravelExpenseDetails_DT where FORMID = " + FormId;
			List<TravelExpenseDetailEntity> lists = DataAccess.Instance("BizDB").ExecuteList<TravelExpenseDetailEntity>(strsql2);
			//if (dtInfo.Rows.Count > 0)
			//{
			//    //travelDitail.DataSource = dtInfo;
			//    //travelDitail.DataBind();
			//    lbHmtl.Text = "<tr  ><td rowspan=\"2\">1</td><td rowspan=\"2></td><td rowspan=\"2\">3</td><td>4</td><td>5</td><td>6</td><td>7</td></tr>";
			//    lbHmtl.Text = lbHmtl.Text + "<tr><td>8</td><td>8</td><td>8</td></tr>";
			//}
			foreach (TravelExpenseDetailEntity item in lists) { 
				int rows = 0;
				if (item.Meal != null) {
					rows++;
				}
				if (item.BME != null)
				{
					rows++;
				}
				if (item.Air != null)
				{
					rows++;
				}
				if (item.Hotel != null)
				{
					rows++;
				}
				if (item.Lau != null)
				{
					rows++;
				}
				if (item.Others != null)
				{
					rows++;
				}
				if (item.TB != null)
				{
					rows++;
				}
				if (item.TT != null)
				{
					rows++;
				}
				lbHmtl.Text += "<tr>";
				lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\" rowspan=\"" + rows + "\"><p style=\"text-align:center\">" + item.ROWID + "</p></td>";
				lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\" rowspan=\"" + rows + "\"><p style=\"text-align:center\">" + item.TravelFrom + "</p></td>";
				lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\" rowspan=\"" + rows + "\"><p style=\"text-align:center\">" + Convert.ToDateTime(item.ItemValue).ToString("yyyy/MM/dd") + "</p></td>";
				if (item.Meal != null)
				{
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">Meal</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.Meal + "</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.CurrencyMeal + "</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.payMeal + "</p></td></tr>";
                    lbHmtl.Text += "<tr>";
				}
				if (item.BME != null)
				{
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">BM/E</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.BME + "</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.CurrencyBME + "</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.payBME + "</p></td></tr>";
					lbHmtl.Text += "<tr>";
				}
				if (item.Air != null)
				{
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">Airfare</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.Air + "</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.CurrencyAir + "</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.payAir + "</p></td></tr>";
					lbHmtl.Text += "<tr>";
				}
				if (item.Hotel != null)
				{
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">Hotel</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.Hotel + "</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.CurrencyHotel + "</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.payHotel + "</p></td></tr>";
					lbHmtl.Text += "<tr>";
				}
				if (item.TB != null)
				{
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">T/B</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.TB + "</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.CurrencyTB + "</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.payTB + "</p></td></tr>";
					lbHmtl.Text += "<tr>";
				}
				if (item.Lau != null)
				{
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">Laundry</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.Lau + "</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.CurrencyLau + "</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.payLau + "</p></td></tr>";
					lbHmtl.Text += "<tr>";
				}
				if (item.TT != null)
				{
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">TT</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.TT + "</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.CurrencyTT + "</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.payTT + "</p></td></tr>";
					lbHmtl.Text += "<tr>";
				}
				if (item.Others != null)
				{
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">Other</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.Others + "</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.CurrencyOther + "</p></td>";
					lbHmtl.Text += "<td style=\"vertical-align:top;text-align:center\"><p style=\"text-align:center\">" + item.payOthers + "</p></td></tr>";
					lbHmtl.Text += "<tr>";
				}
				lbHmtl.Text += "</tr>";
			}
			string strsql3 = "select b.EXT04,a.COMMENTS,a.CREATEDATE,b.EXT04 as Signature from dbo.WF_APPROVALHISTORY a left join ORG_USER b on a.EXT01 = b.LOGINNAME";
			strsql3 += " where a.PROCESSNAME = 'Travel Expense Request' and a.INCIDENT = " + int.Parse(incidents) + " order by a.CREATEDATE";
			DataTable log = DataAccess.Instance("BizDB").ExecuteDataTable(strsql3);
			if (log.Rows.Count > 0)
			{
				logDetail.DataSource = log;
				logDetail.DataBind();
			}
		}
	}
}