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
	public partial class OTAndDayOffReport1 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				bindDeptList();
				bindEmployee("0");
				LoadDataInfo();
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


            string Strsql = "select distinct OTYear from COM_OTAndDayOffManage where  OTYear is not null  order by OTYear desc";
            DataTable dtOTInfo = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            DropYear.DataSource = dtOTInfo;
            DropYear.DataTextField = "OTYear";
            DropYear.DataValueField = "OTYear";
            DropYear.DataBind();
		}
		public void LoadDataInfo()
		{
			try
			{
                int SelectYear = Convert.ToInt32(DropYear.SelectedValue);
                //int pageIndex = AspNetPager1.CurrentPageIndex;
                //int pageSize = AspNetPager1.PageSize;
                //string sql = "select * from ( ";
                //sql += "select A.OTHour,A.DayOffHour,b.OTHourCount,b.UserAccount LoginName,C.DEPARTMENTNAME,c.EXT04 from ";
                //sql += "(select o.SumHour as OTHour,d.SumHour as DayOffHour,o.APPLICANTACCOUNT as LoginName from (select SUM(SumHour) as SumHour,APPLICANTACCOUNT from PROC_OT where STATUS='2' and StartDate between '" + from.Text + "' and '" + to.Text + "' group by APPLICANTACCOUNT) as o ";
                //sql += " left join (select SUM(SumHour) as SumHour,APPLICANTACCOUNT from PROC_DayOffRecord where STATUS='2' and StartDate between '" + from.Text + "' and '" + to.Text + "' group by APPLICANTACCOUNT) as d on o.APPLICANTACCOUNT = d.APPLICANTACCOUNT ) A ";
                //sql += " right join COM_OTAndDayOffManage B on A.LoginName = B.UserAccount ";
                //sql += " left join (select u.LOGINNAME,u.EXT04,d.DEPARTMENTNAME from ORG_USER u left join ORG_JOB j on u.USERID = j.USERID left join ORG_DEPARTMENT d on j.DEPARTMENTID = d.DEPARTMENTID)C ";
                //sql += " on REPLACE(B.UserAccount,'/','\\') = C.LOGINNAME )D";

//                string sql =string.Format(@"select C.*,D.SumHour,
//(ISNULL(TotalHour,0)+isnull(SumHour,0)-(ISNULL(OTHourCount,0)+ISNULL(LastYearHourCount,0))) DayOff,
//(OTHourCount+ISNULL(LastYearHourCount,0)) RemainOT
// from (select A.*,B.* from 	 
//	 (select u.LOGINNAME,u.EXT04,d.DEPARTMENTNAME from ORG_USER u left join ORG_JOB j on u.USERID = j.USERID left join ORG_DEPARTMENT d on j.DEPARTMENTID = d.DEPARTMENTID where ISACTIVE=1)A
//	 right join  (select * from  dbo.COM_OTAndDayOffManage where OTYear='{0}') B
//	  on A.LOGINNAME=REPLACE(B.UserAccount,'/','\')) C left join 
//	  (select SUM(SumHour) as SumHour,APPLICANTACCOUNT from PROC_OT where STATUS='2' and(YEAR(StartDate)='{0}' or YEAR(EndDate)='{0}')   group by APPLICANTACCOUNT) D
//	  on c.UserAccount=d.APPLICANTACCOUNT where 1=1 ", SelectYear);
                string sql = string.Format(@"select C.*,D.SumHour,
(select ISNULL(SUM(SumHour),0) as DayOff  from PROC_DayOffRecord where INCIDENT>0 AND (STATUS='2' OR STATUS=1) AND (YEAR(StartDate)='{0}' OR YEAR(EndDate)='{0}')
AND APPLICANTACCOUNT=C.UserAccount) DayOff,ISNULL(C.OTHourCount,0)+ISNULL(C.LastYearHourCount,0)  AS RemainOT
 from (select A.*,B.* from 	 
	 (select u.LOGINNAME,u.EXT04,d.DEPARTMENTNAME from ORG_USER u left join ORG_JOB j on u.USERID = j.USERID left join ORG_DEPARTMENT d on j.DEPARTMENTID = d.DEPARTMENTID where ISACTIVE=1)A
	 right join  (select * from  dbo.COM_OTAndDayOffManage where OTYear='{0}') B
	  on A.LOGINNAME=REPLACE(B.UserAccount,'/','\')) C left join 
	  (select SUM(SumHour) as SumHour,APPLICANTACCOUNT from PROC_OT where STATUS='2' and(YEAR(StartDate)='{0}' or YEAR(EndDate)='{0}')   group by APPLICANTACCOUNT) D
	  on c.UserAccount=d.APPLICANTACCOUNT where 1=1 ", SelectYear);
				if (dropDepartment.Text.Trim() != "0")
				{
                    sql += "  and DEPARTMENTNAME ='" + dropDepartment.SelectedValue + "'";
                }
				if (dropEmployee.Text.Trim() != "0")
				{
					sql += " and LOGINNAME='" + dropEmployee.SelectedValue+ "'";
				}
                    //else
                    //{
                    //    sql += " where DEPARTMENTNAME ='" + dropDepartment.SelectedValue + "'";
                    //}
				//}
                //string strsqlcount = "select  COUNT(1) from  (" + sql + ")E";
                //AspNetPager1.RecordCount = int.Parse(DataAccess.Instance("BizDB").ExecuteScalar(strsqlcount).ToString());

                //string strsql = @" SELECT * FROM  (select ROW_NUMBER() over(order by DEPARTMENTNAME) RN, q.* from  (" + sql + " ) as q)p";
                //strsql += " WHERE RN BETWEEN '" + pageSize * (pageIndex - 1) + "' AND '" + pageSize * pageIndex + "'";

                    DataTable dtInfo = DataAccess.Instance("BizDB").ExecuteDataTable(sql);
                   
                    if (dtInfo.Rows.Count > 0)
                        rpList.DataSource = dtInfo;
                    else
                        rpList.DataSource = null;
                    rpList.DataBind();
                  foreach (RepeaterItem item in rpList.Items)
                  {
                      Repeater RPOTdayoffDetail = item.FindControl("RPOTDayOffDetail") as Repeater;
                      HiddenField hdUserID = item.FindControl("hdUserID") as HiddenField;
                      string StrsqlDetail = string.Format(@"  select * from (
	 select  DOCUMENTNO,REQUESTDATE, 'OT' as Applying, (CONVERT(nvarchar(50),StartDate,111)+' '+StartHours+':'+StartMinutes+'~'+CONVERT(nvarchar(50),EndDate,111)+' '+EndHours+':'+EndMinutes) CreateDate ,SumHour from PROC_OT where APPLICANTACCOUNT='{0}' and STATUS='2' and(YEAR(StartDate)='{1}' or YEAR(EndDate)='{1}')    
	  union all
	 select DOCUMENTNO,REQUESTDATE, 'Day Off' as Applying, (CONVERT(nvarchar(50),StartDate,111)+' '+StartHours+':'+StartMinutes+'~'+CONVERT(nvarchar(50),EndDate,111)+' '+EndHours+':'+EndMinutes) CreateDate,(SumHour*-1) SumHour from PROC_DayOffRecord where APPLICANTACCOUNT='{0}' and (STATUS='2' or STATUS='1') and(YEAR(StartDate)='{1}' or YEAR(EndDate)='{1}')    
	  union all
	  select '' as DOCUMENTNO,CREATEDATE as REQUESTDATE,LOGTYPE as Applying,LOGCONTENT as CreateDate,FORMID as SumHour from COM_LOG where MODULE='OTType' and FORMNAME='{0}'
	  ) A order by  REQUESTDATE asc", hdUserID.Value, SelectYear);
                      DataTable dtdetailInfo = DataAccess.Instance("BizDB").ExecuteDataTable(StrsqlDetail);
                      if (dtdetailInfo.Rows.Count > 0)
                          RPOTdayoffDetail.DataSource = dtdetailInfo;
                      else
                          RPOTdayoffDetail.DataSource = null;
                      RPOTdayoffDetail.DataBind();
                  }



                  string StrsqlExcelDetail = string.Format(@"  select * from (
	  select APPLICANT, DOCUMENTNO,REQUESTDATE, 'OT' as Applying, (CONVERT(nvarchar(50),StartDate,111)+' '+StartHours+':'+StartMinutes+'~'+CONVERT(nvarchar(50),EndDate,111)+' '+EndHours+':'+EndMinutes) CreateDate ,(SumHour*-1) SumHour from PROC_OT where     STATUS='2' and(YEAR(StartDate)='{0}' or YEAR(EndDate)='{0}')    
	  union all
	 select APPLICANT, DOCUMENTNO,REQUESTDATE, 'Day Off' as Applying, (CONVERT(nvarchar(50),StartDate,111)+' '+StartHours+':'+StartMinutes+'~'+CONVERT(nvarchar(50),EndDate,111)+' '+EndHours+':'+EndMinutes) CreateDate,SumHour from PROC_DayOffRecord where  (STATUS='2' or STATUS='1') and(YEAR(StartDate)='{0}' or YEAR(EndDate)='{0}')    
	  ) A order by  REQUESTDATE asc", SelectYear);
                  DataTable dtExcelInfo = DataAccess.Instance("BizDB").ExecuteDataTable(StrsqlExcelDetail);
                  if (dtExcelInfo.Rows.Count > 0)
                      RPOTDayOffDetail.DataSource = dtExcelInfo;
                  else
                      RPOTDayOffDetail.DataSource = null;
                  RPOTDayOffDetail.DataBind();


			}
			catch (Exception ex)
			{
				MyLib.LogUtil.Error(ex.Message);
			}
		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			//AspNetPager1.CurrentPageIndex = 1;
			LoadDataInfo();
		}

		protected void AspNetPager1_PageChanged(object sender, EventArgs e)
		{
			LoadDataInfo();
		}

		protected void dropDepartment_SelectedIndexChanged(object sender, EventArgs e)
		{
			bindEmployee(dropDepartment.SelectedValue);
		}
	}
}