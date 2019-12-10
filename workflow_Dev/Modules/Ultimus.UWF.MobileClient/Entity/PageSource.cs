using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataBaseLibrary;
using Ultimus;
using Ultimus.OC;
using Ultimus.WFServer;
using EntityLibrary;
using MyLib;
using System.Data.SqlClient;

namespace DALLibrary
{
    public class PageSource
    {
        //private DbHelperSQLP db = new DbHelperSQLP();
        private MobileClient_Process ProcessDAL = new MobileClient_Process();
        private MobileClient_Step StepDAL = new MobileClient_Step();
        private MobileClient_StepControl StepControlDAL = new MobileClient_StepControl();
        private MobileClient_Control ControlDAL = new MobileClient_Control();

        public DataSet GetPageControlSource(string ProcessName, string StepName)
        {
            try
            {
                DataSet ds = new DataSet();

                EntityLibrary.MobileClient_Process ProcessModel = ProcessDAL.GetModel("ProcessName='" + ProcessName + "'")[0];

                List<Variable> Variables = new List<Variable>();
                Variable[] vars = GetVariable(ProcessModel.ProcessName, 0);
                Variables.AddRange(vars);

                List<EntityLibrary.MobileClient_Step> StepModel = StepDAL.GetModel("FK_ID='" + ProcessModel.ID + "' and StepName='" + StepName + "'");

                foreach (EntityLibrary.MobileClient_Step item in StepModel)
                {
                    List<EntityLibrary.MobileClient_StepControl> StepControls = StepControlDAL.GetModel("FK_ID='" + item.ID + "'");

                    if (StepControls.Count > 0)
                    {
                        int TableIndex = 0;
                        foreach (EntityLibrary.MobileClient_StepControl Control in StepControls)
                        {
                            if (Control.SourceType == "DataBase")
                            {
                                if (Control.SourceConnectionString != null)
                                {
                                    //db.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[Control.SourceConnectionString].ConnectionString;
                                    StringBuilder strsql = new StringBuilder();
                                    strsql.AppendLine("select ");
                                    strsql.AppendLine(Control.SourceColumnName);
                                    strsql.AppendLine("from");
                                    strsql.AppendLine(Control.SourceTableName);
                                    strsql.AppendLine("where 1=1");
                                    strsql.AppendLine(Control.SourceWhere);
                                    strsql.AppendLine("order by");
                                    strsql.AppendLine(Control.SourceColumnName);
                                    IDataReader reader = DbHelperSQL.ExecuteReader(strsql.ToString());
                                    DataTable dt = new DataTable();
                                    dt.Load(reader);
                                    ds.Tables.Add(dt);
                                    ds.Tables[ds.Tables.Count - 1].TableName = Control.ColumnName;
                                    TableIndex++;
                                    reader.Close();
                                }
                            }
                            else if (Control.SourceType == "ElectronicForm")
                            {
                                foreach (Variable v in Variables)
                                {
                                    if (v.strVariableName.Trim() == Control.SourceVariableName.Trim())
                                    {
                                        DataTable dt = new DataTable();
                                        if (v.objVariableValue != null)
                                            dt.LoadDataRow(v.objVariableValue, false);
                                        ds.Tables.Add(dt);
                                        ds.Tables[ds.Tables.Count - 1].TableName = Control.ColumnName;
                                        TableIndex++;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                PublicClass.WriteLogOfTxt(ex.Message);
                return null;
            }
        }

        public Variable[] GetVariable(string ProcessName, int nIncident)
        {
            try
            {
                Variable[] vars = null;
                string _error = "";
                if (nIncident > 0)
                {
                    Incident inc = new Incident();
                    inc.LoadIncident(ProcessName, nIncident);
                    string[] names = inc.GetVariableNames();
                    vars = new Variable[names.Length];
                    int index = 0;
                    foreach (string name in names)
                    {
                        Variable var = new Variable();
                        object objs = null;
                        inc.GetVariableValue(name, out objs, out _error);
                        var.strVariableName = name;
                        if (objs != null && objs.GetType().IsArray)
                        {
                            var.objVariableValue = (object[])objs;
                        }
                        else
                        {
                            var.objVariableValue = new object[] { objs };
                        }
                        vars.SetValue(var, index);
                        index++;
                    }
                }
                else
                {
                    string taskID = GetInitTaskID(ProcessName);
                    if (!string.IsNullOrEmpty(taskID))
                    {
                        Ultimus.WFServer.Task t = new Ultimus.WFServer.Task();
                        t.InitializeFromInitiateTaskId("", taskID);
                        string error = "";
                        t.GetAllTaskVariables(out vars, out error);
                    }
                }
                return vars;
            }
            catch (Exception ex)
            {
                PublicClass.WriteLogOfTxt(ex.Message);
                throw ex;
            }
        }

        public string GetInitTaskID(string processName)
        {
            return DataAccess.Instance("UltDB").ExecuteScalar("select top 1 INITIATEID from INITIATE  where  ProcessName='" + processName + "' order by PROCESSVERSION desc").ToString();
        }

        public DataSet GetPageDestSource(string ProcessName, int nIncident, string StepName)
        {
            try
            {
                DataSet ds = new DataSet();

                EntityLibrary.MobileClient_Process ProcessModel = ProcessDAL.GetModel("ProcessName='" + ProcessName.Trim() + "'")[0];

                List<Variable> Variables = new List<Variable>();
                Variable[] vars;
                if (nIncident > 0)
                {
                    vars = GetVariable(ProcessModel.ProcessName, nIncident);
                }
                else
                {
                    vars = GetVariable(ProcessModel.ProcessName, 0);
                }
                Variables.AddRange(vars);

                EntityLibrary.MobileClient_Step StepModel = StepDAL.GetModel("FK_ID='" + ProcessModel.ID + "' and StepName='" + StepName + "'")[0];

                DataTable MasterTable = new DataTable();

                List<EntityLibrary.MobileClient_StepControl> StepControls = StepControlDAL.GetModel("FK_ID='" + StepModel.ID + "'");

                if (StepControls.Count > 0)
                {
                    List<EntityLibrary.MobileClient_StepControl> Master = StepControls.FindAll(delegate(EntityLibrary.MobileClient_StepControl model)
                    {
                        if (string.IsNullOrEmpty(model.IsMasterTable))
                        {
                            return false;
                        }
                        if (model.IsMasterTable.Trim().ToLower() != "true")
                        {
                            return false;
                        }
                        return true;
                    });

                    List<EntityLibrary.MobileClient_StepControl> Sublist = StepControls.FindAll(delegate(EntityLibrary.MobileClient_StepControl model)
                    {
                        if (string.IsNullOrEmpty(model.IsSublist))
                        {
                            return false;
                        }
                        if (model.IsSublist.Trim().ToLower() != "true")
                        {
                            return false;
                        }
                        return true;
                    });

                    //DbHelperSQLP db = new DbHelperSQLP();
                    Dictionary<string, string> MasterTableNameList = new Dictionary<string, string>();

                    foreach (EntityLibrary.MobileClient_StepControl Control in Master)
                    {
                        MasterTable.Columns.Add(Control.ColumnName);
                        if (Control.DestType != null && Control.DestType.IndexOf("DataBase") >= 0)
                        {
                            if (!MasterTableNameList.ContainsKey(Control.DestTableName))
                            {
                                string ColumnNames = "";
                                foreach (EntityLibrary.MobileClient_StepControl model in Master)
                                {
                                    if (model.DestTableName == Control.DestTableName)
                                    {
                                        ColumnNames += model.DestColumnName + ",";
                                    }
                                }
                                MasterTableNameList.Add(Control.DestTableName, null);
                                //db.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[Control.DestConnectionString].ConnectionString.Trim();
                                if (Control.ControlID == 10 || Control.ControlID == 11 || Control.ControlID == 13 || Control.ControlID == 15)
                                {
                                    continue;
                                }
                                DataTable dt = null;
                                try
                                {
                                    dt = DbHelperSQL.Query("select " + ColumnNames.TrimEnd(',') + ",FORMID from " + Control.DestTableName + " where ProcessName='" + ProcessName + "' and Incident='" + nIncident + "'").Tables[0];
                                }
                                catch
                                {
                                    dt = DbHelperSQL.Query("select " + ColumnNames.TrimEnd(',') + " from " + Control.DestTableName + " where ProcessName='" + ProcessName + "' and Incident='" + nIncident + "'").Tables[0];
                                }
                                if (dt.Rows.Count == 0)
                                {
                                    dt.Rows.Add(dt.NewRow());
                                }
                                ds.Tables.Add(dt.Copy());
                                string tablename = Control.DestTableName;
                                if (string.IsNullOrEmpty(tablename))
                                {
                                    tablename = "MasterTable";
                                }
                                ds.Tables[ds.Tables.Count - 1].TableName = tablename;
                            }
                        }
                        else if (Control.DestType != null && Control.DestType.IndexOf("ElectronicForm") >= 0)
                        {
                            foreach (Variable v in Variables)
                            {
                                if (v.strVariableName == Control.DestVariableName)
                                {
                                    if (MasterTable.Rows.Count < 1)
                                    {
                                        MasterTable.Rows.Add(MasterTable.NewRow());
                                    }

                                    MasterTable.Rows[0][Control.ColumnName] = v.objVariableValue == null ? "" : v.objVariableValue[0] != null ? v.objVariableValue[0].ToString().Trim() : "";
                                    break;
                                }
                            }

                            ds.Tables.Add(MasterTable);
                            ds.Tables[ds.Tables.Count - 1].TableName = "MasterTable";
                        }
                    }
                    

                    Incident inc = new Incident();
                    inc.LoadIncident(ProcessName, nIncident);
                    DataTable SublistTable = new DataTable();

                    Dictionary<string, string> SublistTableNameList = new Dictionary<string, string>();

                    Sublist.Sort(delegate(EntityLibrary.MobileClient_StepControl p1, EntityLibrary.MobileClient_StepControl p2) { return Comparer<decimal>.Default.Compare(p1.OrderBy, p2.OrderBy); });

                    foreach (EntityLibrary.MobileClient_StepControl Control in Sublist)
                    {
                        if (Control.DestType != null && Control.DestType.IndexOf("DataBase") >= 0)
                        {
                            if (!SublistTableNameList.ContainsKey(Control.DestTableName))
                            {
                                string ColumnNames = "";
                                foreach (EntityLibrary.MobileClient_StepControl model in Sublist)
                                {
                                    if (model.DestTableName == Control.DestTableName)
                                    {
                                        bool result = true;
                                        if (!string.IsNullOrEmpty(model.IsShow))
                                        {
                                            StringBuilder sb = new StringBuilder();
                                            string[] ss = model.IsShow.Split(',');
                                            for (int i = 0; i < ss.Length; i++)
                                            {
                                                if (ss[i].IndexOf("||") > 0)
                                                {
                                                    string[] filters = ss[i].Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                                                    result = false;
                                                    for (int j = 0; j < filters.Length; j++)
                                                    {
                                                        string[] mystr = filters[j].Split('.');
                                                        if (ds.Tables[ds.Tables.Count - 1].Rows.Count > 0 && ds.Tables[ds.Tables.Count - 1].Rows[0][mystr[0]] == null)
                                                        {
                                                            continue;
                                                        }
                                                        else if (ds.Tables[ds.Tables.Count - 1].Rows.Count > 0 && ds.Tables[ds.Tables.Count - 1].Rows[0][mystr[0]].ToString().Trim() == mystr[1].Trim())
                                                        {
                                                            result = true;
                                                            break;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    string[] s = ss[i].Split('.');
                                                    if (s.Length > 1)
                                                    {
                                                        if (ds.Tables[ds.Tables.Count - 1].Rows.Count > 0 && ds.Tables[ds.Tables.Count - 1].Rows[0][s[0]] == null)
                                                        {
                                                            result = false;
                                                            break;
                                                        }
                                                        else if (ds.Tables[ds.Tables.Count - 1].Rows.Count > 0 && ds.Tables[ds.Tables.Count - 1].Rows[0][s[0]].ToString().Trim() != s[1].Trim())
                                                        {
                                                            result = false;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        if (result)
                                        {
                                            ColumnNames += model.DestColumnName + " as '" + model.ColumnName + "',";
                                        }
                                    }
                                }
                                SublistTableNameList.Add(Control.DestTableName, null);
                                
                                DataTable dt = new DataTable(); //取明细表数据
                                string str = "";
                                try
                                {
                                    //根据流程名和实例号关联
                                    str = "select " + ColumnNames.TrimEnd(',') + " from " + Control.DestTableName + " where ProcessName='" + ProcessName + "' and Incident='" + nIncident + "'";
                                    dt = DbHelperSQL.Query(str).Tables[0];
                                    if (dt.Rows.Count < 1)
                                    {
                                        try
                                        {
                                            //根据FORMID关联
                                            str = "select " + ColumnNames.TrimEnd(',') + " from " + Control.DestTableName + " where FORMID='" + ds.Tables[0].Rows[0]["FORMID"].ToString() + "'";
                                            dt = DbHelperSQL.Query(str).Tables[0];
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                catch
                                {
                                    try
                                    {
                                        //根据FORMID关联
                                        str = "select " + ColumnNames.TrimEnd(',') + " from " + Control.DestTableName + " where FORMID='"+ds.Tables[0].Rows[0]["FORMID"].ToString()+"'";
                                        dt = DbHelperSQL.Query(str).Tables[0];
                                    }
                                    catch
                                    {
                                    }
                                }
                                ds.Tables.Add(dt.Copy());
                                ds.Tables[ds.Tables.Count - 1].TableName = Control.DestTableName;
                            }
                        }
                        else if (Control.DestType != null && Control.DestType.IndexOf("ElectronicForm") >= 0)
                        {
                            string VariableName = Control.DestVariableName;
                            object[] objs;
                            string _ErrorMessage = "";
                            if (inc.GetVariableValues(VariableName, out objs, out _ErrorMessage))
                            {
                                SublistTable.Columns.Add(VariableName);
                                for (int i = 0; i < objs.Length; i++)
                                {
                                    if (objs[i] != null)
                                    {
                                        if(i==SublistTable.Rows.Count)
                                            SublistTable.Rows.Add(SublistTable.NewRow());
                                        SublistTable.Rows[i][VariableName] = objs[i];
                                    }
                                }
                            }
                            else
                            {
                                throw new Exception(_ErrorMessage);
                            }
                        }
                    }
                    ds.Tables.Add(SublistTable);
                    ds.Tables[ds.Tables.Count - 1].TableName = "ElectronicForm";
                }
                return ds;
            }
            catch (Exception ex)
            {
                PublicClass.WriteLogOfTxt(ex.Message);
                return null;
            }
        }
        public int GetStepId(string ProcessName, string stepName)
        {
            try
            {
                object obj = null;
                if (string.IsNullOrEmpty(stepName))
                {
                    obj = DataAccess.Instance("UltDB").ExecuteScalar(string.Format("SELECT STEPLABEL " +
                      "FROM PROCESSSTEPS " +
                      "where PROCESSNAME='{0}' and STEPTYPE=2" +
                      "order by PROCESSVERSION desc ", ProcessName));
                    if (obj != null && obj != DBNull.Value)
                    {
                        stepName = obj.ToString().Trim();
                    }
                }

                obj = DbHelperSQL.GetSingle(string.Format("SELECT a.ID " +
                   "FROM MobileClient_Step a " +
                   "inner join MobileClient_Process b on a.FK_ID=b.ID " +
                   "where b.ProcessName='{0}' and a.StepName='{1}'", ProcessName, stepName));

                return Convert.ToInt32(obj);
            }
            catch (Exception ex)
            {
                PublicClass.WriteLogOfTxt(ex.Message);
                return 0;
            }
        }

        public int SavePageDestSource(int type,string ProcessName, int nIncident, string StepName, string Summary, string UserAccount, List<VariableInfo> variable, DataSet ds)
        {
            try
            {
                List<Variable> var = new List<Variable>();

                EntityLibrary.MobileClient_Process ProcessModel = ProcessDAL.GetModel("ProcessName='" + ProcessName.Trim() + "'")[0];

                List<Variable> Variables = new List<Variable>();
                Variable[] vars = GetVariable(ProcessModel.ProcessName, nIncident);
                Variables.AddRange(vars);

                EntityLibrary.MobileClient_Step StepModel = StepDAL.GetModel("FK_ID='" + ProcessModel.ID + "' and StepName='" + StepName + "'")[0];

                List<EntityLibrary.MobileClient_StepControl> StepControls = StepControlDAL.GetModel("FK_ID='" + StepModel.ID + "'");

                List<EntityLibrary.MobileClient_Control> ConList = ControlDAL.GetModel("ControlEName='ApprovalRemark'");

                List<EntityLibrary.MobileClient_Control> ActionList = ControlDAL.GetModel("ControlEName in ('ApproveButton','ReturnButton')");

                EntityLibrary.MobileClient_Control AppControl = new EntityLibrary.MobileClient_Control();

                if (ConList.Count > 0)
                {
                    AppControl = ConList[0];
                }

                //DbHelperSQLP db = new DbHelperSQLP();

                Tasklist list = new Tasklist();
                TasklistFilter filter = new TasklistFilter();
                filter.strProcessNameFilter = ProcessName;
                filter.strStepLabelFilter = StepName;
                filter.nIncidentNo = nIncident;
                filter.strArrUserName = new string[] { UserAccount };
                list.LoadFilteredTasks(filter);
                Task task = list.GetNextTask();
                int NewIncident = 0; 
                string SendFormError = "";
                string ApprovalRemark = null;
                string Actions = null;

                if (nIncident > 0)
                {
                    Incident incident = new Incident();
                    incident.LoadIncident(task.strProcessName, task.nIncidentNo);
                    if (string.IsNullOrEmpty(Summary))
                    {
                        Summary = incident.strIncidentSummary;
                    }
                    if (type == 1) //退回
                    {
                        task.Return(var.ToArray(), "", Summary, out SendFormError);
                        NewIncident = nIncident;
                        Actions = "Return-tel";
                    }
                    else //提交
                    {
                        task.Send(var.ToArray(), "", Summary, ref NewIncident, out SendFormError);
                        Actions = "Approve-tel";
                    }
                }
                else
                {
                    task.SendFrom(UserAccount, var.ToArray(), "", Summary, ref NewIncident, out SendFormError);
                }
                if (NewIncident <= 0)
                {
                    return 0;
                }

                if (ds.Tables.Count > 0)
                {
                    #region
                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        DataTable dt = ds.Tables[i];
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            string ColumnNames = dt.Columns[j].ColumnName;
                            List<EntityLibrary.MobileClient_StepControl> sc = StepControlDAL.GetModel("FK_ID='" + StepModel.ID + "' and ColumnName='" + ColumnNames + "'");

                            EntityLibrary.MobileClient_StepControl scl = new EntityLibrary.MobileClient_StepControl();

                            if (sc.Count > 0)
                            {
                                scl = sc[0];
                            }

                            foreach (EntityLibrary.MobileClient_Control col in ActionList)
                            {
                                if (col.ID == scl.ControlID)
                                {
                                    if (col.ControlEName == "ApproveButton")
                                    {
                                        if (dt.Rows[0][scl.ColumnName].ToString() == "1")
                                        {
                                            Actions = "同意";
                                        }
                                    }
                                    else if (col.ControlEName == "ReturnButton")
                                    {
                                        if (dt.Rows[0][scl.ColumnName].ToString() == "1")
                                        {
                                            Actions = "退回";
                                        }
                                    }
                                    break;
                                }
                            }
                            foreach (EntityLibrary.MobileClient_StepControl model in StepControls)
                            {
                                if (ColumnNames.ToUpper() == "PROCESSNAME" || ColumnNames.ToUpper() == "INCIDENT")
                                {
                                    continue;
                                }
                                string destCol = ConvertUtil.ToString(model.DestColumnName);
                                if (destCol == null)
                                {
                                    destCol = "";
                                }
                                string destVarCol = ConvertUtil.ToString(model.DestVariableName);
                                if (destVarCol == null)
                                {
                                    destVarCol = "";
                                }
                                if (ColumnNames.ToUpper() == destCol.ToUpper() || ColumnNames.ToUpper() == destVarCol.ToUpper())
                                {
                                    if (model.ControlID == AppControl.ID)
                                    {
                                        ApprovalRemark = dt.Rows[0][j].ToString();
                                    }

                                    if (model.DestType != null && model.DestType.IndexOf("DataBase") >= 0)
                                    {
                                        if (dt.Rows.Count > 0)
                                        {
                                            StringBuilder strsql = new StringBuilder();
                                            strsql.Append("select count(*) from " + model.DestTableName + " where ProcessName='" + ProcessName + "' and Incident='" + NewIncident + "'");
                                            object obj = DbHelperSQL.GetSingle(strsql.ToString());
                                            if (ConvertUtil.ToInt32(obj) == 0)
                                            {
                                                strsql = new StringBuilder();
                                                strsql.Append("insert into ").Append(model.DestTableName).Append(" (ProcessName,Incident) values ");
                                                strsql.Append("('" + ProcessName + "' , '" + NewIncident + "')");
                                                DbHelperSQL.ExecuteSql(strsql.ToString());
                                            }

                                            strsql = new StringBuilder();
                                            strsql.Append("update ").Append(model.DestTableName).Append(" set ");
                                            strsql.Append(ColumnNames).Append("=").Append("'" + dt.Rows[0][j].ToString() + "'");
                                            strsql.Append("where ProcessName='" + ProcessName + "' and Incident='" + NewIncident + "'");
                                            DbHelperSQL.ExecuteSql(strsql.ToString());
                                        }
                                    }
                                    if (model.DestType != null && model.DestType.IndexOf("ElectronicForm") >= 0)
                                    {
                                        Variable var_item = new Variable();
                                        var_item.strVariableName = model.DestVariableName;
                                        var_item.objVariableValue = new object[] { dt.Rows[0][j].ToString().Trim() };
                                        var.Add(var_item);
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    DataTable dd = ds.Tables["ApprovalRemark"];
                    if (dd.Rows.Count > 0)
                    {
                        ApprovalRemark = ds.Tables["ApprovalRemark"].Rows[0][0].ToString();
                    }
                }

                #region 审批信息保存数据库SQL语句拼接
                StringBuilder appsql = new StringBuilder();
                appsql.AppendLine("INSERT INTO WF_APPROVALHISTORY");
                appsql.AppendLine("(");
                appsql.AppendLine("id,ProcessName,");
                appsql.AppendLine("Incident,");
                appsql.AppendLine("StepName,");
                appsql.AppendLine("Approver,");
                appsql.AppendLine("approvedate,");
                appsql.AppendLine("Comments,");
                appsql.AppendLine("Action");
                appsql.AppendLine(")");
                appsql.AppendLine("VALUES");
                appsql.AppendLine("(@GUID,@ProcessName,@Incident,@StepName,@User,@ApproveDate,@ApprovalRemark,@IsAction)");

                SqlParameter[] parameters = {
			            new SqlParameter("@GUID", SqlDbType.NVarChar,36) ,            
                        new SqlParameter("@ProcessName", SqlDbType.NVarChar,156) ,            
                        new SqlParameter("@Incident", SqlDbType.Int,4) ,            
                        new SqlParameter("@StepName", SqlDbType.NVarChar,156) ,            
                        new SqlParameter("@User", SqlDbType.NVarChar,156) ,            
                        new SqlParameter("@ApproveDate", SqlDbType.DateTime,20) ,            
                        new SqlParameter("@ApprovalRemark", SqlDbType.NVarChar,156) ,            
                        new SqlParameter("@IsAction", SqlDbType.NChar,10)     
                };

                parameters[0].Value = Guid.NewGuid().ToString();
                parameters[1].Value = task.strProcessName.Trim();
                parameters[2].Value = NewIncident;
                parameters[3].Value = task.strStepName;
                //parameters[4].Value = getUserName(task.strUser);
                parameters[4].Value = task.strUserFullName;
                parameters[5].Value = DateTime.Now;
                parameters[6].Value = ApprovalRemark;
                parameters[7].Value = Actions;

                DbHelperSQL.ExecuteSql(appsql.ToString(),parameters);
                #endregion

                return NewIncident;
            }
            catch (Exception ex)
            {
                PublicClass.WriteLogOfTxt(ex.Message);
                return 0;
            }
        }

        public DataSet GetProcessApprovalList(string ProcessName, int Incident)
        {
            DataSet ds = new DataSet();
            try
            {
                Incident.Status pstatus = new Incident.Status();
                Incident pincident = new Incident();
                pincident.LoadIncident(ProcessName, Incident);
                pincident.GetIncidentStatus(out pstatus);

                DataTable pGetData = new DataTable();
                DataColumn pCol4 = new DataColumn("StepName");
                pGetData.Columns.Add(pCol4);

                DataColumn pCol5 = new DataColumn("StepUser");
                pGetData.Columns.Add(pCol5);

                DataColumn pCol6 = new DataColumn("StartTime");
                pGetData.Columns.Add(pCol6);

                DataColumn pCol7 = new DataColumn("EndTime");
                pGetData.Columns.Add(pCol7);

                DataColumn pCol8 = new DataColumn("Status");
                pGetData.Columns.Add(pCol8);

                //获取流程步骤状态信息
                if (pstatus.TaskStatuses == null)
                {
                    throw new Exception("frm_TaskStatus_notification");
                }

                Incident.Status.StepStatus[] pStepStatus = null;
                try
                {
                    pStepStatus = pstatus.TaskStatuses;
                }
                catch
                {
                    throw new Exception("frm_TaskStatus_notification1");
                }

                foreach (Incident.Status.StepStatus pStep in pStepStatus)
                {
                    // 5:数据库机器人 2:发起步骤 4:用户步骤 6:子流程
                    int pStepType = pStep.nStepType;
                    if (pStepType != 2 && pStepType != 4 && pStepType != 6)
                    {
                        if (pStep.strStepName.Trim().ToUpper() != "COMPLETE")
                            continue;
                    }

                    DataRow pRow = pGetData.NewRow();
                    pRow["StepName"] = pStep.strStepName;

                    string pStepUser = pStep.strStepRecipient;//ProcessStepUser(pTaskID,pStep.strStepRecipient);
                    //去掉域名
                    int pIndex = pStepUser.Trim().IndexOf("/");
                    if (pIndex > 0)
                        pStepUser = pStepUser.Trim().Substring(pStepUser.Trim().IndexOf("/") + 1);

                    Ultimus.OC.User uUser = new Ultimus.OC.User();
                    Ultimus.OC.OrgChart oc = new Ultimus.OC.OrgChart();
                    oc.FindUser(pStep.strStepRecipient, "", 0, out uUser);

                    string pFullName = uUser.strUserFullName;

                    //去掉域名
                    int pIndex1 = pFullName.Trim().IndexOf("-");
                    if (pIndex1 > 0)
                        pFullName = pFullName.Trim().Substring(pFullName.Trim().IndexOf("-") + 1);

                    pRow["StepUser"] = pFullName + "(" + pStepUser + ")";

                    if (pStep.nStepStatus.ToString() == "13")
                    {
                        pRow["StartTime"] = "**********";
                    }
                    else
                    {
                        pRow["StartTime"] = DateTime.FromOADate(pStep.dtStartTime);
                    }
                    if (pStep.nStepStatus.ToString() == "13" || pStep.nStepStatus.ToString() == "1")
                    {
                        pRow["EndTime"] = "**********";
                    }
                    else
                    {
                        pRow["EndTime"] = DateTime.FromOADate(pStep.dtEndTime);
                    }

                    if (pStep.nStepStatus.ToString() == "1")
                    {
                        pRow["Status"] = "处理中";
                    }
                    else if (pStep.nStepStatus.ToString() == "3")
                    {
                        pRow["Status"] = "已完成";
                    }
                    else if (pStep.nStepStatus.ToString() == "4" || pStep.nStepStatus.ToString() == "7")
                    {
                        pRow["Status"] = "退　回";
                    }
                    else if (pStep.nStepStatus.ToString() == "13")
                    {
                        pRow["Status"] = "队　列";
                    }
                    else if (pStep.nStepStatus.ToString() == "19")
                    {
                        pRow["Status"] = "失　败";
                    }
                    else
                    {
                        pRow["Status"] = "紧　急";
                    }

                    pGetData.Rows.Add(pRow);
                }

                ds.Tables.Add(pGetData);
            }
            catch (Exception ex)
            {
                PublicClass.WriteLogOfTxt(ex.Message);
            }
            return ds;
        }

        public DataSet GetProcessAttachments(string ProcessName, string Incident)
        {
            return DataAccess.Instance("BizDB").ExecuteDataSet("select * from WF_ATTACHMENT where PROCESSNAME='" + ProcessName + "' and INCIDENT='" + Incident + "'");
        }

        public string getUserName(string loginname) {
            string username = "";
            string oldloginname = loginname;
            loginname = loginname.Replace("OHC\\","");
            string sql = "select * from org_user where loginname='" + loginname + "'";
            DataTable dt = DataAccess.Instance("BizDB").ExecuteDataSet(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                username = dt.Rows[0]["username"].ToString();
            }
            else {
                username = oldloginname;
            }
            return username;
        }
    }
}
