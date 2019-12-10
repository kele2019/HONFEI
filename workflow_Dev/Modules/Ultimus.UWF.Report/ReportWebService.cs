using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;

/// <summary>
///ReportWebService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class ReportWebService : System.Web.Services.WebService {

    [WebMethod]
    public List<string> GetDataBaseTablesName(string connectionString)
    {
        DataBase db = new DataBase(connectionString.Replace("/", "\\"));
        string strSql = "";
        if (System.Configuration.ConfigurationManager.AppSettings["DataBaseType"].ToString().ToUpper() == "ORACLE")
        {
            strSql = "select table_name name from user_tables";
        }
        else if (System.Configuration.ConfigurationManager.AppSettings["DataBaseType"].ToString().ToUpper() == "SQL")
        {
            strSql = "select name from sys.objects where type = 'U' or type = 'V' order by name asc";
        }
        DataSet ds = db.QueryTable(strSql);
        List<string> list = new List<string>();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                list.Add(row["name"].ToString());
            }
        }
        return list;
    }

    [WebMethod]
    public List<TableInformation> GetTableInformation(string tableName, string connectionString)
    {
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
        List<TableInformation> list = new List<TableInformation>();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                TableInformation table = new TableInformation();
                table.ColumnName = row["字段名"].ToString().Trim();
                table.DataType = row["数据类型"].ToString().Trim();
                table.DataLength = row["长度"].ToString().Trim();
                table.DataPrecision = row["备注"].ToString().Trim();
                table.NullLable = row["允许空值"].ToString().Trim();
                table.DataDefault = row["缺省值"].ToString().Trim();
                list.Add(table);
            }
        }
        return list;
    }
    
}
