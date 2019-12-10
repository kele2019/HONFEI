using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using System.ComponentModel;
using System.Collections;
 
using System.Data;
using MyLib;
using Presale.Process.Common;
namespace Presale.Process.Cash_Advance
{
    public partial class NewRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
				object ProcessName = Request.QueryString["Processname"];
			
                object myRequest = Request.QueryString["Type"];
                object Incident = Request.QueryString["Incident"];
                if (myRequest != null)
                {
                    if (myRequest.ToString().ToUpper() == "NEWREQUEST")
                    {
                        UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
                        userInfo.AddNewRow(fld_detail_PROC_CashAdvance_DT);
                    }
                    if (myRequest.ToString().ToUpper() == "MYTASK" && int.Parse(Incident.ToString()) > 0)
                    {
                        hdIncident.Value = Incident.ToString();
                    }
                }
                if(Convert.ToInt32(Incident)==0)
                {
                    DataTable dtBankInfo = DataAccess.Instance("BizDB").ExecuteDataTable("select top 1 BankNo,BankName from PROC_CashAdvance where APPLICANTACCOUNT='" + Page.User.Identity.Name.Replace("\\","/") + "' and Status=2 order by INCIDENT DESC");
                    if (dtBankInfo.Rows.Count > 0)
                    {
                        fld_BankNo.Text = dtBankInfo.Rows[0]["BankNo"].ToString();
                        fld_BankName.Text = dtBankInfo.Rows[0]["BankName"].ToString();
                    }
                }
				if (Incident.ToString() != "0")
				{
					string FlagStatus = DataAccess.Instance("BizDB").ExecuteScalar("select  status from PROC_CashAdvance where INCIDENT='" + Incident + "' ").ToString();

					if (FlagStatus == "1" && int.Parse(Incident.ToString()) > 0 && myRequest.ToString().ToUpper() == "MYREQUEST")
					{
						hdUrgeTask.Value = "Yes";
						string strRevoke = string.Format("select status from  dbo.PROC_REVOKE with(nolock) where ProcessName='{0}' and incident='{1}'", ProcessName, Incident);
						object FlagRevoke = DataAccess.Instance("BizDB").ExecuteScalar(strRevoke);
						if (FlagRevoke != null)
						{
							if (FlagRevoke.ToString() == "1" || FlagRevoke.ToString() == "3")
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
            }
            ((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
            ((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
        }
        protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
        {
            Hashtable table = (Hashtable)sender;
            
            string domain = ConfigurationManager.AppSettings["Domain"];
            string strsql = "select * from  dbo.COM_DICTIONRY where TYPE='Fina'";
            DataTable dtFinaInfo =DataAccess.Instance("BizDB").ExecuteDataTable(strsql);
            string IsRepater = "-1";
            if (dtFinaInfo.Rows.Count > 0)
            {
                foreach (DataRow dr in dtFinaInfo.Rows)
                {
                    //if (dr["DicValue"].ToString() == Page.User.Identity.Name)
                    //    table.Add("RequestType", "1");

                    //if (dr["DicCode"].ToString() == "FinaKJ")
                    //{
                    //    if (dr["DicValue"].ToString() == Page.User.Identity.Name)
                    //        table.Add("IsKJ", "1");
                    //    else
                    //        table.Add("IsKJ", "0");
                    //}
                    string RoleCode = dr["DicCode"].ToString();
                    //if (RoleCode == "FinaJL")
                    //{
                    //    if (dr["DicValue"].ToString() == Page.User.Identity.Name)
                    //        IsRepater = "1";
                    //}
                    if (RoleCode == "TreatureManager")
                    {
                        if (dr["DicValue"].ToString() == Page.User.Identity.Name)
                            IsRepater = "0";
                    }
                    if (RoleCode == "Treature")
                    {
                        if (dr["DicValue"].ToString() == Page.User.Identity.Name)
                            IsRepater = "2";
                    }
                    switch (RoleCode)
                    {
                        case "TreatureManager":
                            table.Add("FinaJL", "USER:org=" + domain + ",user=" + dr["DicValue"].ToString().Replace('\\', '/'));
                            break;
                        case "Treature":
                            table.Add("FinaZJ", "USER:org=" + domain + ",user=" + dr["DicValue"].ToString().Replace('\\', '/'));
                            break;
                        default:
                            break;
                    }
                }
                table.Add("IsRepater", IsRepater);
            }
        }
        protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
        {
        }

        protected void fld_detail_PROC_CashAdvance_DT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            userInfo.DeleteRow(fld_detail_PROC_CashAdvance_DT, e);
        }

        protected void fld_detail_PROC_CashAdvance_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
         
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            userInfo.AddNewRow(fld_detail_PROC_CashAdvance_DT);
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