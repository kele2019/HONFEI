using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presale.Process.AssetBorrowing
{
	public partial class ApprovalConfrim : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			object objtype = Request.QueryString["type"];
			if (objtype != null)
			{
				if (objtype.ToString() == "myapproval")
				{
					btnDiv.Visible = false;
				}
				else
				{
					if (objtype.ToString() == "mytask")
					{
						btnDiv.Visible = true;
					}
				}
			}
		}
	}
}