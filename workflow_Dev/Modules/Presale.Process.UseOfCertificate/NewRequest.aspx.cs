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
using Presale.Process.UseOfCertificate;

namespace Presale.Process.UseOfCertificate
{
	public partial class NewRequest : System.Web.UI.Page
	{
		DataTable data = new DataTable();
		protected void Page_Load(object sender, EventArgs e)
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
				string FlagStatus = DataAccess.Instance("BizDB").ExecuteScalar("select  status from PROC_UseOfCertificate where INCIDENT='" + Incident + "' ").ToString();

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
            //fld_AdminLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='Admin Assistant'").ToString()).Replace("\\", "/");
            //fld_DGMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='DGM'").ToString()).Replace("\\", "/");
            //fld_GMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='GM'").ToString()).Replace("\\", "/");
			((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
			((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
		}

		//protected void btnAdd_Click(object sender, EventArgs e)
		//{
		//    UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
		//    userInfo.AddNewRow(fld_detail_PROC_UseOfCertificate_DT);
		//}

		//protected void fld_detail_PROC_UseOfCertificate_DT_ItemCommand(object source, RepeaterCommandEventArgs e)
		//{
		//    UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
		//    userInfo.DeleteRow(fld_detail_PROC_UseOfCertificate_DT, e);
		//}
		protected void fld_detail_PROC_UseOfCertificate_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{

			}
		}
		//protected void btnDelete_Click(object sender, EventArgs e)
		//{
		//    try
		//    {
		//        string tablename = fld_detail_PROC_UseOfCertificate_DT.ID.Replace("fld_detail_", "").Replace("read_detail_", "");
		//        UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
		//        DataTable dt = ((DataTable)userInfo.DetailData[tablename]).Clone();
		//        dt = userInfo.GetDetailData(fld_detail_PROC_UseOfCertificate_DT);
		//        foreach (RepeaterItem item in fld_detail_PROC_UseOfCertificate_DT.Items)
		//        {
		//            HtmlInputCheckBox cb = item.FindControl("cb_SelectValue") as HtmlInputCheckBox;
		//            if (cb.Checked)
		//            {
		//                foreach (DataRow row in dt.Rows)
		//                {
		//                    if (row["ROWID"].ToString() == cb.Value)
		//                    {
		//                        row.Delete();
		//                        dt.AcceptChanges();
		//                        break;
		//                    }
		//                }
		//            }
		//        }
		//        fld_detail_PROC_UseOfCertificate_DT.DataSource = dt;
		//        fld_detail_PROC_UseOfCertificate_DT.DataBind();
		//    }
		//    catch (Exception ex)
		//    {
		//        MyLib.LogUtil.Error(ex);
		//        //Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert(\"" + ex.Message.Replace("\r\n", " ").Replace("\n", "").Replace("'", "") + "\");</script>");
		//    }
		//}

		protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
		{
            #region
            object Incident = Request.QueryString["Incident"];
            object ProcessName = Request.QueryString["Processname"];
            string strRevoke = string.Format("update PROC_REVOKE set status=1,OperatorDate=getdate() where  ProcessName='{0}' and incident='{1}'", ProcessName, Incident);
            DataAccess.Instance("BizDB").ExecuteNonQuery(strRevoke);
            #endregion
			 
			string sql = "select EXT03 from dbo.org_user where loginname = '" + Page.User.Identity.Name + "'";
			Presal.Process.UseOfCertificate.Entity.UserPost user = DataAccess.Instance("BizDB").ExecuteEntity<Presal.Process.UseOfCertificate.Entity.UserPost>(sql);
			fld_UserPost.Text = user.EXT03;
		}
		protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
		{

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