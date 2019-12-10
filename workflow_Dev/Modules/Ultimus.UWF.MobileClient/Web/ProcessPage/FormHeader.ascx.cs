using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;

namespace MobileClient
{
    public partial class FormHeader : System.Web.UI.UserControl
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

            if (url.ToUpper().IndexOf("APPROVALHISTORY") >= 0)
            {
                litTitle.Text = Resources.Resource.Form_FlowChart;
                lt2 = "lt3";
                nav2 = "nav01";
            }

            else if (url.ToUpper().IndexOf("FLOWCHART") >= 0)
            {
                litTitle.Text = Resources.Resource.Form_ApprovalHistory;
                lt3 = "lt3";
                nav3 = "nav01";
            }
            else
            {
               
                    litTitle.Text = Resources.Resource.Form_Text;
                    lt1 = "lt3";
                    nav1 = "nav01";
               
            }

            if (ConvertUtil.ToInt32(Request.QueryString["Incident"]) == 0)
            {
                lt2 = "hide";
                lt3 = "hide";
            }
        }

    }
}