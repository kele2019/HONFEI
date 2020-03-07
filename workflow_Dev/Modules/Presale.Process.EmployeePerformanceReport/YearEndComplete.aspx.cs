using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Presale.Process.Common;
using Ultimus.UWF.Form.ProcessControl;
using System.ComponentModel;
using System.Data;

namespace Presale.Process.EmployeePerformanceReport
{
	public partial class YearEndApproval : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{

			}
			((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
		}
        protected void Page_PreRender(object sender, EventArgs e)
        {
            UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            TextBox txtLoginName = userInfo.FindControl("fld_APPLICANTACCOUNT") as TextBox;

            string Incident = Request.QueryString["Incident"];
            if (!IsPostBack)
            {
                //string Strsql = "select * from PROC_EmployeePerformance where incident=" + Incident + "";
                //DataTable dtPerformance = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
                //if (dtPerformance.Rows.Count > 0)
                //{
                //    string LoginName = dtPerformance.Rows[0]["APPLICANTACCOUNT"].ToString();
                //    string read_Year = dtPerformance.Rows[0]["Year"].ToString();
                string StrsqlRating = "select * from COM_UserRatingData where loginName='" + txtLoginName.Text + "' and RatingYear='" + read_Year.Text + "'";
                DataTable dtRatingPerformance = DataAccess.Instance("BizDB").ExecuteDataTable(StrsqlRating);
                if (dtRatingPerformance.Rows.Count > 0)
                {
                    read_BRating.Text = dtRatingPerformance.Rows[0]["RatingValue"].ToString();
                }
                //}
            }
            //Response.Write(read_Year.Text);
            //Response.Write(txtLoginName.Text);

            //string strsql = "";
            //DataAccess.Instance("BizDB").ExecuteScalar("");
        }
		private void NewRequest_AfterSubmit(object sender, System.ComponentModel.CancelEventArgs e)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			DataAccess.Instance("BizDB").ExecuteNonQuery("update PROC_EmployeePerformance set EPStatus='2' where FORMID = '" + userInfo.FormId.Trim() + "'");
		}
	}
}