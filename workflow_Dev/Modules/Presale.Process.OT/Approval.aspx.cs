using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Presale.Process.OT.Entity;
using Ultimus.UWF.Form.ProcessControl;
using System.ComponentModel;
using Presale.Process.Common;

namespace Presale.Process.OT
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
			string sql = "select EXT04 from dbo.org_user where loginname = '" + Page.User.Identity.Name + "'";
			UserName user = DataAccess.Instance("BizDB").ExecuteEntity<UserName>(sql);
			fld_UserName.Text = user.EXT04;
		}
		protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
		{
			try
			{
				UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
                string FormID = userInfo.FormId;
				ApprovalHistory approvalHistory = Page.FindControl("ApprovalHistory1") as ApprovalHistory;
				int ActiontType = approvalHistory.ActionType;
                string ProcessName = Request.QueryString["ProcessName"].ToString();
                string Incident = Request.QueryString["Incident"].ToString();
                BusniessClass ApproveClass = new BusniessClass();
				if (ActiontType == 1)//退回
				{
					//string sql = "Update COM_OTAndDayOffManage set OTHourCount = OTHourCount-Convert(decimal(5,2),isnull('" + read_SumHour.Text + "',0.0)) where UserAccount = '" + read_ApplierLogin.Text + "'";
					//DataAccess.Instance("BizDB").ExecuteNonQuery(sql);
				}
				if (ActiontType == 0)//成功
				{
					//if (fld_UserName.Text == read_HR.Text)
                    string StepName = Request.QueryString["StepName"];
                    if (StepName == "Department manager Approve")
                    {
                        ApproveClass.UpdateReivewStatus(ProcessName, Incident, "HR Review");
                    }
                    if(StepName=="HR Approve")
					{
						//string sql = "Update COM_OTAndDayOffManage set OTHourCount = OTHourCount+Convert(decimal(5,2),isnull('" + read_SumHour.Text + "',0.0)) where UserAccount = '" + read_ApplierLogin.Text + "'";
                        string Strsql = string.Format(@"if exists (select * from COM_OTAndDayOffManage where OTYear='{0}' and UserAccount='{1}')
Update COM_OTAndDayOffManage set OTHourCount = OTHourCount+Convert(decimal(5,2),isnull('{2}',0.0)) where OTYear='{0}' and UserAccount='{1}'
else
insert into COM_OTAndDayOffManage(UserAccount,OTHourCount,OTYear) values('{1}','{2}','{0}')",DateTime.Now.Year,read_ApplierLogin.Text,read_SumHour.Text);
                        DataAccess.Instance("BizDB").ExecuteNonQuery(Strsql);
						string strinsert = string.Format("update PROC_OT set status='2' where formid='" + FormID + "'");
						DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert);
					}
				}
			}
			catch
			{

			}
		}
	}
}