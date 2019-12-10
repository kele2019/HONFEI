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
	public partial class CostCenter : System.Web.UI.Page
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
			string sql = "select cosetcenter,Description from  dbo.PROC_PURCHASE_COSTCENTER";
			DataTable dtInfo = DataAccess.Instance("BizDB").ExecuteDataTable(sql);
			ItemList.DataSource = dtInfo;
			ItemList.DataBind();
		}
	}
}