using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using System.Data;
using MyLib;
using Presale.Process.TravelExpenseReport.Entity;
using System.ComponentModel;
using Presale.Process.Common;
using System.Collections;
namespace Presale.Process.TravelExpenseReport
{
	public partial class NewRequest : System.Web.UI.Page
	{
		public DataTable Item = DataAccess.Instance("BizDB").ExecuteDataTable("select * from  dbo.COM_DICTIONRY where Type='Item'");
		public DataTable PayMethod = DataAccess.Instance("BizDB").ExecuteDataTable("select * from  dbo.COM_DICTIONRY where Type='PayMethod'");

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string Incident = Request.QueryString["Incident"];
                object ProcessName = Request.QueryString["Processname"];
                object myRequest = Request.QueryString["Type"];
				if (Incident != "0")
				{
					hdIncident.Value = Incident;
				}
                if (Incident.ToString() != "0"&&Incident.ToString() != "-1")
                {
                    object FlagStatus = DataAccess.Instance("BizDB").ExecuteScalar("select  status from PROC_TravelExpense where INCIDENT='" + Incident + "' ");
                    if (FlagStatus != null)
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

                        }
                    }
                }
				//bindingCostCenterList();
				//UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
				//string FormID = userInfo.FormId;
				//status.Text = DataAccess.Instance("BizDB").ExecuteScalar("select Status from PROC_TravelExpense where formID ='" + FormID + "'").ToString();
				RateEntity rate = DataAccess.Instance("BizDB").ExecuteEntity<RateEntity>("select DicValue from COM_DICTIONRY where DicText = 'USD' and type='Currency'");
				status.Text = DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='DGM'").ToString();
				fld_Rate.Text = rate.DicValue;
                fld_DGMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select IDNO from dbo.ORG_USER where loginname = '" + Page.User.Identity.Name + "' ").ToString());
                //fld_GMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='GM'").ToString()).Replace("\\", "/");
                //fld_DGM.Text = DataAccess.Instance("BizDB").ExecuteScalar("select EXT04 from dbo.ORG_USER where EXT03 ='DGM'").ToString();
                //fld_GM.Text = DataAccess.Instance("BizDB").ExecuteScalar("select EXT04 from dbo.ORG_USER where EXT03 ='GM'").ToString();
				string gmgode = DataAccess.Instance("BizDB").ExecuteScalar("select USERCODE from dbo.ORG_USER where EXT03 ='GM'").ToString();
				fld_GMCODE.Text = gmgode;
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
            if (fld_CostCenterValue.Text.Trim() != "")
            {
                table.Add("CostCenter", fld_CostCenterValue.Text);
            }
            #endregion
		}
		protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
		{
			string strinsert = string.Format("update PROC_Travel set TravelExpStatus = 1 where DOCUMENTNO='" + fld_TravelRequestNo.Text + "'");
			DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert);
		}
		//private void bindingCostCenterList()
		//{
		//    DataTable Costcent = DataAccess.Instance("BizDB").ExecuteDataTable("select cosetcenter,cosetcenter+'-'+Description as description from  dbo.PROC_PURCHASE_COSTCENTER ");
		//    costcenter.DataTextField = "description";
		//    costcenter.DataValueField = "cosetcenter";
		//    costcenter.DataSource = Costcent;
		//    costcenter.DataBind();
		//    costcenter.Items.Insert(0, new ListItem("--Select--", ""));
		//}
		protected void fld_detail_PROC_TravalExpenseDetails_DT_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.DeleteRow(fld_detail_PROC_TravelExpenseDetails_DT, e);
		}

		protected void fld_detail_PROC_TravalExpenseDetails_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				//DropDownList ItemList = e.Item.FindControl("ItemList") as DropDownList;
				//ItemList.DataSource = Item;
				//ItemList.DataTextField = "DicText";
				//ItemList.DataValueField = "DicText";
				//ItemList.DataBind();
			}
		}

		protected void btnAddDetail_Click(object sender, EventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.AddNewRow(fld_detail_PROC_TravelExpenseDetails_DT);
		}

		//protected void fld_detail_PROC_TravelExpensePhone_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
		//{
		//    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
		//    {
		//        DropDownList payMethodList = e.Item.FindControl("payMethodList") as DropDownList;
		//        payMethodList.DataSource = PayMethod;
		//        payMethodList.DataTextField = "DicText";
		//        payMethodList.DataValueField = "DicText";
		//        payMethodList.DataBind();
		//    }
		//}

		//protected void fld_detail_PROC_TravelExpensePhone_DT_ItemCommand(object source, RepeaterCommandEventArgs e)
		//{
		//    UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
		//    userInfo.DeleteRow(fld_detail_PROC_TravelExpensePhone_DT, e);
		//}

		//protected void btnAddPhone_Click(object sender, EventArgs e)
		//{
		//    UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
		//    userInfo.AddNewRow(fld_detail_PROC_TravelExpensePhone_DT);
		//}

		protected void fld_detail_PROC_TravelExpenseThird_DT_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.DeleteRow(fld_detail_PROC_TravelExpenseThird_DT, e);
		}

		protected void fld_detail_PROC_TravelExpenseThird_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{

			}
		}

		protected void btnAddThird_Click(object sender, EventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.AddNewRow(fld_detail_PROC_TravelExpenseThird_DT);
		}

		protected void fld_detail_PROC_TravelExpenseForth_DT_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.DeleteRow(fld_detail_PROC_TravelExpenseForth_DT, e);
		}

		protected void fld_detail_PROC_TravelExpenseForth_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{

			}
		}

		protected void btnAddForth_Click(object sender, EventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.AddNewRow(fld_detail_PROC_TravelExpenseForth_DT);
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