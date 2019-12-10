using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using EntityLibrary;
using DataBaseLibrary;
using MyLib;

namespace DALLibrary
{
	/// <summary>
	/// 数据访问类:MobileClient_Process
	/// </summary>
	public partial class MobileClient_Process
	{
		public MobileClient_Process()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "MobileClient_Process"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from MobileClient_Process");
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
		public int Add(EntityLibrary.MobileClient_Process model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into MobileClient_Process(");
			strSql.Append("ProcessName,Logo,CreateTime,UpdateTime,IsCreatePage)");
			strSql.Append(" values (");
			strSql.Append("@ProcessName,@Logo,@CreateTime,@UpdateTime,@IsCreatePage)");
            strSql.Append(";select @@identity");
			SqlParameter[] parameters = {
					new SqlParameter("@ProcessName", SqlDbType.NVarChar,156),
					new SqlParameter("@Logo", SqlDbType.NVarChar,256),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@IsCreatePage", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ProcessName;
			parameters[1].Value = model.Logo;
			parameters[2].Value = model.CreateTime;
			parameters[3].Value = model.UpdateTime;
			parameters[4].Value = model.IsCreatePage;

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
        public bool Update(EntityLibrary.MobileClient_Process model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update MobileClient_Process set ");
			strSql.Append("ProcessName=@ProcessName,");
			strSql.Append("Logo=@Logo,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("IsCreatePage=@IsCreatePage");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ProcessName", SqlDbType.NVarChar,156),
					new SqlParameter("@Logo", SqlDbType.NVarChar,256),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@IsCreatePage", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.ProcessName;
			parameters[1].Value = model.Logo;
			parameters[2].Value = model.CreateTime;
			parameters[3].Value = model.UpdateTime;
			parameters[4].Value = model.IsCreatePage;
			parameters[5].Value = model.ID;

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
			strSql.Append("delete from MobileClient_Process ");
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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from MobileClient_Process ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
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
        public EntityLibrary.MobileClient_Process GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select   ID,ProcessName,Logo,CreateTime,UpdateTime,IsCreatePage from MobileClient_Process ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

            EntityLibrary.MobileClient_Process model = new EntityLibrary.MobileClient_Process();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ProcessName"]!=null && ds.Tables[0].Rows[0]["ProcessName"].ToString()!="")
				{
					model.ProcessName=ds.Tables[0].Rows[0]["ProcessName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Logo"]!=null && ds.Tables[0].Rows[0]["Logo"].ToString()!="")
				{
					model.Logo=ds.Tables[0].Rows[0]["Logo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CreateTime"]!=null && ds.Tables[0].Rows[0]["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UpdateTime"]!=null && ds.Tables[0].Rows[0]["UpdateTime"].ToString()!="")
				{
					model.UpdateTime=DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsCreatePage"]!=null && ds.Tables[0].Rows[0]["IsCreatePage"].ToString()!="")
				{
					model.IsCreatePage=ds.Tables[0].Rows[0]["IsCreatePage"].ToString();
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
			strSql.Append("select ID,ProcessName,Logo,CreateTime,UpdateTime,IsCreatePage ");
			strSql.Append(" FROM MobileClient_Process ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
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
			strSql.Append(" ID,ProcessName,Logo,CreateTime,UpdateTime,IsCreatePage ");
			strSql.Append(" FROM MobileClient_Process ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

        public System.Collections.Generic.List<EntityLibrary.MobileClient_Process> GetModel(string strWhere)
        {
            DataTable dt = GetList(strWhere).Tables[0];
            System.Collections.Generic.List<EntityLibrary.MobileClient_Process> modelList = new System.Collections.Generic.List<EntityLibrary.MobileClient_Process>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                EntityLibrary.MobileClient_Process model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new EntityLibrary.MobileClient_Process();
                    if (dt.Rows[n]["ID"] != null && dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["ProcessName"] != null && dt.Rows[n]["ProcessName"].ToString() != "")
                    {
                        model.ProcessName = dt.Rows[n]["ProcessName"].ToString();
                    }
                    if (dt.Rows[n]["Logo"] != null && dt.Rows[n]["Logo"].ToString() != "")
                    {
                        model.Logo = dt.Rows[n]["Logo"].ToString();
                    }
                    if (dt.Rows[n]["CreateTime"] != null && dt.Rows[n]["CreateTime"].ToString() != "")
                    {
                        model.CreateTime = DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
                    }
                    if (dt.Rows[n]["UpdateTime"] != null && dt.Rows[n]["UpdateTime"].ToString() != "")
                    {
                        model.UpdateTime = DateTime.Parse(dt.Rows[n]["UpdateTime"].ToString());
                    }
                    if (dt.Rows[n]["IsCreatePage"] != null && dt.Rows[n]["IsCreatePage"].ToString() != "")
                    {
                        model.IsCreatePage = dt.Rows[n]["IsCreatePage"].ToString();
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM MobileClient_Process ");
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
			strSql.Append(")AS Row, T.*  from MobileClient_Process T ");
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
			parameters[0].Value = "MobileClient_Process";
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

