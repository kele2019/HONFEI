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
using Ultimus.UWF.V8.Implementation;
using Ultimus.UWF.Common.Logic;
using Ultimus.UWF.OrgChart.Implementation;
using System.Text;
using System.Data.Common;



namespace Ultimus.UWF.V8.Service
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class MobileService : System.Web.Services.WebService
    {

        ITask _task = new UltimusTask();
        IAuthentication _auth = new UltimusAuthentication();
        IOrg _org = new UltimusOrgChart();
        public MobileService()
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
        public List<TaskEntity> GetMyTaskList(string loginName)
        {
            loginName = loginName.Replace("\\", "/");
            Hashtable table = new Hashtable();
            table.Add("STARTTIME", DateTime.Now.AddDays(-90));
            table.Add("ENDTIME", DateTime.Now.AddDays(90));
            //table.Add("ASSIGNEDTOUSER", "'" + loginName + "'");



            return _task.GetMyTaskList(loginName, "", TypeUtil.GetParameterList(table), " a.StartTime", 0, 999);
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
        public int SubmitTask(string loginName, string taskId, string summary, List<VarEntity> vars)
        {
            loginName = loginName.Replace("\\", "/");
            TaskEntity task = new TaskEntity();
            task.VarList = vars;
            task.ASSIGNEDTOUSER = loginName;
            task.TASKID = taskId;
            task.SUMMARY = summary;
            return _task.SubmitTask(task);
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
                    table.Add(var.Name, sz);
                }
                else
                {
                    table.Add(var.Name, var.Value);
                }
            }
            return table;
        }

        [WebMethod(Description = "退回任务 ")]
        public string ReturnTask(string loginName, string taskId, string reason, string summary, List<VarEntity> vars)
        {

            loginName = loginName.Replace("\\", "/");

            TaskEntity task = new TaskEntity();
            task.VarList = vars;
            task.ASSIGNEDTOUSER = loginName;
            task.TASKID = taskId;
            task.SUMMARY = summary;
            task.REASON = reason;
            _task.ReturnTask(task);
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
        public TaskEntity GetTaskInfo(string loginName, string taskId)
        {
            //return _task.GetTaskInfo(loginName, taskId);
            return null;
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
        public List<VarEntity> GetVariableList(string processName, string taskID)
        {
            //List<VarEntity> byte[]
            //Ser ser = new Ser();
            //return ser.SerializeObject();
            return _task.GetVariableList(taskID);
        }

        [WebMethod(Description = "获取变量")]
        public string[] GetVariableXml(string processName, string taskID, int incidentId)
        {
            string[] xmlString_arr = new string[2];
            string xmlString = "";
            Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
            task.InitializeFromTaskId("", taskID);
            string strError = "";
            task.GetTaskXML(out xmlString, out strError);
            xmlString_arr[0] = xmlString;

            Ultimus.WFServer.Incident inc = new Ultimus.WFServer.Incident();
            bool flag = inc.LoadIncident(processName, incidentId);
            string strProcessSchema = ""; string strError1 = "";
            inc.GetIncidentXML(out strProcessSchema, out strError1);
            xmlString_arr[1] = strProcessSchema;
            return xmlString_arr;

        }

        [WebMethod(Description = "获取变量")]
        public List<string> GetInitialVariable(string processName, string stepName)
        {
            List<string> tables = new List<string>();
            string UltimusIniPath = ConfigurationManager.AppSettings["UltimusIniPath"].ToString();
            string sql = @"SELECT PROCESSVERSION,STEPID FROM [PROCESSSTEPS] where STEPLABEL=N'" + stepName + "' and processname=N'" + processName + "' and PROCESSVERSION in (select PROCESSVERSION from INITIATE where processname=N'" + processName + "')";
            DataTable dt = DataAccess.Instance("UltDB").ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                string PROCESSVERSION = ConvertUtil.ToString(dt.Rows[0]["PROCESSVERSION"]).Trim();
                string STEPID = ConvertUtil.ToString(dt.Rows[0]["STEPID"]).Trim();
                string xmlSchema = UltimusIniPath + processName + "\\" + PROCESSVERSION + "\\" + processName + "Types.xsd";
                string xmlSchema_step = UltimusIniPath + processName + "\\" + PROCESSVERSION + "\\" + STEPID + ".xsd";
                string xmlSchema_inci = UltimusIniPath + processName + "\\" + PROCESSVERSION + "\\" + processName + ".xsd";
                
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(xmlSchema);
                string text = xmldoc.OuterXml.Replace("xs:", "");
                xmldoc = new XmlDocument();
                xmldoc.LoadXml(text);
                XmlNodeList list = xmldoc.SelectNodes("//element");
                foreach (XmlNode node in list)
                {
                    tables.Add(node.Attributes["name"].Value);
                }

                XmlDocument xmldoc_step = new XmlDocument();
                xmldoc_step.Load(xmlSchema_step);
                string text_step = xmldoc_step.OuterXml.Replace("xs:", "");
                xmldoc_step = new XmlDocument();
                xmldoc_step.LoadXml(text_step);
                XmlNodeList list_step = xmldoc_step.SelectNodes("//element");
                foreach (XmlNode node_step in list_step)
                {
                    if (node_step.Attributes["name"].Value != "Global" && node_step.Attributes["name"].Value != "TaskData" && node_step.Attributes["name"].Value.IndexOf("SYS_") != 0) 
                        tables.Add("TaskData_" + node_step.Attributes["name"].Value);
                }

                XmlDocument xmldoc_inci = new XmlDocument();
                xmldoc_inci.Load(xmlSchema_inci);
                string text_inci = xmldoc_inci.OuterXml.Replace("xs:", "");
                xmldoc_inci = new XmlDocument();
                xmldoc_inci.LoadXml(text_inci);
                XmlNodeList list_inci = xmldoc_inci.SelectNodes("//element");
                foreach (XmlNode node_inci in list_inci)
                {
                    if (node_inci.Attributes["name"].Value != "IncidentData" && node_inci.Attributes["name"].Value != "Global" && node_inci.Attributes["name"].Value != "TaskData" && node_inci.Attributes["name"].Value.IndexOf("SYS_") != 0)
                        tables.Add("IncidentData_" + node_inci.Attributes["name"].Value);
                }

            }
            return tables;
        }

        [WebMethod(Description = "获取初始TaskId")]
        public string GetInitTaskID(string processName)
        {
            //return _task.GetInitTaskID(processName);
            return null;
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

        [WebMethod(Description = "获取用户信息")]
        public string CarbonOrBackUp(string txtTaskId, string txtProcessName, string txtUserId, string txtUsers, string txtRemark)
        {
            string[] users = txtUserId.Split(',');
            if (users.Length == 0)
            {
                return "";
            }
            ITask task = ServiceContainer.Instance().GetService<ITask>();
            ServerEntity server = new ServerEntity();
            server.SERVERNAME = "UltimusV8";
            server.DBNAME = "UltDB";
            task.SetServerEntity(server);
            //IOrg org = ServiceContainer.Instance().GetService<IOrg>();
            int i = 0;
            bool flag = false;
            TaskEntity oldTask = task.GetTaskEntity(txtTaskId);
            foreach (string user in users)
            {
                TaskEntity entity = new TaskEntity();
                entity.SERVERNAME = "UltimusV8";
                if (oldTask == null)
                {
                    return "";
                }
                entity.ASSIGNEDTOUSER = oldTask.ASSIGNEDTOUSER;
                entity.SUMMARY = txtRemark;
                entity.TASKID = GetStartTaskId(txtProcessName);

                List<VarEntity> vars = new List<VarEntity>();
                VarEntity var = new VarEntity();
                var.Name = "AssistTaskID";
                var.Value = txtTaskId;
                vars.Add(var);

                var = new VarEntity();
                var.Name = "AssistFormUrl";
                var.Value = ConfigurationManager.AppSettings["RootPath"] + "/Modules/Ultimus.UWF.Workflow/OpenForm.aspx?type=view&TaskId=" + txtTaskId;// task.GetTaskUrl(txtTaskId.Text, "", oldTask.ASSIGNEDTOUSER);
                vars.Add(var);

                var = new VarEntity();
                var.Name = "AssistTaskUser";
                var.Value = oldTask.ASSIGNEDTOUSER;
                vars.Add(var);

                //DatabaseOrgLogic dol = new DatabaseOrgLogic();
                string ASSIGNEDTOUSER = oldTask.ASSIGNEDTOUSER;
                string[] ASSIGNEDTOUSER_ARR = oldTask.ASSIGNEDTOUSER.Replace("\\", "/").Split('/');
                if (ASSIGNEDTOUSER_ARR.Length > 1) { ASSIGNEDTOUSER = ASSIGNEDTOUSER_ARR[1]; }
                List<UserEntity> lue = GetUserInfoBySearchText(ASSIGNEDTOUSER);
                if (lue.Count > 0) { ASSIGNEDTOUSER = lue[0].USERNAME; }
                var = new VarEntity();
                var.Name = "AssistTaskUserFullName";
                var.Value = ASSIGNEDTOUSER;
                vars.Add(var);

                var = new VarEntity();
                var.Name = "ReceiptTaskUser";
                //var.Value = "USER:org=Business Organization,user=" + user;
                var.Value = user;
                vars.Add(var);

                var = new VarEntity();
                var.Name = "ReceiptTaskUserFullName";
                var.Value = txtUsers.Split(',')[i];
                vars.Add(var);

                var = new VarEntity();
                var.Name = "AssistDateTime";
                var.Value = DateTime.Now.ToString();
                vars.Add(var);

                var = new VarEntity();
                var.Name = "AssistProcessName";
                var.Value = oldTask.PROCESSNAME;
                vars.Add(var);

                var = new VarEntity();
                var.Name = "AssistIncident";
                var.Value = oldTask.INCIDENT.ToString();
                vars.Add(var);

                var = new VarEntity();
                var.Name = "AssistStepLabel";
                var.Value = oldTask.STEPLABEL;
                vars.Add(var);

                var = new VarEntity();
                var.Name = "AssistComments";
                var.Value = txtRemark;
                vars.Add(var);

                entity.VarList = vars;
                task.SubmitTask(entity);
                i++;
                flag = true;
            }

            SaveApprovalHistroy(txtTaskId, txtRemark, oldTask.ASSIGNEDTOUSER, txtUsers, txtProcessName);

            if (!flag)
            {
                return "发起流程失败";
            }
            else
            {
                return "发起" + txtProcessName + "成功";
            }
        }

        string GetStartTaskId(string processName)
        {
            object obj = DataAccess.Instance("UltDB").ExecuteScalar("select INITIATEID from INITIATE where PROCESSNAME='" + processName + "' order by PROCESSVERSION desc");
            return ConvertUtil.ToString(obj);
        }

        public void SaveApprovalHistroy(string taskId, string comments,
            string loginName, string txtUsers, string txtProcessName)
        {

            string processName = "";
            int incident = 0;
            string stepLabel = "";
            DataTable dt = DataAccess.Instance("UltDB").ExecuteDataTable("select * from tasks with(nolock) where taskid='" + taskId + "'");
            if (dt.Rows.Count > 0)
            {
                processName = ConvertUtil.ToString(dt.Rows[0]["ProcessName"]);
                incident = ConvertUtil.ToInt32(dt.Rows[0]["Incident"]);
                stepLabel = ConvertUtil.ToString(dt.Rows[0]["StepLabel"]);
            }
            ApprovalHistoryEntity approval = new ApprovalHistoryEntity();
            //IOrg _org = ServiceContainer.Instance().GetService<IOrg>();
            //UserEntity user = _org.GetUserEntity(loginName);

            string approver = loginName;
            string[] approve_ARR = approver.Replace("\\", "/").Split('/');
            if (approve_ARR.Length > 1) { approver = approve_ARR[1]; }
            List<UserEntity> lue = GetUserInfoBySearchText(approver);
            if (lue.Count > 0) { approver = lue[0].USERNAME; }

            approval.Action = "送给【" + txtUsers + "】" + txtProcessName.Replace("流程", "");

            approval.ApproveDate = DateTime.Now;
            approval.Approver = approver;// user.USERNAME;
            approval.Comments = comments;
            approval.Ext01 = approver;// user.USERNAME;
            approval.ProcessName = processName;
            approval.Incident = incident;
            approval.StepName = stepLabel == "" ? "提交" : stepLabel;
            //ApprovalHistoryLogic ahl = new ApprovalHistoryLogic();
            if (txtProcessName.IndexOf("抄送") >= 0)
            {
            }
            else
            {
                AddApprovalHistory(null, approval);
            }
        }

        public bool AddApprovalHistory(IDbTransaction trans, ApprovalHistoryEntity Item)
        {
            DataAccess db = new DataAccess("BizDB2");
            string flag = "@";
            try
            {
                StringBuilder strsql = new StringBuilder();
                strsql.Append("INSERT INTO WF_ApprovalHistory (ProcessName,Incident,StepName,ApproverName,Action,Comments,CreateDate,Status,Ext01,Ext02,Ext03,Ext04,Ext05)");
                strsql.Append(" VALUES ");
                strsql.Append("(" + flag + "ProcessName," + flag + "Incident," + flag + "StepName," + flag + "ApproverName," + flag + "Action," + flag + "Comments," + flag + "CREATEDATE," + flag + "Status," + flag + "Ext01," + flag + "Ext02," + flag + "Ext03," + flag + "Ext04," + flag + "Ext05)");
                //IDbCommand cmd = trans.Connection.CreateCommand();
                DbCommand dbcom = db.CreateCommand(strsql.ToString());
                dbcom.CommandText = strsql.ToString();
                SerialNoLogic sn = new SerialNoLogic();
                //db.AddInParameter(dbcom, "" + flag + "Id", DbType.String, sn.GetMaxNo("WF_ApprovalHistory", "ID"));
                db.AddInParameter(dbcom, "" + flag + "ProcessName", DbType.String, Item.ProcessName);
                db.AddInParameter(dbcom, "" + flag + "Incident", DbType.Int32, Item.Incident);
                db.AddInParameter(dbcom, "" + flag + "StepName", DbType.String, Item.StepName);
                //db.AddInParameter(dbcom, "" + flag + "STEPLEVEL", DbType.String, Item.Level);
                db.AddInParameter(dbcom, "" + flag + "ApproverName", DbType.String, Item.Approver);
                //db.AddInParameter(dbcom, "" + flag + "ApproverFrom", DbType.String, Item.ApproverFrom);
                db.AddInParameter(dbcom, "" + flag + "Action", DbType.String, Item.Action);
                db.AddInParameter(dbcom, "" + flag + "Comments", DbType.String, Item.Comments);
                db.AddInParameter(dbcom, "" + flag + "CREATEDATE", DbType.DateTime, Item.ApproveDate);
                db.AddInParameter(dbcom, "" + flag + "Status", DbType.String, Item.Status);
                db.AddInParameter(dbcom, "" + flag + "Ext01", DbType.String, Item.Ext01);
                db.AddInParameter(dbcom, "" + flag + "Ext02", DbType.String, Item.Ext02);
                db.AddInParameter(dbcom, "" + flag + "Ext03", DbType.String, Item.Ext03);
                db.AddInParameter(dbcom, "" + flag + "Ext04", DbType.String, Item.Ext04);
                db.AddInParameter(dbcom, "" + flag + "Ext05", DbType.String, Item.Ext05);

                foreach (DbParameter param in dbcom.Parameters)
                {
                    if (param.Value == null)
                    {
                        param.Value = DBNull.Value;
                    }
                }
                if (dbcom.Connection.State == ConnectionState.Closed)
                {
                    dbcom.Connection.Open();
                }
                dbcom.ExecuteNonQuery();

                dbcom.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
