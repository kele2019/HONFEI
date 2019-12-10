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
    public partial class HRComplete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
            ((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);

        }
        protected void NewRequest_BeforeSubmit(object sender, System.ComponentModel.CancelEventArgs g)
        {
               string Incident = Request.QueryString["Incident"];
               string StrsqlUpdateTraining = " update   PROC_EmployeeTraining set STATUS=2,HRAPPROVEDATE=GETDATE() where  incident='" + Incident + "' ";
               //StrsqlUpdateTraining += @"update dbo.COM_EmployeeTrainSignInfo set TrainDate=GETDATE(),SumDate='" + read_TrainingDuration.Text + "'";
               //StrsqlUpdateTraining += @" where SumDate<>'0' and  TrainDocmentNo=(select DOCUMENTNO from PROC_EmployeeTraining where INCIDENT='" + Incident + "') and  UserID IN(select USERID from ORG_USER WHERE LOGINNAME IN(";
               //StrsqlUpdateTraining += @" select EXT01 from  WF_APPROVALHISTORY where PROCESSNAME='Employee Training management' and INCIDENT='" + Incident + "' and STEPNAME='Trainning' and ACTION='Approve'))";

                
                DataAccess.Instance("BizDB").ExecuteNonQuery(StrsqlUpdateTraining);
        }
    }
}