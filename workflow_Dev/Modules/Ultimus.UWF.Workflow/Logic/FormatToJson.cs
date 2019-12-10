using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ultimus.UWF.Workflow.Logic
{
    public class FormatToJson
    {
        /// <summary> 
        /// Datatable转换为Json 
        /// </summary> 
        /// <param name="table">Datatable对象</param> 
        /// <returns>Json字符串</returns> 
        public static string DataTableToJson(DataTable dt)
        {
            StringBuilder jsonString = new StringBuilder();

            if (dt.Rows.Count == 0)
            {
                jsonString.Append("[{}]");
                return jsonString.ToString();
            }

            jsonString.Append("[");
            DataRowCollection drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strKey = dt.Columns[j].ColumnName;
                    string strValue = drc[i][j].ToString().Trim();
                    Type type = dt.Columns[j].DataType;
                    jsonString.Append("\"" + strKey + "\":");
                    strValue = TypeFormat(strValue, type);
                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.Append(strValue + ",");
                    }
                    else
                    {
                        jsonString.Append(strValue);
                    }
                }
                jsonString.Append("},");
            }
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            return jsonString.ToString();
        }
        /// <summary> 
        /// Datatable转换为Json 
        /// </summary> 
        /// <param name="table">Datatable对象</param> 
        /// <returns>Json字符串</returns> 
        public static string DataRowToJson(DataTable dt,int status,string message)
        {
            StringBuilder jsonString = new StringBuilder();
            string strFormat = "\"status\":{0},\"message\":\"{1}\",\"data\":";
            jsonString.Append("{");
            jsonString.Append(string.Format(strFormat,status,message));
            if (dt.Rows.Count == 0)
            {
                jsonString.Append("{}");
                jsonString.Append("}");
                return jsonString.ToString();
            }
            
            DataRowCollection drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strKey = dt.Columns[j].ColumnName;
                    string strValue = drc[i][j].ToString().Trim();
                    Type type = dt.Columns[j].DataType;
                    jsonString.Append("\"" + strKey + "\":");
                    strValue = TypeFormat(strValue, type);
                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.Append(strValue + ",");
                    }
                    else
                    {
                        jsonString.Append(strValue);
                    }
                }
                jsonString.Append("}");
                if (i < drc.Count - 1)
                    jsonString.Append(",");
            }
            
            jsonString.Append("}");
            return jsonString.ToString();
        }
        /// <summary> 
        /// DataSet转换为分页Json 
        /// </summary> 
        /// <param name="dataSet">DataSet对象</param> 
        /// <returns>Json字符串</returns> 
        public static string DataSetPagerToJson(DataSet dataSet,int status, string message)
        {
            string jsonString = "{\"status\":" + status + ",\"message\":\"" + message + "\",";
            string tableName = "TotalRows";
            for (int i = 0; i < dataSet.Tables.Count; i++)
            {
                if (i > 0)
                {
                    tableName = "DataList";
                    if (dataSet.Tables[i].Rows.Count > 0)
                    {
                        jsonString += "\"" + tableName + "\":" + DataTableToJson(dataSet.Tables[i]) + ",";
                    }
                    else
                    {
                        jsonString += "\"" + tableName + "\":[],";
                    }
                }
                else
                {
                    jsonString += "\"" + tableName + "\":" + dataSet.Tables[i].Rows[0][0].ToString() + ",";
                }
            }
            jsonString = jsonString.TrimEnd(',');
            return jsonString + "}";
        }
        /// <summary> 
        /// DataSet转换为分页Json 
        /// </summary> 
        /// <param name="dataSet">DataSet对象</param> 
        /// <returns>Json字符串</returns> 
        public static string DataSetPagerToJson(DataSet dataSet) {
            string jsonString = "{";
            string tableName = "TotalRows";
            for (int i = 0; i < dataSet.Tables.Count; i++) {
                if (i > 0) {
                    tableName = "DataList";
                    if (dataSet.Tables[i].Rows.Count > 0) {
                        jsonString += "\"" + tableName + "\":" + DataTableToJson(dataSet.Tables[i]) + ",";
                    }
                    else {
                        jsonString += "\"" + tableName + "\":[],";
                    }
                }
                else {
                    jsonString += "\"" + tableName + "\":" + dataSet.Tables[i].Rows[0][0].ToString() + ",";
                }
            }
            jsonString = jsonString.TrimEnd(',');
            return jsonString + "}";
        }
        /// <summary> 
        /// 过滤特殊字符 
        /// </summary> 
        /// <param name="s"></param> 
        /// <returns></returns> 
        private static string StringJson(string s)
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
        /// <summary> 
        /// 格式化字符型、日期型、布尔型 
        /// </summary> 
        /// <param name="str"></param> 
        /// <param name="type"></param> 
        /// <returns></returns> 
        private static string TypeFormat(string str, Type type)
        {
            if (type == typeof(string))
            {
                str = StringJson(str);
                str = "\"" + str + "\"";
            }
            else if (type == typeof(DateTime))
            {
                if(String.IsNullOrEmpty(str))
                    str = "\"\"";
                else
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
    }
}
