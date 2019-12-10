using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Presale.Process.ITChangeRequest.Entity;

namespace Presale.Process.ITChangeRequest
{
	public partial class DepartmentManagerApproval : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				//string sql2 = "select EXT04 from dbo.ORG_USER where loginname = '" + Page.User.Identity.Name + "'";
				//UserName approvalUser = DataAccess.Instance("BizDB").ExecuteEntity<UserName>(sql2);
				//fld_departmentUser.Text = approvalUser.EXT04;
			}
		}
	}
}