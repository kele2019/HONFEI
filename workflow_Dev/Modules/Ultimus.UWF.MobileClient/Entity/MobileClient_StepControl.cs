using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using EntityLibrary;
using DataBaseLibrary;
using MyLib;

namespace DALLibrary
{
	/// <summary>
	/// 数据访问类:MobileClient_StepControl
	/// </summary>
	public partial class MobileClient_StepControl
	{
		public MobileClient_StepControl()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "MobileClient_StepControl"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from MobileClient_StepControl");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(EntityLibrary.MobileClient_StepControl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into MobileClient_StepControl(");
            strSql.Append("FK_ID,ColumnName,ControlID,Format,IsWillFill,ReadOnly,ExternalLinks,IsShow,OrderBy,SourceType,SourceConnectionString,SourceTableName,SourceColumnName,SourceWhere,SourceVariableName,IsMasterTable,IsSublist,DestType,DestConnectionString,DestTableName,DestColumnName,DestVariableName)");
			strSql.Append(" values (");
            strSql.Append("@FK_ID,@ColumnName,@ControlID,@Format,@IsWillFill,@ReadOnly,@ExternalLinks,@IsShow,@OrderBy,@SourceType,@SourceConnectionString,@SourceTableName,@SourceColumnName,@SourceWhere,@SourceVariableName,@IsMasterTable,@IsSublist,@DestType,@DestConnectionString,@DestTableName,@DestColumnName,@DestVariableName)");
            strSql.Append(";select @@identity");
			SqlParameter[] parameters = {
					new SqlParameter("@FK_ID", SqlDbType.Int,4),
					new SqlParameter("@ColumnName", SqlDbType.NVarChar,156),
					new SqlParameter("@ControlID", SqlDbType.Int,4),
					new SqlParameter("@Format", SqlDbType.NVarChar,1000),
					new SqlParameter("@IsWillFill", SqlDbType.NVarChar,50),
					new SqlParameter("@ReadOnly", SqlDbType.NVarChar,50),
					new SqlParameter("@ExternalLinks", SqlDbType.NVarChar,1000),
					new SqlParameter("@IsShow", SqlDbType.NVarChar,50),
					new SqlParameter("@OrderBy", SqlDbType.NVarChar,256),
					new SqlParameter("@SourceType", SqlDbType.NVarChar,50),
					new SqlParameter("@SourceConnectionString", SqlDbType.NVarChar,256),
					new SqlParameter("@SourceTableName", SqlDbType.NVarChar,256),
					new SqlParameter("@SourceColumnName", SqlDbType.NVarChar,256),
					new SqlParameter("@SourceWhere", SqlDbType.NVarChar,1500),
					new SqlParameter("@SourceVariableName", SqlDbType.NVarChar,50),
					new SqlParameter("@IsMasterTable", SqlDbType.NVarChar,50),
					new SqlParameter("@IsSublist", SqlDbType.NVarChar,50),
					new SqlParameter("@DestType", SqlDbType.NVarChar,50),
					new SqlParameter("@DestConnectionString", SqlDbType.NVarChar,256),
					new SqlParameter("@DestTableName", SqlDbType.NVarChar,256),
					new SqlParameter("@DestColumnName", SqlDbType.NVarChar,50),
					new SqlParameter("@DestVariableName", SqlDbType.NVarChar,50)
                                        };
			parameters[0].Value = model.FK_ID;
			parameters[1].Value = model.ColumnName;
			parameters[2].Value = model.ControlID;
			parameters[3].Value = model.Format;
			parameters[4].Value = model.IsWillFill;
			parameters[5].Value = model.ReadOnly;
			parameters[6].Value = model.ExternalLinks;
			parameters[7].Value = model.IsShow;
			parameters[8].Value = model.OrderBy;
			parameters[9].Value = model.SourceType;
			parameters[10].Value = model.SourceConnectionString;
			parameters[11].Value = model.SourceTableName;
			parameters[12].Value = model.SourceColumnName;
            parameters[13].Value = model.SourceWhere;
            parameters[14].Value = model.SourceVariableName;
            parameters[15].Value = model.IsMasterTable;
            parameters[16].Value = model.IsSublist;
			parameters[17].Value = model.DestType;
			parameters[18].Value = model.DestConnectionString;
			parameters[19].Value = model.DestTableName;
			parameters[20].Value = model.DestColumnName;
			parameters[21].Value = model.DestVariableName;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);


            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(EntityLibrary.MobileClient_StepControl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update MobileClient_StepControl set ");
			strSql.Append("FK_ID=@FK_ID,");
			strSql.Append("ColumnName=@ColumnName,");
			strSql.Append("ControlID=@ControlID,");
			strSql.Append("Format=@Format,");
			strSql.Append("IsWillFill=@IsWillFill,");
			strSql.Append("ReadOnly=@ReadOnly,");
			strSql.Append("ExternalLinks=@ExternalLinks,");
			strSql.Append("IsShow=@IsShow,");
			strSql.Append("OrderBy=@OrderBy,");
			strSql.Append("SourceType=@SourceType,");
			strSql.Append("SourceConnectionString=@SourceConnectionString,");
			strSql.Append("SourceTableName=@SourceTableName,");
            strSql.Append("SourceColumnName=@SourceColumnName,");
            strSql.Append("SourceWhere=@SourceWhere,");
            strSql.Append("SourceVariableName=@SourceVariableName,");
            strSql.Append("IsMasterTable=@IsMasterTable,");
            strSql.Append("IsSublist=@IsSublist,");
			strSql.Append("DestType=@DestType,");
			strSql.Append("DestConnectionString=@DestConnectionString,");
			strSql.Append("DestTableName=@DestTableName,");
			strSql.Append("DestColumnName=@DestColumnName,");
			strSql.Append("DestVariableName=@DestVariableName");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@FK_ID", SqlDbType.Int,4),
					new SqlParameter("@ColumnName", SqlDbType.NVarChar,156),
					new SqlParameter("@ControlID", SqlDbType.Int,4),
					new SqlParameter("@Format", SqlDbType.NVarChar,1000),
					new SqlParameter("@IsWillFill", SqlDbType.NVarChar,50),
					new SqlParameter("@ReadOnly", SqlDbType.NVarChar,50),
					new SqlParameter("@ExternalLinks", SqlDbType.NVarChar,1000),
					new SqlParameter("@IsShow", SqlDbType.NVarChar,50),
					new SqlParameter("@OrderBy", SqlDbType.NVarChar,256),
					new SqlParameter("@SourceType", SqlDbType.NVarChar,50),
					new SqlParameter("@SourceConnectionString", SqlDbType.NVarChar,256),
					new SqlParameter("@SourceTableName", SqlDbType.NVarChar,256),
					new SqlParameter("@SourceColumnName", SqlDbType.NVarChar,256),
					new SqlParameter("@SourceWhere", SqlDbType.NVarChar,1500),
					new SqlParameter("@SourceVariableName", SqlDbType.NVarChar,50),
					new SqlParameter("@IsMasterTable", SqlDbType.NVarChar,50),
					new SqlParameter("@IsSublist", SqlDbType.NVarChar,50),
					new SqlParameter("@DestType", SqlDbType.NVarChar,50),
					new SqlParameter("@DestConnectionString", SqlDbType.NVarChar,256),
					new SqlParameter("@DestTableName", SqlDbType.NVarChar,256),
					new SqlParameter("@DestColumnName", SqlDbType.NVarChar,50),
					new SqlParameter("@DestVariableName", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.FK_ID;
			parameters[1].Value = model.ColumnName;
			parameters[2].Value = model.ControlID;
			parameters[3].Value = model.Format;
			parameters[4].Value = model.IsWillFill;
			parameters[5].Value = model.ReadOnly;
			parameters[6].Value = model.ExternalLinks;
			parameters[7].Value = model.IsShow;
			parameters[8].Value = model.OrderBy;
			parameters[9].Value = model.SourceType;
			parameters[10].Value = model.SourceConnectionString;
			parameters[11].Value = model.SourceTableName;
            parameters[12].Value = model.SourceColumnName;
            parameters[13].Value = model.SourceWhere;
            parameters[14].Value = model.SourceVariableName;
            parameters[15].Value = model.IsMasterTable;
            parameters[16].Value = model.IsSublist;
			parameters[17].Value = model.DestType;
			parameters[18].Value = model.DestConnectionString;
			parameters[19].Value = model.DestTableName;
			parameters[20].Value = model.DestColumnName;
			parameters[21].Value = model.DestVariableName;
			parameters[22].Value = model.ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from MobileClient_StepControl ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string FK_ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from MobileClient_StepControl ");
            strSql.Append(" where FK_ID ='" + FK_ID + "'");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public EntityLibrary.MobileClient_StepControl GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select   ID,FK_ID,ColumnName,ControlID,Format,IsWillFill,ReadOnly,ExternalLinks,IsShow,OrderBy,SourceType,SourceConnectionString,SourceTableName,SourceColumnName,SourceWhere,SourceVariableName,IsMasterTable,IsSublist,DestType,DestConnectionString,DestTableName,DestColumnName,DestVariableName from MobileClient_StepControl ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

            EntityLibrary.MobileClient_StepControl model = new EntityLibrary.MobileClient_StepControl();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FK_ID"]!=null && ds.Tables[0].Rows[0]["FK_ID"].ToString()!="")
				{
					model.FK_ID=int.Parse(ds.Tables[0].Rows[0]["FK_ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ColumnName"]!=null && ds.Tables[0].Rows[0]["ColumnName"].ToString()!="")
				{
					model.ColumnName=ds.Tables[0].Rows[0]["ColumnName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ControlID"]!=null && ds.Tables[0].Rows[0]["ControlID"].ToString()!="")
				{
					model.ControlID=int.Parse(ds.Tables[0].Rows[0]["ControlID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Format"]!=null && ds.Tables[0].Rows[0]["Format"].ToString()!="")
				{
					model.Format=ds.Tables[0].Rows[0]["Format"].ToString();
				}
				if(ds.Tables[0].Rows[0]["IsWillFill"]!=null && ds.Tables[0].Rows[0]["IsWillFill"].ToString()!="")
				{
					model.IsWillFill=ds.Tables[0].Rows[0]["IsWillFill"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ReadOnly"]!=null && ds.Tables[0].Rows[0]["ReadOnly"].ToString()!="")
				{
					model.ReadOnly=ds.Tables[0].Rows[0]["ReadOnly"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ExternalLinks"]!=null && ds.Tables[0].Rows[0]["ExternalLinks"].ToString()!="")
				{
					model.ExternalLinks=ds.Tables[0].Rows[0]["ExternalLinks"].ToString();
				}
				if(ds.Tables[0].Rows[0]["IsShow"]!=null && ds.Tables[0].Rows[0]["IsShow"].ToString()!="")
				{
					model.IsShow=ds.Tables[0].Rows[0]["IsShow"].ToString();
				}
				if(ds.Tables[0].Rows[0]["OrderBy"]!=null && ds.Tables[0].Rows[0]["OrderBy"].ToString()!="")
				{
					model.OrderBy=Convert.ToDecimal(ds.Tables[0].Rows[0]["OrderBy"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SourceType"]!=null && ds.Tables[0].Rows[0]["SourceType"].ToString()!="")
				{
					model.SourceType=ds.Tables[0].Rows[0]["SourceType"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SourceConnectionString"]!=null && ds.Tables[0].Rows[0]["SourceConnectionString"].ToString()!="")
				{
					model.SourceConnectionString=ds.Tables[0].Rows[0]["SourceConnectionString"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SourceTableName"]!=null && ds.Tables[0].Rows[0]["SourceTableName"].ToString()!="")
				{
					model.SourceTableName=ds.Tables[0].Rows[0]["SourceTableName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SourceColumnName"]!=null && ds.Tables[0].Rows[0]["SourceColumnName"].ToString()!="")
				{
					model.SourceColumnName=ds.Tables[0].Rows[0]["SourceColumnName"].ToString();
				}
                if (ds.Tables[0].Rows[0]["SourceWhere"] != null && ds.Tables[0].Rows[0]["SourceWhere"].ToString() != "")
                {
                    model.SourceWhere = ds.Tables[0].Rows[0]["SourceWhere"].ToString();
                }
				if(ds.Tables[0].Rows[0]["SourceVariableName"]!=null && ds.Tables[0].Rows[0]["SourceVariableName"].ToString()!="")
				{
					model.SourceVariableName=ds.Tables[0].Rows[0]["SourceVariableName"].ToString();
				}
                if (ds.Tables[0].Rows[0]["IsMasterTable"] != null && ds.Tables[0].Rows[0]["IsMasterTable"].ToString() != "")
                {
                    model.IsMasterTable = ds.Tables[0].Rows[0]["IsMasterTable"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IsSublist"] != null && ds.Tables[0].Rows[0]["IsSublist"].ToString() != "")
                {
                    model.IsSublist = ds.Tables[0].Rows[0]["IsSublist"].ToString();
                }
				if(ds.Tables[0].Rows[0]["DestType"]!=null && ds.Tables[0].Rows[0]["DestType"].ToString()!="")
				{
					model.DestType=ds.Tables[0].Rows[0]["DestType"].ToString();
				}
				if(ds.Tables[0].Rows[0]["DestConnectionString"]!=null && ds.Tables[0].Rows[0]["DestConnectionString"].ToString()!="")
				{
					model.DestConnectionString=ds.Tables[0].Rows[0]["DestConnectionString"].ToString();
				}
				if(ds.Tables[0].Rows[0]["DestTableName"]!=null && ds.Tables[0].Rows[0]["DestTableName"].ToString()!="")
				{
					model.DestTableName=ds.Tables[0].Rows[0]["DestTableName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["DestColumnName"]!=null && ds.Tables[0].Rows[0]["DestColumnName"].ToString()!="")
				{
					model.DestColumnName=ds.Tables[0].Rows[0]["DestColumnName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["DestVariableName"]!=null && ds.Tables[0].Rows[0]["DestVariableName"].ToString()!="")
				{
					model.DestVariableName=ds.Tables[0].Rows[0]["DestVariableName"].ToString();
				}
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select ID,FK_ID,ColumnName,ControlID,Format,IsWillFill,ReadOnly,ExternalLinks,IsShow,OrderBy,SourceType,SourceConnectionString,SourceTableName,SourceColumnName,SourceWhere,SourceVariableName,IsMasterTable,IsSublist,DestType,DestConnectionString,DestTableName,DestColumnName,DestVariableName ");
			strSql.Append(" FROM MobileClient_StepControl ");
			if(strWhere.Trim()!="")
			{
                strSql.Append(" where " + strWhere + " ");
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        public List<EntityLibrary.MobileClient_StepControl> GetModel(string strWhere)
        {
            DataTable dt = GetList(strWhere).Tables[0];
            List<EntityLibrary.MobileClient_StepControl> modelList = new List<EntityLibrary.MobileClient_StepControl>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                EntityLibrary.MobileClient_StepControl model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new EntityLibrary.MobileClient_StepControl();
                    if (dt.Rows[n]["ID"] != null && dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["FK_ID"] != null && dt.Rows[n]["FK_ID"].ToString() != "")
                    {
                        model.FK_ID = int.Parse(dt.Rows[n]["FK_ID"].ToString());
                    }
                    if (dt.Rows[n]["ColumnName"] != null && dt.Rows[n]["ColumnName"].ToString() != "")
                    {
                        model.ColumnName = dt.Rows[n]["ColumnName"].ToString();
                    }
                    if (dt.Rows[n]["ControlID"] != null && dt.Rows[n]["ControlID"].ToString() != "")
                    {
                        model.ControlID = int.Parse(dt.Rows[n]["ControlID"].ToString());
                    }
                    if (dt.Rows[n]["Format"] != null && dt.Rows[n]["Format"].ToString() != "")
                    {
                        model.Format = dt.Rows[n]["Format"].ToString();
                    }
                    if (dt.Rows[n]["IsWillFill"] != null && dt.Rows[n]["IsWillFill"].ToString() != "")
                    {
                        model.IsWillFill = dt.Rows[n]["IsWillFill"].ToString();
                    }
                    if (dt.Rows[n]["ReadOnly"] != null && dt.Rows[n]["ReadOnly"].ToString() != "")
                    {
                        model.ReadOnly = dt.Rows[n]["ReadOnly"].ToString();
                    }
                    if (dt.Rows[n]["ExternalLinks"] != null && dt.Rows[n]["ExternalLinks"].ToString() != "")
                    {
                        model.ExternalLinks = dt.Rows[n]["ExternalLinks"].ToString();
                    }
                    if (dt.Rows[n]["IsShow"] != null && dt.Rows[n]["IsShow"].ToString() != "")
                    {
                        model.IsShow = dt.Rows[n]["IsShow"].ToString();
                    }
                    if (dt.Rows[n]["OrderBy"] != null && dt.Rows[n]["OrderBy"].ToString() != "")
                    {
                        model.OrderBy = Convert.ToDecimal(dt.Rows[n]["OrderBy"].ToString());
                    }
                    if (dt.Rows[n]["SourceType"] != null && dt.Rows[n]["SourceType"].ToString() != "")
                    {
                        model.SourceType = dt.Rows[n]["SourceType"].ToString();
                    }
                    if (dt.Rows[n]["SourceConnectionString"] != null && dt.Rows[n]["SourceConnectionString"].ToString() != "")
                    {
                        model.SourceConnectionString = dt.Rows[n]["SourceConnectionString"].ToString();
                    }
                    if (dt.Rows[n]["SourceTableName"] != null && dt.Rows[n]["SourceTableName"].ToString() != "")
                    {
                        model.SourceTableName = dt.Rows[n]["SourceTableName"].ToString();
                    }
                    if (dt.Rows[n]["SourceColumnName"] != null && dt.Rows[n]["SourceColumnName"].ToString() != "")
                    {
                        model.SourceColumnName = dt.Rows[n]["SourceColumnName"].ToString();
                    }
                    if (dt.Rows[n]["SourceWhere"] != null && dt.Rows[n]["SourceWhere"].ToString() != "")
                    {
                        model.SourceWhere = dt.Rows[n]["SourceWhere"].ToString();
                    }
                    if (dt.Rows[n]["SourceVariableName"] != null && dt.Rows[n]["SourceVariableName"].ToString() != "")
                    {
                        model.SourceVariableName = dt.Rows[n]["SourceVariableName"].ToString();
                    }
                    if (dt.Rows[n]["IsMasterTable"] != null && dt.Rows[n]["IsMasterTable"].ToString() != "")
                    {
                        model.IsMasterTable = dt.Rows[n]["IsMasterTable"].ToString();
                    }
                    if (dt.Rows[n]["IsSublist"] != null && dt.Rows[n]["IsSublist"].ToString() != "")
                    {
                        model.IsSublist = dt.Rows[n]["IsSublist"].ToString();
                    }
                    if (dt.Rows[n]["DestType"] != null && dt.Rows[n]["DestType"].ToString() != "")
                    {
                        model.DestType = dt.Rows[n]["DestType"].ToString();
                    }
                    if (dt.Rows[n]["DestConnectionString"] != null && dt.Rows[n]["DestConnectionString"].ToString() != "")
                    {
                        model.DestConnectionString = dt.Rows[n]["DestConnectionString"].ToString();
                    }
                    if (dt.Rows[n]["DestTableName"] != null && dt.Rows[n]["DestTableName"].ToString() != "")
                    {
                        model.DestTableName = dt.Rows[n]["DestTableName"].ToString();
                    }
                    if (dt.Rows[n]["DestColumnName"] != null && dt.Rows[n]["DestColumnName"].ToString() != "")
                    {
                        model.DestColumnName = dt.Rows[n]["DestColumnName"].ToString();
                    }
                    if (dt.Rows[n]["DestVariableName"] != null && dt.Rows[n]["DestVariableName"].ToString() != "")
                    {
                        model.DestVariableName = dt.Rows[n]["DestVariableName"].ToString();
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
            strSql.Append(" ID,FK_ID,ColumnName,ControlID,Format,IsWillFill,ReadOnly,ExternalLinks,IsShow,OrderBy,SourceType,SourceConnectionString,SourceTableName,SourceColumnName,SourceWhere,SourceVariableName,IsMasterTable,IsSublist,DestType,DestConnectionString,DestTableName,DestColumnName,DestVariableName ");
			strSql.Append(" FROM MobileClient_StepControl ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM MobileClient_StepControl ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 1;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from MobileClient_StepControl T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "MobileClient_StepControl";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
	}
}

