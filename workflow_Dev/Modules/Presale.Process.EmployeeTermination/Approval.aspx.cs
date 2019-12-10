using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Ultimus.UWF.Form.ProcessControl;
using System.ComponentModel;

namespace Presale.Process.EmployeeTermination
{
	public partial class Approval : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
			}
			((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
		}
		protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
		{
			string value = read_TerminationEmployeeValue.Text;
			DataAccess.Instance("BizDB").ExecuteNonQuery("update PROC_VoluntaryResignation set ProcessStatus= '2' where FORMID = '" + value + "';");
		}
	}
}