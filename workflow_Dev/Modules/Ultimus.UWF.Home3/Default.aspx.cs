using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Ultimus.UWF.Security.Logic;
using Ultimus.UWF.Security.Entity;
using Ultimus.UWF.Common.Logic;
using Ultimus.UWF.OrgChart.Interface;

namespace Ultimus.UWF.Home3
{
    public partial class Default : System.Web.UI.Page
    {
        public string Default_LogoutConfirm = "";
        public string User_FullName = "";
        MenuLogic _menu = new MenuLogic();
        IOrg _org = ServiceContainer.Instance().GetService<IOrg>();

        public string DEFAULT_MYTASK = "";

        List<MenuEntity> _list;
        protected void Page_Load(object sender, EventArgs e)
        {
            Default_LogoutConfirm = Lang.Get("Default_LogoutConfirm");
            User_FullName = _org.GetUserEntity(SessionLogic.GetLoginName()).USERNAME;
            _list = _menu.GetMenuList(SessionLogic.GetLoginName());

            List<MenuEntity> toplist=_list.FindAll(p => p.PARENTID.Trim() == "0");
            rptTop.DataSource = toplist;
            rptTop.DataBind();
            rptTop1.DataSource = toplist;
            rptTop1.DataBind();
        }

        protected void rptTop1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rpControl = (Repeater)e.Item.FindControl("rptTop2");
                MenuEntity ety = (MenuEntity)e.Item.DataItem;
                List<MenuEntity> list= _list.FindAll(p => p.PARENTID.Trim() == ety.MENUID.Trim());

                rpControl.DataSource = list;
                rpControl.DataBind();
            }
        }

        protected void rptTop2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rpControl = (Repeater)e.Item.FindControl("rptTop3");
                MenuEntity ety = (MenuEntity)e.Item.DataItem;
                List<MenuEntity> list = _list.FindAll(p => p.PARENTID.Trim() == ety.MENUID.Trim());

                rpControl.DataSource = list;
                rpControl.DataBind();
            }
        }

    }
}