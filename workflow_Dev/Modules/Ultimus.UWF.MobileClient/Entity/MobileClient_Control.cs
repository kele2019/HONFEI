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
	/// 数据访问类:MobileClient_Control
	/// </summary>
	public partial class MobileClient_Control
	{
		public MobileClient_Control()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "MobileClient_Control"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from MobileClient_Control");
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
		public int Add(EntityLibrary.MobileClient_Control model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into MobileClient_Control(");
            strSql.Append("ControlCName,ControlEName,ControlName,IsAction)");
			strSql.Append(" values (");
            strSql.Append("@ControlCName,@ControlEName,@ControlName,@IsAction)");
            strSql.Append(";select @@identity");
			SqlParameter[] parameters = {
					new SqlParameter("@ControlCName", SqlDbType.NVarChar,156),
					new SqlParameter("@ControlEName", SqlDbType.NVarChar,156),
					new SqlParameter("@ControlName", SqlDbType.NVarChar,156),
					new SqlParameter("@IsAction", SqlDbType.NChar,10)
                                        };
			parameters[0].Value = model.ControlCName;
			parameters[1].Value = model.ControlEName;
			parameters[2].Value = model.IsAction;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
                return 0;
            else
                return Convert.ToInt32(obj);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EntityLibrary.MobileClient_Control model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update MobileClient_Control set ");
			strSql.Append("ControlCName=@ControlCName,");
            strSql.Append("ControlEName=@ControlEName,");
            strSql.Append("ControlName=@ControlName,");
			strSql.Append("IsAction=@IsAction");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ControlCName", SqlDbType.NVarChar,156),
					new SqlParameter("@ControlEName", SqlDbType.NVarChar,156),
					new SqlParameter("@ControlName", SqlDbType.NVarChar,156),
					new SqlParameter("@IsAction", SqlDbType.NChar,10),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.ControlCName;
			parameters[1].Value = model.ControlEName;
			parameters[2].Value = model.IsAction;
			parameters[3].Value = model.ID;

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
			strSql.Append("delete from MobileClient_Control ");
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
			strSql.Append("delete from MobileClient_Control ");
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
		public EntityLibrary.MobileClient_Control GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select   ID,ControlCName,ControlEName,ControlName,IsAction from MobileClient_Control ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

            EntityLibrary.MobileClient_Control model = new EntityLibrary.MobileClient_Control();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ControlCName"]!=null && ds.Tables[0].Rows[0]["ControlCName"].ToString()!="")
				{
					model.ControlCName=ds.Tables[0].Rows[0]["ControlCName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ControlEName"]!=null && ds.Tables[0].Rows[0]["ControlEName"].ToString()!="")
				{
					model.ControlEName=ds.Tables[0].Rows[0]["ControlEName"].ToString();
				}
                if (ds.Tables[0].Rows[0]["ControlName"] != null && ds.Tables[0].Rows[0]["ControlName"].ToString() != "")
                {
                    model.ControlName = ds.Tables[0].Rows[0]["ControlName"].ToString();
                }
				if(ds.Tables[0].Rows[0]["IsAction"]!=null && ds.Tables[0].Rows[0]["IsAction"].ToString()!="")
				{
					model.IsAction=ds.Tables[0].Rows[0]["IsAction"].ToString();
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
            strSql.Append("select ID,ControlCName,ControlEName,ControlName,IsAction ");
			strSql.Append(" FROM MobileClient_Control ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        public System.Collections.Generic.List<EntityLibrary.MobileClient_Control> GetModel(string strWhere)
        {
            DataTable dt = GetList(strWhere).Tables[0];
            System.Collections.Generic.List<EntityLibrary.MobileClient_Control> modelList = new System.Collections.Generic.List<EntityLibrary.MobileClient_Control>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                EntityLibrary.MobileClient_Control model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new EntityLibrary.MobileClient_Control();
                    if (dt.Rows[n]["ID"] != null && dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["ControlCName"] != null && dt.Rows[n]["ControlCName"].ToString() != "")
                    {
                        model.ControlCName = dt.Rows[n]["ControlCName"].ToString();
                    }
                    if (dt.Rows[n]["ControlEName"] != null && dt.Rows[n]["ControlEName"].ToString() != "")
                    {
                        model.ControlEName = dt.Rows[n]["ControlEName"].ToString();
                    }
                    if (dt.Rows[n]["ControlName"] != null && dt.Rows[n]["ControlName"].ToString() != "")
                    {
                        model.ControlName = dt.Rows[n]["ControlName"].ToString();
                    }
                    if (dt.Rows[n]["IsAction"] != null && dt.Rows[n]["IsAction"].ToString() != "")
                    {
                        model.IsAction = dt.Rows[n]["IsAction"].ToString();
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
			strSql.Append(" ID,ControlCName,ControlEName,ControlName,IsAction ");
			strSql.Append(" FROM MobileClient_Control ");
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
			strSql.Append("select count(1) FROM MobileClient_Control ");
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
			strSql.Append(")AS Row, T.*  from MobileClient_Control T ");
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
			parameters[0].Value = "MobileClient_Control";
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

