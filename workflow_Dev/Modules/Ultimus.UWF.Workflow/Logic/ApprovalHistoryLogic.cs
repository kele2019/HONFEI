using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Data;
using System.Data.Common;
using Ultimus.UWF.Workflow.Entity;
using MyLib;
using Ultimus.UWF.Common.Logic;

namespace Ultimus.UWF.Workflow.Logic
{
    public class ApprovalHistoryLogic
    {
        private DataAccess db = new DataAccess("BizDB");
        string flag = "@";
        public DataTable GetApprovalHistoryByProc(string ProcessaName, int Incident)
        {
            try
            {
                StringBuilder strsql = new StringBuilder();
                //strsql.Append(" select A.*,(case when   ApproverName=B.EXT04 then ApproverName else ApproverName+' '+B.EXT04 end) as UserName from(  ");
                //strsql.Append(" select t.* from WF_ApprovalHistory t where ProcessName=" + flag + "ProcessName and Incident=" + flag + "Incident ");
                //strsql.Append(") A left join ORG_USER B on A.EXT01=B.LOGINNAME ");
                //strsql.Append("order by CREATEDATE");
                strsql.Append(" select A.*,");
                strsql.Append(" (case when A.EXT02=''   then '' ELSE (SELECT  (case when   USERNAME=EXT04 then USERNAME ");
                strsql.Append(" else USERNAME+ORG_USER.EXT04   end) UserName FROM ORG_USER WHERE LOGINNAME=A.EXT02)+' on behalf of ' END  ) AgentName,");
                strsql.Append("(case when   ApproverName=B.EXT04 then ApproverName else B.EXT04 end) as UserName from(  ");
                strsql.Append(" select t.* from WF_ApprovalHistory t where ProcessName=" + flag + "ProcessName and Incident=" + flag + "Incident ");
                strsql.Append(") A left join ORG_USER B on A.EXT01=B.LOGINNAME ");
                strsql.Append("order by CREATEDATE");
                DbCommand dbcom = db.CreateCommand(strsql.ToString());
                db.AddInParameter(dbcom, "" + flag + "ProcessName", DbType.String, ProcessaName);
                db.AddInParameter(dbcom, "" + flag + "Incident", DbType.Int32, Incident);
                return db.ExecuteDataTable(dbcom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ApprovalHistoryEntity> GetApprovalHistory(string ProcessaName, int Incident)
        {
            try
            {
                StringBuilder strsql = new StringBuilder();
                strsql.Append("select * from WF_ApprovalHistory where ProcessName=" + flag + "ProcessName and Incident=" + flag + "Incident order by id");
                DbCommand dbcom = db.CreateCommand(strsql.ToString());
                db.AddInParameter(dbcom, "" + flag + "ProcessName", DbType.String, ProcessaName);
                db.AddInParameter(dbcom, "" + flag + "Incident", DbType.Int32, Incident);
                DbDataReader dr = db.ExecuteReader(dbcom);
                List<ApprovalHistoryEntity> list = new List<ApprovalHistoryEntity>();
                while (dr.Read())
                {
                    ApprovalHistoryEntity item = new ApprovalHistoryEntity();
                    if (dr["ID"] != null && !String.IsNullOrEmpty(dr["ID"].ToString()))
                    {
                        item.ID = dr["ID"].ToString();
                    }
                    if (dr["ProcessName"] != null && !String.IsNullOrEmpty(dr["ProcessName"].ToString()))
                    {
                        item.ProcessName = dr["ProcessName"].ToString();
                    }
                    if (dr["Incident"] != null && !String.IsNullOrEmpty(dr["Incident"].ToString()))
                    {
                        try
                        {
                            item.Incident = Convert.ToInt32(dr["Incident"].ToString());
                        }
                        catch
                        {
                            item.Incident = 0;
                        }
                    }
                    
                    if (dr["StepName"] != null && !String.IsNullOrEmpty(dr["StepName"].ToString()))
                    {
                        item.StepName = dr["StepName"].ToString();
                    }
                    if (dr["ApproverName"] != null && !String.IsNullOrEmpty(dr["ApproverName"].ToString()))
                    {
                        item.Approver = dr["ApproverName"].ToString();
                    }
                   
                    if (dr["Action"] != null && !String.IsNullOrEmpty(dr["Action"].ToString()))
                    {
                        item.Action = dr["Action"].ToString();
                    }
                    if (dr["Comments"] != null && !String.IsNullOrEmpty(dr["Comments"].ToString()))
                    {
                        item.Comments = dr["Comments"].ToString();
                    }
                    if (dr["CREATEDATE"] != null && !String.IsNullOrEmpty(dr["CREATEDATE"].ToString()))
                    {
                        try
                        {
                            item.ApproveDate = Convert.ToDateTime(dr["CREATEDATE"].ToString());
                        }
                        catch { }
                    }
                    if (dr["Status"] != null && !String.IsNullOrEmpty(dr["Status"].ToString()))
                    {
                        item.Status = dr["Status"].ToString();
                    }
                    if (dr["Ext01"] != null && !String.IsNullOrEmpty(dr["Ext01"].ToString()))
                    {
                        item.Ext01 = dr["Ext01"].ToString();
                    }
                    if (dr["Ext02"] != null && !String.IsNullOrEmpty(dr["Ext02"].ToString()))
                    {
                        item.Ext02 = dr["Ext02"].ToString();
                    }
                    if (dr["Ext03"] != null && !String.IsNullOrEmpty(dr["Ext03"].ToString()))
                    {
                        item.Ext03 = dr["Ext03"].ToString();
                    }
                    if (dr["Ext04"] != null && !String.IsNullOrEmpty(dr["Ext04"].ToString()))
                    {
                        item.Ext04 = dr["Ext04"].ToString();
                    }
                    if (dr["Ext05"] != null && !String.IsNullOrEmpty(dr["Ext05"].ToString()))
                    {
                        item.Ext05 = dr["Ext05"].ToString();
                    }
                    list.Add(item);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ApprovalHistoryEntity GetApprovalHistoryByID(string ID)
        {
            try
            {
                StringBuilder strsql = new StringBuilder();
                strsql.Append("select * from WF_ApprovalHistory where ID=" + flag + "ID order by id");
                DbCommand dbcom = db.CreateCommand(strsql.ToString());
                db.AddInParameter(dbcom, "" + flag + "ID", DbType.Int32, ID);
                DbDataReader dr = db.ExecuteReader(dbcom);
                ApprovalHistoryEntity item = new ApprovalHistoryEntity();
                while (dr.Read())
                {
                    if (dr["ID"] != null && !String.IsNullOrEmpty(dr["ID"].ToString()))
                    {
                        item.ID = dr["ID"].ToString();
                    }
                    if (dr["ProcessName"] != null && !String.IsNullOrEmpty(dr["ProcessName"].ToString()))
                    {
                        item.ProcessName = dr["ProcessName"].ToString();
                    }
                    if (dr["Incident"] != null && !String.IsNullOrEmpty(dr["Incident"].ToString()))
                    {
                        try
                        {
                            item.Incident = Convert.ToInt32(dr["Incident"].ToString());
                        }
                        catch
                        {
                            item.Incident = 0;
                        }
                    }
                    if (dr["StepName"] != null && !String.IsNullOrEmpty(dr["StepName"].ToString()))
                    {
                        item.StepName = dr["StepName"].ToString();
                    }
                    //if (dr["STEPLEVEL"] != null && !String.IsNullOrEmpty(dr["STEPLEVEL"].ToString()))
                    //{
                    //    item.Level = dr["STEPLEVEL"].ToString();
                    //}
                    if (dr["ApproverName"] != null && !String.IsNullOrEmpty(dr["ApproverName"].ToString()))
                    {
                        item.Approver = dr["ApproverName"].ToString();
                    }
                    //if (dr["ApproverFrom"] != null && !String.IsNullOrEmpty(dr["ApproverFrom"].ToString()))
                    //{
                    //    item.ApproverFrom = dr["ApproverFrom"].ToString();
                    //}
                    if (dr["Action"] != null && !String.IsNullOrEmpty(dr["Action"].ToString()))
                    {
                        item.Action = dr["Action"].ToString();
                    }
                    if (dr["Comments"] != null && !String.IsNullOrEmpty(dr["Comments"].ToString()))
                    {
                        item.Comments = dr["Comments"].ToString();
                    }
                    if (dr["CREATEDATE"] != null && !String.IsNullOrEmpty(dr["CREATEDATE"].ToString()))
                    {
                        try
                        {
                            item.ApproveDate = Convert.ToDateTime(dr["CREATEDATE"].ToString());
                        }
                        catch { }
                    }
                    if (dr["Status"] != null && !String.IsNullOrEmpty(dr["Status"].ToString()))
                    {
                        item.Status = dr["Status"].ToString();
                    }
                    if (dr["Ext01"] != null && !String.IsNullOrEmpty(dr["Ext01"].ToString()))
                    {
                        item.Ext01 = dr["Ext01"].ToString();
                    }
                    if (dr["Ext02"] != null && !String.IsNullOrEmpty(dr["Ext02"].ToString()))
                    {
                        item.Ext02 = dr["Ext02"].ToString();
                    }
                    if (dr["Ext03"] != null && !String.IsNullOrEmpty(dr["Ext03"].ToString()))
                    {
                        item.Ext03 = dr["Ext03"].ToString();
                    }
                    if (dr["Ext04"] != null && !String.IsNullOrEmpty(dr["Ext04"].ToString()))
                    {
                        item.Ext04 = dr["Ext04"].ToString();
                    }
                    if (dr["Ext05"] != null && !String.IsNullOrEmpty(dr["Ext05"].ToString()))
                    {
                        item.Ext05 = dr["Ext05"].ToString();
                    }
                }
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool AddApprovalHistory(IDbTransaction trans, ApprovalHistoryEntity Item)
        {
            try
            {
                StringBuilder strsql = new StringBuilder();
                strsql.Append("INSERT INTO WF_ApprovalHistory (ProcessName,Incident,StepName,ApproverName,Action,Comments,CreateDate,Status,Ext01,Ext02,Ext03,Ext04,Ext05)");
                strsql.Append(" VALUES ");
                strsql.Append("("  + flag + "ProcessName," + flag + "Incident," + flag + "StepName," + flag + "ApproverName," + flag + "Action," + flag + "Comments," + flag + "CREATEDATE," + flag + "Status," + flag + "Ext01," + flag + "Ext02," + flag + "Ext03," + flag + "Ext04," + flag + "Ext05)");
                //IDbCommand cmd = trans.Connection.CreateCommand();
                DbCommand dbcom = db.CreateCommand(strsql.ToString());
                dbcom.CommandText = strsql.ToString();
                SerialNoLogic sn=new SerialNoLogic();
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


        public bool DeleteApprovalHistoryByID(string ID)
        {
            try
            {
                StringBuilder strsql = new StringBuilder();
                if (ID.IndexOf(",") > 0)
                {
                    strsql.Append("delete from WF_ApprovalHistory where ID in(" + ID + ")");
                }
                else
                {
                    strsql.Append("delete from WF_ApprovalHistory where ID ='" + ID + "'");
                }
                if (db.ExecuteNonQuery(strsql.ToString()) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteApprovalHistoryByProc(string ProcessaName, int Incident)
        {
            try
            {
                StringBuilder strsql = new StringBuilder();
                strsql.Append("delete from WF_ApprovalHistory where ProcessName=" + flag + "ProcessName and Incident=" + flag + "Incident");
                DbCommand dbcom = db.CreateCommand(strsql.ToString());
                db.AddInParameter(dbcom, "" + flag + "ProcessName", DbType.String, ProcessaName);
                db.AddInParameter(dbcom, "" + flag + "Incident", DbType.Int32, Incident);
                if (db.ExecuteNonQuery(dbcom) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}