using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using MyLib;
using System.Data;
using Presale.Process.Common;
using System.ComponentModel;
using System.Collections;

namespace Presale.Process.PurchaseApplication
{
	public partial class NewRequest : System.Web.UI.Page
	{
		public DataTable Item = DataAccess.Instance("BizDB").ExecuteDataTable("select * from  dbo.COM_DICTIONRY where Type='PurchaseItem'");

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
					string FlagStatus = DataAccess.Instance("BizDB").ExecuteScalar("select  status from PROC_Purchase where INCIDENT='" + Incident + "' ").ToString();

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
				bindingCurrencyList();
				bingdingCostCenter();
				fld_rate.Text = DataAccess.Instance("BizDB").ExecuteScalar("select DicValue from dbo.COM_DICTIONRY where Type='Currency' and DicText='USD'").ToString();
                fld_DGMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select IDNO from dbo.ORG_USER where loginname = '" + Page.User.Identity.Name + "' ").ToString());
                //fld_DGM.Text = DataAccess.Instance("BizDB").ExecuteScalar("select EXT04  from dbo.ORG_USER where EXT03='DGM'").ToString();
                //fld_GM.Text = DataAccess.Instance("BizDB").ExecuteScalar("select EXT04  from dbo.ORG_USER where EXT03='GM'").ToString();
                //fld_SupplierMLogin.Text = DataAccess.Instance("BizDB").ExecuteScalar("select replace(LOGINNAME,'\\','/')+'|USER'  from dbo.ORG_USER where EXT03='Supply Chain Manager'").ToString();
                //fld_FMLogin.Text = DataAccess.Instance("BizDB").ExecuteScalar("select replace(LOGINNAME,'\\','/')+'|USER'  from dbo.ORG_USER where EXT03='CFO'").ToString();
                //fld_deptManagerLogin.Text = DataAccess.Instance("BizDB").ExecuteScalar("select Replace(EXT02,'\\','/')+'|USER'  from dbo.ORG_USER where loginname = '" + Page.User.Identity.Name + "'").ToString();
                //fld_DGMLogin.Text = DataAccess.Instance("BizDB").ExecuteScalar("select replace(LOGINNAME,'\\','/')+'|USER'  from dbo.ORG_USER where EXT03='DGM'").ToString();
                //fld_GMLogin.Text = DataAccess.Instance("BizDB").ExecuteScalar("select replace(LOGINNAME,'\\','/')+'|USER'  from dbo.ORG_USER where EXT03='GM'").ToString();
                //fld_SupplierLogin.Text = DataAccess.Instance("BizDB").ExecuteScalar("select replace(LOGINNAME,'\\','/')+'|USER'  from dbo.ORG_USER where EXT03='Buyer'").ToString();
                //fld_PURMLogin.Text = DataAccess.Instance("BizDB").ExecuteScalar("select replace(LOGINNAME,'\\','/')+'|USER'  from dbo.ORG_USER where EXT03='Supply Chain Manager'").ToString();
                //fld_ApplierLogin.Text = Page.User.Identity.Name.Replace("\\", "/") + "|USER";
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

            Hashtable table = (Hashtable)sender;
            if (fld_CostCenterCode.Text.Trim() != "")
            {
                table.Add("CostCenter", fld_CostCenterCode.Text);
            }
            #endregion
        }
		private void bingdingCostCenter()
		{
			string sql = "select EXT03 from ORG_USER where loginname='" + Page.User.Identity.Name + "'";
			string post = DataAccess.Instance("BizDB").ExecuteScalar(sql).ToString();
			string dept = "";
			switch(post)
			{
				case "GM":
					dept = "General Management";
					break;
				case "DGM":
                case "PM":
					dept = "Operation";
					break;
				case "CTO":
				case "Deputy CTO":
					dept = "CTO/DCTO";
					break;
				case "Admin Assistant":
					dept = "Admin";
					break;
				case "HRM":
					dept = "Human Resources";
					break;
				case "IT Manager":
				case "IT Specialist":
					dept = "Information Technology";
					break;
				case "CFO":
				case "Controller":
					dept = "Finance";
					break;
				case "Engineer":
					dept = "Engineering-Mgt.";
					break;
				case "Quality Engineer":
				case "QAM":
					dept = "Quality";
					break;
				case "Supply Chain Manager":
				case "Buyer":
                    dept = "Operation";
					break;
                case "HSE&F Manager":
                    dept = "HSE&F";
                    break;
			}
			string sql2 = "select cosetcenter + '-' + Description from  dbo.PROC_PURCHASE_COSTCENTER where Description = '" + dept + "'";
			string sql3 = "select cosetcenter from  dbo.PROC_PURCHASE_COSTCENTER where Description = '" + dept + "'";
			fld_CostCenterDisplay.Text = DataAccess.Instance("BizDB").ExecuteScalar(sql2).ToString();
			fld_CostCenterCode.Text = DataAccess.Instance("BizDB").ExecuteScalar(sql3).ToString();
		}
		private void bindingCurrencyList()
		{
			string strSql = "select DicText from COM_DICTIONRY where type='Currency'";
			DataTable dtFinaInfo = DataAccess.Instance("BizDB").ExecuteDataTable(strSql);
			currency.DataTextField = "DicText";
			currency.DataValueField = "DicText";
			currency.DataSource = dtFinaInfo;
			currency.DataBind();
			currency.Items.Insert(0, new ListItem("--Select--", ""));
		}
		protected void fld_detail_PROC_Purchase_DT_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.DeleteRow(fld_detail_PROC_Purchase_DT, e);
		}

		protected void fld_detail_PROC_Purchase_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			//if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			//{
			//    DropDownList ItemList = e.Item.FindControl("Item") as DropDownList;
			//    ItemList.DataSource = Item;
			//    ItemList.DataTextField = "DicText";
			//    ItemList.DataValueField = "DicText";
			//    ItemList.DataBind();
			//}
		}

		protected void btnAdd_Click(object sender, EventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.AddNewRow(fld_detail_PROC_Purchase_DT);
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