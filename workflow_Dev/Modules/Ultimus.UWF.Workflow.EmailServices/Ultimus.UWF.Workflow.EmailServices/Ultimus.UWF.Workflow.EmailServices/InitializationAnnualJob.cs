using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ptcent;
using System.Data;
using MyLib;
using System.IO;

namespace Ultimus.UWF.Workflow.EmailServices
{
    class InitializationAnnualJob : BaseJob
    {

        public string LogName { get { return "InitializationAnnual"; } }
        protected override void OnStart()
        {
            try
            {
                CreateStartYearEmployeeLevelInfo();
            }
            catch (Exception err)
            {
                LogFactory.All.WriteWithError(LogName, err);
            }
        }
        /// <summary>
        /// 生成每年初始化年假信息
        /// </summary>
        public   void CreateStartYearEmployeeLevelInfo()
        {
            //string Strsql = "select * from ORG_USER where ISACTIVE=1";
            string Strsql = @"select B.* from  COM_LevalManager A  left join ORG_USER B on A.UserAccount=REPLACE(B.LOGINNAME,'\','/')
	  where B.LOGINNAME is not null";
            DataTable dtUserInfo = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            if (dtUserInfo.Rows.Count > 0)
            {
                foreach (DataRow item in dtUserInfo.Rows)
                {
                    string TwentyYear = item["EXT15"].ToString();//工作超过20年
                    string JoinDate = item["EntryDate"].ToString();//入职日期
                    if (!string.IsNullOrEmpty(JoinDate))
                    {
                        string UserAccount = item["LOGINNAME"].ToString().Replace("\\", "/");
                        int StartYear = Convert.ToDateTime(JoinDate).Year;
                        int EndYear = DateTime.Now.Year;
                        int JoinYear = EndYear - StartYear;
                        if (TwentyYear == "Y")
                        {
                            if (JoinYear <= 10)
                            {
                                GenalLevelManager(UserAccount, 15);
                            }
                            if (JoinYear > 10 && JoinYear <= 15)
                            {
                                GenalLevelManager(UserAccount, 16 + (JoinYear - 10));
                            }
                            if (JoinYear > 15)
                            {
                                GenalLevelManager(UserAccount, 20);
                            }
                            // GenalLevelManager(UserAccount, 15);
                        }
                        else
                        {
                            if (JoinYear <= 5)
                            {
                                GenalLevelManager(UserAccount, 12);
                            }
                            if (JoinYear > 5 && JoinYear <= 10)
                            {
                                GenalLevelManager(UserAccount, 15);
                            }
                            if (JoinYear > 10 && JoinYear <= 15)
                            {
                                GenalLevelManager(UserAccount, 16 + (JoinYear - 10));
                            }
                            if (JoinYear > 15)
                            {
                                GenalLevelManager(UserAccount, 20);
                            }
                        }
                    }

                }
            }
        }

        public   bool GenalLevelManager(string UserAccount, int DayCount)
        {
            string Strsql = "select * from COM_LevalManager where UserAccount='" + UserAccount + "' and LeaveYear='" + (DateTime.Now.Year - 1) + "' and LeaveYearHourCount>0";
            DataTable dtUserLevelInfo = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            decimal LastYearHour = 0;
            if (dtUserLevelInfo.Rows.Count > 0)
            {
                LastYearHour = Convert.ToDecimal(dtUserLevelInfo.Rows[0]["LeaveYearHourCount"]);
                if (LastYearHour > 40)
                    LastYearHour = 40;
            }
            string StrCreateAnnualLog = @"insert into COM_LOG(ID,MODULE,LOGTYPE,FORMNAME,FORMID,LOGCONTENT,CREATEDATE)
  values((select ISNULL(MAX(ID),0)+1 from COM_LOG),'LevalType','AnnualCreate','" + UserAccount + "','" + DayCount + "','Initialization of annual leave [" + DayCount + "] Day',GETDATE())";
            DataAccess.Instance("BizDB").ExecuteNonQuery(StrCreateAnnualLog);

            string StrInsertsql = @"insert into COM_LevalManager(UserAccount,LeaveYear,LeaveYearCount,LeaveYearHourCount,LeaveLastYearHourCount,FuallpaySick,CountLeave,CountLastLeave)
  values('" + UserAccount + "','" + DateTime.Now.Year + "','" + DayCount + "','" + (DayCount * 8) + "','" + LastYearHour + "','5','" + DayCount + "','" + (LastYearHour / 8).ToString("f2") + "')";
            return DataAccess.Instance("BizDB").ExecuteNonQuery(StrInsertsql) > 0;
        }


        protected override void OnStop()
        {

        }
    }
}
