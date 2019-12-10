using System;
using System.Collections.Generic;
using System.Web;

namespace MobileClient.BasePageClass
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void InitializeCulture()
        {
            string Language = Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"].ToString().Split(',')[0];
            if (Session["Language"] != null)
            {
                Language = Session["Language"].ToString();
            }
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(Language);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Language);
            base.InitializeCulture();
        }

        protected override void OnPreInit(EventArgs e)
        {
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("~/Web/Login.aspx");
            }
            base.OnPreInit(e);
        }

    }
}