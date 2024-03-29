﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using System.ComponentModel;
using MyLib;

namespace Presale.Process.CompanySeal
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
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			string FormID = userInfo.FormId;
			string strinsert = string.Format("update PROC_CompanySeal set status='2' where formid='" + FormID + "'");
			DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert);
		}
	}
}