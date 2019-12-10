using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyLib;
using System.Data;
using System.Threading;

namespace Ultimus.Service.EventSubscription
{
    public class SubscriptionRun
    {
        private Object thisLock = new Object();

        public void Run()
        {
            lock (thisLock)
            {
                try
                {
                    ISubscription[] list = ServiceContainer.Instance().GetAll<ISubscription>();
                    LogUtil.Info("开始执行Subscription...");
                    DataTable dtTask = DataAccess.Instance("BizDB").ExecuteDataTable("select * from WF_SUBSCRIPTION where status=1 and retrytimes<=6 ");
                    foreach (DataRow row in dtTask.Rows)
                    {
                        try
                        {

                            foreach (ISubscription sub in list)
                            {
                                string eventtype = ConvertUtil.ToString(row["EVENTTYPE"]).ToUpper().Trim();
                                switch (eventtype)
                                {
                                    case "TASKACTIVATED":
                                        //执行TaskActivated
                                        sub.TaskActivated(ConvertUtil.ToString(row["ProcessName"])
                                        , ConvertUtil.ToInt32(row["Incident"])
                                        , ConvertUtil.ToInt32(row["StepId"])
                                        , ConvertUtil.ToString(row["TaskID"]));
                                        break;
                                    case "INCIDENTCOMPLETED":
                                        //执行IncidentCompleted
                                        sub.IncidentCompleted(ConvertUtil.ToString(row["ProcessName"])
                                        , ConvertUtil.ToInt32(row["Incident"]));
                                        break;
                                }
                            }
                            DataAccess.Instance("BizDB").ExecuteNonQuery("update WF_SUBSCRIPTION set status=2,COMPLETEDATE=getdate() where id=" + ConvertUtil.ToInt32(row["ID"]));
                        }
                        catch (Exception ex)
                        {
                            LogUtil.Info("执行Subscription...失败");
                            DataAccess.Instance("BizDB").ExecuteNonQuery("update WF_SUBSCRIPTION set RETRYTIMES=RETRYTIMES+1 where id=" + ConvertUtil.ToInt32(row["ID"]));
                            LogUtil.Error(ex);
                        }
                    }
                    LogUtil.Info("完成执行Subscription...");
                }
                catch (Exception e)
                {
                    LogUtil.Error(e);
                }
            }
        }

        public DateTime GetLastExecuteDate()
        {
            string str = ConfigurationManager.AppSettings["SubscriptionLastDate"];
            if (string.IsNullOrEmpty(str))
            {
                DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                int maxId=ConvertUtil.ToInt32(DataAccess.Instance("BizDB").ExecuteScalar("select max(id) from com_appsettings"));
                DataAccess.Instance("BizDB").ExecuteNonQuery("insert into com_appsettings(id,module,name,value,isactive) values(@id,@module,@name,@value,@isactive)",
                    maxId + 1, "Ultimus.UWF.Workflow", "SubscriptionLastDate", dt.ToString("yyyy/MM/dd HH:mm:ss"), "1");
                return dt;
            }
            return ConvertUtil.ToDateTime(str);
        }

    }
}
