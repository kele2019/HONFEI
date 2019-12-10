using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Ultimus.UWF.Common.Logic;

namespace Ultimus.UWF.Home2.Code
{
    public class BasePage : Page
    {
        protected string UserID
        {
            get
            {
                return SessionLogic.GetLoginName();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.Cache.SetNoStore();
        }

        protected DateTime GetStartTime()
        {
            return new DateTime(2010, 1, 1);
        }
        protected DateTime GetEndTime()
        {
            return new DateTime(2099, 1, 1);
        }
    }
}