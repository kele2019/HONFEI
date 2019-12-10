using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MyLib;
using System.Data.SqlClient;
using System.Text;

namespace Ultimus.UWF.Workflow.Controller
{
    /// <summary>
    /// PerformanceHandler 的摘要说明
    /// </summary>
    public class PerformanceHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            switch (context.Request["tag"])
            {
                case "GetProcessTimeConsuming":
                    GetProcessTimeConsuming(context);
                    break;
                case "GetProcessTimeConsumingTable":
                    GetProcessTimeConsumingTable(context);
                    break;
                case "GetProcessInfo":
                    GetProcessInfo(context);
                    break;

            }
        }

        /// <summary>
        /// DataTable to Json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string ToJson(DataTable dt)
        {
            string jsonName = "";
            StringBuilder Json = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName))
                jsonName = dt.TableName;
            Json.Append("{\"" + jsonName + "\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Type type = dt.Rows[i][j].GetType();
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + StringFormat(dt.Rows[i][j].ToString(), type));
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }

        private static string String2Json(String s)
        {
            System.Text.StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];

                switch (c)
                {
                    case '\"':
                        sb.Append("\\\""); break;
                    case '\\':
                        sb.Append("\\\\"); break;
                    case '/':
                        sb.Append("\\/"); break;
                    case '\b':
                        sb.Append("\\b"); break;
                    case '\f':
                        sb.Append("\\f"); break;
                    case '\n':
                        sb.Append("\\n"); break;
                    case '\r':
                        sb.Append("\\r"); break;
                    case '\t':
                        sb.Append("\\t"); break;
                    default:
                        sb.Append(c); break;
                }
            }
            return sb.ToString();
        }

        private static string StringFormat(string str, Type type)
        {
            if (type == typeof(string))
            {
                str = String2Json(str);
                str = "\"" + str + "\"";
            }
            else if (type == typeof(DateTime))
            {
                str = "\"" + Convert.ToDateTime(str).ToShortDateString() + "\"";
            }
            else if (type == typeof(bool))
            {
                str = str.ToLower();
            }

            if (str.Length == 0)
                str = "\"\"";

            return str;
        }

        //获取列表数据
        public void GetProcessTimeConsumingTable(HttpContext context)
        {
            string Type = context.Request["Type"].ToString(); //1为数据、2为数据总数量
            string Page = context.Request["Page"].ToString(); //分页
            string ReportId = context.Request["ReportId"].ToString(); //报表Id
            string ProcessName = context.Request["ProcessName"].ToString();
            string StartTime = context.Request["StartTime"].ToString();
            string EndTime = context.Request["EndTime"].ToString();

            string sql = @"SELECT ROW_NUMBER() OVER(ORDER BY T.[DAYS] DESC) AS NUM,* FROM  (SELECT Convert(decimal(18,2),Convert(decimal(18,2),SUM(DATEDIFF(d,STARTTIME,isnull(ENDTIME,GETDATE()))))/Convert(decimal(18,2),COUNT(INCIDENT))) AS [DAYS],Convert(decimal(18,2),Convert(decimal(18,2),Convert(decimal(18,2),SUM(DATEDIFF(d,STARTTIME,isnull(ENDTIME,GETDATE()))))/Convert(decimal(18,2),COUNT(INCIDENT)))*24) AS [HOURS],PROCESSNAME FROM [dbo].[INCIDENTS] WHERE 1=1 ";
            if (ProcessName != "")
            {
                sql += " AND '" + ProcessName + @"' LIKE '%'+ RTRIM(PROCESSNAME) + '%' ";
            }
            if (StartTime != "")
            {
                sql += " AND StartTime >= '" + StartTime + "' ";
            }
            if (EndTime != "")
            {
                sql += " AND StartTime <= '" + EndTime + "' ";
            }
            sql += " GROUP BY PROCESSNAME ) T";
            if (ReportId == "2")
            {
                sql = @"SELECT ROW_NUMBER() OVER(ORDER BY T.[DAYS] DESC) AS NUM,T.* FROM (SELECT Convert(decimal(18,2),Convert(decimal(18,2),SUM(DATEDIFF(d,STARTTIME,isnull(ENDTIME,GETDATE()))))/Convert(decimal(18,2),COUNT(TASKID))) AS [DAYS],Convert(decimal(18,2),Convert(decimal(18,2),SUM(DATEDIFF(d,STARTTIME,isnull(ENDTIME,GETDATE()))))/Convert(decimal(18,2),COUNT(TASKID))) * 24 AS [HOURS],PROCESSNAME,STEPLABEL AS STEPNAME FROM [dbo].TASKS WHERE 1=1 ";
                if (ProcessName != "")
                {
                    sql += " AND '" + ProcessName + @"' LIKE '%'+ RTRIM(PROCESSNAME) + '%' ";
                }
                if (StartTime != "")
                {
                    sql += " AND StartTime >= '" + StartTime + "' ";
                }
                if (EndTime != "")
                {
                    sql += " AND StartTime <= '" + EndTime + "' ";
                }
                sql += " GROUP BY PROCESSNAME,STEPLABEL ) T";
            }
            else if (ReportId == "3")
            {
                sql = @"SELECT ROW_NUMBER() OVER(ORDER BY T.[DAYS] DESC) AS NUM,T.ASSIGNEDTOUSER AS USERACCOUNT,T.*,'' AS USERNAME,'' AS ORGNAME FROM (SELECT Convert(decimal(18,2),Convert(decimal(18,2),SUM(DATEDIFF(d,STARTTIME,isnull(ENDTIME,GETDATE()))))/Convert(decimal(18,2),COUNT(TASKID))) AS [DAYS],Convert(decimal(18,2),Convert(decimal(18,2),SUM(DATEDIFF(d,STARTTIME,isnull(ENDTIME,GETDATE()))))/Convert(decimal(18,2),COUNT(TASKID))) * 24 AS [HOURS],ASSIGNEDTOUSER FROM [dbo].TASKS WHERE 1=1 ";
                if (ProcessName != "")
                {
                    sql += " AND '" + ProcessName + @"' LIKE '%'+ RTRIM(PROCESSNAME) + '%' ";
                }
                if (StartTime != "")
                {
                    sql += " AND StartTime >= '" + StartTime + "' ";
                }
                if (EndTime != "")
                {
                    sql += " AND StartTime <= '" + EndTime + "' ";
                }
                sql += @" GROUP BY ASSIGNEDTOUSER ) T ";
                            //LEFT JOIN " + "" + @".DBO.V_EMPLOYEE ON T.ASSIGNEDTOUSER = V_EMPLOYEE.SHORTNAME
                            //LEFT JOIN " + "" + @".DBO.ORG_DEPARTMENT ON V_EMPLOYEE.DEPARTMENTID = ORG_DEPARTMENT.DEPARTMENTID";
            }

            if (Type == "1")
            {
                int skipResults = (Convert.ToInt32(Page) - 1) * 10;
                string sqlstring = "select * from (" + sql + ") t1 where t1.num>" + skipResults + " and t1.num <=" + (skipResults + 10).ToString();

                DataTable dt = DataAccess.Instance("UltDB").ExecuteDataTable(sqlstring);
                dt.TableName = "TABLE";

                //如果是人员报表，需要获取姓名和组织
                if (ReportId == "3") 
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string userName = "";
                        string orgName = "";
                        DataTable temtDt = DataAccess.Instance("BizDB").ExecuteDataTable(@"SELECT ISNULL(ORG_USER.USERNAME,'') AS USERNAME,ISNULL(ORG_DEPARTMENT.DEPARTMENTNAME,'') AS ORGNAME FROM ORG_USER 
                          LEFT JOIN V_ORG_USERDEPARTMENT ON ORG_USER.USERID = V_ORG_USERDEPARTMENT.USERID
                          LEFT JOIN ORG_DEPARTMENT ON ORG_DEPARTMENT.DEPARTMENTID = V_ORG_USERDEPARTMENT.DEPARTMENTID
                          WHERE ORG_USER.LOGINNAME='" + dt.Rows[i]["USERACCOUNT"].ToString() + "'");
                        if (temtDt.Rows.Count > 0) { userName = temtDt.Rows[0]["USERNAME"].ToString(); orgName = temtDt.Rows[0]["ORGNAME"].ToString(); }
                        dt.Rows[i]["USERNAME"] = userName;
                        dt.Rows[i]["ORGNAME"] = orgName;
                    }
                }

                string jsonString = ToJson(dt);
                context.Response.Write(jsonString); // 返回客户端json格式数据
            }
            else if (Type == "2")
            {
                string sqlcount = @"select count(1) from (" + sql + ") t1";
                string count = DataAccess.Instance("UltDB").ExecuteScalar(sqlcount).ToString();
                context.Response.Write("{'count':'" + count + "'}");
            }
        }

        //获取流程名称
        public void GetProcessInfo(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string sql = @"SELECT CATEGORYID AS RESOURCEID,DISPLAYNAME as CNNAME FROM WF_PROCESSCATEGORY ORDER BY ORDERNO
                SELECT ID AS RESOURCEID,PROCESSNAME as CNNAME,CATEGORYID AS PARENTID FROM WF_PROCESS ORDER BY ORDERNO";
            string sql2 = @"SELECT PROCESSNAME FROM PROCESSES ";
            DataSet ds = DataAccess.Instance("BizDB").ExecuteDataSet(sql);
            DataTable dt1 = ds.Tables[0]; //流程分类
            DataTable dt2 = ds.Tables[1]; //有流程分类的流程
            DataTable dt3 = DataAccess.Instance("UltDB").ExecuteDataTable(sql2); //所有发布过的流程

            string select = "";
            //循环流程分类
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                select += "<optgroup label='" + dt1.Rows[i]["CNNAME"].ToString() + "'>";
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    if (dt2.Rows[j]["PARENTID"].ToString() == dt1.Rows[i]["RESOURCEID"].ToString())
                    {
                        select += "<option value='" + dt2.Rows[j]["CNNAME"].ToString().Trim() + "'>" + dt2.Rows[j]["CNNAME"].ToString().Trim() + "</option>";
                    }
                }
                select += "</optgroup>";
            }
            //没有分类的均为“其它”
            select += "<optgroup label='其它'>";
            for (int j = 0; j < dt3.Rows.Count; j++)
            {
                //判断没有分类的流程是否在dt2中已存在
                DataRow[] dataRows = dt2.Select("CNNAME='" + dt3.Rows[j]["PROCESSNAME"].ToString() + "'");
                if (dataRows.Length.Equals(1) == false) {
                    select += "<option value='" + dt3.Rows[j]["PROCESSNAME"].ToString().Trim() + "'>" + dt3.Rows[j]["PROCESSNAME"].ToString().Trim() + "</option>";
                }
            }
            select += "</optgroup>";

            string jsonString = "{\"selectInfo\":\"" + select + "\"}";
            context.Response.Write(jsonString); // 返回客户端json格式数据
        }

        //获取流程图表信息 top10
        public void GetProcessTimeConsuming(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string ReportId = context.Request["ReportId"].ToString(); //报表Id
            string ProcessName = context.Request["ProcessName"].ToString();
            string StartTime = context.Request["StartTime"].ToString();
            string EndTime = context.Request["EndTime"].ToString();
            //BIZ库数据库名
            //string bizDataBase = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["BizDB"].ConnectionString).InitialCatalog;

            //流程耗时
            string sql = @"SELECT TOP 10 Convert(decimal(18,2),Convert(decimal(18,2),SUM(DATEDIFF(d,STARTTIME,isnull(ENDTIME,GETDATE()))))/Convert(decimal(18,2),COUNT(INCIDENT))) AS TS,PROCESSNAME FROM [dbo].[INCIDENTS] WHERE 1=1 ";
            if (ProcessName != "")
            {
                sql += " AND '" + ProcessName + @"' LIKE '%'+ RTRIM(PROCESSNAME) + '%' ";
            }
            if (StartTime != "")
            {
                sql += " AND StartTime >= '" + StartTime + "' ";
            }
            if (EndTime != "")
            {
                sql += " AND StartTime <= '" + EndTime + "' ";
            }
            sql += " GROUP BY PROCESSNAME ORDER BY TS DESC";

            //流程步骤耗时
            if (ReportId == "2")
            {
                sql = @"SELECT TOP 10 Convert(decimal(18,2),Convert(decimal(18,2),SUM(DATEDIFF(d,STARTTIME,isnull(ENDTIME,GETDATE()))))/Convert(decimal(18,2),COUNT(TASKID))) AS TS,RTRIM(STEPLABEL) + '\n' + RTRIM(PROCESSNAME) AS PROCESSNAME FROM [dbo].TASKS WHERE 1=1 ";
                if (ProcessName != "")
                {
                    sql += " AND '" + ProcessName + @"' LIKE '%'+ RTRIM(PROCESSNAME) + '%' ";
                }
                if (StartTime != "")
                {
                    sql += " AND StartTime >= '" + StartTime + "' ";
                }
                if (EndTime != "")
                {
                    sql += " AND StartTime <= '" + EndTime + "' ";
                }
                sql += " GROUP BY PROCESSNAME,STEPLABEL ORDER BY TS DESC";
            }
            //人员耗时
            else if (ReportId == "3")
            {
                sql = @"SELECT TOP 10 Convert(decimal(18,2),Convert(decimal(18,2),SUM(DATEDIFF(d,STARTTIME,isnull(ENDTIME,GETDATE()))))/Convert(decimal(18,2),COUNT(TASKID))) AS TS,ASSIGNEDTOUSER AS PROCESSNAME FROM [dbo].TASKS WHERE 1=1 ";
                if (ProcessName != "")
                {
                    sql += " AND '" + ProcessName + @"' LIKE '%'+ RTRIM(PROCESSNAME) + '%' ";
                }
                if (StartTime != "")
                {
                    sql += " AND StartTime >= '" + StartTime + "' ";
                }
                if (EndTime != "")
                {
                    sql += " AND StartTime <= '" + EndTime + "' ";
                }
                sql += @" GROUP BY ASSIGNEDTOUSER ORDER BY TS DESC ";
            }

            DataTable dt = DataAccess.Instance("UltDB").ExecuteDataTable(sql);
            
            //如果是人员报表，需要获取姓名
            if (ReportId == "3")
            {
                for (int i = 0; i < dt.Rows.Count; i++) 
                {
                    string userName = "";
                    DataTable temtDt = DataAccess.Instance("BizDB").ExecuteDataTable("SELECT TOP 1 isnull(USERNAME,'') AS NAME FROM ORG_USER WHERE LOGINNAME='" + dt.Rows[i]["PROCESSNAME"].ToString() + "'");
                    if (temtDt.Rows.Count > 0) userName = temtDt.Rows[0]["NAME"].ToString();
                    dt.Rows[i]["PROCESSNAME"] = userName;
                }
            }

            string[] color = { "#FF0F00", "#FF6600", "#FF9E01", "#FCD202", "#F8FF01", "#B0DE09", "#04D215", "#0D8ECF", "#0D52D1", "#2A0CD0" };

            string jsonString = "";
            jsonString += "[";
            for (int i = 0; i < 10; i++)
            {
                jsonString += "{";

                if (i >= dt.Rows.Count)
                {
                    jsonString += "\"PROCESSNAME\": \"\",\"TS\": 0, \"color\": \"" + color[i] + "\"";
                }
                else
                {
                    jsonString += "\"PROCESSNAME\": \"" + dt.Rows[i]["PROCESSNAME"].ToString() + "\",\"TS\": " + dt.Rows[i]["TS"].ToString() + ", \"color\": \"" + color[i] + "\"";
                }
                jsonString += "}";
                if (i < 9)
                {
                    jsonString += ",";
                }
            }
            jsonString += "]";

            context.Response.Write(jsonString); // 返回客户端json格式数据
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