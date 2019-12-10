using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;

namespace Presale.Process.LocalExpense
{
	public partial class Employee : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				bindDeptList();
				bindEmployee("0");
				repeaterbind();
			}
		}
		private void bindEmployee(string department)
		{
            string sql = "select u.EXT04 as applier,u.LOGINNAME as UserAccount,d.DEPARTMENTNAME from ORG_USER u left join ORG_JOB j on u.USERID = j.USERID left join ORG_DEPARTMENT d on j.DEPARTMENTID = d.DEPARTMENTID   where u.ISACTIVE=1";
			if (department != "0")
			{
				sql += " where DEPARTMENTNAME = '" + department + "'";
			}
			DataTable dtFinaInfo = DataAccess.Instance("BizDB").ExecuteDataTable(sql);
			dropEmployee.DataTextField = "applier";
			dropEmployee.DataValueField = "applier";
			dropEmployee.DataSource = dtFinaInfo;
			dropEmployee.DataBind();
			dropEmployee.Items.Insert(0, new ListItem("All", "0"));
		}

		private void bindDeptList()
		{
			string sql = "select DEPARTMENTNAME FROM ORG_DEPARTMENT where PARENTID=1";
			DataTable dtFinaInfo = DataAccess.Instance("BizDB").ExecuteDataTable(sql);
			dropDepartment.DataTextField = "DEPARTMENTNAME";
			dropDepartment.DataValueField = "DEPARTMENTNAME";
			dropDepartment.DataSource = dtFinaInfo;
			dropDepartment.DataBind();
			dropDepartment.Items.Insert(0, new ListItem("All", "0"));
		}

		public void repeaterbind()
		{
			int pageIndex = AspNetPager1.CurrentPageIndex;
			int pageSize = AspNetPager1.PageSize;
			string sql = "select * from ( select * from PROC_Vendor1 where type = 'Employee') B";
			if (dropDepartment.Text.Trim() != "0")
			{

				if (dropEmployee.Text.Trim() != "0")
				{
					sql += " where VendorName='" + dropEmployee.SelectedValue + "'";
				}
				else { 
					sql += " where VendorName in ";
                    sql += "(select u.EXT04 from dbo.ORG_USER u left join dbo.ORG_JOB j on u.USERID = j.USERID left join dbo.ORG_DEPARTMENT d on j.DEPARTMENTID = d.DEPARTMENTID where d.DEPARTMENTNAME = '" + dropDepartment.SelectedValue + "' and u.ISACTIVE=1)";
				}
			}
			string strsqlcount = "select  COUNT(1) from  (" + sql + ")E";
			AspNetPager1.RecordCount = int.Parse(DataAccess.Instance("BizDB").ExecuteScalar(strsqlcount).ToString());

			string strsql = @" SELECT * FROM  (select ROW_NUMBER() over(order by VendorCode) RN, q.* from  (" + sql + " ) as q)p";
			strsql += " WHERE RN BETWEEN '" + pageSize * (pageIndex - 1) + "' AND '" + pageSize * pageIndex + "'";
			DataTable dtInfo = DataAccess.Instance("BizDB").ExecuteDataTable(strsql);
			if (dtInfo.Rows.Count > 0)
			{
				EmployeeList.DataSource = dtInfo;
				EmployeeList.DataBind();
			}

		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			AspNetPager1.CurrentPageIndex = 1;
			repeaterbind();
		}
		protected void AspNetPager1_PageChanged(object sender, EventArgs e)
		{
			repeaterbind();
		}
		protected void dropDepartment_SelectedIndexChanged(object sender, EventArgs e)
		{
			bindEmployee(dropDepartment.SelectedValue);
		}
	}
}