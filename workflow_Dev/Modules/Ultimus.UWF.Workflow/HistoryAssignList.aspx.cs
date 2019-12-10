using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;

namespace Ultimus.UWF.Workflow
{
    public partial class HistoryAssignList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetBindDate();
            }
        }
        public void GetBindDate()
        {
            int PageIndex = AspNetPager1.CurrentPageIndex - 1;
            int PageSize = AspNetPager1.PageSize;
            string StrsqlCount = "select COUNT(1) from dbo.COM_ASSGININFO where 1=1 ";// TaskUser='"+Page.User.Identity.Name.Replace('\\','/')+"'";
            AspNetPager1.RecordCount = Convert.ToInt32(DataAccess.Instance("BizDB").ExecuteScalar(StrsqlCount));

            string Strsql = @"select ID,RN,( SELECT EXT04 FROM ORG_USER WHERE USERID=(select USERID from dbo.V_ORG_USER where LOGINNAME=TaskUser)) AssginTaskUser, 
 ( SELECT EXT04 FROM ORG_USER WHERE USERID=(select USERID from dbo.V_ORG_USER where LOGINNAME=AssginTaskUser)) AssginTaskTo,
 AssginStartDate,AssginEndDate,Comments,CreateDate from (
select ROW_NUMBER() over(order by ID desc) RN, * from dbo.COM_ASSGININFO where 1=1) A where RN between '" + (PageIndex*PageSize+1)+"' and '"+PageSize*(PageIndex+1)+"'  ";
            DataTable dtAssginlist=DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            if (dtAssginlist.Rows.Count > 0)
                RPList.DataSource = dtAssginlist;
            else
            {
                lbText.Text = "No Data";
                AspNetPager1.Visible = false;
                RPList.DataSource = null;
            }
            RPList.DataBind();
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            GetBindDate();
        }

    }
}