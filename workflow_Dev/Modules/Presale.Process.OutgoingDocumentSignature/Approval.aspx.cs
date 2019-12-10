using System;
using System.Web.UI;
using Ultimus.UWF.Form.ProcessControl;
using Presale.Process.OutgoingDocumentSignature.Entity;
using System.Data;
using MyLib;
using System.ComponentModel;

namespace Presale.Process.OutgoingDocumentSignature
{
	public partial class Approval : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				//string sql = "select EXT04 from dbo.ORG_USER where LOGINNAME = '" + Page.User.Identity.Name + "'";
				//ApprovalUser user = DataAccess.Instance("BizDB").ExecuteEntity<ApprovalUser>(sql);
				//fld_ApprovalUser.Text = user.EXT04;
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