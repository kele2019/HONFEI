using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ultimus.UWF.OrgChart.Entity;
using System.Data.Common;
using System.Data.SqlClient;
using MyLib;
using System.Data;

namespace Ultimus.UWF.Home2.Code.DAO
{
    public class OrgMgmt
    {
        public static string RootOrgName = "HonFei";
        public static string RootOrgEngName = "";

        static OrgMgmt _Instance = new OrgMgmt();
        public static OrgMgmt Instance { get { return _Instance; } }

        private static string ConnStr { get { return ConfigurationManager.ConnectionStrings["BizDB"].ConnectionString; } }

        public DepartmentEntityExt Create(DepartmentEntityExt o)
        {
            string sql = @"
Insert Into [ORG_DEPARTMENT](DEPARTMENTNAME,PARENTID,DEPARTMENTTYPE,ORGANIZATION,CREATEDATE,UPDATEDATE,EXT01,EXT02,EXT03,EXT04,EXT05)
Values(@DEPARTMENTNAME, @PARENTID, @DEPARTMENTTYPE, @ORGANIZATION,@CREATEDATE,@UPDATEDATE,@EXT01,@EXT02,@EXT03,@EXT04,@EXT05)

SELECT @@IDENTITY
";
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(new SqlParameter("@DEPARTMENTNAME", o.DEPARTMENTNAME));
            p.Add(new SqlParameter("@PARENTID", o.PARENTID));
            p.Add(new SqlParameter("@DEPARTMENTTYPE", o.DEPARTMENTTYPE));
            p.Add(new SqlParameter("@ORGANIZATION", o.ORGANIZATION));
            p.Add(new SqlParameter("@CREATEDATE", DateTime.Now));
            p.Add(new SqlParameter("@UPDATEDATE", DateTime.Now));
            p.Add(new SqlParameter("@EXT01", o.EXT01));
            p.Add(new SqlParameter("@EXT02", o.EXT02));
            p.Add(new SqlParameter("@EXT03", o.EXT03));
            p.Add(new SqlParameter("@EXT04", o.EXT04));
            p.Add(new SqlParameter("@EXT05", o.EXT05));

            object objID = SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, sql, p.ToArray());
            o = Get(int.Parse(objID + ""));
            return o;
        }

        public void Update(DepartmentEntityExt o)
        {
            string sql = @"
Update [ORG_DEPARTMENT] 
Set DEPARTMENTNAME=@DEPARTMENTNAME,PARENTID=@PARENTID,DEPARTMENTTYPE=@DEPARTMENTTYPE,
ORGANIZATION=@ORGANIZATION,CREATEDATE=@CREATEDATE,UPDATEDATE=@UPDATEDATE,EXT01=@EXT01,
EXT02=@EXT02,EXT03=@EXT03,EXT04=@EXT04,EXT05=@EXT05
Where DEPARTMENTID=@DEPARTMENTID
";
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(new SqlParameter("@DEPARTMENTID", o.DEPARTMENTID));
            p.Add(new SqlParameter("@DEPARTMENTNAME", o.DEPARTMENTNAME));
            p.Add(new SqlParameter("@PARENTID", o.PARENTID));
            p.Add(new SqlParameter("@DEPARTMENTTYPE", o.DEPARTMENTTYPE));
            p.Add(new SqlParameter("@ORGANIZATION", o.ORGANIZATION));
            p.Add(new SqlParameter("@CREATEDATE", o.CREATEDATE));
            p.Add(new SqlParameter("@UPDATEDATE", o.UPDATEDATE));
            p.Add(new SqlParameter("@EXT01", o.EXT01));
            p.Add(new SqlParameter("@EXT02", o.EXT02));
            p.Add(new SqlParameter("@EXT03", o.EXT03));
            p.Add(new SqlParameter("@EXT04", o.EXT04));
            p.Add(new SqlParameter("@EXT05", o.EXT05));
            SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, p.ToArray());
        }

        public void Delete(int id)
        {
            string sql = "Delete From [ORG_DEPARTMENT] Where DEPARTMENTID=@DEPARTMENTID";
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(new SqlParameter("@DEPARTMENTID", id));
            SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, p.ToArray());
        }

        public DepartmentEntityExt Get(int id)
        {
            string sql = "Select * From [ORG_DEPARTMENT] Where DEPARTMENTID=@DEPARTMENTID";
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(new SqlParameter("@DEPARTMENTID", id));
            DataSet ds = SqlHelper.ExecuteDataset(ConnStr, CommandType.Text, sql, p.ToArray());
            List<DepartmentEntityExt> list = new List<DepartmentEntityExt>();
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return Transfer(ds.Tables[0].Rows[0]);
        }

        public List<DepartmentEntityExt> Query(string name, string cbCenter, int? parentID, int pageIndex, int pageSize, out int totalPageCount)
        {
            totalPageCount = 0;
            string sql = "Select * From [ORG_DEPARTMENT] Where 1=1";
            List<SqlParameter> p = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(name))
            {
                sql += " And DEPARTMENTNAME Like @DEPARTMENTNAME";
                p.Add(new SqlParameter("@DEPARTMENTNAME", "%" + name + "%"));
            }
            if (!string.IsNullOrWhiteSpace(cbCenter))
            {
                sql += " And EXT02 Like @EXT02";
                p.Add(new SqlParameter("@EXT02", "%" + cbCenter + "%"));
            }
            if (parentID != null && parentID.HasValue)
            {
                var tmpOrg = Get(parentID.Value);
                if (tmpOrg != null && tmpOrg.PARENTID != 0)
                {
                    sql += " And PARENTID=@PARENTID";
                    p.Add(new SqlParameter("@PARENTID", parentID));
                }
            }

            string pageSql = string.Format(@"
SELECT TOP {1} * 
FROM 
    (
        SELECT ROW_NUMBER() OVER (ORDER BY CONVERT(int, [EXT01]), [DEPARTMENTNAME]) AS RowNumber,* FROM 
        (
            {0}
        ) B
    ) A
WHERE RowNumber > {2}
", sql, pageSize, (pageIndex - 1 ) * pageSize);

            DataSet ds = SqlHelper.ExecuteDataset(ConnStr, CommandType.Text, pageSql, p.ToArray());
            List<DepartmentEntityExt> list = new List<DepartmentEntityExt>();
            foreach (DataRow row in ds.Tables[0].Rows)
	        {
                DepartmentEntityExt u = Transfer(row);
		        list.Add(u);
	        }

            string countSql = string.Format("Select Count(*) From ({0}) ttt1", sql);
            totalPageCount = (int)SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, countSql, p.ToArray());

            return list;
        }

        private DepartmentEntityExt Transfer(DataRow row)
        {
            DepartmentEntityExt u = new DepartmentEntityExt();
            u.DEPARTMENTID = (int)row["DEPARTMENTID"];
            u.PARENTID = (int)row["PARENTID"];
            u.CREATEDATE = row["CREATEDATE"] as DateTime?;
            u.UPDATEDATE = row["UPDATEDATE"] as DateTime?;
            u.DEPARTMENTNAME = row["DEPARTMENTNAME"] as string;
            u.DEPARTMENTTYPE = row["DEPARTMENTTYPE"] as string;
            u.ORGANIZATION = row["ORGANIZATION"] as string;
            u.EXT01 = row["EXT01"] as string;//排序
            u.EXT02 = row["EXT02"] as string;//成本中心
            u.EXT03 = row["EXT03"] as string;//EngName
            u.EXT04 = row["EXT04"] as string;//部门级别
            u.EXT05 = row["EXT05"] as string;//Path
            return u;
        }


        public bool ContainsName(string name, string engName, int parentID, int withoutID)
        {
            string sql = "select count(*) from ORG_DEPARTMENT where PARENTID=" + parentID;
            List<SqlParameter> p = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(name))
            {
                sql += " and DEPARTMENTNAME=@Name";
                p.Add(new SqlParameter("@Name", name));
            }
            else if (!string.IsNullOrWhiteSpace(engName))
            {
                sql += " and EXT03=@EngName";
                p.Add(new SqlParameter("@EngName", engName));
            }
            if (withoutID != 0)
            {
                sql += " and DEPARTMENTID<>" + withoutID;
            }

            int count = (int)SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, sql, p.ToArray());
            return count > 0;
        }
    }
}