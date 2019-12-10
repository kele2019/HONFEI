using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using MyLib;
using System.ComponentModel;

namespace Presale.Process.VoluntaryResignation
{
	public partial class HRFinish : System.Web.UI.Page
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
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			DataAccess.Instance("BizDB").ExecuteNonQuery("update PROC_VoluntaryResignation set ProcessStatus='2' where FORMID = '" + userInfo.FormId.Trim() + "'");
		}
	}
}