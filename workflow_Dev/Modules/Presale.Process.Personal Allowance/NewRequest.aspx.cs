using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data;
using Ultimus.UWF.Form.ProcessControl;
using MyLib;
using System.ComponentModel;
using System.Collections;
using Presale.Process.Common;
namespace Personal_Allowance
{
    public partial class NewRequest : System.Web.UI.Page
    {
        public DataTable GetCurrencyData = DataAccess.Instance("BizDB").ExecuteDataTable("select * from  dbo.COM_DICTIONRY where Type='Currency'");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
			{
				object ProcessName = Request.QueryString["Processname"];
			
                object myRequest = Request.QueryString["Type"];
                object Incident = Request.QueryString["Incident"];
                if (myRequest != null)
                {
                    if (myRequest.ToString() == "NEWREQUEST")
                    {
                        UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
                        userInfo.AddNewRow(fld_detail_PROC_PersonalAllownce_DT);
                    }
                    if (myRequest.ToString().ToUpper() == "MYTASK" && int.Parse(Incident.ToString()) > 0)
                    {
                        hdIncident.Value = Incident.ToString();
                    }
                    if (Incident != null)
                    {
                        hdPrint.Value = DataAccess.Instance("BizDB").ExecuteScalar("select  COUNT(1) from PROC_PersonalAllownce where INCIDENT='" + Incident + "' and STATUS=2").ToString();
                    }
					if (Incident.ToString() != "0")
					{
						string FlagStatus = DataAccess.Instance("BizDB").ExecuteScalar("select  status from PROC_PersonalAllownce where INCIDENT='" + Incident + "' ").ToString();

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
                }
                string strsql="select * from COM_AllowanceStandar where UserLevel=(select top 1 EXT01 from ORG_USER where LOGINNAME='" + Page.User.Identity.Name+ "')";
                DataTable dtAllowance = DataAccess.Instance("BizDB").ExecuteDataTable(strsql);
                if (dtAllowance.Rows.Count > 0)
                {
                    TranAndHouse.Value = dtAllowance.Rows[0]["Transportation"].ToString();
                    Tel.Value = dtAllowance.Rows[0]["Tel"].ToString();
                }
            }
            ((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
            ((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
        }
        /// <summary>
        /// 明细行添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            userInfo.AddNewRow(fld_detail_PROC_PersonalAllownce_DT);
        }

        protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
        {
            #region
            object Incident = Request.QueryString["Incident"];
            object ProcessName = Request.QueryString["Processname"];
            string strRevoke = string.Format("update PROC_REVOKE set status=1,OperatorDate=getdate() where  ProcessName='{0}' and incident='{1}'", ProcessName, Incident);
            DataAccess.Instance("BizDB").ExecuteNonQuery(strRevoke);
            #endregion
            #region 获取财务人员和HR信息
            Hashtable table = (Hashtable)sender;
            string domain = ConfigurationManager.AppSettings["Domain"];
            string strsql = @"select DicCode,DicValue  from  dbo.COM_DICTIONRY where TYPE='Fina' or  TYPE='HR'
    union all    select 'FinaKJ' DicCode,LOGINNAME DicValue from ORG_USER where USERID in(
    select  MEMBERID from ORG_GROUPMEMBER    where GROUPID=(select GROUPID from ORG_GROUP where   GROUPNAME='Fina Account'))";
            DataTable dtFinaInfo = DataAccess.Instance("BizDB").ExecuteDataTable(strsql);
            string IsRepater = "-1";
            if (dtFinaInfo.Rows.Count > 0)
            {
                foreach (DataRow dr in dtFinaInfo.Rows)
                {
                    if (dr["DicCode"].ToString() == "HR")
                    {
                        if (dr["DicValue"].ToString() == Page.User.Identity.Name)
                            IsRepater = "0";
                    }
                    if (dr["DicCode"].ToString() == "FinaKJ")
                    {
                        if (dr["DicValue"].ToString() == Page.User.Identity.Name)
                            IsRepater = "1";
                    }
                    if (dr["DicCode"].ToString() == "FinaJL")
                    {
                        if (dr["DicValue"].ToString() == Page.User.Identity.Name)
                            IsRepater = "2";
                    }
                    if (dr["DicCode"].ToString() == "FinaZJ")
                    {
                        if (dr["DicValue"].ToString() == Page.User.Identity.Name)
                            IsRepater = "3";
                    }
                    if (dr["DicCode"].ToString() == "FinaFZC")
                    {
                        if (dr["DicValue"].ToString() == Page.User.Identity.Name)
                            IsRepater = "4";
                    }
                    string RoleCode = dr["DicCode"].ToString();
                    switch (RoleCode)
                    {
                        //case "FinaKJ":
                        //    table.Add("FinaKJ", "USER:org=" + domain + ",user=" + dr["DicValue"].ToString().Replace('\\', '/'));
                        //    break;
                        case "FinaJL":
                            table.Add("FinaJL", "USER:org=" + domain + ",user=" + dr["DicValue"].ToString().Replace('\\', '/'));
                            break;
                        case "FinaZJ":
                            table.Add("FinaZJ", "USER:org=" + domain + ",user=" + dr["DicValue"].ToString().Replace('\\', '/'));
                            break;
                        case "FinaFZC":
                            table.Add("FinaFZC", "USER:org=" + domain + ",user=" + dr["DicValue"].ToString().Replace('\\', '/'));
                            break;
                        default:
                            break;
                    }
                }
            }
            table.Add("IsRepater", IsRepater);
            #endregion
        }

        protected void NewRequest_AfterSubmit(object j, CancelEventArgs g)
        {
            try
            {

                // find.Close_TASelectStatus(this.fld_TAFORMID.Text);
            }
            catch (Exception ex)
            {
                LogUtil.Error("审批阶段调用发送邮件方法失败！", ex);
            }

        }
        /// <summary>
        /// 明细行删除
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void fld_detail_PROC_TravalExpense_DT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            userInfo.DeleteRow(fld_detail_PROC_PersonalAllownce_DT, e);
        }
       
        public void fld_detail_PROC_TravalExpense_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
           
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //string CostCenter = GetOrgLevel.GetDeptInfo(Page.User.Identity.Name);
                //TextBox fld_CostCenter = (TextBox)e.Item.FindControl("fld_CostCenter");
                //fld_CostCenter.Text = CostCenter;
                DropDownList dropCurrency = e.Item.FindControl("dropCurrency") as DropDownList;
                dropCurrency.DataSource = GetCurrencyData;
                dropCurrency.DataTextField = "DicText";
                dropCurrency.DataValueField = "DicValue";
                dropCurrency.DataBind();
                //ddl.Items.Clear();
                //ddl.DataSource = GetExpenseCategory("TravelExpense");
                //ddl.DataValueField = "CATEGORYCODE";
                //ddl.DataTextField = "CATEGORY";
                //ddl.DataBind();
                //ddl.Items.Insert(0, new ListItem("-请选择分类-", ""));

                //DropDownList ddl1 = (DropDownList)e.Item.FindControl("fld_Currency");
                //ddl1.Items.Clear();
                //ddl1.DataSource = GetCurrency();
                //ddl1.DataValueField = "CURRENCY_CODE";
                //ddl1.DataTextField = "CURRENCY_CODE";
                //ddl1.DataBind();
                ////David Edit at 2014/12/23 修正加行币种变化问题
                //ddl1.Items.Insert(0, new ListItem("-请选择-", ""));
                //TextBox txt_Currency = ((TextBox)e.Item.FindControl("txt_Currency"));
                //if (string.IsNullOrEmpty(txt_Currency.Text))
                //{
                //    ddl1.Items.FindByValue("CNY").Selected = true;
                //    txt_Currency.Text = "CNY";
                //    TextBox fld_ExchangeRate = ((TextBox)e.Item.FindControl("fld_ExchangeRate"));
                //    fld_ExchangeRate.Text = "1.0000";
                //}

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