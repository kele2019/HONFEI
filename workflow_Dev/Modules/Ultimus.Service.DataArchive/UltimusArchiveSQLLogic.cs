using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using System.Configuration;
using MyLib;

namespace UltimusArchive
{
    public class UltimusArchiveSQLLogic
    {
        /// <summary>
        /// 数据迁移服务是否正在执行
        /// </summary>
        private static bool TransferBegin = false;

        /// <summary>
        /// 数据迁移
        /// </summary>
        public void Run()
        {
            try
            {
                //判断是否现在正在执行数据迁移动作
                if (!TransferBegin)
                {
                    //先将标示修改成正在执行数据迁移动作
                    TransferBegin = true;

                    //Ultimus系统数据库操作对象
                    DbHelper UltimusDB = new DbHelper(MyLib.ConfigurationManager.ConnectionStrings["UltDB"].ToString(), MyLib.ConfigurationManager.ConnectionStrings["UltDB"].ProviderName);
                    string UltimusTransferStr = MyLib.ConfigurationManager.ConnectionStrings["UltArchiveDB"].ToString();
                    //数据迁移库名称
                    string TransferDBName = MyLib.ConfigurationManager.AppSettings["ArchiveDBName"].ToString();

                    //sql语句
                    string ExecuteStrSql;

                    //数据迁移库操作对象
                    DbHelper TransferDB = new DbHelper(UltimusTransferStr, MyLib.ConfigurationManager.ConnectionStrings["UltArchiveDB"].ProviderName);
                    while (true)//批量处理 500 一次
                    {
                        int ExecuteMigrate = 0;

                        ExecuteStrSql = "select top(500) * from INCIDENTS with(nolock) where STATUS in (2,4) and Datediff(DD,ENDTIME,GETDATE())>{0}";
                        ExecuteStrSql = string.Format(ExecuteStrSql, MyLib.ConfigurationManager.AppSettings["IncidentCompleted_Days"]);
                        DataTable dt = UltimusDB.ExecuteDataTable(ExecuteStrSql);
                        if (dt.Rows.Count < 1)
                        {
                            LogUtil.Info("当前没有数据满足迁移条件...,此次不做动作.");
                            TransferBegin = false;
                            break;
                        }
                        LogUtil.Info("开始数据迁移...可迁移的Incident总记录数：" + dt.Rows.Count);

                        Trans TransferTran = new Trans(UltimusTransferStr, MyLib.ConfigurationManager.ConnectionStrings["UltArchiveDB"].ProviderName);
                        Trans UltimusTran = new Trans(MyLib.ConfigurationManager.ConnectionStrings["UltDB"].ToString(), MyLib.ConfigurationManager.ConnectionStrings["UltDB"].ProviderName);

                        foreach (DataRow row in dt.Rows)
                        {
                            try
                            {
                                #region Incident data Migrate
                                ExecuteStrSql = @"delete from INCIDENTS where PROCESSNAME=@PROCESSNAME and INCIDENT=@INCIDENT;
                                                  insert into INCIDENTS values(@PROCESSNAME,@PROCESSVERSION,@INCIDENT,@PRIORITY,@SUMMARY,@STARTTIME,@ENDTIME,@STATUS,@INITIATOR,@PARENTTASKID,@TIMELIMIT,@HELPURL,@MAINSS);";
                                DbCommand IncidentCommand = TransferDB.GetSqlStringCommond(ExecuteStrSql);

                                TransferDB.AddInParameter(IncidentCommand, "@PROCESSNAME", DbType.String, row["PROCESSNAME"]);
                                TransferDB.AddInParameter(IncidentCommand, "@PROCESSVERSION", DbType.Int32, row["PROCESSVERSION"]);
                                TransferDB.AddInParameter(IncidentCommand, "@INCIDENT", DbType.Int32, row["INCIDENT"]);
                                TransferDB.AddInParameter(IncidentCommand, "@PRIORITY", DbType.Int32, row["PRIORITY"]);
                                TransferDB.AddInParameter(IncidentCommand, "@SUMMARY", DbType.String, row["SUMMARY"]);
                                TransferDB.AddInParameter(IncidentCommand, "@STARTTIME", DbType.DateTime, row["STARTTIME"]);
                                TransferDB.AddInParameter(IncidentCommand, "@ENDTIME", DbType.DateTime, row["ENDTIME"]);
                                TransferDB.AddInParameter(IncidentCommand, "@STATUS", DbType.Int32, row["STATUS"]);
                                TransferDB.AddInParameter(IncidentCommand, "@INITIATOR", DbType.String, row["INITIATOR"]);
                                TransferDB.AddInParameter(IncidentCommand, "@PARENTTASKID", DbType.String, row["PARENTTASKID"]);
                                TransferDB.AddInParameter(IncidentCommand, "@TIMELIMIT", DbType.DateTime, row["TIMELIMIT"]);
                                TransferDB.AddInParameter(IncidentCommand, "@HELPURL", DbType.String, row["HELPURL"]);
                                TransferDB.AddInParameter(IncidentCommand, "@MAINSS", DbType.Binary, row["MAINSS"]);

                                #endregion

                                if (TransferDB.ExecuteNonQuery(IncidentCommand, TransferTran) >= 0)
                                {
                                    #region Delete Incident data
                                    ExecuteStrSql = "delete from INCIDENTS where PROCESSNAME=@PROCESSNAME and INCIDENT=@INCIDENT and PROCESSVERSION=@PROCESSVERSION";
                                    DbCommand IncidentComand2 = UltimusDB.GetSqlStringCommond(ExecuteStrSql);
                                    UltimusDB.AddInParameter(IncidentComand2, "@PROCESSNAME", DbType.String, row["PROCESSNAME"]);
                                    UltimusDB.AddInParameter(IncidentComand2, "@INCIDENT", DbType.Int32, row["INCIDENT"]);
                                    UltimusDB.AddInParameter(IncidentComand2, "@PROCESSVERSION", DbType.String, row["PROCESSVERSION"]);
                                    if (UltimusDB.ExecuteNonQuery(IncidentComand2, UltimusTran) < 0)
                                    {
                                        TransferTran.RollBack();
                                        UltimusTran.RollBack();
                                        LogUtil.Error(this.GetType(), "删除 INCIDENTS 数据时出错.PROCESSNAME:[" + row["PROCESSNAME"].ToString().Trim() + "],INCIDENT:[" + row["INCIDENT"].ToString().Trim() + "],PROCESSVERSION:[" + row["PROCESSVERSION"].ToString().Trim() + "]");
                                        continue;
                                    }
                                    LogUtil.Info("成功 INCIDENTS表数据 流程名称:[" + row["PROCESSNAME"].ToString().Trim() + "] 流程版本:[" + row["PROCESSVERSION"].ToString().Trim() + "] 实例编号:[" + row["INCIDENT"].ToString().Trim() + "] ");
                                    #endregion

                                    #region TASK data Migrate

                                    ExecuteStrSql = "select * from tasks with(nolock) where PROCESSNAME='{0}' and INCIDENT='{1}'";
                                    ExecuteStrSql = string.Format(ExecuteStrSql, row["PROCESSNAME"], row["INCIDENT"]);
                                    DataTable tsk = UltimusDB.ExecuteDataTable(ExecuteStrSql);
                                    foreach (DataRow dr in tsk.Rows)
                                    {
                                        ExecuteStrSql = @"delete from tasks where TASKID=@TASKID and PROCESSNAME=@PROCESSNAME and INCIDENT=@INCIDENT;
                                                          insert into tasks values(@TASKID,@PROCESSNAME,@PROCESSVERSION,@INCIDENT,@STEPID,@STEPLABEL,@RECIPIENT,@RECIPIENTTYPE,@TASKUSER,@ASSIGNEDTOUSER,@STATUS,@SUBSTATUS,@STARTTIME,@ENDTIME,@QSTARTTIME,@QENDTIME,@DELAYTIME,@OVERDUETIME,@URGENTDUETIME,@TASKRATE,@TASKTIME,@COST,@HELPURL,@NOTES,@REFERER,@LOCALSS);";
                                        DbCommand TaskCommand = TransferDB.GetSqlStringCommond(ExecuteStrSql);

                                        TransferDB.AddInParameter(TaskCommand, "@TASKID", DbType.String, dr["TASKID"]);
                                        TransferDB.AddInParameter(TaskCommand, "@PROCESSNAME", DbType.String, dr["PROCESSNAME"]);
                                        TransferDB.AddInParameter(TaskCommand, "@PROCESSVERSION", DbType.Int32, dr["PROCESSVERSION"]);
                                        TransferDB.AddInParameter(TaskCommand, "@INCIDENT", DbType.Int32, dr["INCIDENT"]);
                                        TransferDB.AddInParameter(TaskCommand, "@STEPID", DbType.String, dr["STEPID"]);
                                        TransferDB.AddInParameter(TaskCommand, "@STEPLABEL", DbType.String, dr["STEPLABEL"]);
                                        TransferDB.AddInParameter(TaskCommand, "@RECIPIENT", DbType.String, dr["RECIPIENT"]);
                                        TransferDB.AddInParameter(TaskCommand, "@RECIPIENTTYPE", DbType.Int32, dr["RECIPIENTTYPE"]);
                                        TransferDB.AddInParameter(TaskCommand, "@TASKUSER", DbType.String, dr["TASKUSER"]);
                                        TransferDB.AddInParameter(TaskCommand, "@ASSIGNEDTOUSER", DbType.String, dr["ASSIGNEDTOUSER"]);
                                        TransferDB.AddInParameter(TaskCommand, "@STATUS", DbType.Int32, dr["STATUS"]);
                                        TransferDB.AddInParameter(TaskCommand, "@SUBSTATUS", DbType.Int32, dr["SUBSTATUS"]);
                                        TransferDB.AddInParameter(TaskCommand, "@STARTTIME", DbType.DateTime, dr["STARTTIME"]);
                                        TransferDB.AddInParameter(TaskCommand, "@ENDTIME", DbType.DateTime, dr["ENDTIME"]);
                                        TransferDB.AddInParameter(TaskCommand, "@QSTARTTIME", DbType.DateTime, dr["QSTARTTIME"]);
                                        TransferDB.AddInParameter(TaskCommand, "@QENDTIME", DbType.DateTime, dr["QENDTIME"]);
                                        TransferDB.AddInParameter(TaskCommand, "@DELAYTIME", DbType.DateTime, dr["DELAYTIME"]);
                                        TransferDB.AddInParameter(TaskCommand, "@OVERDUETIME", DbType.DateTime, dr["OVERDUETIME"]);
                                        TransferDB.AddInParameter(TaskCommand, "@URGENTDUETIME", DbType.DateTime, dr["URGENTDUETIME"]);
                                        TransferDB.AddInParameter(TaskCommand, "@TASKRATE", DbType.Decimal, dr["TASKRATE"]);
                                        TransferDB.AddInParameter(TaskCommand, "@TASKTIME", DbType.Int32, dr["TASKTIME"]);
                                        TransferDB.AddInParameter(TaskCommand, "@COST", DbType.Decimal, dr["COST"]);
                                        TransferDB.AddInParameter(TaskCommand, "@HELPURL", DbType.String, dr["HELPURL"]);
                                        TransferDB.AddInParameter(TaskCommand, "@NOTES", DbType.String, dr["NOTES"]);
                                        TransferDB.AddInParameter(TaskCommand, "@REFERER", DbType.String, dr["REFERER"]);
                                        TransferDB.AddInParameter(TaskCommand, "@LOCALSS", DbType.Binary, dr["LOCALSS"]);

                                        if (TransferDB.ExecuteNonQuery(TaskCommand, TransferTran) >= 0)
                                        {
                                            LogUtil.Info("成功 TASKS表数据 流程名称:[" + dr["PROCESSNAME"].ToString().Trim() + "] 流程版本:[" + dr["PROCESSVERSION"].ToString().Trim() + "] 实例编号:[" + dr["INCIDENT"].ToString().Trim() + "] 步骤名称:[" + dr["STEPLABEL"].ToString().Trim() + "]");
                                            continue;
                                        }
                                        else
                                        {
                                            TransferTran.RollBack();
                                            UltimusTran.RollBack();
                                            LogUtil.Error(this.GetType(), "失败 TASKS表数据 流程名称:[" + dr["PROCESSNAME"].ToString().Trim() + "] 流程版本:[" + dr["PROCESSVERSION"].ToString().Trim() + "] 实例编号:[" + dr["INCIDENT"].ToString().Trim() + "] 步骤名称:[" + dr["STEPLABEL"].ToString().Trim() + "]");
                                            continue;
                                        }
                                    }
                                    //将Incidnets中对应的Tasks数据全部迁移成功后删除
                                    ExecuteStrSql = "delete from tasks where PROCESSNAME=@PROCESSNAME and INCIDENT=@INCIDENT and PROCESSVERSION=@PROCESSVERSION";
                                    DbCommand TaskCommand2 = UltimusDB.GetSqlStringCommond(ExecuteStrSql);
                                    UltimusDB.AddInParameter(TaskCommand2, "@PROCESSNAME", DbType.String, row["PROCESSNAME"]);
                                    UltimusDB.AddInParameter(TaskCommand2, "@INCIDENT", DbType.Int32, row["INCIDENT"]);
                                    UltimusDB.AddInParameter(TaskCommand2, "@PROCESSVERSION", DbType.String, row["PROCESSVERSION"]);

                                    if (UltimusDB.ExecuteNonQuery(TaskCommand2, UltimusTran) >= 0)
                                    {
                                        //提交事物
                                        TransferTran.Commit();
                                        UltimusTran.Commit();
                                        //从新创建事物对象
                                        TransferTran = new Trans(UltimusTransferStr, MyLib.ConfigurationManager.ConnectionStrings["UltArchiveDB"].ProviderName);
                                        UltimusTran = new Trans(MyLib.ConfigurationManager.ConnectionStrings["UltDB"].ToString(), MyLib.ConfigurationManager.ConnectionStrings["UltDB"].ProviderName);
                                        //记录迁移记录数的标示递增
                                        ExecuteMigrate++;
                                        LogUtil.Info("成功迁移 流程名称:[" + row["PROCESSNAME"].ToString().Trim() + "] 流程版本:[" + row["PROCESSVERSION"].ToString().Trim() + "] 实例编号:[" + row["INCIDENT"].ToString().Trim() + "]");
                                    }
                                    else
                                    {
                                        TransferTran.RollBack();
                                        UltimusTran.RollBack();
                                        LogUtil.Error(this.GetType(), "删除 TASKS 数据时出错.PROCESSNAME:[" + row["PROCESSNAME"].ToString().Trim() + "],INCIDENT:[" + row["INCIDENT"].ToString().Trim() + "],PROCESSVERSION:[" + row["PROCESSVERSION"].ToString().Trim() + "]");
                                        continue;
                                    }
                                    #endregion
                                }
                                else
                                {
                                    TransferTran.RollBack();
                                    UltimusTran.RollBack();
                                    LogUtil.Error(this.GetType(), "新增 INCIDENTS 数据时出错.PROCESSNAME:[" + row["PROCESSNAME"].ToString().Trim() + "],INCIDENT:[" + row["INCIDENT"].ToString().Trim() + "],PROCESSVERSION:[" + row["PROCESSVERSION"].ToString().Trim() + "]");
                                    continue;
                                }
                            }
                            catch (Exception ex1)
                            {
                                TransferTran.RollBack();
                                UltimusTran.RollBack();
                                LogUtil.Error(this.GetType(), ex1);
                                continue;
                            }
                        }
                        LogUtil.Info("成功迁移 " + ExecuteMigrate.ToString() + " 条实例数据.");
                        TransferBegin = false;
                    }

                }
            }
            catch (Exception ex)
            {
                TransferBegin = false;
                LogUtil.Error(this.GetType(), ex);
                
            }
        }
    }
}
