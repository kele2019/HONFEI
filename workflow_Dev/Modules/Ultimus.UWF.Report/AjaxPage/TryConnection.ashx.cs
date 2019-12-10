using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Web;

namespace BPM.ReportDesign.AjaxPage
{
    /// <summary>
    /// TryConnection 的摘要说明
    /// </summary>
    public class TryConnection : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string connectionString = context.Request.Form["connectionString"].ToString().Replace("/","\\");
            OleDbConnection oleConnection = new OleDbConnection();
            oleConnection.ConnectionString = connectionString;
            string responseText = "";
            try
            {
                oleConnection.Open();
                responseText = "测试连接成功";
                oleConnection.Close();
            }
            catch
            {
                responseText = "";
            }
            context.Response.Write(responseText);
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