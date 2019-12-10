using System;
using System.Collections.Generic;
using System.Text;

namespace Ultimus.UWF.Security.Interface
{
    /// <summary>
    /// 用户验证
    /// </summary>
    public interface IAuthentication
    {
        /// <summary>
        /// 校验用户密码
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool CheckUser(string loginName,string password);

        /// <summary>
        /// 获取系统登录的域名
        /// </summary>
        /// <returns></returns>
        List<string> GetDomains();

        void ChangePassword(string loginName, string oldPassword, string newPassword);

    }
}
