﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;

namespace Ultimus.UWF.Report
{
	public partial class DocumentReiwBallotingList : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadDataInfo();
			}
		}

		public void LoadDataInfo()
		{
			try
			{
				int pageIndex = AspNetPager1.CurrentPageIndex;
				int pageSize = AspNetPager1.PageSize;

                string DeptUsersql = @"select COUNT(*) fROM (select * from ORG_USER where LOGINNAME='"+Page.User.Identity.Name +"') A ";
                       DeptUsersql+="inner join ( select USERID from ORG_JOB where DEPARTMENTID=(select DEPARTMENTID from ORG_DEPARTMENT  where  DEPARTMENTNAME='Quality')) B on  A.USERID=B.USERID";
                int DeptUserCount =Convert.ToInt32(DataAccess.Instance("BizDB").ExecuteScalar(DeptUsersql));
                string sql="";
                if (DeptUserCount > 0)
                    sql = " select * from dbo.PROC_QualityDocumentManagement where status = '2' ";
                else
                    sql = "select * from dbo.PROC_QualityDocumentManagement where status = '2'  and APPLICANTACCOUNT='" + Page.User.Identity.Name.Replace("\\", "/") + "' ";
				if (txtStartTime.Text != "" && txtEndTime.Text != "")
				{
					sql += " and REQUESTDATE between " + txtStartTime.Text + " and" + txtEndTime.Text;
				}
				string strsqlcount = "select  COUNT(1) from (" + sql + ")B";
				AspNetPager1.RecordCount = int.Parse(DataAccess.Instance("BizDB").ExecuteScalar(strsqlcount).ToString());
				string strsql = "SELECT * FROM  (select  ROW_NUMBER() over(order by  REQUESTDATE) RN, * from (" + sql + " ) as q)p";
				strsql += " WHERE RN BETWEEN '" + pageSize * (pageIndex - 1) + "' AND '" + pageSize * pageIndex + "'";
				DataTable dtInfo = DataAccess.Instance("BizDB").ExecuteDataTable(strsql);
				if (dtInfo.Rows.Count > 0)
				{
					travelExpenseReport.DataSource = dtInfo;
					travelExpenseReport.DataBind();
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
			LoadDataInfo();
		}

		protected void AspNetPager1_PageChanged(object sender, EventArgs e)
		{
			LoadDataInfo();
		}
	}
}