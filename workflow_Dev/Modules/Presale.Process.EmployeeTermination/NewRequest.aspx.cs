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
using Presale.Process.EmployeeTermination.Entity;

namespace Presale.Process.EmployeeTermination
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
					string FlagStatus = DataAccess.Instance("BizDB").ExecuteScalar("select  status from PROC_EmployeeTerminationCheckOut where INCIDENT='" + Incident + "' ").ToString();

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
				bindingList();
				fld_AdminLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='Admin Assistant'").ToString()).Replace("\\", "/");
				fld_FINMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='CFO'").ToString()).Replace("\\", "/");
				fld_HRLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='HRM'").ToString()).Replace("\\", "/");
				fld_ITMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='IT Manager'").ToString()).Replace("\\", "/");
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

            string Strsql=@" SELECT B.LOGINNAME+'|USER' FROM (
 select IDNumber from  PROC_VoluntaryResignation WHERE FORMID='"+fld_TerminationEmployeeValue.Text+"')A LEFT JOIN ORG_USER B ON A.IDNumber=B.USERCODE";
            object TerminationUser=DataAccess.Instance("BizDB").ExecuteScalar(Strsql);
            if (TerminationUser != null)
                fld_TerminationUserLoginName.Text = TerminationUser.ToString().Replace("\\","/");
        }
		//private void bindingList()
		//{
		//    List<UserEntity> dtFinaInfo = DataAccess.Instance("BizDB").ExecuteList<UserEntity>("select FORMID,Applicant,Department,ResignationDate from PROC_VoluntaryResignation where ProcessStatus = '1'");
		//    foreach (UserEntity user in dtFinaInfo) {
		//        dropTerminationEmployee.Items.Add(new ListItem(user.Applicant + "_" + user.Department + "_" + user.ResignationDate.ToString("yyyyMMdd") + "_" + "Voluntary Resignation Application Form"));
		//    }
		//}
		private void bindingList()
		{
			string strSql = "select FORMID,Applicant+'_'+Department+'_'+replace(left(convert(nvarchar(20),ResignationDate,120),11),'-','')+ '_Voluntary Resignation Application Form' as processDetail from PROC_VoluntaryResignation where ProcessStatus = '2'";
			DataTable dtFinaInfo = DataAccess.Instance("BizDB").ExecuteDataTable(strSql);
			dropTerminationEmployee.DataTextField = "processDetail";
			dropTerminationEmployee.DataValueField = "FORMID";
			dropTerminationEmployee.DataSource = dtFinaInfo;
			dropTerminationEmployee.DataBind();
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