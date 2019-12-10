using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;

namespace Presale.Process.TravelExpenseReport
{
	public partial class Project : System.Web.UI.Page
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
			string sql = "select PROJECT as ProjectValue,DESCRIPTION from  dbo.PROC_PURCHASE_PROJECT";
			DataTable dtInfo = DataAccess.Instance("BizDB").ExecuteDataTable(sql);
			ProjectList.DataSource = dtInfo;
			ProjectList.DataBind();
		}
	}
}