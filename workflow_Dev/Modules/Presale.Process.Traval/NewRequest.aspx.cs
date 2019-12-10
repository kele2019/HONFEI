using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data;
using System.ComponentModel;
using MyLib;
using Ultimus.UWF.Form.ProcessControl;
using System.Collections;
using Presale.Process.Common;
namespace Presale.Process.Traval
{
    public partial class NewRequest : System.Web.UI.Page
    {

        DataTable data = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               string STRUSER=Page.User.Identity.Name;
			   object ProcessName = Request.QueryString["Processname"]; 
               object myRequest = Request.QueryString["Type"];
                object Incident=Request.QueryString["Incident"];
               if (myRequest != null)
               {
                   if (myRequest.ToString().ToUpper() == "NEWREQUEST")
                   {
                       UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
                       userInfo.AddNewRow(fld_detail_PROC_Travel_DT);
                   }
                   if (myRequest.ToString().ToUpper() == "MYTASK" && int.Parse(Incident.ToString()) > 0)
                   {
                       hdIncident.Value = Incident.ToString();
                   }
                   if (Incident != null)
                   {
					hdPrint.Value=DataAccess.Instance("BizDB").ExecuteScalar("select  COUNT(1) from PROC_TRAVEL where INCIDENT='" + Incident + "' and STATUS=2").ToString();
                   }
				   if (Incident.ToString() != "0")
				   {
					   string FlagStatus = DataAccess.Instance("BizDB").ExecuteScalar("select  status from PROC_TRAVEL where INCIDENT='" + Incident + "' ").ToString();

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

            //设置流程号
           // userInfo.ProcessPrefix = "TE-" + ((Label)userInfo.FindControl("fld_COMPANYCODE")).Text;
        }

        private void NewRequest_CloseProcessClick(object sender, CancelEventArgs e)
        {
            // 关闭流程，释放TA单信息
            try
            {
                //FindDOAAndBudget find = new FindDOAAndBudget();
                //int i = find.Open_TASelectStatus(this.fld_TAFORMID.Text);
                //if (i == 0)
                //{
                //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript'> alert('取消流程更改TA单状态出错，请联系管理员！');</script>");
                //    e.Cancel = true;
                //    return;
                //}
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript'> alert('取消流程更改TA单状态出错，请联系管理员！ 错误原因：" + ex.ToString() + "');</script>");
                e.Cancel = true;
                return;
            }
        }


        private void NewRequest_AfterFormDataLoad(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (!string.IsNullOrEmpty(Request.QueryString["Type"].ToString()))
            //{
            //    string View = Request.QueryString["Type"].ToString();
            //    if (View == "myrequest")
            //    {
            //        UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            //        string sql = "select top 1 [STATUS] from PROC_TravelExpense where FORMID = N'" + userInfo.FormId.Trim() + "'";
            //        string _STATUS = (string)DataAccess.Instance("BizDB").ExecuteScalar(sql);
            //        if (("2,3,4,").IndexOf(_STATUS) == -1)
            //        {
            //            ButtonList buttonlist = Page.FindControl("ButtonList1") as ButtonList;

            //            Button tt = (Button)buttonlist.FindControl("btnCloseProcess");

            //            tt.Visible = true;
            //        }
            //    }

            //}


            //this.fld_PrediemMealsStandard.Text = string.IsNullOrEmpty(this.fld_PrediemMealsStandard.Text) ? "" : string.Format("{0:N2}", Convert.ToSingle(this.fld_PrediemMealsStandard.Text));
            //fld_ActualPrediemMeals.Text = string.IsNullOrEmpty(this.fld_ActualPrediemMeals.Text) ? "" : string.Format("{0:N2}", Convert.ToSingle(this.fld_ActualPrediemMeals.Text));
            //fld_ActualPrediemMealsTotal.Text = string.IsNullOrEmpty(this.fld_ActualPrediemMealsTotal.Text) ? "" : string.Format("{0:N2}", Convert.ToSingle(this.fld_ActualPrediemMealsTotal.Text));
            //fld_TotalBudget.Text = string.IsNullOrEmpty(this.fld_TotalBudget.Text) ? "" : string.Format("{0:N2}", Convert.ToSingle(this.fld_TotalBudget.Text));
            //fld_TravelBudget.Text = string.IsNullOrEmpty(this.fld_TravelBudget.Text) ? "" : string.Format("{0:N2}", Convert.ToSingle(this.fld_TravelBudget.Text));
            //fld_TotalBudget.Text = string.IsNullOrEmpty(this.fld_TotalBudget.Text) ? "" : string.Format("{0:N2}", Convert.ToSingle(this.fld_TotalBudget.Text));
            //fld_HomeCarMileageClaim.Text = string.IsNullOrEmpty(this.fld_HomeCarMileageClaim.Text) ? "" : string.Format("{0:N2}", Convert.ToSingle(this.fld_HomeCarMileageClaim.Text));
        }

        /// <summary>
        /// 明细行添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
          userInfo.AddNewRow(fld_detail_PROC_Travel_DT);
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
            string domain = ConfigurationManager.AppSettings["Domain"];
            ApprovalInfo mode = new ApprovalInfo();
            if (fld_JPYDNo.Checked && fld_YDJDNo.Checked)
                table.Add("AllNo", "1");
            mode=GetOrgLevel.UserLevelInfo(Page.User.Identity.Name);
               table.Add("Level", mode.EXT01);
               if (mode.LeaderName == mode.ManagerName || mode.ManagerName=="")
               table.Add("Manager1", "");
           else
                   table.Add("Manager1", "USER:org=" + domain + ",user=" + mode.ManagerName.Replace('\\', '/'));
               if (mode.LeaderName == mode.DirectManagerName || mode.DirectManagerName == "")
           table.Add("SuperManager", "");
           else
               table.Add("SuperManager", "USER:org=" + domain + ",user=" + mode.DirectManagerName.Replace('\\', '/'));

        }

        protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
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
        protected void fld_detail_PROC_Travel_DT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            userInfo.DeleteRow(fld_detail_PROC_Travel_DT, e);
        }
        public void fld_detail_PROC_Travel_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                 
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string tablename = fld_detail_PROC_Travel_DT.ID.Replace("fld_detail_", "").Replace("read_detail_", "");
                UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
                DataTable dt = ((DataTable)userInfo.DetailData[tablename]).Clone();
                dt = userInfo.GetDetailData(fld_detail_PROC_Travel_DT);
                foreach (RepeaterItem item in fld_detail_PROC_Travel_DT.Items)
                {
                    HtmlInputCheckBox cb = item.FindControl("cb_SelectValue") as HtmlInputCheckBox;
                    if (cb.Checked)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["ROWID"].ToString() == cb.Value)
                            {
                                row.Delete();
                                dt.AcceptChanges();
                                break;
                            }
                        }
                    }
                }
                fld_detail_PROC_Travel_DT.DataSource = dt;
                fld_detail_PROC_Travel_DT.DataBind();
            }
            catch (Exception ex)
            {
                MyLib.LogUtil.Error(ex);
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert(\"" + ex.Message.Replace("\r\n", " ").Replace("\n", "").Replace("'", "") + "\");</script>");
            }
        }
        public DataTable GetVLookupDT(string LookUp_Type)
        {
            string SQL = "Select TYPEVAULESCODE,TYPEVAULES from V_LOOKUP where LOOKUP_TYPE='{0}'";
            SQL = string.Format(SQL, LookUp_Type);
            DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(SQL);
            return dt;
        }
        public DataTable GetExpenseCategory(string ProcessName)
        {
            string SQL = "Select CATEGORYCODE,CATEGORY from Mng_ExpenseCategory where ProcessName='{0}' and IsDisplay=1";
            SQL = string.Format(SQL, ProcessName);
            DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(SQL);
            return dt;
        }
        public DataTable GetCurrency()
        {
            string SQL = "Select CURRENCY_CODE from Mng_CurrencyType where Status=1";
            DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(SQL);
            return dt;
        }
        protected void fld_detail_PROC_TravelExpense_DT_PreRender(object sender, EventArgs e)
        {
             
        }


        // jack Add at 20140814  验证Remark 是否必填
        public void CheckRemarkIsCanEmpty(TextBox fld_Date, TextBox fld_ToDate, TextBox fld_Note)
        {
            

        }

        protected void fld_HotelCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            fld_HotelCity.Text = drop_HotelCity.SelectedItem.Text;
            fld_HotelCityValue.Text = drop_HotelCity.SelectedItem.Value;
            GetCityHotel(fld_HotelCityValue.Text);
        }

        protected void fld_HotelCity_PreRender(object sender, EventArgs e)
        {
            DataTable dtHotelCity = DataAccess.Instance("BizDB").ExecuteDataTable("select ID, (CityName+' '+CityEnName) City from  dbo.COM_HotelCity");
            if (dtHotelCity.Rows.Count > 0)
            {
               drop_HotelCity.DataSource = dtHotelCity;
               drop_HotelCity.DataTextField = "City";
               drop_HotelCity.DataValueField = "ID";
               drop_HotelCity.DataBind();
               drop_HotelCity.Items.Insert(0, new ListItem("--请选择--", ""));
            }
            drop_HotelCity.SelectedIndex = drop_HotelCity.Items.IndexOf(drop_HotelCity.Items.FindByValue(fld_HotelCityValue.Text));
            if (fld_HotelCityValue.Text != "")
            {
                GetCityHotel(fld_HotelCityValue.Text);
            }
        }
        public void GetCityHotel(string CityID )
        {
            DataTable dtHotel = DataAccess.Instance("BizDB").ExecuteDataTable("select (HotelName+' '+HotelEnName) Hotel from COM_CityHotel where CityID='" + CityID + "'");
            if (dtHotel.Rows.Count > 0)
            {
                drop_CityHotel.DataSource = dtHotel;
                drop_CityHotel.DataTextField = "Hotel";
                drop_CityHotel.DataValueField = "Hotel";
                drop_CityHotel.DataBind();
                drop_CityHotel.Items.Insert(0, new ListItem("--请选择--", ""));
            }
            drop_CityHotel.SelectedIndex = drop_CityHotel.Items.IndexOf(drop_CityHotel.Items.FindByValue(fld_CityHotel.Text));


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