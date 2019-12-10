using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Collections;
using MyLib;
using System.Xml;
using Ultimus.UWF.Workflow.Entity;
using Ultimus.UWF.Workflow.Interface;
using Ultimus.UWF.Security.Interface;
using Ultimus.UWF.OrgChart.Entity;
using Ultimus.UWF.OrgChart.Interface;
using Ultimus.UWF.Workflow.Logic;
using System.Data; 
using Ultimus.UWF.Common.Logic;


namespace Ultimus.UWF.Workflow.Service
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class EIKService : System.Web.Services.WebService
    {

        ITask _task = ServiceContainer.Instance().GetService<ITask>();
        IAuthentication _auth = ServiceContainer.Instance().GetService<IAuthentication>();
        IOrg _org = ServiceContainer.Instance().GetService<IOrg>();
        public EIKService()
        {
        }

        [WebMethod(Description = "获取有权限发起的流程")]
        public List<ProcessEntity> GetInitProcessList(string loginName)
        {
            List<TaskEntity> tasks= _task.GetInitTaskList(loginName,"");
            List<ProcessEntity> pros = new List<ProcessEntity>();
            foreach (TaskEntity task in tasks)
            {
                ProcessEntity pro = new ProcessEntity();
                pro.PROCESSNAME = task.PROCESSNAME;
                pro.PROCESSENNAME = task.PROCESSNAME;
                pro.INITIATEID = task.TASKID;
                pros.Add(pro);
            }
            return pros;
        }
        

        [WebMethod(Description = "待办任务")]
        public List<TaskEntity> GetMyTaskList(string loginName,string ProcessName)
        {
            loginName = loginName.Replace("\\", "/");
            Hashtable table = new Hashtable();
            table.Add("STARTTIME", DateTime.Now.AddDays(-90));
            table.Add("ENDTIME", DateTime.Now.AddDays(90));
            //table.Add("ASSIGNEDTOUSER", "'" + loginName + "'");
            string filter = "";
            //if (!ProcessName.ToUpper().Contains("ALL"))
            if (ProcessName!="")
            {
                filter = " and  a.PROCESSNAME='"+ProcessName+"' ";
            }
            filter += " and a.STEPLABEL<>'Applicant' ";
			object UserAccount = DataAccess.Instance("BizDB").ExecuteScalar(" select top (1) LOGINNAME from ORG_USER where USERCODE='" + loginName + "'");
			if (UserAccount != null)
				loginName = UserAccount.ToString().Replace("\\", "/");
            List<TaskEntity> listMode=_task.GetMyTaskList(loginName, filter, TypeUtil.GetParameterList(table), " a.StartTime desc", 0, 999);
			if(listMode.Count>11)
			listMode=listMode.GetRange(0, 8);
			foreach (TaskEntity item in listMode)
			{
				item.SUMMARY =item.PROCESSNAME+" "+item.RequestorDate;
				item.STARTTIME = item.OVERDUETIME;
			}
			return listMode;
        }
		 
        [WebMethod(Description = "我的申请")]
        public List<TaskEntity> GetMyRequestList(string loginName)
        {
            loginName = loginName.Replace("\\", "/");
            Hashtable table = new Hashtable();
            table.Add("STARTTIME", DateTime.Now.AddDays(-90));
            table.Add("ENDTIME", DateTime.Now.AddDays(90));
            //table.Add("ASSIGNEDTOUSER", "'" + loginName + "'");

            return _task.GetMyRequestList(loginName, "", TypeUtil.GetParameterList(table), " a.StartTime", 0, 999);
        }

        [WebMethod(Description = "已办任务")]
        public List<TaskEntity> GetMyApprovalList(string loginName)
        {
            loginName = loginName.Replace("\\", "/");
            Hashtable table = new Hashtable();
            table.Add("STARTTIME", DateTime.Now.AddDays(-90));
            table.Add("ENDTIME", DateTime.Now.AddDays(90));
            //table.Add("ASSIGNEDTOUSER", "'" + loginName + "'");

            return _task.GetMyApprovalList(loginName, "", TypeUtil.GetParameterList(table), " a.StartTime", 0, 999);
        }

        [WebMethod(Description = "提交任务")]
        public int SubmitTask(string loginName, string taskId, string summary, DataTable vars, string comments, string ActionType)
        {
          
           
            loginName = loginName.Replace("\\", "/");
            TaskEntity task = new TaskEntity();
            task.VarList = GetVarList(vars);
            task.ASSIGNEDTOUSER = loginName;
            task.TASKID = taskId;
            task.SUMMARY = summary;
            int Incident=_task.SubmitTask(task);
            #region
            SaveApproval(ActionType, "MYTASK", taskId, comments, loginName,"",Incident,"");
            return Incident;
            #endregion
        }

        [WebMethod(Description = "创建任务")]
        public int CreateTask(string loginName, string taskId, string summary, DataTable vars, string comments, string ActionType, string processName, string StepName)
        {
            loginName = loginName.Replace("\\", "/");
            TaskEntity task = new TaskEntity();
            task.VarList = GetVarList(vars);
            task.ASSIGNEDTOUSER = loginName;
            task.TASKID = taskId;
            task.SUMMARY = summary;
            int Incident = _task.SubmitTask(task);
            #region 保存审批记录
            SaveApproval(ActionType, "", taskId, comments, loginName, processName, Incident, StepName);
            #endregion
            return Incident;
        }


        List<VarEntity> GetVarList(DataTable table)
        {
            List<VarEntity> list = new List<VarEntity>();
            if (table != null)
            {
                foreach (DataRow ety in table.Rows)
                {
                    VarEntity p = new VarEntity();
                    p.Name = Convert.ToString(ety["Key"].ToString());
                    p.Value = Convert.ToString(ety["Value"].ToString());
                    list.Add(p);
                }
            }
            return list;
        }

        public void SaveApproval(string actionType, string type, string taskId, string comments,
            string loginName, string processName, int incident, string stepLabel)
        {
            //string processName = "";
            //int incident = 0;
           // string stepLabel = "";
            DataTable dt = DataAccess.Instance("UltDB").ExecuteDataTable("select * from tasks with(nolock) where taskid='" + taskId + "'");
            if (dt.Rows.Count > 0)
            {
                processName = ConvertUtil.ToString(dt.Rows[0]["ProcessName"]);
                incident = ConvertUtil.ToInt32(dt.Rows[0]["Incident"]);
                stepLabel = ConvertUtil.ToString(dt.Rows[0]["StepLabel"]);
            }
            ApprovalHistoryEntity approval = new ApprovalHistoryEntity();
            IOrg _org = ServiceContainer.Instance().GetService<IOrg>();
            UserEntity user = _org.GetUserEntity(loginName);
            //if (actionType == 0 && type == "NEWREQUEST" || type == "DRAFT")
            //{
            //    approval.Action = "提交";
            //}
            //if (actionType == 0 && type == "MYTASK")
            //{
            //    approval.Action = "Approve";
            //}
            //if (actionType == 1)
            //{
            //    approval.Action = "Reject";
            //}
            approval.Action = actionType;
            approval.ApproveDate = DateTime.Now;
            approval.Approver = user.USERNAME;
            approval.Comments = comments;
            approval.Ext01 = loginName.Replace('/','\\');
            approval.ProcessName = processName;
            approval.Incident = incident;
            approval.StepName = stepLabel == "" ? "Unknown" : stepLabel;
            ApprovalHistoryLogic ahl = new ApprovalHistoryLogic();
            ahl.AddApprovalHistory(null, approval);
        }
        [WebMethod(Description = "保存审批记录")]
        public void SaveApprovalHistroy(int actionType, string type, string taskId, string comments,
            string loginName)
        {

            string processName = "";
            int incident=0;
            string stepLabel = "";
            DataTable dt= DataAccess.Instance("UltDB").ExecuteDataTable("select * from tasks with(nolock) where taskid='"+taskId+"'");
            if (dt.Rows.Count > 0)
            {
                processName = ConvertUtil.ToString(dt.Rows[0]["ProcessName"]);
                incident = ConvertUtil.ToInt32(dt.Rows[0]["Incident"]);
                stepLabel = ConvertUtil.ToString(dt.Rows[0]["StepLabel"]);
            }
            ApprovalHistoryEntity approval = new ApprovalHistoryEntity();
            IOrg _org = ServiceContainer.Instance().GetService<IOrg>();
            UserEntity user = _org.GetUserEntity(loginName);
            if (actionType == 0 && type == "NEWREQUEST" || type == "DRAFT")
            {
                approval.Action = "提交";
            }
            if (actionType == 0 && type == "MYTASK")
            {
                approval.Action = "同意";
            }
            if (actionType == 1)
            {
                approval.Action = "退回";
            }
            approval.ApproveDate = DateTime.Now;
            approval.Approver = user.USERNAME;
            approval.Comments = comments;
            approval.Ext01 = user.USERNAME;
            approval.ProcessName = processName;
            approval.Incident = incident;
            approval.StepName = stepLabel == "" ? "提交" : stepLabel;
            ApprovalHistoryLogic ahl = new ApprovalHistoryLogic();
            ahl.AddApprovalHistory(null, approval);
        }

        Hashtable GetTable(List<VarEntity> vars)
        {
            Hashtable table = new Hashtable();
            if (vars == null)
            {
                return table;
            }
            foreach (VarEntity var in vars)
            {
                //处理数组
                string[] sz = var.Value.Split('|');
                if (sz.Length > 0)
                {
                    table.Add(var.Name,sz);
                }
                else
                {
                    table.Add(var.Name, var.Value);
                }
            }
            return table;
        }

        [WebMethod(Description = "退回任务 ")]
        public string ReturnTask(string loginName, string taskId, string reason, string summary, DataTable vars, string comments,string ActionType)
        {

            loginName = loginName.Replace("\\", "/");

            TaskEntity task = new TaskEntity();
            task.VarList = GetVarList(vars); ;
            task.ASSIGNEDTOUSER = loginName;
            task.TASKID = taskId;
            task.SUMMARY = summary;
            task.REASON = reason;
            _task.ReturnTask(task);
            SaveApproval(ActionType, "MYTASK", taskId, comments, loginName,"",0,"");
            return task.ERRORMESSAGE;
        }

        [WebMethod(Description = "拒绝任务 ")]
        public string RejectTask(string loginName, string taskId, string reason, List<VarEntity> vars)
        {

            loginName = loginName.Replace("\\", "/");

            TaskEntity task = new TaskEntity();
            task.VarList = vars;
            task.ASSIGNEDTOUSER = loginName;
            task.TASKID = taskId; 
            task.REASON = reason;
            _task.RejectTask(task);
            return task.ERRORMESSAGE;
        }

        [WebMethod(Description = "获取任务信息")]
        public TaskEntity GetTaskInfo(string taskId)
        {
           return _task.GetTaskEntity( taskId);
        }

        /// <summary>
        /// 获得流程监控图
        /// </summary>
        /// <param name="ProcessName">流程名称</param>
        /// <param name="Incident">实例编号</param>
        /// <returns>byte[]</returns>
        [WebMethod(Description = "获取流程监控图")]
        public byte[] GetGraphicalStatus(string processName, int incident)
        {
            return _task.GetGraphicalStatus(processName, incident);
        }

        [WebMethod(Description = "获取变量")]
        public List<VarEntity> GetVariableList( string taskID)
        {
            //List<VarEntity> byte[]
            Ser ser = new Ser();
            //return ser.SerializeObject();
            return _task.GetVariableList(taskID);
        }

        [WebMethod(Description = "获取变量")]
        public string GetVariableXml(string processName, string taskID)
        {
            return "";
        }

        [WebMethod(Description = "获取变量")]
        public List<string> GetInitialVariable(string processName)
        {
            return new List<string>();// _task.GetInitialVariable(processName);
        }

        [WebMethod(Description = "获取初始TaskId")]
        public string GetInitTaskID(string processName)
        {
            //return _task.GetInitTaskID(processName);
           // TaskEntity taskEntiy=_task.GetInitTaskEntityByProcessName(processName);
           //return taskEntiy.TASKID;
            Implementation.MultiServerTask tasksEntiy = new Implementation.MultiServerTask();
            TaskEntity taskEntiynew=tasksEntiy.GetInitTaskEntityByProcessName(processName);
            return taskEntiynew.TASKID;
            //return null;
        }

        [WebMethod(Description = "获取域名")]
        public List<string> GetDomains()
        {
            return _auth.GetDomains();
        }

        [WebMethod(Description = "检验用户")]
        public bool CheckUser(string loginName, string password)
        {
            try
            {
                return _auth.CheckUser(loginName, password);
            }
            catch(Exception e)
            {
                LogUtil.Error(e);
                throw e;
            }
        }

        [WebMethod(Description = "获取用户信息")]
        public UserEntity GetUserEntity(string loginName)
        {
            return _org.GetUserEntity(loginName);
        }

        [WebMethod(Description = "获取用户信息")]
        public List<UserEntity> GetUserInfoBySearchText(string searchText)
        {
            return _org.GetUserInfoBySearchText(searchText);
        }
        [WebMethod(Description = "获取询问人列表")]
        public List<UserEntity> GetInquireList(string ProcessName,string IncidentID)
        {
            Implementation.BusinessData DAO = new Implementation.BusinessData();
            return DAO.GetInquireList(ProcessName,IncidentID);
        }
    }
}
