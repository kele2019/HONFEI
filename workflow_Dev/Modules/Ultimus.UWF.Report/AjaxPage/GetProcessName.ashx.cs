using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BPM.ReportDesign.AjaxPage
{
    /// <summary>
    /// GetProcessName 的摘要说明
    /// </summary>
    public class GetProcessName : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            clsOleDB ole = new clsOleDB();
            string strcon = ole.getConn("bpmDB");
            DataBase db = new DataBase(strcon);
            System.Data.DataSet ds = db.QueryTable("select distinct processname from INITIATE");
            string json = "";
            if (ds.Tables[0].Rows.Count > 0)
            {
                json += "[";
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    json += "{'name':'" + row["processname"].ToString().Trim() + "'},";
                }
                json = json.TrimEnd(',');
                json += "]";
            }
            context.Response.Write(json);
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