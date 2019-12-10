using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;

namespace Presale.Process.PurchaseOrder
{
	public partial class Item : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				repeaterbind();
			}
		}

		private void repeaterbind()
		{
			int pageIndex = AspNetPager1.CurrentPageIndex;
			int pageSize = AspNetPager1.PageSize;
			string sql = "select * from ( select * from  dbo.OITM_Mid) B";
			if (code.Text.Trim() != "" && name.Text.Trim() != "") {
				sql += " where ItemCode LIKE '" + code.Text.ToUpperInvariant() + "%' and LOWER(ItemName) LIKE '" + name.Text.ToLowerInvariant() + "%'";
			}
			if (code.Text.Trim() != "" && name.Text.Trim() == "")
			{
				sql += " where ItemCode LIKE '" + code.Text.ToUpperInvariant() + "%'";
			}
			if (name.Text.Trim() != "" && code.Text.Trim() == "")
			{
				sql += " where LOWER(ItemName) LIKE '" + name.Text.ToLowerInvariant() + "%'";
			}
			string strsqlcount = "select  COUNT(1) from  (" + sql + ")E";
			AspNetPager1.RecordCount = int.Parse(DataAccess.Instance("BizDB").ExecuteScalar(strsqlcount).ToString());

			string strsql = @" SELECT * FROM  (select ROW_NUMBER() over(order by ItemCode) RN, q.* from  (" + sql + " ) as q)p";
			strsql += " WHERE RN BETWEEN '" + pageSize * (pageIndex - 1) + "' AND '" + pageSize * pageIndex + "'";
			DataTable dtInfo = DataAccess.Instance("BizDB").ExecuteDataTable(strsql);
			if (dtInfo.Rows.Count > 0)
			{
				ItemList.DataSource = dtInfo;
				ItemList.DataBind();
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