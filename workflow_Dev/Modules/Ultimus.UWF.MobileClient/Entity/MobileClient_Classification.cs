using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using EntityLibrary;
using DataBaseLibrary;
using MyLib;

namespace DALLibrary
{
    //MobileClient_Classification
    public partial class MobileClient_Classification
    {

        //public bool Exists(int ID)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select count(1) from MobileClient_Classification");
        //    strSql.Append(" where ");
        //    strSql.Append(" ID = @ID  ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ID", SqlDbType.Int,4)
        //    };
        //    parameters[0].Value = ID;

        //    return DbHelperSQL.Exists(strSql.ToString(), parameters);
        //}



        /// <summary>
        /// 增加一条数据
        /// </summary>
        //public int Add(EntityLibrary.MobileClient_Classification model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("insert into MobileClient_Classification(");
        //    strSql.Append("CategoryCName,CategoryEName,ProcessName,IsAction,ID");
        //    strSql.Append(") values (");
        //    strSql.Append("@CategoryCName,@CategoryEName,@ProcessName,@IsAction,@ID");
        //    strSql.Append(") ");
        //    //strSql.Append(";");
        //    SqlParameter[] parameters = {
        //                new SqlParameter("@CategoryCName", SqlDbType.NVarChar,156) ,            
        //                new SqlParameter("@CategoryEName", SqlDbType.NVarChar,156) ,            
        //                new SqlParameter("@ProcessName", SqlDbType.NVarChar,156) ,            
        //                new SqlParameter("@IsAction", SqlDbType.NChar,10)
        //            ,new SqlParameter("@ID", SqlDbType.Int,8)
        //    };

        //    parameters[0].Value = model.CategoryCName;
        //    parameters[1].Value = model.CategoryEName;
        //    parameters[2].Value = model.ProcessName;
        //    parameters[3].Value = model.IsAction;
        //    parameters[4].Value = ConvertUtil.ToInt32(DateTime.Now.ToOADate() * 10000);

        //    object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
        //    return ConvertUtil.ToInt32(parameters[4].Value);
        //    //if (obj == null)
        //    //{
        //    //    return 1;
        //    //}
        //    //else
        //    //{
        //    //    return Convert.ToInt32(obj);
        //    //}

        //}


        /// <summary>
        /// 更新一条数据
        /// </summary>
        //public bool Update(EntityLibrary.MobileClient_Classification model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("update MobileClient_Classification set ");

        //    strSql.Append(" CategoryCName = @CategoryCName , ");
        //    strSql.Append(" CategoryEName = @CategoryEName , ");
        //    strSql.Append(" ProcessName = @ProcessName , ");
        //    strSql.Append(" IsAction = @IsAction  ");
        //    strSql.Append(" where ID=@ID ");

        //    SqlParameter[] parameters = {
        //                new SqlParameter("@ID", SqlDbType.Int,4) ,            
        //                new SqlParameter("@CategoryCName", SqlDbType.NVarChar,156) ,            
        //                new SqlParameter("@CategoryEName", SqlDbType.NVarChar,156) ,            
        //                new SqlParameter("@ProcessName", SqlDbType.NVarChar,156) ,            
        //                new SqlParameter("@IsAction", SqlDbType.NChar,10)             
              
        //    };

        //    parameters[0].Value = model.ID;
        //    parameters[1].Value = model.CategoryCName;
        //    parameters[2].Value = model.CategoryEName;
        //    parameters[3].Value = model.ProcessName;
        //    parameters[4].Value = model.IsAction;
        //    int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        //    if (rows > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}


        /// <summary>
        /// 删除一条数据
        /// </summary>
        //public bool Delete(int ID)
        //{

        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("delete from MobileClient_Classification ");
        //    strSql.Append(" where ID=@ID");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ID", SqlDbType.Int,4)
        //    };
        //    parameters[0].Value = ID;


        //    int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        //    if (rows > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// 批量删除一批数据
        ///// </summary>
        //public bool DeleteList(string IDlist)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("delete from MobileClient_Classification ");
        //    strSql.Append(" where ID in (" + IDlist + ")  ");
        //    int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
        //    if (rows > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}


        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public EntityLibrary.MobileClient_Classification GetModel(int ID)
        //{

        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select ID, CategoryCName, CategoryEName, ProcessName, IsAction  ");
        //    strSql.Append("  from MobileClient_Classification ");
        //    strSql.Append(" where ID=@ID");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ID", SqlDbType.Int,4)
        //    };
        //    parameters[0].Value = ID;

        //    EntityLibrary.MobileClient_Classification model = new EntityLibrary.MobileClient_Classification();
        //    IDataReader reader = DbHelperSQL.ExecuteReader(strSql.ToString(),parameters);
        //    while (reader.Read())
        //    {
        //        model.ID = int.Parse(reader["ID"].ToString());
        //        model.CategoryCName = reader["CategoryCName"].ToString();
        //        model.CategoryEName = reader["CategoryEName"].ToString();
        //        model.ProcessName = reader["ProcessName"].ToString();
        //        model.IsAction = reader["IsAction"].ToString();
        //    }
        //    reader.Close();
        //    return model;
        //}


        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        //public List<EntityLibrary.MobileClient_Classification> GetList(string strWhere)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select * FROM MobileClient_Classification");
        //    if (strWhere.Trim() != "")
        //    {
        //        strSql.Append(" where " + strWhere);
        //    }
        //    IDataReader reader = DbHelperSQL.ExecuteReader(strSql.ToString());
        //    List<EntityLibrary.MobileClient_Classification> list = new List<EntityLibrary.MobileClient_Classification>();
        //    while (reader.Read())
        //    {
        //        EntityLibrary.MobileClient_Classification model = new EntityLibrary.MobileClient_Classification();
        //        model.ID = int.Parse(reader["ID"].ToString());
        //        model.CategoryCName = reader["CategoryCName"].ToString();
        //        model.CategoryEName = reader["CategoryEName"].ToString();
        //        model.ProcessName = reader["ProcessName"].ToString();
        //        model.IsAction = reader["IsAction"].ToString();
        //        list.Add(model);
        //    }
        //    reader.Close();
        //    return list;
        //}

        public int GetCategoryNameCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT count(1) FROM V_CATEGORY");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            return Convert.ToInt32(obj);
        }

        /// <summary>
        /// 根据索引获得每一行的信息
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public List<EntityLibrary.MobileClient_Classification> GetMobileClient_ClassificationInfoListByIndex(int index)
        {
            string sqlStr = "select top 3 * from V_CATEGORY where   ID not in (select top " + index * 3 + " ID from V_CATEGORY where IsAction='1' order by ID) order by ID";
            IDataReader reader = DbHelperSQL.ExecuteReader(sqlStr);
            List<EntityLibrary.MobileClient_Classification> list = new List<EntityLibrary.MobileClient_Classification>();
            while (reader.Read())
            {
                EntityLibrary.MobileClient_Classification model = new EntityLibrary.MobileClient_Classification();
                if (reader["ID"].ToString() != "")
                {
                    model.ID = int.Parse(reader["ID"].ToString());
                }
                model.CategoryCName = reader["CategoryName"].ToString();
                model.CategoryEName = reader["CategoryENName"].ToString();
                model.ProcessName = reader["ProcessName"].ToString();
                object obj = DataAccess.Instance("UltDB").ExecuteScalar(string.Format("SELECT  STEPLABEL " +
                      "FROM PROCESSSTEPS " +
                      "where PROCESSNAME='{0}' and STEPTYPE=2" +
                      "order by PROCESSVERSION desc ", model.ProcessName));
                if (obj != null && obj != DBNull.Value)
                {
                    model.Beginstepname = obj.ToString().Trim();
                }
                //model.IsAction = reader["IsAction"].ToString();
                list.Add(model);
            }
            reader.Close();
            return list;
        }

        public List<EntityLibrary.MobileClient_Classification> GetCategoryName(string strWhere)
        {
            DbHelperSQL.Query("select distinct CategoryName,CategoryENName from V_CATEGORY");
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct CategoryName,CategoryENName from V_CATEGORY");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            IDataReader reader = DbHelperSQL.ExecuteReader(strSql.ToString());
            List<EntityLibrary.MobileClient_Classification> list = new List<EntityLibrary.MobileClient_Classification>();
            while (reader.Read())
            {
                EntityLibrary.MobileClient_Classification model = new EntityLibrary.MobileClient_Classification();
                model.CategoryCName = reader["CategoryName"].ToString();
                model.CategoryEName = reader["CategoryENName"].ToString();
                list.Add(model);
            }
            reader.Close();
            return list;
        }

    }
}

