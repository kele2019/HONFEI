using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ptcent;
using System.Data;
using MyLib;
using System.IO;

namespace Ultimus.UWF.Workflow.EmailServices
{
    class EmployeePerformanceJob : BaseJob
    {
        public string LogName { get { return "PeformanceData"; } }
        protected override void OnStart()
        {
            try
            {
                OverrPerformanceData();
            }
            catch (Exception err)
            {
                LogFactory.All.WriteWithError(LogName, err);
            }
        }

        public void OverrPerformanceData()
        {
            List<UserInfo> UserList = GetUserNamebyAccount();
            DataTable dtMainData = DataAccess.Instance("UltDB").ExecuteDataTable(@"select INCIDENT,STATUS,INITIATOR,SUMMARY,PROCESSNAME from 
INCIDENTS where  PROCESSNAME = 'Employee Performance Report'");
            foreach (DataRow item in dtMainData.Rows)
            {
                string Status = item["STATUS"].ToString();
                string Incident = item["Incident"].ToString();
                //if (Status == "1")
                //    Status = "1";
                //if (Status == "4" || Status == "33")
                //    continue;
                //select APPLICANT,APPLICANTACCOUNT,REQUESTDATE,Year,ReportType,EmployeeName from  dbo.PROC_EmployeePerformance where INCIDENT=10
                string DeptManager = "";
                string DeptApproveDate = "";
                string SManager = "";
                string SApproveDate = "";
                string AFeedBackDate = "";
                string GMApprove = "";
                string GMApproveDate = "";
                string HRApprove = "";
                string HRApproveDate = "";
                #region  获取审批人信息
                string StrsqlTasks = "select  STEPLABEL,ENDTIME,ASSIGNEDTOUSER,Status from TASKS where   PROCESSNAME='Employee Performance Report' and INCIDENT=" + Incident;
                DataTable TData = DataAccess.Instance("UltDB").ExecuteDataTable(StrsqlTasks);
                if (TData.Rows.Count > 0)
                {
                    foreach (DataRow Titem in TData.Rows)
                    {
                        string StepLabel = Titem["STEPLABEL"].ToString().Trim();
                        string TStatus = Titem["Status"].ToString().Trim();
                        string ENDTIME = Titem["ENDTIME"].ToString();
                        string ASSIGNEDTOUSER = Titem["ASSIGNEDTOUSER"].ToString().Trim();
                        if (StepLabel == "Department manager BeginYear Approve" || StepLabel == "Department manager MidYear Approve" || StepLabel == "Department manager EndYear Approve")
                        {
                            var u1 = UserList.FirstOrDefault(o => o.LOGINNAME == ASSIGNEDTOUSER);
                            if (u1 != null)
                                DeptManager = u1.USERNAME + u1.EXT04;
                            if (TStatus == "3" || TStatus == "4")
                                DeptApproveDate = ENDTIME;
                        }
                        if (StepLabel == "Secondary Department manager Approve")
                        {
                            var u1 = UserList.FirstOrDefault(o => o.LOGINNAME == ASSIGNEDTOUSER);
                            if (u1 != null)
                                SManager = u1.USERNAME + u1.EXT04;
                            if (TStatus == "3" || TStatus == "4")
                                SApproveDate = ENDTIME;
                        }
                        if (StepLabel == "Applier Submit")
                        {
                            //SManager = "";
                            if (TStatus == "3" || TStatus == "4")
                                AFeedBackDate = ENDTIME;
                        }
                        if (StepLabel == "GM Approval")
                        {
                            var u1 = UserList.FirstOrDefault(o => o.LOGINNAME == ASSIGNEDTOUSER);
                            if (u1 != null)
                                GMApprove = u1.USERNAME + u1.EXT04;
                            if (TStatus == "3" || TStatus == "4")
                                GMApproveDate = ENDTIME;
                        }
                        if (StepLabel == "HR BeginYear Approve" || StepLabel == "HR MidYear Approve" || StepLabel == "HR EndYear Approve")
                        {
                            var u1 = UserList.FirstOrDefault(o => o.LOGINNAME == ASSIGNEDTOUSER);
                            if (u1 != null)
                                HRApprove = u1.USERNAME + u1.EXT04;
                            if (TStatus == "3" || TStatus == "4")
                                HRApproveDate = ENDTIME;
                        }
                    }

                }
                #endregion



                string StrsqlReport = "select ProcessStatus from PROC_EmployeePerformanceReport where Incident = " + Incident+ " and ProcessStatus in(1,2)";
                DataTable DataCount =DataAccess.Instance("BizDB").ExecuteDataTable(StrsqlReport);
                if (DataCount.Rows.Count > 0)
                {
                    string DataStatus = DataCount.Rows[0]["ProcessStatus"].ToString();
                    if (DataStatus == "1")//更新数据
                    {
                        string Strsqlupdate = "update  PROC_EmployeePerformanceReport set ";
                        Strsqlupdate += " DeptManager='" + DeptManager + "'";
                        Strsqlupdate += "  ,DeptManagerDate='" + DeptApproveDate + "'";
                       
                        Strsqlupdate += "  ,SDeptManager='" + SManager + "'";
                        Strsqlupdate += "  ,SDeptManagerDate='" + SApproveDate + "'";
                        Strsqlupdate += "  ,ApplicantFeedDate='" + AFeedBackDate + "'";
                        Strsqlupdate += "  ,GMApprove='" + GMApprove + "'";
                        Strsqlupdate += "  ,GMApproveDate='" + GMApproveDate + "'";
                        Strsqlupdate += "  ,HRApprove='" + HRApprove + "'";
                        Strsqlupdate += "  ,HRApproveDate='" + HRApproveDate + "'";
                        Strsqlupdate += "  ,ProcessStatus='" + Status + "'";
                        Strsqlupdate += " where incident=" + Incident;
                        DataAccess.Instance("BizDB").ExecuteNonQuery(Strsqlupdate);
                    }
                }
                else//新增数据
                {
                    string Strql = "select APPLICANT,APPLICANTACCOUNT,REQUESTDATE,Year,ReportType,EmployeeName,DEPARTMENT from  dbo.PROC_EmployeePerformance where INCIDENT=" + Incident;
                    DataTable BData = DataAccess.Instance("BizDB").ExecuteDataTable(Strql);
                    if(BData.Rows.Count>0)
                    {
                        DataRow dr = BData.Rows[0];
                        string APPLICANT = dr["APPLICANT"].ToString();
                        string APPLICANTACCOUNT = dr["APPLICANTACCOUNT"].ToString();
                        string REQUESTDATE = dr["REQUESTDATE"].ToString();
                        string Year = dr["Year"].ToString();
                        string ReportType = dr["ReportType"].ToString();
                        string EmployeeName = dr["EmployeeName"].ToString();
                        string DocumentNo = item["SUMMARY"].ToString().Trim();
                        string DEPARTMENT = dr["DEPARTMENT"].ToString().Trim();



                        string StrsqlInsert = @"INSERT INTO [PROC_EmployeePerformanceReport]
           ([Incident] ,[DocumentNo] ,[AppicantAccount],[Applicant],[ReportYear]
           ,[ReportType] ,[RequestDate] ,[DeptManager] ,[DeptManagerDate] ,[SDeptManager]  ,[SDeptManagerDate]
           ,[ApplicantFeedDate] ,[GMApprove]  ,[GMApproveDate]  ,[HRApprove]  ,[HRApproveDate],[ProcessStatus] ,[CreateDate],DEPARTMENT)  VALUES(";
                        StrsqlInsert += Incident;
                        StrsqlInsert += ",'" + DocumentNo+"'";
                        StrsqlInsert += ",'" + APPLICANTACCOUNT + "'";
                        StrsqlInsert += ",'" + APPLICANT + "'";
                        StrsqlInsert += ",'" + Year + "'";
                        StrsqlInsert += ",'" + ReportType + "'";
                        StrsqlInsert += ",'" + REQUESTDATE + "'";
                        StrsqlInsert += ",'" + DeptManager + "'";
                        StrsqlInsert += ",'" + (DeptApproveDate.IsNullOrEmpty() ? null : DeptApproveDate) + "'";
                        StrsqlInsert += ",'" + SManager + "'";
                        StrsqlInsert += ",'" + (SApproveDate.IsNullOrEmpty() ? null : SApproveDate) + "'";
                        StrsqlInsert += ",'" + (AFeedBackDate.IsNullOrEmpty() ? null : AFeedBackDate) + "'";
                        StrsqlInsert += ",'" + GMApprove + "'";
                        StrsqlInsert += ",'" + (GMApproveDate.IsNullOrEmpty() ? null : GMApproveDate) + "'";
                        StrsqlInsert += ",'" + HRApprove + "'";
                        StrsqlInsert += ",'" + (HRApproveDate.IsNullOrEmpty() ? null : HRApproveDate) + "'";
                        StrsqlInsert += ",'" + Status + "'";
                        StrsqlInsert += ",'" + DateTime.Now.ToString() + "'";
                        StrsqlInsert += ",'" + DEPARTMENT + "'";
                        StrsqlInsert += ")";

                        if (Status == "1" || Status == "2")
                            DataAccess.Instance("BizDB").ExecuteNonQuery(StrsqlInsert);
                    }
                }

            }
        }

        public List<UserInfo> GetUserNamebyAccount()
        {
            string Strsql = "select USERNAME,EXT04,REPLACE(LOGINNAME,'\\','/') LOGINNAME,EMAIL from ORG_USER  ";
           List< UserInfo> ListUserData=DataAccess.Instance("BizDB").ExecuteList<UserInfo>(Strsql);
            return ListUserData;
        }

        protected override void OnStop()
        {

        }

    }
    public class UserInfo {
        public string USERNAME { get; set; }
        public string EXT04 { get; set; }
        public string LOGINNAME { get; set; }
        public string EMAIL { get; set; }
    }
}
