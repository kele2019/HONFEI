using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Presale.Process.EmployeeTraining.Entity;
using Ultimus.UWF.Form.ProcessControl;


namespace Presale.Process.EmployeeTraining
{
	public partial class Approval : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string Sql = "select * from (";
				Sql += " select A.USERNAME USERNAME,C.DEPARTMENTNAME DEPARTMENTNAME from dbo.ORG_USER A ";
				Sql += " left join dbo.ORG_JOB B on A.USERID = B.USERID ";
				Sql += " left join dbo.ORG_DEPARTMENT C on B.DEPARTMENTID = C.DEPARTMENTID ";
				Sql += " where A.LOGINNAME = '" + Page.User.Identity.Name + "')E";
				UserIntity user = DataAccess.Instance("BizDB").ExecuteEntity<UserIntity>(Sql);
				EmployeeName.Text = user.USERNAME;
				OBDepartment.Text = user.DEPARTMENTNAME;
			}
            ((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
        }
        protected void NewRequest_AfterSubmit(object sender, System.ComponentModel.CancelEventArgs g)
		{
              string Incident = Request.QueryString["Incident"];
            if (CbNoParticeipate.Checked)
            {
                string Strsql = @" update dbo.COM_EmployeeTrainSignInfo  set SumDate='0' 
  where UserID=(select USERID from ORG_USER where LOGINNAME='" + Page.User.Identity.Name + "') and TrainDocmentNo=(select DOCUMENTNO from  PROC_EmployeeTraining where  incident='" + Incident + "')";
                DataAccess.Instance("BizDB").ExecuteNonQuery(Strsql);
            }
            else
            {
                string StrsqlUpdateTraining = @" update dbo.COM_EmployeeTrainSignInfo  set  TrainDate=GETDATE(),SumDate='" + read_TrainingDuration.Text + "'";
                StrsqlUpdateTraining += @" where UserID=(select USERID from ORG_USER where LOGINNAME='" + Page.User.Identity.Name + "') and TrainDocmentNo=(select DOCUMENTNO from  PROC_EmployeeTraining where  incident='" + Incident + "')";
               
               
                //if (Request.QueryString["StepName"].ToString() == "HR Aprove")
                //{
                //    StrsqlUpdateTraining = @"update dbo.COM_EmployeeTrainSignInfo set TrainDate=GETDATE(),SumDate='" + read_TrainingDuration.Text + "'";
                //    StrsqlUpdateTraining += @" where SumDate<>'-1' and  TrainDocmentNo=(select DOCUMENTNO from PROC_EmployeeTraining where INCIDENT='" + Incident + "') and  UserID IN(select USERID from ORG_USER WHERE LOGINNAME IN(";
                //    StrsqlUpdateTraining += @" select EXT01 from  WF_APPROVALHISTORY where PROCESSNAME='Employee Training management' and INCIDENT='" + Incident + "' and STEPNAME='Trainning' and ACTION='Approve'))";

                //}
                //else
                //{
                //    if (cbtday.Checked)
                //        StrsqlUpdateTraining += "update PROC_EmployeeTraining set EvaluationDays='3',HRAPPROVEDATE=GETDATE() where INCIDENT='" + Incident + "'";
                //    if (cbsday.Checked)
                //        StrsqlUpdateTraining += "update PROC_EmployeeTraining set EvaluationDays='6',HRAPPROVEDATE=GETDATE() where INCIDENT='" + Incident + "'";
                //}
                DataAccess.Instance("BizDB").ExecuteNonQuery(StrsqlUpdateTraining);
            }
		}
	}
}