using System;
using System.Collections.Generic;
using System.Text;
using SAP.Middleware.Connector;
using System.Windows.Forms;

namespace SAPDemoforeApproval
{
    public class BAPI_CUSTOMER_GETDETAIL2
    {
        public string GetCustomerName(string customerNo,string companyCode)
        {
            RfcDestination destination1 = RfcDestinationManager.GetDestination(new SAPConfigConn().GetParameters("StaticSapCon1"));

            RfcRepository repo = destination1.Repository;
            IRfcFunction rfcFunc = repo.CreateFunction("BAPI_CUSTOMER_GETDETAIL2");

            rfcFunc.SetValue("CUSTOMERNO", customerNo);
            rfcFunc.SetValue("COMPANYCODE", companyCode);

            //2.调用sap函数
            rfcFunc.Invoke(destination1); //execute the function in the backend

            try
            {
                //3.输出
                IRfcStructure customer_address = rfcFunc.GetStructure("CUSTOMERADDRESS");
                //Console.WriteLine("客户名称:" + customer_address["NAME"].GetString());
                return customer_address["NAME"].GetString();

            }
            catch (Exception e)
            {
                //Console.WriteLine("Error:" + e.Message);
                MessageBox.Show(e.Message);
                throw e;
            }

        }
    }
}
