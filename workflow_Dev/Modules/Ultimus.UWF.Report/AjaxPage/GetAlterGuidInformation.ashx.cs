using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace BPM.ReportDesign.AjaxPage
{
    /// <summary>
    /// GetAlterGuidInformation 的摘要说明
    /// </summary>
    public class GetAlterGuidInformation : IHttpHandler
    {
        private DataBase db;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //修改的报表GUID（唯一标示）
            string AlterGuid = context.Request.Form["alterGuid"].ToString();
            //报表设计器的系统表连接字符串
            string SystemConnectionString = new clsOleDB().getConn("oleDB");

            string sql = "select * from COM_REPORTDESIGN where guid='" + AlterGuid + "'";

            db = new DataBase(SystemConnectionString);

            DataSet ds = db.QueryTable(sql);

            string json = "";

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                json = "{'Guid':'" + row["GUID"].ToString() + "',"
                     + "'UserCode':'" + row["USERCODE"].ToString() + "',"
                     + "'UserName':'" + row["USERNAME"].ToString() + "',"
                     + "'CreatDate':'" + row["CREATDATE"].ToString() + "',"
                     + "'ReportPagePath':'" + row["REPORTPAGEPATH"].ToString() + "',"
                     + "'ReportName':'" + row["REPORTNAME"].ToString() + "',"
                     + "'ConnectionString':'" + row["CONNECTIONSTRING"].ToString() + "',"
                     + "'TableNamesAndFieldNames':'" + row["TABLESNAMES_FIELDNAMES"].ToString() + "',"
                     + "'TableRelation':'" + row["TABLERELATION"].ToString() + "',"
                     + "'FieldRules':'" + row["FIELDRULES"].ToString() + "',"
                     + "'SelectWhere':'" + row["SELECTWHERE"].ToString() + "',"
                     + "'Alias':'" + row["ALIAS"].ToString() + "',"
                     + "'WhetherPaging':'" + row["WHETHERPAGING"].ToString() + "',"
                     + "'ArticlethatNumber':'" + row["ARTICLETHATNUMBER"].ToString() + "',"
                     + "'SortStyle':'" + row["SORTSTYLE"].ToString() + "',"
                     + "'ViewPrivilege':'" + row["VIEWPRIVILEGE"].ToString() + "',"
                     + "'UserAccountFieldName':'" + row["USERACCOUNTFIELDNAME"].ToString() + "',"
                     + "'ViewReportUserAccount':'" + row["VIEWREPORTUSERACCOUNT"].ToString() + "',"
                     + "'ViewReportUserName':'" + row["VIEWREPORTUSERNAME"].ToString() + "',"
                     + "'ProcessName':'" + row["PROCESSNAME"].ToString().Trim() + "',"
                     + "'TableName':'" + row["TABLENAME"].ToString().Trim() + "'}";
            }
            context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
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