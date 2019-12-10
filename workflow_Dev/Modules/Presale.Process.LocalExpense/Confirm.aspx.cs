﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using System.ComponentModel;
using MyLib;

namespace Presale.Process.LocalExpense
{
	public partial class Confirm : System.Web.UI.Page
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

		}
		protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
		{
			try
			{
				UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
				string FormID = userInfo.FormId;
				ApprovalHistory approvalHistory = Page.FindControl("ApprovalHistory1") as ApprovalHistory;
				int ActiontType = approvalHistory.ActionType;
				if (ActiontType == 0)//成功
				{
					string strinsert1 = string.Format("update PROC_LocalExpense set status='2' where formid='" + FormID + "'");
					DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert1);
                    string strinsert = string.Format("update PROC_Purchase set PurchaseOrdStatus = 2 where DOCUMENTNO='" + read_PurchaseOrderNo.Text + "'");
					DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert);
				}
				else
				{
                    string strinsert = string.Format("update PROC_Purchase set PurchaseOrdStatus = 0 where DOCUMENTNO='" + read_PurchaseOrderNo.Text + "'");
					DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert);
				}
			}
			catch
			{

			}
		}
	}
}