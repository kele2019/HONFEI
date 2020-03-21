using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using System.ComponentModel;
using Presale.Process.Common;
using MyLib;
using System.Data;

namespace Presale.Process.Leave
{
    public partial class HRReview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);

        }
        protected void NewRequest_AfterSubmit(object j, CancelEventArgs g)
        {
            try
            {
                ApprovalHistory approvalHistory = Page.FindControl("ApprovalHistory1") as ApprovalHistory;
                int ActiontType = approvalHistory.ActionType;
                string ProcessName = Request.QueryString["ProcessName"].ToString();
                string Incident = Request.QueryString["Incident"].ToString();
                BusniessClass ApproveClass = new BusniessClass();
                 if (ActiontType == 1)//退回
                 {
                     UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
                     string FormID = userInfo.FormId;
                     string strinsert = string.Format("exec proc_ALdayRerejeck '" + FormID + "'");
                     DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert);

                     DataTable newdt = DataAccess.Instance("BizDB").ExecuteDataTable("select  ROWID,NoODays  from PROC_Leave_DT where Applying='Full Pay Sick Leave' and formid='" + FormID + "'");//全薪病假计算
                     for (int i = 0; i < newdt.Rows.Count; i++)
                     {
                         string ROWID = newdt.Rows[i]["ROWID"].ToString();
                         string leaves = newdt.Rows[i]["NoODays"].ToString();
                         string backSick = string.Format("update COM_LevalManager set FuallpaySick=FuallpaySick+Convert(decimal(4,2),isnull('" + leaves + "',0.0)) where LeaveYear=DATEPART(YY,GETDATE()) and  UserAccount in (select APPLICANTACCOUNT  from proc_leave where formid='" + FormID + "')");
                         DataAccess.Instance("BizDB").ExecuteNonQuery(backSick);
                     }
                     string StepName = Request.QueryString["StepName"];
                     if (StepName == "HR Review")
                     {
                         ApproveClass.RejectProcessStep(ProcessName, Incident, "Department manager Approve");
                     }
                 }
            }
            catch (Exception ex)
            { 
            
            }
        }
    }
}