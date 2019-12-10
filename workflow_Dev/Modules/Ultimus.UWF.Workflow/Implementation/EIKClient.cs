using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using MyLib;
using System.Web;
using System.Data;
using Ultimus.UWF.Workflow.Interface;
using Ultimus.UWF.Common.Interface;
using Ultimus.UWF.Workflow.EIKServiceRef;


namespace Ultimus.UWF.Workflow.Implementation
{
    public class EIKClient : DatabaseTask,IClientService
    {
        string _serviceUrl = "";
        public string ServiceUrl
        {
            get
            {
                return _serviceUrl;
            }
            set
            {
                _serviceUrl = value;
            }
        }

        EIKService GetService()
        {
            EIKService srv = new EIKService();
            srv.Url = ServiceUrl;
            //srv.CookieContainer = new System.Net.CookieContainer();
            srv.SetServerEntity(GetServerEntityRef( base.GetServerEntity()));
            return srv;
        }

        EIKServiceRef.ParameterEntity[] GetParas(List<Common.Entity.ParameterEntity> paras)
        {
            EIKServiceRef.ParameterEntity[] ps = new ParameterEntity[paras.Count];
            for(int i=0;i<ps.Length;i++)
            {
                EIKServiceRef.ParameterEntity ety = new ParameterEntity();
                ety.Name=paras[i].Name;
                ety.Value=paras[i].Value;

                ps[i] = ety;
            }
            return ps;
        }

        EIKServiceRef.ServerEntity GetServerEntityRef(Entity.ServerEntity ety)
        {
            EIKServiceRef.ServerEntity aa = new EIKServiceRef.ServerEntity();
            aa.CLIENTASSEMBLY = ety.CLIENTASSEMBLY;
            aa.DBNAME = ety.DBNAME;
            aa.DESCRIPTION = ety.DESCRIPTION;
            aa.DOMAINNAME = ety.DOMAINNAME;
            aa.PRODUCT = ety.PRODUCT;
            aa.SERVERNAME = ety.SERVERNAME;
            aa.VERSION = ety.VERSION;
            aa.WEBSERVICEURL = ety.WEBSERVICEURL;
            return aa;
        }


        Entity.TaskEntity GetTaskEntity(EIKServiceRef.TaskEntity ety)
        {
            Entity.TaskEntity task = new Entity.TaskEntity();
            task.APLICANT = ety.APLICANT;
            task.ASSIGNEDTOUSER = ety.ASSIGNEDTOUSER;
            task.DEPARTMENT = ety.DEPARTMENT;
            task.ENDTIME = ety.ENDTIME;
            task.FORMURL = ety.FORMURL;
            task.HELPURL = ety.HELPURL;
            task.INCIDENT = ety.INCIDENT;
            task.INITIATOR = ety.INITIATOR;
            task.OVERDUETIME = ety.OVERDUETIME;
            task.PROCESSNAME = ety.PROCESSNAME;
            task.PROCESSSTATUS = ety.PROCESSSTATUS;
            task.REASON = ety.REASON;
            task.SERVERNAME = ety.SERVERNAME;
            task.STARTTIME = ety.STARTTIME;
            task.STATUS = ety.STATUS;
            task.STEPID = ety.STEPID;
            task.STEPLABEL = ety.STEPLABEL;
            task.SUBSTATUS = ety.SUBSTATUS;
            task.SUMMARY = ety.SUMMARY;
            task.SYNC = ety.SYNC;
            task.TASKID = ety.TASKID;
            task.TASKUSER = ety.TASKUSER;
            task.ERRORMESSAGE = ety.ERRORMESSAGE;
            task.COMMENTS = ety.COMMENTS;
            task.REASON = ety.REASON;
            task.TYPE = ety.TYPE;
            task.SERVERNAME = ety.SERVERNAME;
            task.SERVERTASKID = ety.SERVERTASKID;
            task.VarList = new List<Entity.VarEntity>();
            foreach (VarEntity v in ety.VarList)
            {
                Entity.VarEntity var = new Entity.VarEntity();
                var.Name = v.Name;
                var.Value = v.Value;
                task.VarList.Add(var);
            }

            return task;
        }

        EIKServiceRef.TaskEntity GetTaskEntityRef(Entity.TaskEntity ety)
        {
            EIKServiceRef.TaskEntity task = new EIKServiceRef.TaskEntity();
            task.APLICANT = ety.APLICANT;
            task.ASSIGNEDTOUSER = ety.ASSIGNEDTOUSER;
            task.DEPARTMENT = ety.DEPARTMENT;
            task.ENDTIME = ety.ENDTIME;
            task.FORMURL = ety.FORMURL;
            task.HELPURL = ety.HELPURL;
            task.INCIDENT = ety.INCIDENT;
            task.INITIATOR = ety.INITIATOR;
            task.OVERDUETIME = ety.OVERDUETIME;
            task.PROCESSNAME = ety.PROCESSNAME;
            task.PROCESSSTATUS = ety.PROCESSSTATUS;
            task.REASON = ety.REASON;
            task.SERVERNAME = ety.SERVERNAME;
            task.STARTTIME = ety.STARTTIME;
            task.STATUS = ety.STATUS;
            task.STEPID = ety.STEPID;
            task.STEPLABEL = ety.STEPLABEL;
            task.SUBSTATUS = ety.SUBSTATUS;
            task.SUMMARY = ety.SUMMARY;
            task.SYNC = ety.SYNC;
            task.TASKID = ety.TASKID;
            task.TASKUSER = ety.TASKUSER;
            task.ERRORMESSAGE = ety.ERRORMESSAGE;
            task.COMMENTS = ety.COMMENTS;
            task.REASON = ety.REASON;
            task.TYPE = ety.TYPE;
            task.SERVERNAME = ety.SERVERNAME;
            task.SERVERTASKID = ety.SERVERTASKID;
            task.VarList = new VarEntity[ety.VarList.Count];
            for (int i = 0; i < ety.VarList.Count;i++ )
            {
                EIKServiceRef.VarEntity var = new EIKServiceRef.VarEntity();
                Entity.VarEntity v = ety.VarList[i];
                var.Name = v.Name;
                var.Value = v.Value;
                task.VarList[i] = var;
            }

            return task;
        }

        public override List<Entity.TaskEntity> GetInitTaskList(string loginName, string filter)
        {
            List<Entity.TaskEntity> list = new List<Entity.TaskEntity>();
            TaskEntity[] tt = GetService().GetInitTaskList(loginName,filter);
            foreach (TaskEntity t in tt)
            {
                list.Add(GetTaskEntity(t));
            }
            return list;
        }

        public override List<Entity.TaskEntity> GetDraftTaskList(string loginName, string filter)
        {
            List<Entity.TaskEntity> list = new List<Entity.TaskEntity>();
            TaskEntity[] tt = GetService().GetDraftTaskList(loginName,filter);
            foreach (TaskEntity t in tt)
            {
                list.Add(GetTaskEntity(t));
            }
            return list;
        }

        public override string GetTaskUrl(string taskID, string type, string loginName)
        {
            return GetService().GetTaskUrl(taskID, type, loginName);
        }

        public override int SubmitTask(Entity.TaskEntity task)
        {
            return GetService().SubmitTask(GetTaskEntityRef(task));
            
        }

        public override void ReturnTask(Entity.TaskEntity task)
        {
            GetService().ReturnTask(GetTaskEntityRef(task));
        }

        public override void RejectTask(Entity.TaskEntity task)
        {
            GetService().RejectTask(GetTaskEntityRef(task));
        }

        public override void AbortIncident(Entity.TaskEntity task)
        {
            GetService().AbortIncident(GetTaskEntityRef(task));
        }

        public override byte[] GetGraphicalStatus(string processName, int incident)
        {
            return GetService().GetGraphicalStatus(processName, incident);
        }

        public override bool AssignTask(string taskId, string toUser)
        {
            return GetService().AssignTask(taskId, toUser);
            
        }

        public override bool AssignAllCurrentTasks(string fromUser, string toUser)
        {
            return GetService().AssignAllCurrentTasks(fromUser, toUser);
            
        }

        public override bool AssignAllFutureTasks(string fromUser, string toUser, DateTime toDate)
        {
            return GetService().AssignAllFutureTasks(fromUser, toUser,toDate);
            
        }

        public override bool AssignProcessFutureTasks(string processName, string stepName, string fromUser, string toUser, DateTime toDate)
        {
            return GetService().AssignProcessFutureTasks(processName, stepName, fromUser, toUser, toDate);

        }

        public override bool DeleteTask(Entity.TaskEntity task)
        {
            return GetService().DeleteTask(GetTaskEntityRef(task));
        }

        public override void LogoutUser(string sessionId)
        {
            GetService().LogoutUser(sessionId);
        }
    }
}