using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Data;


namespace Presale.Process.EmployeePerformanceReport
{
    public partial class PerformanceReportPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetReportYear();
                GetDateBind();
            }
        }

        public void GetReportYear()
        {
            string Strsql = "select distinct Year from  dbo.PROC_EmployeePerformance order by Year desc";
            DataTable dtReportYear = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            if (dtReportYear.Rows.Count > 0)
            {
                DropReportYear.DataSource = dtReportYear;
                DropReportYear.DataTextField = "Year";
                DropReportYear.DataValueField = "Year";
            }
            //else
            //    DropReportYear.DataSource = null;
            DropReportYear.DataBind();
            DropReportYear.Items.Insert(0, new ListItem("--Please Select--", ""));

            //string Strsqluser = "select REPLACE(LOGINNAME,'\','/') as  LOGINNAME,(USERNAME+' '+EXT04) UserName from ORG_USER order by USERNAME";
            string Strsqluser = "select  LOGINNAME,(EXT04+' '+USERNAME) UserName from ORG_USER order by EXT04";
            DataTable dtUserInfo = DataAccess.Instance("BizDB").ExecuteDataTable(Strsqluser);
            if (dtUserInfo.Rows.Count > 0)
            {
                DropEmployeeUser.DataSource = dtUserInfo;
                DropEmployeeUser.DataTextField = "UserName";
                DropEmployeeUser.DataValueField = "LOGINNAME";
            }
            else
                DropEmployeeUser.DataSource = null;
            DropEmployeeUser.DataBind();
            DropEmployeeUser.Items.Insert(0,new ListItem("--Please Select--",""));
        }


        public void GetDateBind()
        {
            int PageSize = AspNetPager1.PageSize;
            int PageIndex = AspNetPager1.CurrentPageIndex;
            string strfilter = "";
            string strsqlcount = "SELECT  count(1) FROM PROC_EmployeePerformanceReport WHERE 1=1";
            string Strsql = " select * from ( select ROW_NUMBER() over(order by RequestDate desc, AppicantAccount asc) RN ,* from  dbo.PROC_EmployeePerformanceReport where 1=1 ";
            if (DropReportType.SelectedItem.Value.Trim() != "")
            {
                strfilter += " and ReportType ='" + DropReportType.SelectedItem.Value.Trim() + "'";
            }
            if (DropReportYear.SelectedItem.Value.Trim() != "")
            {
                strfilter += " and ReportYear = '" + DropReportYear.SelectedItem.Value.Trim() + "' ";
            }
            if (DropEmployeeUser.SelectedItem.Value.Trim() != "")
            {
                strfilter += " and AppicantAccount = '" + DropEmployeeUser.SelectedItem.Value.Trim().Replace('\\','/') + "' ";
            }
            if (txtDept.Text.Trim() != "")
            {
                strfilter += " and DEPARTMENT like  '%" + txtDept.Text.Trim() + "%'";
            }
            strsqlcount += strfilter;
            Strsql += strfilter;
            Strsql += " ) A  where 1=1 and RN between " + PageSize * (PageIndex - 1) + " and " + PageSize * (PageIndex);
            DataTable dtPayment = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            int DataCount = Convert.ToInt32(DataAccess.Instance("BizDB").ExecuteScalar(strsqlcount));
            AspNetPager1.RecordCount = DataCount;
            if (dtPayment.Rows.Count > 0)
            {
                RPlist.DataSource = dtPayment;
            }
            else
                RPlist.DataSource = null;
            RPlist.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            GetDateBind();
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            GetDateBind();
        }
        public static string OpenPageURL(object ReportType)
        {
            if (ReportType.ToString() == "Begin-Year goal plan")
                return "BeginYearGoalPlanComplete.aspx";
            else
                if (ReportType.ToString() == "Mid-Year Update")
                    return "MidYearComplete.aspx";
                else
                    if (ReportType.ToString() == "End-Year Performance")
                        return "YearEndComplete.aspx";
                    else
                        return "";

        }
        public string  TransferDateData(string DateData)
        {
            if (!string.IsNullOrEmpty(DateData))
            {
                if (Convert.ToDateTime(DateData).Year >= 2000)
                    return DateData;
                else
                    return "";
            }
            else
                return "";
        }
    }
}