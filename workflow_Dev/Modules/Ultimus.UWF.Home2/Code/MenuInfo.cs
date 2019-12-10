using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ultimus.UWF.Home2.Code
{
    public class MenuInfo
    {
        public string Code { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }

        public MenuInfo(string code, string url)
        {
            Code = code;
            Url = url;
        }
        public MenuInfo()
        {
        }
    }
}