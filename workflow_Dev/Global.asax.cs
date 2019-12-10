using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using MyLib;

namespace WebPortal
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //try
            //{
            //    if (Request.Cookies["lang"] != null)
            //    {
            //        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(Request.Cookies["lang"].Value.ToString());
            //        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Request.Cookies["lang"].Value.ToString());
            //    }
            //}
            //catch
            //{
            //}
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {            
            if (HttpContext.Current.Session != null)
            {
                Exception objErr = Server.GetLastError().GetBaseException();
                LogUtil.Error(objErr);
                HttpContext.Current.Session["Exception"] = objErr;
                //Server.Transfer("~/Moduels/Home/Error.aspx");
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}