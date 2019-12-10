using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Data;

namespace Ultimus.UWF.Home2
{
    public partial class OtherPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetMeanuBindData();
            }
        }

        public void GetMeanuBindData()
        {
            string MID = Request.QueryString["MID"].ToString();
            string MeanuSql = string.Format(@"select M.* from (select ICON,URL,MENUNAME,MENUID from  dbo.SEC_MENU where PARENTID='{0}') M left join 
SEC_MENURIGHTSMEMBER MM on M.MENUID=MM.RIGHTSID where MEMBERID=(select USERID from ORG_USER where LOGINNAME='{1}')", MID, Page.User.Identity.Name);
            DataTable dtMeaun = DataAccess.Instance("BizDB").ExecuteDataTable(MeanuSql);
            if (dtMeaun.Rows.Count > 0)
                RpList.DataSource = dtMeaun;
            else
                RpList.DataSource = null;
            RpList.DataBind();
        }

    }
}