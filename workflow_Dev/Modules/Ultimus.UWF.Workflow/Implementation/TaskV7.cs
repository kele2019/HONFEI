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


namespace Ultimus.UWF.Workflow.Implementation
{
    public class TaskV7 : Workflow.Interface.ITask
    {
        PageEntity pageEntity;

        public virtual List<TaskEntity> GetMyTask(string filter, Hashtable table, string sort, int skipResults, int maxResults)
        {
            if (table == null)
            {
                table = new Hashtable();
            }
            table.Add("filter", filter);
            table.Add("Sort", sort);
            return DataAccess.Instance("UltDB").GetList<TaskEntity>("TaskLogic_GetMyTask", table, skipResults, maxResults);
        }

        public virtual int GetMyTaskCount(string filter, Hashtable table)
        {
            if (table == null)
            {
                table = new Hashtable();
            }
            table.Add("filter", filter);
            return ConvertUtil.ToInt32(DataAccess.Instance("UltDB").GetObject("TaskLogic_GetMyTaskCount", table));
        }

        public virtual List<TaskEntity> GetMyApproval(string filter, Hashtable table, string sort, int skipResults, int maxResults)
        {
            if (table == null)
            {
                table = new Hashtable();
            }
            table.Add("filter", filter);
            table.Add("Sort", sort);
            return DataAccess.Instance("UltDB").GetList<TaskEntity>("TaskLogic_GetMyApproval", table, skipResults, maxResults);
        }

        public virtual int GetMyApprovalCount(string filter, Hashtable table)
        {
            if (table == null)
            {
                table = new Hashtable();
            }
            table.Add("filter", filter);
            return ConvertUtil.ToInt32(DataAccess.Instance("UltDB").GetObject("TaskLogic_GetMyApprovalCount", table));
        }

        public virtual List<TaskEntity> GetMyRequest(string filter, Hashtable table, string sort, int skipResults, int maxResults)
        {
            if (table == null)
            {
                table = new Hashtable();
            }
            table.Add("filter", filter);
            table.Add("Sort", sort);
            return DataAccess.Instance("UltDB").GetList<TaskEntity>("TaskLogic_GetMyRequest", table, skipResults, maxResults);
        }

        public virtual int GetMyRequestCount(string filter, Hashtable table)
        {
            if (table == null)
            {
                table = new Hashtable();
            }
            table.Add("filter", filter);
            return ConvertUtil.ToInt32(DataAccess.Instance("UltDB").GetObject("TaskLogic_GetMyRequestCount", table));
        }

        /// <summary>
        /// 获取我的任务
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual PageEntity GetMyTask(FilterEntity filter)
        {
            Hashtable table = new Hashtable();
            table.Add("filter", filter.GetAllFilterSql());
            table.Add("Sort", filter.OrderBy);
            pageEntity = new PageEntity();
            pageEntity.TaskDatas = DataAccess.Instance("UltDB").GetList<TaskEntity>("TaskLogic_GetMyTask", table, filter.PageIndex, filter.PageIndex * filter.PageSize);
            pageEntity.Count = GetMyTaskCount(filter);
            return pageEntity;
        }

        public virtual int GetMyTaskCount(FilterEntity filter)
        {
            Hashtable table = new Hashtable();
            table.Add("filter", filter.GetAllFilterSql());
            return ConvertUtil.ToInt32(DataAccess.Instance("UltDB").GetObject("TaskLogic_GetMyTaskCount", table));
        }

        /// <summary>
        /// 获取我的代办
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual PageEntity GetMyApproval(FilterEntity filter)
        {
            Hashtable table = new Hashtable();
            table.Add("filter", filter.GetAllFilterSql());
            table.Add("Sort", filter.OrderBy);
            pageEntity = new PageEntity();
            pageEntity.TaskDatas = DataAccess.Instance("UltDB").GetList<TaskEntity>("TaskLogic_GetMyApproval", table, filter.PageIndex, filter.PageIndex * filter.PageSize);
            pageEntity.Count = GetMyApprovalCount(filter);
            return pageEntity;
        }

        public virtual int GetMyApprovalCount(FilterEntity filter)
        {
            Hashtable table = new Hashtable();
            table.Add("filter", filter.GetAllFilterSql());
            return ConvertUtil.ToInt32(DataAccess.Instance("UltDB").GetObject("TaskLogic_GetMyApprovalCount", table));
        }

        /// <summary>
        /// 获取我的发起任务
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual PageEntity GetMyRequest(FilterEntity filter)
        {
            Hashtable table = new Hashtable();
            table.Add("filter", filter.GetAllFilterSql());
            table.Add("Sort", filter.OrderBy);
            pageEntity = new PageEntity();
            pageEntity.TaskDatas = DataAccess.Instance("UltDB").GetList<TaskEntity>("TaskLogic_GetMyRequest", table, filter.PageIndex, filter.PageIndex * filter.PageSize);
            pageEntity.Count = GetMyRequestCount(filter);
            return pageEntity;
        }

        public virtual int GetMyRequestCount(FilterEntity filter)
        {
            Hashtable table = new Hashtable();
            table.Add("filter", filter.GetAllFilterSql());
            return ConvertUtil.ToInt32(DataAccess.Instance("UltDB").GetObject("TaskLogic_GetMyRequestCount", table));
        }


        public virtual TaskEntity GetEntity(string taskID)
        {
            Hashtable table = new Hashtable();
            table.Add("taskID", taskID);
            return DataAccess.Instance("UltDB").GetEntity<TaskEntity>("TaskLogic_GetEntity", table);
        }

        public virtual string GetTaskId(string processName, int incident)
        {
            return ConvertUtil.ToString(DataAccess.Instance("UltDB").ExecuteScalar(string.Format("select TASKID from TASKS where PROCESSNAME='{0}' and INCIDENT={1}  and charindex('_',RECIPIENT,0)=0  order by stepid desc", processName, incident)));
        }
        public virtual string GetTaskId(string processName, int incident, string userName)
        {
            return ConvertUtil.ToString(DataAccess.Instance("UltDB").ExecuteScalar(string.Format("select TASKID from TASKS where PROCESSNAME='{0}' and INCIDENT={1} AND STATUS=1 and ASSIGNEDTOUSER='{2}'", processName, incident, userName)));
        }

        public virtual TaskEntity GetStartEntity(string taskID)
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

        public virtual string GetTaskUrl(string taskID, string type, string userName)
        {
            TaskEntity entity = new TaskEntity();
            if (taskID.StartsWith("S"))
            {
                entity = GetStartEntity(taskID);
            }
            else
            {
                entity = GetEntity(taskID);
            }
            string processName = "";
            string stepLabel = "";
            int incident;
            if (entity == null) //表里面没有该Task，从EIK中拿
            {
                Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
                task.InitializeFromTaskId(userName.Replace("\\", "/"), taskID);
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
                t.InitializeFromTaskId(userName, taskID);
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
                    srv.LoginUser(userName.Split('/')[0], userName.Split('/')[1], "", out sessionid, out error);
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
                        t.InitializeFromTaskId(userName.Replace("\\", "/"), taskID);
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
                            srv.LoginUser(userName.Split('/')[0], userName.Split('/')[1], "", out sessionid, out error);
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
                   + incident + "&TaskID=" + taskID.Trim() + "&UserName=" + HttpContext.Current.Server.UrlEncode(userName) + "&Type=" + type;
            }
            return url;
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

        public virtual Hashtable LoadTask(string taskId)
        {
            Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
            bool flag = task.InitializeFromTaskId(taskId);
            Hashtable table = new Hashtable();
            if (flag)
            {
                string strStepSchema = "";
                string strError = "";
                UltEIKXMLResolver UltEikXmlResolver;
                if (task.GetStepSchema(out strStepSchema, out UltEikXmlResolver, out strError))
                {
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    doc.LoadXml(strStepSchema);
                    foreach (System.Xml.XmlNode var in doc.ChildNodes)
                    {
                        if (var.Name != null)
                        {
                            table.Add(var.Name, var.Value);
                        }
                    }
                }
            }
            return table;
        }

        public virtual string SetVariables(Task task, Hashtable vars)
        {
            string error = "";
            string lasterror = "";
            foreach (DictionaryEntry ety in vars) //USER:org=Business Organization,user=WIN-O7BVH1JUCSN/Administrator
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
                    if (ety.Value is Array)
                    {
                        task.SetNodeValue("TaskData.Global." + ety.Key.ToString(), ety.Value, out error);

                    }
                    else
                    {
                        task.SetNodeValue("TaskData.Global." + ety.Key.ToString(), val, out error);

                    }
                    if (!string.IsNullOrEmpty(error))
                    {
                        lasterror = error;
                    }
                }

            }
            return lasterror;
        }


        //public virtual Ultimus.WFServer.Variable[] GetVariables(Hashtable vars)
        //{
        //    if (vars == null)
        //    {
        //        return null;
        //    }
        //    Ultimus.WFServer.Variable[] vs = new Ultimus.WFServer.Variable[vars.Count];
        //    int i = 0;
        //    foreach (string str in vars.Keys)
        //    {
        //        Ultimus.WFServer.Variable pvar = new Ultimus.WFServer.Variable();
        //        pvar.strVariableName = str;
        //        object value = vars[str];

        //        //-----------------------------modify by Sky 判断是否数组变量 2013-6-7 18:42W
        //        if (value.GetType().FullName == "System.String[]")
        //        {
        //            pvar.objVariableValue = value as object[];
        //        }
        //        else if (value.ToString().Contains("|USER"))
        //        {
        //            object[] objVal = null;
        //            string strValue = value.ToString().Replace("|USER", "");
        //            if (strValue.Contains("<+>"))
        //            {
        //                string[] strVals = strValue.Replace("<", "").Replace(">", "").Split(new char[] { '+' });
        //                if (strVals.Length > 5)
        //                    objVal = strVals;
        //                else
        //                {
        //                    string[] strNew = new string[5];
        //                    strVals.CopyTo(strNew, 0);
        //                    objVal = strVals;
        //                }
        //            }
        //            else
        //            {
        //                objVal = new object[5];
        //                objVal[0] = strValue;
        //            }
        //            pvar.objVariableValue = objVal;
        //        }
        //        else
        //        {
        //            pvar.objVariableValue = new object[] { value };
        //        }
        //        //------------ ----------------------------
        //        vs[i] = pvar;
        //        i++;
        //    }
        //    return vs;
        //}

        /// <summary>
        /// 提交任务
        /// </summary>
        /// <param name="userName">任务所属的用户名</param>
        /// <param name="taskId">任务ID</param>
        /// <param name="vars">电子表格变量</param>
        /// <param name="sync">true:同步 false:异步</param>
        /// <returns>同步>0的流程实例号 同步=0 失败 异步：-1</returns>
        public virtual string SubmitTask(string userName, string taskId, string summary, Hashtable vars, bool sync, ref int incident)
        {
            Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
            bool flag = task.InitializeFromTaskId(userName.Replace("\\", "/"), taskId);
            string strError = "";
            if (flag)
            {
                //Ultimus.WFServer.Variable[] vs = GetVariables(vars);
                SetVariables(task, vars);
                if (!sync)
                {
                    incident = -1;
                }
                if (!task.SendFrom(userName.Replace("\\", "/"), "", summary, false, ref incident, out strError))
                {
                    if (string.IsNullOrEmpty(strError) || incident == 0)
                    {
                        strError = "请稍候从草稿箱打开，重新提交！";
                    }
                }
            }
            return strError;
        }

        //Task GetTask(string taskId,string userName)
        //{
        //    Task t = new Task();
        //    Tasklist tl = new Tasklist();
        //    TasklistFilter tlf = new TasklistFilter();
        //    tlf.strArrUserName = new string[] { Session["UserID"].ToString() };
        //    tlf.str = strProcessName;
        //    tlf.nIncidentNo = int.Parse(strIncident);
        //    tlf.strArrStepLabelFilter = new string[] { strStepName };
        //    tl.LoadFilteredTasks(tlf, out strErr);
        //    if (tl.GetTasksCount() > 0)
        //        t = tl.GetFirstTask();
        //    return t;
        //}

        public virtual string ReturnTask(string userName, string taskId, string reason, string summary, Hashtable vars, bool sync)
        {
            Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
            bool flag = task.InitializeFromTaskId(userName.Replace("\\", "/"), taskId);
            string strError = "";
            if (flag)
            {
                SetVariables(task, vars);
                task.Return(reason, summary, false, out strError);

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
            bool flag = task.InitializeFromTaskId(userName.Replace("\\", "/"), taskId);
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


        public virtual List<ProcessEntity> GetInitProcessList(string userName)
        {

            List<ProcessEntity> initProcessList = new List<ProcessEntity>();
            //load init process
            Ultimus.WFServer.TasklistFilter filter = new Ultimus.WFServer.TasklistFilter();
            filter.strArrUserName = new string[1] { userName };
            Ultimus.WFServer.Tasklist tl = new Ultimus.WFServer.Tasklist();
            filter.nFiltersMask = Ultimus.WFServer.Filters.nFilter_Initiate;
            string error = "";
            tl.LoadFilteredTasks(filter, out error);
            for (int i = 0; i < tl.GetTasksCount(); i++)
            {
                ProcessEntity process = new ProcessEntity();
                process.PROCESSNAME = tl.GetAt(i).strProcessName;
                process.INITIATEID = tl.GetAt(i).strTaskId;
                initProcessList.Add(process);
            }
            return initProcessList;
        }


        public virtual bool AbortIncident(string taskId, out string strError)
        {
            strError = "";
            try
            {
                Ultimus.WFServer.Task tsk = new Ultimus.WFServer.Task();
                tsk.InitializeFromTaskId(taskId);

                //初始话Incident
                Ultimus.WFServer.Incident inc = new Ultimus.WFServer.Incident();
                inc.LoadIncident(tsk.strProcessName, tsk.nIncidentNo);

                Ultimus.WFServer.Incident.Status iStatus = new Ultimus.WFServer.Incident.Status();
                inc.GetIncidentStatus(out iStatus);
                if (iStatus.nIncidentStatus == Ultimus.WFServer.IncidentStatuses.INCIDENT_STATUS_ACTIVE ||
                    iStatus.nIncidentStatus == Ultimus.WFServer.IncidentStatuses.INCIDENT_STATUS_PENDING)
                {
                    return inc.AbortIncident(inc.strIncidentOwner, "用户Portal取消", out strError);
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                strError = strError + ex.Message;
                return false;
            }

        }


        public List<TaskEntity> GetTaskList(string processName, int incdient)
        {
            Hashtable table = new Hashtable();
            table.Add("PROCESSNAME", processName);
            table.Add("INCIDENT", incdient);
            return DataAccess.Instance("UltDB").GetList<TaskEntity>("TaskLogic_GetTaskList", table);
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
            if (fromUser.StartsWith("ULTIMUSAD"))
            {
                porg = new Ultimus.OC.OrgChart("ULTIMUSAD");
            }

            porg.FindUser(fromUser, "", "", out pUserTask);
            return pUserTask.AssignAllFutureTasks(toUser, toDate.ToOADate());
        }

        public string GetCurrentApprover(string strProcess, int incident)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 权限指派
        /// </summary>
        /// <param name="taskId">TaskID</param>
        /// <param name="toUser">指派给用户</param>
        /// <param name="isSendMail">是否发送邮件</param>
        /// <param name="toMails">收件人</param>
        /// <returns>是否成功</returns>
        public virtual bool AssignTask(string taskId, string toUser, bool isSendMail, string toMails)
        {
            bool isOk = AssignTask(taskId, toUser);
            if (isSendMail)
            {
                //mailHelp.SendMail(toMails, "指派邮件", "您被指派", out isOk, null);
            }
            return isOk;
        }

        /// <summary>
        /// 获取被指派流程
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns>指派流程信息</returns>
        public virtual DataSet GetAssignTask(string user)
        {
            return null;
        }

        /// <summary>
        /// 撤销被指派的流程
        /// </summary>
        /// <param name="taskId">TaskId</param>
        /// <returns>是否成功</returns>
        public virtual bool UnAssignTask(string taskId)
        {
            return false;
        }

        /// <summary>
        /// 根据流程指派任务
        /// </summary>
        /// <param name="fromUser">当前人</param>
        /// <param name="toUser">被指派人</param>
        /// <param name="fromDate">生效起始日期</param>
        /// <param name="toDate">生效结束日期</param>
        /// <param name="isSendMail">是否发送邮件</param>
        /// <param name="toMails">收件人地址，多人可用分号隔开</param>
        /// <returns>是否成功</returns>
        public virtual bool AssignProcessTasks(string fromUser, string toUser, DateTime fromDate, DateTime toDate, bool isSendMail, string toMails)
        {
            return false;
        }

        /// <summary>
        /// 获取转办信息
        /// </summary>
        /// <param name="fromUser">当前人</param>
        /// <returns>返回结果集</returns>
        public virtual DataSet GetAssignProcessTasks(string fromUser)
        {
            return null;
        }

        /// <summary>
        /// 取消任务指派
        /// </summary>
        /// <param name="id">唯一ID</param>
        /// <param name="msg">错误消息</param>
        /// <returns>是否成功</returns>
        public virtual bool UnAssignProcessTask(string id)
        {
            return false;
        }

        /// <summary>
        /// 获取知会内容
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <returns>数据结果集</returns>
        public virtual DataSet GetNotify(string userId)
        {
            return null;
        }

        /// <summary>
        /// 创建一条消息
        /// </summary>
        /// <param name="processName">流程名称</param>
        /// <param name="incdient">实例号</param>
        /// <param name="parentId">上一行消息ID，以此产生“楼层”关系</param>
        /// <param name="fromUser">发出用户</param>
        /// <param name="toUser">知会到用户</param>
        /// <param name="toMails">知会人邮件地址，多人用分号隔开</param>
        /// <param name="ccMails">抄送人邮件地址，多人用分号隔开</param>
        /// <param name="title">知会主题</param>
        /// <param name="context">主会正文内容</param>
        /// <returns>知会是否成功</returns>
        public virtual bool CreateNotify(string processName, int incdient, string parentId, string fromUser, string toUser, string toMails, string ccMails, string title, string context)
        {
            return false;
        }

        /// <summary>
        /// 撤回流程
        /// </summary>
        /// <param name="processName">流程名称</param>
        /// <param name="incdient">实例号</param>
        /// <param name="tagStepName">撤回到的步骤名称</param>
        /// <returns>是否成功</returns>
        public virtual bool BackProcess(string processName, int incdient, string tagStepName)
        {
            return false;
        }

        /// <summary>
        /// 是否允许撤回流程
        /// </summary>
        /// <param name="processName">流程名称</param>
        /// <param name="incdient">实例号</param>
        /// <param name="tagStepName">撤回到的步骤名称</param>
        /// <returns>是否允许撤回。true允许，false不允许</returns>
        public virtual bool IsBackProcess(string processName, int incdient, string tagStepName)
        {
            return false;
        }

        /// <summary>
        /// 催签
        /// </summary>
        /// <param name="toMails">收件人</param>
        /// <param name="title">主题</param>
        /// <param name="context">邮件正文内容</param>
        /// <returns>是否成功</returns>
        public virtual bool UrgeSign(string toMails, string title, string context)
        {
            return false;
        }

        /// <summary>
        /// 获取草稿箱
        /// </summary>
        /// <param name="userId">用户ID，根据传入ID获取对应草稿箱</param>
        /// <returns>数据结果集</returns>
        public virtual DataSet GetDraft(string userId)
        {
            return null;
        }

        /// <summary>
        /// 删除草稿箱
        /// </summary>
        /// <param name="id">唯一ID</param>
        /// <returns>是否成功</returns>
        public virtual bool DeleteDraft(string id)
        {
            return false;
        }

        /// <summary>
        /// 创建一个草稿箱
        /// </summary>
        /// <param name="crtUser">创建人</param>
        /// <param name="processName">流程名称</param>
        /// <param name="fromId">业务主表关键字</param>
        /// <param name="stepName">步骤名称</param>
        /// <param name="incident">实例号，如果是新建则可写入0</param>
        /// <returns>返回是否成功</returns>
        public virtual bool CreateDraft(string crtUser, string processName, string fromId, string stepName, int incident)
        {
            return false;
        }

        /// <summary>
        /// 获取代替信息
        /// </summary>
        /// <param name="fromUserId">被代替人，例如A指派由B代替，这里传入A，传入null不过滤</param>
        /// <param name="toUserId">代替人，例如A指派由B代替，这里传入B，传入null不过滤</param>
        /// <returns></returns>
        public virtual DataSet GetInsteadRequest(string fromUserId, string toUserId)
        {
            return null;
        }

        /// <summary>
        /// 插入一行代填信息
        /// </summary>
        /// <param name="fromUserId">被代替人</param>
        /// <param name="toUserId">代替人</param>
        /// <param name="fromDate">生效开始日期</param>
        /// <param name="endDate">生效结束日期</param>
        /// <returns>返回是否成功</returns>
        public virtual bool CreateInsReq(string fromUserId, string toUserId, DateTime fromDate, DateTime endDate)
        {
            return false;
        }

        /// <summary>
        /// 删除指派信息
        /// </summary>
        /// <param name="id">当前行唯一ID</param>
        /// <returns>返回是否成功</returns>
        public virtual bool DeleteInsReq(string id)
        {
            return false;
        }

        /// <summary>
        /// 获取流程变量
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        public virtual List<VarEntity> GetVariableList(string processName, string taskID)
        {
            //return task.GetVariableList(id);
            return null;
        }

        /// <summary>
        /// 获取流程变量
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        public virtual string GetVariableXml(string processName, string taskID)
        {
            //return task.GetVariableList(id);
            return null;
        }

        public virtual List<string> GetInitialVariable(string processName)
        {
            return null;
        }
    }
}