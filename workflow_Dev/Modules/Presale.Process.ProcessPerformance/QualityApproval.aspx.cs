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
	public partial class Quality : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string sql = "select DicCode,DicText,DicValue,Comments from COM_DICTIONRY where Type = 'HFPD-09' and Comments = '1' ";
				DepartmentEntity entity = DataAccess.Instance("BizDB").ExecuteEntity<DepartmentEntity>(sql);
				Process.Text = entity.DicCode;
				Measurement.Text = entity.DicText;
				Standard.Text = entity.DicValue;
				string sql3 = "select DicCode,DicText,DicValue,Comments from COM_DICTIONRY where Type = 'HFPD-09' and Comments = '2' ";
				DepartmentEntity entity3 = DataAccess.Instance("BizDB").ExecuteEntity<DepartmentEntity>(sql3);
				Process3.Text = entity3.DicCode;
				Measurement3.Text = entity3.DicText;
				standard3.Text = entity3.DicValue;
				string sql2 = "select DicCode,DicText,DicValue,Comments from COM_DICTIONRY where Type = 'HFPD-10'";
				DepartmentEntity entity2 = DataAccess.Instance("BizDB").ExecuteEntity<DepartmentEntity>(sql2);
				Process2.Text = entity2.DicCode;
				Measurement2.Text = entity2.DicText;
				Standard2.Text = entity2.DicValue;
			}
		}
	}
}