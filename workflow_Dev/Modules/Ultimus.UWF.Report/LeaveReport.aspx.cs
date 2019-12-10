using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Data;

namespace Ultimus.UWF.Report
{
    public partial class LeaveReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            repeaterbind();
        }
        public void repeaterbind()
        {
            int pageIndex = AspNetPager1.CurrentPageIndex;
            int pageSize = AspNetPager1.PageSize;

            string strsqlcount = "select  COUNT(*) from  COM_LevalManager WHERE LeaveYear=DATEPART(YY,GETDATE())";
            AspNetPager1.RecordCount = int.Parse(DataAccess.Instance("BizDB").ExecuteScalar(strsqlcount).ToString());

            string sql = @"select top '" + pageSize + "' from COM_LevalManager where  LeaveYear=DATEPART(YY,GETDATE()) and UserAccount not in (select top '" + (pageSize * (pageIndex - 1) + 1) + "' UserAccount from COM_LevalManager where  LeaveYear=DATEPART(YY,GETDATE()) order by UserAccount) order by UserAccount";
            DataTable dtInfo = DataAccess.Instance("BizDB").ExecuteDataTable(sql);
            if (dtInfo.Rows.Count > 0)
            {
                rpList.DataSource = dtInfo;
                rpList.DataBind();
            }
          
        }
    }
}