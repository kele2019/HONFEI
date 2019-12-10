using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using MyLib;
using System.Web;
using System.Data;
using Ultimus.UWF.Workflow.Entity;
using Ultimus.UWF.Workflow.Logic;
using Ultimus.UWF.Workflow.Interface;
using System.Xml;

namespace Ultimus.UWF.Workflow.Implementation
{
    public class MultiServerTask : ITask
    {

        public List<TaskEntity> GetInitTaskList(string loginName, string filter)
        {
            List<ITask> tasks = ServerLogic.GetAllTask();
            List<TaskEntity> list = new List<TaskEntity>();
            foreach (ITask task in tasks)
            {
                loginName = ServerLogic.GetLoginName(task.GetServerEntity().SERVERNAME, loginName);
                list.AddRange(task.GetInitTaskList(loginName,filter));
            }
            return list;
        }

        public List<TaskEntity> GetMyTaskList(string loginName, string filter, List<Common.Entity.ParameterEntity> paras, string sort, int skipResults, int maxResults)
        {
            List<ITask> tasks = ServerLogic.GetAllTask();
            List<TaskEntity> list = new List<TaskEntity>();
            foreach (ITask task in tasks)
            {
                loginName = ServerLogic.GetLoginName(task.GetServerEntity().SERVERNAME, loginName);
                list.AddRange(task.GetMyTaskList(loginName,filter,paras,sort,0,1000));
            }
            list = GetPagedList(list,sort, skipResults, maxResults);
            return list;
        }

        List<TaskEntity> GetPagedList(List<TaskEntity> list,string sort, int skipResults, int maxResults)
        {
            if (sort.Equals("a.STARTTIME"))
            {
                ListUtil.Sort<TaskEntity, DateTime>(list, "STARTTIME");
            }
            else
            {
                ListUtil.Sort<TaskEntity, DateTime>(list, "STARTTIME",ListUtil.SortingDirection.Descending);

            }
            if (maxResults > list.Count-skipResults)
            {
                maxResults = list.Count - skipResults;
            }
            return list.GetRange(skipResults, maxResults);
        }

        public int GetMyTaskCount(string loginName, string filter, List<Common.Entity.ParameterEntity> paras)
        {
            int count = 0;
            List<ITask> tasks = ServerLogic.GetAllTask();
            foreach (ITask task in tasks)
            {
                loginName = ServerLogic.GetLoginName(task.GetServerEntity().SERVERNAME, loginName);
                count+=task.GetMyTaskCount(loginName, filter, paras);
            }
            return count;
        }

        public List<TaskEntity> GetMyApprovalList(string loginName, string filter, List<Common.Entity.ParameterEntity> paras, string sort, int skipResults, int maxResults)
        {
            List<ITask> tasks = ServerLogic.GetAllTask();
            List<TaskEntity> list = new List<TaskEntity>();
            foreach (ITask task in tasks)
            {
                loginName = ServerLogic.GetLoginName(task.GetServerEntity().SERVERNAME, loginName);
                list.AddRange(task.GetMyApprovalList(loginName, filter, paras, sort, 0, 1000));
            }
            list = GetPagedList(list,sort, skipResults, maxResults);
            return list;
        }

        public int GetMyApprovalCount(string loginName, string filter, List<Common.Entity.ParameterEntity> paras)
        {
            int count = 0;
            List<ITask> tasks = ServerLogic.GetAllTask();
            foreach (ITask task in tasks)
            {
                loginName = ServerLogic.GetLoginName(task.GetServerEntity().SERVERNAME, loginName);
                count += task.GetMyApprovalCount(loginName, filter, paras);
            }
            return count;
        }

        public List<TaskEntity> GetMyRequestList(string loginName, string filter, List<Common.Entity.ParameterEntity> paras, string sort, int skipResults, int maxResults)
        {
            List<ITask> tasks = ServerLogic.GetAllTask();
            List<TaskEntity> list = new List<TaskEntity>();
            foreach (ITask task in tasks)
            {
                loginName = ServerLogic.GetLoginName(task.GetServerEntity().SERVERNAME, loginName);
                list.AddRange(task.GetMyRequestList(loginName, filter, paras, sort, 0, 1000));
            }
            list = GetPagedList(list,sort, skipResults, maxResults);
            return list;
        }

        public int GetMyRequestCount(string loginName, string filter, List<Common.Entity.ParameterEntity> paras)
        {
            int count = 0;
            List<ITask> tasks = ServerLogic.GetAllTask();
            foreach (ITask task in tasks)
            {
                loginName = ServerLogic.GetLoginName(task.GetServerEntity().SERVERNAME, loginName);
                count += task.GetMyRequestCount(loginName, filter, paras);
            }
            return count;
        }

        public List<TaskEntity> GetTaskList(string filter, List<Common.Entity.ParameterEntity> paras, string sort, int skipResults, int maxResults)
        {
            List<ITask> tasks = ServerLogic.GetAllTask();
            List<TaskEntity> list = new List<TaskEntity>();
            foreach (ITask task in tasks)
            {
                list.AddRange(task.GetTaskList( filter, paras, sort, 0, 1000));
            }
            list = GetPagedList(list,sort, skipResults, maxResults);
            return list;
        }

        public int GetTaskListCount(string filter, List<Common.Entity.ParameterEntity> paras)
        {
            int count = 0;
            List<ITask> tasks = ServerLogic.GetAllTask();
            foreach (ITask task in tasks)
            {
                count += task.GetTaskListCount( filter, paras);
            }
            return count;
        }

        public TaskEntity GetTaskEntity(string taskID)
        {
            ITask cls = ServerLogic.GetTaskClassByServer(GetServerEntity().SERVERNAME);
            return cls.GetTaskEntity(taskID);
        }

        public TaskEntity GetTaskEntityByName(string processName, int incident, string loginName)
        {
            ITask cls = ServerLogic.GetTaskClassByServer(GetServerEntity().SERVERNAME);
            return cls.GetTaskEntityByName(processName, incident, loginName);
        }

        public string GetTaskUrl(string taskID, string type, string loginName)
        {
            ITask cls = ServerLogic.GetTaskClassByServer(GetServerEntity().SERVERNAME);
            return cls.GetTaskUrl(taskID,type,loginName);
        }

        public int SubmitTask(TaskEntity task)
        {
            ITask cls = ServerLogic.GetTaskClassByServer(GetServerEntity().SERVERNAME);
            return cls.SubmitTask(task);
        }

        public void ReturnTask(TaskEntity task)
        {
            ITask cls = ServerLogic.GetTaskClassByServer(GetServerEntity().SERVERNAME);
            cls.ReturnTask(task);
        }

        public void RejectTask(TaskEntity task)
        {
            ITask cls = ServerLogic.GetTaskClassByServer(GetServerEntity().SERVERNAME);
            cls.RejectTask(task);
        }

        public void AbortIncident(TaskEntity task)
        {
            ITask cls = ServerLogic.GetTaskClassByServer(GetServerEntity().SERVERNAME);
            cls.AbortIncident(task);
        }

        public string GetCurrentApprover(string processName, int incident)
        {
            ITask cls = ServerLogic.GetTaskClassByServer(GetServerEntity().SERVERNAME);
            return cls.GetCurrentApprover(processName, incident);
        }

        public string GetViewTaskId(string processName, int incident, string loginName)
        {
            ITask cls = ServerLogic.GetTaskClassByServer(GetServerEntity().SERVERNAME);
            loginName = ServerLogic.GetLoginName(GetServerEntity().SERVERNAME, loginName);
            return cls.GetViewTaskId(processName, incident, loginName);
        }

        public byte[] GetGraphicalStatus(string processName, int incident)
        {
            ITask cls = ServerLogic.GetTaskClassByServer(GetServerEntity().SERVERNAME);
            return cls.GetGraphicalStatus(processName, incident);
        }

        public int GetStepType(string taskID,string stepID)
        {
            ITask cls = ServerLogic.GetTaskClassByServer(GetServerEntity().SERVERNAME);
            return cls.GetStepType(taskID,stepID);
        }

        public bool AssignTask(string taskId, string toUser)
        {
            List<ITask> tasks = ServerLogic.GetAllTask();
            foreach (ITask task in tasks)
            {
                toUser = ServerLogic.GetLoginName(task.GetServerEntity().SERVERNAME, toUser);
                task.AssignTask(taskId, toUser);
            }
            return true;
        }

        public bool AssignAllCurrentTasks(string fromUser, string toUser)
        {
            List<ITask> tasks = ServerLogic.GetAllTask();
            foreach (ITask task in tasks)
            {
                fromUser = ServerLogic.GetLoginName(task.GetServerEntity().SERVERNAME, fromUser);
                toUser = ServerLogic.GetLoginName(task.GetServerEntity().SERVERNAME, toUser);

                task.AssignAllCurrentTasks(fromUser, toUser);
            }
            return true;
        }

        public bool AssignAllFutureTasks(string fromUser, string toUser, DateTime toDate)
        {
            List<ITask> tasks = ServerLogic.GetAllTask();
            foreach (ITask task in tasks)
            {
                fromUser = ServerLogic.GetLoginName(task.GetServerEntity().SERVERNAME, fromUser);
                toUser = ServerLogic.GetLoginName(task.GetServerEntity().SERVERNAME, toUser);
                task.AssignAllFutureTasks(fromUser, toUser,toDate);
            }
            return true;
        }

        public bool AssignProcessFutureTasks(string processName, string stepName, string fromUser, string toUser, DateTime toDate)
        {
            List<ITask> tasks = ServerLogic.GetAllTask();
            foreach (ITask task in tasks)
            {
                fromUser = ServerLogic.GetLoginName(task.GetServerEntity().SERVERNAME, fromUser);
                toUser = ServerLogic.GetLoginName(task.GetServerEntity().SERVERNAME, toUser);
                task.AssignProcessFutureTasks(processName,stepName, fromUser, toUser, toDate);
            }
            return true;
        }

        public List<VarEntity> GetVariableList( string taskID)
        {
            ITask cls = ServerLogic.GetTaskClassByServer(GetServerEntity().SERVERNAME);
            return cls.GetVariableList(taskID);
        }

        public List<TaskEntity> GetDraftTaskList(string loginName, string filter)
        {
            List<ITask> tasks = ServerLogic.GetAllTask();
            List<TaskEntity> list = new List<TaskEntity>();
            foreach (ITask task in tasks)
            {
                loginName = ServerLogic.GetLoginName(task.GetServerEntity().SERVERNAME, loginName);
                list.AddRange(task.GetDraftTaskList(loginName,filter));
            }
            return list;
        }


        public bool DeleteTask(TaskEntity entity)
        {
            ITask task = ServerLogic.GetTaskClassByServer(GetServerEntity().SERVERNAME);
            return task.DeleteTask(entity);
        }

        public void LogoutUser(string sessionId)
        {
            ITask task = ServerLogic.GetTaskClassByServer(GetServerEntity().SERVERNAME);
            task.LogoutUser(sessionId);
        }


        public TaskEntity GetInitTaskEntityByProcessName(string processName)
        {
             ITask task = ServerLogic.GetTaskClassByServer(GetServerEntity().SERVERNAME);
            //return task.GetInitTaskEntity(processName);
            return task.GetInitTaskEntityByProcessName(processName);
        }

        ServerEntity _server = new ServerEntity();
        public ServerEntity GetServerEntity()
        {
            string SERVERNAME = HttpContext.Current.Request["ServerName"];
            if (!string.IsNullOrEmpty(SERVERNAME))
            {
                if (SERVERNAME.Equals("(local)"))
                {
                    _server = new ServerEntity();
                    _server.SERVERNAME = "(local)";
                    _server.DBNAME = "UltDB";
                }
                else
                {
                    _server = ServerLogic.GetServerEntity(SERVERNAME);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(_server.SERVERNAME))
                {
                    _server = new ServerEntity();
                    _server.SERVERNAME = "UltimusV8";
                    _server.DBNAME = "UltDB";
                }
            }
            return _server;
        }

        public void SetServerEntity(ServerEntity entity)
        {
            _server = entity;
        }


        public TaskEntity GetInitTaskEntity(string taskID)
        {
            ITask task = ServerLogic.GetTaskClassByServer(GetServerEntity().SERVERNAME);
            return task.GetInitTaskEntity(taskID);
        }
    }
}