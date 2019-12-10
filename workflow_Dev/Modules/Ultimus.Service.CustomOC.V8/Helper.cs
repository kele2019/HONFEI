using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace CustomDirectory
{
    /// <summary>
    /// This class contains some helper fucntions tpo be used in OCSync class
    /// </summary>
    public class Helper
    {
        private const string strConnection = "Data Source=192.168.101.112;Initial Catalog=UltimusBizHF;User ID=sa;Password=Password01!;";
        private const string strProvider = "System.Data.SqlClient";
        private string strLogFilePath = "C:\\CustomOC\\Log" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0') + ".log";
        private StreamWriter logWriter;

        /// <summary>
        /// constructor
        /// </summary>
        public Helper()
        {
        }

        /// <summary>
        /// This method executes the specifed query and returns the result in a record set
        /// </summary>
        /// <param name="strQuery">SQL query to be executed as a string</param>
        /// <param name="strError">contains the error description if method fails</param>
        /// <param name="dbConnection">An object of DbConnection.</param>
        /// <returns>An object of type OleDbDataReader containing record set returned by the SQL query</returns>
        public DbDataReader RunQuery(string strQuery, out string strError, out DbConnection dbConnection)
        {
            DbDataReader dbReader = null;
            strError = "";

            try
            {
                DbHelper db = new DbHelper(strConnection, strProvider);
                dbReader = db.ExecuteReader(strQuery);
                dbConnection = db._dbConnection;

            }
            catch (Exception Ex)
            {
                strError = Ex.Message;
                dbConnection = null;
                return null;
            }

            //return reader
            strError = "";
            return dbReader;
        }

        public DataTable ExecuteDataTable(string sql)
        {
            DbHelper db = new DbHelper(strConnection, strProvider);
            DbCommand cmd= db.GetSqlStringCommond(sql);
            return db.ExecuteDataTable(cmd);
        }


        public DbDataReader RunQuery(string strQuery, out string strError)
        {
            DbDataReader dbReader = null;
            strError = "";

            try
            {
                DbHelper db = new DbHelper(strConnection, strProvider);
                dbReader = db.ExecuteReader(strQuery);

            }
            catch (Exception Ex)
            {
                strError = Ex.Message;
                return null;
            }

            //return reader
            strError = "";
            return dbReader;
        }

        /// <summary>
        /// This method executes the specifed query and returns the result in a record set
        /// </summary>
        /// <param name="strQuery">SQL query to be executed as a string</param>
        /// <param name="strError">contains the error description if method fails</param>
        /// <param name="dbConnection">An object of DbConnection.</param>
        /// <returns>An object of type OleDbDataReader containing record set returned by the SQL query</returns>
        public DbDataReader RunQueryRef(string strQuery, out string strError, ref DbConnection dbConnection)
        {
            DbDataReader dbReader = null;
            strError = "";

            try
            {
                DbHelper db = new DbHelper(strConnection, strProvider);
                dbReader = db.ExecuteReader(strQuery);
                dbConnection = db._dbConnection;

            }
            catch (Exception Ex)
            {
                strError = Ex.Message;
                dbConnection = null;
                return null;
            }

            //return reader
            strError = "";
            return dbReader;
        }

        public DataTable RunQueryRef(string strQuery)
        {
            DataTable dbReader = null;

            try
            {
                DbHelper db = new DbHelper(strConnection, strProvider);
                dbReader = db.ExecuteDataTable(strQuery);

            }
            catch (Exception Ex)
            {
                return null;
            }

            //return reader
            return dbReader;
        }
        /// <summary>
        /// Executes the select query and returns the total number of rows returned
        /// </summary>
        /// <param name="strQuery">SQL query to be executed as a string</param>
        /// <returns>Total number of rows returned by the SQL query</returns>
        public int GetTotalRows(string strQuery)
        {
            int nRows = 0;
            DbDataReader dbReader=null;

            try
            {
                if (!string.IsNullOrEmpty(strQuery))
                {
                    DbHelper db = new DbHelper(strConnection, strProvider);
                    //Execute the query and get the rows
                    dbReader = db.ExecuteReader(strQuery);

                    while (dbReader.Read())
                        nRows++;

                    dbReader.Close();
                }

            }
            catch (Exception Ex)
            {
                AppendTextToLog("GetTotalRows failed with exception : " + Ex.Message);
                AppendTextToLog("GetTotalRows failed with exception sql: " + strQuery);
                return -1;
            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
            //if (nRows == 0)
            //{
            //AppendTextToLog("sql: " + strQuery);
            //}

            return nRows;
        }

        /// <summary>
        /// Appends a text line to the log file
        /// </summary>
        /// <param name="strText">Text to be appended</param>
        public void AppendTextToLog(string strText)
        {
            if (!Directory.Exists("c:\\CustomOC"))
            {
                Directory.CreateDirectory("c:\\CustomOC");
            }
            logWriter = File.AppendText(strLogFilePath);
            logWriter.Write(DateTime.Now.ToString() + ": ");
            logWriter.WriteLine(strText);
            logWriter.Close();
            logWriter.Dispose();

            Debug.WriteLine("Ultimus CustomOC:" + strConnection +";"+ strText);
        }

    }
}
