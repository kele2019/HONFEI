using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presale.Process.ITChangeRequest.Entity;
using MyLib;

namespace Presale.Process.ITChangeRequest
{
	public partial class ITAdminApproval : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				
			}
			string sql3 = "select EXT04 from dbo.ORG_USER where loginname = '" + Page.User.Identity.Name + "'";
			UserName departmentUser = DataAccess.Instance("BizDB").ExecuteEntity<UserName>(sql3);
			ITAdmin.Text = departmentUser.EXT04;
		}
	}
}