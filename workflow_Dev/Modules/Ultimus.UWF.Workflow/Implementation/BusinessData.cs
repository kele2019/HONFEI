using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 
using MyLib;
using Ultimus.UWF.Workflow.Entity;
using System.Text;
using System.Data.Common;
using System.Data;
using Presale.Process.TravalExpense;

namespace Ultimus.UWF.Workflow.Implementation
{
    public class BusinessData
    {
      public   TravalEntity GetTaskEntityByName(string IncidentID)
        {
            TravalEntity mode = DataAccess.Instance("BizDB").ExecuteEntity<TravalEntity>("select * from PROC_TRAVEL where INCIDENT=@INCIDENT ", IncidentID);
            mode.TravalType=mode.TravalType == "1" ? "国内/Domesitc" : "国外/Abroad";
            return mode;
        }
        public   List<MyDB> GetTravalEntityDetail(string IncidentID)
      {
          List<MyDB> list = DataAccess.Instance("BizDB").ExecuteList<MyDB>("select * from PROC_TRAVEL_DT where INCIDENT=@INCIDENT ", IncidentID);
          return list;
      }
        public   TravalExpenseEntity GetTravalExpenseData(string IncidentID)
        {
            TravalExpenseEntity mode = DataAccess.Instance("BizDB").ExecuteEntity<TravalExpenseEntity>("select * from PROC_TravalExpense where INCIDENT=@INCIDENT ", IncidentID);
            return mode;
        }
        public   List<TravalExpenseEntityDetail> GetTravalExpenseDataDetail(string IncidentID)
        {
            List<TravalExpenseEntityDetail> list = DataAccess.Instance("BizDB").ExecuteList<TravalExpenseEntityDetail>("select * from Proc_TravalExpense_DT where INCIDENT=@INCIDENT ", IncidentID);
            return list;
        }

        public CashEntity GetCashAdvanceData(string IncidentID)
        {
            CashEntity mode = DataAccess.Instance("BizDB").ExecuteEntity<CashEntity>("select * from PROC_CashAdvance where INCIDENT=@INCIDENT ", IncidentID);
            return mode;
        }

        public PersonalExpense GetPersonalExpenseData(string IncidentID)
        {
            PersonalExpense mode = DataAccess.Instance("BizDB").ExecuteEntity<PersonalExpense>("select * from PROC_PersonalExpense where INCIDENT=@INCIDENT ", IncidentID);
            return mode;
        }
        public List<PeronalExpenseDetail> GetPeronalExpenseDetail(string IncidentID)
        {
            List<PeronalExpenseDetail> list = DataAccess.Instance("BizDB").ExecuteList<PeronalExpenseDetail>("select * from PROC_PersonalExpense_DT where INCIDENT=@INCIDENT ", IncidentID);
            return list;
        }

        public PersonalAllownce GetPersonalAllownceData(string IncidentID)
        {
            PersonalAllownce mode = DataAccess.Instance("BizDB").ExecuteEntity<PersonalAllownce>("select * from PROC_PersonalAllownce where INCIDENT=@INCIDENT ", IncidentID);
            return mode;
        }
        public List<PersonalAllownceDetail> GetPersonalAllownceDetail(string IncidentID)
        {
            List<PersonalAllownceDetail> list = DataAccess.Instance("BizDB").ExecuteList<PersonalAllownceDetail>("select * from PROC_PersonalAllownce_DT where INCIDENT=@INCIDENT ", IncidentID);
            return list;
        }
        public   List<Presale.Process.Common.ApprovalHistoryEntity> GetApprovalHistoryList(string ProcessName, string IncidentID)
        {
            DataAccess db = new DataAccess("BizDB");
            string flag = "@";
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" select A.*,(case when   ApproverName=B.EXT04 then ApproverName else ApproverName+' '+B.EXT04 end) as UserName from(");
            strsql.Append(" select t.* from WF_ApprovalHistory t where ProcessName=" + flag + "ProcessName and Incident=" + flag + "Incident ");
            strsql.Append(") A left join ORG_USER B on A.EXT01=B.LOGINNAME ");
            strsql.Append("order by CREATEDATE");
            DbCommand dbcom = db.CreateCommand(strsql.ToString());
            db.AddInParameter(dbcom, "" + flag + "ProcessName", DbType.String, ProcessName);
            db.AddInParameter(dbcom, "" + flag + "Incident", DbType.Int32, IncidentID);
            return db.ExecuteList<Presale.Process.Common.ApprovalHistoryEntity>(dbcom);
        }
        public List<Presale.Process.Common.AttachmentEntity> GetAttachments(string strFormid)
        {
            try
            {
                DataAccess db = new DataAccess("BizDB");
                StringBuilder strsql = new StringBuilder();
                strsql.Append("select FILENAME FileName,(PROCESSNAME+'\\'+[NEWNAME]+FILETYPE) NewName from WF_Attachment where FORMID='" + strFormid + "'");
                DbCommand dbcom = db.CreateCommand(strsql.ToString());
                return db.ExecuteList<Presale.Process.Common.AttachmentEntity>(dbcom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrgChart.Entity.UserEntity> GetInquireList(string ProcessName, string IncidentID)
        {
            string strsql = "select  (B.LOGINNAME+'|USER') LOGINNAME,B.USERNAME from (SELECT distinct RTRIM(ASSIGNEDTOUSER) ASSIGNEDTOUSER FROM TASKS with(nolock)";
            strsql += " WHERE 1=1 and processname='" + ProcessName + "' and incident='" + IncidentID + "' and STATUS<>1)";
             strsql += " A inner join   View_User B on  A.ASSIGNEDTOUSER=REPLACE(B.LOGINNAME,'\\','/')";
             return DataAccess.Instance("UltDB").ExecuteList<OrgChart.Entity.UserEntity>(strsql);
        }
        public bool OffsetCashAdvanceAmount(string ProcessName, string IncidentID)
        {
            if (ProcessName.ToLower() == "travel expense process")
            {
                object FORMID = DataAccess.Instance("BizDB").ExecuteScalar("select FORMID from PROC_TravalExpense where INCIDENT='" + IncidentID + "' and BrrowYes=1");
                if (FORMID!=null)
                {
                    StringBuilder strsql = new StringBuilder();
                    DataTable dtResevese = DataAccess.Instance("BizDB").ExecuteDataTable("select *  from PROC_ResevserData where FormID='" + FORMID.ToString() + "' and RetrunType='TravelExpese'");
                    foreach (DataRow item in dtResevese.Rows)
                    {
                        string CashAdvanceNo = item["CANo"].ToString();
                        string Amount = item["ReturnAmount"].ToString();
                        strsql.AppendFormat("update PROC_CashAdvance set ReturnAmount=ISNULL(ReturnAmount,0)-{0} where  DOCUMENTNO='{1}'", Amount, CashAdvanceNo);
                    }
                    if (strsql.ToString() != "")
                    {
                      return  DataAccess.Instance("BizDB").ExecuteNonQuery(strsql.ToString())>0;
                    }
                }
            }
            if (ProcessName.ToLower() == "personal expense process")
            {
                object FORMID = DataAccess.Instance("BizDB").ExecuteScalar("select FORMID from PROC_PersonalExpense where INCIDENT='" + IncidentID + "' and BrrowYes=1");
                if (FORMID != null)
                {
                    StringBuilder strsql = new StringBuilder();
                    DataTable dtResevese = DataAccess.Instance("BizDB").ExecuteDataTable("select *  from PROC_ResevserData where FormID='" + FORMID.ToString() + "' and RetrunType='PersonalExpese'");
                    foreach (DataRow item in dtResevese.Rows)
                    {
                        string CashAdvanceNo = item["CANo"].ToString();
                        string Amount = item["ReturnAmount"].ToString();
                        strsql.AppendFormat("update PROC_CashAdvance set ReturnAmount=ISNULL(ReturnAmount,0)-{0} where  DOCUMENTNO='{1}'", Amount, CashAdvanceNo);
                    }
                    if (strsql.ToString() != "")
                    {
                       return  DataAccess.Instance("BizDB").ExecuteNonQuery(strsql.ToString())>0;
                    }
                }
            }
            return true;
        }
       
  }
        
    
}