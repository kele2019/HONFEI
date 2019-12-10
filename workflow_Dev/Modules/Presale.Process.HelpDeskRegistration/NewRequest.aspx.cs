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


namespace Presale.Process.HelpDeskRegistration
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
					string FlagStatus = DataAccess.Instance("BizDB").ExecuteScalar("select  status from PROC_HelpDeskRegistration where INCIDENT='" + Incident + "' ").ToString();

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
                //fld_ApplierLogin.Text = Page.User.Identity.Name.Replace("\\", "/") + "|USER";
                //fld_ITMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='IT Manager'").ToString()).Replace("\\", "/");
				bindingList();
			}
            ((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);

		}
		private void bindingList()
		{
			string strSql = "select DicCode,DicText from COM_DICTIONRY where type = 'HelpdeskQuestionType'";
			DataTable dtFinaInfo = DataAccess.Instance("BizDB").ExecuteDataTable(strSql);

			questionType.DataTextField = "DicText";
			questionType.DataValueField = "DicCode";
			questionType.DataSource = dtFinaInfo;
			questionType.DataBind();
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
					UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
					string FormID = userInfo.FormId;
					string strinsert = string.Format("exec proc_ALdayRerejeck '" + FormID + "'");
					DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert);
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