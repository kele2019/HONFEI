using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Data;

namespace Presale.Process.PaymentRequestForm
{
    public partial class POList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                repeaterbind();
            }
        }
         
        public void repeaterbind()
        {
            int pageIndex = AspNetPager1.CurrentPageIndex;
            int pageSize = AspNetPager1.PageSize;
            string Vendercode = Request.QueryString["Vendercode"];
            string sql = "select * from  V_SupplierOrder where CardCode='"+Vendercode+"'";
            if (txtPONo.Text.Trim() != "")
            {

                sql += "  AND PONum like '%" + txtPONo.Text.Trim() + "%'";
              
            }
            if (txtGRNo.Text.Trim() != "")
            {
                sql += "  AND PDNum like '%" + txtGRNo.Text.Trim() + "%'";
            }
            string strsqlcount = "select  COUNT(1) from  (" + sql + ")E";
            AspNetPager1.RecordCount = int.Parse(DataAccess.Instance("BizDB").ExecuteScalar(strsqlcount).ToString());

            string strsql = @" SELECT * FROM  (select ROW_NUMBER() over(order by PONum) RN, q.* from  (" + sql + " ) as q)p";
            strsql += " WHERE RN BETWEEN '" + pageSize * (pageIndex - 1) + "' AND '" + pageSize * pageIndex + "'";
            DataTable dtInfo = DataAccess.Instance("BizDB").ExecuteDataTable(sql);
            if (dtInfo.Rows.Count > 0)
            {
                purchaseList.DataSource = dtInfo;
                purchaseList.DataBind();
            }
            else
            {
                purchaseList.DataSource = dtInfo;
                purchaseList.DataBind();
            }


        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            repeaterbind();
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            repeaterbind();
        }
    }
}