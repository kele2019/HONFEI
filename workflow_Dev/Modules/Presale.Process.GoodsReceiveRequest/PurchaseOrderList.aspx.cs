using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;

namespace Presale.Process.GoodsReceiveRequest
{
    public partial class PurchaseOrderList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                object venderInfo = Request.QueryString["VenderInfo"];
                object CostCenter = Request.QueryString["CostCenter"];

                if (venderInfo != null && CostCenter!=null)
                {
                    repeaterbind(venderInfo.ToString(), CostCenter.ToString());
                }
            }
        }
        public void repeaterbind(string CardCode,string CostCenter)
        {
            //int pageIndex = AspNetPager1.CurrentPageIndex;
            //int pageSize = AspNetPager1.PageSize;

            string sql = "select * from V_SAPPO where CardCode='" + CardCode + "' and SharepointCostCenter='" + CostCenter + "'";
            if (txtPONO.Text.Trim() != "")
            {
                sql += " AND DocNum LIKE '%" + txtPONO.Text.Trim() + "%'";
            }
            if (txtPRNO.Text.Trim() != "")
            {
                sql += " AND U_M_PRNo LIKE '%" + txtPRNO.Text.Trim() + "%'";
            }
            DataTable dtInfo = DataAccess.Instance("BizDB").ExecuteDataTable(sql);
            if (dtInfo.Rows.Count > 0)
            {
                purchaseList.DataSource = dtInfo;
                purchaseList.DataBind();
            }
            else
            {
                purchaseList.DataSource = null;
                purchaseList.DataBind();
            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            object venderInfo = Request.QueryString["VenderInfo"];
            object CostCenter = Request.QueryString["CostCenter"];

            if (venderInfo != null)
            {
                repeaterbind(venderInfo.ToString(), CostCenter.ToString());
            }
        }
    }
}