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
	public partial class Vendor : System.Web.UI.Page
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

			string sql = "select * from PROC_Vendor1 where type='Vendor'";
			if (ByVendorCode.Text.Trim() != "")
			{
                sql += " and Lower(VendorDesp) like '%" + ByVendorCode.Text + "%'";
			}
			if (ByVendorName.Text.Trim() != "")
			{
				sql += " and Lower(VendorName) like '%" + ByVendorName.Text + "%'";
			}
			string strsqlcount = "select  COUNT(1) from  (" + sql + ")E";
			AspNetPager1.RecordCount = int.Parse(DataAccess.Instance("BizDB").ExecuteScalar(strsqlcount).ToString());

			string strsql = @" SELECT * FROM  (select ROW_NUMBER() over(order by VendorCode) RN, q.* from  (" + sql + " ) as q)p";
			strsql += " WHERE RN BETWEEN '" + pageSize * (pageIndex - 1) + "' AND '" + pageSize * pageIndex + "'";
			DataTable dtInfo = DataAccess.Instance("BizDB").ExecuteDataTable(strsql);
			
			EmployeeList.DataSource = dtInfo;
			EmployeeList.DataBind();

		}

		protected void AspNetPager1_PageChanged(object sender, EventArgs e)
		{
			repeaterbind();
		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			repeaterbind();
		}
	}
}