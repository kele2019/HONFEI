using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Ultimus.UWF.Form.ProcessControl;
using Presale.Process.EmployeePerformanceReport.Entity;
using System.ComponentModel;


namespace Presale.Process.EmployeePerformanceReport
{
	public partial class YearEndDepartmentApproval : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{

			}
			((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
		}
		protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
		{
			string sql = "select EXT02 from dbo.org_user where loginname = '" + Page.User.Identity.Name + "'";
			UserManager manager = DataAccess.Instance("BizDB").ExecuteEntity<UserManager>(sql);
			fld_SecondManagerLogin.Text = manager.EXT02.Replace("\\","/") + "|USER";
		}
	}
}