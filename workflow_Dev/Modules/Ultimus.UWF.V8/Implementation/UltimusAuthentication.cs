using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ultimus.UWF.Security.Interface;
using ULTCONFIGURATIONLib; 
using MyLib;
using System.DirectoryServices;
using Ultimus.UWF.Security.Implementation;

namespace Ultimus.UWF.V8.Implementation
{
    public class UltimusAuthentication : DatabaseAuthentication
    {
        public override bool CheckUser(string loginName, string password)
        {
            return true;//TODO:自定义OC，如果没有AD，那么验证不通过
            Ultimus.OC.OrgChart oc = new Ultimus.OC.OrgChart();
            bool flag = oc.VerifyUser(loginName.Trim().Replace("\\", "/"), password);
            return flag;
        }

        static List<string> _domains = new List<string>();
        public virtual List<string> GetDomains()
        {
            return base.GetDomains();
            if (_domains.Count > 0)
            {
                return _domains;
            }
            byte num1;
            string[] array1;
            uint num2;
            Array array2;
            int num3;
            num1 = 0;
            array1 = null;
            num2 = 0;

            ConfigureClass oConfig = new ConfigureClass();
            array2 = ((Array)oConfig.GetDomains(num1, num2));
            if (array2.Length > 0)
            {
                array1 = new string[array2.Length];
                for (num3 = 0; (num3 < array2.Length); num3 = (num3 + 1))
                {
                    array1[num3] = array2.GetValue(num3).ToString();

                }
            }
            _domains.AddRange(array1);
            return _domains;
        }


        public void ChangePassword(string loginName, string oldPassword, string newPassword)
        {
            DirectoryEntry myDirectoryEntry;

            myDirectoryEntry = new DirectoryEntry(ConfigurationManager.AppSettings["ADPath"] + loginName.Replace("\\","/") + ",User");

            myDirectoryEntry.Invoke("setPassword", newPassword);

            myDirectoryEntry.CommitChanges();
        }
    }
}
