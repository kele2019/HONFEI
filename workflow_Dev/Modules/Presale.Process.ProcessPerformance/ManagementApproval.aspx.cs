using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Presale.Process.ProcessPerformance.Entity;

namespace Presale.Process.ProcessPerformance
{
	public partial class ManagementApproval : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string sql = "select DicCode,DicText,DicValue,Comments from COM_DICTIONRY where Type = 'HFPD-01'";
				DepartmentEntity entity = DataAccess.Instance("BizDB").ExecuteEntity<DepartmentEntity>(sql);
				process.Text = entity.DicCode;
				Measurement.Text = entity.DicText;
				Standard.Text = entity.DicValue;
			}
		}
	}
}