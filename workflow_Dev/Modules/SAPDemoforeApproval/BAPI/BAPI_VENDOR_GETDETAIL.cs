using System;
using System.Collections.Generic;
using System.Text;
using SAP.Middleware.Connector;

namespace SAPDemoforeApproval
{
    public class BAPI_VENDOR_GETDETAIL
    {
        public string GetVendorName(string vendorNo, string companyCode)
        {
            RfcDestination destination1 = RfcDestinationManager.GetDestination(new SAPConfigConn().GetParameters("StaticSapCon1"));

            RfcRepository repo = destination1.Repository;
            IRfcFunction rfcFunc = repo.CreateFunction("BAPI_VENDOR_GETDETAIL");

            rfcFunc.SetValue("VENDORNO", vendorNo);
            rfcFunc.SetValue("COMPANYCODE", companyCode);

            //2.调用sap函数
            rfcFunc.Invoke(destination1); //execute the function in the backend

            try
            {
                //3.输出
                IRfcStructure result = rfcFunc.GetStructure("GENERALDETAIL");
                return result["NAME"].GetString();
               

            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
