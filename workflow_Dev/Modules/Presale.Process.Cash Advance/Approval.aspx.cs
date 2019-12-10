using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using System.ComponentModel;
using MyLib;

namespace Presale.Process.Cash_Advance
{
    public partial class Approval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Ultimus.UWF.V8.Implementation.UltimusTask ut = new Ultimus.UWF.V8.Implementation.UltimusTask();
            //string TaskID = Request.QueryString["TaskID"].ToString();
            //System.Collections.Hashtable hs = ut.LoadTask(TaskID);
            //foreach (System.Collections.DictionaryEntry item in hs)
            //{

            //}
            ((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
        }
        protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
        {
             ApprovalHistory approvalHistory = Page.FindControl("ApprovalHistory1") as ApprovalHistory;
            int ActiontType = approvalHistory.ActionType;
            string StepName = Request.QueryString["StepName"].ToString();
            if (StepName.Trim() == "Treature Director")
            {
                if (ActiontType == 0)//流程结束
                {
                    string Incident = Request.QueryString["Incident"].ToString();
                    DataAccess.Instance("BizDB").ExecuteNonQuery("update PROC_CashAdvance set status=2 where INCIDENT='"+Incident+"' ");
                }
            }
        }
    }
}