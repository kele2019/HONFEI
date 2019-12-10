using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileClient
{
    public partial class Header : System.Web.UI.UserControl
    {
        public string lt1 = "lt4";
        public string lt2 = "lt4";
        public string lt3 = "lt4";
        public string nav1 = "nav03";
        public string nav2 = "nav03";
        public string nav3 = "nav03";
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = this.Page.Request.RawUrl;
            if (url.ToUpper().IndexOf("NEWTASK") >= 0)
            {
                litTitle.Text = Resources.Resource.NewTask_NewTask;
                lt1 = "lt3";
                nav1 = "nav01";
            }

            if (url.ToUpper().IndexOf("TODOTASK") >= 0)
            {
                litTitle.Text = Resources.Resource.NewTask_ToDoTask;
                lt2 = "lt3";
                nav2 = "nav01";
            }

            if (url.ToUpper().IndexOf("ALREADYDOTASK") >= 0)
            {
                litTitle.Text = Resources.Resource.NewTask_AlreadyDoTask;
                lt3 = "lt3";
                nav3 = "nav01";
            }
        }

    }
}