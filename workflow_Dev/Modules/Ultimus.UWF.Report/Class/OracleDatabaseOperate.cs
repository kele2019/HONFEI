using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.OleDb;

/// <summary>
///OracleDatabaseOperate 的摘要说明
/// </summary>
public class OracleDatabaseOperate
{
    private static string connStr;
    private static OleDbConnection oleConn;

    public OracleDatabaseOperate()
    {
        oleConn = new OleDbConnection();
    }

    public OracleDatabaseOperate(string connectionString)
    {
        connStr = connectionString;
        oleConn = new OleDbConnection(connStr);
    }

    public int ExectionQuery(string strSql)
    {
        int operate = 0;
        if (oleConn.State != ConnectionState.Open)
        {
            oleConn.Open();
        }
        try
        {
            OleDbCommand cmd = new OleDbCommand(strSql, oleConn);
            operate=cmd.ExecuteNonQuery();
        }
        catch{}
        finally
        {
            oleConn.Close();
        }
        return operate;
    }

    public DataSet SelectQuery(string strSql)
    {
        DataSet ds = new DataSet();
        try
        {
            using (OleDbCommand cmd = new OleDbCommand(strSql, oleConn))
            {
                using (OleDbDataAdapter oda = new OleDbDataAdapter(cmd))
                {
                    oda.Fill(ds);
                }
            }
        }
        catch{}
        return ds;
    }

}