using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Ultimus.UWF.FunctionManager.Quality_document_management
{
    public partial class DepartAndQualityManageapprove : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string STRUSER = Page.User.Identity.Name;
                object ProcessName = Request.QueryString["Processname"];
                object myRequest = Request.QueryString["Type"];
                object Incident = Request.QueryString["Incident"];
                object formid = Request.QueryString["FORMID"];
                repeatebind();
            }
        }
        private void repeatebind()
        {
            Business bu = new Business();
            DataTable dtUserInfo = bu.getdepartmentlist();
            DataTable UserData = new DataTable();
            UserData.Columns.Add("USER1", typeof(string));
            UserData.Columns.Add("USER2", typeof(string));
            UserData.Columns.Add("USER3", typeof(string));
            if (dtUserInfo.Rows.Count > 0)
            {
                int UserCount = dtUserInfo.Rows.Count;
                for (int i = 0; i < UserCount; i++)
                {
                    UserData.Rows.Add(UserData.NewRow());
                    DataRow DRUSER = UserData.Rows[(i / 3)];
                    if ((UserCount - 1) > i) DRUSER["USER1"] = dtUserInfo.Rows[i]["departmentname"];
                    if ((UserCount - 1) > i) DRUSER["USER2"] = dtUserInfo.Rows[++i]["departmentname"];
                    if ((UserCount - 1) > i) DRUSER["USER3"] = dtUserInfo.Rows[++i]["departmentname"];

                }
            }
            Repeaterlist.DataSource = UserData;
            Repeaterlist.DataBind();

        }

        protected void Repeaterlist_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            { 
                
            }
        }
    }
}