using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml.Serialization;
using System.Data;
using MyLib;
using System.Text;
using Ultimus.UWF.Form.Entity;

namespace Ultimus.UWF.Form
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class AutoSearchHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                string xml = context.Request.Form["xml"].ToString().Trim();
                string key = context.Request.Form["key"].ToString().Trim();

                StringReader sr = new StringReader(xml);
                XmlSerializer xs = new XmlSerializer(typeof(SearchEntity));
                SearchEntity se = xs.Deserialize(sr) as SearchEntity;
                DataTable dt = new DataTable();
                List<ColumnEntity> list = se.Columns;

                DataAccess db = null;
                if (!String.IsNullOrEmpty(se.ConnectionString))
                {
                    db = new DataAccess(se.ConnectionString);
                }
                else
                {
                    db = new DataAccess();
                }

                string strsql = "select distinct top " + se.SearchCount + " ";
                string strwhere = " where 1=1 ";
                bool isbegin = true;
                int count = 0;
                foreach (ColumnEntity item in list)
                {
                    string columnName = item.Column;
                    if (!string.IsNullOrEmpty(item.ColumnName))
                    {
                        columnName = item.ColumnName;
                    }
                    if (String.IsNullOrEmpty(item.DisplayConditions))
                    {
                        if (item.Column.Contains("Date") || item.Column.Contains("date") || item.Column.Contains("DATE"))
                        {
                            strsql += "CONVERT(varchar(100), " + item.Column + ", 23) as  " + columnName + ",";
                        }
                        else
                        {
                            strsql += item.Column + " as " + columnName + ",";
                        }
                    }
                    else
                    {
                        strsql += item.DisplayConditions + " as " + columnName + ",";
                    }
                    if (!String.IsNullOrEmpty(key))
                    {
                        if (isbegin && list.Count == 1)
                        {
                            strwhere += "and (" + item.Column + " like N'%" + key + "%')";
                            isbegin = false;
                        }
                        else if (isbegin)
                        {
                            strwhere += "and (" + item.Column + " like N'%" + key + "%' ";
                            isbegin = false;
                        }
                        else if (!isbegin && count < list.Count - 1)
                        {
                            strwhere += "or " + item.Column + " like N'%" + key + "%' ";
                        }
                        else
                        {
                            strwhere += "or " + item.Column + " like N'%" + key + "%') ";
                        }
                    }
                    count++;
                }
                strsql = strsql.TrimEnd(',');
                strsql += " from " + se.TableName + "";
                if (!String.IsNullOrEmpty(se.StrWhere))
                {
                    strwhere += "and " + se.StrWhere;
                }
                strsql += strwhere;
                strsql += " " + se.OrderBy;

                dt = db.ExecuteDataTable(strsql);

                string returnValue = "";
                if (dt.Rows.Count > 0)
                {
                    int Count = 1;
                    returnValue += "{'SearchType':'" + se.SearchType + "',data:[";
                    foreach (DataRow row in dt.Rows)
                    {
                        returnValue += "{'rowIndex':'" + Count + "',data:[";
                        foreach (ColumnEntity item in list)
                        {
                            string columnName = item.Column;
                            if (!string.IsNullOrEmpty(item.ColumnName))
                            {
                                columnName = item.ColumnName;
                            }
                            returnValue += "{'title':'" + item.Title.Replace("\n", "$br$").Replace("<br/>", "$br$") + "','Column':'" + columnName + "','value':'" + row[columnName].ToString().Replace("'", "&#39;").Replace("\"", "&#34;").Replace("\n", "").Replace("\r", "").Replace("\r\n", "") + "','Display':'" + item.Display + "'},";
                        }
                        returnValue = returnValue.TrimEnd(',');
                        returnValue += "]},";
                        Count++;
                    }
                    returnValue = returnValue.TrimEnd(',');
                    returnValue += "]}";
                }
                returnValue = StringFormat(returnValue);
                context.Response.Write(returnValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>   
        /// 格式化字符型 
        /// </summary>   
        /// <param name="str"></param>    
        /// <returns></returns>   
        private static string StringFormat(string str)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                char c = str.ToCharArray()[i];
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