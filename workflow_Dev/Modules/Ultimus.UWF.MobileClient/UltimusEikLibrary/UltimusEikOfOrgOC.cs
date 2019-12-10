using System;
using System.Collections.Generic;
using EntityLibrary;
using Ultimus.OC;
using System.Configuration;

namespace UltimusEikLibrary
{
    public class UltimusEikOfOrgOC
    {
        private PublicFunctionClass pfc = new PublicFunctionClass();

        /// <summary>
        /// 登陆效验
        /// </summary>
        /// <param name="UserName">用户账号（带域名）</param>
        /// <param name="PassWord">密码</param>
        /// <returns>bool</returns>
        public UserInfo CheckUser(string UserName, string PassWord)
        {
            UserInfo info = new UserInfo();
            try
            {
                OrgChart oc = new OrgChart();
                //根据w2验证密码
                bool flag = false;
                string passauth = ConfigurationManager.AppSettings["PassAuth"];
                
                    flag = oc.VerifyUser(UserName, PassWord);
                
                if (flag)
                {
                    User pUser = new User();
                    oc.FindUser(UserName, "", 0, out pUser);
                    info.UserAccount = pUser.strUserName;
                    if (string.IsNullOrEmpty(pUser.strUserFullName))
                    {
                        info.UserFullName = UserName;
                    }
                    else
                    {
                        info.UserFullName = pUser.strUserFullName;
                    }
                    info.UserDepartment = pUser.strDepartmentName;
                    info.JobFunction = pUser.strJobFunction;
                }
                else
                {
                    info.UserFullName = "";
                }
            }
            catch (Exception ex)
            {
                pfc.WriteLogOfTxt(ex.Message);
            }
            return info;
        }
    }
}
