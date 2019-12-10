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
namespace PersonalExpense
{
    public partial class NewRequest : System.Web.UI.Page
    {
        public int BorrowCount=0;
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
                        userInfo.AddNewRow(fld_detail_PROC_PersonalExpense_DT);
                        string strsql = "select  count(1) from PROC_CashAdvance  where APPLICANTACCOUNT='" + Page.User.Identity.Name.Replace('\\', '/') + "' and Status=2  and (BorrowAmount-ISNULL(ReturnAmount,0))>0 ";
                         BorrowCount=int.Parse(DataAccess.Instance("BizDB").ExecuteScalar(strsql).ToString());
                    }
                    if (myRequest.ToString().ToUpper() == "MYTASK" && int.Parse(Incident.ToString()) > 0)
                    {
                        hdIncident.Value = Incident.ToString();
                    }
                    if (Incident != null)
                    {
                        hdPrint.Value = DataAccess.Instance("BizDB").ExecuteScalar("select  COUNT(1) from PROC_PersonalExpense where INCIDENT='" + Incident + "' and STATUS=2").ToString();
                    }
					if (Incident.ToString() != "0")
					{
						string FlagStatus = DataAccess.Instance("BizDB").ExecuteScalar("select  status from PROC_PersonalExpense where INCIDENT='" + Incident + "' ").ToString();

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
            userInfo.AddNewRow(fld_detail_PROC_PersonalExpense_DT);
        }

        protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
        {
            Hashtable table = (Hashtable)sender;
            ApprovalInfo mode = new ApprovalInfo();
            string domain = ConfigurationManager.AppSettings["Domain"];
            mode = GetOrgLevel.UserLevelInfo(Page.User.Identity.Name);
           
            table.Add("Level", mode.EXT01);
            table.Add("Manager", "USER:org=" + domain + ",user=" + mode.LeaderSuprName.Replace('\\', '/'));
            if (mode.LeaderName == mode.ManagerName || mode.ManagerName == "")
                table.Add("Manager1", "");
            else
                table.Add("Manager1", "USER:org=" + domain + ",user=" + mode.ManagerName.Replace('\\', '/'));
            if (mode.LeaderName == mode.DirectManagerName || mode.DirectManagerName == "")
                table.Add("SuperManager", "");
            else
                table.Add("SuperManager", "USER:org=" + domain + ",user=" + mode.DirectManagerName.Replace('\\', '/'));

            string strsql = @" select DicCode,DicValue  from  dbo.COM_DICTIONRY where TYPE='Fina'   
    union all    select 'FinaKJ' DicCode,LOGINNAME DicValue from ORG_USER where USERID in(
    select  MEMBERID from ORG_GROUPMEMBER  where GROUPID=(select GROUPID from ORG_GROUP where   GROUPNAME='Fina Account'))";
            DataTable dtFinaInfo = DataAccess.Instance("BizDB").ExecuteDataTable(strsql);
            if (dtFinaInfo.Rows.Count > 0)
            {
                foreach (DataRow dr in dtFinaInfo.Rows)
                {
                    
                    if (dr["DicCode"].ToString() == "FinaKJ")
                    {
                        if (dr["DicValue"].ToString() == Page.User.Identity.Name)
                            table.Add("IsKJ", "1");
                    }
                    string RoleCode = dr["DicCode"].ToString();
                    switch (RoleCode)
                    {
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
        }

        protected void NewRequest_AfterSubmit(object j, CancelEventArgs g)
        {
            try
            {
            ApprovalHistory approvalHistory = Page.FindControl("ApprovalHistory1") as ApprovalHistory;
            int ActiontType = approvalHistory.ActionType;
            UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            string FormID = userInfo.FormId;
            DataAccess.Instance("BizDB").ExecuteNonQuery("delete from PROC_ResevserData where FormID='" + FormID + "' and RetrunType='PersonalExpese'");
            if (ActiontType == 0)//提交
            {
                string ReverseAmount = fld_ReverseAmount.Text.TrimEnd('|');
                StringBuilder strsql = new StringBuilder();
                if (fld_BrrowYes.Checked)
                {
                    foreach (var item in ReverseAmount.Split('|'))
                    {
                        string CashAdvanceNo = item.Split(',')[0];
                        string Amount = item.Split(',')[1];
                        strsql.AppendFormat("insert into dbo.PROC_ResevserData (FormID,CANo,ReturnAmount,RetrunType,ReturnDate)  values('{0}','{1}','{2}','PersonalExpese',GETDATE()) ", FormID, CashAdvanceNo, Amount);
                        strsql.AppendFormat("update PROC_CashAdvance set ReturnAmount=ISNULL(ReturnAmount,0)+{0} where  DOCUMENTNO='{1}'", Amount, CashAdvanceNo);
                    }
                    if (strsql.ToString() != "")
                    {
                        DataAccess.Instance("BizDB").ExecuteNonQuery(strsql.ToString());
                    }
                }
            }
            }
            catch (Exception ex)
            {
                LogUtil.Error("冲账方法失败！", ex);
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
            userInfo.DeleteRow(fld_detail_PROC_PersonalExpense_DT, e);
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
                TextBox txtCostCenter=e.Item.FindControl("fld_CostCenter") as TextBox;
                
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