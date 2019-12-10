using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;

namespace Ultimus.UWF.Report
{
	public partial class OTAndDayOffReport : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				bindDeptList();
				bindEmployee();
				bindYear();
				bindMonth();
				LoadDataInfo("dept");
			}
		}

		private void bindMonth()
		{
			dropMonth.Items.Insert(0, new ListItem("--Select please--", "0"));
			dropMonth.Items.Insert(1, new ListItem("January", "1"));
			dropMonth.Items.Insert(2, new ListItem("February", "2"));
			dropMonth.Items.Insert(3, new ListItem("March", "3"));
			dropMonth.Items.Insert(4, new ListItem("April", "4"));
			dropMonth.Items.Insert(5, new ListItem("May", "5"));
			dropMonth.Items.Insert(6, new ListItem("June", "6"));
			dropMonth.Items.Insert(7, new ListItem("July", "7"));
			dropMonth.Items.Insert(8, new ListItem("August", "8"));
			dropMonth.Items.Insert(9, new ListItem("September", "9"));
			dropMonth.Items.Insert(10, new ListItem("October", "10"));
			dropMonth.Items.Insert(11, new ListItem("November", "11"));
			dropMonth.Items.Insert(12, new ListItem("December", "12"));
		}

		private void bindYear()
		{
			int year = DateTime.Now.Year;
			//ListItem yearItem = null;
			//for (int i = year; i > year; i--)
			//{
			//yearItem = new ListItem(i.ToString(), i.ToString());
			dropYear.Items.Insert(0, new ListItem(year.ToString(), year.ToString()));
			dropYear.Items.Insert(1, new ListItem((year - 1).ToString(), (year - 1).ToString()));
			//}
		}

		private void bindEmployee()
		{
			string sql = "select LOGINNAME,EXT04 FROM ORG_USER";
			DataTable dtFinaInfo = DataAccess.Instance("BizDB").ExecuteDataTable(sql);
			dropEmployee.DataTextField = "EXT04";
			dropEmployee.DataValueField = "LOGINNAME";
			dropEmployee.DataSource = dtFinaInfo;
			dropEmployee.DataBind();
		}

		private void bindDeptList()
		{
			string sql = "select DEPARTMENTNAME FROM ORG_DEPARTMENT where PARENTID=1";
			DataTable dtFinaInfo = DataAccess.Instance("BizDB").ExecuteDataTable(sql);
			dropDepartment.DataTextField = "DEPARTMENTNAME";
			dropDepartment.DataValueField = "DEPARTMENTNAME";
			dropDepartment.DataSource = dtFinaInfo;
			dropDepartment.DataBind();
			dropDepartment.Items.Insert(0, new ListItem("All", "All"));
		}
		public void LoadDataInfo(string type)
		{
			try
			{
				int pageIndex = AspNetPager1.CurrentPageIndex;
				int pageSize = AspNetPager1.PageSize;
				string sql = " select B.DEPARTMENT as departmentName,B.APPLICANT as applier,A.hour as hour from (select Sum(SumHour) as hour,APPLICANTACCOUNT from PROC_OT where OTYear ='" + dropYear.SelectedValue + "' and OTMonth = '" + dropMonth.SelectedValue + "'  AND STATUS='2'  GROUP BY APPLICANTACCOUNT)A left join (select DISTINCT  DEPARTMENT,APPLICANT,APPLICANTACCOUNT from PROC_OT) B on A.APPLICANTACCOUNT = B.APPLICANTACCOUNT";
				if (type == "dept")
				{
					if (dropDepartment.Text.Trim() != "All")
					{
						sql += " where B.DEPARTMENTNAME ='" + dropDepartment.SelectedValue + "'";
					}
					
				}
				if (type == "employee") 
				{
					sql += " where B.APPLICANTACCOUNT ='" + dropEmployee.SelectedValue + "'";
				}
				string strsqlcount = "select * from (" + sql + ") as q";
				AspNetPager1.RecordCount = int.Parse(DataAccess.Instance("BizDB").ExecuteScalar(strsqlcount).ToString());

				string strsql = @" SELECT * FROM  (select  ROW_NUMBER() over(order by  INCIDENT) RN, * from (" + sql + " ) as q)";
				strsql += " WHERE RN BETWEEN '" + pageSize * (pageIndex - 1) + "' AND '" + pageSize * pageIndex + "'";

				DataTable dtInfo = DataAccess.Instance("BizDB").ExecuteDataTable(strsql);
				if (dtInfo.Rows.Count > 0)
				{
					rpList.DataSource = dtInfo;
					rpList.DataBind();
				}
			}
			catch (Exception ex)
			{
				MyLib.LogUtil.Error(ex.Message);
			}
		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			AspNetPager1.CurrentPageIndex = 1;
			LoadDataInfo(select.Text);
		}

		protected void AspNetPager1_PageChanged(object sender, EventArgs e)
		{
			LoadDataInfo(select.Text);
		}
	}
}