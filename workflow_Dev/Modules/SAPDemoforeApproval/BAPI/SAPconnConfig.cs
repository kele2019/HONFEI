using System;
using System.Collections.Generic;
using System.Text;
using SAP.Middleware.Connector;
using System.Web.Configuration;

namespace SAPDemoforeApproval
{
    public class SAPConfigConn : IDestinationConfiguration
    {
        public RfcConfigParameters GetParameters(String destinationName)
        {
            //if(!object.Equals(WebConfigurationManager.ConnectionStrings[destinationName],null)) 
            //{ 
            //retrieve parameters in connection string 
            string[] paramsstr = "AppServerHost=172.16.251.225;Client=900;Password=006235;SystemNumber=00;UserName=zhangxh;Language=en".Split(';');  //;WebConfigurationManager.ConnectionStrings[destinationName].ConnectionString.Split(';'); 
//            IP地址：172.16.104.155
//系统编号：00
//系统标识：DAQ
//客户端：800
//账号：TEST
//密码：test4321

            RfcConfigParameters parms = new RfcConfigParameters();
            parms.Add(RfcConfigParameters.Name, destinationName);
            foreach (string str in paramsstr)
            {
                string[] strpar = str.Split('=');

                switch (strpar[0])
                {
                    case "AppServerHost": parms.Add(RfcConfigParameters.AppServerHost, strpar[1]); break;
                    case "Client": parms.Add(RfcConfigParameters.Client, strpar[1]); break;
                    case "SystemNumber": parms.Add(RfcConfigParameters.SystemNumber, strpar[1]); break;
                    case "Language": parms.Add(RfcConfigParameters.Language, strpar[1]); break;
                    case "UserName": parms.Add(RfcConfigParameters.User, strpar[1]); break;
                    case "Password": parms.Add(RfcConfigParameters.Password, strpar[1]); break;
                    //case "AppServerHost": parms.Add(RfcConfigParameters.AppServerHost, "172.16.104.155"); break;
                    //case "Client": parms.Add(RfcConfigParameters.Client, "800"); break;
                    //case "SystemNumber": parms.Add(RfcConfigParameters.SystemNumber, "00"); break;
                    //case "Language": parms.Add(RfcConfigParameters.Language, "zh"); break;
                    //case "UserName": parms.Add(RfcConfigParameters.User, "TEST"); break;
                    //case "Password": parms.Add(RfcConfigParameters.Password, "test4321"); break;
                }
            }

            return parms;
            //else return null; 

        }

        public bool ChangeEventsSupported() { return false; }
        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;

    }
}
