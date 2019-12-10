using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presale.Process.Badge.Entity;
using MyLib;
using System.ComponentModel;
using Ultimus.UWF.Form.ProcessControl;

namespace Presale.Process.Badge
{
	public partial class Approval : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{

			}
			((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
			((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
		}
		protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
		{
			string sql = "select EXT03 from dbo.org_user where loginname = '" + Page.User.Identity.Name + "'";
			UserPost user = DataAccess.Instance("BizDB").ExecuteEntity<UserPost>(sql);
			fld_UserPost.Text = user.EXT03;
		}
		protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
		{

		}
	}
}