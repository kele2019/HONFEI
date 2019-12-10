using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ultimus.UWF.FunctionManager.Entity;
using Ultimus.UWF.FunctionManager.Interface;
using MyLib;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Configuration;
using System.IO;

namespace Ultimus.UWF.FunctionManager.Logic
{
    public class CurrencyLogic
    {
        public List<CurrencyEntity> GetCurrencyList()
        {
            List<CurrencyEntity> lists = DataAccess.Instance("BizDB").ExecuteList<CurrencyEntity>("select ID,DicCode,DicText,DicValue FROM COM_DICTIONRY WHERE TYPE='CURRENCY';");
            return lists;
        }
        public void addCurrencyEntity(CurrencyEntity currency)
        {
            DataAccess.Instance("BizDB").ExecuteNonQuery("insert into COM_DICTIONRY(DicCode,DicText,DicValue,type) values('" + currency.DicCode + "','" + currency.DicText + "','" + currency.DicValue + "','CURRENCY');");
        }
        public void updateCurrency(CurrencyEntity currency)
        {
            DataAccess.Instance("BizDB").ExecuteNonQuery("update COM_DICTIONRY set DicValue= '" +  currency.DicValue + "' where type = 'CURRENCY' and ID = " + currency.ID + ";" );
        }
        public CurrencyEntity getCurreneyByID(int ID)
        {
            DataAccess db = new DataAccess("BizDB");
            StringBuilder strsql = new StringBuilder();
            strsql.Append("select * from COM_DICTIONRY where ID=@ID;");
            DbCommand dbcom = db.CreateCommand(strsql.ToString());
            db.AddInParameter(dbcom, "@ID", DbType.Int32, ID);
            DbDataReader dr = db.ExecuteReader(dbcom);
            CurrencyEntity currency = new CurrencyEntity();
            while (dr.Read())
            {
                if (dr["ID"] != null && !String.IsNullOrEmpty(dr["ID"].ToString()))
                {
                    currency.ID = int.Parse(dr["ID"].ToString());
                }
                if (dr["DicCode"] != null && !String.IsNullOrEmpty(dr["DicCode"].ToString()))
                {
                    currency.DicCode = dr["DicCode"].ToString();
                }
                if (dr["DicText"] != null && !String.IsNullOrEmpty(dr["DicText"].ToString()))
                {
                    currency.DicText = dr["DicText"].ToString();
                }
                if (dr["DicValue"] != null && !String.IsNullOrEmpty(dr["DicValue"].ToString()))
                {
                    currency.DicValue = dr["DicValue"].ToString();
                }
            }
            dr.Close();
            return currency;
        }
    }
}