using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using MyLib;

namespace Ultimus.UWF.Common.Logic
{
    public class WebUtil
    {
        public static string GetRootPath()
        {
            if (HttpContext.Current == null)
            {
                return ConfigurationManager.AppSettings["RootPath"];
            }
            else 
            {
                if (HttpContext.Current.Request.Url.Port == 80)
                {
                    return "http://" + HttpContext.Current.Request.Url.Host + "/UWF";
                }
                else
                {
                    return "http://" + HttpContext.Current.Request.Url.Host + ":" +
                      HttpContext.Current.Request.Url.Port;
                }
            }
        }
    }
}
