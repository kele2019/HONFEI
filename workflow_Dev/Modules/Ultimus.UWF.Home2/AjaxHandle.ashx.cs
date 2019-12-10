using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ultimus.UWF.Security;
using MyLib;
using System.Data.Common;
using System.Data;
namespace Ultimus.UWF.Home2
{
    /// <summary>
    /// AjaxHandle1 的摘要说明
    /// </summary>
    public class AjaxHandle1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
          //  context.Response.Write("Hello World");
            string username = context.Request.Form["username"];
            string password = context.Request.Form["password"];
            if (username == "" || password == "")
            {
                context.Response.Write("请填写用户名或密码！");
            }
            else
            {
                string Md5PWD=Security.Logic.SecurityLogic.GetMd5(password, 16);
                string Domain=ConfigurationManager.AppSettings["Domain"];
                DataAccess db = new DataAccess("BizDB");
                string strsql = "select count(1) from ORG_USER where LOGINNAME=@LOGINNAME and PASSWORD=@PASSWORD";
                DbCommand dbcom = db.CreateCommand(strsql);
                db.AddInParameter(dbcom, "@LOGINNAME", DbType.String,Domain+"\\"+username);
                db.AddInParameter(dbcom, "@PASSWORD", DbType.String, Md5PWD);
                 if(int.Parse(db.ExecuteScalar(dbcom).ToString())>0)
                context.Response.Write("success");
                 else
                     context.Response.Write("fail");
            }
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