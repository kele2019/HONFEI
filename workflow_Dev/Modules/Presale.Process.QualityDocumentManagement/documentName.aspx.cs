using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using MyLib;
using Presale.Process.QualityDocumentManagement.Entity;

namespace Presale.Process.QualityDocumentManagement
{
	public partial class documentName : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			  
            if (!IsPostBack)
            {
				GetUserInfo();
			}
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			AspNetPager1.CurrentPageIndex = 1;
			GetUserInfo();
		}

		private void GetUserInfo()
		{
			string doctype = Request.QueryString["doctype"];
			string sql2 = "select d.DEPARTMENTNAME,u.USERCODE from ORG_USER u left join ORG_JOB j on u.USERID = j.USERID left join ORG_DEPARTMENT d on j.DEPARTMENTID = d.DEPARTMENTID where u.LoginName = '" + Page.User.Identity.Name + "'";
			UserCode user = DataAccess.Instance("BizDB").ExecuteEntity<UserCode>(sql2);
			string docLibraryName = null;
			string moniUserLoginName = null;
			string ownerLoginName = null;
			string folderUrl = null;
			int PageSize = AspNetPager1.PageSize;
			int PageIndex = (AspNetPager1.CurrentPageIndex)-1;
			if(doctype == "1")
			{
				docLibraryName += "CompanyDocument";
				moniUserLoginName += user.USERCODE;
				ownerLoginName += user.USERCODE;
			}
			if (doctype == "2")
			{
				docLibraryName += "DepartmentDocument";
				moniUserLoginName += user.USERCODE;
				ownerLoginName += user.USERCODE;
				folderUrl += user.DEPARTMENTNAME;
			}
			DocumentAPI.API api = new DocumentAPI.API();
			DocumentAPI.ItemPageOfDocumentEntity mode = api.QueryDocument(docLibraryName, moniUserLoginName, ownerLoginName, folderUrl, PageIndex, PageSize);
			DocumentAPI.DocumentEntity[] docarry = mode.Data;
			AspNetPager1.RecordCount = mode.Totle;
			Repeater1.DataSource = docarry;
			Repeater1.DataBind();
		}

		protected void AspNetPager1_PageChanged(object sender, EventArgs e)
		{
			GetUserInfo();
		}
	}
}