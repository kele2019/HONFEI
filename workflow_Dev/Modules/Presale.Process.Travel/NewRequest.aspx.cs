using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using MyLib;
using Presale.Process.Travel.Entity;
using System.ComponentModel;
using Presale.Process.Common;

namespace Presale.Process.Travel
{
	public partial class NewRequest : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				object ProcessName = Request.QueryString["Processname"];
				object myRequest = Request.QueryString["Type"];
				string Incident = Request.QueryString["Incident"];
				if (Incident != "0")
				{
					hdIncident.Value = Incident;
				}
				if (Incident.ToString() != "0")
				{
					string FlagStatus = DataAccess.Instance("BizDB").ExecuteScalar("select  status from PROC_Travel where INCIDENT='" + Incident + "' ").ToString();

					if (FlagStatus == "1" && int.Parse(Incident.ToString()) > 0 && myRequest.ToString().ToUpper() == "MYREQUEST")
					{
						hdUrgeTask.Value = "Yes";
						string strRevoke = string.Format("select status from  dbo.PROC_REVOKE with(nolock) where ProcessName='{0}' and incident='{1}'", ProcessName, Incident);
						object FlagRevoke = DataAccess.Instance("BizDB").ExecuteScalar(strRevoke);
						if (FlagRevoke != null)
						{
							if (FlagRevoke.ToString() == "1")
							{
								btnRevoke.Visible = true;
							}

						}
						object Requestor = Request.QueryString["Requestor"];
						if (Requestor != null)
						{
							string CurrentUser = ConfigurationManager.AppSettings["Domain"] + "\\" + Requestor.ToString();
							if (Page.User.Identity.Name.ToLower() == CurrentUser.ToLower())
							{

								btnRevoke.Visible = false;
							}
						}
					}
					else
					{

					}
				}
                fld_DGMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select IDNO from dbo.ORG_USER where loginname = '" + Page.User.Identity.Name + "' ").ToString());
                //fld_GMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='GM'").ToString()).Replace("\\", "/");
                //fld_deptLogin.Text = DataAccess.Instance("BizDB").ExecuteScalar("select Replace(EXT02,'\\','/')+'|USER'  from dbo.ORG_USER where loginname = '" + Page.User.Identity.Name + "'").ToString();
				RateEntity rate = DataAccess.Instance("BizDB").ExecuteEntity<RateEntity>("select DicText,DicValue from COM_DICTIONRY where Type = 'Currency' and DicText='USD'");
				fld_Rate.Text = rate.DicValue;
			
				//string sql = "select EXT03 from dbo.org_user where loginname = '" + Page.User.Identity.Name + "'";
				//ApprovalUserPost user = DataAccess.Instance("BizDB").ExecuteEntity<ApprovalUserPost>(sql);
				//fld_USERPOST.Text = user.EXT03;
			}
            ((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
		}
		protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
		{
            #region
            object Incident = Request.QueryString["Incident"];
            object ProcessName = Request.QueryString["Processname"];
            string strRevoke = string.Format("update PROC_REVOKE set status=1,OperatorDate=getdate() where  ProcessName='{0}' and incident='{1}'", ProcessName, Incident);
            DataAccess.Instance("BizDB").ExecuteNonQuery(strRevoke);
            #endregion
			string sql = "select EXT03 from dbo.org_user where loginname = '" + Page.User.Identity.Name + "'";
			ApprovalUserPost user = DataAccess.Instance("BizDB").ExecuteEntity<ApprovalUserPost>(sql);
			fld_USERPOST.Text = user.EXT03;
		}

		protected void fld_detail_PROC_TravelOne_DT_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.DeleteRow(fld_detail_PROC_TravelOne_DT, e);
		}

		protected void fld_detail_PROC_TravelOne_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{

			}
		}

		protected void btnAdd_Click(object sender, EventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.AddNewRow(fld_detail_PROC_TravelOne_DT);
		}

		protected void fld_detail_PROC_TravelSecond_DT_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.DeleteRow(fld_detail_PROC_TravelSecond_DT, e);
		}

		protected void fld_detail_PROC_TravelSecond_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{

			}
		}

		protected void btnAddPurpost_Click(object sender, EventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.AddNewRow(fld_detail_PROC_TravelSecond_DT);
		}
		protected void btnRevoke_Click(object sender, EventArgs e)//撤销
		{
			object ProcessName = Request.QueryString["Processname"];
			object Incident = Request.QueryString["Incident"];
			object StepName = Request.QueryString["StepName"];
			string strRevoke = string.Format("select status from  dbo.PROC_REVOKE with(nolock) where ProcessName='{0}' and incident='{1}'", ProcessName.ToString(), Incident.ToString());
			string FlagRevoke = DataAccess.Instance("BizDB").ExecuteScalar(strRevoke).ToString();
			if (FlagRevoke != "2")
			{
				if (GetOrgLevel.RevokeFunc(ProcessName.ToString(), StepName.ToString(), Incident.ToString(), Page.User.Identity.Name))
				{
					Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "RevokSuccess()", true);

				}
				else
				{
					MessageBox.Show(this.Page, "撤回失败！\\nRevoke Faile!");
				}
			}
			else
			{
				MessageBox.Show(this.Page, "任务已经被处理，无法撤回！\\n Task Already Pass, Don't Revoke!");
			}
		}
	}
}