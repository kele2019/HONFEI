using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;

namespace Presale.Process.LocalExpense
{
	public partial class Subject1 : System.Web.UI.Page
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
			string sql = "select * from  dbo.PROC_LocalExpence_Subject";
			DataTable dtInfo = DataAccess.Instance("BizDB").ExecuteDataTable(sql);
			SubjectDetail.DataSource = dtInfo;
			SubjectDetail.DataBind();
		}
	}
}
