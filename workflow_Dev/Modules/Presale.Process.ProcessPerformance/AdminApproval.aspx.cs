using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presale.Process.ProcessPerformance.Entity;
using MyLib;

namespace Presale.Process.ProcessPerformance
{
	public partial class AdminApproval : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string sql = "select DicCode,DicText,DicValue,Comments from COM_DICTIONRY where Type = 'HFPD-08'";
				DepartmentEntity entity = DataAccess.Instance("BizDB").ExecuteEntity<DepartmentEntity>(sql);
				Process.Text = entity.DicCode;
				Measurement.Text = entity.DicText;
				Standard.Text = entity.DicValue;
			}
		}
	}
}