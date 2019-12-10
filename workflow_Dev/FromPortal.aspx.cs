using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using MyPortalService = UWF.WebServiceProxy;
using MyLib;
using Ultimus.UWF.Common.Logic;

namespace UWF
{
    public partial class FromPortal : System.Web.UI.Page
    {
        public string Ticket { get { return Request.QueryString["Ticket"]; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Ticket))
            {
                Response.Redirect(FormsAuthentication.DefaultUrl);
            }
			//MyPortalService.Service service = new MyPortalService.Service();
			//service.Url = ConfigurationManager.AppSettings["SSOWebService"];
			SSOLogin.API ssologin = new SSOLogin.API();
			var rep=ssologin.GetUserAccountByTicket(Ticket);
          //  var rep = service.GetUserAccountByTicket(Ticket);
            if (rep.Code != 0)
            {
                //Response.Redirect(FormsAuthentication.DefaultUrl);
                Response.Redirect(ConfigurationManager.AppSettings["Errorpage"]);
            }
			object FormURL = Request.QueryString["FormURL"];
            string domian = ConfigurationManager.AppSettings["Domain"];
			SessionLogic.LogOut();
		object UserAccount=DataAccess.Instance("BizDB").ExecuteScalar(" select top (1) LOGINNAME from ORG_USER where USERCODE='" + rep.Data + "'");
			if(UserAccount!=null)
            SessionLogic.Login(UserAccount.ToString());
			else
				Response.Redirect(ConfigurationManager.AppSettings["Errorpage"]);
			if (FormURL != null)
				Response.Redirect(FormURL.ToString());
			else
				Response.Redirect(ConfigurationManager.AppSettings["FromPortalGotoDefaultPageUrl"]);
        }
    }
}