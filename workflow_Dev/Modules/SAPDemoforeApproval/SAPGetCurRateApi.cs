using System;
using System.Collections.Generic;
using System.Text;
using SAP.Middleware.Connector;
namespace SAPDemoforeApproval
{
    public class SAPGetCurRateApi
    {
        public string SAPGetCurRateApiX(string sCurCurrency) 
        {
            string sRetu = string.Empty;
            RfcDestination destination1 = RfcDestinationManager.GetDestination(new SAPConfigConn().GetParameters("StaticSapCon1"));
            RfcRepository repo = destination1.Repository;
            IRfcFunction rfcFunc = repo.CreateFunction("ZIF_GET_BCE");
            rfcFunc.SetValue("EXCURR", sCurCurrency);

            //2.调用sap函数
            rfcFunc.Invoke(destination1); //execute the function in the backend

            try
            {
                //3.输出
                object oObject = rfcFunc.GetValue("RATE");
                if (oObject != null)
                {
                    sRetu = oObject.ToString(); ;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sRetu;
        }
    }
}
