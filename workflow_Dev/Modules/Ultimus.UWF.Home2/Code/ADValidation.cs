using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;
namespace Ultimus.UWF.Home2.Code
{
    public class ADValidation
    {
    
            private static string _ADROOTPATH = string.Empty;
            /// <summary>
            /// 获取AD域路径
            /// </summary>
            private static string ADROOTPATH
            {
                get
                {
                    if (_ADROOTPATH == string.Empty)
                    {
                        _ADROOTPATH = System.Configuration.ConfigurationManager.AppSettings["ADRootGroupPath"];
                        if (_ADROOTPATH == string.Empty)
                        {
                            throw new Exception("没有配置AD路径");
                        }
                    }
                    return _ADROOTPATH;
                }
            }

            /// <summary>
            /// 验证AD用户是否登录成功
            /// </summary>
            /// <param name="userName">用户名</param>
            /// <param name="password">密码</param>
            /// <returns>返回登录是否成功</returns>
            public static bool TryAuthenticate(string userName, string password)
            {
                return TryAuthenticate(ADROOTPATH, userName, password);
            }
            /// <summary>
            /// 验证AD用户是否登录成功
            /// </summary>
            /// <param name="domain">要认证AD域路径</param>
            /// <param name="userName">用户名</param>
            /// <param name="password">密码</param>
            /// <returns>返回登录是否成功</returns>
            public static bool TryAuthenticate(string domain, string userName, string password)
            {
                if (string.Empty.Equals(domain) || string.Empty.Equals(userName) || string.Empty.Equals(password))
                {
                    return false;
                }
                bool isLogin = false;
                try
                {
                    DirectoryEntry entry = new DirectoryEntry(domain, userName, password);
                    entry.RefreshCache();
                    isLogin = true;
                }
                catch
                {
                    isLogin = false;
                }
                return isLogin;
            }
        
    }
}