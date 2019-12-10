using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Ultimus.UWF.Common.Logic
{
    public class SessionLogic
    {
        /// <summary>
        /// 获取当前登录用户名
        /// </summary>
        /// <returns></returns>
        public static string GetLoginName()
        {
            string user = HttpContext.Current.User.Identity.Name;
            if (string.IsNullOrEmpty(user))
            {
                if (HttpContext.Current.Session["loginName"] != null)
                {
                    user = HttpContext.Current.Session["loginName"].ToString();
                }
            }

            if (string.IsNullOrEmpty(user))
            {
                FormsAuthentication.RedirectToLoginPage();
            }

            return user;
        }

        public static string GetUltimusLoginName()
        {
            return GetLoginName().Replace("\\","/");
        }

        /// <summary>
        /// 设置当前登录用户
        /// </summary>
        /// <param name="loginName"></param>
        public static void Login(string loginName)
        {
            HttpContext.Current.Session["loginName"] = loginName;
            FormsAuthentication.SetAuthCookie(loginName, false);
        }

        /// <summary>
        /// 注销用户
        /// </summary>
        public static void LogOut()
        {
            HttpContext.Current.Session["loginName"] = null;
            HttpContext.Current.Session["LoginUser"] = null;
            FormsAuthentication.SignOut();
        }
    }
}
