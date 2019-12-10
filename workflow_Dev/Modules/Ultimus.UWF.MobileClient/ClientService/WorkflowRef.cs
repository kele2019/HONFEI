using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientService.WorkflowSrv;
using MyLib;
using System.Data;
using System.Data.Common;
using System.Web;

namespace ClientService
{
    public class WorkflowRef
    {
        Ser ser = new Ser();
        WorkflowSrv.MobileService eik = new MobileService();
        //public VarEntity[] GetVariableList(string processName,string taskId)
        //{
        //    //return eik.GetVariableList(processName, "S0421158c540122b61e67257aa2da7b");
        //    return null;
        //}

        public VarEntity[] GetVariableList(string processName, string taskId)
        {
            //List<ClientService.Entity.VarEntity> ve = ser.DeserializeObject(eik.GetVariableList(processName, taskId)) as List<ClientService.Entity.VarEntity>;
            //VarEntity[] ve = 
            return eik.GetVariableList(processName, taskId);
        }

        public string[] GetVariableXml(string processName, string taskId, int incidentId)
        {
            return eik.GetVariableXml(processName, taskId, incidentId);
        }

        public string[] GetInitialVariable(string processName, string stepName)
        {
            return eik.GetInitialVariable(processName, stepName);
        }

        public string[] GetDomainList()
        {
            return eik.GetDomains();
        }

        public bool CheckUser(string loginName, string password)
        {
            return eik.CheckUser(loginName, password);
        }

        public UserEntity GetUserEntity(string loginName)
        {
            return eik.GetUserEntity(loginName);
        }

        public byte[] GetGraphicalStatus(string processName, int incident)
        {
            return eik.GetGraphicalStatus(processName, incident);
        }

        public ProcessEntity[] GetInitProcessList(string loginName)
        {
            return eik.GetInitProcessList(loginName);
        }

        public TaskEntity[] GetMyTaskList(string loginName)
        {
            return eik.GetMyTaskList(loginName);
        }

        public TaskEntity[] GetMyRequestList(string loginName)
        {
            return eik.GetMyRequestList(loginName);
        }

        public TaskEntity[] GetMyApprovalList(string loginName)
        {
            return eik.GetMyApprovalList(loginName);
        }

        public int SubmitTask(string loginName, string taskId, string summary, VarEntity[] vars,DataSet ds)
        {
            int i = 0;
            string sql = @"select COUNT(1)
              from [TASKS] where PROCESSNAME='协办流程'
              and CHARINDEX('>" + taskId + "</AssistTaskID>',CONVERT(nvarchar(max),SCHEMADATA),1) !=0 and [status]=1";
            int result = Convert.ToInt32(DataAccess.Instance("UltDB").ExecuteScalar(sql).ToString());
            if (result == 0)
            {
                DataTable varDt = ds.Tables[0];
                //VarEntity[] varsNew = new VarEntity[varDt.Columns.Count];
                List<VarEntity> varsNew = new List<VarEntity>();
                for (int j = 0; j < varDt.Columns.Count; j++)
                {
                    VarEntity n = new VarEntity();
                    n.Name = varDt.Columns[j].ColumnName.Replace("TaskData_", "TaskData.").Replace("IncidentData_", "IncidentData.");
                    n.Value = varDt.Rows[0][varDt.Columns[j].ColumnName].ToString();
                    //varsNew[j] = n;
                    varsNew.Add(n);
                    //if (varDt.Rows[0][varDt.Columns[j].ColumnName].ToString().IndexOf("USER:org=") != -1)
                    //{
                    //    string[] tempStringArr = varDt.Rows[0][varDt.Columns[j].ColumnName].ToString().Split('|');
                    //    foreach (string temp in tempStringArr) 
                    //    {
                    //        VarEntity n = new VarEntity();
                    //        n.Name = varDt.Columns[j].ColumnName.Replace("TaskData_", "TaskData.").Replace("IncidentData_", "IncidentData.");
                    //        n.Value = temp;
                    //        //varsNew[j] = n;
                    //        varsNew.Add(n);
                    //    }
                    //}
                    //else
                    //{
                    //    VarEntity n = new VarEntity();
                    //    n.Name = varDt.Columns[j].ColumnName.Replace("TaskData_", "TaskData.").Replace("IncidentData_", "IncidentData.");
                    //    n.Value = varDt.Rows[0][varDt.Columns[j].ColumnName].ToString();
                    //    //varsNew[j] = n;
                    //    varsNew.Add(n);
                    //}
                }

                string tasksql = @"select STEPLABEL,TASKUSER from tasks (nolock) where TASKID='" + taskId.Trim() + "'";
                DataTable taskDt = DataAccess.Instance("UltDB").ExecuteDataTable(tasksql);

                if (taskDt.Rows.Count > 0)
                {
                    //20150718 固定增加4个步骤变量
                    VarEntity n1 = new VarEntity();
                    n1.Name = "TaskData.TaskID";
                    n1.Value = taskId;
                    varsNew.Add(n1);

                    VarEntity n2 = new VarEntity();
                    n2.Name = "TaskData.StepLabel";
                    n2.Value = taskDt.Rows[0]["STEPLABEL"].ToString().Trim();
                    varsNew.Add(n2);

                    VarEntity n3 = new VarEntity();
                    n3.Name = "TaskData.TaskUser";
                    n3.Value = taskDt.Rows[0]["TASKUSER"].ToString().Trim();
                    varsNew.Add(n3);

                    string userAccount = "";
                    string[] user_Arry = taskDt.Rows[0]["TASKUSER"].ToString().Trim().Split('/');
                    if (user_Arry.Length > 1) { userAccount = user_Arry[1]; }
                    UserEntity[] uee = eik.GetUserInfoBySearchText(userAccount);
                    string name = "";
                    if (uee.Length > 0) 
                    {
                        name = uee[0].USERNAME.Trim();
                    }
                    VarEntity n4 = new VarEntity();
                    n4.Name = "TaskData.TaskUserFullName";
                    n4.Value = name;
                    varsNew.Add(n4);
                }

                i = eik.SubmitTask(loginName, taskId, summary, varsNew.ToArray());
            }
            else {
                i = -999;
            }
            //DataTable dt = ds.Tables["ApprovalRemark"];
            //string comments = "";
            //if (dt.Rows.Count > 0)
            //{
            //    comments = ConvertUtil.ToString(dt.Rows[0][0]);
            //}
            //公用
            //eik.SaveApprovalHistroy(0,"MYTASK",taskId,comments,loginName);
            //标准表单
            //SaveApprovalHistroy(0, "MYTASK", taskId, comments, loginName, ds);
            return i;
        }

        void SaveApprovalHistroy(int actionType, string type, string taskId, string comments,
            string loginName, DataSet ds)
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
            UserEntity user = GetUserEntity(loginName);
            string idear="同意";
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                try
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.ColumnName.IndexOf("不同意") >= 0 && Convert.ToInt32(dt.Rows[0][col]) == 1)
                        {
                            idear = "不同意";
                        }
                        if (col.ColumnName.IndexOf("拒绝") >= 0 && Convert.ToInt32(dt.Rows[0][col]) == 1)
                        {
                            idear = "不同意";
                        }
                        if (col.ColumnName.IndexOf("退回") >= 0 && Convert.ToInt32(dt.Rows[0][col]) == 1)
                        {
                            idear = "退回";
                        }
                    }
                }
                catch
                {
                }
            }

            DataAccess.Instance("StdDB").ExecuteNonQuery("insert into t_signinfo(guid,process,incident,stepname,username,userfullname,department,opinion,signdate,remarks) values(newid(),@process,@incident,@stepname,@username,@userfullname,@department,@opinion,getdate(),@remarks)",
                processName,incident,stepLabel,loginName,user.USERNAME,user.DEPARTMENT,idear,comments);
            
        }

        public int ReturnTask(string loginName, string taskId, string reason, string summary, VarEntity[] vars, DataSet ds)
        {
            //string i = eik.ReturnTask(loginName, taskId, reason, summary, vars);

            //DataTable dt = ds.Tables["ApprovalRemark"];
            //string comments = "";
            //if (dt.Rows.Count > 0)
            //{
            //    comments = ConvertUtil.ToString(dt.Rows[0][0]);
            //}
            //eik.SaveApprovalHistroy(1, "MYTASK", taskId, comments, loginName);
            //return i;

            int i = 0;
            string sql = @"select COUNT(1)
              from [TASKS] where PROCESSNAME='协办流程'
              and CHARINDEX('>" + taskId + "</AssistTaskID>',CONVERT(nvarchar(max),SCHEMADATA),1) !=0 and [status]=1";
            int result = Convert.ToInt32(DataAccess.Instance("UltDB").ExecuteScalar(sql).ToString());
            if (result == 0)
            {
                DataTable varDt = ds.Tables[0];
                VarEntity[] varsNew = new VarEntity[varDt.Columns.Count];
                for (int j = 0; j < varDt.Columns.Count; j++)
                {
                    VarEntity n = new VarEntity();
                    n.Name = varDt.Columns[j].ColumnName.Replace("TaskData_", "TaskData.").Replace("IncidentData_", "IncidentData.");
                    n.Value = varDt.Rows[0][varDt.Columns[j].ColumnName].ToString();
                    varsNew[j] = n;
                }
                eik.ReturnTask(loginName, taskId, reason, summary, varsNew);
                i = 1;
            }
            else
            {
                i = -999;
            }
            return i;
        }

        public string RejectTask(string loginName, string taskId, string reason, VarEntity[] vars, DataSet ds)
        {
            return eik.RejectTask(loginName, taskId, reason, vars);
        }

        private DataAccess db = new DataAccess("BizDB");
        string flag = "@";
        public DataTable GetApprovalHistoryByProc(string ProcessaName, int Incident)
        {
            try
            {
                //公用
                //StringBuilder strsql = new StringBuilder();
                //strsql.Append("select t.*,t.ApproverName as UserName from WF_ApprovalHistory t where ProcessName=" + flag + "ProcessName and Incident=" + flag + "Incident order by CREATEDATE");
                //DbCommand dbcom = db.CreateCommand(strsql.ToString());
                //db.AddInParameter(dbcom, "" + flag + "ProcessName", DbType.String, ProcessaName);
                //db.AddInParameter(dbcom, "" + flag + "Incident", DbType.Int32, Incident);
                //return db.ExecuteDataTable(dbcom);

                //标准表单
                //DataAccess db = new DataAccess("StdDB");
                //StringBuilder strsql = new StringBuilder();
                //strsql.Append("select StepName,UserFullName as UserName,signdate as CreateDate,opinion as Action,Remarks from t_signinfo t where Process=" + flag + "ProcessName and Incident=" + flag + "Incident order by signdate");
                //DbCommand dbcom = db.CreateCommand(strsql.ToString());
                //db.AddInParameter(dbcom, "" + flag + "ProcessName", DbType.String, ProcessaName);
                //db.AddInParameter(dbcom, "" + flag + "Incident", DbType.Int32, Incident);
                //DataTable dt= db.ExecuteDataTable(dbcom);

                //DataTable taskdt= DataAccess.Instance("UltDB").ExecuteDataTable("select StepLabel as StepName,AssignedToUser as loginName from tasks with(nolock) where processname=@processname and incident=@incident and status=1",ProcessaName,Incident);
                //foreach (DataRow row in taskdt.Rows)
                //{
                //    DataRow dr = dt.NewRow();
                //    dr["StepName"] = row["StepName"];
                //    UserEntity user= GetUserEntity(Convert.ToString(row["loginName"]));
                //    if (user != null)
                //    {
                //        dr["UserName"] = user.USERNAME;
                //    }
                //    dr["CreateDate"] = "**********";
                //    dt.Rows.Add(dr);
                //}

                string sql = @"SELECT StepName,APPROVERNAME AS UserName,CreateDate,[Action],Comments FROM [WF_APPROVALHISTORY] WHERE PROCESSNAME='" + ProcessaName + "' AND INCIDENT='" + Incident + "' ORDER BY CREATEDATE"; ;
                DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public UserEntity[] GetUserInfoBySearchText(string searchText)
        {
            return eik.GetUserInfoBySearchText(searchText);
        }

        public string CarbonOrBackUp(string txtTaskId, string txtProcessName, string txtUserId, string txtUsers, string txtRemark)
        {
            //return eik.CarbonOrBackUp( txtTaskId, txtProcessName, txtUserId, txtUsers, txtRemark);
            return "";
        }

    }
}
