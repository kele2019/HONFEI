using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Presale.Process.AssetBorrowing.Entity;
using Ultimus.UWF.Form.ProcessControl;
using System.ComponentModel;

namespace Presale.Process.AssetBorrowing
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
			string sql3 = "select EXT04 from dbo.ORG_USER where loginname = '" + Page.User.Identity.Name + "'";
			UserName departmentUser = DataAccess.Instance("BizDB").ExecuteEntity<UserName>(sql3);
			fld_UserName.Text = departmentUser.EXT04;
		}
		protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
		{

		}
	}
}