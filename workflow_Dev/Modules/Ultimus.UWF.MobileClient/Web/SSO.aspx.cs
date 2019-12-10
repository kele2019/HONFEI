using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientService;
using System.Text;
using System.Configuration;

namespace MobileClient.Web
{
    public partial class SSO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WorkflowRef services = new WorkflowRef();
            string account = Request.QueryString["loginName"];
            account = Base64Decode(account);
            string domain = ConfigurationManager.AppSettings["Domain"].ToString();
            if (account.IndexOf("/") == -1) { account = domain + "/" + account; }
            if (!string.IsNullOrEmpty(account))
            {
                account = Server.UrlDecode(account);
                Session["UserInfo"] = services.GetUserEntity(account);
                Session["Account"] = account;
                Response.Redirect("ToDoTask.aspx");
            }
        }

        public string Base64Decode(string Message)
        {
            byte[] bytes = Convert.FromBase64String(Message);
            return Encoding.Default.GetString(bytes);
        }  
    }
}