using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MyLib;
using Presale.Process.Common;
using Presale.Process.EmployeePerformanceReport.Entity;
using Ultimus.UWF.Form.ProcessControl;

namespace Presale.Process.EmployeePerformanceReport
{
	public partial class GoalPlan : System.Web.UI.Page
	{
		DataTable dtFinaInfo = DataAccess.Instance("BizDB").ExecuteDataTable("select DicText,DicValue from COM_DICTIONRY where Type = 'Leave'");

		protected void Page_Load(object sender, EventArgs e)
		{
           
            string Incident=Request.QueryString["Incident"];
            string StrsqlE = "select COUNT(1) from PROC_EmployeePerformance where INCIDENT='" + Incident + "' and ReportType='End-Year Performance' and EPStatus=2";
            bool IsRedirect = Convert.ToInt32(DataAccess.Instance("BizDB").ExecuteScalar(StrsqlE)) > 0;
            if (IsRedirect)
                Response.Redirect("YearEndComplete.aspx?Incident=" + Incident + "&ProcessName=Employee%20Performance%20Report&Type=myapproval");
            string StrsqlM = "select COUNT(1) from PROC_EmployeePerformance where INCIDENT='" + Incident + "' and ReportType='Mid-Year Update' and EPStatus=2";
            bool IsRedirect1 = Convert.ToInt32(DataAccess.Instance("BizDB").ExecuteScalar(StrsqlM)) > 0;
            if (IsRedirect1)
                Response.Redirect("MidYearComplete.aspx?Incident=" + Incident + "&ProcessName=Employee%20Performance%20Report&Type=myapproval");


			object myRequest = Request.QueryString["Type"];
			if (!IsPostBack) {
				fld_ApplicantUserLogin.Text = Page.User.Identity.Name.Replace("\\","/") + "|USER";
				string Sql = "select * from (";
				Sql += " select A.EXT13 EXT03, A.USERNAME USERNAME,C.DEPARTMENTNAME DEPARTMENTNAME from dbo.ORG_USER A ";
				Sql += " left join dbo.ORG_JOB B on A.USERID = B.USERID ";
				Sql += " left join dbo.ORG_DEPARTMENT C on B.DEPARTMENTID = C.DEPARTMENTID ";
				Sql += " where A.LOGINNAME = '" + Page.User.Identity.Name + "')E";
				UserIntity user = DataAccess.Instance("BizDB").ExecuteEntity<UserIntity>(Sql);
				fld_EmployeeName.Text = user.USERNAME;
				fld_OnBoardingDepartment.Text = user.DEPARTMENTNAME;
				fld_EmployeePost.Text = user.EXT03;
				string sql1 = "select LOGINNAME from ORG_USER where EXT03 = 'GM'";
				EmployeeLoginEntity loginname = DataAccess.Instance("BizDB").ExecuteEntity<EmployeeLoginEntity>(sql1);
				fld_GM.Text = loginname.LOGINNAME.Replace("\\","/") + "|USER";

				if (myRequest != null)
				{
					if (myRequest.ToString().ToUpper() == "NEWREQUEST")
					{
						UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
						userInfo.AddNewRow(fld_detail_PROC_EmployeePerformance_DT);
					}
                     
                    object FlagStatus = DataAccess.Instance("BizDB").ExecuteScalar("select  status from PROC_EmployeePerformance where INCIDENT='" + Incident + "' ");
                    object ProcessName = Request.QueryString["Processname"];
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
                    }

				}
				//string Incident = Request.QueryString["Incident"];
				if (Incident != "0")
				{
					hdIncident.Value = Incident;
				}
				bindingList();		
			}
            ((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);

			((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
		}
		private void bindingList()
		{
			int year = DateTime.Now.Year;
			//ListItem yearItem = null;
			//for (int i = year; i > year; i--)
			//{
				//yearItem = new ListItem(i.ToString(), i.ToString());
			dropYear.Items.Insert(0, new ListItem(year.ToString(), year.ToString()));
			dropYear.Items.Insert(1, new ListItem((year + 1).ToString(), (year + 1).ToString()));
			dropYear.Items.Insert(2,new ListItem((year - 1).ToString(), (year - 1).ToString()));
			fld_Year.Text = year.ToString();
			//}
		}
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.AddNewRow(fld_detail_PROC_EmployeePerformance_DT);
			Page.RegisterStartupScript("key", "<script>$('#beginOrMidYear').show();</script>");
		}
        protected void BtnChangeReportType_Click(object sender, EventArgs e)
        {
            string Flag = hdReporttype.Value;
            UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            DataTable dt = userInfo.GetDetailData(fld_detail_PROC_EmployeePerformance_DT);
            if (Flag == "1")
            {
                string year = fld_Year.Text;
                string applierLogin = fld_ApplicantUserLogin.Text;
                string reportType = "Begin-Year goal plan";
                getEmployeePerformanceStatus(year, applierLogin, reportType);
                ReportLabel.Text = " Goal Plan（\'<span style=\' background:red\'>&nbsp;</span>\' must write）";
                Page.RegisterStartupScript("key", "<script>$('#beginOrMidYear').show();</script>");

                fld_ReportType.Text = "Begin-Year goal plan";
                foreach (RepeaterItem item in fld_detail_PROC_EmployeePerformance_DT.Items)
                {
                    HtmlInputCheckBox cb = item.FindControl("cb_SelectValue") as HtmlInputCheckBox;
                    foreach (DataRow row in dt.Rows)
                    {
                        row.Delete();
                        dt.AcceptChanges();
                        break;
                    }
                }
                fld_detail_PROC_EmployeePerformance_DT.DataSource = dt;
                fld_detail_PROC_EmployeePerformance_DT.DataBind();
            }
            if (Flag == "2")
            {
                string year = fld_Year.Text;
                string applierLogin = fld_ApplicantUserLogin.Text;
                string reportType = "Mid-Year Update";
                getEmployeePerformanceStatus(year, applierLogin, reportType);
                ReportLabel.Text = "Mid-Year Update（\'<span style=\' background:red;\'>&nbsp;</span>\' must write）";
                Page.RegisterStartupScript("key", "<script>$('#beginOrMidYear').show();</script>");
                fld_ReportType.Text = "Mid-Year Update";
                string sql = "select * from PROC_EmployeePerformance_DT where FormId in( select FORMID from PROC_EmployeePerformance where year='" + year + "' and ApplicantUserLogin = '" + applierLogin + "' and EPStatus ='2')";
                DataTable dtFinaInfo = DataAccess.Instance("BizDB").ExecuteDataTable(sql);
                fld_detail_PROC_EmployeePerformance_DT.DataSource = dtFinaInfo;
                fld_detail_PROC_EmployeePerformance_DT.DataBind();
            }
            if (Flag == "3")
            {
                string year = fld_Year.Text;
                string applierLogin = fld_ApplicantUserLogin.Text;
                string reportType = "End-Year Performance";
                getEmployeePerformanceStatus(year, applierLogin, reportType);
                Page.RegisterStartupScript("key", "<script>$('#beginOrMidYear').show();</script>");
                fld_ReportType.Text = "End-Year Performance";
                //foreach (RepeaterItem item in fld_detail_PROC_EmployeePerformance_DT.Items)
                //{
                //    HtmlInputCheckBox cb = item.FindControl("cb_SelectValue") as HtmlInputCheckBox;
                //    foreach (DataRow row in dt.Rows)
                //    {
                //        row.Delete();
                //        dt.AcceptChanges();
                //        break;
                //    }
                //}
                string sql = "select * from PROC_EmployeePerformance_DT where FormId in( select FORMID from PROC_EmployeePerformance where year='" + year + "' and ApplicantUserLogin = '" + applierLogin + "' and EPStatus ='2' and reporttype='Mid-Year Update')";
                DataTable dtFinaInfo = DataAccess.Instance("BizDB").ExecuteDataTable(sql);

                fld_detail_PROC_EmployeePerformance_DT.DataSource = dtFinaInfo;
                fld_detail_PROC_EmployeePerformance_DT.DataBind();
            }
        }

		protected void fld_detail_PROC_EmployeePerformance_DT_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.DeleteRow(fld_detail_PROC_EmployeePerformance_DT, e);
			Page.RegisterStartupScript("key", "<script>$('#beginOrMidYear').show();</script>");
		}
		public void fld_detail_PROC_EmployeePerformance_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				//DropDownList dropApplying = e.Item.FindControl("dropCompletionStatus") as DropDownList;
				//dropApplying.DataSource = dtFinaInfo;
				//dropApplying.DataTextField = "DicText";
				//dropApplying.DataValueField = "DicValue";
				//dropApplying.DataBind();
			}
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			try
			{
				string tablename = fld_detail_PROC_EmployeePerformance_DT.ID.Replace("fld_detail_", "").Replace("read_detail_", "");
				UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
				DataTable dt = userInfo.GetDetailData(fld_detail_PROC_EmployeePerformance_DT);
				foreach (RepeaterItem item in fld_detail_PROC_EmployeePerformance_DT.Items)
				{
					HtmlInputCheckBox cb = item.FindControl("cb_SelectValue") as HtmlInputCheckBox;
					if (cb.Checked)
					{
						foreach (DataRow row in dt.Rows)
						{
							//if (row["ROWID"].ToString() == cb.Value)
							//{
							row.Delete();
							dt.AcceptChanges();
							break;
							//}
						}
					}
				}
				fld_detail_PROC_EmployeePerformance_DT.DataSource = dt;
				fld_detail_PROC_EmployeePerformance_DT.DataBind();
				Page.RegisterStartupScript("key", "<script>$('#beginOrMidYear').show();</script>");
			}
			catch (Exception ex)
			{
				MyLib.LogUtil.Error(ex);
				//Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert(\"" + ex.Message.Replace("\r\n", " ").Replace("\n", "").Replace("'", "") + "\");</script>");
			}
		}

		//protected void BeginYear_Clicked(object sender, EventArgs e)
		//{
		//    string year = fld_Year.Text;
		//    string applierLogin = fld_ApplicantUserLogin.Text;
		//    string reportType = "Begin-Year goal plan";
		//    status.Text = GetStatus(year, applierLogin, reportType);
		//}
		//protected void MidYear_Clicked(object sender, EventArgs e)
		//{
		//    string year = fld_Year.Text;
		//    string applierLogin = fld_ApplicantUserLogin.Text;
		//    string reportType = "Mid-Year Update";
		//    status.Text = GetStatus(year, applierLogin, reportType);
		//}

		//protected void EndYear_Clicked(object sender, EventArgs e)
		//{
		//    string year = fld_Year.Text;
		//    string applierLogin = fld_ApplicantUserLogin.Text;
		//    string reportType = "End-Year Performance";
		//    status.Text = GetStatus(year, applierLogin, reportType);
		//}
		//protected void dropYear_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//    string year = fld_Year.Text;
		//    string applierLogin = fld_ApplicantUserLogin.Text;
		//    string reportType = fld_ReportType.Text;
		//    status.Text = GetStatus(year, applierLogin, reportType);
		//}
		private string GetStatus(string year, string applierLogin, string reportType)
		{
			string sql = "select EPStatus from PROC_EmployeePerformance where Year = '" + year + "' and ApplicantUserLogin = '" + applierLogin + "' and ReportType = '" + reportType + "'";
			List < StatusEntity > lists = DataAccess.Instance("BizDB").ExecuteList<StatusEntity>(sql);
			string value = "0";
			string value1 = "0";
			string value2 = "0";
			foreach(StatusEntity status in lists){
				if (status.EPStatus == "1") {
					value1 = "1";
				}
				if (status.EPStatus == "2")
				{
					value2 = "2";
				}
			}
			if (value1 == "1") { value = value1; }
			if (value2 == "2") { value = value2; }
			return value;
		}


        protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
        {
            #region
            object Incident = Request.QueryString["Incident"];
            object ProcessName = Request.QueryString["Processname"];
            string strRevoke = string.Format("update PROC_REVOKE set status=1,OperatorDate=getdate() where  ProcessName='{0}' and incident='{1}'", ProcessName, Incident);
            DataAccess.Instance("BizDB").ExecuteNonQuery(strRevoke);
            #endregion
        }
        private void NewRequest_AfterSubmit(object sender, System.ComponentModel.CancelEventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			DataAccess.Instance("BizDB").ExecuteNonQuery("update PROC_EmployeePerformance set EPStatus='1' where FORMID = '" + userInfo.FormId.Trim() + "'");
		}

		protected void BeginYear_CheckedChanged(object sender, EventArgs e)
		{
			string year = fld_Year.Text;
			string applierLogin = fld_ApplicantUserLogin.Text;
			string reportType = "Begin-Year goal plan";
			getEmployeePerformanceStatus(year, applierLogin, reportType);
			ReportLabel.Text = " Goal Plan（\'<span style=\' background:red\'>&nbsp;</span>\' must write）";
			Page.RegisterStartupScript("key", "<script>$('#beginOrMidYear').show();</script>");
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			DataTable dt = userInfo.GetDetailData(fld_detail_PROC_EmployeePerformance_DT);
			foreach (RepeaterItem item in fld_detail_PROC_EmployeePerformance_DT.Items)
			{
				HtmlInputCheckBox cb = item.FindControl("cb_SelectValue") as HtmlInputCheckBox;
				foreach (DataRow row in dt.Rows)
				{
					//if (row["ROWID"].ToString() == cb.Value)
					//{
					row.Delete();
					dt.AcceptChanges();
					break;
					//}
				}
			}
			fld_detail_PROC_EmployeePerformance_DT.DataSource = dt;
			fld_detail_PROC_EmployeePerformance_DT.DataBind();
			userInfo.AddNewRow(fld_detail_PROC_EmployeePerformance_DT);
			
		}

		protected void MidYear_CheckedChanged(object sender, EventArgs e)
		{
			string year = fld_Year.Text;
			string applierLogin = fld_ApplicantUserLogin.Text;
			string reportType = "Mid-Year Update";
			getEmployeePerformanceStatus(year, applierLogin, reportType);
			ReportLabel.Text = "Mid-Year Update（\'<span style=\' background:red;\'>&nbsp;</span>\' must write）";
			Page.RegisterStartupScript("key", "<script>$('#beginOrMidYear').show();</script>");
			string sql = "select * from PROC_EmployeePerformance_DT where FormId in( select FORMID from PROC_EmployeePerformance where year='" + year + "' and ApplicantUserLogin = '" + applierLogin + "' and EPStatus ='2')";
			DataTable dtFinaInfo = DataAccess.Instance("BizDB").ExecuteDataTable(sql);
			fld_detail_PROC_EmployeePerformance_DT.DataSource = dtFinaInfo;
			fld_detail_PROC_EmployeePerformance_DT.DataBind();
		}

		protected void EndYear_CheckedChanged(object sender, EventArgs e)
		{
			string year = fld_Year.Text;
			string applierLogin = fld_ApplicantUserLogin.Text;
			string reportType = "End-Year Performance";
			getEmployeePerformanceStatus(year,applierLogin,reportType);
			Page.RegisterStartupScript("key", "<script>$('#beginOrMidYear').hide();</script>");
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			DataTable dt = userInfo.GetDetailData(fld_detail_PROC_EmployeePerformance_DT);
			foreach (RepeaterItem item in fld_detail_PROC_EmployeePerformance_DT.Items)
			{
				HtmlInputCheckBox cb = item.FindControl("cb_SelectValue") as HtmlInputCheckBox;
					foreach (DataRow row in dt.Rows)
					{
						//if (row["ROWID"].ToString() == cb.Value)
						//{
						row.Delete();
						dt.AcceptChanges();
						break;
						//}
					}
			}
			fld_detail_PROC_EmployeePerformance_DT.DataSource = dt;
			fld_detail_PROC_EmployeePerformance_DT.DataBind();
		}

		private void getEmployeePerformanceStatus(string year,string applierLogin,string reportType)
		{
			string sql = "select EPStatus from PROC_EmployeePerformance where Year = '" + year + "' and ApplicantUserLogin = '" + applierLogin + "' and ReportType = '" + reportType + "'";
			List<StatusEntity> lists = DataAccess.Instance("BizDB").ExecuteList<StatusEntity>(sql);
			string value = "0";
			string value1 = "0";
			string value2 = "0";
			foreach (StatusEntity status in lists)
			{
				if (status.EPStatus.Trim() == "1")
				{
					value1 = "1";
				}
				if (status.EPStatus.Trim() == "2")
				{
					value2 = "2";
				}
			}
			if (value1 == "1") { value = value1; }
			if (value2 == "2") { value = value2; }
			employeePerformanceStatus.Text = value;
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