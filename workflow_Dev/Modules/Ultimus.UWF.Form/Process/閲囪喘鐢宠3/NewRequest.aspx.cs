using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data;
using Ultimus.UWF.Form.ProcessControl;
using MyLib;

namespace 采购申请3
{
    public partial class NewRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btn_ItemAdd_Click(object sender, EventArgs e)
        {
            UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            userInfo.AddNewRow(fld_detail_PROC_采购申请3_Item);
        }
        protected void fld_detail_PROC_采购申请3_Item_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            userInfo.DeleteRow(fld_detail_PROC_采购申请3_Item, e);
        }
    }
}