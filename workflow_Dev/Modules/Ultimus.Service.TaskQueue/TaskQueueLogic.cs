using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using System.Configuration;
using MyLib;
using System.Collections;
using System.Threading;

namespace TaskQueueService
{
    public class TaskQueueLogic
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
                    DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable("select * from WF_TASKQUEUE where status=1 and retrytime<=6");
                    foreach (DataRow row in dt.Rows)
                    {
                        string ID = ConvertUtil.ToString(row["ID"]);
                        string action = ConvertUtil.ToString(row["ACTION"]);
                        string LOGINNAME = ConvertUtil.ToString(row["LOGINNAME"]);
                        string TASKID = ConvertUtil.ToString(row["TASKID"]);
                        string SUMMARY = ConvertUtil.ToString(row["SUMMARY"]);
                        string REASON = ConvertUtil.ToString(row["REASON"]);
                        string SYNC = ConvertUtil.ToString(row["SYNC"]);
                        string VARLIST = ConvertUtil.ToString(row["VARLIST"]);
                        int INCIDENT = ConvertUtil.ToInt32(row["INCIDENT"]);
                        bool sync = true;
                        if (SYNC == "0")
                        {
                            sync = false;
                        }
                        string error = "";
                        List<VarEntity> list = SerializeUtil.XMLDeserialize<List<VarEntity>>(VARLIST);
                        Hashtable table=new Hashtable();
                        if (list.Count>0)
                        {
                            VarLogic logic = new VarLogic();
                            table = logic.GetHashtable(list);
                        }
                        switch (action.ToUpper().Trim())
                        {
                            case "SUBMITTASK":
                                error = SubmitTask(LOGINNAME, TASKID, SUMMARY, table, sync, ref INCIDENT);
                                break;
                            case "RETURNTASK":
                                error = ReturnTask(LOGINNAME, TASKID, REASON, SUMMARY, table, sync);
                                break;
                            case "REJECTTASK":
                                error = RejectTask(LOGINNAME, TASKID, REASON, table, sync);
                                break;
                        }
                        if (!string.IsNullOrEmpty(error))
                        {
                            DataAccess.Instance("BizDB").ExecuteNonQuery("update WF_TASKQUEUE set retrytime=retrytime+1,submitdate=getdate() where id= "+ID);
                            LogUtil.Info("失败." + ID);
                        }
                        else
                        {
                            DataAccess.Instance("BizDB").ExecuteNonQuery("update WF_TASKQUEUE set status=2,submitdate=getdate() where id= " + ID);
                            LogUtil.Info("成功." + ID);
                        }
                        Thread.Sleep(2000);
                    }

                    TransferBegin = false;

                }
            }
            catch (Exception ex)
            {
                TransferBegin = false;
                LogUtil.Info("Error:"+ex.Message);
                LogUtil.Error(ex);
            }
        }

        public virtual string SubmitTask(string userName, string taskId, string summary, Hashtable vars, bool sync, ref int incident)
        {
            Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
            bool flag = task.InitializeFromTaskId(taskId);
            string strError = "";

            if (flag)
            {
                Ultimus.WFServer.Variable[] vs = GetVariables(vars, taskId, userName.Replace("\\", "/"));

                Ultimus.WFServer.Variable[] vs1 = null;
                task.GetAllTaskVariables(out vs1, out strError);
                //vs1.
                if (!sync)
                {
                    incident = -1;
                }
                if (!task.SendFrom(userName.Replace("\\", "/"), vs, "", summary, ref incident, out strError))
                {
                    if (string.IsNullOrEmpty(strError) || incident == 0)
                    {
                        strError = "请稍候从草稿箱打开，重新提交！";
                    }
                }
            }
            return strError;
        }

        public virtual string ReturnTask(string userName, string taskId, string reason, string summary, Hashtable vars, bool sync)
        {
            Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
            bool flag = task.InitializeFromTaskId(taskId);
            string strError = "";
            if (flag)
            {
                Ultimus.WFServer.Variable[] vs = GetVariables(vars, taskId, userName.Replace("\\", "/"));
                task.Return(vs, reason, summary, out strError);

            }
            return strError;
        }

        public virtual string RejectTask(string userName, string taskId, string reason, Hashtable vars, bool sync)
        {
            return AbortProcess(userName, taskId, reason, vars, sync);
        }

        public virtual string AbortProcess(string userName, string taskId, string reason, Hashtable vars, bool sync)
        {
            Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
            bool flag = task.InitializeFromTaskId(taskId);
            userName = userName.Replace("\\", "/");
            string strError = "";
            if (flag)
            {
                Ultimus.WFServer.Incident incident = new Ultimus.WFServer.Incident();
                incident.LoadIncident(task.strProcessName, task.nIncidentNo);
                incident.AbortIncident(userName, reason, out strError);
            }
            return strError;
        }

        public virtual Ultimus.WFServer.Variable[] GetVariables(Hashtable vars, string taskid, string username)
        {
            if (vars == null)
            {
                return null;
            }

            Ultimus.WFServer.Task tsk = new Ultimus.WFServer.Task();
            bool bInitflag = true;
            if (taskid.StartsWith("S"))
            {
                bInitflag = tsk.InitializeFromInitiateTaskId(username, taskid);
            }
            else
            {
                bInitflag = tsk.InitializeFromTaskId(taskid);
            }

            if (!bInitflag)
            {
                throw new Exception("方法GetVariables中初始化Task对象失败。");
            }
            int i = 0;
            Ultimus.WFServer.Variable[] vTaskVarList = null;
            string strError = "";
            tsk.GetAllTaskVariables(out vTaskVarList, out strError);


            Ultimus.WFServer.Variable[] vs = new Ultimus.WFServer.Variable[vTaskVarList.Length];
            foreach (string str in vars.Keys)
            {
                // vTaskVarList.
                bool bflag = false;
                for (int j = 0; j < vTaskVarList.Length; j++)
                {
                    Ultimus.WFServer.Variable vTask = vTaskVarList[j];
                    if (vTask.strVariableName.Trim().ToLower() == str.Trim().ToLower())
                    {
                        bflag = true;
                    }
                }
                if (bflag)
                {
                    Ultimus.WFServer.Variable pvar = new Ultimus.WFServer.Variable();
                    pvar.strVariableName = str;
                    object value = vars[str];

                    //-----------------------------modify by Sky 判断是否数组变量 2013-6-7 18:42W
                    if (value.GetType().FullName == "System.String[]")
                    {
                        pvar.objVariableValue = value as object[];
                    }
                    else if (value.ToString().Contains("|USER"))
                    {
                        object[] objVal = null;
                        string strValue = value.ToString().Replace("|USER", "");
                        if (strValue.Contains("<+>"))
                        {
                            string[] strVals = strValue.Replace("<", "").Replace(">", "").Split(new char[] { '+' });
                            if (strVals.Length > 5)
                                objVal = strVals;
                            else
                            {
                                string[] strNew = new string[5];
                                strVals.CopyTo(strNew, 0);
                                objVal = strVals;
                            }
                        }
                        else
                        {
                            objVal = new object[5];
                            objVal[0] = strValue;
                        }
                        pvar.objVariableValue = objVal;
                    }
                    else
                    {
                        pvar.objVariableValue = new object[] { value };
                    }
                    //------------ ----------------------------
                    vs[i] = pvar;
                    i++;
                }
            }
            return vs;
        }

    }
}
