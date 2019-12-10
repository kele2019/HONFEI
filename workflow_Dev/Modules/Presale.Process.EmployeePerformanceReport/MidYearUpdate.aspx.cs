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
	public partial class MidYearUpdate : System.Web.UI.Page
	{
		DataTable dtFinaInfo = DataAccess.Instance("BizDB").ExecuteDataTable("select DicText,DicValue from COM_DICTIONRY where Type = 'Leave'");

		protected void Page_Load(object sender, EventArgs e)
		{
			object myRequest = Request.QueryString["Type"];
			if (!IsPostBack) {

				string Sql = "select * from (";
				Sql += " select A.USERNAME USERNAME,C.DEPARTMENTNAME DEPARTMENTNAME from dbo.ORG_USER A ";
				Sql += " left join dbo.ORG_JOB B on A.USERID = B.USERID ";
				Sql += " left join dbo.ORG_DEPARTMENT C on B.DEPARTMENTID = C.DEPARTMENTID ";
				Sql += " where A.LOGINNAME = '" + Page.User.Identity.Name + "')E";
				UserIntity user = DataAccess.Instance("BizDB").ExecuteEntity<UserIntity>(Sql);
				fld_EmployeeName.Text = user.USERNAME;
				fld_OnBoardingDepartment.Text = user.DEPARTMENTNAME;

				if (myRequest != null)
				{
					if (myRequest.ToString().ToUpper() == "NEWREQUEST")
					{
						UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
						userInfo.AddNewRow(fld_detail_PROC_EmployeePerformance_DT);
					}
				}
				fld_ApplicantUser.Text = Page.User.Identity.Name.Replace("\\", "/") + "|USER;";
				string Incident = Request.QueryString["Incident"];
				if (Incident != "0")
				{
					hdIncident.Value = Incident;
				}
				bindingList();
			}
		}
		private void bindingList()
		{
			int year = DateTime.Now.Year;
			ListItem yearItem = null;
			for (int i = year; i > year - 100; i--)
			{
				yearItem = new ListItem(i.ToString(), i.ToString());
				dropYear.Items.Add(yearItem);
			}
		}
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.AddNewRow(fld_detail_PROC_EmployeePerformance_DT);
		}
		protected void fld_detail_PROC_EmployeePerformance_DT_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.DeleteRow(fld_detail_PROC_EmployeePerformance_DT, e);
		}
		public void fld_detail_PROC_EmployeePerformance_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DropDownList dropApplying = e.Item.FindControl("dropCompletionStatus") as DropDownList;
				dropApplying.DataSource = dtFinaInfo;
				dropApplying.DataTextField = "DicText";
				dropApplying.DataValueField = "DicValue";
				dropApplying.DataBind();
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
			}
			catch (Exception ex)
			{
				MyLib.LogUtil.Error(ex);
				//Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert(\"" + ex.Message.Replace("\r\n", " ").Replace("\n", "").Replace("'", "") + "\");</script>");
			}
		}
	}
}