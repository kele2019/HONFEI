using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Presale.Process.Common;
using Ultimus.UWF.Form.ProcessControl;
using System.ComponentModel;

namespace Presale.Process.EmployeePerformanceReport
{
	public partial class YearEndApproval : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{

			}
			((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
		}
		private void NewRequest_AfterSubmit(object sender, System.ComponentModel.CancelEventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			DataAccess.Instance("BizDB").ExecuteNonQuery("update PROC_EmployeePerformance set EPStatus='2' where FORMID = '" + userInfo.FormId.Trim() + "'");
		}
	}
}