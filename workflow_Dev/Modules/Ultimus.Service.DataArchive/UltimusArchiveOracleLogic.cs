using System;
using System.Collections.Generic;
using System.Text;
using MyLib;
using System.Data.SqlClient;
using System.Data;
using System.Data.OracleClient;

namespace UltimusArchive
{
    public class UltimusArchiveOracleLogic
    {
        public void Run()
        {
            try
            {
                LogUtil.Info("开始执行");
                string UltimusDBServices_Days = ConfigurationManager.AppSettings["IncidentCompleted_Days"];//获得迁移的时间
                string ArchiveDBName = ConfigurationManager.AppSettings["ArchiveDBName"];

                StringBuilder StrSQL = new StringBuilder();
                DateTime dt = DateTime.Now.AddDays(ConvertUtil.ToInt32(UltimusDBServices_Days) * -1);
                StrSQL.Append("select count(1) from INCIDENTS where STATUS in (2,4) and ENDTIME<=to_date('" + dt.ToShortDateString() + "','yyyy-MM-dd')");
                int count =ConvertUtil.ToInt32( DataAccess.Instance("UltDB").ExecuteScalar(StrSQL.ToString()));
                if (count==0)
                {
                    LogUtil.Info("没有可以迁移的数据!");
                    return;
                }

                StrSQL = new StringBuilder();
                LogUtil.Info("准备迁移");                
                //将已经完成的实例并且时间在迁移时间之前的
                StrSQL.Append("insert into " + ArchiveDBName + ".INCIDENTS select * from INCIDENTS  where STATUS in (2,4) and ENDTIME<=to_date('" + dt.ToShortDateString() + "','yyyy-MM-dd');");
                //将实例的步骤数据迁移
                StrSQL.Append("insert into " + ArchiveDBName + ".TASKS select * from TASKS  where trim(ProcessName)||to_char(INCIDENT) in (select trim(ProcessName)||to_char(INCIDENT) from INCIDENTS  where STATUS in (2,4) and ENDTIME<=to_date('" + dt.ToShortDateString() + "','yyyy-MM-dd'));");
                //迁移完成后删除实例数据
                StrSQL.Append("delete TASKS where trim(ProcessName)||to_char(INCIDENT) in (select trim(ProcessName)||to_char(INCIDENT) from INCIDENTS  where STATUS in (2,4) and ENDTIME<=to_date('" + dt.ToShortDateString() + "','yyyy-MM-dd'));");
                //迁移完成后删除步骤数据
                StrSQL.Append("delete INCIDENTS where  STATUS in (2,4) AND ENDTIME<=to_date('" + dt.ToShortDateString() + "','yyyy-MM-dd');");

                System.Data.OracleClient.OracleConnection conn = new System.Data.OracleClient.OracleConnection(ConfigurationManager.ConnectionStrings["UltDB"].ConnectionString);
                conn.Open();
                try
                {
                    System.Data.OracleClient.OracleTransaction Tran = conn.BeginTransaction();
                    OracleCommand OleComm = new OracleCommand();
                    OleComm.Transaction = Tran;
                    OleComm.Connection = conn;
                    OleComm.CommandTimeout = 360000000;
                    OleComm.CommandText = "begin " + StrSQL.ToString() + " end;"; 
                    OleComm.CommandType = CommandType.Text;
                    count = OleComm.ExecuteNonQuery();
                    if (count > 0)
                    {
                        Tran.Commit();
                        LogUtil.Info("数据迁移成功!影响行数:" + count.ToString());
                    }
                    else
                    {
                        Tran.Rollback();
                        LogUtil.Info("数据迁移失败!");
                    }
                    conn.Close();
                }
                catch (Exception ee)
                {
                    LogUtil.Error(ee);
                    conn.Close();
                }
                conn.Close();
                LogUtil.Info("====SQL:" + StrSQL.ToString());
                LogUtil.Info("");

            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
            }
        }

    }
}
