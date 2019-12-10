using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using MyLib;
using System.Web;
using System.Data;
using Ultimus.WFServer;
using Ultimus.UWF.Workflow.Entity;
using Ultimus.UWF.Workflow.Logic;
using System.Xml;
using Ultimus.UWF.Common.Logic;

namespace Ultimus.UWF.V8.Implementation
{
    public class TaskBase: Workflow.Interface.ITask
    {
        public List<TaskEntity> GetInitTask(string loginName)
        {
            List<TaskEntity> initProcessList = new List<TaskEntity>();
            //load init process
            Ultimus.WFServer.TasklistFilter filter = new Ultimus.WFServer.TasklistFilter();
            filter.strArrUserName = new string[1] { loginName };
            Ultimus.WFServer.Tasklist tl = new Ultimus.WFServer.Tasklist();
            filter.nFiltersMask = Ultimus.WFServer.Filters.nFilter_Initiate;
            string error = "";
            tl.LoadFilteredTasks(filter, out error);
            for (int i = 0; i < tl.GetTasksCount(); i++)
            {
                TaskEntity task = new TaskEntity();
                task.PROCESSNAME = tl.GetAt(i).strProcessName;
                task.TASKID = tl.GetAt(i).strTaskId;
                task.SUMMARY = tl.GetAt(i).strSummary;
                task.HELPURL = tl.GetAt(i).strHelpUrl;

                initProcessList.Add(task);
            }
            return initProcessList;
        }

        public List<TaskEntity> GetMyTask(string filter, List<Common.Entity.ParameterEntity> paras, string sort, int skipResults, int maxResults)
        {
            Hashtable table = TypeUtil.GetHashtable(paras);
            if (table == null)
            {
                table = new Hashtable();
            }
            table.Add("filter", filter);
            table.Add("Sort", sort);
            return DataAccess.Instance("UltDB").GetList<TaskEntity>("TaskLogic_GetMyTask", table, skipResults, maxResults);
        }

        public int GetMyTaskCount(string filter, List<Common.Entity.ParameterEntity> paras)
        {
            Hashtable table = TypeUtil.GetHashtable(paras);
            if (table == null)
            {
                table = new Hashtable();
            }
            table.Add("filter", filter);
            return ConvertUtil.ToInt32(DataAccess.Instance("UltDB").GetObject("TaskLogic_GetMyTaskCount", table));
        }

        public List<TaskEntity> GetMyApproval(string filter, List<Common.Entity.ParameterEntity> paras, string sort, int skipResults, int maxResults)
        {
            Hashtable table = TypeUtil.GetHashtable(paras);
            if (table == null)
            {
                table = new Hashtable();
            }
            table.Add("filter", filter);
            table.Add("Sort", sort);
            return DataAccess.Instance("UltDB").GetList<TaskEntity>("TaskLogic_GetMyApproval", table, skipResults, maxResults);
        }

        public int GetMyApprovalCount(string filter, List<Common.Entity.ParameterEntity> paras)
        {
            Hashtable table = TypeUtil.GetHashtable(paras);
            if (table == null)
            {
                table = new Hashtable();
            }
            table.Add("filter", filter);
            return ConvertUtil.ToInt32(DataAccess.Instance("UltDB").GetObject("TaskLogic_GetMyApprovalCount", table));
        }

        public List<TaskEntity> GetMyRequest(string filter, List<Common.Entity.ParameterEntity> paras, string sort, int skipResults, int maxResults)
        {
            Hashtable table = TypeUtil.GetHashtable(paras);
            if (table == null)
            {
                table = new Hashtable();
            }
            table.Add("filter", filter);
            table.Add("Sort", sort);
            return DataAccess.Instance("UltDB").GetList<TaskEntity>("TaskLogic_GetMyRequest", table, skipResults, maxResults);
        }

        public int GetMyRequestCount(string filter, List<Common.Entity.ParameterEntity> paras)
        {
            Hashtable table = TypeUtil.GetHashtable(paras);
            if (table == null)
            {
                table = new Hashtable();
            }
            table.Add("filter", filter);
            return ConvertUtil.ToInt32(DataAccess.Instance("UltDB").GetObject("TaskLogic_GetMyRequestCount", table));
        }

        public List<TaskEntity> GetTaskList(string filter, List<Common.Entity.ParameterEntity> paras, string sort, int skipResults, int maxResults)
        {
            return DataAccess.Instance("UltDB").ExecuteList<TaskEntity>("SELECT * FROM TASKS WHERE 1=1 "+filter);
        }

        public int GetTaskListCount(string filter, List<Common.Entity.ParameterEntity> paras)
        {
            return 0;
        }

        public TaskEntity GetTaskEntity(string taskID)
        {
            Hashtable table = new Hashtable();
            table.Add("taskID", taskID);
            return DataAccess.Instance("UltDB").GetEntity<TaskEntity>("TaskLogic_GetEntity", table);
        }

        public TaskEntity GetTaskEntityByName(string processName, int incident, string loginName)
        {
            return DataAccess.Instance("UltDB").ExecuteEntity<TaskEntity>("SELECT * FROM TASKS WHERE PROCESSNAME=@PROCESSNAME AND INCIDENT=@INCIDENT AND ASSIGENDTOUSER=@ASSIGENDTOUSER",processName,incident,loginName);
        }

        public string GetTaskUrl(string taskID, string type, string loginName)
        {
            TaskEntity entity = new TaskEntity();
            if (taskID.StartsWith("S"))
            {
                entity = GetStartEntity(taskID);
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
                task.InitializeFromTaskId(loginName.Replace("\\", "/"), taskID);
                processName = task.strProcessName.Trim();
                stepLabel = task.strStepName.Trim();
                incident = task.nIncidentNo;
            }
            else
            {
                processName = entity.PROCESSNAME.Trim();
                stepLabel = entity.STEPLABEL.Trim();
                incident = entity.INCIDENT;
            }

            string page = StepSettingsLogic.GetStepPage(processName, stepLabel);
            string url = "";
            if (string.IsNullOrEmpty(page)) //Standard Form
            {
                string result = "";
                Ultimus.WFServer.Task t = new Ultimus.WFServer.Task();
                t.InitializeFromTaskId(loginName, taskID);
                t.ExtractFormURL(out result);
                if (!string.IsNullOrEmpty(result)) //EIK没有调用到该task
                {
                    if (result.StartsWith("."))
                    {
                        result = result.Replace("./", "");
                    }
                    url = GetStandardClientUrl(result);
                    Ultimus.ClientServices.Services srv = new Ultimus.ClientServices.Services();
                    string sessionid = "";
                    string error = "";
                    srv.LoginUser(loginName.Split('/')[0], loginName.Split('/')[1], "", out sessionid, out error);
                    if (!string.IsNullOrEmpty(error))
                    {
                        throw new Exception("Ultimus Login Error:" + error);
                    }
                    url += "&sid=" + sessionid;
                    //HttpContext.Current.Response.AddHeader("Ultimus Workflow1", "Ultimus Workflow");
                    //HttpContext.Current.Response.Cookies["TaskID"].Value = taskID;
                    //HttpContext.Current.Response.Cookies["TaskID"].Path = @"/";
                    //HttpContext.Current.Response.Cookies["UserID"].Value = userName;
                    //HttpContext.Current.Response.Cookies["UserID"].Path = @"/";
                }
                else
                {
                    if (entity != null) //有这个task,把该task再插入回来
                    {
                        InsertBackFromArchive(entity.PROCESSNAME, entity.INCIDENT);
                        t.InitializeFromTaskId(loginName.Replace("\\", "/"), taskID);
                        t.ExtractFormURL(out result);
                        if (!string.IsNullOrEmpty(result))
                        {
                            if (result.StartsWith("."))
                            {
                                result = result.Replace("./", "");
                            }
                            url = GetStandardClientUrl(result);
                            Ultimus.ClientServices.Services srv = new Ultimus.ClientServices.Services();
                            string sessionid = "";
                            string error = "";
                            srv.LoginUser(loginName.Split('/')[0], loginName.Split('/')[1], "", out sessionid, out error);
                            if (!string.IsNullOrEmpty(error))
                            {
                                throw new Exception("Ultimus Login Error:" + error);
                            }
                            url += "&sid=" + sessionid;
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

        TaskEntity GetStartEntity(string taskID)
        {
            return DataAccess.Instance("UltDB").GetEntity<TaskEntity>("TaskLogic_GetStartEntity", taskID);
        }

        public void InsertBackFromArchive(string processName, int incident)
        {
            string archiveDBName = ConfigurationManager.AppSettings["ArchiveDBName"];
            if (!string.IsNullOrEmpty(archiveDBName))
            {
                Hashtable table = new Hashtable();
                table.Add("processName", processName);
                table.Add("incident", incident);
                table.Add("ArchiveDBName", archiveDBName);
                if (DataAccess.Instance("UltDB").SqlMapper.DataSource.DbProvider.Name.ToUpper().IndexOf("ORACLE") >= 0)
                {
                    DataAccess.Instance("UltDB").Insert("TaskLogic_InsertBackFromArchiveOracle", table);
                }
                else
                {
                    DataAccess.Instance("UltDB").Insert("TaskLogic_InsertBackFromArchive", table);
                }
            }
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

        public int SubmitTask(TaskEntity entity)
        {
            Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
            bool flag = task.InitializeFromTaskId(entity.ASSIGNEDTOUSER.Replace("\\", "/"), entity.TASKID);
            string strError = "";
            int incident = 0;
            if (flag)
            {
                //Ultimus.WFServer.Variable[] vs = GetVariables(vars);
                SetVariables(task, entity.VarList);
                if (!entity.SYNC)
                {
                    incident = -1;
                }
                if (!task.SendFrom(entity.ASSIGNEDTOUSER.Replace("\\", "/"), "", entity.SUMMARY, false, ref incident, out strError))
                {
                    if (string.IsNullOrEmpty(strError) || incident == 0)
                    {
                        strError = "请稍候从草稿箱打开，重新提交！";
                    }
                }
            }
            return incident;
        }

        public virtual string SetVariables(Task task, List<VarEntity> vars)
        {
            string error = "";
            string lasterror = "";
            foreach (VarEntity ety in vars) //USER:org=Business Organization,user=WIN-O7BVH1JUCSN/Administrator
            {
                if (ety.Value != null)
                {
                    // modify by sky 2013/12/5 添加用户赋值
                    string varValue;
                    if (ety.Value.ToString().Contains("|USER"))
                    {
                        string org = ConvertUtil.ToString(ConfigurationManager.AppSettings["Org"]);
                        varValue = "USER:org=" + org + ",user=" + ety.Value.ToString().Replace("|USER", "");
                    }
                    else
                    {
                        varValue = ety.Value.ToString();
                    }
                    object val = new object[] { varValue };
                    //_----------------------------------------------
                    string value = varValue;
                    string[] sz = value.Split('|');
                    if (sz.Length >1)
                    {
                        task.SetNodeValue("TaskData.Global." + ety.Name.ToString(), sz, out error);

                    }
                    else
                    {
                        task.SetNodeValue("TaskData.Global." + ety.Name.ToString(), val, out error);

                    }
                    if (!string.IsNullOrEmpty(error))
                    {
                        lasterror = error;
                    }
                }

            }
            return lasterror;
        }

        public void ReturnTask(TaskEntity entity)
        {
            Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
            bool flag = task.InitializeFromTaskId(entity.ASSIGNEDTOUSER.Replace("\\", "/"), entity.TASKID);
            string strError = "";
            if (flag)
            {
                SetVariables(task, entity.VarList);
                task.Return(entity.REASON, entity.SUMMARY, false, out strError);

            }
        }

        public void RejectTask(TaskEntity task)
        {
            AbortIncident(task);
        }

        public void AbortIncident(TaskEntity entity)
        {
            Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
            bool flag = task.InitializeFromTaskId(entity.ASSIGNEDTOUSER.Replace("\\", "/"), entity.TASKID);
            string userName = entity.ASSIGNEDTOUSER.Replace("\\", "/");
            string strError = "";
            if (flag)
            {
                Ultimus.WFServer.Incident incident = new Ultimus.WFServer.Incident();
                incident.LoadIncident(task.strProcessName, task.nIncidentNo);
                incident.AbortIncident(userName, entity.REASON, out strError);
            }
        }

        public string GetCurrentApprover(string processName, int incident)
        {
            return "";
        }

        public string GetTaskId(string processName, int incident, string loginName)
        {
            return "";
        }

        public byte[] GetGraphicalStatus(string processName, int incident)
        {
            Incident.Status pstatus = new Incident.Status();
            Incident pincident = new Incident();
            pincident.LoadIncident(processName, incident);
            pincident.GetIncidentStatus(out pstatus);
            byte[] bytesGif;
            pstatus.GetGraphicalStatus(pincident.strProcessName, pincident.nIncidentNo, pincident.nVersion, out bytesGif);
            return bytesGif;
        }

        public int GetStepType(string stepID)
        {
            object obj = DataAccess.Instance("UltDB").GetObject("TaskLogic_GetStepType", stepID);
            return ConvertUtil.ToInt32(obj);
        }

        public bool AssignTask(string taskId, string toUser)
        {
            Ultimus.WFServer.Task pTask = new Ultimus.WFServer.Task();
            pTask.InitializeFromTaskId(taskId);
            return pTask.AssignTask(toUser);
        }

        public bool AssignAllCurrentTasks(string fromUser, string toUser)
        {
            Ultimus.OC.User pUserTask = null;
            Ultimus.OC.OrgChart porg = new Ultimus.OC.OrgChart();
            porg.FindUser(fromUser, "", "", out pUserTask);
            return pUserTask.AssignAllCurrentTasks(toUser);
        }

        public bool AssignAllFutureTasks(string fromUser, string toUser, DateTime toDate)
        {
            Ultimus.OC.User pUserTask = null;
            Ultimus.OC.OrgChart porg = new Ultimus.OC.OrgChart();
            if (fromUser.StartsWith("Ultimus"))
            {
                porg = new Ultimus.OC.OrgChart("Ultimus");
            }

            porg.FindUser(fromUser, "", "", out pUserTask);
            return pUserTask.AssignAllFutureTasks(toUser, toDate.ToOADate());
        }

        public List<VarEntity> GetVariableList(string taskID)
        {
            return null;
        }
    }
}