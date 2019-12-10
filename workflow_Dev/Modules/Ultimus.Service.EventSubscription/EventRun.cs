using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyLib;
using System.Data;
using System.Threading;

namespace Ultimus.Service.EventSubscription
{
    public class EventRun
    {
        private Object thisLock = new Object();

        public void Run()
        {
            lock (thisLock)
            {
                try
                {
                    LogUtil.Info("开始执行Event...");
                    //取task status=1
                    DateTime lastTime=GetLastExecuteDate();
                    DataTable dtTasks = DataAccess.Instance("UltDB").ExecuteDataTable("select * from tasks with(nolock) where status=1 and starttime>=@starttime and taskuser not like '%FLOBOT%'", lastTime);
                    //取incident status=2
                    DataTable dtIncidents = DataAccess.Instance("UltDB").ExecuteDataTable("select * from incidents with(nolock) where status=2 and endtime>=@endtime", lastTime);
                    //执行插入操作
                    DateTime lastupate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
                    DataAccess dac = new DataAccess("BizDB");
                    try
                    {
                        dac.BeginTransaction();
                        foreach (DataRow row in dtTasks.Rows)
                        {
                            dac.ExecuteNonQuery("insert into WF_SUBSCRIPTION(  EVENTTYPE, DESCRIPTION, PROCESSNAME, INCIDENT, TASKID, STEPID, STEPLABEL, TASKUSER, ASSIGNEDTOUSER, CREATEDATE,  STATUS, RETRYTIMES) values( @EVENTTYPE, '', @PROCESSNAME, @INCIDENT, @TASKID, @STEPID, @STEPLABEL, @TASKUSER, @ASSIGNEDTOUSER, @CREATEDATE,  @STATUS, @RETRYTIMES);"
                                , "TaskActivated", ConvertUtil.ToString(row["ProcessName"]),
                                ConvertUtil.ToUInt32(row["INCIDENT"]),
                                ConvertUtil.ToString(row["TASKID"]),
                                ConvertUtil.ToUInt32(row["STEPID"]),
                                ConvertUtil.ToString(row["STEPLABEL"]),
                                ConvertUtil.ToString(row["TASKUSER"]),
                                ConvertUtil.ToString(row["ASSIGNEDTOUSER"]),
                                DateTime.Now, 1, 0);
                        }
                        foreach (DataRow row in dtIncidents.Rows)
                        {
                            dac.ExecuteNonQuery("insert into WF_SUBSCRIPTION(  EVENTTYPE, DESCRIPTION, PROCESSNAME, INCIDENT, TASKID, STEPID, STEPLABEL, TASKUSER, ASSIGNEDTOUSER, CREATEDATE,  STATUS, RETRYTIMES) values( @EVENTTYPE, '', @PROCESSNAME, @INCIDENT, @TASKID, @STEPID, @STEPLABEL, @TASKUSER, @ASSIGNEDTOUSER, @CREATEDATE,  @STATUS, @RETRYTIMES);"
                                , "IncidentCompleted", ConvertUtil.ToString(row["ProcessName"]),
                                ConvertUtil.ToUInt32(row["INCIDENT"]),
                                "",
                                0,
                                "",
                                "",
                                ConvertUtil.ToString(row["INITIATOR"]),
                                DateTime.Now, 1, 0);
                        }
                        dac.ExecuteNonQuery("update com_appsettings set value='"+lastupate.ToString("yyyy/MM/dd HH:mm:ss")+"' where name='SubscriptionLastDate'");
                        dac.CommitTransaction();
                    }
                    catch
                    {
                        dac.RollBackTransaction();
                        throw;
                    }
                    LogUtil.Info("完成执行Event...");
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
