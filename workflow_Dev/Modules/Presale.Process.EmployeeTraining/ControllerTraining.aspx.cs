using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using System.Text;
using Presale.Process.Common;
using MyLib;
namespace Presale.Process.EmployeeTraining
{
    public partial class ControllerTraining : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
        }

        protected void NewRequest_BeforeSubmit(object sender, System.ComponentModel.CancelEventArgs g)
        {
            object Incident = Request.QueryString["Incident"];
            if (Incident != null)
            {
                DataAccess.Instance("UltDB").ExecuteNonQuery(" update  TASKS set STATUS=3 where PROCESSNAME='Employee Training management' and INCIDENT='" + Incident + "' and  STEPLABEL='Trainning'");
            }
        }
    }
}