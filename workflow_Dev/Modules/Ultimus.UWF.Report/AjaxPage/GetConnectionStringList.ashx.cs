using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using BPM.ReportDesign.Class;
namespace BPM.ReportDesign.AjaxPage
{
    /// <summary>
    /// GetConnectionStringList 的摘要说明
    /// </summary>
    public class GetConnectionStringList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string json = "";
            DataTable dt = new ReaderTextConnectionString().ReaderConnectionList();
            if (dt.Rows.Count > 0)
            {
                json += "[";
                foreach (DataRow row in dt.Rows)
                {
                    json += "{'name':'" + row["ConnectionStringName"].ToString() + "',"
                          + "'value':'" + row["ConnectionStringValue"].ToString() + "'},";
                }
                json = json.Substring(0, json.LastIndexOf(','));
                json += "]";
            }
            context.Response.Write(json);
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