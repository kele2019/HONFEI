using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MyLib;
using ClientService;
using System.Text;
using ClientService.WorkflowSrv;

namespace MobileClient.Web.ProcessPage
{
    /// <summary>
    /// Ajax 的摘要说明
    /// </summary>
    public class Ajax : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            switch (context.Request["tag"])
            {
                case "GetUserInfo":
                    GetUserInfo(context);
                    break;
                case "CarbonOrBackUp":
                    CarbonOrBackUp(context);
                    break;
                case "CheckFormLink":
                    CheckFormLink(context);
                    break;
            }
        }

        //获取用户信息
        public void GetUserInfo(HttpContext context)
        {
            string domain = ConfigurationManager.AppSettings["Domain"].ToString();
            string searchText = "%" + context.Request["searchText"].ToString() + "%";
            //string sql = @"SELECT ID,NAME FROM T_PROCEDUREINFO WHERE TYPE = '" + typeId + "' ORDER BY CODE")
            //StrSQL.AppendFormat("select * from (select ID as EMPLOYEEID ,FULLNAME as FIRSTNAME,' ' as LASTNAME,NAME as SHORTNAME,JOBFUNCTION as TITLE, '' as EMAIL,SUPERVISORID as SUPERVISORID,DEPTID as DEPARTMENTID FROM JOBS) ve where title like '%{0}%' or firstname like '%{0}%' or shortname like '%{0}%' or email like '%{0}%'", txtSearch.Text.Trim());
            //    DataTable dt = DataAccess.Instance("UltDB").ExecuteDataTable(StrSQL.ToString());
            //DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(sql);
            WorkflowRef _ref = new WorkflowRef();
            UserEntity[] uelist = _ref.GetUserInfoBySearchText(searchText);
            DataTable dt = new DataTable();
            dt.TableName = "UserInfo";
            dt.Columns.Add("Name");
            dt.Columns.Add("LoginName");
            dt.Columns.Add("LoginNameShow");
            dt.Columns.Add("DepartName");
            int count = 1;
            foreach (UserEntity ue in uelist)
            {
                if (count == 20) { break; }
                DataRow dr = dt.NewRow();
                dr["Name"] = ue.USERNAME;
                dr["LoginName"] = "USER:org=" + domain + ", user=" + domain + "/" + ue.LOGINNAME;
                dr["LoginNameShow"] = ue.LOGINNAME.Replace(domain + "/", "");
                dr["DepartName"] = ue.DEPARTMENT;
                dt.Rows.Add(dr);
                count++;
            }
            string json = ToJson(dt);

            context.Response.Write(json);
            context.Response.End();
        }

        //抄送或协办
        public void CarbonOrBackUp(HttpContext context)
        {
            string taskId = context.Request["taskId"].ToString();
            string processName = context.Request["procName"].ToString();
            string txtUserId = context.Request["txtUserId"].ToString();
            string[] txtUserId_arr = txtUserId.Split('|');
            string userId = "";
            foreach (string temp in txtUserId_arr) 
            {
                string[] temp_arr = temp.Split(',');
                userId += temp_arr[1].Substring(6) + ",";
            }
            userId = userId.TrimEnd(',');
            string txtUsers = context.Request["txtUsers"].ToString();
            string txtRemark = context.Request["txtRemark"].ToString();
            WorkflowRef _ref = new WorkflowRef();
            string result = _ref.CarbonOrBackUp(taskId, processName, userId, txtUsers, txtRemark);
            if (result == "")
            {
                context.Response.Write("{result:'发起流程失败，传入参数不全'}");
            }
            else 
            {
                context.Response.Write("{result:'" + result + "'}"); 
            }
            context.Response.End();
        }

        //判断表单是否配置了移动
        public void CheckFormLink(HttpContext context)
        {
            string result = "failure";
            string taskId = context.Request["taskId"].ToString();
            string processName = "";
            string stepLabel = "";
            string incident = "";
            if (taskId != "") 
            {
                //string taskId = taskUrl.Substring(taskUrl.IndexOf("TaskId=") + 7).Trim();
                string sql = "select PROCESSNAME,STEPLABEL,INCIDENT from TASKS where TASKID='" + taskId + "'";
                DataTable dt = DataAccess.Instance("UltDB").ExecuteDataTable(sql);
                if (dt.Rows.Count > 0) 
                {
                    processName = dt.Rows[0]["PROCESSNAME"].ToString();
                    stepLabel = dt.Rows[0]["STEPLABEL"].ToString();
                    incident = dt.Rows[0]["INCIDENT"].ToString();
                    string sql2 = @"select STEPCNAME,STEPENAME from [MOBILECLIENT_STEP]
                      left join [MOBILECLIENT_PROCESS] on [MOBILECLIENT_STEP].FK_ID = [MOBILECLIENT_PROCESS].ID
                      where [MOBILECLIENT_PROCESS].PROCESSNAME='" + processName.Trim() + "' and [MOBILECLIENT_STEP].STEPNAME='" + stepLabel.Trim() + "'";
                    DataTable dt2 = DataAccess.Instance("BizDB").ExecuteDataTable(sql2);
                    if (dt2.Rows.Count > 0) 
                    {
                        if (dt2.Rows[0]["STEPCNAME"].ToString() != "" && dt2.Rows[0]["STEPCNAME"].ToString().ToUpper() != "NULL") 
                        {
                            result = "success";
                        }
                    }
                }
            }
            context.Response.Write("{result:'" + result + "',processName:'" + processName.Trim() + "',stepLabel:'" + stepLabel.Trim() + "',incident:'" + incident.Trim() + "',rootPath:'" + ConfigurationManager.AppSettings["RootPath"].ToString() + "'}"); 
        }

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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}