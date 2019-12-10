using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyLib;
using System.Xml;
using System.Data;
using Ultimus.UWF.Form.Entity;
using System.IO;
using System.Web;
using System.Collections.ObjectModel;
using System.CodeDom.Compiler;
using Ultimus.UWF.Common.Logic;

namespace Ultimus.UWF.Form.Logic
{
    public class FormLogic
    {
        public void CreateForm(string processName)
        {
            //是否生成过表单
            string sql = "select HASFORM from WF_PROCESS where PROCESSNAME='"+processName+"'";
            string hasform = ConvertUtil.ToString(DataAccess.Instance("BizDB").ExecuteScalar(sql));
            if (hasform == "1")
            {
                return;
            }

            //1.取数据结构
            string xmlSchema = GetXmlSchema(processName);

            FormEntity form = new FormEntity();
            form = GetForm(processName, xmlSchema);

            //2.建表
            CreateTable(processName,form);

            //3.建页面
            string templatePath = "/Modules/Ultimus.UWF.Form/Templates/Process/";
            string formPath = "/Modules/" + ConfigurationManager.AppSettings["ProjectName"] + ".Process." + processName + "/";
            form.ProjectName = ConfigurationManager.AppSettings["ProjectName"];
            form.ProjectFullName = ConfigurationManager.AppSettings["ProjectName"] + ".Process." + processName;
            if (HttpContext.Current == null)
            {
                templatePath = "C:\\Ultimus\\UWF\\Modules\\Ultimus.UWF.Form\\Templates\\Process";
                formPath = "C:\\Ultimus\\UWF\\Modules\\Ultimus.UWF.Form\\Process\\" + processName;
            }
            else
            {
                templatePath = HttpContext.Current.Server.MapPath(templatePath);
                formPath = HttpContext.Current.Server.MapPath(formPath);
            }
            if (!Directory.Exists(formPath))
            {
                Directory.CreateDirectory(formPath);
            }
            string[] sz=Directory.GetFiles(templatePath);
            
            foreach (string str in sz)
            {
                FileInfo fi=new FileInfo(str);
                if (fi.Name.StartsWith("Process.csproj"))
                {
                    TemplateEngine.CreatePage<FormEntity>(str, formPath + "\\" + form.ProjectFullName+".csproj", form);
                }
                else
                {
                    TemplateEngine.CreatePage<FormEntity>(str, formPath + "\\" + fi.Name.Replace(".cshtml", ""), form);
                }
            }

            //更新已经生成表单
            SerialNoLogic sn=new SerialNoLogic();
            int maxid=sn.GetMaxNo("wf_process","id");
            sql = "if exists(select 1 from wf_process where processname='" + processName + "') begin update wf_process set hasform='1' where processname='" + processName + "' end else begin insert into wf_process (id,processname,displayname,hasform) values("+maxid+",'"+processName+"','"+processName+"','1') end;";
            DataAccess.Instance("BizDB").ExecuteNonQuery(sql);
            string pPath = "Modules/" + ConfigurationManager.AppSettings["ProjectName"] + ".Process." + processName + "";
            //更新步骤的.net表单
            DataTable dt = GetProcessSteps(processName);
            for (int i = 0; i < dt.Rows.Count;)
            {
                string pcform = pPath + "/NewRequest.aspx";
                if (i > 0)
                {
                    pcform = "Modules/Ultimus.UWF.Form/Process/" + processName + "/Approval.aspx";
                }

                sql = "if not exists(select 1 from WF_PROCESSSTEP where processname='" + ConvertUtil.ToString(dt.Rows[i]["Processname"]) + "' and stepname='" + ConvertUtil.ToString(dt.Rows[i]["StepLabel"]) + "') begin insert into WF_PROCESSSTEP (id,processname,stepname,pcform,mobileform) values({0},'{1}','{2}','{3}','{4}') end";
                maxid = sn.GetMaxNo("WF_PROCESSSTEP", "ID");
                sql = string.Format(sql, maxid, ConvertUtil.ToString(dt.Rows[i]["Processname"]), ConvertUtil.ToString(dt.Rows[i]["StepLabel"]), pcform, "");
                DataAccess.Instance("BizDB").ExecuteNonQuery(sql);
                break;
            }

            DataAccess.Instance("BizDB").ExecuteNonQuery("UPDATE WF_PROCESS SET DEFAULTPCFORM='" + pPath + "/Approval.aspx" + "' where processname='" + processName + "'");

        }

        public int GetProcessMaxVersion(string processName)
        {
            return ConvertUtil.ToInt32(DataAccess.Instance("UltDB").ExecuteScalar("SELECT max(PROCESSVERSION) FROM PROCESSES WHERE PROCESSNAME='" + processName + "'"));
        }

        public DataTable GetProcessSteps(string processName)
        {
            int maxversion = GetProcessMaxVersion(processName);
            return DataAccess.Instance("UltDB").ExecuteDataTable("SELECT * FROM PROCESSSTEPS WHERE PROCESSNAME='" + processName + "' and PROCESSVERSION=" + maxversion + " ORDER By STEPTYPE");
        }

        public string GetXmlSchema(string processName)
        {
            string sql = "select PROCESSVERSION from INITIATE where processname=N'" + processName + "'";
            string PROCESSVERSION = ConvertUtil.ToString(DataAccess.Instance("UltDB").ExecuteScalar(sql)).Trim();
            string str = ConfigurationManager.AppSettings["UltimusPath"] + "\\Schemas and Assemblies\\" + processName + "\\" + PROCESSVERSION + "\\" + processName + "Types.xsd";
            return str;
        }

        public FormEntity GetForm(string processName,string xmlSchema)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(xmlSchema);

            string text = xmldoc.OuterXml.Replace("xs:", "");
            xmldoc = new XmlDocument();
            xmldoc.LoadXml(text);
            //Fields
            XmlNodeList list = xmldoc.SelectNodes("//element");
            List<FieldEntity> etys = new List<FieldEntity>();
            List<string> tables = new List<string>();
            foreach (XmlNode node in list)
            {
                FieldEntity ety = new FieldEntity();
                ety.Description = node.InnerText;
                ety.MinOccurs = ConvertUtil.ToInt32(node.Attributes["minOccurs"].Value);
                ety.MaxOccurs = ConvertUtil.ToInt32(node.Attributes["maxOccurs"].Value);
                ety.Name = node.Attributes["name"].Value;
                ety.Type = node.Attributes["type"].Value;
                if (node.Attributes["nillable"] == null)
                {
                    ety.Nillable = false;
                }
                else
                {
                    ety.Nillable = ConvertUtil.ToString(node.Attributes["nillable"].Value) == "true" ? true : false;
                }
                ety.ComplexType = node.ParentNode.ParentNode.Attributes["name"].Value;
                if (!ety.Type.StartsWith("s0"))
                {
                    etys.Add(ety);
                }
                if (!tables.Contains(ety.ComplexType) && ety.ComplexType.Trim().ToUpper()!="GLOBALTYPE")
                {
                    tables.Add(ety.ComplexType);
                }
            }
            //Tables
            list = xmldoc.SelectNodes("//complexType");
            List<TableEntity> tbls = new List<TableEntity>();
            foreach (XmlNode node in list)
            {
                TableEntity tbl = new TableEntity();
                tbl.Name = node.Attributes["name"].Value;
                if (node.SelectSingleNode("//documentation") != null)
                {
                    tbl.Description = node.SelectSingleNode("//documentation").InnerText;
                }
                if (tbl.Name.Trim().ToUpper() != "GLOBALTYPE")
                {
                    tbls.Add(tbl);
                }
            }

            FormEntity form = new FormEntity();
            form.ProcessName = processName;
            form.TableName = "PROC_" + processName.ToUpper();
            foreach (string table in tables)
            {
                form.TableNameDetail += "PROC_" + processName.ToUpper()+"_" + table.ToUpper() + ",";
            }
            form.TableNameDetail = form.TableNameDetail.TrimEnd(',');
            form.Fields = etys;
            form.Tables = tbls;
            return form;
        }

        public void CreateTable(string processName,FormEntity form)
        {
            string[] sz = form.TableNameDetail.Split(',');
            List<string> tables = new List<string>();
            tables.Add(form.TableName);
            if (!string.IsNullOrEmpty(form.TableNameDetail))
            {
                tables.AddRange(sz);
            }
            int i = 0;
            foreach (string table in tables)
            {
                string sql = "";
                if (i == 0) //主表
                {
                    sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + table + "]') AND type in (N'U')) BEGIN CREATE TABLE " + table + "(	FORMID char(36) NULL,	PROCESSNAME nvarchar(100) NOT NULL,	INCIDENT int NOT NULL,	DOCUMENTNO nvarchar(50) NULL,	APPLICANT nvarchar(50) NULL,	REQUESTDATE datetime NULL,	DEPARTMENT nvarchar(100) NULL,	COMPANY nvarchar(100) NULL,	PROCESSSUMMARY nvarchar(100) NULL,	STATUS nvarchar(50) NULL,";
                }
                else //明细表
                {
                    sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + table + "]') AND type in (N'U')) BEGIN CREATE TABLE " + table + "(	FORMID char(36) NULL,	PROCESSNAME nvarchar(100) NOT NULL,	INCIDENT int NOT NULL,";
                }
                List<string> strs = GetFixFields();
                List<FieldEntity> etys = new List<FieldEntity>();
                if (i == 0) //主表
                {
                    etys = form.Fields.FindAll(p => p.ComplexType.ToUpper().Trim() == "GLOBALTYPE");
                }
                else //明细表
                {
                    etys = form.Fields.FindAll(p =>"PROC_"+processName.ToUpper()+"_"+p.ComplexType.ToUpper().Trim() == table.ToUpper().Trim());
                }
                foreach (FieldEntity ety in etys)
                {
                    if (!strs.Contains(ety.Name.Trim().ToUpper()))
                    {
                        sql += "["+ety.Name+"]";

                        switch (ety.Type.ToUpper().Trim())
                        {
                            case "INT":
                                sql += " int ";
                                break;
                            case "DOUBLE":
                                sql += " decimal(18,2) ";
                                break;
                            case "DATETIME":
                                sql += " datetime ";
                                break;
                            default:
                                sql += " nvarchar(100) ";
                                break;
                        }
                        if (!ety.Nillable)
                        {
                            sql += " NOT NULL";
                        }
                        sql += " ,";
                    }
                }
                //sql += " CONSTRAINT PK_" + table + " PRIMARY KEY CLUSTERED 	(		PROCESSNAME ASC,		INCIDENT ASC	)) END";
                sql =sql.TrimEnd(',')+ " ) END";
                DataAccess.Instance("BizDB").ExecuteNonQuery(sql);

                i++;
            }
        }

        public List<string> GetFixFields()
        {
            List<string> strs = new List<string>();
            strs.Add("FORMID");
            strs.Add("PROCESSNAME");
            strs.Add("INCIDENT");
            strs.Add("DOCUMENTNO");
            strs.Add("APPLICANT");
            strs.Add("REQUESTDATE");
            strs.Add("DEPARTMENT");
            strs.Add("COMPANY");
            strs.Add("PROCESSSUMMARY");
            strs.Add("STATUS");

            return strs;
        }

        
    }
}
