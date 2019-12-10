using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data;
using System.ComponentModel;
using MyLib;
using Ultimus.UWF.Form.ProcessControl;
using System.Collections;
using Presale.Process.Common;
using Presale.Process.HongFeiJVAccess.Entity;

namespace Presale.Process.HongFeiJVAccess
{
	public partial class NewRequest1 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			object ProcessName = Request.QueryString["Processname"];
			object myRequest = Request.QueryString["Type"];
			
			if (!IsPostBack)
			{
				if (myRequest != null) 
				{
					if (myRequest.ToString().ToUpper() == "NEWREQUEST")
					{
						UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
						userInfo.AddNewRow(fld_detail_PROC_HongFeiJVAccess_DT);
					}	
				}
				string Incident = Request.QueryString["Incident"];
				if (Incident != "0")
				{
					hdIncident.Value = Incident;
				}
				if (Incident.ToString() != "0")
				{
					string FlagStatus = DataAccess.Instance("BizDB").ExecuteScalar("select  status from PROC_HongFeiJVAccess where INCIDENT='" + Incident + "' ").ToString();

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
				//fld_deptLogin.Text = DataAccess.Instance("BizDB").ExecuteScalar("select Replace(EXT02,'\\','/')+'|USER'  from dbo.ORG_USER where loginname = '" + Page.User.Identity.Name + "'").ToString();
                //fld_ApplierLogin.Text = Page.User.Identity.Name.Replace("\\", "/") + "|USER";
                //fld_ITMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='IT Manager'").ToString()).Replace("\\", "/");
                //fld_ITHelpLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='IT Specialist'").ToString()).Replace("\\", "/");
                //fld_CTOLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT06 ='Special'").ToString()).Replace("\\", "/");
                //fld_DGMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='DGM'").ToString()).Replace("\\", "/");
                //fld_GMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='GM'").ToString()).Replace("\\", "/");
                //fld_Special.Text = DataAccess.Instance("BizDB").ExecuteScalar("select EXT04 from dbo.ORG_USER where EXT06 ='Special'").ToString();
				string sql = "select A.EXT04 as EXT04 from dbo.ORG_USER A left join dbo.ORG_USER B on A.LOGINNAME = B.EXT02 where B.loginname = '" + Page.User.Identity.Name + "'";
				DepartmentManager manager = DataAccess.Instance("BizDB").ExecuteEntity<DepartmentManager>(sql);
				fld_departmentManager.Text = manager.EXT04;
				string sql2 = "select EXT03 from dbo.org_user where loginname = '" + Page.User.Identity.Name + "'";
				UserPost user = DataAccess.Instance("BizDB").ExecuteEntity<UserPost>(sql2);
				fld_UserPost.Text = user.EXT03;
				string sql3 = "select EXT04 from dbo.org_user where loginname = '" + Page.User.Identity.Name + "'";
				Applier applier = DataAccess.Instance("BizDB").ExecuteEntity<Applier>(sql3);
				fld_applier.Text = applier.EXT04;
			}
            ((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
        }
        protected void NewRequest_BeforeSubmit(object sender, System.ComponentModel.CancelEventArgs g)
        {
            #region
            object Incident = Request.QueryString["Incident"];
            object ProcessName = Request.QueryString["Processname"];
            string strRevoke = string.Format("update PROC_REVOKE set status=1,OperatorDate=getdate() where  ProcessName='{0}' and incident='{1}'", ProcessName, Incident);
            DataAccess.Instance("BizDB").ExecuteNonQuery(strRevoke);
            #endregion
        }
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.AddNewRow(fld_detail_PROC_HongFeiJVAccess_DT);
		}
		protected void fld_detail_PROC_HongFeiJVAccess_DT_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.DeleteRow(fld_detail_PROC_HongFeiJVAccess_DT, e);
		}
		public void fld_detail_PROC_HongFeiJVAccess_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{

			}
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			try
			{
				string tablename = fld_detail_PROC_HongFeiJVAccess_DT.ID.Replace("fld_detail_", "").Replace("read_detail_", "");
				UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
				DataTable dt = userInfo.GetDetailData(fld_detail_PROC_HongFeiJVAccess_DT);
				foreach (RepeaterItem item in fld_detail_PROC_HongFeiJVAccess_DT.Items)
				{
					HtmlInputCheckBox cb = item.FindControl("cb_SelectValue") as HtmlInputCheckBox;
					if (cb.Checked)
					{
						foreach (DataRow row in dt.Rows)
						{
							//if (row["ROWID"].ToString() == cb.Value)
							//{
								row.Delete();
								dt.AcceptChanges();
								break;
							//}
						}
					}
				}
				fld_detail_PROC_HongFeiJVAccess_DT.DataSource = dt;
				fld_detail_PROC_HongFeiJVAccess_DT.DataBind();
			}
			catch (Exception ex)
			{
				MyLib.LogUtil.Error(ex);
				//Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert(\"" + ex.Message.Replace("\r\n", " ").Replace("\n", "").Replace("'", "") + "\");</script>");
			}
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