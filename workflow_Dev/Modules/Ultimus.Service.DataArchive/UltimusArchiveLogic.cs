using System;
using System.Collections.Generic;
using System.Text;
using MyLib;
using System.Data.SqlClient;
using System.Data;

namespace UltimusArchive
{
    public class UltimusArchiveLogic
    {
        public void Run()
        {
            try
            {
                LogUtil.Info("开始执行");
                string UltimusDBServices_Days = ConfigurationManager.AppSettings["IncidentCompleted_Days"];//获得迁移的时间
                string ArchiveDBName = ConfigurationManager.AppSettings["ArchiveDBName"];

                StringBuilder StrSQL = new StringBuilder();
                StrSQL.AppendLine("select count(1) from INCIDENTS where STATUS in (2,4) and ENDTIME<=Dateadd(DD,-" + UltimusDBServices_Days + ",GETDATE());");
                int count =ConvertUtil.ToInt32( DataAccess.Instance("UltDB").ExecuteScalar(StrSQL.ToString()));
                if (count == 0)
                {
                    LogUtil.Info("没有可以迁移的数据!");
                    return;
                }

                StrSQL = new StringBuilder();
                LogUtil.Info("准备迁移");
                //将已经完成的实例并且时间在迁移时间之前的
                StrSQL.AppendLine("insert into " + ArchiveDBName + "..INCIDENTS select * from INCIDENTS with(nolock) where STATUS in (2,4) and ENDTIME<=Dateadd(DD,-" + UltimusDBServices_Days + ",GETDATE());");
                //将实例的步骤数据迁移
                StrSQL.AppendLine("insert into " + ArchiveDBName + "..TASKS select * from TASKS with(nolock) where rtrim(ProcessName)+Convert(varchar(10),INCIDENT) in (select rtrim(ProcessName)+Convert(varchar(10),INCIDENT) from INCIDENTS with(nolock) where STATUS in (2,4) and ENDTIME<=Dateadd(DD,-" + UltimusDBServices_Days + ",GETDATE()));");
                //迁移完成后删除实例数据
                StrSQL.AppendLine("delete TASKS where rtrim(ProcessName)+Convert(varchar(10),INCIDENT) in (select rtrim(ProcessName)+Convert(varchar(10),INCIDENT) from INCIDENTS with(nolock) where STATUS in (2,4) and ENDTIME<=Dateadd(DD,-" + UltimusDBServices_Days + ",GETDATE()));");
                //迁移完成后删除步骤数据
                StrSQL.AppendLine("delete INCIDENTS where  STATUS in (2,4) and ENDTIME<=Dateadd(DD,-" + UltimusDBServices_Days + ",GETDATE());");

                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["UltDB"].ConnectionString);
                conn.Open();
                try
                {
                    System.Data.SqlClient.SqlTransaction Tran = conn.BeginTransaction();
                    SqlCommand OleComm = new SqlCommand();
                    OleComm.Transaction = Tran;
                    OleComm.Connection = conn;
                    OleComm.CommandTimeout = 360000000;
                    OleComm.CommandText = StrSQL.ToString();
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
