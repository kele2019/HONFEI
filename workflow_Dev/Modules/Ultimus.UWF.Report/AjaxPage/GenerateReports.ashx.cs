using System;
using System.Web;
using System.Text;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using Ultimus;
using Ultimus.OC;
using Ultimus.WFServer;

namespace BPM.ReportDesign.AjaxPage
{
    /// <summary>
    /// GenerateReports 的摘要说明
    /// </summary>
    public class GenerateReports : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        #region 变量申明
        /// <summary>
        /// 是否是修改
        /// </summary>
        private string isAlter;
        /// <summary>
        /// 修改报表的唯一标示
        /// </summary>
        private string AlterGuid;
        /// <summary>
        /// 连接字符串
        /// </summary>
        private string connectionString;
        /// <summary>
        /// 报表名称
        /// </summary>
        private string ReportName;
        /// <summary>
        /// 是否分页
        /// </summary>
        private string whetherPaging;
        /// <summary>
        /// 每页显示条数
        /// </summary>
        private string articleThatNumber;
        /// <summary>
        /// 排序规则
        /// </summary>
        private string SortStyle;
        /// <summary>
        /// 选择的表和字段
        /// </summary>
        private string tablesAndFields;
        /// <summary>
        /// 表之间关系
        /// </summary>
        private string tableRelation;
        /// <summary>
        /// 字段规则
        /// </summary>
        private string fieldRules;
        /// <summary>
        /// 查询条件
        /// </summary>
        private string selectWhere;
        /// <summary>
        /// 别名
        /// </summary>
        private string alias;
        /// <summary>
        /// Json序列化对象
        /// </summary>
        private JavaScriptSerializer ScriptSerializerObject = new JavaScriptSerializer();
        /// <summary>
        /// 排序规则
        /// </summary>
        private SortType Sort;
        /// <summary>
        /// 表和字段
        /// </summary>
        private List<TablesAndFields> Tables_Fields_List;
        /// <summary>
        /// 表之间的关系
        /// </summary>
        private List<TableRelation> Relation;
        /// <summary>
        /// 字段规则
        /// </summary>
        private List<FieldRules> Rules;
        /// <summary>
        /// 查询条件
        /// </summary>
        private List<SelectWhere> Where;
        /// <summary>
        /// 字段别名
        /// </summary>
        private List<Alias> ListAlias;
        /// <summary>
        /// 模板的页面字符串
        /// </summary>
        private string TemplatePageString;
        /// <summary>
        /// 模板的后台字符串
        /// </summary>
        private string TemplatePageClassString;
        /// <summary>
        /// 模板的控件文件
        /// </summary>
        private string TemplatePageDesignerString;
        /// <summary>
        /// 模板的控件替换字符串
        /// </summary>
        private string TemplateDesignerString;
        /// <summary>
        /// 模板中的表格列头
        /// </summary>
        private string PageFieldItems = "<th class=\"reptTH\" style=\"width:20px;\">No.</th>";
        /// <summary>
        /// Repeater中的数据绑定列
        /// </summary>
        private string RepeaterBingRow = "<td class=\"reptTD\" style=\"border-right-width:1px;\" ><%# Container.ItemIndex + 1%></td>";
        /// <summary>
        /// 统计有多少列
        /// </summary>
        private int CellCount = 1;
        /// <summary>
        /// 分页控件页面代码
        /// </summary>
        private string PagewhetherPaging;
        /// <summary>
        /// 分页控件后台代码
        /// </summary>
        private string PagewhetherPagingClass;
        /// <summary>
        /// 报表后台的SQL语句
        /// </summary>
        private string ReportSQL;
        /// <summary>
        /// 报表后台查询记录总数SQL语句
        /// </summary>
        private string ReportSelectPageCountSQL;
        /// <summary>
        /// 报表后台导出Excel的SQL语句
        /// </summary>
        private string ExportToExcelSQL;
        /// <summary>
        /// SQL中查询的表名
        /// </summary>
        private string SqlTables;
        /// <summary>
        /// 报表页面中查询部分
        /// </summary>
        private string SelectWhereHead;
        /// <summary>
        /// 分组查询的字段
        /// </summary>
        private string GroupByField;
        /// <summary>
        /// 判断是否有汇总的字段规则
        /// </summary>
        private bool isGroup = false;
        /// <summary>
        /// 报表页面中的查询条件代码
        /// </summary>
        private string SelectWhere;
        /// <summary>
        /// 报表后台中的查询条件代码
        /// </summary>
        private string TemplateReportClassWhere;
        /// <summary>
        /// 报表页面中的查询按钮
        /// </summary>
        private string SearchButtonString;
        /// <summary>
        /// 报表后台中的查询方法
        /// </summary>
        private string SearchClassFunction;
        /// <summary>
        /// 后台中的绑定数据方法
        /// </summary>
        private string BingDateFunction;
        /// <summary>
        /// 后台中查询数据库的方法
        /// </summary>
        private string DataBaseFunction;
        /// <summary>
        /// 后台中导出Excel的方法
        /// </summary>
        private string ExportToExcel;
        /// <summary>
        /// 数据库类型
        /// </summary>
        private string DBType = System.Configuration.ConfigurationManager.AppSettings["DataBaseType"].ToString().ToUpper();
        /// <summary>
        /// 权限类型
        /// </summary>
        private string ViewType;
        /// <summary>
        /// 权限查看的人或部门
        /// </summary>
        private string ViewUser;
        /// <summary>
        /// 权限查看的名词
        /// </summary>
        private string ViewName;
        /// <summary>
        /// 人员帐号字段
        /// </summary>
        private string UserAccount;
        private int WhereCount = 0;
        #endregion

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Request.ContentEncoding = Encoding.UTF8;
            try
            {

                #region 接收参数
                this.connectionString = context.Request.Form["connectionString"].ToString().Replace("/", "\\");
                this.ReportName = context.Request.Form["reportName"].ToString();
                this.whetherPaging = context.Request.Form["whetherPaging"].ToString();
                this.articleThatNumber = context.Request.Form["articleThatNumber"].ToString();
                this.SortStyle = context.Request.Form["sortStyle"].ToString();
                this.tablesAndFields = context.Request.Form["TablesAndFields"].ToString();
                this.tableRelation = context.Request.Form["tableRelation"].ToString();
                this.fieldRules = context.Request.Form["fieldRules"].ToString();
                this.selectWhere = context.Request.Form["selectWhere"].ToString();
                this.alias = context.Request.Form["alias"].ToString();
                this.isAlter = context.Request.Form["isAlter"].ToString();
                this.AlterGuid = context.Request.Form["AlterGuid"].ToString();
                this.ViewType = context.Request.Form["ViewType"].ToString();
                this.ViewUser = context.Request.Form["ViewUser"].ToString();
                this.UserAccount = context.Request.Form["UserAccount"].ToString();
                this.ViewName = context.Request.Form["ViewUserName"].ToString();
                string tableName = context.Request.Form["tableName"].ToString();
                string processName = context.Request.Form["processName"].ToString();
                #endregion

                this.ScriptSerializer();

                this.ReaderTemplateFile(context);

                this.GetSelectTablesSQL();

                if (this.Rules != null && this.Rules.Count > 0)
                {
                    foreach (FieldRules fr in this.Rules)
                    {
                        if (fr.Rules.ToUpper() == "SUM")
                        {
                            this.isGroup = true;
                            break;
                        }
                    }
                }

                this.GetSelectFields();

                this.GetSelectWhere();

                string name = DateTime.Now.ToString("yyyyMMddhhmmss");

                string SystemConnectionString = new clsOleDB().getConn("ReportDesignSystem");
                string OleFilePath1 = ""; string OleFilePath2 = ""; string OleFilePath3 = "";
                string OleFileName = "";
                if (isAlter == "Y")
                {
                    DataSet ds = new DataBase(SystemConnectionString).QueryTable("select * from COM_REPORTDESIGN where guid='" + AlterGuid + "'");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        OleFileName = ds.Tables[0].Rows[0]["REPORTPAGEPATH"].ToString();
                        OleFilePath1 = context.Server.MapPath("../GenerateReportFiles/" + OleFileName);
                        OleFilePath2 = context.Server.MapPath("../GenerateReportFiles/" + OleFileName + ".designer.cs");
                        OleFilePath3 = context.Server.MapPath("../GenerateReportFiles/" + OleFileName + ".cs");
                        File.Delete(OleFilePath1);
                        File.Delete(OleFilePath2);
                        File.Delete(OleFilePath3);
                        name = OleFileName.Substring(OleFileName.IndexOf('_') + 1);
                        name = name.Substring(0, name.LastIndexOf('.'));
                    }
                }

                //替换模板页面中对应的标示
                this.TemplatePageString = this.TemplatePageString.Replace("Inherits=\"BPM.ReportDesign.GenerateReportFiles.Template\"", "Inherits=\"BPM.ReportDesign.GenerateReportFiles.Template_" + name + "\"");
                this.TemplatePageString = this.TemplatePageString.Replace("CodeFile=\"Template.aspx.cs\"", "CodeFile=\"Template_" + name + ".aspx.cs\"");
                this.TemplatePageString = this.TemplatePageString.Replace("$ReportName$", this.ReportName);
                this.TemplatePageString = this.TemplatePageString.Replace("$TemplateTableHead$", this.PageFieldItems);
                this.TemplatePageString = this.TemplatePageString.Replace("$CellCount$", this.CellCount.ToString());
                this.TemplatePageString = this.TemplatePageString.Replace("$TableWidth$", (this.CellCount * 12).ToString());
                this.TemplatePageString = this.TemplatePageString.Replace("$RepeaterBingRow$", this.RepeaterBingRow);
                this.TemplatePageString = this.TemplatePageString.Replace("$whetherPaging$", this.PagewhetherPaging);
                this.TemplatePageString = this.TemplatePageString.Replace("$SelectWhere$", this.SelectWhere);
                this.TemplatePageString = this.TemplatePageString.Replace("$SearchButton$", this.SearchButtonString);
                this.TemplatePageString = this.TemplatePageString.Replace("$SelectWhereHead$", this.SelectWhereHead);


                this.GetClassFunction();

                //替换模板后台代码标示
                this.TemplatePageClassString = this.TemplatePageClassString.Replace("public partial class Template : System.Web.UI.Page", "public partial class Template_" + name + " : System.Web.UI.Page");
                this.TemplatePageClassString = this.TemplatePageClassString.Replace("//$PagewhetherPaging$", this.PagewhetherPagingClass);
                this.TemplatePageClassString = this.TemplatePageClassString.Replace("//$BingDataFunction$", this.BingDateFunction);
                this.TemplatePageClassString = this.TemplatePageClassString.Replace("//$DataBaseQueryFunction$", this.DataBaseFunction);
                this.TemplatePageClassString = this.TemplatePageClassString.Replace("//$SearchFunction$", this.SearchClassFunction);
                this.TemplatePageClassString = this.TemplatePageClassString.Replace("//$ExportToExcel$", this.ExportToExcel);

                //替换模板的控件文件
                this.TemplatePageDesignerString = this.TemplatePageDesignerString.Replace("public partial class Template", "public partial class Template_" + name + "");
                this.TemplatePageDesignerString = this.TemplatePageDesignerString.Replace("namespace BPM.ReportDesign.Template", "namespace BPM.ReportDesign.GenerateReportFiles");
                this.TemplatePageDesignerString = this.TemplatePageDesignerString.Replace("//$SystemControls$", this.TemplateDesignerString);


                if (OleFileName == "")
                {
                    string NewReportPagePath = context.Server.MapPath("../GenerateReportFiles/Template_" + name + ".aspx");
                    File.WriteAllText(NewReportPagePath, this.TemplatePageString, Encoding.UTF8);

                    string NewReportClassPath = context.Server.MapPath("../GenerateReportFiles/Template_" + name + ".aspx.cs");
                    File.WriteAllText(NewReportClassPath, this.TemplatePageClassString, Encoding.UTF8);

                    string NewReportDesignerPath = context.Server.MapPath("../GenerateReportFiles/Template_" + name + ".aspx.designer.cs");
                    File.WriteAllText(NewReportDesignerPath, this.TemplatePageDesignerString, Encoding.UTF8);
                }
                else
                {
                    File.WriteAllText(context.Server.MapPath("../GenerateReportFiles/" + OleFileName), this.TemplatePageString, Encoding.UTF8);
                    File.WriteAllText(context.Server.MapPath("../GenerateReportFiles/" + OleFileName + ".cs"), this.TemplatePageClassString, Encoding.UTF8);
                    File.WriteAllText(context.Server.MapPath("../GenerateReportFiles/" + OleFileName + ".designer.cs"), this.TemplatePageDesignerString, Encoding.UTF8);
                }
                //将信息保存到数据库
                string SaveReportInforationSQL = "";
                if (isAlter == "N")
                {
                    SaveReportInforationSQL = "insert into COM_REPORTDESIGN "
                                            + "("
                                            + "GUID, "
                                            + "USERCODE, "
                                            + "USERNAME, "
                                            + "CREATDATE, "
                                            + "REPORTPAGEPATH, "
                                            + "REPORTNAME, "
                                            + "CONNECTIONSTRING, "
                                            + "TABLESNAMES_FIELDNAMES, "
                                            + "TABLERELATION, "
                                            + "FIELDRULES, "
                                            + "SELECTWHERE, "
                                            + "ALIAS, "
                                            + "WHETHERPAGING, "
                                            + "ARTICLETHATNUMBER, "
                                            + "SORTSTYLE, "
                                            + "VIEWPRIVILEGE, "
                                            + "USERACCOUNTFIELDNAME, "
                                            + "VIEWREPORTUSERACCOUNT, "
                                            + "VIEWREPORTUSERNAME,"
                                            + "PROCESSNAME,"
                                            + "TABLENAME "
                                            + ")"
                                            + " values "
                                            + "("
                                            + "'" + Guid.NewGuid().ToString() + "',"
                                            + "'',"
                                            + "'',"
                                            + "" + (this.DBType == "ORACLE" ? "(select sysdate from dual)" : "getdate()") + ","
                                            + "'Template_" + name + ".aspx',"
                                            + "'" + this.ReportName + "',"
                                            + "'" + this.connectionString + "',"
                                            + "'" + this.tablesAndFields.Replace("'", "\"") + "',"
                                            + "'" + this.tableRelation.Replace("'", "\"") + "',"
                                            + "'" + this.fieldRules.Replace("'", "\"") + "',"
                                            + "'" + this.selectWhere.Replace("'", "\"") + "',"
                                            + "'" + this.alias.Replace("'", "\"") + "',"
                                            + "'" + this.whetherPaging + "',"
                                            + "'" + this.articleThatNumber + "',"
                                            + "'" + this.SortStyle.Replace("'", "\"") + "',"
                                            + "'" + this.ViewType + "',"
                                            + "'" + this.ViewUser.Replace("'", "\"") + "',"
                                            + "'" + this.UserAccount + "',"
                                            + "'" + this.ViewName + "',"
                                            + "'" + processName + "',"
                                            + "'" + tableName + "'"
                                            + ")";
                }
                else
                {
                    SaveReportInforationSQL = "update COM_REPORTDESIGN set "
                                            + "REPORTPAGEPATH='" + OleFileName + "',"
                                            + "REPORTNAME='" + this.ReportName + "',"
                                            + "CONNECTIONSTRING='" + this.connectionString + "',"
                                            + "TABLESNAMES_FIELDNAMES='" + this.tablesAndFields.Replace("'", "\"") + "',"
                                            + "TABLERELATION='" + this.tableRelation.Replace("'", "\"") + "',"
                                            + "FIELDRULES='" + this.fieldRules.Replace("'", "\"") + "',"
                                            + "SELECTWHERE='" + this.selectWhere.Replace("'", "\"") + "',"
                                            + "ALIAS='" + this.alias.Replace("'", "\"") + "',"
                                            + "WHETHERPAGING='" + this.whetherPaging + "',"
                                            + "ARTICLETHATNUMBER='" + this.articleThatNumber + "',"
                                            + "SORTSTYLE='" + this.SortStyle.Replace("'", "\"") + "',"
                                            + "UPDATEDATE=getdate(),"
                                            + "VIEWPRIVILEGE='" + this.ViewType + "',"
                                            + "USERACCOUNTFIELDNAME='" + this.ViewUser.Replace("'", "\"") + "',"
                                            + "VIEWREPORTUSERACCOUNT='" + this.UserAccount + "',"
                                            + "VIEWREPORTUSERNAME='" + this.ViewName + "',"
                                            + "PROCESSNAME='" + processName + "',"
                                            + "TABLENAME='" + tableName + "' "
                                            + "where GUID='" + AlterGuid + "'";
                }

                if (new DataBase(SystemConnectionString).ExecuteNonQuery(SaveReportInforationSQL) > 0)
                {
                    context.Response.Write("");
                }
                else
                {
                    context.Response.Write("数据保存失败,请联系管理员.");
                }
                GC.Collect();
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 将接收的参数序列化城对象
        /// </summary>
        public void ScriptSerializer()
        {
            if (this.SortStyle != string.Empty)
            {
                this.Sort = this.ScriptSerializerObject.Deserialize(this.SortStyle, typeof(SortType)) as SortType;
            }
            if (this.tablesAndFields != string.Empty)
            {
                this.Tables_Fields_List = this.ScriptSerializerObject.Deserialize(this.tablesAndFields, typeof(List<TablesAndFields>)) as List<TablesAndFields>;
            }
            if (this.tableRelation != string.Empty)
            {
                this.Relation = this.ScriptSerializerObject.Deserialize(this.tableRelation, typeof(List<TableRelation>)) as List<TableRelation>;
            }
            if (this.fieldRules != string.Empty)
            {
                this.Rules = this.ScriptSerializerObject.Deserialize(this.fieldRules, typeof(List<FieldRules>)) as List<FieldRules>;
            }
            if (this.selectWhere != string.Empty)
            {
                this.Where = this.ScriptSerializerObject.Deserialize(this.selectWhere, typeof(List<SelectWhere>)) as List<SelectWhere>;
            }
            if (this.alias != string.Empty)
            {
                this.ListAlias = this.ScriptSerializerObject.Deserialize(this.alias, typeof(List<Alias>)) as List<Alias>;
            }
        }

        /// <summary>
        /// 读取模板文件
        /// </summary>
        /// <param name="context"></param>
        public void ReaderTemplateFile(HttpContext context)
        {
            //读取模板的页面文件
            string TemplatePagePath = context.Server.MapPath("../Template/Template.aspx");
            FileInfo TemplatePage = new FileInfo(TemplatePagePath);
            StreamReader TemplatePageReader = TemplatePage.OpenText();
            this.TemplatePageString = TemplatePageReader.ReadToEnd();

            //读取模板的后台代码文件
            string TemplatePageClassPath = context.Server.MapPath("../Template/Template.aspx.cs");
            FileInfo TemplatePageClass = new FileInfo(TemplatePageClassPath);
            StreamReader TemplatePageClassReader = TemplatePageClass.OpenText();
            this.TemplatePageClassString = TemplatePageClassReader.ReadToEnd();

            //读取模板的控件文件
            string TemplatePageDesignerPath = context.Server.MapPath("../Template/Template.aspx.designer.cs");
            FileInfo TemplatePageDesigner = new FileInfo(TemplatePageDesignerPath);
            StreamReader TemplatePageDesignerReader = TemplatePageDesigner.OpenText();
            this.TemplatePageDesignerString = TemplatePageDesignerReader.ReadToEnd();
        }

        /// <summary>
        /// 拼接查询的表名
        /// </summary>
        public void GetSelectTablesSQL()
        {
            //SQL语句中的表名
            bool isStart = true;
            if (this.Relation != null && this.Relation.Count > 0)
            {
                foreach (TableRelation sr in Relation)
                {
                    if (isStart)
                    {
                        this.SqlTables += sr.TableName1 + " with(nolock) " + sr.Relation + " " + sr.TableName2 + " with(nolock) ON ";
                        foreach (TableRelationFields field in sr.Fields)
                        {
                            this.SqlTables += field.TableName1 + "." + field.Table1FieldName + " " + field.FieldRelation + " " + field.TableName2 + "." + field.Table2FieldName + " AND ";
                        }
                        this.SqlTables = this.SqlTables.Substring(0, this.SqlTables.LastIndexOf("AND"));
                        isStart = false;
                    }
                    else
                    {
                        this.SqlTables += " " + sr.Relation + " " + sr.TableName2 + " with(nolock) ON ";
                        foreach (TableRelationFields field in sr.Fields)
                        {
                            this.SqlTables += field.TableName1 + "." + field.Table1FieldName + " " + field.FieldRelation + " " + field.TableName2 + "." + field.Table2FieldName + " AND ";
                        }
                        this.SqlTables = this.SqlTables.Substring(0, this.SqlTables.LastIndexOf("AND"));
                    }
                }
            }
            else
            {
                //如果没有添加表对应关系，则默认只查询第一个表
                this.SqlTables = Tables_Fields_List[0].TableName + " with(nolock) ";
            }
        }

        /// <summary>
        /// 拼接需要查询的字段、分页的页面和后台方法
        /// </summary>
        public void GetSelectFields()
        {
            if (this.DBType == "ORACLE")
            {
                if (this.whetherPaging == "Y")
                {
                    this.ReportSQL = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM "
                                   + "(SELECT ";
                    //导出SQL
                    this.ExportToExcelSQL += "SELECT ";
                    //查询总页数SQL
                    this.ReportSelectPageCountSQL = "SELECT count(*) FROM (select ";
                    //循环别名集合,拼接SQL语句中查询的字段
                    foreach (Alias alias_field in this.ListAlias)
                    {
                        string SqlFieldName = alias_field.SliasName;
                        string FieldName = alias_field.TableName.Trim() + "." + alias_field.FieldName.Trim();
                        //有汇总字段规则，其他没有汇总的字段则需要放到分组查询中
                        if (this.isGroup)
                        {
                            //循环字段规则
                            if (this.Rules != null && this.Rules.Count > 0)
                            {
                                foreach (FieldRules fr in this.Rules)
                                {
                                    string fr_field = fr.TableName.Trim() + "." + fr.FieldName.Trim();
                                    //根据字段规则进行匹配
                                    if (fr_field == FieldName && fr.Rules.ToUpper() == "SUM")
                                    {
                                        FieldName = "SUM(" + FieldName + ")";
                                    }
                                    else if (fr_field == FieldName && fr.Rules.ToUpper() == "GROUPBY")
                                    {
                                        this.GroupByField += FieldName + ",";
                                    }
                                    else
                                    {
                                        this.GroupByField += FieldName + ",";
                                    }
                                }
                            }
                        }

                        //页面中的表格标题行
                        this.PageFieldItems += "<th class=\"reptTH\" style=\"width:" + alias_field.Width + "%;\">" + SqlFieldName + "</th>";
                        //数据绑定控件
                        if (alias_field.OpenUrl.Trim() != "")
                        {
                            if (alias_field.OpenUrl.IndexOf("?") > 0)
                            {
                                string UrlDate = alias_field.OpenUrl.Split('?')[1];
                                string[] DateItems = UrlDate.Split('&');
                                string OpenDate = alias_field.OpenUrl.Split('?')[0] + "?";
                                for (int d = 0; d < DateItems.Length; d++)
                                {
                                    string[] sss = DateItems[d].Split('=');
                                    for (int g = 0; g < sss.Length; g++)
                                    {
                                        if (g % 2 == 0)
                                        {
                                            OpenDate += DateItems[d].Split('=')[0] + "=<%# Server.UrlEncode(Eval('" + DateItems[d].Split('=')[1].Replace("{", "").Replace("}", "") + "').ToString()) %>&";
                                        }
                                    }
                                }
                                OpenDate = OpenDate.Substring(0, OpenDate.LastIndexOf('&'));
                                this.RepeaterBingRow += "<td class=\"reptTD\" style=\"width:" + alias_field.Width + "%;\"><a href=\"" + OpenDate + "\" style=\"color: Blue;\"  target=\"_blank\"><%# Eval(\"" + SqlFieldName + "\") %></a></td>";
                            }
                            else
                            {
                                this.RepeaterBingRow += "<td class=\"reptTD\" style=\"width:" + alias_field.Width + "%;\"><a href=\"javascript:void(0)\" style=\"color: Blue;\"  target=\"_blank\" onclick=\"window.open('" + alias_field.OpenUrl.Trim() + "?" + alias_field.FieldName + "=<%# Eval(\"" + SqlFieldName + "\") %>');\"><%# Eval(\"" + SqlFieldName + "\") %></a></td>";
                            }
                        }
                        else
                        {
                            this.RepeaterBingRow += "<td class=\"reptTD\" style=\"width:" + alias_field.Width + "%;\"><%# Eval(\"" + SqlFieldName + "\") %></td>";
                        }
                        this.CellCount++;

                        //查询总页数SQL
                        this.ReportSelectPageCountSQL += FieldName + " AS " + SqlFieldName + ",";
                        //sql语句
                        this.ReportSQL += FieldName + " AS " + SqlFieldName + ",";
                        //导出SQL
                        this.ExportToExcelSQL += FieldName + " AS " + SqlFieldName + ",";
                    }

                    //删除最后一个逗号
                    this.ReportSQL = this.ReportSQL.Substring(0, this.ReportSQL.LastIndexOf(','));
                    //查询总页数SQL
                    this.ReportSelectPageCountSQL = this.ReportSelectPageCountSQL.Substring(0, this.ReportSelectPageCountSQL.LastIndexOf(','));
                    //分组字段
                    if (this.GroupByField != null && this.GroupByField.Length > 0)
                    {
                        this.GroupByField = this.GroupByField.Substring(0, this.GroupByField.LastIndexOf(','));
                    }
                    //导出SQL
                    this.ExportToExcelSQL = this.ExportToExcelSQL.Substring(0, this.ExportToExcelSQL.LastIndexOf(','));

                    this.ReportSQL += " FROM "
                                   + "" + this.SqlTables + " "
                                   + " WHERE 1=1 \"+strWhere+\" "
                                   + "" + (this.GroupByField != null ? " group by " + this.GroupByField : "") + ""
                                   + " order by " + (this.isGroup == false ? "" + Sort.TableName + "." + Sort.FieldName.Trim() + " " + Sort.SortStyle + "" : "" + Sort.FieldName.Trim() + " " + Sort.SortStyle + "") + ""
                                   + ") A WHERE ROWNUM <= \"+AspNetPager1.CurrentPageIndex+\"*" + this.articleThatNumber + ") WHERE RN >= (\"+AspNetPager1.CurrentPageIndex+\"-1) *" + this.articleThatNumber + "";
                    //导出SQL
                    this.ExportToExcelSQL += " FROM " + this.SqlTables + " WHERE 1=1 \"+strWhere+\" " + (this.GroupByField != null ? "group by " + this.GroupByField : "") + " order by " + (this.isGroup == false ? "" + Sort.TableName + "." + Sort.FieldName.Trim() + " " + Sort.SortStyle + "" : "" + Sort.FieldName.Trim() + " " + Sort.SortStyle + "") + "";
                    //查询总页数SQL
                    this.ReportSelectPageCountSQL += " FROM " + this.SqlTables + " WHERE 1=1 \"+strWhere+\" " + (this.GroupByField != null ? "group by " + this.GroupByField : "") + ")";

                    //页面中的分页控件
                    this.PagewhetherPaging = "<webdiyer:AspNetPager ID=\"AspNetPager1\" runat=\"server\" CustomInfoHTML=\"Count %RecordCount%\" "
                                           + "HorizontalAlign=\"right\" Width=\"100%\" CssClass=\"aspNetPager\" OnPageChanged=\"AspNetPager1_PageChanged\" "
                                           + "PageSize=\"" + this.articleThatNumber + "\" AlwaysShow=\"true\" SubmitButtonStyle=\"display:none\" InputBoxStyle=\"display:none\" "
                                           + "NextPageText=\"下一页\" FirstPageText=\"首页\" LastPageText=\"末页\" PrevPageText=\"上一页\">"
                                           + "</webdiyer:AspNetPager>";
                    //分页控件的后台方法
                    this.PagewhetherPagingClass += "protected void AspNetPager1_PageChanged(object sender, EventArgs e) \r\n"
                                                + "{ \r\n"
                                                + "  BingData(); \r\n"
                                                + "} \r\n"
                                                + "\r\n"
                                                + "public int PageCountQuery(string strWhere) \r\n"
                                                + "{ \r\n"
                                                + "DataSet ds = ExecuteNonQuery(\"" + ReportSelectPageCountSQL.ToString() + "\"); \r\n"
                                                + "return int.Parse(ds.Tables[0].Rows[0][0].ToString()); \r\n"
                                                + "} \r\n";
                }
                else
                {
                    this.ReportSQL = "select ";
                    this.ExportToExcelSQL = "select ";
                    foreach (Alias alias_field in this.ListAlias)
                    {
                        string SqlFieldName = alias_field.SliasName;
                        string FieldName = alias_field.TableName + "." + alias_field.FieldName;
                        //有汇总字段规则，其他没有汇总的字段则需要放到分组查询中
                        if (this.isGroup)
                        {
                            //循环字段规则
                            if (this.Rules != null && this.Rules.Count > 0)
                            {
                                foreach (FieldRules fr in this.Rules)
                                {
                                    string fr_field = fr.TableName.Trim() + "." + fr.FieldName.Trim();
                                    //根据字段规则进行匹配
                                    if (fr_field == FieldName && fr.Rules.ToUpper() == "SUM")
                                    {
                                        FieldName = "SUM(" + FieldName + ")";
                                    }
                                    else if (fr_field == FieldName && fr.Rules.ToUpper() == "GROUPBY")
                                    {
                                        this.GroupByField += FieldName + ",";
                                    }
                                    else
                                    {
                                        this.GroupByField += FieldName + ",";
                                    }
                                }
                            }
                        }
                        this.ReportSQL += FieldName + " AS " + SqlFieldName + ",";
                        //导出SQL
                        this.ExportToExcelSQL += FieldName + " AS " + SqlFieldName + ",";
                        //页面中的表格标题行
                        this.PageFieldItems += "<th  class=\"reptTH\" style=\"width:" + alias_field.Width + "%;\">" + SqlFieldName + "</th>";

                        if (alias_field.OpenUrl.Trim() != "")
                        {
                            if (alias_field.OpenUrl.IndexOf("?") > 0)
                            {
                                string UrlDate = alias_field.OpenUrl.Split('?')[1];
                                string[] DateItems = UrlDate.Split('&');
                                string OpenDate = alias_field.OpenUrl.Split('?')[0] + "?";
                                for (int d = 0; d < DateItems.Length; d++)
                                {
                                    string[] sss = DateItems[d].Split('=');
                                    for (int g = 0; g < sss.Length; g++)
                                    {
                                        if (g % 2 == 0)
                                        {
                                            OpenDate += DateItems[d].Split('=')[0] + "=<%# Server.UrlEncode(Eval('" + DateItems[d].Split('=')[1].Replace("{", "").Replace("}", "") + "').ToString()) %>&";
                                        }
                                    }
                                }
                                OpenDate = OpenDate.Substring(0, OpenDate.LastIndexOf('&'));
                                this.RepeaterBingRow += "<td class=\"reptTD\" style=\"width:" + alias_field.Width + "%;\"><a href=\"" + OpenDate + "\" style=\"color: Blue;\"  target=\"_blank\"><%# Eval(\"" + SqlFieldName + "\") %></a></td>";
                            }
                            else
                            {
                                this.RepeaterBingRow += "<td class=\"reptTD\" style=\"width:" + alias_field.Width + "%;\"><a href=\"javascript:void(0)\" style=\"color: Blue;\"  target=\"_blank\" onclick=\"window.open('" + alias_field.OpenUrl.Trim() + "?" + alias_field.FieldName + "=<%# Eval(\"" + SqlFieldName + "\") %>');\"><%# Eval(\"" + SqlFieldName + "\") %></a></td>";
                            }
                        }
                        else
                        {
                            this.RepeaterBingRow += "<td class=\"reptTD\" style=\"width:" + alias_field.Width + "%;\"><%# Eval(\"" + SqlFieldName + "\") %></td>";
                        }
                        this.CellCount++;
                    }
                    //删除最后一个逗号
                    this.ReportSQL = this.ReportSQL.Substring(0, this.ReportSQL.LastIndexOf(','));
                    //导出SQL
                    this.ExportToExcelSQL = this.ExportToExcelSQL.Substring(0, this.ExportToExcelSQL.LastIndexOf(','));

                    this.ReportSQL += " FROM " + this.SqlTables + " " + (this.GroupByField != null ? "group by " + this.GroupByField : "") + " order by " + Sort.TableName.Trim() + "." + Sort.FieldName.Trim() + " " + Sort.SortStyle + "";
                }
            }
            else//SQL
            {
                string EmpCode = "";
                if (this.whetherPaging == "Y")
                {
                    this.ReportSQL = "select * from (select  row_number() over(order by " + Sort.TableName + "." + Sort.FieldName + " " + Sort.SortStyle + ") as rowid,";
                    //导出SQL
                    this.ExportToExcelSQL += "SELECT ";
                    //循环别名集合,拼接SQL语句中查询的字段
                    foreach (Alias alias_field in this.ListAlias)
                    {
                        string SqlFieldName = alias_field.SliasName.Trim();
                        string FieldName = alias_field.TableName.Trim() + "." + alias_field.FieldName.Trim();

                        //有汇总字段规则，其他没有汇总的字段则需要放到分组查询中
                        if (this.isGroup)
                        {
                            //循环字段规则
                            if (this.Rules != null && this.Rules.Count > 0)
                            {
                                foreach (FieldRules fr in this.Rules)
                                {
                                    string fr_field = fr.TableName.Trim() + "." + fr.FieldName.Trim();
                                    //根据字段规则进行匹配
                                    if (fr_field == FieldName && fr.Rules.ToUpper() == "SUM")
                                    {
                                        FieldName = "SUM(" + FieldName + ")";
                                    }
                                    else if (fr_field == FieldName && fr.Rules.ToUpper() == "GROUPBY")
                                    {
                                        this.GroupByField += FieldName + ",";
                                    }
                                    else
                                    {
                                        this.GroupByField += FieldName + ",";
                                    }
                                }
                            }

                        }
                        if (this.UserAccount == FieldName)
                        {
                            EmpCode = SqlFieldName;
                        }
                        if (alias_field.IsHide != "Y")
                        {
                            //页面中的表格标题行
                            this.PageFieldItems += "<th class=\"reptTH\" style=\"width:" + alias_field.Width + "%;\">" + SqlFieldName + "</th>";
                            //数据绑定控件
                            if (alias_field.OpenUrl.Trim() != "")
                            {
                                if (alias_field.OpenUrl.IndexOf("?") > 0)
                                {
                                    string UrlDate = alias_field.OpenUrl.Split('?')[1];
                                    string[] DateItems = UrlDate.Split('&');
                                    string OpenDate = alias_field.OpenUrl.Split('?')[0] + "?";
                                    for (int d = 0; d < DateItems.Length; d++)
                                    {
                                        string[] sss = DateItems[d].Split('=');
                                        for (int g = 0; g < sss.Length; g++)
                                        {
                                            if (g % 2 == 0)
                                            {
                                                OpenDate += DateItems[d].Split('=')[0] + "=<%# Server.UrlEncode( Eval(\"" + DateItems[d].Split('=')[1].Replace("{", "").Replace("}", "") + "\").ToString()) %>&";
                                            }
                                        }
                                    }
                                    OpenDate = OpenDate.Substring(0, OpenDate.LastIndexOf('&'));
                                    this.RepeaterBingRow += "<td class=\"reptTD\" style=\"width:" + alias_field.Width + "%;\"><a href='" + OpenDate + "' style=\"color: Blue;\"  target=\"_blank\"><%# Eval(\"" + SqlFieldName + "\") %></a></td>";
                                }
                                else
                                {
                                    this.RepeaterBingRow += "<td class=\"reptTD\" style=\"width:" + alias_field.Width + "%;\"><a href=\"javascript:void(0)\" style=\"color: Blue;\"  target=\"_blank\" onclick=\"window.open('" + alias_field.OpenUrl.Trim() + "?" + alias_field.FieldName + "=<%# Eval(\"" + SqlFieldName + "\") %>');\"><%# Eval(\"" + SqlFieldName + "\") %></a></td>";
                                }
                            }
                            else
                            {
                                this.RepeaterBingRow += "<td class=\"reptTD\" style=\"width:" + alias_field.Width + "%;\"><%# Eval(\"" + SqlFieldName + "\")%></td>";
                            }
                            //导出SQL
                            this.ExportToExcelSQL += FieldName + " AS '" + SqlFieldName + "',";
                            this.CellCount++;
                        }
                        //sql语句
                        this.ReportSQL += FieldName + " AS '" + SqlFieldName + "',";
                    }

                    //删除最后一个逗号
                    this.ReportSQL = this.ReportSQL.Substring(0, this.ReportSQL.LastIndexOf(','));

                    //分组字段
                    if (this.GroupByField != null && this.GroupByField.Length > 0)
                    {
                        this.GroupByField = this.GroupByField.Substring(0, this.GroupByField.LastIndexOf(','));
                    }
                    //导出SQL
                    this.ExportToExcelSQL = this.ExportToExcelSQL.Substring(0, this.ExportToExcelSQL.LastIndexOf(','));

                    string viewstring = "";
                    if (this.ViewType == "1")
                    {
                        viewstring = "and " + this.UserAccount + " in ( \"+new GetUserInfo().GetDepart()+\" ) ";
                    }
                    string strReportConfigPath = ConfigurationManager.AppSettings["ReportConfigPath"].ToString().Trim();
                    if (this.ViewType == "2")
                    {
                        viewstring = " and \"+(new clsOleDB(\"" + strReportConfigPath + "\").getConn(\"" + this.ReportName + "\").IndexOf(UserName)>=0?\" 1=1 \":\"" + this.UserAccount + " in (\"+new GetUserInfo().GetDepart()+\")\")+\"";
                        File.AppendAllLines(strReportConfigPath, new string[] { this.ReportName + "=" + this.ViewUser }, Encoding.UTF8);
                    }

                    this.ReportSQL += " FROM "
                                   + "" + this.SqlTables + " where 1=1 " + viewstring + " \"+strWhere+\" " + (this.GroupByField != null ? " group by " + this.GroupByField : "") + ") a"
                                   + " WHERE (a.rowid > (\"+AspNetPager1.CurrentPageIndex+\"-1) *" + this.articleThatNumber + " and a.rowid <= \"+AspNetPager1.CurrentPageIndex+\"*" + this.articleThatNumber + ")";
                    //导出SQL
                    this.ExportToExcelSQL += " FROM " + this.SqlTables + " WHERE 1=1 " + viewstring + " \"+strWhere+\" " + (this.GroupByField != null ? "group by " + this.GroupByField : "") + " order by " + (this.isGroup == false ? "" + Sort.TableName + "." + Sort.FieldName.Trim() + " " + Sort.SortStyle + "" : "" + Sort.FieldName.Trim() + " " + Sort.SortStyle + "") + "";

                    //查询总页数SQL
                    this.ReportSelectPageCountSQL = "SELECT count(*) FROM " + this.SqlTables + " WHERE 1=1 " + viewstring + " \"+strWhere+\" " + (this.GroupByField != null ? "group by " + this.GroupByField : "") + "";

                    //页面中的分页控件
                    this.PagewhetherPaging = "<webdiyer:AspNetPager ID=\"AspNetPager1\" runat=\"server\" CustomInfoHTML=\"Count %RecordCount%\" "
                                           + "HorizontalAlign=\"left\" Width=\"100%\" CssClass=\"aspNetPager\" OnPageChanged=\"AspNetPager1_PageChanged\" "
                                           + "PageSize=\"" + this.articleThatNumber + "\" AlwaysShow=\"true\" SubmitButtonStyle=\"display:none\" InputBoxStyle=\"display:none\" "
                                           + "NextPageText=\"下一页\" FirstPageText=\"首页\" LastPageText=\"末页\" PrevPageText=\"上一页\">"
                                           + "</webdiyer:AspNetPager>";
                    //分页控件的后台方法
                    this.PagewhetherPagingClass += "protected void AspNetPager1_PageChanged(object sender, EventArgs e) \r\n"
                                                + "{ \r\n"
                                                + "  BingData(); \r\n"
                                                + "} \r\n"
                                                + "\r\n"
                                                + "public int PageCountQuery(string strWhere) \r\n"
                                                + "{ \r\n"
                                                + "DataSet ds = ExecuteNonQuery(\"" + ReportSelectPageCountSQL.ToString() + "\"); \r\n"
                                                + "return int.Parse(ds.Tables[0].Rows[0][0].ToString()); \r\n"
                                                + "} \r\n";
                }
                else
                {
                    this.ReportSQL = "select ";
                    this.ExportToExcelSQL = "select ";
                    foreach (Alias alias_field in this.ListAlias)
                    {
                        string SqlFieldName = alias_field.SliasName;
                        string FieldName = alias_field.TableName + "." + alias_field.FieldName;

                        //有汇总字段规则，其他没有汇总的字段则需要放到分组查询中
                        if (this.isGroup)
                        {
                            //循环字段规则
                            if (this.Rules != null && this.Rules.Count > 0)
                            {
                                foreach (FieldRules fr in this.Rules)
                                {
                                    string fr_field = fr.TableName.Trim() + "." + fr.FieldName.Trim();
                                    //根据字段规则进行匹配
                                    if (fr_field == FieldName && fr.Rules.ToUpper() == "SUM")
                                    {
                                        FieldName = "SUM(" + FieldName + ")";
                                    }
                                    else if (fr_field == FieldName && fr.Rules.ToUpper() == "GROUPBY")
                                    {
                                        this.GroupByField += FieldName + ",";
                                    }
                                    else
                                    {
                                        this.GroupByField += FieldName + ",";
                                    }
                                }
                            }
                        }
                        if (alias_field.IsHide != "Y")
                        {
                            //页面中的表格标题行
                            this.PageFieldItems += "<th class=\"reptTH\">" + SqlFieldName + "</th>";

                            if (alias_field.OpenUrl.Trim() != "")
                            {
                                if (alias_field.OpenUrl.IndexOf("?") > 0)
                                {
                                    string UrlDate = alias_field.OpenUrl.Split('?')[1];
                                    string[] DateItems = UrlDate.Split('&');
                                    string OpenDate = alias_field.OpenUrl.Split('?')[0] + "?";
                                    for (int d = 0; d < DateItems.Length; d++)
                                    {
                                        string[] sss = DateItems[d].Split('=');
                                        for (int g = 0; g < sss.Length; g++)
                                        {
                                            if (g % 2 == 0)
                                            {
                                                OpenDate += DateItems[d].Split('=')[0] + "=<%# Server.UrlEncode(Eval(\"" + DateItems[d].Split('=')[1].Replace("{", "").Replace("}", "") + "\").ToString()) %>&";
                                            }
                                        }
                                    }
                                    OpenDate = OpenDate.Substring(0, OpenDate.LastIndexOf('&'));
                                    this.RepeaterBingRow += "<td class=\"reptTD\" style=\"width:" + alias_field.Width + "%;\"><a href='" + OpenDate + "' style=\"color: Blue;\"  target=\"_blank\"><%# Eval(\"" + SqlFieldName + "\") %></a></td>";
                                }
                                else
                                {
                                    this.RepeaterBingRow += "<td class=\"reptTD\" style=\"width:" + alias_field.Width + "%;\"><a href=\"javascript:void(0)\" style=\"color: Blue;\"  target=\"_blank\" onclick=\"window.open('" + alias_field.OpenUrl.Trim() + "?" + alias_field.FieldName + "=<%# Eval(\"" + SqlFieldName + "\") %>');\"><%# Eval(\"" + SqlFieldName + "\") %></a></td>";
                                }
                            }
                            else
                            {
                                this.RepeaterBingRow += "<td class=\"reptTD\" style=\"width:" + alias_field.Width + "%;\"><%# Eval(\"" + SqlFieldName + "\") %></td>";
                            }

                            this.ExportToExcelSQL += FieldName + " AS " + SqlFieldName + ",";
                            this.CellCount++;
                        }
                        this.ReportSQL += FieldName + " AS " + SqlFieldName + ",";
                    }
                    string strReportConfigPath = ConfigurationManager.AppSettings["ReportConfigPath"].ToString().Trim();
                    string viewstring = "";
                    if (this.ViewType == "1")
                    {
                        viewstring = "and " + this.UserAccount + " in ( \"+new GetUserInfo().GetDepart()+\" ) ";
                    }
                    if (this.ViewType == "2")
                    {
                        viewstring = " and \"+(new clsOleDB(\"" + strReportConfigPath + "\").getConn(\"" + this.ReportName + "\").IndexOf(UserName)>=0?\" 1=1 \":\"" + this.UserAccount + " in (\"+new GetUserInfo().GetDepart()+\")\")+\"";
                    }

                    //删除最后一个逗号
                    this.ReportSQL = this.ReportSQL.Substring(0, this.ReportSQL.LastIndexOf(','));
                    //导出SQL
                    this.ExportToExcelSQL = this.ExportToExcelSQL.Substring(0, this.ExportToExcelSQL.LastIndexOf(','));

                    this.ReportSQL += " FROM " + this.SqlTables + " where 1=1 " + viewstring + " \"+strWhere+\" " + (this.GroupByField != null ? "group by " + this.GroupByField : "") + " order by " + Sort.TableName.Trim() + "." + Sort.FieldName.Trim() + " " + Sort.SortStyle + "";
                    //导出SQL
                    this.ExportToExcelSQL += " FROM " + this.SqlTables + " WHERE 1=1 " + viewstring + " \"+strWhere+\" " + (this.GroupByField != null ? "group by " + this.GroupByField : "") + " order by " + (this.isGroup == false ? "" + Sort.TableName + "." + Sort.FieldName.Trim() + " " + Sort.SortStyle + "" : "" + Sort.FieldName.Trim() + " " + Sort.SortStyle + "") + "";
                }
            }
        }

        /// <summary>
        /// 拼接报表页面中的查询和后台的代码
        /// </summary>
        public void GetSelectWhere()
        {
            //模板中的查询条件
            if (this.Where != null && this.Where.Count > 0)
            {
                this.SelectWhere += "<tr>";
                foreach (SelectWhere sw in this.Where)
                {
                    if (sw.TimeSelect == "N" && sw.MoneySelect == "N")
                    {
                        this.SelectWhere += "<td width=\"33%\">"
                                         + "    <label  style=\"white-space:nowrap;word-break:break-all;\">" + sw.DisplayListing + "</label>"
                                         + "    <asp:TextBox ID=\"" + sw.TableField + "\" runat=\"server\" CssClass=\"inputboder2\"></asp:TextBox>"
                                         + "</td>";
                        this.TemplateDesignerString += "protected global::System.Web.UI.WebControls.TextBox " + sw.TableField + "; \r\n";
                        this.WhereCount++;
                    }
                    else if (sw.TimeSelect == "Y" && sw.MoneySelect == "N")
                    {
                        this.SelectWhere += "<td width=\"33%\">"
                                        + "    <label  style=\"white-space:nowrap;word-break:break-all;\">" + sw.DisplayListing + "(开始)</label>"
                                        + "    <asp:TextBox ID=\"" + sw.TableField + "1\" runat=\"server\" onclick=\"WdatePicker()\" CssClass=\"inputboder2 Wdate\" style=\"cursor:pointer;\"></asp:TextBox>"
                                        + "</td>";
                        this.TemplateDesignerString += "protected global::System.Web.UI.WebControls.TextBox " + sw.TableField + "1; \r\n";
                        this.WhereCount++;
                        //每行3个查询条件
                        if (this.WhereCount == 3)
                        {
                            this.SelectWhere += "</tr><tr>";
                            this.WhereCount = 0;
                        }
                        this.SelectWhere += "<td width=\"33%\">"
                                        + "    <label  style=\"white-space:nowrap;word-break:break-all;\">" + sw.DisplayListing + "(结束)</label>"
                                        + "    <asp:TextBox ID=\"" + sw.TableField + "2\" runat=\"server\" onclick=\"WdatePicker()\" CssClass=\"inputboder2 Wdate\" style=\"cursor:pointer;\"></asp:TextBox>"
                                        + "</td>";
                        this.WhereCount++;
                        this.TemplateDesignerString += "protected global::System.Web.UI.WebControls.TextBox " + sw.TableField + "2; \r\n";
                    }
                    else if (sw.MoneySelect == "Y")
                    {
                        this.SelectWhere += "<td width=\"33%\">"
                                        + "    <label  style=\"white-space:nowrap;word-break:break-all;\">" + sw.DisplayListing + "(最小)</label>"
                                        + "    <asp:TextBox ID=\"" + sw.TableField + "1\" runat=\"server\" CssClass=\"inputboder2\" style=\"cursor:pointer;\"></asp:TextBox>"
                                        + "</td>";
                        this.TemplateDesignerString += "protected global::System.Web.UI.WebControls.TextBox " + sw.TableField + "1; \r\n";
                        this.WhereCount++;
                        //每行3个查询条件
                        if (this.WhereCount == 3)
                        {
                            this.SelectWhere += "</tr><tr>";
                            this.WhereCount = 0;
                        }
                        this.SelectWhere += "<td width=\"33%\">"
                                        + "    <label  style=\"white-space:nowrap;word-break:break-all;\">" + sw.DisplayListing + "(最大)</label>"
                                        + "    <asp:TextBox ID=\"" + sw.TableField + "2\" runat=\"server\" CssClass=\"inputboder2\" style=\"cursor:pointer;\"></asp:TextBox>"
                                        + "</td>";
                        this.WhereCount++;
                        this.TemplateDesignerString += "protected global::System.Web.UI.WebControls.TextBox " + sw.TableField + "2; \r\n";
                    }
                    //每行3个查询条件
                    if (this.WhereCount == 3)
                    {
                        this.SelectWhere += "</tr><tr>";
                        this.WhereCount = 0;
                    }
                    //启用模糊查询
                    if (sw.FuzzyInquires.ToUpper() == "Y" && sw.TimeSelect == "N" && sw.MoneySelect == "N")
                    {
                        this.TemplateReportClassWhere += "if (" + sw.TableField + ".Text.Trim() != \"\") \r\n"
                                                      + "{ \r\n"
                                                      + "    strWhere += \" AND " + sw.TableName + "." + sw.TableField + " LIKE '%\" + " + sw.TableField + ".Text.Trim() + \"%' \"; \r\n"
                                                      + "} \r\n";
                    }
                    else if (sw.FuzzyInquires.ToUpper() == "N" && sw.TimeSelect == "N" && sw.MoneySelect == "N")
                    {
                        this.TemplateReportClassWhere += "if (" + sw.TableField + ".Text.Trim() != \"\") \r\n"
                                                      + "{ \r\n"
                                                      + "    strWhere += \" AND " + sw.TableName + "." + sw.TableField + " = '\" + " + sw.TableField + ".Text.Trim() + \"' \"; \r\n"
                                                      + "} \r\n";
                    }
                    else if (sw.TimeSelect == "Y" || sw.MoneySelect == "Y")
                    {
                        this.TemplateReportClassWhere += "if (" + sw.TableField + "1.Text.Trim() != \"\") \r\n"
                                                      + "{ \r\n"
                                                      + "    strWhere += \" AND " + sw.TableName + "." + sw.TableField + " >= '\" + " + sw.TableField + "1.Text.Trim() + \"' \"; \r\n"
                                                      + "} \r\n";
                        this.TemplateReportClassWhere += "if (" + sw.TableField + "2.Text.Trim() != \"\") \r\n"
                                                      + "{ \r\n"
                                                      + "    strWhere += \" AND " + sw.TableName + "." + sw.TableField + " <= '\" + " + sw.TableField + "2.Text.Trim() + \"' \"; \r\n"
                                                      + "} \r\n";
                    }
                    this.SelectWhereHead = "<tr>"
                                         + "  <td colspan=\"3\">"
                                         + "    <h3 class=\"titleBG\">"
                                         + "        查询条件设置</h3>"
                                         + "  </td>"
                                         + "</tr>";

                }
                this.SelectWhere += "</tr>";
                this.SearchButtonString = "<asp:Button ID=\"SearchButton\" runat=\"server\" Text=\"查询\" CssClass=\"btnBg\" onclick=\"SearchButton_Click\"/>";
                this.TemplateDesignerString += "protected global::System.Web.UI.WebControls.Button SearchButton; \r\n";
                this.SearchClassFunction = "public void SearchButton_Click(object sender, EventArgs e) \r\n"
                                         + "{ \r\n"
                                         + "     BingData(); \r\n"
                                         + "} \r\n";
            }
            else
            {
                this.SelectWhere = "";
                this.SearchButtonString = "";
                this.TemplateDesignerString = "";
                this.SearchClassFunction = "";
                this.TemplateReportClassWhere = "";
            }
        }

        /// <summary>
        /// 报表后台的方法
        /// </summary>
        public void GetClassFunction()
        {
            string str = "int PageIndex = AspNetPager1.CurrentPageIndex - 1; \r\n"
                       + "int PageSize = AspNetPager1.PageSize; \r\n";
            string sss = "";
            if (ViewType.Trim() == "1")
            {
                sss = "";
            }
            this.BingDateFunction = " " + (this.whetherPaging == "Y" ? str : "") + ""
                                  + "string strWhere = \"\"; \r\n"
                                  + "" + this.TemplateReportClassWhere + " \r\n"
                                  + "string sql=\"" + this.ReportSQL.ToString() + "\"; \r\n"
                                  + "DataSet ds = ExecuteNonQuery(sql); \r\n"
                                  + "ReportList.DataSource = ds; \r\n"
                                  + "ReportList.DataBind(); \r\n"
                                  + "" + (this.whetherPaging == "Y" ? "AspNetPager1.RecordCount = PageCountQuery(strWhere);" : "") + " \r\n";
            
            this.DataBaseFunction = "public DataSet ExecuteNonQuery(string sql) \r\n"
                                  + "{ \r\n"
                                  + "     using(OleDbConnection oleConn=new OleDbConnection(\"" + this.connectionString + "\")) \r\n"
                                  + "     { \r\n"
                                  + "         using (OleDbCommand cmd = new OleDbCommand(sql, oleConn)) \r\n"
                                  + "         { \r\n"
                                  + "             cmd.CommandTimeout = 999999999;"
                                  + "             using (OleDbDataAdapter oda = new OleDbDataAdapter(cmd)) \r\n"
                                  + "             { \r\n"
                                  + "                 DataSet ds = new DataSet(); \r\n"
                                  + "                 oda.Fill(ds); \r\n"
                                  + "                 return ds; \r\n"
                                  + "             } \r\n"
                                  + "         } \r\n"
                                  + "     } \r\n"
                                  + "} \r\n";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("string strWhere = \"\";");
            sb.AppendLine("" + this.TemplateReportClassWhere + "");
            sb.AppendLine("DataTable tb = ExecuteNonQuery(\"" + this.ExportToExcelSQL + "\").Tables[0];");
            sb.AppendLine("StringBuilder sb = new StringBuilder();");
            sb.AppendLine("for (int i = 0; i < tb.Columns.Count; i++)");
            sb.AppendLine("{");
            sb.AppendLine("sb.Append(@\"\"\"\" + \"'\"+tb.Columns[i].ToString().Trim() + @\"\"\"\" + \",\"); ");
            sb.AppendLine("}");
            sb.AppendLine("sb.Append(\"\\n\");");
            sb.AppendLine("foreach (DataRow row in tb.Rows)");
            sb.AppendLine("{");
            sb.AppendLine("for (int j = 0; j < tb.Columns.Count; j++)");
            sb.AppendLine("{");
            sb.AppendLine("bool flag = false;decimal number;");
            sb.AppendLine("try");
            sb.AppendLine("{");
            sb.AppendLine("number=Convert.ToDecimal(row[tb.Columns[j].ToString().Trim()].ToString().Replace(@\"\"\"\", @\"\"\"\"\"\"));");
            sb.AppendLine("if(number.ToString().Length>10){flag=true;}");
            sb.AppendLine("} catch{}");
            sb.AppendLine("if(flag)");
            sb.AppendLine("sb.Append(@\"\"\"\" + \"'\"+row[tb.Columns[j].ToString().Trim()].ToString().Replace(@\"\"\"\", @\"\"\"\"\"\") + @\"\"\"\" + \",\");");
            sb.AppendLine("else");
            sb.AppendLine("sb.Append(@\"\"\"\" + row[tb.Columns[j].ToString().Trim()].ToString().Replace(@\"\"\"\", @\"\"\"\"\"\") + @\"\"\"\" + \",\");");
            sb.AppendLine("}");
            sb.AppendLine("sb.Append(\"\\n\");");
            sb.AppendLine("}");
            sb.AppendLine("sb.Remove(sb.Length - 2, 1);");
            sb.AppendLine("Response.Clear();");
            sb.AppendLine("Response.ContentEncoding = System.Text.Encoding.Default;");
            sb.AppendLine("Response.ContentType = \"test/csv\";");
            sb.AppendLine("Response.AppendHeader(\"content-disposition\", \"attachment; filename=\" + string.Format(\"{0:yyyyMMddhhmmss}\", DateTime.Now) + \".csv\");");
            sb.AppendLine("Response.Write(sb);");
            sb.AppendLine("Response.End();");
            this.ExportToExcel = sb.ToString();
        }

    }
}