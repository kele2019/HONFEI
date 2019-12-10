using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using MyLib;
using System.Data;
using Presale.Process.PaymentRequestForm.Entity;
using Presale.Process.Common;
using System.ComponentModel;
using System.Collections;

namespace Presale.Process.PaymentRequestForm
{
	public partial class NewRequest : System.Web.UI.Page
	{
		DataTable TaxList = DataAccess.Instance("BizDB").ExecuteDataTable("select ID + '-' + DESCRIPTION as TaxDisplay,code from dbo.PROC_PURCHASE_TAXCODE");
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
                    string FlagStatus = DataAccess.Instance("BizDB").ExecuteScalar("select  status from PROC_PaymentRequest where INCIDENT='" + Incident + "' ").ToString();
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

                BusniessClass ApproveClass = new BusniessClass();
                DataTable dtuser = ApproveClass.GetUserDOA(Page.User.Identity.Name);
                if (dtuser.Rows.Count > 0)
                {
                    if (dtuser.Rows[0]["DEPARTMENTTYPE"].ToString() == "Dept" && dtuser.Rows[0]["IDNO"].ToString() != "1")
                    {
                        fld_DGMLogin.Text = "0";
                    }
                    else
                    {
                        if (dtuser.Rows[0]["IDNO"].ToString() == "1")
                        {
                            fld_DGMLogin.Text = "1";
                        }
                    } 
                    if (dtuser.Rows[0]["IDNO"].ToString() == "0")
                    {
                        fld_DGMLogin.Text = "";
                    }

                }
               // fld_DGMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select IDNO from dbo.ORG_USER where loginname = '" + Page.User.Identity.Name + "' ").ToString());
                //fld_GMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='GM'").ToString()).Replace("\\", "/");
                //fld_DGMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='DGM'").ToString()).Replace("\\", "/");
                //fld_deptLogin.Text = DataAccess.Instance("BizDB").ExecuteScalar("select Replace(EXT02,'\\','/')+'|USER' from dbo.ORG_USER where loginname = '" + Page.User.Identity.Name + "'").ToString();
                fld_Rate.Text = DataAccess.Instance("BizDB").ExecuteScalar("select DicValue from COM_DICTIONRY where type='Currency' and DicText = 'USD'").ToString();
                //fld_deptName.Text = DataAccess.Instance("BizDB").ExecuteScalar("select d.DEPARTMENTNAME from ORG_USER u  left join  dbo.ORG_JOB j on u.USERID = j.USERID left join ORG_DEPARTMENT d on j.DEPARTMENTID = d.DEPARTMENTID where u.LOGINNAME = '" + Page.User.Identity.Name + "'").ToString();
				//bindingList();
			}
            ((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
            ((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
		}
        protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
        {

            Hashtable table = (Hashtable)sender;
            if(fld_Emergency.Checked)
            table.Add("CONFIDENTIAL","1");
            #region
            object Incident = Request.QueryString["Incident"];
            object ProcessName = Request.QueryString["Processname"];
            string strRevoke = string.Format("update PROC_REVOKE set status=1,OperatorDate=getdate() where  ProcessName='{0}' and incident='{1}'", ProcessName, Incident);
            DataAccess.Instance("BizDB").ExecuteNonQuery(strRevoke);
            #endregion
        }
        protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
        {
            string strinsert = string.Format("update PROC_Purchase set PurchaseOrdStatus = 1 where DOCUMENTNO='" + fld_PurchaseOrderNo.Text + "'");
            DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert);
        }


		protected void btnAdd_Click(object sender, EventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.AddNewRow(fld_detail_PROC_PaymentRequest_DT);
		}

		protected void fld_detail_PROC_PaymentRequest_DT_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.DeleteRow(fld_detail_PROC_PaymentRequest_DT, e);
		}

		protected void fld_detail_PROC_PaymentRequest_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DropDownList tax = e.Item.FindControl("tax") as DropDownList;
				tax.DataSource = TaxList;
				tax.DataTextField = "TaxDisplay";
				tax.DataValueField = "code";
				tax.DataBind();
				tax.Items.Insert(0, new ListItem("--Select--", "0.00"));
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