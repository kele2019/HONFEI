using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ultimus.UWF.Security.Interface;
using ULTCONFIGURATIONLib;
using MyLib; 

namespace Ultimus.UWF.V7.Implementation
{
    public class UltimusAuthentication : Ultimus.UWF.Security.Implementation.DatabaseAuthentication
    {
        public override bool CheckUser(string loginName, string password)
        {
            Ultimus.OC.OrgChart oc = new Ultimus.OC.OrgChart();
            bool flag = oc.VerifyUser(loginName.Trim().Replace("\\", "/"), password);
            return flag;
        }

        static List<string> _domains = new List<string>();
        public override List<string> GetDomains()
        {
            if (_domains.Count > 0)
            {
                return _domains;
            }
            byte num1;
            string[] array1;
            int num2;
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
            return _domains; ;
        }

    }
}
