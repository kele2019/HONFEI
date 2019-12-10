using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Presale.Process.HongfeiFilghtControl.Entity;

namespace Presale.Process.HongfeiFilghtControl
{
	public partial class Approval : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string sql2 = "select EXT04 from dbo.ORG_USER where loginname = '" + Page.User.Identity.Name + "'";
				UserName departmentManager = DataAccess.Instance("BizDB").ExecuteEntity<UserName>(sql2);
				fld_UserName.Text = departmentManager.EXT04;
			}
		}
	}
}