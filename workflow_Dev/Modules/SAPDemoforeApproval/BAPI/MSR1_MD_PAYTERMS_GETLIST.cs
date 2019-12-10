using System;
using System.Collections.Generic;
using System.Text;
using SAP.Middleware.Connector;

namespace SAPDemoforeApproval
{
    public class MSR1_MD_PAYTERMS_GETLIST
    {
        public string GetPaymenttermName(string PI_LANGU, string PI_ZTERM)
        {
            RfcDestination destination1 = RfcDestinationManager.GetDestination(new SAPConfigConn().GetParameters("StaticSapCon1"));

            RfcRepository repo = destination1.Repository;
            IRfcFunction rfcFunc = repo.CreateFunction("MSR1_MD_PAYTERMS_GETLIST");

            rfcFunc.SetValue("PI_LANGU", PI_LANGU);
            rfcFunc.SetValue("PI_ZTERM", PI_ZTERM);

            //2.调用sap函数
            rfcFunc.Invoke(destination1); //execute the function in the backend

            try
            {
                //3.输出
                IRfcTable table = rfcFunc.GetTable("POT_PAYMENTTERMS");
                if (table.Count > 0)
                {
                    string text1 = table[0]["TEXT1"].GetString();
                    return text1;
                }
                return "";
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
