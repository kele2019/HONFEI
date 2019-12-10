﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Data;

namespace Presale.Process.PaymentRequestForm
{
	public partial class Purchase : System.Web.UI.Page
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
			string sql = "select u.EXT04 as applier,u.LOGINNAME as UserAccount,d.DEPARTMENTNAME from ORG_USER u left join ORG_JOB j on u.USERID = j.USERID left join ORG_DEPARTMENT d on j.DEPARTMENTID = d.DEPARTMENTID";
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
            string sql = "select * from ( select DOCUMENTNO,APPLICANT,DEPARTMENT,CostCenterDisplay,CostCenterCode,PriceTerms,Currency,Remarks,Date,TotalAmount,APPLICANTACCOUNT FROM PROC_Purchase where status='2' and (ISNULL(PurchaseOrdStatus,0)=0 or PurchaseOrdStatus=0)) B";
			if (dropDepartment.Text.Trim() != "0")
			{
				if (dropEmployee.Text.Trim() != "0")
				{
					sql += " where APPLICANT='" + dropEmployee.SelectedValue + "'";
				}
				else
				{
					sql += " where DEPARTMENTNAME ='" + dropDepartment.SelectedValue + "'";
				}
			}
			string strsqlcount = "select  COUNT(1) from  (" + sql + ")E";
			AspNetPager1.RecordCount = int.Parse(DataAccess.Instance("BizDB").ExecuteScalar(strsqlcount).ToString());

			string strsql = @" SELECT * FROM  (select ROW_NUMBER() over(order by DEPARTMENTNAME) RN, q.* from  (" + sql + " ) as q)p";
			strsql += " WHERE RN BETWEEN '" + pageSize * (pageIndex - 1) + "' AND '" + pageSize * pageIndex + "'";
			DataTable dtInfo = DataAccess.Instance("BizDB").ExecuteDataTable(sql);
			if (dtInfo.Rows.Count > 0)
			{
				purchaseList.DataSource = dtInfo;
				purchaseList.DataBind();
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
