using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Data.OleDb;


public class DataBase
{
    OleDbConnection conn;
    public DataBase()
    { }
    public DataBase(string ConnectionString)
    {
        conn = new OleDbConnection(ConnectionString);
    }
    public void SetConnectionString(string ConnectionString)
    {
        conn = new OleDbConnection(ConnectionString);
    }

    public DataSet QueryTable(string sql)
    { 
        using(OleDbDataAdapter oda=new OleDbDataAdapter(sql,conn))
        {
            using (DataSet ds = new DataSet())
            {
                oda.Fill(ds);
                return ds;
            }
        }
    }

    public int ExecuteNonQuery(string sql)
    {
        if (conn.State != ConnectionState.Open)
        {
            conn.Open();
        }
        using (OleDbCommand ocd = new OleDbCommand(sql, conn))
        {
            return ocd.ExecuteNonQuery();
        }
    }
}