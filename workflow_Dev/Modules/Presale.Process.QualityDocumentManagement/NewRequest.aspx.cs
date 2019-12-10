using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;
using Ultimus.UWF.Form.ProcessControl;
using System.ComponentModel;
using Presale.Process.QualityDocumentManagement.Entity;
using System.Text;
using Presale.Process.Common;
 
 
namespace Presale.Process.QualityDocumentManagement
{
	  

	public partial class NewRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			  
            if (!IsPostBack)
            {
                string STRUSER = Page.User.Identity.Name;
                object ProcessName = Request.QueryString["Processname"];
                object myRequest = Request.QueryString["Type"];
                object Incident = Request.QueryString["Incident"];

               
               
				if (Incident.ToString() != "0")
				{
                    hdIncident.Value = Incident.ToString();
					string FlagStatus = DataAccess.Instance("BizDB").ExecuteScalar("select  status from PROC_QualityDocumentManagement where INCIDENT='" + Incident + "' ").ToString();

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
                repeatebind();
				bindDocNameList();
                //fld_deptITLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='IT Manager'").ToString()).Replace("\\", "/");
                //fld_deptPMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='PM'").ToString()).Replace("\\", "/");
                //fld_DGMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='DGM'").ToString()).Replace("\\", "/");
                //fld_deptAdminLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='Admin Assistant'").ToString()).Replace("\\", "/");
                //fld_deptQELogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='QAM'").ToString()).Replace("\\", "/");
                //fld_deptHRMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='HRM'").ToString()).Replace("\\", "/");
                //fld_CTOLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='CTO'").ToString()).Replace("\\", "/");
                //fld_deptSupplierMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='Supply Chain Manager'").ToString()).Replace("\\", "/");
                //fld_QAMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='QAM1'").ToString()).Replace("\\", "/");


                fld_QAMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='QAM1'").ToString()).Replace("\\", "/");
                fld_ITMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='IT Manager'").ToString()).Replace("\\", "/");
                fld_HRMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='HRM'").ToString()).Replace("\\", "/");
                fld_CTOLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='CTO'").ToString()).Replace("\\", "/");
                fld_CFOLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='CFO'").ToString()).Replace("\\", "/");
                fld_AdimLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='HSEF Manager'").ToString()).Replace("\\", "/");
                fld_SupplierMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='Supply Chain Manager'").ToString()).Replace("\\", "/");
                fld_PMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='PM'").ToString()).Replace("\\", "/");
                fld_DGMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='DGM'").ToString()).Replace("\\", "/");
                fld_GMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='GM'").ToString()).Replace("\\", "/");
                fld_deptMarketingLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='Marketing Manager'").ToString()).Replace("\\", "/");

            }
			((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
			((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
        }
		protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
		{
            #region
            object Incident = Request.QueryString["Incident"];
            object ProcessName = Request.QueryString["Processname"];
            string strRevoke = string.Format("update PROC_REVOKE set status=1,OperatorDate=getdate() where  ProcessName='{0}' and incident='{1}'", ProcessName, Incident);
            DataAccess.Instance("BizDB").ExecuteNonQuery(strRevoke);
            #endregion
			string sql = "select LOGINNAME,EXT04,EXT02 from dbo.org_user where loginname = '" + Page.User.Identity.Name + "'";
			ApplicantUser user = DataAccess.Instance("BizDB").ExecuteEntity<ApplicantUser>(sql);
			fld_ApplicantUserLogin.Text = user.LOGINNAME.Replace("\\", "/") + "|USER";
			fld_ApplicantUser.Text = user.EXT04;
			fld_ApplicantDeptManager.Text = user.EXT02.Replace("\\","/") + "|USER";


			 
		}
		protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
		{

		}

		private void bindDocNameList()
		{
			
			string sql = "select DepartmentName,DepartmentID from ORG_DEPARTMENT where PARENTID=1";
			DataTable dtFinaInfo = DataAccess.Instance("BizDB").ExecuteDataTable(sql);

			 
		}
        private void repeatebind()
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