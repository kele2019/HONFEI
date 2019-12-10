using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using MyLib;
using System.Web;
using System.Data;
using Ultimus.UWF.Workflow.Entity;
using Ultimus.UWF.Workflow.Logic;
using System.Xml;
using Ultimus.UWF.Common.Logic;

namespace Ultimus.UWF.Workflow.Implementation
{
    public class DatabaseTask: Workflow.Interface.ITask
    {
        //调用接口实现

        public virtual List<TaskEntity> GetInitTaskList(string loginName,string filter)
        {
            string sql = "SELECT INITIATEID AS TASKID,PROCESSNAME,PROCESSVERSION,PROCESSHELPURL AS HELPURL,INITIATOR AS ASSIGNEDTOUSER,'" + GetServerName() + "' AS SERVERNAME FROM INITIATE WITH(NOLOCK)";
            if (!string.IsNullOrEmpty(filter))
            {
                sql = "SELECT INITIATEID AS TASKID,PROCESSNAME,PROCESSVERSION,PROCESSHELPURL AS HELPURL,INITIATOR AS ASSIGNEDTOUSER,'" + GetServerName() + "' AS SERVERNAME FROM INITIATE  WITH(NOLOCK) where processname like '%" + filter + "%'";
            }
            List<TaskEntity> list = DataAccess.Instance(GetDBName()).ExecuteList<TaskEntity>(sql);
            return list;
        }

        //调用接口实现
        public virtual List<TaskEntity> GetDraftTaskList(string loginName, string filter)
        {
            return null;
        }

        public virtual List<TaskEntity> GetMyTaskList(string loginName,string filter, List<Common.Entity.ParameterEntity> paras, string sort, int skipResults, int maxResults)
        {
            Hashtable table = TypeUtil.GetHashtable(paras);
            if (table == null)
            {
                table = new Hashtable();
            }
            table.Add("filter", filter);
            table.Add("Sort", sort);
            table.Add("SERVERNAME", "'" + GetServerName() + "'");
            table.Add("ASSIGNEDTOUSER", "'" + loginName.Replace("\\", "/") + "'");
            return DataAccess.Instance(GetDBName()).GetList<TaskEntity>("TaskLogic_GetMyTask", table, skipResults, maxResults);
        }

        string GetServerName()
        {
            return GetServerEntity().SERVERNAME;
        }

        string GetDBName()
        {
            string str = GetServerEntity().DBNAME;
            if (string.IsNullOrEmpty(str))
            {
                str = "UltDB";
            }
            return str;
        }

        public virtual int GetMyTaskCount(string loginName, string filter, List<Common.Entity.ParameterEntity> paras)
        {
            Hashtable table = TypeUtil.GetHashtable(paras);
            if (table == null)
            {
                table = new Hashtable();
            }
            table.Add("filter", filter);
            table.Add("ASSIGNEDTOUSER", "'" + loginName.Replace("\\", "/") + "'");
            return ConvertUtil.ToInt32(DataAccess.Instance(GetDBName()).GetObject("TaskLogic_GetMyTaskCount", table));
        }

        public virtual List<TaskEntity> GetMyApprovalList(string loginName, string filter, List<Common.Entity.ParameterEntity> paras, string sort, int skipResults, int maxResults)
        {
            Hashtable table = TypeUtil.GetHashtable(paras);
            if (table == null)
            {
                table = new Hashtable();
            }
            table.Add("filter", filter);
            table.Add("Sort", sort);
            table.Add("SERVERNAME", "'" + GetServerName() + "'");
            table.Add("ASSIGNEDTOUSER", "'" + loginName.Replace("\\", "/") + "'");
            return DataAccess.Instance(GetDBName()).GetList<TaskEntity>("TaskLogic_GetMyApproval", table, skipResults, maxResults);
        }

        public virtual int GetMyApprovalCount(string loginName, string filter, List<Common.Entity.ParameterEntity> paras)
        {
            Hashtable table = TypeUtil.GetHashtable(paras);
            if (table == null)
            {
                table = new Hashtable();
            }
            table.Add("filter", filter);
            table.Add("ASSIGNEDTOUSER", "'" + loginName.Replace("\\", "/") + "'");
            return ConvertUtil.ToInt32(DataAccess.Instance(GetDBName()).GetObject("TaskLogic_GetMyApprovalCount", table));
        }

        public virtual List<TaskEntity> GetMyRequestList(string loginName, string filter, List<Common.Entity.ParameterEntity> paras, string sort, int skipResults, int maxResults)
        {
            Hashtable table = TypeUtil.GetHashtable(paras);
            if (table == null)
            {
                table = new Hashtable();
            }
            table.Add("filter", filter);
            table.Add("Sort", sort);
            table.Add("SERVERNAME", "'" + GetServerName() + "'");
            table.Add("ASSIGNEDTOUSER", "'" + loginName.Replace("\\", "/") + "'");
            return DataAccess.Instance(GetDBName()).GetList<TaskEntity>("TaskLogic_GetMyRequest", table, skipResults, maxResults);
        }

        public virtual int GetMyRequestCount(string loginName, string filter, List<Common.Entity.ParameterEntity> paras)
        {
            Hashtable table = TypeUtil.GetHashtable(paras);
            if (table == null)
            {
                table = new Hashtable();
            }
            table.Add("filter", filter);
            table.Add("ASSIGNEDTOUSER", "'" + loginName.Replace("\\", "/") + "'");
            return ConvertUtil.ToInt32(DataAccess.Instance(GetDBName()).GetObject("TaskLogic_GetMyRequestCount", table));
        }

        public virtual List<TaskEntity> GetTaskList(string filter, List<Common.Entity.ParameterEntity> paras, string sort, int skipResults, int maxResults)
        {
            return DataAccess.Instance(GetDBName()).ExecuteList<TaskEntity>("SELECT *,'" + GetServerName() + "' as SERVERNAME  FROM TASKS with(nolock) WHERE 1=1 " + filter);
        }

        public virtual int GetTaskListCount(string filter, List<Common.Entity.ParameterEntity> paras)
        {
            return 0;
        }

        public virtual TaskEntity GetTaskEntity(string taskID)
        {
            if (taskID.StartsWith("S"))
            {
                TaskEntity task = DataAccess.Instance(GetDBName()).ExecuteEntity<TaskEntity>("SELECT INITIATEID AS TASKID,PROCESSNAME FROM INITIATE  with(nolock) WHERE INITIATEID=@INITIATEID", taskID);
                return task;
            }
            else
            {
                Hashtable table = new Hashtable();
                table.Add("taskID", taskID);
                table.Add("SERVERNAME", "'" + GetServerName() + "'");
                return DataAccess.Instance(GetDBName()).GetEntity<TaskEntity>("TaskLogic_GetEntity", table);
            }
        }

        public virtual TaskEntity GetTaskEntityByName(string processName, int incident, string loginName)
        {
            return DataAccess.Instance(GetDBName()).ExecuteEntity<TaskEntity>("SELECT * FROM TASKS  with(nolock) WHERE PROCESSNAME=@PROCESSNAME AND INCIDENT=@INCIDENT AND ASSIGNEDTOUSER=@ASSIGENDTOUSER", processName, incident, loginName);
        }

        //调用接口实现
        public virtual string GetTaskUrl(string taskID, string type, string loginName)
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
            int incident=0;
            if (entity == null)  
            {
                 
            }
            else
            {
                processName = entity.PROCESSNAME.Trim();
                stepLabel = entity.STEPLABEL.Trim();
                incident = entity.INCIDENT;
            }

            string page = StepSettingsLogic.GetStepPage(processName, stepLabel);
            string url = "";
           
                url = "http://" + HttpContext.Current.Request.Url.Host + ":"
                    + HttpContext.Current.Request.Url.Port + "/" + page + "?ProcessName=" + processName.Trim() + "&StepName=" + stepLabel.Trim() + "&Incident="
                   + incident + "&TaskID=" + taskID.Trim() + "&UserName=" + HttpContext.Current.Server.UrlEncode(loginName) + "&Type=" + type;
           
            return url;
        }

        public virtual TaskEntity GetInitTaskEntity(string taskID)
        {
            return DataAccess.Instance(GetDBName()).GetEntity<TaskEntity>("TaskLogic_GetStartEntity", taskID);
        }


        //调用接口实现
        public virtual int SubmitTask(TaskEntity entity)
        {
            return 0;
        }



        //调用接口实现
        public virtual void ReturnTask(TaskEntity entity)
        {
            
        }

        //调用接口实现
        public virtual void RejectTask(TaskEntity task)
        {
            AbortIncident(task);
        }

        //调用接口实现
        public virtual void AbortIncident(TaskEntity entity)
        {
             
        }

        public virtual string GetCurrentApprover(string processName, int incident)
        {
            DataTable dt = DataAccess.Instance(GetDBName()).ExecuteDataTable("select STEPLABEL,ASSIGNEDTOUSER from tasks  with(nolock) where processname=@processname and incident=@incident and status=1 ", processName, incident);
            string str = "";
            foreach (DataRow dr in dt.Rows)
            {
                str += "[步骤："+ConvertUtil.ToString(dr[0]) + "]:" + ConvertUtil.ToString(dr[1])+",";
            }
            return str.TrimEnd(',');
        }

        public virtual string GetViewTaskId(string processName, int incident, string loginName)
        {
            object obj = null;
            if (string.IsNullOrEmpty(loginName))
            {
                obj = DataAccess.Instance(GetDBName()).ExecuteScalar("select taskid from tasks  with(nolock) where processname=@processname and incident=@incident ", processName, incident);
            }
            else
            {

                obj = DataAccess.Instance(GetDBName()).ExecuteScalar("select taskid from tasks  with(nolock) where processname=@processname and incident=@incident and assignedtouser=@assignedtouser ", processName, incident, loginName);
            }
            return ConvertUtil.ToString(obj);
        }

        public int GetProcessVersion(string processName)
        {
            object obj = null;
           
                obj = DataAccess.Instance(GetDBName()).ExecuteScalar("select top 1 PROCESSVERSION from INITIATE  with(nolock) where processname=@processname  ", processName);
           
            return ConvertUtil.ToInt32(obj);
        }

        //调用接口实现
        public virtual byte[] GetGraphicalStatus(string processName, int incident)
        {
            return null;
        }

        public virtual int GetStepType(string taskID,string stepID)
        {
            object obj = DataAccess.Instance(GetDBName()).GetObject("TaskLogic_GetStepType", stepID);
            return ConvertUtil.ToInt32(obj);
        }

        //调用接口实现
        public virtual bool AssignTask(string taskId, string toUser)
        {
            return false;
        }

        //调用接口实现
        public virtual bool AssignAllCurrentTasks(string fromUser, string toUser)
        {
            return false;
        }

        //调用接口实现
        public virtual bool AssignAllFutureTasks(string fromUser, string toUser, DateTime toDate)
        {
            return false;
        }

        //调用接口实现
        public virtual List<VarEntity> GetVariableList( string taskID)
        {
          
            return null;
        }


        //调用接口实现
        public virtual bool AssignProcessFutureTasks(string processName, string stepName, string fromUser, string toUser, DateTime toDate)
        {
            return false;
        }

        //调用接口实现
        public virtual bool DeleteTask(TaskEntity task)
        {
            return false;
        }

        ServerEntity _server = new ServerEntity();
        public ServerEntity GetServerEntity()
        {
            return _server;
        }

        public void SetServerEntity(ServerEntity entity)
        {
            _server = entity;
        }

       
        public TaskEntity GetInitTaskEntityByProcessName(string processName)
        {
            TaskEntity task = DataAccess.Instance(GetDBName()).ExecuteEntity<TaskEntity>("SELECT top 1 INITIATEID AS TASKID,PROCESSNAME FROM INITIATE  with(nolock) WHERE PROCESSNAME=@PROCESSNAME Order by PROCESSVERSION desc", processName);
            task.SERVERNAME = GetServerEntity().SERVERNAME;
            return task;
        }

        public virtual void LogoutUser(string sessionId)
        {
        }
       
    }
}