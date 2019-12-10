using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Resources;

namespace Ultimus.UWF.Home2.Code
{
    public class LanguageHelper
    {
        public static string Get(string key)
        {
            ResourceManager resource = null;
            string l = "";

            HttpCookie cookie = HttpContext.Current.Request.Cookies["ClientLanguage"];
            if (cookie != null)
            {
                l = (cookie.Value + string.Empty).ToLower();
            }
            if (string.IsNullOrEmpty(l))
            {
                l = HttpContext.Current.Request.UserLanguages[0].ToLower();
            }

            if (l == "zh-cn")
            {
                resource = Resources.zh_cn.ResourceManager;
            }
            else
            {
                resource = Resources.en_us.ResourceManager;
            }

            return resource.GetString(key);
        }
    }
}