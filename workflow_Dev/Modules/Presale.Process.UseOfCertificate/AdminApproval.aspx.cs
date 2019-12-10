using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using System.ComponentModel;
using System.Collections;
using System.Data;
using MyLib;

namespace Presal.Process.UseOfCertificate
{
	public partial class AdminApproval : System.Web.UI.Page
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				object objtype = Request.QueryString["type"];
				if (objtype != null)
				{
					if (objtype.ToString() == "myapproval")
					{
						btnDiv.Visible = false;
					}
					else
					{
						if (objtype.ToString() == "mytask")
						{
							btnDiv.Visible = true;
						}
					}
				}
			}
			((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
			((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
		}
		protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
		{

		}
		protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
		{

		}
	}
}