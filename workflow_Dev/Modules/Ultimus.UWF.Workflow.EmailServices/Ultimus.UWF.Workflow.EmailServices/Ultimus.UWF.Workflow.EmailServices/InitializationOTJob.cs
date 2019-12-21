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
    class InitializationOTJob : BaseJob
    {
        public string LogName { get { return "InitializationOT"; } }
        protected override void OnStart()
        {
            try
            {
                CreateOTLastYear();
            }
            catch (Exception err)
            {
                LogFactory.All.WriteWithError(LogName, err);
            }
        }
        public static void CreateOTLastYear()
        {
            string Strsql = @"select * from COM_OTAndDayOffManage where OTYear='" + (DateTime.Now.Year - 1) + "' and OTHourCount>0";
            DataTable dtOTLY = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            if (dtOTLY.Rows.Count > 0)
            {
                foreach (DataRow item in dtOTLY.Rows)
                {
                    string UserAccount = item["UserAccount"].ToString();
                    string LasYearOTHour = item["OTHourCount"].ToString();
                    string StrsqlInsert = string.Format(@"insert into COM_OTAndDayOffManage(UserAccount,LastYearHourCount,OTYear,TotalHour) values
('{0}','{1}','{2}',{3})", UserAccount, LasYearOTHour, DateTime.Now.Year, LasYearOTHour);
                    DataAccess.Instance("BizDB").ExecuteNonQuery(StrsqlInsert);
                }
            }

        }
        protected override void OnStop()
        {

        }
    }
}
