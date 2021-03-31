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
	public partial class Leavetest : System.Web.UI.Page
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
			dropEmployee.DataValueField = "UserAccount";
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


            string Strsql = "select distinct LeaveYear from COM_LevalManager  order by LeaveYear desc";
            DataTable dtLeaveInfo = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            dropYear.DataSource = dtLeaveInfo;
            dropYear.DataTextField = "LeaveYear";
            dropYear.DataValueField = "LeaveYear";
            dropYear.DataBind();

		}
		
		public void repeaterbind()
		{
            //int pageIndex = AspNetPager1.CurrentPageIndex;
            //int pageSize = AspNetPager1.PageSize;
            //string sql = "select * from ( ";
            //sql += " select L.UserAccount,A.EXT04,A.DEPARTMENTNAME,L.LeaveYear,L.LeaveYearCount,L.LeaveLastYearCount,L.FuallpaySick from ";
            //sql += " (select UserAccount,LeaveYear,LeaveYearCount,cast(LeaveLastYearHourCount/8 as NUMERIC(18,2)) as LeaveLastYearCount,FuallpaySick from COM_LevalManager) L left join "; 
            //sql += " ( select u.EXT04,d.DEPARTMENTNAME,u.LOGINNAME from ORG_USER u left join ORG_JOB j on u.USERID = j.USERID left join ORG_DEPARTMENT d on j.DEPARTMENTID = d.DEPARTMENTID) A ";
            //sql += " on REPLACE(L.UserAccount,'/','\\') = A.LOGINNAME ) B where  LeaveYear='"+dropYear.SelectedValue+"'";

            string sql = @" select * from (  
		 select L.UserAccount,A.EXT04,A.DEPARTMENTNAME,L.LeaveYear,
		 (L.CountLeave) LeaveCount, L.CountLastLeave,
		  (L.CountLeave+L.CountLastLeave)-(L.LeaveYearCount+L.LeaveLastYearCount) ALeavecount,
LeaveLastYearCount,
		 (L.LeaveYearCount+L.LeaveLastYearCount) EnableLeaveCount  ,L.FuallpaySick from  
		 (select CountLeave,CountLastLeave,UserAccount,LeaveYear,LeaveYearCount,cast(LeaveLastYearHourCount/8 as NUMERIC(18,3)) as LeaveLastYearCount,FuallpaySick from COM_LevalManager) L left join  
		 ( select u.EXT04,d.DEPARTMENTNAME,u.LOGINNAME from ORG_USER u left join ORG_JOB j on u.USERID = j.USERID left join ORG_DEPARTMENT d on j.DEPARTMENTID = d.DEPARTMENTID) A  
		  on REPLACE(L.UserAccount,'/','\') = A.LOGINNAME ) B where  LeaveYear='" + dropYear.SelectedValue + "'";

			if (dropDepartment.Text.Trim() != "0")
			{
				if (dropEmployee.Text.Trim() != "0")
				{
					sql += " and UserAccount='" + dropEmployee.SelectedValue.Replace("\\", "/") + "'";
				}
				else
				{
					sql += " and DEPARTMENTNAME ='" + dropDepartment.SelectedValue + "'";
				}
			}

            //string strsqlcount = "select  COUNT(1) from  (" + sql + ")E";
            //AspNetPager1.RecordCount = int.Parse(DataAccess.Instance("BizDB").ExecuteScalar(strsqlcount).ToString());

			string strsql = @" SELECT * FROM  (select ROW_NUMBER() over(order by DEPARTMENTNAME) RN, q.* from  (" + sql + " ) as q)p";
			//strsql += " WHERE RN BETWEEN '" + pageSize * (pageIndex - 1) + "' AND '" + pageSize * pageIndex + "'";
			DataTable dtInfo = DataAccess.Instance("BizDB").ExecuteDataTable(sql);
            if (dtInfo.Rows.Count > 0)
                rpList.DataSource = dtInfo;
            else
                rpList.DataSource = null;
            rpList.DataBind();

            int SelectYear = Convert.ToInt32(dropYear.SelectedValue);

            foreach (RepeaterItem item in rpList.Items)
            {
                Repeater RPLeaveDetail = item.FindControl("RPLeaveDetail") as Repeater;
                HiddenField hdUserID = item.FindControl("hdUserID") as HiddenField;
//                string StrsqlDetail = string.Format(@"select A.DOCUMENTNO,A.REQUESTDATE,B.* from (
//		  select * from PROC_Leave where APPLICANTACCOUNT='{0}' and INCIDENT>0 and  (STATUS=1 or STATUS=2))A
//		  left join PROC_Leave_DT B on A.FORMID=B.FORMID where (StartDate>='{1}' or EndDate>='{1}')
//		  and (StartDate<'{2}' or EndDate<'{2}')", hdUserID.Value, SelectYear + "-01-01", (SelectYear + 1) + "-01-01");
                string StrsqlDetail = string.Format(@"select * from (
		  select A.DOCUMENTNO,A.REQUESTDATE,Applying,
		  (CONVERT(nvarchar(50),StartDate,111)+' '+StartHours+':'+StartMinutes+'~'+CONVERT(nvarchar(50),EndDate,111)+' '+EndHours+':'+EndMinutes) CreateDate, 
		  NoODays,A.INCIDENT
		   from (
		  select * from PROC_Leave where APPLICANTACCOUNT='{0}' and INCIDENT>0 and  (STATUS=1 or STATUS=2))A
		  left join PROC_Leave_DT B on A.FORMID=B.FORMID where (StartDate>='{1}' or EndDate>='{1}')
		  and (StartDate<'{2}' or EndDate<'{2}')
		  union all
		  select '' as DOCUMENTNO,GETDATE() as REQUESTDATE, LOGTYPE Applying, CONVERT(nvarchar(50),CreateDate,111)+' '+ LOGCONTENT,FORMID as NoODays,'' as INCIDENT from COM_LOG where MODULE='LevalType' and FORMNAME='{0}' and(CREATEDATE>='{1}' and CREATEDATE<'{2}')
		  ) C order by REQUESTDATE,INCIDENT", hdUserID.Value, SelectYear + "-01-01", (SelectYear + 1) + "-01-01");
                DataTable dtTraningDetailData = DataAccess.Instance("BizDB").ExecuteDataTable(StrsqlDetail);
                if (dtTraningDetailData.Rows.Count > 0)
                    RPLeaveDetail.DataSource = dtTraningDetailData;
                else
                    RPLeaveDetail.DataSource = null;
                RPLeaveDetail.DataBind();
            }


            string StrsqlExcel =string.Format(@"select A.APPLICANT, A.DOCUMENTNO,A.REQUESTDATE,Applying,
		  (CONVERT(nvarchar(50),StartDate,111)+' '+StartHours+':'+StartMinutes+'~'+CONVERT(nvarchar(50),EndDate,111)+' '+EndHours+':'+EndMinutes) CreateDate, 
		  NoODays  from ( select * from PROC_Leave where   INCIDENT>0 and  (STATUS=1 or STATUS=2))A
		  left join PROC_Leave_DT B on A.FORMID=B.FORMID where (StartDate>='{0}' or EndDate>='{0}')
		  and (StartDate<'{1}' or EndDate<'{1}')", SelectYear + "-01-01", (SelectYear + 1) + "-01-01");
            DataTable dtExcelDetailData = DataAccess.Instance("BizDB").ExecuteDataTable(StrsqlExcel);
            if (dtExcelDetailData.Rows.Count > 0)
                ExcelRPLeaveDetail.DataSource = dtExcelDetailData;
            else
                ExcelRPLeaveDetail.DataSource = null;
            ExcelRPLeaveDetail.DataBind();

		}

        public void BindLeaveYear()
        {
        }

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			//AspNetPager1.CurrentPageIndex = 1;
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