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
    class ClearLastYearOTJob : BaseJob
    {
        public string LogName { get { return "ClearLastYearAnnual"; } }
        protected override void OnStart()
        {
            try
            {
                ClearOTLastYear();
            }
            catch (Exception err)
            {
                LogFactory.All.WriteWithError(LogName, err);
            }
        }
        public static void ClearOTLastYear()
        {
            string Strsql = @"SELECT * FROM (
select  A.*,LEFT(B.USERCODE,2) USERCODE  from COM_OTAndDayOffManage A LEFT JOIN ORG_USER B
ON A.UserAccount=REPLACE(B.LOGINNAME,'\','/')) C where USERCODE IN('U1','U2') AND OTYear='" + DateTime.Now.Year + "' and LastYearHourCount>0";
            DataTable dtOTLY = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            if (dtOTLY.Rows.Count > 0)
            {
                foreach (DataRow item in dtOTLY.Rows)
                {
                    string UserAccount = item["UserAccount"].ToString();
                    string LastYearHourCount = item["LastYearHourCount"].ToString();
                    string ID = item["ID"].ToString();
                    string StrsqlClear = string.Format(@"update COM_OTAndDayOffManage set LastYearHourCount=0 where ID='" + ID + "'");
                    DataAccess.Instance("BizDB").ExecuteNonQuery(StrsqlClear);
                    string StrClearsql = @"insert into COM_LOG(ID,MODULE,LOGTYPE,FORMNAME,FORMID,LOGCONTENT,CREATEDATE)
  values((select MAX(ID)+1 from COM_LOG),'OTType','OTClear','" + UserAccount + "','" + LastYearHourCount + "','Clear up last year OT [" + LastYearHourCount + "] Hour',GETDATE())";
                    DataAccess.Instance("BizDB").ExecuteNonQuery(StrClearsql);
                }
            }
        }

        protected override void OnStop()
        {

        }
    }
}
