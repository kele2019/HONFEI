using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;
using Ultimus.UWF.Form.ProcessControl;
using Presale.Process.PurchaseOrder.Entity;
using System.ComponentModel;
using System.Text;
using Presale.Process.Common;
using System.Collections;

namespace Presale.Process.PurchaseOrder
{
	public partial class NewRequest : System.Web.UI.Page
	{
		public DataTable Item = DataAccess.Instance("BizDB").ExecuteDataTable("select * from  dbo.OITM_Mid ");
		public DataTable Costcent = DataAccess.Instance("BizDB").ExecuteDataTable("select cosetcenter+'-'+Description as cosetcenter from  dbo.PROC_PURCHASE_COSTCENTER ");
		public DataTable PROJECT = DataAccess.Instance("BizDB").ExecuteDataTable("select PROJECT,PROJECT+'-'+DESCRIPTION as DESCRIPTION from dbo.PROC_PURCHASE_PROJECT ");

		public DataTable taxcode = DataAccess.Instance("BizDB").ExecuteDataTable("SELECT CODE+';'+ID TAXCODE,ID+' '+description as description FROM PROC_Purchase_TAXCODE");
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
					string FlagStatus = DataAccess.Instance("BizDB").ExecuteScalar("select  status from PROC_PurchaseOrder where INCIDENT='" + Incident + "' ").ToString();

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
				//bindingCurrencyList();
                //fld_DGM.Text = DataAccess.Instance("BizDB").ExecuteScalar("select EXT04  from dbo.ORG_USER where EXT03='DGM'").ToString();
                //fld_GM.Text = DataAccess.Instance("BizDB").ExecuteScalar("select EXT04  from dbo.ORG_USER where EXT03='GM'").ToString();
                //fld_SupplierMLogin.Text = DataAccess.Instance("BizDB").ExecuteScalar("select replace(LOGINNAME,'\\','/')+'|USER'  from dbo.ORG_USER where EXT03='Supply Chain Manager'").ToString();
                //fld_FMLogin.Text = DataAccess.Instance("BizDB").ExecuteScalar("select replace(LOGINNAME,'\\','/')+'|USER'  from dbo.ORG_USER where EXT03='CFO'").ToString();
                //fld_deptManagerLogin.Text = DataAccess.Instance("BizDB").ExecuteScalar("select Replace(EXT02,'\\','/')+'|USER' from dbo.ORG_USER where loginname = '" + Page.User.Identity.Name + "'").ToString();
                //fld_DGMLogin.Text = DataAccess.Instance("BizDB").ExecuteScalar("select replace(LOGINNAME,'\\','/')+'|USER'  from dbo.ORG_USER where EXT03='DGM'").ToString();
                //fld_GMLogin.Text = DataAccess.Instance("BizDB").ExecuteScalar("select replace(LOGINNAME,'\\','/')+'|USER'  from dbo.ORG_USER where EXT03='GM'").ToString();
                //fld_PURMLogin.Text = DataAccess.Instance("BizDB").ExecuteScalar("select replace(LOGINNAME,'\\','/')+'|USER'  from dbo.ORG_USER where EXT03='Supply Chain Manager'").ToString();
                //fld_SupplierLogin.Text = DataAccess.Instance("BizDB").ExecuteScalar("select replace(LOGINNAME,'\\','/')+'|USER'  from dbo.ORG_USER where EXT03='Buyer'").ToString();
				fld_Rate.Text = DataAccess.Instance("BizDB").ExecuteScalar("select DicValue from dbo.COM_DICTIONRY where Type='Currency' and DicText='USD'").ToString();
                DataBindBussinessData();
			}
			((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
			((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
		}
        public void DataBindBussinessData()
        {
            string strsql = @"  select USERNAME,REPLACE(LOGINNAME,'\','/') +'|USER' LOGINNAME from ORG_USER where USERID in(
select USERID from ORG_JOB where DEPARTMENTID=(
select DEPARTMENTID from ORG_DEPARTMENT where   DEPARTMENTNAME='PM')) and ISACTIVE=1";
            DataTable dtPurchaseDpet=DataAccess.Instance("BizDB").ExecuteDataTable(strsql);
            if (dtPurchaseDpet.Rows.Count > 0)
            {
                DropPMUser.DataSource = dtPurchaseDpet;
                DropPMUser.DataTextField = "USERNAME";
                DropPMUser.DataValueField = "LOGINNAME";
                DropPMUser.DataBind();
                DropPMUser.Items.Insert(0, new ListItem("--Please Select--", ""));
            }
            string strsql1 = @" select USERNAME,REPLACE(LOGINNAME,'\','/') +'|USER' LOGINNAME from ORG_USER where USERID in(
select USERID from ORG_JOB where DEPARTMENTID=(
select DEPARTMENTID from ORG_DEPARTMENT where  DEPARTMENTNAME='Quality')) and ISACTIVE=1";
            DataTable dtQualityDpet = DataAccess.Instance("BizDB").ExecuteDataTable(strsql1);
            if (dtQualityDpet.Rows.Count > 0)
            {
              DropQulityUser.DataSource = dtQualityDpet;
              DropQulityUser.DataTextField = "USERNAME";
              DropQulityUser.DataValueField = "LOGINNAME";
              DropQulityUser.DataBind();
              DropQulityUser.Items.Insert(0, new ListItem("--Please Select--", ""));
            }
        }


		protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
		{

            #region
            object Incident = Request.QueryString["Incident"];
            object ProcessName = Request.QueryString["Processname"];
            string strRevoke = string.Format("update PROC_REVOKE set status=1,OperatorDate=getdate() where  ProcessName='{0}' and incident='{1}'", ProcessName, Incident);
            DataAccess.Instance("BizDB").ExecuteNonQuery(strRevoke);
            #endregion
            Hashtable table = (Hashtable)sender;

			string allApplicant = Allpplicant.Text;
			string[] applicants = allApplicant.Split(',');
			StringBuilder str = new StringBuilder();
			foreach (string applicant in applicants)
			{
				string sql = " declare @ret nvarchar(4000) ";
				sql += " set @ret = '' ";
				sql += " select (@ret + EXT02 +'|USER,') as userinfo from  dbo.ORG_USER ";
				sql += " where LOGINNAME in ('" + applicant.Replace("/", "\\") + "') ";
				sql += " print(@ret) ";
				List<UserDeptManager> list = DataAccess.Instance("BizDB").ExecuteList<UserDeptManager>(sql);
				foreach (UserDeptManager personal in list)
				{
					str.Append((personal.userinfo).Replace("\\", "/"));
				}
			}
            if (Allpplicant.Text.Trim() != "")
            {
                if (Allpplicant.Text.Split(',').Length > 0)
                {
                    table.Add("ApplierLogin", Allpplicant.Text.Split(',')[0] + "|USER");
                }
            }
            else
            {
                string Strsql = @"select top(1) APPLICANTACCOUNT from PROC_Purchase where   DOCUMENTNO='" + fld_PurchaseOrderNo.Text.Split(',')[0]+"'";
                object ApplierLogin=DataAccess.Instance("BizDB").ExecuteScalar(Strsql);
                if (ApplierLogin != null)
                {
                    table.Add("ApplierLogin", ApplierLogin + "|USER");
                }

                
                
            }
			fld_ApprovalArr_DeptApplicant.Text = str.ToString();

		}
		protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
		{
			string strinsert = string.Format("update PROC_Purchase set PurchaseOrdStatus = 1 where DOCUMENTNO='" + fld_PurchaseOrderNo.Text + "'");
			DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert);
		}

		protected void fld_detail_PROC_PurchaseOrder_DT_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.DeleteRow(fld_detail_PROC_PurchaseOrder_DT, e);
		}

		protected void fld_detail_PROC_PurchaseOrder_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				//DropDownList ItemList = e.Item.FindControl("Items") as DropDownList;
				//ItemList.DataSource = Item;
				//ItemList.DataTextField = "ItemCode";
				//ItemList.DataValueField = "ItemCode";
				//ItemList.DataBind();
				//ItemList.Items.Insert(0, new ListItem("--Select--", ""));

				//DropDownList costcentlist = e.Item.FindControl("DropCOSTCENTER") as DropDownList;
				//costcentlist.DataSource = Costcent;
				//costcentlist.DataTextField = "cosetcenter";
				//costcentlist.DataValueField = "cosetcenter";
				//costcentlist.DataBind();
				//costcentlist.Items.Insert(0, new ListItem("--Select--", ""));

				//DropDownList DropPROJECTlist = e.Item.FindControl("DropPROJECT") as DropDownList;
				//DropPROJECTlist.DataSource = PROJECT;
				//DropPROJECTlist.DataTextField = "DESCRIPTION";
				//DropPROJECTlist.DataValueField = "PROJECT";
				//DropPROJECTlist.DataBind();
				//DropPROJECTlist.Items.Insert(0, new ListItem("--Select--", ""));

				DropDownList DropTAXcodelist = e.Item.FindControl("DropTAXcode") as DropDownList;
				DropTAXcodelist.DataSource = taxcode;
				DropTAXcodelist.DataTextField = "DESCRIPTION";
				DropTAXcodelist.DataValueField = "TAXCODE";
				DropTAXcodelist.DataBind();
				DropTAXcodelist.Items.Insert(0, new ListItem("--Select--", ""));
			}
		}

		protected void btnAdd_Click(object sender, EventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.AddNewRow(fld_detail_PROC_PurchaseOrder_DT);
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