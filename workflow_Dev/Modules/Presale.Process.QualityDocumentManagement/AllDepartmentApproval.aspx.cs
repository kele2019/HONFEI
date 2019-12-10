using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using System.ComponentModel;
using System.Collections;
using MyLib;

namespace Presale.Process.QualityDocumentManagement
{
	public partial class AllDepartmentApproval : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {

            ((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
        }
        protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
        {
            Hashtable table = (Hashtable)sender;
            ApprovalHistory approve = Page.FindControl("ApprovalHistory1") as ApprovalHistory;
            int ResultType = approve.ActionType;
            string Incident = Request.QueryString["Incident"];
              string UpdateStep = "select ApprovalArr_CheckDeptManager from PROC_QualityDocumentManagement where INCIDENT='" + Incident + "'";
               object ApprovalArr=DataAccess.Instance("BizDB").ExecuteScalar(UpdateStep);
            if (ResultType == 0)
            {
                string Domain = ConfigurationManager.AppSettings["Domain"];
                string CurrentUser = Page.User.Identity.Name;
                CurrentUser = Page.User.Identity.Name.Replace(@"\",@"/")+ "|USER";
                string StepInfo="";
              
               if (ApprovalArr != null)
               {
                   for (int i = 0; i < ApprovalArr.ToString().TrimEnd(',').Split(',').Length; i++)
                   {
                       if (CurrentUser.ToLower() != ApprovalArr.ToString().Split(',')[i].ToLower())
                           StepInfo += ApprovalArr.ToString().Split(',')[i]+",";
                   }
               }
               UpdateStep = "  update PROC_QualityDocumentManagement set ApprovalArr_CheckDeptManager='" + StepInfo.TrimEnd(',') + "' where INCIDENT='" + Incident + "'";
               DataAccess.Instance("BizDB").ExecuteNonQuery(UpdateStep);
               table.Add("ApprovalArr_CheckDeptManager", StepInfo.TrimEnd(','));
            }
            else
                table.Add("ApprovalArr_CheckDeptManager", ApprovalArr.ToString());
        }
	}
}