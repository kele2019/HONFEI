using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Ultimus.UWF.Form.ProcessControl;
using System.ComponentModel;
using System.Data;
using System.Text;
using Presale.Process.Common;

namespace Presale.Process.ProcessPerformance
{
	public partial class NewRequest : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				object ProcessName = Request.QueryString["Processname"];
				object myRequest = Request.QueryString["Type"];
				string Incident = Request.QueryString["Incident"].ToString() ;
				if (Incident != "0")
				{
					hdIncident.Value = Incident;
				}
				if (Incident.ToString() != "0")
				{
                    object FlagStatus = DataAccess.Instance("BizDB").ExecuteScalar("select  status from PROC_ProcessPerformance where INCIDENT='" + Incident + "' ");
                    if(FlagStatus!=null)
                    {
                        if (FlagStatus.ToString() == "1" && int.Parse(Incident.ToString()) > 0 && myRequest.ToString().ToUpper() == "MYREQUEST")
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
                        hdUrgeTask.Value = "No";
					}
                    }
				}
                fld_deptITLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='IT Manager'").ToString()).Replace("\\", "/");
                fld_deptPMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='PM'").ToString()).Replace("\\", "/");
                fld_DGMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='DGM'").ToString()).Replace("\\", "/");
                fld_deptAdminLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='Admin Assistant'").ToString()).Replace("\\", "/");
                fld_deptQELogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='QAM'").ToString()).Replace("\\", "/");
                fld_deptHRMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='HRM'").ToString()).Replace("\\", "/");
                fld_CTOLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='CTO'").ToString()).Replace("\\", "/");
                fld_deptSupplierMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='Supply Chain Manager'").ToString()).Replace("\\", "/");
                fld_QAMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='QAM1'").ToString()).Replace("\\", "/");
                //fld_deptITLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='IT Manager'").ToString()).Replace("\\", "/");
                //fld_deptPMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='PM'").ToString()).Replace("\\", "/");
                //fld_DGMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='DGM'").ToString()).Replace("\\", "/");
                //fld_deptAdminLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='Admin Assistant'").ToString()).Replace("\\", "/");
                //fld_deptQELogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='QAM'").ToString()).Replace("\\", "/");
                //fld_deptHRMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='HRM'").ToString()).Replace("\\", "/");
                //fld_CTOLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='CTO'").ToString()).Replace("\\", "/");
                //fld_deptSupplierMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='Supply Chain Manager'").ToString()).Replace("\\", "/");
                //fld_QAMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select UserAccount+'|USER' from dbo.ORG_ROLEINFO where RoleName ='QAM1'").ToString()).Replace("\\", "/");
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
		//private void repeatebind(）

		//{
		//    DataTable dtUserInfo = DataAccess.Instance("BizDB").ExecuteDataSet("select DEPARTMENTNAME FROM ORG_DEPARTMENT where PARENTID=1 or PARENTID=2").Tables[0];
		//    DataTable UserData = new DataTable();
		//    UserData.Columns.Add("USER1", typeof(string));
		//    UserData.Columns.Add("USER2", typeof(string));
		//    UserData.Columns.Add("USER3", typeof(string));
		//    if (dtUserInfo.Rows.Count > 0)
		//    {
		//        int UserCount = dtUserInfo.Rows.Count;
		//        for (int i = 0; i < UserCount; i++)
		//        {
		//            UserData.Rows.Add(UserData.NewRow());
		//            DataRow DRUSER = UserData.Rows[(i / 3)];
		//            if ((UserCount - 1) > i) DRUSER["USER1"] = dtUserInfo.Rows[i]["DEPARTMENTNAME"];
		//            if ((UserCount - 1) > i) DRUSER["USER2"] = dtUserInfo.Rows[++i]["DEPARTMENTNAME"];
		//            if ((UserCount - 1) > i) DRUSER["USER3"] = dtUserInfo.Rows[++i]["DEPARTMENTNAME"];
		//        }
		//    }
		//    Repeaterlist.DataSource = UserData;
		//    Repeaterlist.DataBind();

		//}
		//protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
		//{
		//    StringBuilder post = new StringBuilder();
		//    foreach (RepeaterItem item in Repeaterlist.Items)
		//    {
		//        CheckBox User1 = (item.FindControl("USER1") as CheckBox);
		//        CheckBox User2 = ((CheckBox)item.FindControl("USER2"));
		//        CheckBox User3 = ((CheckBox)item.FindControl("USER3"));
		//        if (User1.Checked)
		//        {
		//            post.Append(User1.Text.ToString() + ",");
		//        }
		//        if (User2.Checked)
		//        {
		//            post.Append(User2.Text.ToString() + ",");
		//        }
		//        if (User3.Checked)
		//        {
		//            post.Append(User3.Text.ToString() + ",");
		//        }
		//    }
		//    string departstr = post.ToString();
		//    Array depatarray = post.ToString().Split(',');
		//    foreach (string department in depatarray) {
		//        if (department == "Management")
		//        {
		//            fld_departmentManagement.Text = "yes";
		//        }
		//        if (department == "IT")
		//        {
		//            fld_departmentIT.Text = "yes";
		//        }
		//        if (department == "HR")
		//        {
		//            fld_departmentHR.Text = "yes";
		//        }
		//        if (department == "Engineering")
		//        {
		//            fld_departmentEngineering.Text = "yes";
		//        }
		//        if (department == "Finance")
		//        {
		//            fld_departmentFinance.Text = "yes";
		//        }
		//        if (department == "Quality")
		//        {
		//            fld_departmentQuality.Text = "yes";
		//        }
		//        if (department == "Administration")
		//        {
		//            fld_departmentAdministration.Text = "yes";
		//        }
		//        if (department == "Purchase")
		//        {
		//            fld_departmentPurchase.Text = "yes";
		//        }
		//        if (department == "Supply Chain")
		//        {
		//            fld_departmentSupplyChain.Text = "yes";
		//        }
		//    }
		//}
	}
}