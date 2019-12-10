using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyLib;
using System.Data;

namespace Ultimus.UWF.Workflow
{
    /// <summary>
    /// AjaxClass 的摘要说明
    /// </summary>
    public class AjaxClass : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string ProcessID = context.Request.Form["ProcessID"];
            string Strwhere="";

            if(!string.IsNullOrEmpty(ProcessID))
           Strwhere+=" where CATEGORYID='"+ProcessID+"'";
            DataTable dtProcessInfo = DataAccess.Instance("BizDB").ExecuteDataTable("select PROCESSNAME from WF_PROCESS " + Strwhere + " order by PROCESSNAME");
            string ReturnValue="";
            if (dtProcessInfo.Rows.Count > 0)
            {
                foreach (DataRow item in dtProcessInfo.Rows)
                {
                    ReturnValue += item["PROCESSNAME"].ToString()+"|";
                }
            }
             context.Response.Write(ReturnValue.TrimEnd('|'));
            //context.Response.Write("Hello World");
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