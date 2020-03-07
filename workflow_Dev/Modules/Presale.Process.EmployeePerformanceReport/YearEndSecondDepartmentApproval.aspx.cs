using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Ultimus.UWF.Form.ProcessControl;
using MyLib;

namespace Presale.Process.EmployeePerformanceReport
{
	public partial class YearEndSecondDepartmentApproval : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

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
	}
}