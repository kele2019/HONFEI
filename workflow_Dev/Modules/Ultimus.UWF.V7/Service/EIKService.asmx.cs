using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Ultimus.UWF.Workflow.Interface;
using MyLib;

namespace Ultimus.UWF.V7.Service
{
    /// <summary>
    /// TaskService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class EIKService : System.Web.Services.WebService, ITask
    {
        ITask _task = ServiceContainer.Instance().GetService<ITask>();
        [WebMethod]
        public List<Workflow.Entity.TaskEntity> GetInitTaskList(string loginName, string filter)
        {
            return _task.GetInitTaskList(loginName,filter);
        }

        [WebMethod]
        public List<Workflow.Entity.TaskEntity> GetDraftTaskList(string loginName, string filter)
        {
            return _task.GetDraftTaskList(loginName,filter);
        }

        [WebMethod]
        public List<Workflow.Entity.TaskEntity> GetMyTaskList(string loginName, string filter, List<Common.Entity.ParameterEntity> paras, string sort, int skipResults, int maxResults)
        {
            return _task.GetMyTaskList(loginName, filter, paras, sort, skipResults, maxResults);
        }

        [WebMethod]
        public int GetMyTaskCount(string loginName, string filter, List<Common.Entity.ParameterEntity> paras)
        {
            return _task.GetMyTaskCount(loginName, filter, paras);
        }

        [WebMethod]
        public List<Workflow.Entity.TaskEntity> GetMyApprovalList(string loginName, string filter, List<Common.Entity.ParameterEntity> paras, string sort, int skipResults, int maxResults)
        {
            return _task.GetMyApprovalList(loginName, filter, paras, sort, skipResults, maxResults);
        }

        [WebMethod]
        public int GetMyApprovalCount(string loginName, string filter, List<Common.Entity.ParameterEntity> paras)
        {
            return _task.GetMyApprovalCount(loginName, filter, paras);
        }

        [WebMethod]
        public List<Workflow.Entity.TaskEntity> GetMyRequestList(string loginName, string filter, List<Common.Entity.ParameterEntity> paras, string sort, int skipResults, int maxResults)
        {
            return _task.GetMyRequestList(loginName, filter, paras, sort, skipResults, maxResults);
        }

        [WebMethod]
        public int GetMyRequestCount(string loginName, string filter, List<Common.Entity.ParameterEntity> paras)
        {
            return _task.GetMyRequestCount(loginName, filter, paras);
        }

        [WebMethod]
        public List<Workflow.Entity.TaskEntity> GetTaskList(string filter, List<Common.Entity.ParameterEntity> paras, string sort, int skipResults, int maxResults)
        {
            return _task.GetTaskList(filter, paras, sort, skipResults, maxResults);
        }

        [WebMethod]
        public int GetTaskListCount(string filter, List<Common.Entity.ParameterEntity> paras)
        {
            return _task.GetTaskListCount(filter, paras);
        }

        [WebMethod]
        public Workflow.Entity.TaskEntity GetTaskEntity(string taskID)
        {
            return _task.GetTaskEntity(taskID);
        }

        [WebMethod]
        public Workflow.Entity.TaskEntity GetTaskEntityByName(string processName, int incident, string loginName)
        {
            return _task.GetTaskEntityByName(processName, incident, loginName);
        }

        [WebMethod]
        public string GetTaskUrl(string taskID, string type, string loginName)
        {
            return _task.GetTaskUrl(taskID, type, loginName);
        }

        [WebMethod]
        public int SubmitTask(Workflow.Entity.TaskEntity task)
        {
            return _task.SubmitTask(task);
        }

        [WebMethod]
        public void ReturnTask(Workflow.Entity.TaskEntity task)
        {
            _task.ReturnTask(task);
        }

        [WebMethod]
        public void RejectTask(Workflow.Entity.TaskEntity task)
        {
            _task.RejectTask(task);
        }

        [WebMethod]
        public void AbortIncident(Workflow.Entity.TaskEntity task)
        {
            _task.AbortIncident(task);
        }

        [WebMethod]
        public string GetCurrentApprover(string processName, int incident)
        {
            return _task.GetCurrentApprover(processName, incident);
        }

        [WebMethod]
        public string GetViewTaskId(string processName, int incident, string loginName)
        {
            return _task.GetViewTaskId(processName, incident, loginName);
        }

        [WebMethod]
        public byte[] GetGraphicalStatus(string processName, int incident)
        {
            return _task.GetGraphicalStatus(processName, incident);
        }

        [WebMethod]
        public int GetStepType(string taskID, string stepID)
        {
            return _task.GetStepType(taskID, stepID);
        }

        [WebMethod]
        public bool AssignTask(string taskId, string toUser)
        {
            return _task.AssignTask(taskId, toUser);
        }

        [WebMethod]
        public bool AssignAllCurrentTasks(string fromUser, string toUser)
        {
            return _task.AssignAllCurrentTasks(fromUser, toUser);
        }

        [WebMethod]
        public bool AssignAllFutureTasks(string fromUser, string toUser, DateTime toDate)
        {
            return _task.AssignAllFutureTasks(fromUser, toUser, toDate);
        }

        [WebMethod]
        public List<Workflow.Entity.VarEntity> GetVariableList(string taskID)
        {
            return _task.GetVariableList(taskID);
        }

        [WebMethod]
        public bool AssignProcessFutureTasks(string processName, string stepName, string fromUser, string toUser, DateTime toDate)
        {
            return _task.AssignProcessFutureTasks(processName, stepName, fromUser, toUser, toDate);

        }

        [WebMethod]
        public Workflow.Entity.TaskEntity GetInitTaskEntity(string taskID)
        {
            return _task.GetInitTaskEntity(taskID);
        }

        [WebMethod]
        public bool DeleteTask(Workflow.Entity.TaskEntity task)
        {
            return _task.DeleteTask(task);
        }

        [WebMethod]
        public Workflow.Entity.TaskEntity GetInitTaskEntityByProcessName(string processName)
        {
            return _task.GetInitTaskEntityByProcessName(processName);
        }

        [WebMethod]
        public Workflow.Entity.ServerEntity GetServerEntity()
        {
            return _task.GetServerEntity();
        }

        [WebMethod]
        public void SetServerEntity(Workflow.Entity.ServerEntity entity)
        {
            _task.SetServerEntity(entity);
        }




        [WebMethod]
        public void LogoutUser(string sessionId)
        {
            _task.LogoutUser(sessionId);
        }
    }
}
