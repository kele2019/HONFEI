using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using System.ComponentModel;
using MyLib;
using System.Data;

namespace Presale.Process.ProcessPerformance
{
	public partial class DeptApproval : System.Web.UI.Page
	{
        string dept = "";
		protected void Page_Load(object sender, EventArgs e)
		{
		
			if (!IsPostBack)
			{
				string post = DataAccess.Instance("BizDB").ExecuteScalar("select EXT03 from dbo.ORG_USER where loginname = '" + Page.User.Identity.Name + "'").ToString();
				string stepName = Request.QueryString["StepName"].ToString();
				string Incident = Request.QueryString["Incident"].ToString();
				string FormId = DataAccess.Instance("BizDB").ExecuteScalar("select FORMID from dbo.PROC_ProcessPerformance where INCIDENT = " + int.Parse(Incident)).ToString();
				switch (stepName)
				{
					case "Management Approve":
						dept += "'HFPD-01'";
						break;
					case "Engineering Approve":
						dept += "'HFPD-04'";
						break;
					case "SupplyChain Approve":
						dept += "'HFPD-03'";
						break;
					case "PM Approve":
						dept += "'HFPD-02'";
						break;
					case "IT Approve":
						dept += "'HFPD-06'";
						break;
					case "HR Approve":
						dept += "'HFPD-07'";
						break;
					case "Admin Approve":
                    case "HSE Approval":
						dept += "'HFPD-08'";
						break;
					case "Quality Approve":
						dept += "'HFPD-09','HFPD-10'";
						break;
                    case "FIN Approve":
                        dept += "'FIN'";
                        break;
				}
				string year = read_Year.Text;
				string month = read_Month.Text;
				bindRepeate(dept, year, month, FormId);
			}
			((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
			((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
		}

		private void bindRepeate(string dept, string year, string month, string FormId)
		{
			//string sql = "select a.*,b.StatusValue,b.ActionNeed from (select  * from PROC_ProcessPerformance_Thrid where DEPTMENTCODE in(" + dept + "))a left join PROC_ProcessPerformance_Forth b  on a.ID=B.ID  ";
			string sql = "";
			//判断数据表里有没有提交过的单子
			string sqlflag = "select * from PROC_ProcessPerformance_Forth where FORMID='" + FormId + "' and DEPTMENTCODE in(" + dept + ")";
			DataTable flag = DataAccess.Instance("BizDB").ExecuteDataTable(sqlflag);
			if (flag.Rows.Count > 0)//有
			{
                sql += "select a.DEPTMENTCODE,a.ID,a.PROCESS,a.PROCESSMEA,a.STANDARD,b.StatusValue,b.ActionNeed,b.YTD from PROC_ProcessPerformance_Thrid a left join PROC_ProcessPerformance_Forth b on a.ID = b.ID where a.DEPTMENTCODE in(" + dept + ") and b.FORMID='" + FormId + "' Order by a.DEPTMENTCODE,a.ID";
			}
			else {
                sql += "select *,'' as YTD from PROC_ProcessPerformance_Thrid where DEPTMENTCODE in(" + dept + ")  and ContactInfo=(select top(1) ContactInfo  from PROC_ProcessPerformanceVersion order by  ID  desc)  Order by DEPTMENTCODE,ID";
			}
			DataTable dtInfo = DataAccess.Instance("BizDB").ExecuteDataTable(sql);
			if (dtInfo.Rows.Count > 0)
			{
				RepeaterDetail.DataSource = dtInfo;
				RepeaterDetail.DataBind();
			}
		}
		protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
		{

		}
		protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			string formId = userInfo.FormId;

            string sqldel = "delete from PROC_ProcessPerformance_Forth where FORMID = '" + formId + "' and DEPTMENTCODE='"+dept+"'";
            DataAccess.Instance("BizDB").ExecuteNonQuery(sqldel);
            string sql="";
			for (int i = 0; i < RepeaterDetail.Items.Count; i++)
			{
				Label ID = (Label)RepeaterDetail.Items[i].FindControl("ID");
				string Id = ID.Text;
				Label ROWID = (Label)RepeaterDetail.Items[i].FindControl("ROWID");
				string rowID = ROWID.Text;
				Label DEPTMENTCODE = (Label)RepeaterDetail.Items[i].FindControl("DEPTMENTCODE");
                string dept1 = DEPTMENTCODE.Text;
				TextBox StatusValue = (TextBox)RepeaterDetail.Items[i].FindControl("StatusValue");
				string statusValue = StatusValue.Text;
				TextBox ActionNeed = (TextBox)RepeaterDetail.Items[i].FindControl("ActionNeed");
				string actionNeed = ActionNeed.Text;
				Label PROCESS = (Label)RepeaterDetail.Items[i].FindControl("PROCESS");
				string Process = PROCESS.Text;
				Label PROCESSMEA = (Label)RepeaterDetail.Items[i].FindControl("PROCESSMEA");
				string ProcessMea = PROCESSMEA.Text;
				Label STANDARD = (Label)RepeaterDetail.Items[i].FindControl("STANDARD");
				string Standard = STANDARD.Text;

                TextBox YTD = (TextBox)RepeaterDetail.Items[i].FindControl("YTD");
                string ytd = YTD.Text;
                sql += "insert into PROC_ProcessPerformance_Forth values('" + Id + "','" + formId + "'," + int.Parse(rowID) + ",'" + dept1 + "','" + statusValue + "','" + actionNeed + "','" + Process + "','" + ProcessMea + "','" + Standard + "','0','" + read_Year.Text + "','" + read_Month.Text + "','" + ytd+ "')";
			}
            DataAccess.Instance("BizDB").ExecuteNonQuery(sql);
		}
	}
}