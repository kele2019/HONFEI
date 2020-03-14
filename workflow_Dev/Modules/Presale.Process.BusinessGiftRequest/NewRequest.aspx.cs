using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using MyLib;
using System.Data;
using System.ComponentModel;
using Presale.Process.Common;
using System.Collections;


namespace Presale.Process.BusinessGiftRequest
{
	public partial class NewRequest : System.Web.UI.Page
	{
       // public DataTable dtAsset = new DataTable();
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
                    userInfo.AddNewRow(fld_detail_PROC_BusinessGift_DT);
				}
				if (Incident.ToString() != "0")
				{
                    hdIncident.Value = Incident;
                    string FlagStatus = DataAccess.Instance("BizDB").ExecuteScalar("select  status from PROC_BusinessGift where INCIDENT='" + Incident + "' ").ToString();

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
               // bindingList();
                //fld_DGMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select IDNO from dbo.ORG_USER where loginname = '" + Page.User.Identity.Name + "' ").ToString());
			}
			((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
			((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
		}
        private void bindingList()
        {
            //string strSql = "select DicCode,DicText from COM_DICTIONRY where type = 'ITAsstesName'";
            //DataTable dtFinaInfo = DataAccess.Instance("BizDB").ExecuteDataTable(strSql);
           
            //dropIAName.DataTextField = "DicText";
            //dropIAName.DataValueField = "DicCode";
            //dropIAName.DataSource = dtFinaInfo;
            //dropIAName.DataBind();
        }
		protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
		{
            #region
            object Incident = Request.QueryString["Incident"];
            object ProcessName = Request.QueryString["Processname"];
            string strRevoke = string.Format("update PROC_REVOKE set status=1,OperatorDate=getdate() where  ProcessName='{0}' and incident='{1}'", ProcessName, Incident);
            DataAccess.Instance("BizDB").ExecuteNonQuery(strRevoke);

              Hashtable table = (Hashtable)sender;
              string IDNO= (DataAccess.Instance("BizDB").ExecuteScalar("select IDNO from dbo.ORG_USER where loginname = '" + Page.User.Identity.Name + "' ").ToString());
              table.Add("IDNO", IDNO);

            //string Strsql = @"SELECT EXT02 FROM ORG_DEPARTMENT WHERE DEPARTMENTID=(SELECT DEPARTMENTID FROM ORG_JOB WHERE USERID=(sELECT USERID FROM ORG_USER WHERE LOGINNAME='" + Page.User.Identity.Name + "'))";
            //object RequestCostcenter=DataAccess.Instance("BizDB").ExecuteScalar(Strsql);
            //if(RequestCostcenter!=null)
            //    if (RequestCostcenter.ToString() != fld_CostCenterValue.Text.Trim())
            //    {
            //        string Strsqlmanager = @"SELECT LOGINNAME+'|USER' FROM ORG_USER WHERE USERID=(select  top(1) DepartmentManager from ORG_DEPARTMENT WHERE EXT02='" + fld_CostCenterValue .Text+ "')";
            //        object StrsqlmanagerObj = DataAccess.Instance("BizDB").ExecuteScalar(Strsqlmanager);
            //        if(StrsqlmanagerObj!=null)
            //            table.Add("BizManager", StrsqlmanagerObj.ToString().Replace('\\','/'));
            //    }
            //if (fld_CostCenterValue.Text.Trim() != "")
            //{
            //    table.Add("CostCenter", fld_CostCenterValue.Text);
            //}

            #endregion
		}
		protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
		{
          
		}
        protected void fld_detail_PROC_BusinessGift_DT_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            userInfo.DeleteRow(fld_detail_PROC_BusinessGift_DT, e);
		}

        protected void fld_detail_PROC_BusinessGift_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
                string strSql = "select DicCode,DicText from COM_DICTIONRY where type = 'GiftData'";
                DataTable dtAssetsInfo = DataAccess.Instance("BizDB").ExecuteDataTable(strSql);

                DropDownList ItemList = e.Item.FindControl("Item") as DropDownList;
                ItemList.DataSource = dtAssetsInfo;
                ItemList.DataTextField = "DicText";
                ItemList.DataValueField = "DicText";
                ItemList.DataBind();
                ItemList.Items.Insert(0, new ListItem("--Pls select--", ""));
			}
		}
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            userInfo.AddNewRow(fld_detail_PROC_BusinessGift_DT);
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