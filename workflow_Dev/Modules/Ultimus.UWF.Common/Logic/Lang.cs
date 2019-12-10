using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyLib;
using System.Xml;
using System.Data;
using System.IO;
using System.Web;
using System.Collections.ObjectModel;
using System.CodeDom.Compiler;
using Ultimus.UWF.Common.Entity;

namespace Ultimus.UWF.Common.Logic
{
    public class Lang
    {
        public static string Get(string name)
        {
            List<LangEntity> langs = DataAccess.Instance("BizDB").GetList<LangEntity>("Lang_Get", null);
            string str = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            string value = null;
            LangEntity lang = null;

            string[] sz = name.Split('_');
            if (sz.Length > 1)
            {
                lang = langs.Find(p => p.NAME.Equals(name));
            }
            if (lang == null)
            {
                lang = langs.Find(p => ConvertUtil.ToString(p.MODULE).ToUpper() + "_" + ConvertUtil.ToString(p.FORMNAME).ToUpper() + "_" + p.NAME.ToUpper() == name.ToUpper());
            }
            if (lang == null)
            {
                lang = langs.Find(p => p.NAME.Equals(sz[sz.Length - 1]));
                name = sz[sz.Length - 1];
            }
            if (lang != null)
            {
                switch (str.ToUpper())
                {
                    case "ZH-CN":
                        value = lang.ZH_CN;
                        break;
                    case "JA":
                        value = lang.JA;
                        break;
                    default:
                        value = lang.EN_US;
                        break;
                }
            }
            
            if (string.IsNullOrEmpty(value))
            {
                return name;
            }
            else
            {
                return value;
            }
        }
    }
}
