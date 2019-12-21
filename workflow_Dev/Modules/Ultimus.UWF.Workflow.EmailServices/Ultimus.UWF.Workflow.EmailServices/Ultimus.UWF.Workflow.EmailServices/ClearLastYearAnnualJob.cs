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
    class ClearLastYearAnnualJob : BaseJob
    {

        public string LogName { get { return "ClearLastYearAnnual"; } }
        protected override void OnStart()
        {
            try
            {
                ClearPrevYearLevel();
            }
            catch (Exception err)
            {
                LogFactory.All.WriteWithError(LogName, err);
            }
        }
      
        public static void ClearPrevYearLevel()
        {
            string Strsql = "select * from COM_LevalManager where LeaveYear='" + DateTime.Now.Year + "' and LeaveLastYearHourCount>0";
            DataTable dtLastYear = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            if (dtLastYear.Rows.Count > 0)
            {
                foreach (DataRow item in dtLastYear.Rows)
                {
                    string UserAccount = item["UserAccount"].ToString();
                    decimal LeaveLastYearHourCount = Convert.ToDecimal(item["LeaveLastYearHourCount"]);
                    int TableID = Convert.ToInt32(item["ID"]);
                    string StrClearsql = @"insert into COM_LOG(ID,MODULE,LOGTYPE,FORMNAME,FORMID,LOGCONTENT,CREATEDATE)
  values((select MAX(ID)+1 from COM_LOG),'LevalType','AnnualClear','" + UserAccount + "','" + (LeaveLastYearHourCount / 8).ToString("f2") + "','Clear up last year Annual [" + LeaveLastYearHourCount + "] Hour',GETDATE())";
                    DataAccess.Instance("BizDB").ExecuteNonQuery(StrClearsql);
                    DataAccess.Instance("BizDB").ExecuteNonQuery("update COM_LevalManager set LeaveLastYearHourCount='0.00' where ID='" + TableID + "'");
                }
            }
        }

        protected override void OnStop()
        {

        }

    }
}
