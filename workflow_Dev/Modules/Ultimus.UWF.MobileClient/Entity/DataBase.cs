using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DataBaseLibrary;
using MyLib;

namespace DALLibrary
{
    public class DataBase
    {
        //private DbHelperSQLP db = new DbHelperSQLP();
        public List<string> GetAllTableName(string connectionString)
        {
            //db.connectionString = connectionString;
            StringBuilder strsql = new StringBuilder();
            List<string> list = new List<string>();
            if (DataAccess.Instance("UltDB").SqlMapper.DataSource.DbProvider.Name.ToUpper().IndexOf("ORACLE") >= 0)
            {
                strsql.AppendLine("select table_name as name from user_tables");
            }
            else
            {
                strsql.AppendLine("select [name] from [sysobjects] where [type] = 'u' order by [name]");
            }
            IDataReader reader = DbHelperSQL.ExecuteReader(strsql.ToString());
            while (reader.Read())
            {
                list.Add(reader["name"].ToString().Trim());
            }
            reader.Close();
            return list;
        }
        public List<string> GetTableFieldName(string connectionString,string tableName)
        {
            //db.connectionString = connectionString;
            StringBuilder strsql = new StringBuilder();
            List<string> list = new List<string>();
            /*
             * SELECT t.[name] AS 'tableName',c.[name] AS 'columnName',cast(ep.[value] as varchar(100)) AS 'remark' 
             * FROM sys.tables AS t INNER JOIN sys.columns  
             * AS c ON t.object_id = c.object_id LEFT JOIN sys.extended_properties AS ep  
             * ON ep.major_id = c.object_id AND ep.minor_id = c.column_id WHERE ep.class =1
             * and t.[name]='表名称' 
             */
            if (DataAccess.Instance("UltDB").SqlMapper.DataSource.DbProvider.Name.ToUpper().IndexOf("ORACLE") >= 0)
            {
                strsql.AppendLine("select COLUMN_NAME as name from user_tab_columns where TABLE_NAME='"+tableName+"'");
            }
            else
            {
                strsql.AppendLine("Select name from syscolumns Where ID=OBJECT_ID('" + tableName + "')");
            }
            //IDataReader reader = DbHelperSQL.ExecuteReader(strsql.ToString());
            //while (reader.Read())
            //{
            //    list.Add(reader["name"].ToString().Trim());
            //}
            DataSet dt = DbHelperSQL.Query(strsql.ToString());
            foreach (DataRow dr in dt.Tables[0].Rows)
            {
                list.Add(dr["name"].ToString());
            }
            return list;
        }


    }
}
