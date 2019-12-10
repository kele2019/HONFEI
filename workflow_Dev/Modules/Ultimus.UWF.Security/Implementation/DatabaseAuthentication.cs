using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ultimus.UWF.Security.Interface;
using MyLib;
using System.Data;

namespace Ultimus.UWF.Security.Implementation
{
    public class DatabaseAuthentication : IAuthentication
    {
        public virtual bool CheckUser(string loginName, string password)
        {
            string pwd=StringUtil.MD5Encrypt(password);
            object obj=DataAccess.Instance("BizDB").ExecuteScalar("SELECT COUNT(1) FROM ORG_USER WHERE LOGINNAME=@loginName AND PASSWORD=@password AND ISACTIVE=1", loginName, pwd);
            if (ConvertUtil.ToInt32(obj)>0)
            {
                return true;
            }
            return false;
        }

        public virtual List<string> GetDomains()
        {
            string str = ConfigurationManager.AppSettings["DomainList"];
            List<string> strs = new List<string>();
            strs.AddRange(str.Split(','));
            return strs;
        }

        public virtual void ChangePassword(string loginName, string oldPassword, string newPassword)
        {
            if (oldPassword != newPassword)
            {
                return;
            }
            DataAccess.Instance("BizDB").ExecuteNonQuery("UPDATE ORG_USER SET PASSWORD=@password WHERE LOGINNAME=@loginName AND ISACTIVE=1",newPassword, loginName);
            
        }
    }
}
