using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Configuration;
using System.IO;
using Ultimus.UWF.Form.ProcessControl.Entity;
using MyLib;

namespace Ultimus.UWF.Form.ProcessControl.Logic
{
	public class AttachmentLogic
	{
		public string flag = "@";
		public DataTable GetAttachmentsByProcessAndIncident(string ProcessName, int Incident)
		{
			try
			{
				DataAccess db = new DataAccess("BizDB");
				StringBuilder strsql = new StringBuilder();
				strsql.Append("select * from WF_Attachment where ProcessName='" + ProcessName + "' and Incident=" + Incident.ToString());
				DbCommand dbcom = db.CreateCommand(strsql.ToString());
				//db.AddInParameter(dbcom, flag+"ProcessName", DbType.String, ProcessName);
				//db.AddInParameter(dbcom, flag+"Incident", DbType.Int32, Incident);
				return db.ExecuteDataTable(dbcom);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public DataTable GetAttachments(string processName, string incident, string type)
		{
			try
			{
				DataAccess db = new DataAccess("BizDB");
				StringBuilder strsql = new StringBuilder();
				strsql.Append("select * from WF_Attachment where ProcessName='" + processName + "' and Incident=" + incident + " and Type='" + type + "'");
				DbCommand dbcom = db.CreateCommand(strsql.ToString());
				return db.ExecuteDataTable(dbcom);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public DataTable GetAttachmentsByFormID(string strFormid, string type)
		{
			try
			{
				DataAccess db = new DataAccess("BizDB");
				StringBuilder strsql = new StringBuilder();
                string filePath = MyLib.ConfigurationManager.AppSettings["AttachmentPath"].TrimStart('~');
                strsql.Append("select *,('" + filePath + "\'+PROCESSNAME+'\\'+NEWNAME+FileType) filePath from WF_Attachment where FORMID='" + strFormid + "' and TYPE='" + type + "'");
				DbCommand dbcom = db.CreateCommand(strsql.ToString());
				//db.AddInParameter(dbcom, flag+"ProcessName", DbType.String, ProcessName);
				//db.AddInParameter(dbcom, flag+"Incident", DbType.Int32, Incident);
				return db.ExecuteDataTable(dbcom);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public AttachmentEntity GetAttachmentsByID(string ID)
		{
			try
			{
				DataAccess db = new DataAccess("BizDB");
				StringBuilder strsql = new StringBuilder();
				strsql.Append("select * from WF_Attachment where NEWNAME=" + flag + "ID");
				DbCommand dbcom = db.CreateCommand(strsql.ToString());
				db.AddInParameter(dbcom, flag + "ID", DbType.String, ID);
				DbDataReader dr = db.ExecuteReader(dbcom);
				AttachmentEntity item = new AttachmentEntity();
				while (dr.Read())
				{
					//if (dr["ID"] != null && !String.IsNullOrEmpty(dr["ID"].ToString()))
					//{
					//    item.ID = dr["ID"].ToString();
					//}
					if (dr["ProcessName"] != null && !String.IsNullOrEmpty(dr["ProcessName"].ToString()))
					{
						item.ProcessName = dr["ProcessName"].ToString();
					}
					if (dr["Incident"] != null && !String.IsNullOrEmpty(dr["Incident"].ToString()))
					{
						try
						{
							item.Incident = Convert.ToInt32(dr["Incident"].ToString());
						}
						catch
						{
							item.Incident = 0;
						}
					}
					if (dr["STEPNAME"] != null && !String.IsNullOrEmpty(dr["STEPNAME"].ToString()))
					{
						item.UploadStepName = dr["STEPNAME"].ToString();
					}
					if (dr["FileName"] != null && !String.IsNullOrEmpty(dr["FileName"].ToString()))
					{
						item.FileName = dr["FileName"].ToString();
					}
					if (dr["NewName"] != null && !String.IsNullOrEmpty(dr["NewName"].ToString()))
					{
						item.NewName = dr["NewName"].ToString();
					}
					if (dr["FileSize"] != null && !String.IsNullOrEmpty(dr["FileSize"].ToString()))
					{
						try
						{
							item.FileSize = Convert.ToInt32(dr["FileSize"].ToString());
						}
						catch
						{
							item.FileSize = 0;
						}
					}
					if (dr["FileType"] != null && !String.IsNullOrEmpty(dr["FileType"].ToString()))
					{
						item.FileType = dr["FileType"].ToString();
					}
					if (dr["Comments"] != null && !String.IsNullOrEmpty(dr["Comments"].ToString()))
					{
						item.Comments = dr["Comments"].ToString();
					}
					if (dr["CreateBy"] != null && !String.IsNullOrEmpty(dr["CreateBy"].ToString()))
					{
						item.CreateByBadge = dr["CreateBy"].ToString();
					}
					if (dr["CreateBy"] != null && !String.IsNullOrEmpty(dr["CreateBy"].ToString()))
					{
						item.CreateByName = dr["CreateBy"].ToString();
					}
					//if (dr["CreateByDomain"] != null && !String.IsNullOrEmpty(dr["CreateByDomain"].ToString()))
					//{
					//    item.CreateByDomain = dr["CreateByDomain"].ToString();
					//}
					if (dr["CreateDate"] != null && !String.IsNullOrEmpty(dr["CreateDate"].ToString()))
					{
						try
						{
							item.CreateDate = Convert.ToDateTime(dr["CreateDate"].ToString());
						}
						catch
						{ }
					}
					if (dr["Status"] != null && !String.IsNullOrEmpty(dr["Status"].ToString()))
					{
						item.Status = dr["Status"].ToString();
					}
					if (dr["Ext01"] != null && !String.IsNullOrEmpty(dr["Ext01"].ToString()))
					{
						item.Ext01 = dr["Ext01"].ToString();
					}
					if (dr["Ext02"] != null && !String.IsNullOrEmpty(dr["Ext02"].ToString()))
					{
						item.Ext02 = dr["Ext02"].ToString();
					}
					if (dr["Ext03"] != null && !String.IsNullOrEmpty(dr["Ext03"].ToString()))
					{
						item.Ext03 = dr["Ext03"].ToString();
					}
					if (dr["Ext04"] != null && !String.IsNullOrEmpty(dr["Ext04"].ToString()))
					{
						item.Ext04 = dr["Ext04"].ToString();
					}
					if (dr["Ext05"] != null && !String.IsNullOrEmpty(dr["Ext05"].ToString()))
					{
						item.Ext05 = dr["Ext05"].ToString();
					}
				}
				return item;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool DeleteAttachmentsByID(string ID)
		{
			try
			{
				AttachmentEntity item = GetAttachmentsByID(ID);
				string attp = MyLib.ConfigurationManager.AppSettings["AttachmentPath"];
				string path = "";
				if (attp.StartsWith("\\"))
				{
					path = attp + "\\" + item.ProcessName + "/" + item.NewName + item.FileType;
				}
				else
				{
					path = HttpContext.Current.Server.MapPath(MyLib.ConfigurationManager.AppSettings["AttachmentPath"] + "/" + item.ProcessName + "/" + item.NewName + item.FileType);
				}
				File.Delete(path);
				StringBuilder strsql = new StringBuilder();
				if (ID.IndexOf(",") < 0)
					strsql.Append("delete from WF_Attachment where NEWNAME='" + ID + "'");
				else
					strsql.Append("delete from WF_Attachment where NEWNAME in (" + ID + ")");

				if (DataAccess.Instance("BizDB").ExecuteNonQuery(strsql.ToString()) > 0)
					return true;
				else
					return false;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void DownloadByID(System.Web.UI.Page page, string ID)
		{
			try
			{
				//DataAccess db = new DataAccess("BizDB");
				//string encoding = "gb2312";
				//AttachmentEntity item = GetAttachmentsByID(ID);
				//string path = "";//
				//string attp = MyLib.ConfigurationManager.AppSettings["AttachmentPath"];
				//if (attp.StartsWith("\\"))
				//{
				//    path = attp + "\\" +  item.ProcessName + "\\" + item.NewName + item.FileType + "\\";
				//}
				//else
				//{
				//    path = HttpContext.Current.Server.MapPath(attp) + "/"  + item.ProcessName + "/" + item.NewName + item.FileType + "/";
				//}

				//string filePath = path;

				//FileInfo fileInfo = new FileInfo(filePath);
				//string fileName = item.FileName;
				//fileName = HttpUtility.UrlEncode(fileName);
				//page.Response.Clear();
				//page.Response.ClearContent();
				//page.Response.ClearHeaders();
				//System.Web.HttpContext.Current.Response.Charset = encoding;
				//page.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
				//page.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
				//page.Response.AddHeader("Content-Transfer-Encoding", "binary");
				//page.Response.ContentType = "application/octet-stream";
				//System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding(encoding);
				//System.Web.HttpContext.Current.Response.HeaderEncoding = System.Text.Encoding.GetEncoding(encoding);
				////page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
				//page.Response.WriteFile(fileInfo.FullName);
				//page.Response.Flush();
				////page.Response.End();
				string encoding = "gb2312";
				AttachmentEntity item = GetAttachmentsByID(ID);
				// string filePath = HttpContext.Current.Server.MapPath(MyLib.ConfigurationManager.AppSettings["AttachmentPath"] + "/" + item.ProcessName + "/" + item.NewName + item.FileType);
				string filePath = MyLib.ConfigurationManager.AppSettings["AttachmentPath"];
				if (filePath.StartsWith("\\"))
				{
					filePath = filePath + "\\" + item.ProcessName + "\\" + item.NewName + item.FileType;
				}
				else
				{
					filePath = HttpContext.Current.Server.MapPath(filePath) + "/" + item.ProcessName + "/" + item.NewName + item.FileType;
				}

				FileInfo fileInfo = new FileInfo(filePath);
				string fileName = item.FileName;
				fileName = HttpUtility.UrlEncode(fileName);
				page.Response.Clear();
				page.Response.ClearContent();
				page.Response.ClearHeaders();
				System.Web.HttpContext.Current.Response.Charset = encoding;
				page.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
				page.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
				page.Response.AddHeader("Content-Transfer-Encoding", "binary");
				page.Response.ContentType = "application/octet-stream";
				System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding(encoding);
				System.Web.HttpContext.Current.Response.HeaderEncoding = System.Text.Encoding.GetEncoding(encoding);
				//page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
				page.Response.WriteFile(fileInfo.FullName);
				page.Response.Flush();
				//page.Response.End();


			}
			catch (Exception ex)
			{
				LogUtil.Error(ex);
			}
		}

		public bool UploadFile(AttachmentEntity item)
		{
			try
			{
				DataAccess db = new DataAccess("BizDB");
				StringBuilder strsql = new StringBuilder();
				strsql.Append("INSERT INTO WF_Attachment(ProcessName,Incident,STEPNAME,FileName,NewName,FileSize,FileType,Comments,CreateBy,CreateDate,Status,TASKID,FORMID,TYPE,Ext01,Ext02,Ext03,Ext04,Ext05)");
				strsql.Append(" values ");
				strsql.Append("(" + flag + "ProcessName," + flag + "Incident," + flag + "UploadStepName," + flag + "FileName," + flag + "NewName," + flag + "FileSize," + flag + "FileType," + flag + "Comments," + flag + "CreateBy," + flag + "CreateDate," + flag + "Status," + flag + "TASKID," + flag + "FORMID," + flag + "TYPE," + flag + "Ext01," + flag + "Ext02," + flag + "Ext03," + flag + "Ext04," + flag + "Ext05)");
				DbCommand dbcom = db.CreateCommand(strsql.ToString());
				//  db.AddInParameter(dbcom, flag + "ID", DbType.Int32, item.ID);
				db.AddInParameter(dbcom, flag + "ProcessName", DbType.String, item.ProcessName);
				db.AddInParameter(dbcom, flag + "Incident", DbType.Int32, item.Incident);
				db.AddInParameter(dbcom, flag + "UploadStepName", DbType.String, item.UploadStepName);
				db.AddInParameter(dbcom, flag + "FileName", DbType.String, item.FileName);
				db.AddInParameter(dbcom, flag + "NewName", DbType.String, item.NewName);
				db.AddInParameter(dbcom, flag + "FileSize", DbType.Decimal, item.FileSize);
				db.AddInParameter(dbcom, flag + "FileType", DbType.String, item.FileType);
				db.AddInParameter(dbcom, flag + "Comments", DbType.String, item.Comments);
				db.AddInParameter(dbcom, flag + "CreateBy", DbType.String, item.CreateByName);
				//db.AddInParameter(dbcom, flag + "CreateByDomain", DbType.String, item.CreateByDomain);
				db.AddInParameter(dbcom, flag + "CreateDate", DbType.DateTime, item.CreateDate);
				db.AddInParameter(dbcom, flag + "Status", DbType.String, item.Status);
				db.AddInParameter(dbcom, flag + "FORMID", DbType.String, item.FORMID);
				db.AddInParameter(dbcom, flag + "TYPE", DbType.String, item.TYPE);
				db.AddInParameter(dbcom, flag + "TASKID", DbType.String, item.TASKID);
				db.AddInParameter(dbcom, flag + "EXT01", DbType.String, item.Ext01);
				db.AddInParameter(dbcom, flag + "EXT02", DbType.String, item.Ext02);
				db.AddInParameter(dbcom, flag + "EXT03", DbType.String, item.Ext03);
				db.AddInParameter(dbcom, flag + "EXT04", DbType.String, item.Ext04);
				db.AddInParameter(dbcom, flag + "EXT05", DbType.String, item.Ext05);

				foreach (DbParameter param in dbcom.Parameters)
				{
					if (param.Value == null)
					{
						param.Value = DBNull.Value;
					}
				}
				if (db.ExecuteNonQuery(dbcom) > 0)
					return true;
				else
					return false;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}