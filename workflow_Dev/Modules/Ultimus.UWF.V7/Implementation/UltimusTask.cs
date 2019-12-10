using System;
using System.Collections.Generic;
using System.Text;
using Ultimus.UWF.Workflow.Entity;
using Ultimus.WFServer;
using MyLib;
using System.Collections;
using Ultimus.UWF.Workflow.Implementation;
using System.Web;
using Ultimus.UWF.Workflow.Logic;

namespace Ultimus.UWF.V7.Implementation
{
    public class UltimusTask:DatabaseTask
    {
        //public override List<TaskEntity> GetInitTaskList(string loginName, string str)
        //{
        //    List<TaskEntity> initProcessList = new List<TaskEntity>();
        //    //load init process
        //    Ultimus.WFServer.TasklistFilter filter = new Ultimus.WFServer.TasklistFilter();
        //    filter.strArrUserName = new string[1] { loginName };
        //    Ultimus.WFServer.Tasklist tl = new Ultimus.WFServer.Tasklist();
        //    filter.nFiltersMask = Ultimus.WFServer.Filters.nFilter_Initiate;
        //    List<string> list = new List<string>();
        //    if (!string.IsNullOrEmpty(str))
        //    {
        //        string[] sz = str.Split(',');
        //        foreach (string ss in sz)
        //        {
        //            list.Add(ss);
        //        }
        //    }
        //    string error = "";
        //    tl.LoadFilteredTasks(filter);
        //    for (int i = 0; i < tl.GetTasksCount(); i++)
        //    {
        //        if (list.Count > 0)
        //        {
        //            if (list.Exists(p => tl.GetAt(i).strProcessName.IndexOf(p) >= 0))
        //            {
        //                TaskEntity task = new TaskEntity();
        //                task.PROCESSNAME = tl.GetAt(i).strProcessName;
        //                task.TASKID = tl.GetAt(i).strTaskId;
        //                task.SUMMARY = tl.GetAt(i).strSummary;
        //                task.HELPURL = tl.GetAt(i).strHelpUrl;
        //                if (string.IsNullOrEmpty(task.SUMMARY))
        //                {
        //                    initProcessList.Add(task);
        //                }
        //                task.SERVERNAME = GetServerEntity().SERVERNAME;
        //            }
        //        }
        //        else
        //        {
        //            TaskEntity task = new TaskEntity();
        //            task.PROCESSNAME = tl.GetAt(i).strProcessName;
        //            task.TASKID = tl.GetAt(i).strTaskId;
        //            task.SUMMARY = tl.GetAt(i).strSummary;
        //            task.HELPURL = tl.GetAt(i).strHelpUrl;
        //            if (string.IsNullOrEmpty(task.SUMMARY))
        //            {
        //                initProcessList.Add(task);
        //            }
        //            task.SERVERNAME = GetServerEntity().SERVERNAME;
        //        }
        //    }
        //    return initProcessList;
        //}

        public override List<TaskEntity> GetInitTaskList(string loginName, string filter)
        {
            string sql = "SELECT INITIATEID AS TASKID,PROCESSNAME,PROCESSVERSION,PROCESSHELPURL AS HELPURL,INITIATOR AS ASSIGNEDTOUSER,'UltimusV7' AS SERVERNAME FROM INITIATE WITH(NOLOCK) where description is null";
            if (!string.IsNullOrEmpty(filter))
            {
                sql = "SELECT INITIATEID AS TASKID,PROCESSNAME,PROCESSVERSION,PROCESSHELPURL AS HELPURL,INITIATOR AS ASSIGNEDTOUSER,'UltimusV7' AS SERVERNAME FROM INITIATE  WITH(NOLOCK) where processname like '%" + filter + "%' and description is null";
            }
            List<TaskEntity> list = DataAccess.Instance("UltDB7").ExecuteList<TaskEntity>(sql);
            return list;
        }

        public override List<TaskEntity> GetDraftTaskList(string loginName, string str)
        {
            List<TaskEntity> initProcessList = new List<TaskEntity>();
            //load init process
            Ultimus.WFServer.TasklistFilter filter = new Ultimus.WFServer.TasklistFilter();
            filter.strArrUserName = new string[1] { loginName };
            Ultimus.WFServer.Tasklist tl = new Ultimus.WFServer.Tasklist();
            filter.nFiltersMask = Ultimus.WFServer.Filters.nFilter_Initiate;
            tl.LoadFilteredTasks(filter);
            for (int i = 0; i < tl.GetTasksCount(); i++)
            {
                TaskEntity task = new TaskEntity();
                task.PROCESSNAME = tl.GetAt(i).strProcessName;
                task.TASKID = tl.GetAt(i).strTaskId;
                task.SUMMARY = tl.GetAt(i).strSummary;
                task.HELPURL = tl.GetAt(i).strHelpUrl;
                if (!string.IsNullOrEmpty(task.SUMMARY))
                {
                    initProcessList.Add(task);
                }
                task.SERVERNAME = GetServerEntity().SERVERNAME;
            }
            return initProcessList;
        }

        public override bool DeleteTask(TaskEntity entity)
        {
            Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
            bool flag = task.InitializeFromTaskId( entity.TASKID);
            if (flag)
            {
                return task.DeleteTask();
            }
            return false;
        }

        public override string GetTaskUrl(string taskID, string type, string loginName)
        {
            TaskEntity entity = new TaskEntity();
            if (taskID.StartsWith("S"))
            {
                entity = GetInitTaskEntity(taskID);
            }
            else
            {
                entity = GetTaskEntity(taskID);
            }
            string processName = "";
            string stepLabel = "";
            int incident;
            if (entity == null) //表里面没有该Task，从EIK中拿
            {
                Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
                task.InitializeFromTaskId( taskID);
                processName = task.strProcessName.Trim();
                stepLabel = task.strStepName.Trim();
                incident = task.nIncidentNo;
            }
            else
            {
                processName = entity.PROCESSNAME.Trim();
                stepLabel = entity.STEPLABEL.Trim();
                incident = entity.INCIDENT;
                if (string.IsNullOrEmpty(loginName))
                {
                    loginName = entity.ASSIGNEDTOUSER;
                }
            }

            string page = StepSettingsLogic.GetStepPage(processName, stepLabel);
            string url = "";
            if (string.IsNullOrEmpty(page)) //Standard Form
            {
                string result = "";
                Ultimus.WFServer.Task t = new Ultimus.WFServer.Task();
                t.InitializeFromTaskId( taskID);
                t.ExtractFormURL(out result);
                if (!string.IsNullOrEmpty(result)) //EIK没有调用到该task
                {
                    if (result.StartsWith("."))
                    {
                        result = result.Replace("./", "");
                    }
                    url = GetStandardClientUrl(result);
                    //Ultimus.ClientServices.Services srv = new Ultimus.ClientServices.Services();
                    //string sessionid = "";
                    //string error = "";
                    //srv.LoginUser(loginName.Split('/')[0], loginName.Split('/')[1], "", out sessionid, out error);
                    //if (!string.IsNullOrEmpty(error))
                    //{
                    //    throw new Exception("Ultimus Login Error:" + error);
                    //}
                    //url += "&sid=" + sessionid;
                    HttpContext.Current.Response.AddHeader("Ultimus Workflow1", "Ultimus Workflow");
                    HttpContext.Current.Response.Cookies["TaskID"].Value = taskID;
                    HttpContext.Current.Response.Cookies["TaskID"].Path = @"/";
                    HttpContext.Current.Response.Cookies["UserID"].Value = loginName;
                    HttpContext.Current.Response.Cookies["UserID"].Path = @"/";
                }
                else
                {
                    if (entity != null) //有这个task,把该task再插入回来
                    {
                        InsertBackFromArchive(entity.PROCESSNAME, entity.INCIDENT);
                        t.InitializeFromTaskId(  taskID);
                        t.ExtractFormURL(out result);
                        if (!string.IsNullOrEmpty(result))
                        {
                            if (result.StartsWith("."))
                            {
                                result = result.Replace("./", "");
                            }
                            url = GetStandardClientUrl(result);
                            //Ultimus.ClientServices.Services srv = new Ultimus.ClientServices.Services();
                            //string sessionid = "";
                            //string error = "";
                            //srv.LoginUser(loginName.Split('/')[0], loginName.Split('/')[1], "", out sessionid, out error);
                            //if (!string.IsNullOrEmpty(error))
                            //{
                            //    throw new Exception("Ultimus Login Error:" + error);
                            //}
                            //url += "&sid=" + sessionid;
                            HttpContext.Current.Response.AddHeader("Ultimus Workflow1", "Ultimus Workflow");
                            HttpContext.Current.Response.Cookies["TaskID"].Value = taskID;
                            HttpContext.Current.Response.Cookies["TaskID"].Path = @"/";
                            HttpContext.Current.Response.Cookies["UserID"].Value = loginName;
                            HttpContext.Current.Response.Cookies["UserID"].Path = @"/";
                        }
                    }
                    else
                    {
                        throw new Exception("OpenForm_CannotLoadTask");
                    }
                }
            }
            else //.net Form
            {
                url = "http://" + HttpContext.Current.Request.Url.Host + ":"
                    + HttpContext.Current.Request.Url.Port + "/" + page + "?ProcessName=" + processName.Trim() + "&StepName=" + stepLabel.Trim() + "&Incident="
                   + incident + "&TaskID=" + taskID.Trim() + "&UserName=" + HttpContext.Current.Server.UrlEncode(loginName) + "&Type=" + type;
            }
            return url;
        }


        public void InsertBackFromArchive(string processName, int incident)
        {
            //string archiveDBName = ConfigurationManager.AppSettings["ArchiveDBName"];
            //if (!string.IsNullOrEmpty(archiveDBName))
            //{
            //    Hashtable table = new Hashtable();
            //    table.Add("processName", processName);
            //    table.Add("incident", incident);
            //    table.Add("ArchiveDBName", archiveDBName);
            //    if (DataAccess.Instance("UltDB").SqlMapper.DataSource.DbProvider.Name.ToUpper().IndexOf("ORACLE") >= 0)
            //    {
            //        DataAccess.Instance("UltDB").Insert("TaskLogic_InsertBackFromArchiveOracle", table);
            //    }
            //    else
            //    {
            //        DataAccess.Instance("UltDB").Insert("TaskLogic_InsertBackFromArchive", table);
            //    }
            //}
        }

        string GetStandardClientUrl(string pUrl)
        {
            string url = MyLib.ConfigurationManager.AppSettings["StandardClientUrl"];
            if (string.IsNullOrEmpty(url))
            {
                string ServerName = HttpContext.Current.Request.Url.Host;

                string URL = "http://" + ServerName + "/Ultweb/" + pUrl;
                return URL;
            }
            else
            {
                return url + pUrl;
            }
        }


        public override void ReturnTask(TaskEntity entity)
        {
            Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
            bool flag = task.InitializeFromTaskId(  entity.TASKID);
            string strError = "";
            if (flag)
            {
                Ultimus.WFServer.Variable[] vs = GetVariables(entity.VarList, entity.TASKID, entity.ASSIGNEDTOUSER.Replace("\\", "/"));
                task.Return(vs, entity.REASON, entity.SUMMARY,  out strError);

            }
        }

        public override void RejectTask(TaskEntity task)
        {
            AbortIncident(task);
        }

        public override void AbortIncident(TaskEntity entity)
        {
            Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
            bool flag = task.InitializeFromTaskId(entity.TASKID);
            string userName = entity.ASSIGNEDTOUSER.Replace("\\", "/");
            string strError = "";
            if (flag)
            {
                Ultimus.WFServer.Incident incident = new Ultimus.WFServer.Incident();
                incident.LoadIncident(task.strProcessName, task.nIncidentNo);
                incident.AbortIncident(userName, entity.REASON, out strError);
            }
        }
         
        public override bool AssignTask(string taskId, string toUser)
        {
            Ultimus.WFServer.Task pTask = new Ultimus.WFServer.Task();
            pTask.InitializeFromTaskId(taskId);
            return pTask.AssignTask(toUser);
        }

        public override bool AssignAllCurrentTasks(string fromUser, string toUser)
        {
            Ultimus.OC.User pUserTask = null;
            Ultimus.OC.OrgChart porg = new Ultimus.OC.OrgChart();
            porg.FindUser(fromUser, "", 0, out pUserTask);
            return pUserTask.AssignAllCurrentTasks(toUser);
        }

        public override bool AssignAllFutureTasks(string fromUser, string toUser, DateTime toDate)
        {
            Ultimus.OC.User pUserTask = null;
            Ultimus.OC.OrgChart porg = new Ultimus.OC.OrgChart();
             

            porg.FindUser(fromUser, "", 0, out pUserTask);
            return pUserTask.AssignAllFutureTasks(toUser, toDate.ToOADate());
        }

        public override bool AssignProcessFutureTasks(string processName, string stepName, string fromUser, string toUser, DateTime toDate)
        {
            return base.AssignProcessFutureTasks(processName, stepName, fromUser, toUser, toDate);
        }


        /// <summary>
        /// 提交任务
        /// </summary>
        /// <param name="userName">任务所属的用户名</param>
        /// <param name="taskId">任务ID</param>
        /// <param name="vars">电子表格变量</param>
        /// <param name="sync">true:同步 false:异步</param>
        /// <returns>同步>0的流程实例号 同步=0 失败 异步：-1</returns>
        public override int SubmitTask(TaskEntity entity)
        {
            Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
            bool flag = task.InitializeFromTaskId(entity.TASKID);
            string strError = "";
            int incident = 0;
            if (flag)
            {
                Ultimus.WFServer.Variable[] vs = GetVariables(entity.VarList, entity.TASKID, entity.ASSIGNEDTOUSER.Replace("\\", "/"));

                Ultimus.WFServer.Variable[] vs1 = null;
                task.GetAllTaskVariables(out vs1, out strError);
                if (!entity.SYNC)
                {
                    incident = -1;
                }
                try
                {
                    if (!task.SendFrom(entity.ASSIGNEDTOUSER.Replace("\\", "/"), vs, "", entity.SUMMARY, ref incident, out strError))
                    {
                        if (string.IsNullOrEmpty(strError) || incident == 0)
                        {
                            strError = "请稍候从草稿箱打开，重新提交！" + strError.Replace("\r", "").Replace("\n", "");
                        }
                    }
                }
                catch
                {
                    if (string.IsNullOrEmpty(strError) || incident == 0)
                    {
                        strError = "请稍候从草稿箱打开，重新提交！" + strError.Replace("\r", "").Replace("\n", "");
                    }
                }
                entity.ERRORMESSAGE = strError;
            }
            return incident;
        }

        public Ultimus.WFServer.Variable[] GetVariables(List<VarEntity> vars, string taskid, string username)
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
            foreach (VarEntity str in vars)
            {
                // vTaskVarList.
                bool bflag = false;
                for (int j = 0; j < vTaskVarList.Length; j++)
                {
                    Ultimus.WFServer.Variable vTask = vTaskVarList[j];
                    if (vTask.strVariableName.Trim().ToLower() == str.Name.Trim().ToLower())
                    {
                        bflag = true;
                    }
                }
                if (bflag)
                {
                    Ultimus.WFServer.Variable pvar = new Ultimus.WFServer.Variable();
                    pvar.strVariableName = str.Name;
                    object value = str.Value;

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

        public override byte[] GetGraphicalStatus(string processName, int incident)
        {
            Incident.Status pstatus = new Incident.Status();
            Incident pincident = new Incident();
            pincident.LoadIncident(processName, incident);
            pincident.GetIncidentStatus(out pstatus);
            byte[] bytesGif;
            pstatus.GetGraphicalStatus(pincident.strProcessName, pincident.nIncidentNo, pincident.nVersion, out bytesGif);
            return bytesGif;
        }
    }
}
