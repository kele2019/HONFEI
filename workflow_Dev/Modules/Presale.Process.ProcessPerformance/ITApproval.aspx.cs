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
	public partial class ITApproval : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string sql1 = "select DicCode,DicText,DicValue,Comments from COM_DICTIONRY where Type = 'HFPD-06' and Comments = '1'";
				string sql2 = "select DicCode,DicText,DicValue,Comments from COM_DICTIONRY where Type = 'HFPD-06' and Comments = '2'";
				string sql3 = "select DicCode,DicText,DicValue,Comments from COM_DICTIONRY where Type = 'HFPD-06' and Comments = '3'";
				DepartmentEntity entity1 = DataAccess.Instance("BizDB").ExecuteEntity<DepartmentEntity>(sql1);
				DepartmentEntity entity2 = DataAccess.Instance("BizDB").ExecuteEntity<DepartmentEntity>(sql2);
				DepartmentEntity entity3 = DataAccess.Instance("BizDB").ExecuteEntity<DepartmentEntity>(sql3);
				Process1.Text = entity1.DicCode;
				Measurement1.Text = entity1.DicText;
				Standard1.Text = entity1.DicValue;
				Process2.Text = entity2.DicCode;
				Measurement2.Text = entity2.DicText;
				Standard2.Text = entity2.DicValue;
				Process3.Text = entity3.DicCode;
				Measurement3.Text = entity3.DicText;
				Standard3.Text = entity3.DicValue;
			}
		}
	}
}