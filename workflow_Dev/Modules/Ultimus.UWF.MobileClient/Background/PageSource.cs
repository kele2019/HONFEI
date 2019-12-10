using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataBaseLibrary;
using EntityLibrary;
using MyLib;
using System.Data.SqlClient;
using ClientService;
using System.Xml;

namespace DALLibrary
{
    public class PageSource
    {
        //private DbHelperSQLP db = new DbHelperSQLP();
        private MobileClient_Process ProcessDAL = new MobileClient_Process();
        private MobileClient_Step StepDAL = new MobileClient_Step();
        private MobileClient_StepControl StepControlDAL = new MobileClient_StepControl();
        private MobileClient_Control ControlDAL = new MobileClient_Control();
        WorkflowRef _ref = new WorkflowRef();
        public DataSet GetPageControlSource(string ProcessName, string StepName, string TaskId, string Incident)
        {
            try
            {
                DataSet ds = new DataSet();

                EntityLibrary.MobileClient_Process ProcessModel = ProcessDAL.GetModel("ProcessName='" + ProcessName + "'")[0];

                //List<Variable> Variables = new List<Variable>();
                //Variable[] vars = GetVariable(ProcessModel.ProcessName, 0);
                //Variables.AddRange(vars);
                //ClientService.WorkflowSrv.VarEntity[] Variables = _ref.GetVariableList(ProcessName, TaskId);

                //XmlDocument Variables = new XmlDocument();
                //Variables.LoadXml(_ref.GetVariableXml(ProcessName, TaskId));

                string[] xmlArr = _ref.GetVariableXml(ProcessName, TaskId, Convert.ToInt32(Incident));
                XmlDocument Variables = new XmlDocument();
                Variables.LoadXml(xmlArr[0].Replace("xmlns=", "aaaaaaaaa="));//.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>",""));

                XmlDocument Variables_Process = new XmlDocument();
                Variables_Process.LoadXml(xmlArr[1].Replace("xmlns=", "aaaaaaaaa="));

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
                                    strsql.AppendLine("where 1=1 ");
                                    strsql.AppendLine(Control.SourceWhere);
                                    strsql.AppendLine("order by");
                                    strsql.AppendLine(Control.SourceColumnName);
                                    //IDataReader reader = DbHelperSQL.ExecuteReader(strsql.ToString());
                                    DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(strsql.ToString());
                                    //dt.Load(reader);
                                    ds.Tables.Add(dt);
                                    if (Control.DestType == "ElectronicForm")
                                    {
                                        ds.Tables[ds.Tables.Count - 1].TableName = Control.DestVariableName;
                                    }
                                    else
                                    {
                                        ds.Tables[ds.Tables.Count - 1].TableName = Control.DestColumnName;//Control.ColumnName;

                                    }
                                    TableIndex++;
                                    //reader.Close();
                                }
                            }
                            else if (Control.SourceType == "ElectronicForm")
                            {
                                DataTable dt = new DataTable();
                                dt.Columns.Add(Control.SourceVariableName);
                                XmlNodeList ndl;
                                //如果是step
                                if (Control.SourceVariableName.IndexOf("TaskData_") == 0) 
                                {
                                    ndl = Variables.SelectNodes("/TaskData/" + Control.SourceVariableName.Replace("TaskData_", ""));
                                }
                                //如果是Process
                                else if (Control.SourceVariableName.IndexOf("IncidentData_") == 0)
                                {
                                    ndl = Variables_Process.SelectNodes("/IncidentData/" + Control.SourceVariableName.Replace("IncidentData_", ""));
                                }
                                else
                                {
                                    ndl = Variables.SelectNodes("/TaskData/Global/" + Control.SourceVariableName);
                                }
                                foreach (XmlNode xn in ndl)
                                {
                                    DataRow dr = dt.NewRow();
                                    dr[0] = Convert.ToString(xn.InnerText);
                                    dt.Rows.Add(dr);
                                }
                                ds.Tables.Add(dt);
                                if (Control.DestType == "ElectronicForm")
                                {
                                    ds.Tables[ds.Tables.Count - 1].TableName = Control.DestVariableName;
                                }
                                else
                                {
                                    ds.Tables[ds.Tables.Count - 1].TableName = Control.DestColumnName;//Control.ColumnName;

                                }
                                TableIndex++;
                                //foreach (Variable v in Variables)
                                //{
                                //    if (v.strVariableName.Trim() == Control.SourceVariableName.Trim())
                                //    {
                                //        DataTable dt = new DataTable();
                                //        if (v.objVariableValue != null)
                                //            dt.LoadDataRow(v.objVariableValue, false);
                                //        ds.Tables.Add(dt);
                                //        ds.Tables[ds.Tables.Count - 1].TableName = Control.ColumnName;
                                //        TableIndex++;
                                //        break;
                                //    }
                                //}
                                //foreach (ClientService.WorkflowSrv.VarEntity v in Variables) 
                                //{
                                //    if (v.Name.Trim() == Control.SourceVariableName.Trim()) 
                                //    {
                                //        DataTable dt = new DataTable();
                                //        dt.Columns.Add("Value", typeof(string));
                                //        if (v.Value != null)
                                //        {
                                //            DataRow dr = dt.NewRow();
                                //            dr[0] = v.Value;
                                //            dt.Rows.Add(dr);
                                //                //.LoadDataRow(v.Value, false);
                                //        }
                                //        ds.Tables.Add(dt);
                                //        ds.Tables[ds.Tables.Count - 1].TableName = Control.ColumnName;
                                //        TableIndex++;
                                //        break;
                                //    }
                                //}
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

   

        public string GetInitTaskID(string processName)
        {
            return DataAccess.Instance("UltDB").ExecuteScalar("select top 1 INITIATEID from INITIATE  where  ProcessName='" + processName + "' order by PROCESSVERSION desc").ToString();
        }

        public DataSet GetPageDestSource(string ProcessName, int nIncident, string StepName, string TaskId)
        {
            try
            {
                DataSet ds = new DataSet();

                EntityLibrary.MobileClient_Process ProcessModel = ProcessDAL.GetModel("ProcessName='" + ProcessName.Trim() + "'")[0];

                //ClientService.WorkflowSrv.VarEntity[] Variables = _ref.GetVariableList(ProcessName, TaskId);
                //string ttt = "<?xml version=\"1.0\" encoding=\"utf-16\"?><TaskData xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"http://schema.ultimus.com/付款申请/1/8469\"><Global><Name xmlns=\"http://schema.ultimus.com/付款申请/1/Types\">ttttt</Name><Reason xmlns=\"http://schema.ultimus.com/付款申请/1/Types\">213123123</Reason><Item1 xmlns=\"http://schema.ultimus.com/付款申请/1/Types\"><ItemName>test1</ItemName><ItemCount>test11</ItemCount></Item1><Item1 xmlns=\"http://schema.ultimus.com/付款申请/1/Types\"><ItemName>test11</ItemName><ItemCount>test111</ItemCount></Item1><Item21 xmlns=\"http://schema.ultimus.com/付款申请/1/Types\"><ItemName2>test2</ItemName2><ItemCount2>test22</ItemCount2></Item21><Item21 xmlns=\"http://schema.ultimus.com/付款申请/1/Types\"><ItemName2>test22</ItemName2><ItemCount2>test222</ItemCount2></Item21></Global><SYS_PROCESSATTACHMENTS /></TaskData>";
                string[] xmlArr = _ref.GetVariableXml(ProcessName, TaskId, nIncident);
                XmlDocument Variables = new XmlDocument();
                Variables.LoadXml(xmlArr[0].Replace("xmlns=", "aaaaaaaaa="));//.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>",""));

                XmlDocument Variables_Process = new XmlDocument();
                Variables_Process.LoadXml(xmlArr[1].Replace("xmlns=", "aaaaaaaaa="));
                //Variables.LoadXml(ttt.Replace("xmlns=","aaaaaaaaa="));
                //List<Variable> Variables = new List<Variable>();
                //Variable[] vars;
                //if (nIncident > 0)
                //{
                //    vars = GetVariable(ProcessModel.ProcessName, nIncident);
                //}
                //else
                //{
                //    vars = GetVariable(ProcessModel.ProcessName, 0);
                //}
                //Variables.AddRange(vars);

                EntityLibrary.MobileClient_Step StepModel = StepDAL.GetModel("FK_ID='" + ProcessModel.ID + "' and StepName='" + StepName + "'")[0];

                DataTable MasterTable = new DataTable();

                List<EntityLibrary.MobileClient_StepControl> StepControls = StepControlDAL.GetModel("FK_ID='" + StepModel.ID + "' ORDER BY convert(int,isnull([ORDERBY],0))");
                
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

                    DataTable sqlDt = null;

                    foreach (EntityLibrary.MobileClient_StepControl Control in Master)
                    {
                        if (MasterTable.Rows.Count < 1) //加行
                        {
                            MasterTable.Rows.Add(MasterTable.NewRow());
                        }

                        if (Control.DestType != null && Control.DestType.IndexOf("DataBase") >= 0)
                        {
                            if (sqlDt == null) sqlDt = DbHelperSQL.Query("select * from " + Control.DestTableName + " where ProcessName='" + ProcessName + "' and Incident='" + nIncident + "'").Tables[0];

                            if (Control.ControlID == 16)
                            {
                                MasterTable.Columns.Add(Control.Format);
                                if (sqlDt.Columns.Contains(Control.DestColumnName))
                                {
                                    MasterTable.Rows[0][Control.Format] = Convert.ToString(sqlDt.Rows[0][Control.DestColumnName].ToString());
                                }
                            }
                            else
                            {
                                MasterTable.Columns.Add(Control.DestColumnName); //加列
                                if (sqlDt.Columns.Contains(Control.DestColumnName))
                                {
                                    MasterTable.Rows[0][Control.DestColumnName] = Convert.ToString(sqlDt.Rows[0][Control.DestColumnName].ToString());
                                }
                            }
                        }
                        //if (Control.DestType != null && Control.DestType.IndexOf("DataBase") >= 0)
                        //{
                        //    if (!MasterTableNameList.ContainsKey(Control.DestTableName))
                        //    {
                        //        string ColumnNames = "";
                        //        foreach (EntityLibrary.MobileClient_StepControl model in Master)
                        //        {
                        //            if (model.DestTableName == Control.DestTableName)
                        //            {
                        //                ColumnNames += model.DestColumnName + ",";
                        //            }
                        //        }
                        //        MasterTableNameList.Add(Control.DestTableName, null);
                        //        //db.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[Control.DestConnectionString].ConnectionString.Trim();
                        //        DataTable dt = DbHelperSQL.Query("select " + ColumnNames.TrimEnd(',') + " from " + Control.DestTableName + " where ProcessName='" + ProcessName + "' and Incident='" + nIncident + "'").Tables[0];
                        //        if (dt.Rows.Count == 0)
                        //        {
                        //            dt.Rows.Add(dt.NewRow());
                        //        }
                        //        ds.Tables.Add(dt.Copy());
                        //        string tablename = Control.DestTableName;
                        //        if (string.IsNullOrEmpty(tablename))
                        //        {
                        //            tablename = "MasterTable";
                        //        }
                        //        ds.Tables[ds.Tables.Count - 1].TableName = tablename;
                        //    }
                        //}
                        if (Control.DestType != null && Control.DestType.IndexOf("ElectronicForm") >= 0)
                        {
                            //foreach (ClientService.WorkflowSrv.VarEntity v in Variables)
                            //{
                            //    if (v.Name == Control.DestVariableName)
                            //    {
                            //       // MasterTable.Rows[0][Control.ColumnName] = v.Value == null ? "" : v.Value[0] != null ? v.Value[0].ToString().Trim() : "";
                            //        MasterTable.Rows[0][Control.ColumnName] =Convert.ToString( v.Value);
                            //        break;
                            //    }
                            //}
                            try
                            {
                                string cName = Control.Format;
                                //如果
                                if (Control.ControlID == 16)
                                {
                                    cName = Control.Format;
                                }
                                else
                                {
                                    cName = Control.DestVariableName;
                                }
                                MasterTable.Columns.Add(cName);
                                //如果是step
                                if (Control.DestVariableName.IndexOf("TaskData_") == 0) 
                                {
                                    //XmlNode node = Variables.SelectSingleNode("/TaskData/" + Control.DestVariableName.Replace("TaskData_",""));
                                    //MasterTable.Rows[0][cName] = Convert.ToString(node.InnerText);
                                    XmlNodeList ndl = Variables.SelectNodes("/TaskData/" + Control.DestVariableName.Replace("TaskData_", ""));
                                    foreach (XmlNode xn in ndl) 
                                    {
                                        MasterTable.Rows[0][cName] += Convert.ToString(xn.InnerText) + "|";
                                    }
                                    MasterTable.Rows[0][cName] = MasterTable.Rows[0][cName].ToString().TrimEnd('|');
                                }
                                //如果是Process
                                else if (Control.DestVariableName.IndexOf("IncidentData_") == 0)
                                {
                                    //XmlNode node = Variables_Process.SelectSingleNode("/IncidentData/" + Control.DestVariableName.Replace("IncidentData_", ""));
                                    //MasterTable.Rows[0][cName] = Convert.ToString(node.InnerText);
                                    XmlNodeList ndl = Variables_Process.SelectNodes("/IncidentData/" + Control.DestVariableName.Replace("IncidentData_", ""));
                                    foreach (XmlNode xn in ndl)
                                    {
                                        MasterTable.Rows[0][cName] += Convert.ToString(xn.InnerText) + "|";
                                    }
                                    MasterTable.Rows[0][cName] = MasterTable.Rows[0][cName].ToString().TrimEnd('|');
                                }
                                else
                                {
                                    //XmlNode node = Variables.SelectSingleNode("/TaskData/Global/" + Control.DestVariableName);
                                    //MasterTable.Rows[0][cName] = Convert.ToString(node.InnerText);
                                    XmlNodeList ndl = Variables.SelectNodes("/TaskData/Global/" + Control.DestVariableName);
                                    foreach (XmlNode xn in ndl)
                                    {
                                        MasterTable.Rows[0][cName] += Convert.ToString(xn.InnerText) + "|";
                                    }
                                    MasterTable.Rows[0][cName] = MasterTable.Rows[0][cName].ToString().TrimEnd('|');
                                }
                                //foreach (XmlNode node in list)
                                //{
                                //    if (node.Name == Control.DestVariableName)
                                //    {
                                //        MasterTable.Rows[0][Control.ColumnName] = Convert.ToString(node.InnerText);
                                //        break;
                                //    }
                                //}
                            }
                            catch (Exception e) { }
                        }
                    }

                    ds.Tables.Add(MasterTable);
                    ds.Tables[ds.Tables.Count - 1].TableName = "MasterTable";

                    //Incident inc = new Incident();
                    //inc.LoadIncident(ProcessName, nIncident);
                    

                    //Dictionary<string, string> SublistTableNameList = new Dictionary<string, string>();
                    List<string> grids = new List<string>(); //表名清单
                    foreach (EntityLibrary.MobileClient_StepControl Control in Sublist)
                    {
                        if (!grids.Contains(Control.Format))
                        {
                            grids.Add(Control.Format);
                        }
                    }
                    //遍历所有子表
                    foreach (string grid in grids) 
                    {
                        DataTable SublistTable = new DataTable();
                        foreach (EntityLibrary.MobileClient_StepControl Control in Sublist)
                        {
                            if (Control.Format == grid)
                            {
                                //如果是电子表格
                                if (Control.DestType != null && Control.DestType.IndexOf("ElectronicForm") >= 0)
                                {
                                    XmlNodeList list = Variables.SelectNodes("/TaskData/Global/" + grid);
                                    for (int i = 0; i < list.Count; i++)
                                    {
                                        if (SublistTable.Rows.Count < list.Count)
                                        {
                                            DataRow newRow = SublistTable.NewRow();
                                            SublistTable.Rows.Add(newRow);
                                        }
                                        XmlNodeList list_ = list[i].ChildNodes;
                                        foreach (XmlNode node_ in list_)
                                        {
                                            if (node_.Name == Control.DestVariableName)
                                            {
                                                if (!SublistTable.Columns.Contains(Control.ColumnName)) SublistTable.Columns.Add(Control.ColumnName);
                                                SublistTable.Rows[i][Control.ColumnName] = Convert.ToString(node_.InnerText);
                                            }
                                        }
                                    }
                                    //if (Control.Format == grid)
                                    //{
                                    //    string VariableValue = "";
                                    //    foreach (ClientService.WorkflowSrv.VarEntity v in Variables)
                                    //    {
                                    //        if (v.Name == Control.DestVariableName)
                                    //        {
                                    //            VariableValue = v.Value;
                                    //            break;
                                    //        }
                                    //    }

                                    //    string VariableName = Control.DestVariableName;
                                    //    SublistTable.Columns.Add(VariableName);
                                    //    string[] columnValue = VariableValue.Split('|');
                                    //    if (SublistTable.Rows.Count == 0) 
                                    //    {
                                    //        for (int i = 0; i < columnValue.Length; i++)
                                    //        {
                                    //            DataRow newRow = SublistTable.NewRow();
                                    //            SublistTable.Rows.Add(newRow);
                                    //        }
                                    //    }
                                    //    for (int i = 0; i < columnValue.Length; i++) 
                                    //    {
                                    //        SublistTable.Rows[i][VariableName] = columnValue[i].ToString();
                                    //    }
                                    //}
                                }
                                //如果是数据库表
                                if (Control.DestType != null && Control.DestType.IndexOf("DataBase") >= 0)
                                {
                                    DataTable dt = DbHelperSQL.Query("select * from " + Control.DestTableName + " where ProcessName='" + ProcessName + "' and Incident='" + nIncident + "'").Tables[0];
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        if (SublistTable.Rows.Count < dt.Rows.Count)
                                        {
                                            DataRow newRow = SublistTable.NewRow();
                                            SublistTable.Rows.Add(newRow);
                                        }
                                        if (!SublistTable.Columns.Contains(Control.ColumnName)) SublistTable.Columns.Add(Control.ColumnName);
                                        SublistTable.Rows[i][Control.ColumnName] = Convert.ToString(dt.Rows[i][Control.DestColumnName].ToString());
                                    }
                                }
                            }
                        }
                        ds.Tables.Add(SublistTable);
                        ds.Tables[ds.Tables.Count - 1].TableName = grid;// "ElectronicForm";
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
                //List<Variable> var = new List<Variable>();

                EntityLibrary.MobileClient_Process ProcessModel = ProcessDAL.GetModel("ProcessName='" + ProcessName.Trim() + "'")[0];

                //List<Variable> Variables = new List<Variable>();
                //Variable[] vars = GetVariable(ProcessModel.ProcessName, nIncident);
                //Variables.AddRange(vars);

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

                //Tasklist list = new Tasklist();
                //TasklistFilter filter = new TasklistFilter();
                //filter.strProcessNameFilter = ProcessName;
                //filter.strStepLabelFilter = StepName;
                //filter.nIncidentNo = nIncident;
                //filter.strArrUserName = new string[] { UserAccount };
                //list.LoadFilteredTasks(filter);
                //Task task = list.GetNextTask();
                int NewIncident = 0; 
                string SendFormError = "";
                string ApprovalRemark = null;
                string Actions = null;

                if (nIncident > 0)
                {
                    if (type == 1) //退回
                    {
                        //task.Return(var.ToArray(), "", Summary, out SendFormError);
                        //NewIncident = nIncident;
                    }
                    else //提交
                    {
                        //task.Send(var.ToArray(), "", Summary, ref NewIncident, out SendFormError);
                    }
                }
                else
                {
                    //task.SendFrom(UserAccount, var.ToArray(), "", Summary, ref NewIncident, out SendFormError);
                }
                if (NewIncident <= 0)
                {
                    return 0;
                }
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
                            string destCol=ConvertUtil.ToString( model.DestColumnName);
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
                                        object obj=DbHelperSQL.GetSingle(strsql.ToString());
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
                                    //Variable var_item = new Variable();
                                    //var_item.strVariableName = model.DestVariableName;
                                    //var_item.objVariableValue = new object[] { dt.Rows[0][j].ToString().Trim() };
                                    //var.Add(var_item);
                                }
                            }
                        }
                    }
                }

                DataTable dd = ds.Tables["ApprovalRemark"];
                if (dd.Rows.Count > 0)
                {
                    ApprovalRemark = ds.Tables["ApprovalRemark"].Rows[0][0].ToString();
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
                //appsql.AppendLine("'"+Guid.NewGuid().ToString()+"','" + task.strProcessName.Trim() + "',");
                //appsql.AppendLine("'" + NewIncident + "',");
                //appsql.AppendLine("'" + task.strStepName + "',");
                //appsql.AppendLine("'" + task.strUser + "',");
                //appsql.AppendLine("'" + ApprovalRemark + "',");
                //appsql.AppendLine("'" + Actions + "'");
                //appsql.AppendLine(")");

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
                //parameters[1].Value = task.strProcessName.Trim();
                //parameters[2].Value = NewIncident;
                //parameters[3].Value = task.strStepName;
                //parameters[4].Value = task.strUser;
                parameters[5].Value = DateTime.Now;
                parameters[6].Value = ApprovalRemark;
                parameters[7].Value = Actions;

                DbHelperSQL.ExecuteSql(appsql.ToString(),parameters);
                #endregion

                //db.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["W2DB"].ConnectionString;

                //DbHelperSQL.ExecuteSql(appsql.ToString());

                

                //appsql = new StringBuilder();
                //appsql.AppendLine("update [CinderellaOA_WorkFlow].[dbo].[Aid_SignInfo] set [Incident]='" + NewIncident + "' where [TaskID]='" + task.strTaskId + "'");
                //DbHelperSQL.ExecuteSql(appsql.ToString());
                return NewIncident;
            }
            catch (Exception ex)
            {
                PublicClass.WriteLogOfTxt(ex.Message);
                return 0;
            }
        }


        public DataTable GetAttchmentInfo(string formId, string type) 
        {
            string sql = "select FILETYPE,[FILENAME],[NEWNAME] from dbo.WF_ATTACHMENT where FORMID='" + formId + "' and [Type]='" + type + "'";
            DataTable dt =  DataAccess.Instance("BizDB").ExecuteDataTable(sql);
            return dt;
        }
    }
}
