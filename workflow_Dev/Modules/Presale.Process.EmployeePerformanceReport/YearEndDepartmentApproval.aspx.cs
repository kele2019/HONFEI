using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Ultimus.UWF.Form.ProcessControl;
using Presale.Process.EmployeePerformanceReport.Entity;
using System.ComponentModel;
using System.Data;

namespace Presale.Process.EmployeePerformanceReport
{
	public partial class YearEndDepartmentApproval : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{

			}
			((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
		}
        protected void Page_PreRender(object sender, EventArgs e)
        {
            UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            TextBox txtLoginName =userInfo.FindControl("fld_APPLICANTACCOUNT") as TextBox;
          
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
                       fld_BRating.Text = dtRatingPerformance.Rows[0]["RatingValue"].ToString();
                   }
                //}
            }
           //Response.Write(read_Year.Text);
           //Response.Write(txtLoginName.Text);

            //string strsql = "";
            //DataAccess.Instance("BizDB").ExecuteScalar("");
        }
		protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
		{
			string sql = "select EXT02 from dbo.org_user where loginname = '" + Page.User.Identity.Name + "'";
			UserManager manager = DataAccess.Instance("BizDB").ExecuteEntity<UserManager>(sql);
			fld_SecondManagerLogin.Text = manager.EXT02.Replace("\\","/") + "|USER";
		}
	}
}