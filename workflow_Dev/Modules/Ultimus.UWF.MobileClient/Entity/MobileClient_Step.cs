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
	/// 数据访问类:MobileClient_Step
	/// </summary>
	public partial class MobileClient_Step
	{
		public MobileClient_Step()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "MobileClient_Step"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from MobileClient_Step");
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
		public int Add(EntityLibrary.MobileClient_Step model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into MobileClient_Step(");
			strSql.Append("FK_ID,StepName,StepCName,StepEName)");
			strSql.Append(" values (");
            strSql.Append("@FK_ID,@StepName,@StepCName,@StepEName)");
            strSql.Append(";select @@identity");
			SqlParameter[] parameters = {
					new SqlParameter("@FK_ID", SqlDbType.Int,4),
					new SqlParameter("@StepName", SqlDbType.NVarChar,156),
					new SqlParameter("@StepCName", SqlDbType.NVarChar,156),
					new SqlParameter("@StepEName", SqlDbType.NVarChar,156)
                                        };
			parameters[0].Value = model.FK_ID;
			parameters[1].Value = model.StepName;
			parameters[2].Value = model.StepCName;
            parameters[3].Value = model.StepEName;
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
        public bool Update(EntityLibrary.MobileClient_Step model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update MobileClient_Step set ");
			strSql.Append("FK_ID=@FK_ID,");
			strSql.Append("StepName=@StepName,");
			strSql.Append("StepCName=@StepCName,");
			strSql.Append("StepEName=@StepEName");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@FK_ID", SqlDbType.Int,4),
					new SqlParameter("@StepName", SqlDbType.NVarChar,156),
					new SqlParameter("@StepCName", SqlDbType.NVarChar,156),
					new SqlParameter("@StepEName", SqlDbType.NVarChar,156),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.FK_ID;
			parameters[1].Value = model.StepName;
			parameters[2].Value = model.StepCName;
			parameters[3].Value = model.StepEName;
			parameters[4].Value = model.ID;

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
			strSql.Append("delete from MobileClient_Step ");
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
			strSql.Append("delete from MobileClient_Step ");
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
        public EntityLibrary.MobileClient_Step GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select   ID,FK_ID,StepName,StepCName,StepEName from MobileClient_Step ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

            EntityLibrary.MobileClient_Step model = new EntityLibrary.MobileClient_Step();
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
				if(ds.Tables[0].Rows[0]["StepName"]!=null && ds.Tables[0].Rows[0]["StepName"].ToString()!="")
				{
					model.StepName=ds.Tables[0].Rows[0]["StepName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["StepCName"]!=null && ds.Tables[0].Rows[0]["StepCName"].ToString()!="")
				{
					model.StepCName=ds.Tables[0].Rows[0]["StepCName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["StepEName"]!=null && ds.Tables[0].Rows[0]["StepEName"].ToString()!="")
				{
					model.StepEName=ds.Tables[0].Rows[0]["StepEName"].ToString();
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
			strSql.Append("select ID,FK_ID,StepName,StepCName,StepEName ");
			strSql.Append(" FROM MobileClient_Step ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        public System.Collections.Generic.List<EntityLibrary.MobileClient_Step> GetModel(string strWhere)
        {
            DataTable dt = GetList(strWhere).Tables[0];
            System.Collections.Generic.List<EntityLibrary.MobileClient_Step> modelList = new System.Collections.Generic.List<EntityLibrary.MobileClient_Step>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                EntityLibrary.MobileClient_Step model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new EntityLibrary.MobileClient_Step();
                    if (dt.Rows[n]["ID"] != null && dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["FK_ID"] != null && dt.Rows[n]["FK_ID"].ToString() != "")
                    {
                        model.FK_ID = int.Parse(dt.Rows[n]["FK_ID"].ToString());
                    }
                    if (dt.Rows[n]["StepName"] != null && dt.Rows[n]["StepName"].ToString() != "")
                    {
                        model.StepName = dt.Rows[n]["StepName"].ToString();
                    }
                    if (dt.Rows[n]["StepCName"] != null && dt.Rows[n]["StepCName"].ToString() != "")
                    {
                        model.StepCName = dt.Rows[n]["StepCName"].ToString();
                    }
                    if (dt.Rows[n]["StepEName"] != null && dt.Rows[n]["StepEName"].ToString() != "")
                    {
                        model.StepEName = dt.Rows[n]["StepEName"].ToString();
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
			strSql.Append(" ID,FK_ID,StepName,StepCName,StepEName ");
			strSql.Append(" FROM MobileClient_Step ");
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
			strSql.Append("select count(1) FROM MobileClient_Step ");
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
			strSql.Append(")AS Row, T.*  from MobileClient_Step T ");
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
			parameters[0].Value = "MobileClient_Step";
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

