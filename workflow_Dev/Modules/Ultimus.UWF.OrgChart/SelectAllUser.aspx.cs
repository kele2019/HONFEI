using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using MyLib;
namespace Ultimus.UWF.OrgChart
{
    public partial class SelectAllUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetUserInfo();
            }
        }
        public void GetUserInfo()
        {
                StringBuilder StrSQLCount = new StringBuilder();
                StringBuilder StrSQLList = new StringBuilder();
                int PageSize=AspNetPager1.PageSize;
                int PageIndex=AspNetPager1.CurrentPageIndex;
                StrSQLCount.Append("select count(1) from  ORG_USER where 1=1 ");
                StrSQLList.Append("select * from( select UserName,EXT04,Email,REPLACE(LOGINNAME,'\\','/') EmployeeID,UserID, ROW_NUMBER()  over(order by UserName) RN from  ORG_USER where 1=1 ");
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    string strwhere=txtSearch.Text.Trim();
                    StrSQLCount.AppendFormat(" and( USERNAME like '%{0}%' or EXT04 like '%{0}%' or LOGINNAME like '%{0}%')",strwhere);
                    StrSQLList.AppendFormat(" and( USERNAME like '%{0}%' or EXT04 like '%{0}%' or LOGINNAME like '%{0}%')", strwhere);
                }
                StrSQLList.Append(") A where RN between '"+PageSize*(PageIndex-1)+"' and '"+PageSize*PageIndex+"' ");
                int PageCount =int.Parse(DataAccess.Instance("BizDB").ExecuteScalar(StrSQLCount.ToString()).ToString());
                AspNetPager1.RecordCount = PageCount;
                DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(StrSQLList.ToString());
                Repeater1.DataSource = dt.DefaultView;
                Repeater1.DataBind();
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            GetUserInfo();
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            GetUserInfo();
        }
    }
}