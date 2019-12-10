using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

namespace MobileClientBackground.BasePage
{
    public class BasePageClass : Page
    {
        protected override void InitializeCulture()
        {
            string Language = Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"].ToString().Split(',')[0];
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(Language);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Language);
            base.InitializeCulture();
        }

        //protected override void OnPreInit(EventArgs e)
        //{
        //    if (Session["UserInfo"] == null)
        //    {
        //        Response.Redirect("~/Login.aspx");
        //    }
        //    base.OnPreInit(e);
        //}

    }
}