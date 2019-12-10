using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using EntityLibrary;
using DataBaseLibrary;
using MyLib;

namespace DALLibrary
{
    public class Initiate
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<EntityLibrary.Initiate> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select INITIATEID,PROCESSNAME,STEPLABEL FROM INITIATE");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<EntityLibrary.Initiate> list = new List<EntityLibrary.Initiate>();
            //DbHelperSQLP db = new DbHelperSQLP();
            IDataReader reader = DataAccess.Instance("UltDB").ExecuteReader(strSql.ToString());
            while (reader.Read())
            {
                EntityLibrary.Initiate model = new EntityLibrary.Initiate();
                model.TaskID = reader["INITIATEID"].ToString();
                model.ProcessName = reader["PROCESSNAME"].ToString();
                model.Incident = 0;
                model.StepName = reader["STEPLABEL"].ToString();
                list.Add(model);
            }
            reader.Close();
            return list;
        }
    }
}
