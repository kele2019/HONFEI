using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Data;

namespace Presale.Process.QualityDocumentManagement
{
	public partial class DepartmentAndQualityManagerApproval : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				repeatebind();
			}
		}

		private void repeatebind()
		{
			//DataTable dtUserInfo = DataAccess.Instance("BizDB").ExecuteDataSet("select DEPARTMENTNAME FROM ORG_DEPARTMENT where PARENTID=1").Tables[0];
			//DataTable UserData = new DataTable();
			//UserData.Columns.Add("USER1", typeof(string));
			//UserData.Columns.Add("USER2", typeof(string));
			//UserData.Columns.Add("USER3", typeof(string));
			//if (dtUserInfo.Rows.Count > 0)
			//{
			//    int UserCount = dtUserInfo.Rows.Count;
			//    for (int i = 0; i < UserCount; i++)
			//    {
			//        UserData.Rows.Add(UserData.NewRow());
			//        DataRow DRUSER = UserData.Rows[(i / 3)];
			//        if ((UserCount - 1) > i) DRUSER["USER1"] = dtUserInfo.Rows[i]["DEPARTMENTNAME"];
			//        if ((UserCount - 1) > i) DRUSER["USER2"] = dtUserInfo.Rows[++i]["DEPARTMENTNAME"];
			//        if ((UserCount - 1) > i) DRUSER["USER3"] = dtUserInfo.Rows[++i]["DEPARTMENTNAME"];

			//    }
			//}
			//Repeaterlist.DataSource = UserData;
			//Repeaterlist.DataBind();

		}
	}
}