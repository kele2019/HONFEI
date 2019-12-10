<%@ WebHandler Language="C#" Class="GetTableInformcation" %>

using System;
using System.Web;
using System.Data;

public class GetTableInformcation : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string tableName = context.Request.Form["tableName"].ToString().Trim();
        string connectionString = context.Request.Form["connectionString"].ToString().Trim();
        string strSql = "";
        if (System.Configuration.ConfigurationManager.AppSettings["DataBaseType"].ToString().ToUpper() == "ORACLE")
        {
            strSql = @"select
                             A.column_name 字段名,A.data_type 数据类型,A.data_length 长度,A.nullable 允许空值,A.Data_default 缺省值,B.comments 备注
                         from
                             user_tab_columns A,user_col_comments B
                         where
                             A.Table_Name = B.Table_Name
                             and A.Column_Name = B.Column_Name
                             and A.Table_Name = '" + tableName + "'";
        }
        else if (System.Configuration.ConfigurationManager.AppSettings["DataBaseType"].ToString().ToUpper() == "SQL")
        {
            strSql = @"SELECT      
                         a.name as 字段名,  --字段名 
                         b.name as 数据类型,  --类型 
                         COLUMNPROPERTY(a.id,a.name,'PRECISION' ) as 长度,   --长度
                         (case when a.isnullable=1 then 'Y' else '' end) as 允许空值,   --允许空
                         isnull(e.text,'' ) as 缺省值,   --默认值
                         isnull(g.[value],'' ) AS 备注   --备注 
                         FROM syscolumns a left join systypes b    
                         on a.xtype=b.xusertype   
                         inner join sysobjects d    
                         on a.id=d.id and (d.xtype='U' or d.xtype='V') and d.name<> 'dtproperties'    
                         left join syscomments e   
                         on a.cdefault=e.id   
                         left join sys.extended_properties g   
                         on a.id=g.major_id AND a.colid = g.minor_id    
                         where d.name='" + tableName + @"' --所要查询的表   
                         order by a.id,a.colorder ";
        }
        DataSet ds = new DataBase(connectionString.Replace("/", "\\")).QueryTable(strSql);
        string json = "";
        if (ds.Tables[0].Rows.Count > 0)
        {
            json += "[";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                json += "{'ColumnName':'" + row["字段名"].ToString().Trim() + "',"
                      + "'DataPrecision':'" + row["备注"].ToString().Trim().Replace("'","") + "'},";
            }
            json = json.Substring(0, json.LastIndexOf(','));
            json += "]";
        }
        context.Response.Write(json);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}