using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections;

namespace Ultimus.UWF.FunctionManager.Quality_document_management
{
   
        public class DAL : IDisposable
        {
            protected SqlConnection Connection;

            protected string ConncetionString;
            //初始函數
            public DAL()
            {
                //ConncetionString = "server=.;uid=sa;pwd=qazwsx!23;database=UltimusBiz";
                ConncetionString = ConfigurationManager.ConnectionStrings["BizDB"].ConnectionString;
                // ConncetionString = "server=10.110.2.34;uid=sa;pwd=Sa123456;database=UltimusBiz";
            }
            //初始函數重載
            public DAL(int pconnection)
            {
                ConncetionString = "";
            }
            public DAL(string pconnection)
            {
                ConncetionString = pconnection;
            }
            //析構函數
            ~DAL()
            {
                try
                {
                    if (Connection != null)
                        Connection.Close();
                }
                catch
                {
                }
                try
                {
                    Dispose();
                }
                catch
                { }
            }
            //垃圾回收
            public void Dispose()
            {
                if (Connection != null)
                {
                    Connection.Dispose();
                    Connection = null;
                }
            }
            //鏈接打開
            public void Open()
            {
                if (Connection == null)
                {
                    Connection = new SqlConnection(ConncetionString);
                }
                if (Connection.State.Equals(ConnectionState.Closed))
                {
                    Connection.Open();
                }
            }
            //鏈接關閉
            public void Close()
            {
                if (Connection.State.Equals(ConnectionState.Open))
                {
                    Connection.Close();
                }
            }
            //獲取dr
            public SqlDataReader GetDataReader(string sqlstring)
            {
                Open();
                SqlCommand cmd = new SqlCommand(sqlstring, Connection);
                SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                return dr;
            }
            //獲取ds
            public DataSet GetDataSet(string sqlstring)
            {
                Open();
                SqlDataAdapter da = new SqlDataAdapter(sqlstring, Connection);
                DataSet ds = new DataSet();
                da.Fill(ds);
                Close();
                return ds;
            }
            //獲取datarow
            public DataRow GetDataRow(string sql)
            {
                DataSet ds = GetDataSet(sql);
                ds.CaseSensitive = false;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0];
                }
                else
                {
                    return null;
                }
            }
            //獲取datatable
            public DataTable GetDataTable(string sql)
            {
                Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, Connection);
                da.Fill(dt);
                Close();
                return dt;
            }
            //執行單條語句
            public bool ExcuteSql(string sql)
            {
                bool Count = true;
                Open();
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, Connection);
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    Count = false;
                }
                finally
                {
                    Close();
                }
                return Count;
            }
            //執行多條語句
            public bool ExecuteSQL(ArrayList SqlStrings)
            {
                bool success = true;
                Open();
                SqlCommand cmd = new SqlCommand();
                SqlTransaction trans = Connection.BeginTransaction();//事物的使用
                cmd.Connection = Connection;
                cmd.Transaction = trans;
                try
                {
                    foreach (String str in SqlStrings)
                    {
                        cmd.CommandText = str;
                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                }
                catch
                {
                    success = false;
                    trans.Rollback();
                }
                finally
                {
                    Close();
                }
                return success;
            }
            //按哈希表插入數據
            public bool Insert(string table, Hashtable Cols)
            {
                int Count = 0;

                if (Cols.Count <= 0)
                {
                    return true;
                }

                String Fields = " (";
                String Values = " Values(";
                foreach (DictionaryEntry item in Cols)
                {
                    if (Count != 0)
                    {
                        Fields += ",";
                        Values += ",";
                    }
                    Fields += item.Key.ToString();
                    Values += item.Value.ToString();
                    Count++;
                }
                Fields += ")";
                Values += ")";

                String SqlString = "Insert into " + table + Fields + Values;

                return Convert.ToBoolean(ExcuteSql(SqlString));
            }
            //按哈希表更新數據
            public bool Update(String TableName, Hashtable Cols, String Where)
            {
                int Count = 0;
                if (Cols.Count <= 0)
                {
                    return true;
                }
                String Fields = " ";
                foreach (DictionaryEntry item in Cols)
                {
                    if (Count != 0)
                    {
                        Fields += ",";
                    }
                    Fields += item.Key.ToString();
                    Fields += "=";
                    Fields += item.Value.ToString();
                    Count++;
                }
                Fields += " ";

                String SqlString = "Update " + TableName + " Set " + Fields + Where;

                return Convert.ToBoolean(ExcuteSql(SqlString));
            }
        }
  }