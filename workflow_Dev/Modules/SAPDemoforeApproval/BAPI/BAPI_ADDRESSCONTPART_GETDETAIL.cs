using System;
using System.Collections.Generic;
using System.Text;
using SAP.Middleware.Connector;

namespace SAPDemoforeApproval
{
    public class BAPI_ADDRESSCONTPART_GETDETAIL
    {
        public string GetInfo(string search1,string search2,string search3)
        {

            RfcDestination destination1 = RfcDestinationManager.GetDestination(new SAPConfigConn().GetParameters("StaticSapCon1"));
            try
            {
            RfcRepository repo = destination1.Repository;
            IRfcFunction rfcFunc = repo.CreateFunction("Z_ZFKSP_DISPLAY");

            rfcFunc.SetValue("P_BUKRS", search1);
            rfcFunc.SetValue("P_LIFNR", search2);
            rfcFunc.SetValue("P_DATE", search3);

            //rfcFunc.SetValue("P_BUKRS", "");
            //rfcFunc.SetValue("P_LIFNR", "");
            //rfcFunc.SetValue("P_DATE", "");

            //2.调用sap函数
            rfcFunc.Invoke(destination1); //execute the function in the backend


            //return rfcFunc.GetValue("RESULT").ToString() + rfcFunc.GetValue("RET_MSG").ToString();
                //3.输出
                //IRfcTable result = rfcFunc.GetTable("RESULT");
                //string jsonStirng = "";
                //for (int i = 0; i < result.Count; i++) 
                //{
                //    IRfcStructure item = result[i];
                //    jsonStirng += "," + item["KUNNR"].GetString() + "|" + item["NAME1"].GetString();
                //}
                //return jsonStirng.Substring(1);

                IRfcStructure result = rfcFunc.GetStructure("ZFKSP_DATA");
                
                //IRfcStructure header = 
                string jsonStirng = "";
                //IRfcStructure item = result[0];
                jsonStirng = "{NAME1:'" + result["NAME1"].GetString() +
                    "',SORT2:'" + result["SORT2"].GetString() +
                    "',BANKA:'" + result["BANKA"].GetString() +
                    "',BANKN:'" + result["BANKN"].GetString() +
                    "',VTEXT:'" + result["VTEXT"].GetString() +
                    "',ZWELST:'" + result["ZWELST"].GetString() +
                    "',BQZYE:'" + result["BQZYE"].GetString() +
                    "',ZBJ:'" + result["ZBJ"].GetString() +
                    "',ZMQK:'" + result["ZMQK"].GetString() +
                    "',HDPWD:'" + result["HDPWD"].GetString() +
                    "',DQYFK:'" + result["DQYFK"].GetString() +
                    "',TQFK:'" + result["TQFK"].GetString() +
                    "',KK:'" + result["KK"].GetString() +
                    "',BCSBJE:'" + result["BCSBJE"].GetString() +
                    "',SJFKFS:'" + result["SJFKFS"].GetString() +
                    
                    "'}";
                //jsonStirng = item["SORT2"].GetString();
                return jsonStirng;
            }
            catch (Exception e)
            {
                throw e;
            }
            


            //RfcDestination destination1 = RfcDestinationManager.GetDestination(new SAPConfigConn().GetParameters("StaticSapCon1"));

            //RfcRepository repo = destination1.Repository;
            //IRfcFunction rfcFunc = repo.CreateFunction("BAPI_ADDRESSCONTPART_GETDETAIL");

            //rfcFunc.SetValue("OBJ_TYPE_P", OBJ_TYPE_P);
            //rfcFunc.SetValue("OBJ_ID_P", OBJ_ID_P);
            //rfcFunc.SetValue("OBJ_TYPE_C", OBJ_TYPE_C);
            //rfcFunc.SetValue("OBJ_ID_C", OBJ_ID_C);
            //rfcFunc.SetValue("CONTEXT", CONTEXT);
            //rfcFunc.SetValue("IV_CURRENT_COMM_DATA", IV_CURRENT_COMM_DATA);

            ////2.调用sap函数
            //rfcFunc.Invoke(destination1); //execute the function in the backend

            //try
            //{
            //    //3.输出
            //    IRfcTable customer_address = rfcFunc.GetTable("BAPIAD3VL");
            //    if (customer_address.Count > 0)
            //    {
            //        return customer_address[0]["FULLNAME"].GetString();
            //    }
            //    return "";

            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}

        }

        public string InsertInfo(string comments, string type, string string1, string string2, string string3)
        {

            RfcDestination destination1 = RfcDestinationManager.GetDestination(new SAPConfigConn().GetParameters("StaticSapCon1"));
            try
            {
                RfcRepository repo = destination1.Repository;
                IRfcFunction rfcFunc = repo.CreateFunction("Z_ZFKSP_APPROVAL");

                rfcFunc.SetValue("P_BUKRS", string1);
                rfcFunc.SetValue("P_LIFNR", string2);
                rfcFunc.SetValue("P_DATE", string3);
                rfcFunc.SetValue("USER", type);
                rfcFunc.SetValue("TEXT", comments);

                //2.调用sap函数
                rfcFunc.Invoke(destination1); //execute the function in the backend


                return rfcFunc.GetValue("RESULT").ToString();
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public string GetCustomer2(string S_KUNNR, string S_VKORG)
        {

            RfcDestination destination1 = RfcDestinationManager.GetDestination(new SAPConfigConn().GetParameters("StaticSapCon1"));

            RfcRepository repo = destination1.Repository;
            IRfcFunction rfcFunc = repo.CreateFunction("ZSD_O_002");

            rfcFunc.SetValue("SMODEL", "2");
            rfcFunc.SetValue("S_KUNNR", "0000100065");
            rfcFunc.SetValue("S_VKORG", "1110");

            //2.调用sap函数
            rfcFunc.Invoke(destination1); //execute the function in the backend

            try
            {
                //3.输出
                IRfcTable result = rfcFunc.GetTable("ZSD_KUNNR");
                string jsonStirng = "";
                IRfcStructure item = result[0];
                jsonStirng = "{KUNNR:'" + item["KUNNR"].GetString() + "',NAME1:'" + item["NAME1"].GetString() + "',VKORG:'" + item["VKORG"].GetString() + "',VTWEG:'" + item["VTWEG"].GetString() + "',VKBUR:'" + item["VKBUR"].GetString() + "',VKGRP:'" + item["VKGRP"].GetString() + "',ZTERM:'" + item["ZTERM"].GetString() + "',INCO1:'" + item["INCO1"].GetString() + "',INCO2:'" + item["INCO2"].GetString() + "',KURST:'" + item["KURST"].GetString() + "'}";
                return jsonStirng;


            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string CreateOrder()
        {
            RfcDestination destination1 = RfcDestinationManager.GetDestination(new SAPConfigConn().GetParameters("StaticSapCon1"));

            RfcRepository repo = destination1.Repository;
            IRfcFunction rfcFunc = repo.CreateFunction("ZSD_I_001");

            //Header
            rfcFunc.SetValue("Z_MODE", "1");
            IRfcStructure header = rfcFunc.GetStructure("Z_SO_HEAD");
            header.SetValue("AUART", "ZOR");
            header.SetValue("VKORG", "1110");
            header.SetValue("VTWEG", "10");
            header.SetValue("SPART", "00");
            header.SetValue("VKBUR", "1106");
            header.SetValue("VKGRP", "S43");
            //header.SetValue("BSTKD", "");
            //header.SetValue("BSTDK", "");
            //header.SetValue("BNAME", "");
            header.SetValue("KUNNR", "0000100065");
            //header.SetValue("ABLAD", "");
            header.SetValue("ZTERM", "0012");
            header.SetValue("INCO1", "CIF");
            header.SetValue("INCO2", "HAMBURG");
            header.SetValue("CURRENCY", "EUR");
            rfcFunc.SetValue("Z_SO_HEAD", header);

            //更改的订单号
            //rfcFunc.SetValue("Z_IN_VBELN", "");

            //Items
            IRfcTable items = rfcFunc.GetTable("Z_SO_ITEM");
            IRfcStructure item = items.Metadata.LineType.CreateStructure();
            item.SetValue("POSNR", "10");
            item.SetValue("MATNR", "C000053081");
            item.SetValue("KWMENG", "100");
            item.SetValue("KSCHL", "ZPR2");
            item.SetValue("KPEIN", "10000");
            item.SetValue("KBETR", "100");
            item.SetValue("WAERK", "EUR");
            item.SetValue("MEINS", "ST");
            item.SetValue("PSTYV", "ZTAN");
            item.SetValue("WERKS", "1210");
            item.SetValue("EDATU", "20150330");
            item.SetValue("LGORT", "2199");
            item.SetValue("REQMTS_TYP", "KEV");
            items.Append(item);

            item = items.Metadata.LineType.CreateStructure();
            item.SetValue("POSNR", "20");
            item.SetValue("MATNR", "C000053072");
            item.SetValue("KWMENG", "10");
            item.SetValue("KSCHL", "ZPR2");
            item.SetValue("KPEIN", "10000");
            item.SetValue("KBETR", "100");
            item.SetValue("WAERK", "EUR");
            item.SetValue("MEINS", "ST");
            item.SetValue("PSTYV", "ZTAN");
            item.SetValue("WERKS", "1120");
            item.SetValue("EDATU", "20150330");
            item.SetValue("LGORT", "1299");
            item.SetValue("REQMTS_TYP", "KEV");
            items.Append(item);

            item = items.Metadata.LineType.CreateStructure();
            item.SetValue("POSNR", "30");
            item.SetValue("MATNR", "C000053077");
            item.SetValue("KWMENG", "29");
            item.SetValue("KSCHL", "ZPR2");
            item.SetValue("KPEIN", "10000");
            item.SetValue("KBETR", "100");
            item.SetValue("WAERK", "EUR");
            item.SetValue("MEINS", "ST");
            item.SetValue("PSTYV", "ZTAN");
            item.SetValue("WERKS", "1220");
            item.SetValue("EDATU", "20150330");
            item.SetValue("LGORT", "2299");
            item.SetValue("REQMTS_TYP", "KEV");
            items.Append(item);
            rfcFunc.SetValue("Z_SO_ITEM", items);

            //2.调用sap函数
            rfcFunc.Invoke(destination1); //execute the function in the backend

            try
            {
                //3.输出
                object result = rfcFunc.GetValue("Z_MESSAGE");

                return Convert.ToString(result);


            }
            catch (Exception e)
            {
                throw e;
            }
            return "";
        }

    }
}
