using System;
using System.Collections.Generic;
using System.Text;
 
using MyLib;

namespace Ultimus.UWF.Workflow.EmailServices
{
    public class SerialNoLogic
    {
        /// <summary>
        /// 获取序列号，如TRA20131204001
        /// </summary>
        /// <param name="type"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public string GetSerialNo(string type, int year, int month, int day, int length)
        {
            int serialNo = GetSerialNo(type, year, month, day);
            
            return type+year.ToString()+month.ToString().PadLeft(2,'0')+day.ToString().PadLeft(2,'0')+serialNo.ToString().PadLeft(length,'0');
        }

        /// <summary>
        /// 获取下一个ID
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetSerialNo(string type)
        {
            return GetSerialNo(type, 0, 0, 0);
        }

        public int GetMaxNo(string tableName,string fieldName)
        {
            int max=ConvertUtil.ToInt32( DataAccess.Instance("BizDB").ExecuteScalar("select max("+fieldName+") from "+tableName));

            return max + 1;
        }

        public int GetCount(string tableName, string fieldName,string fieldValue)
        {
            int count = ConvertUtil.ToInt32(DataAccess.Instance("BizDB").ExecuteScalar("SELECT COUNT(1) FROM "+tableName+" WHERE "+fieldName+"='"+fieldValue+"'"));

            return count;
        }

        public int GetSerialNo(string type, int year, int month, int day)
        {
            SerialNoEntity ety = new SerialNoEntity();
            ety.ID = GetMaxNo("COM_SERIALNO", "ID");
            ety.SERIALTYPE = type;
            ety.SERIALYEAR = year;
            ety.SERIALMONTH = month;
            ety.SERIALDAY = day;
            ety.UPDATEDATE = DateTime.Now;

            string StrsqlNo = "select SERIALNO from COM_SERIALNO where SERIALTYPE='"+ety.SERIALTYPE+"' and SERIALYEAR='"+ety.SERIALYEAR+"' and SERIALMONTH='"+ety.SERIALMONTH+"' and SERIALDAY='"+ety.SERIALDAY+"'";

            //int serailNo =ConvertUtil.ToInt32(DataAccess.Instance("BizDB").GetObject("SerialNoLogic_GetSerailNo", ety));
            int serailNo = ConvertUtil.ToInt32(DataAccess.Instance("BizDB").ExecuteScalar(StrsqlNo));
            string Strsql = "";
            if (serailNo == 0)
            {
                serailNo++;
                ety.SERIALNO = serailNo;

                Strsql = @" INSERT INTO COM_SERIALNO
      (ID, SERIALTYPE  , SERIALYEAR  , SERIALMONTH  , SERIALDAY  , SERIALNO  , UPDATEDATE)  VALUES(";
                Strsql += "'"+ety.ID+"','"+ety.SERIALTYPE+"','"+ety.SERIALYEAR+"','"+ety.SERIALMONTH+"','"+ety.SERIALDAY+"','"+ety.SERIALNO+"','"+ety.UPDATEDATE+"')";

                // DataAccess.Instance("BizDB").Insert("SerialNoLogic_InsertSerailNo", ety);
                DataAccess.Instance("BizDB").ExecuteNonQuery(Strsql);
            }
            else
            {
                serailNo++;
                ety.SERIALNO = serailNo;
                Strsql = @"   UPDATE COM_SERIALNO
      SET SERIALNO='"+ ety.SERIALNO + "',UPDATEDATE='"+ety.UPDATEDATE + "'  WHERE SERIALTYPE='"+ety.SERIALTYPE + "' AND SERIALYEAR='"+ety.SERIALYEAR + "' AND SERIALMONTH='"+ety.SERIALMONTH + "'";
               // DataAccess.Instance("BizDB").Update("SerialNoLogic_UpdateSerailNo", ety);
                DataAccess.Instance("BizDB").ExecuteNonQuery(Strsql);
            }
            return serailNo;
        }

    }
    public class SerialNoEntity {
        public int ID { get; set; }
        public string SERIALTYPE { get; set; }
        public int SERIALYEAR { get; set; }
        public int SERIALMONTH { get; set; }
        public int SERIALDAY { get; set; }
        public DateTime UPDATEDATE { get; set; }
        public int SERIALNO { get; set; }

    }
}
