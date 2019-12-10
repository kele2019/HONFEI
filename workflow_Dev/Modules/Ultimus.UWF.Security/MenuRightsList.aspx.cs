using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Ultimus.UWF.Common.Logic;
using Ultimus.UWF.Security.Logic;
using Ultimus.UWF.Security.Entity;

namespace Ultimus.UWF.Security
{
    public partial class MenuRightsList : System.Web.UI.Page
    {
        SecurityLogic _logic = new SecurityLogic();
        public string SecurityList_ConfirmDelete = Lang.Get("SecurityList_ConfirmDelete");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();

                //AspNetPager1.FirstPageText = Portal.Resources.lang.FirstPage;
                //AspNetPager1.PrevPageText = Portal.Resources.lang.PrevPage;
                //AspNetPager1.NextPageText = Portal.Resources.lang.NextPage;
                //AspNetPager1.LastPageText = Portal.Resources.lang.LastPage;
                btnAdd.Text = Lang.Get("SecurityList_Add");
            }
        }

        List<MenuRightsEntity> GetList()
        {
            List<MenuRightsEntity> list = new List<MenuRightsEntity>();
            object obj = ViewState["list"];
            if (obj == null)
            {
                obj = _logic.GetList();
                ViewState["list"] = obj;
            }
            return (List<MenuRightsEntity>)ViewState["list"];
        }

        List<MenuRightsEntity> GetList(List<MenuRightsEntity> list, int skip, int max)
        {
            List<MenuRightsEntity> temp = new List<MenuRightsEntity>();
            for (int i = skip; i < skip + max; i++)
            {
                if (list.Count > i)
                {
                    temp.Add(list[i]);
                }
            }
            return temp;
        }


        void BindGrid()
        {
            int skipResults = (AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize;
            int maxResults = AspNetPager1.PageSize;
            AspNetPager1.RecordCount = GetList().Count;
            rptList.DataSource = GetList(GetList(), skipResults, maxResults);
            rptList.DataBind();
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string str = e.CommandArgument.ToString();
                _logic.Delete(str);
                ViewState["list"] = null;
                BindGrid();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+Lang.Get("SecurityList_DeleteSuccess")+"!');", true);
            }
        }
    }
}