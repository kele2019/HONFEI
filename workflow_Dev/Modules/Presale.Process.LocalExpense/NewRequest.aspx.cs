using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using Presale.Process.LocalExpense.Entity;
using MyLib;
using System.Data;
using System.ComponentModel;
using Presale.Process.Common;
using System.Collections;


namespace Presale.Process.LocalExpense
{
	public partial class NewRequest : System.Web.UI.Page
	{
		DataTable Description = DataAccess.Instance("BizDB").ExecuteDataTable("select DicText,DicCode from dbo.COM_DICTIONRY where type='LocalExpenseDescription'");

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				object ProcessName = Request.QueryString["Processname"];
				object myRequest = Request.QueryString["Type"];
				string Incident = Request.QueryString["Incident"];
                if (myRequest.ToString().ToUpper() == "NEWREQUEST")
				{
                    UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
                    userInfo.AddNewRow(fld_detail_PROC_LocalExpense_DT);
				}
				if (Incident.ToString() != "0")
				{
                    hdIncident.Value = Incident;
                    string FlagStatus = DataAccess.Instance("BizDB").ExecuteScalar("select  status from PROC_LocalExpense where INCIDENT='" + Incident + "' ").ToString();

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
				//bindingCostCenterList();
				RateEntity rate = DataAccess.Instance("BizDB").ExecuteEntity<RateEntity>("select DicText,DicValue from COM_DICTIONRY where type='Currency' and DicText = 'USD'");
				fld_Rate.Text = rate.DicValue;
                fld_DGMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select IDNO from dbo.ORG_USER where loginname = '" + Page.User.Identity.Name + "' ").ToString());
                //fld_DGMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='DGM'").ToString()).Replace("\\", "/");
                fld_DGM.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select USERCODE from ORG_USER  where loginname = '" + Page.User.Identity.Name + "' ").ToString());
                //fld_DGM.Text = DataAccess.Instance("BizDB").ExecuteScalar("select EXT04 from dbo.ORG_USER where EXT03 ='DGM'").ToString();
                fld_GM.Text = DataAccess.Instance("BizDB").ExecuteScalar("select USERCODE from dbo.ORG_USER where EXT03 ='GM'").ToString();
                //fld_FinLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='CFO'").ToString()).Replace("\\", "/");
                //fld_deptLogin.Text = DataAccess.Instance("BizDB").ExecuteScalar("select Replace(EXT02,'\\','/')+'|USER'  from dbo.ORG_USER where loginname = '" + Page.User.Identity.Name + "'").ToString();
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

              Hashtable table = (Hashtable)sender;
            string Strsql = @"SELECT EXT02 FROM ORG_DEPARTMENT WHERE DEPARTMENTID=(SELECT DEPARTMENTID FROM ORG_JOB WHERE USERID=(sELECT USERID FROM ORG_USER WHERE LOGINNAME='" + Page.User.Identity.Name + "'))";
            object RequestCostcenter=DataAccess.Instance("BizDB").ExecuteScalar(Strsql);
            if(RequestCostcenter!=null)
                if (RequestCostcenter.ToString() != fld_CostCenterValue.Text.Trim())
                {
                    string Strsqlmanager = @"SELECT LOGINNAME+'|USER' FROM ORG_USER WHERE USERID=(select  top(1) DepartmentManager from ORG_DEPARTMENT WHERE EXT02='" + fld_CostCenterValue .Text+ "')";
                    object StrsqlmanagerObj = DataAccess.Instance("BizDB").ExecuteScalar(Strsqlmanager);
                    if(StrsqlmanagerObj!=null)
                        table.Add("BizManager", StrsqlmanagerObj.ToString().Replace('\\','/'));
                }
            if (fld_CostCenterValue.Text.Trim() != "")
            {
                table.Add("CostCenter", fld_CostCenterValue.Text);
            }

            #endregion
		}
		protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
		{
            string strinsert = string.Format("update PROC_Purchase set PurchaseOrdStatus = 1 where DOCUMENTNO='" + fld_PurchaseOrderNo.Text + "'");
			DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert);
		}
		protected void fld_detail_PROC_LocalExpense_DT_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.DeleteRow(fld_detail_PROC_LocalExpense_DT, e);
		}
		//private void bindingCostCenterList()
		//{
		//    DataTable Costcent = DataAccess.Instance("BizDB").ExecuteDataTable("select cosetcenter,cosetcenter+'-'+Description as description from  dbo.PROC_PURCHASE_COSTCENTER ");
		//    costcenter.DataTextField = "description";
		//    costcenter.DataValueField = "cosetcenter";
		//    costcenter.DataSource = Costcent;
		//    costcenter.DataBind();
		//}
		protected void fld_detail_PROC_LocalExpense_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				//DropDownList ItemList = e.Item.FindControl("dropDescription") as DropDownList;
				//ItemList.DataSource = Description;
				//ItemList.DataTextField = "DicText";
				//ItemList.DataValueField = "DicCode";
				//ItemList.DataBind();
			}
		}

        protected void btnAddThird_Click(object sender, EventArgs e)
        {
            UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            userInfo.AddNewRow(fld_detail_PROC_LocalExpenseThird_DT);
        }
        protected void fld_detail_PROC_LocalExpenseThird_DT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            userInfo.DeleteRow(fld_detail_PROC_LocalExpenseThird_DT, e);
        }

        protected void fld_detail_PROC_LocalExpenseThird_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

            }
        }

		protected void btnAdd_Click(object sender, EventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.AddNewRow(fld_detail_PROC_LocalExpense_DT);
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