using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace BPM.ReportDesign.AjaxPage
{
    /// <summary>
    /// GetDataBaseTables 的摘要说明
    /// </summary>
    public class GetDataBaseTables : IHttpHandler
    {
        private DataBase db;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string connectionString = context.Request.Form["connectionString"].ToString().Replace("/","\\");
            db = new DataBase(connectionString);
            string strSql = "";
            if (System.Configuration.ConfigurationManager.AppSettings["DataBaseType"].ToString().ToUpper() == "ORACLE")
            {
                strSql = "select table_name name from user_tables";
            }
            else if (System.Configuration.ConfigurationManager.AppSettings["DataBaseType"].ToString().ToUpper() == "SQL")
            {
                strSql = "select name from sys.objects where type = 'U' or type = 'V' order by name asc";
            }
            string responseContext = "";
            DataSet ds = db.QueryTable(strSql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                responseContext += "[";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow row = ds.Tables[0].Rows[i];
                    responseContext += "{'tableName':'" + row["name"].ToString() + "'},";
                }
                responseContext = responseContext.Substring(0, responseContext.LastIndexOf(','));
                responseContext += "]";
            }
            context.Response.Write(responseContext);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}