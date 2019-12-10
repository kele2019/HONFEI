using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.CompilerServices;
using MyLib;
namespace Presale.Process.NewEmployee
{
	public partial class PMApproval : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            object PMobj=DataAccess.Instance("BizDB").ExecuteScalar("select UserName from ORG_ROLEINFO where  RoleforOnboard='PM'");
            if (PMobj != null)
            {
                lbPMUser.Text = PMobj.ToString();
            }
           

          //  ((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
        }
        //protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
        //{
//            Hashtable table = (Hashtable)sender;
//            ApprovalHistory approve = Page.FindControl("ApprovalHistory1") as ApprovalHistory;
//            int ResultType = approve.ActionType;
//            string Incident = Request.QueryString["Incident"];
//            if (ResultType == 0)
//            {
//                string UpdateStep = "update PROC_NewEmployee set Trainer4=null where INCIDENT='" + Incident + "'";
//                DataAccess.Instance("BizDB").ExecuteNonQuery(UpdateStep);

//                string CheckStep = @"select  COUNT(1) from PROC_NewEmployee
//where INCIDENT=" + Incident + " and  Trainer1 is null and Trainer2 is null and Trainer3 is null and Trainer4 is null  and Trainer5 is null  and Trainer6 is null and Trainer7 is null";
//                int Rowcount = Convert.ToInt32(DataAccess.Instance("BizDB").ExecuteScalar(CheckStep));
//                if (Rowcount > 0)
//                {
//                    table.Add("IsCompleted", "isCompleted");
//                }

//            }
       // }
	}
}