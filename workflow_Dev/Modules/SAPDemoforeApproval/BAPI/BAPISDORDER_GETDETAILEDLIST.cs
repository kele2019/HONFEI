using System;
using System.Collections.Generic;
using System.Text;
using SAP.Middleware.Connector;

namespace SAPDemoforeApproval
{
    public class BAPISDORDER_GETDETAILEDLIST
    {
        public SalesOrder GetSalesOrder(string VBELN)
        {
            RfcDestination destination1 = RfcDestinationManager.GetDestination(new SAPConfigConn().GetParameters("StaticSapCon1"));

            RfcRepository repo = destination1.Repository;
            IRfcFunction rfcFunc = repo.CreateFunction("BAPISDORDER_GETDETAILEDLIST");
                       
            //1.输入 
            //1.1 订单号
            IRfcTable sales_keyTable = rfcFunc.GetTable("Sales_Documents");
            IRfcStructure sales_key = sales_keyTable.Metadata.LineType.CreateStructure();
            sales_key.SetValue("VBELN",VBELN );
            sales_keyTable.Append(sales_key);
            rfcFunc.SetValue("Sales_Documents", sales_keyTable);
            //1.2 显示头信息
            IRfcStructure order_view = rfcFunc.GetStructure("I_BAPI_VIEW");
            order_view.SetValue("HEADER", "X");
            order_view.SetValue("ITEM", "X");
            order_view.SetValue("BUSINESS", "X");
            order_view.SetValue("PARTNER", "X");
            rfcFunc.SetValue("I_BAPI_VIEW", order_view);

            //2.调用sap函数
            rfcFunc.Invoke(destination1); //execute the function in the backend

            try
            {
                //3.输出
                SalesOrder salesOrder = new SalesOrder();
                IRfcTable order_header_table = rfcFunc.GetTable("ORDER_HEADERS_OUT");
                if (order_header_table.Count > 0)
                {
                    //salesOrder.NET_VAL_HD = order_header_table[0]["NET_VAL_HD"].GetString();
                    salesOrder.CURRENCY = order_header_table[0]["CURRENCY"].GetString();
                    salesOrder.REQ_DATE_H = order_header_table[0]["REQ_DATE_H"].GetString();
                    salesOrder.PURCH_NO = order_header_table[0]["PURCH_NO"].GetString();
                    salesOrder.SOLD_TO = order_header_table[0]["SOLD_TO"].GetString();
                }

                IRfcTable order_item_table = rfcFunc.GetTable("ORDER_ITEMS_OUT");
                decimal net_value = 0;
                decimal tax_amount = 0;
                for (int i = 0; i < order_item_table.Count; i++)
                {
                    IRfcStructure item = order_item_table[i];
                    net_value += item["NET_VALUE"].GetDecimal();
                    tax_amount += item["TAX_AMOUNT"].GetDecimal();
                }
                salesOrder.NET_VAL_HD = net_value + tax_amount;

                IRfcTable business = rfcFunc.GetTable("ORDER_BUSINESS_OUT");
                if (business.Count > 0)
                {
                    salesOrder.PMNTTRMS = business[0]["PMNTTRMS"].GetString();
                    salesOrder.PURCH_NO_C = business[0]["PURCH_NO_C"].GetString();
                }

                IRfcTable partners = rfcFunc.GetTable("ORDER_PARTNERS_OUT");
                List<Contact> contacts = new List<Contact>();
                for (int i = 0; i < partners.Count; i++)
                {
                    IRfcStructure item = partners[i];
                    Contact contact = new Contact();
                    contact.PARTN_ROLE = item["PARTN_ROLE"].GetString();
                    contact.CONTACT = item["CONTACT"].GetString();
                    if (contact.PARTN_ROLE.Trim().ToUpper() == "ZK" || contact.PARTN_ROLE.Trim().ToUpper() == "ZN")
                    {
                        contacts.Add(contact);
                    }

                }
                salesOrder.Contacts = contacts;

                return salesOrder;
            }
            catch (Exception e)
            {
                //Console.WriteLine("Error:"+e.Message);
                throw e;
            }
            
        }
    }
}
