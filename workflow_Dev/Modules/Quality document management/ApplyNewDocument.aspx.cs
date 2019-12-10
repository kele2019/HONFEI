using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Ultimus.UWF.FunctionManager.Quality_document_management
{
    public partial class ApplyNewDocument : System.Web.UI.Page
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
               
            }
        }
       
    }
}