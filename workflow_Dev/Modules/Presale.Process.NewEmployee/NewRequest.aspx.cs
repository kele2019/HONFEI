using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Presale.Process.Common;
using Ultimus.UWF.Form.ProcessControl;
using System.ComponentModel;
using System.Collections;


namespace Presale.Process.NewEmployee
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
                     string FlagStatus = DataAccess.Instance("BizDB").ExecuteScalar("select  status from PROC_NewEmployee where INCIDENT='" + Incident + "' ").ToString();

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
				domin.Text = ConfigurationManager.AppSettings["Domain"] + "/";
				bindingList();

                //fld_HRLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='HRM'").ToString()).Replace("\\", "/");
                //fld_ITMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='IT Manager'").ToString()).Replace("\\", "/");
                //fld_AdminLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='Admin Assistant'").ToString()).Replace("\\", "/");
                //fld_CTOLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='CTO'").ToString()).Replace("\\", "/");
                //fld_FINMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='CFO'").ToString()).Replace("\\", "/");
                //fld_PMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='PM'").ToString()).Replace("\\", "/");
                //fld_QMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='QAM'").ToString()).Replace("\\", "/");
                //fld_PURMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='Supply Chain Manager'").ToString()).Replace("\\", "/");

                fld_HRLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select B.LOGINNAME+'|USER' from  dbo.ORG_ROLEINFO A LEFT JOIN ORG_USER B ON A.UserAccount=B.LOGINNAME WHERE A.RoleforOnboard='HRM'").ToString()).Replace("\\", "/");
                fld_ITMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select B.LOGINNAME+'|USER' from  dbo.ORG_ROLEINFO A LEFT JOIN ORG_USER B ON A.UserAccount=B.LOGINNAME WHERE A.RoleforOnboard='IT'").ToString()).Replace("\\", "/");
                fld_AdminLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select B.LOGINNAME+'|USER' from  dbo.ORG_ROLEINFO A LEFT JOIN ORG_USER B ON A.UserAccount=B.LOGINNAME WHERE A.RoleforOnboard='Admin'").ToString()).Replace("\\", "/");
                fld_CTOLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select B.LOGINNAME+'|USER' from  dbo.ORG_ROLEINFO A LEFT JOIN ORG_USER B ON A.UserAccount=B.LOGINNAME WHERE A.RoleforOnboard='Engineer'").ToString()).Replace("\\", "/");
                fld_FINMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select B.LOGINNAME+'|USER' from  dbo.ORG_ROLEINFO A LEFT JOIN ORG_USER B ON A.UserAccount=B.LOGINNAME WHERE A.RoleforOnboard='FIN'").ToString()).Replace("\\", "/");
                fld_PMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select B.LOGINNAME+'|USER' from  dbo.ORG_ROLEINFO A LEFT JOIN ORG_USER B ON A.UserAccount=B.LOGINNAME WHERE A.RoleforOnboard='PM'").ToString()).Replace("\\", "/");
                fld_QMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select B.LOGINNAME+'|USER' from  dbo.ORG_ROLEINFO A LEFT JOIN ORG_USER B ON A.UserAccount=B.LOGINNAME WHERE A.RoleforOnboard='QAM'").ToString()).Replace("\\", "/");
                fld_PURMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select B.LOGINNAME+'|USER' from  dbo.ORG_ROLEINFO A LEFT JOIN ORG_USER B ON A.UserAccount=B.LOGINNAME WHERE A.RoleforOnboard='SCM'").ToString()).Replace("\\", "/");
                fld_HSEFLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select B.LOGINNAME+'|USER' from  dbo.ORG_ROLEINFO A LEFT JOIN ORG_USER B ON A.UserAccount=B.LOGINNAME WHERE A.RoleforOnboard='HSEF'").ToString()).Replace("\\", "/");

                //fld_HR.Text = DataAccess.Instance("BizDB").ExecuteScalar("select EXT04 from dbo.ORG_USER where EXT03 ='HRM'").ToString();
                //fld_ITM.Text = DataAccess.Instance("BizDB").ExecuteScalar("select EXT04 from dbo.ORG_USER where EXT03 ='IT Manager'").ToString();
                //fld_AdminORG.Text = DataAccess.Instance("BizDB").ExecuteScalar("select EXT04 from dbo.ORG_USER where EXT03 ='Admin Assistant'").ToString();
                //fld_CTO.Text = DataAccess.Instance("BizDB").ExecuteScalar("select EXT04 from dbo.ORG_USER where EXT03 ='CTO'").ToString();
                //fld_FINM.Text = DataAccess.Instance("BizDB").ExecuteScalar("select EXT04 from dbo.ORG_USER where EXT03 ='CFO'").ToString();
                //fld_PM.Text = DataAccess.Instance("BizDB").ExecuteScalar("select EXT04 from dbo.ORG_USER where EXT03 ='PM'").ToString();
                //fld_QM.Text = DataAccess.Instance("BizDB").ExecuteScalar("select EXT04 from dbo.ORG_USER where EXT03 ='QAM'").ToString();
                //fld_PURM.Text = DataAccess.Instance("BizDB").ExecuteScalar("select EXT04 from dbo.ORG_USER where EXT03 ='Supply Chain Manager'").ToString();
                fld_HR.Text = DataAccess.Instance("BizDB").ExecuteScalar("select B.EXT04 from  dbo.ORG_ROLEINFO A LEFT JOIN ORG_USER B ON A.UserAccount=B.LOGINNAME WHERE A.RoleforOnboard='HRM'").ToString();
                fld_ITM.Text = DataAccess.Instance("BizDB").ExecuteScalar("select B.EXT04 from  dbo.ORG_ROLEINFO A LEFT JOIN ORG_USER B ON A.UserAccount=B.LOGINNAME WHERE A.RoleforOnboard='IT'").ToString();
                fld_AdminORG.Text = DataAccess.Instance("BizDB").ExecuteScalar("select B.EXT04 from  dbo.ORG_ROLEINFO A LEFT JOIN ORG_USER B ON A.UserAccount=B.LOGINNAME WHERE A.RoleforOnboard='Admin'").ToString();
                fld_CTO.Text = DataAccess.Instance("BizDB").ExecuteScalar("select B.EXT04 from  dbo.ORG_ROLEINFO A LEFT JOIN ORG_USER B ON A.UserAccount=B.LOGINNAME WHERE A.RoleforOnboard='Engineer'").ToString();
                fld_FINM.Text = DataAccess.Instance("BizDB").ExecuteScalar("select B.EXT04 from  dbo.ORG_ROLEINFO A LEFT JOIN ORG_USER B ON A.UserAccount=B.LOGINNAME WHERE A.RoleforOnboard='FIN'").ToString();
                fld_PM.Text = DataAccess.Instance("BizDB").ExecuteScalar("select B.EXT04 from  dbo.ORG_ROLEINFO A LEFT JOIN ORG_USER B ON A.UserAccount=B.LOGINNAME WHERE A.RoleforOnboard='PM'").ToString();
                fld_QM.Text = DataAccess.Instance("BizDB").ExecuteScalar("select B.EXT04 from  dbo.ORG_ROLEINFO A LEFT JOIN ORG_USER B ON A.UserAccount=B.LOGINNAME WHERE A.RoleforOnboard='QAM'").ToString();
                fld_PURM.Text = DataAccess.Instance("BizDB").ExecuteScalar("select B.EXT04 from  dbo.ORG_ROLEINFO A LEFT JOIN ORG_USER B ON A.UserAccount=B.LOGINNAME WHERE A.RoleforOnboard='SCM'").ToString();
                fld_HSEF.Text = DataAccess.Instance("BizDB").ExecuteScalar("select B.EXT04 from  dbo.ORG_ROLEINFO A LEFT JOIN ORG_USER B ON A.UserAccount=B.LOGINNAME WHERE A.RoleforOnboard='HSEF'").ToString();
			}
            ((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
        }
        protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
        {

            fld_EmployeeName.Text = dropEmployeeLoginName.SelectedItem.Text;
            fld_EmployeeLoginName.Text = dropEmployeeLoginName.SelectedItem.Value;
            Hashtable table = (Hashtable)sender;
            string domain = ConfigurationManager.AppSettings["Domain"];
            table.Add("NewUser", "USER:org=" + domain + ",user=" + fld_EmployeeLoginName.Text.Replace('\\', '/'));
            #region
            object Incident = Request.QueryString["Incident"];
            object ProcessName = Request.QueryString["Processname"];
            string strRevoke = string.Format("update PROC_REVOKE set status=1,OperatorDate=getdate() where  ProcessName='{0}' and incident='{1}'", ProcessName, Incident);
            DataAccess.Instance("BizDB").ExecuteNonQuery(strRevoke);
            #endregion
        }
		private void bindingList()
		{
			string strSql = "select DepartmentName,DepartmentID from ORG_DEPARTMENT where PARENTID = 1";
			DataTable dtFinaInfo = DataAccess.Instance("BizDB").ExecuteDataTable(strSql);

			dropDepartment.DataTextField = "DepartmentName";
			dropDepartment.DataValueField = "DepartmentID";
			dropDepartment.DataSource = dtFinaInfo;
			dropDepartment.DataBind();

            string strUserSql = "select LOGINNAME,(EXT04+' '+USERNAME) UserName from ORG_USER where ISACTIVE='1'";
            DataTable dtUserList = DataAccess.Instance("BizDB").ExecuteDataTable(strUserSql);
            dropEmployeeLoginName.DataTextField = "UserName";
            dropEmployeeLoginName.DataValueField = "LOGINNAME";
            dropEmployeeLoginName.DataSource = dtUserList;
            dropEmployeeLoginName.DataBind();
            dropEmployeeLoginName.Items.Insert(0, new ListItem("--Pls select--", ""));
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