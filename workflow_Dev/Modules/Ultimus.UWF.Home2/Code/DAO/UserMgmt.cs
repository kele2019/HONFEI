using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ultimus.UWF.OrgChart.Entity;
using System.Data.Common;
using System.Data.SqlClient;
using MyLib;
using System.Data;
using Ultimus.UWF.Home2.Code.Entity;

namespace Ultimus.UWF.Home2.Code.DAO
{
    public class UserMgmt
    {
        static UserMgmt _Instance = new UserMgmt();
        public static UserMgmt Instance { get { return _Instance; } }

        private static string ConnStr { get { return ConfigurationManager.ConnectionStrings["BizDB"].ConnectionString; } }

        public UserEntityExt Create(UserEntityExt u)
        {
            string sql = @"
INSERT INTO ORG_USER (ORDERNO,LOGINNAME,USERNAME,USERCODE,EMAIL,MOBILENO,ISACTIVE,EXT01,EXT02,EXT03,EXT04,EXT05,EXT06,EXT07,EXT08,EXT11,EXT12,EXT15,CREATEDATE,UPDATEDATE,EntryDate,StartWorkDate)
VALUES(@ORDERNO,@LOGINNAME,@USERNAME,@USERCODE,@EMAIL,@MOBILENO,@ISACTIVE,@EXT01,@EXT02,@EXT03,@EXT04,@EXT05,@EXT06,@EXT07,@EXT08,@EXT11,@EXT12,@EXT15,@CREATEDATE,@UPDATEDATE,@EntryDate,@StartWorkDate)

declare @UID int
set @UID= @@IDENTITY
select @UID

INSERT INTO ORG_JOB (USERID,DEPARTMENTID,SUPERVISORJOBID)
VALUES(@UID,@DEPARTMENTID,@SUPERVISORJOBID)

SELECT @UID
";
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(new SqlParameter("@ORDERNO", u.ORDERNO));
            p.Add(new SqlParameter("@LOGINNAME", u.LOGINNAME));
            p.Add(new SqlParameter("@USERNAME", u.USERNAME));
            p.Add(new SqlParameter("@USERCODE", u.USERCODE));
            p.Add(new SqlParameter("@EMAIL", u.EMAIL));
            p.Add(new SqlParameter("@MOBILENO", u.MOBILENO));
            p.Add(new SqlParameter("@ISACTIVE", u.ISACTIVE));
            p.Add(new SqlParameter("@EXT01", u.EXT01));
            p.Add(new SqlParameter("@EXT02", u.EXT02));
            p.Add(new SqlParameter("@EXT03", u.EXT03));
            p.Add(new SqlParameter("@EXT04", u.EXT04));
            p.Add(new SqlParameter("@EXT05", u.EXT05));
            p.Add(new SqlParameter("@EXT06", u.EXT06));
            p.Add(new SqlParameter("@EXT07", u.EXT07));
            p.Add(new SqlParameter("@EXT08", u.EXT08));

            p.Add(new SqlParameter("@EXT11", u.EXT11));
            p.Add(new SqlParameter("@EXT12", u.EXT12));
            p.Add(new SqlParameter("@EXT15", u.EXT15));

            p.Add(new SqlParameter("@CREATEDATE", DateTime.Now));
            p.Add(new SqlParameter("@UPDATEDATE", DateTime.Now));
            p.Add(new SqlParameter("@EntryDate",u.EXT09));
            p.Add(new SqlParameter("@StartWorkDate", u.EXT10));
           
            p.Add(new SqlParameter("@DEPARTMENTID", u.OrgID));
            p.Add(new SqlParameter("@SUPERVISORJOBID",u.JobID));


            object objID = SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, sql, p.ToArray());
            u = Get(int.Parse(objID + ""));
            return u;
        }

        public void Update(UserEntityExt u)
        {
            string sql = @"
UPDATE ORG_USER SET ORDERNO=@ORDERNO,LOGINNAME=@LOGINNAME,USERNAME=@USERNAME,USERCODE=@USERCODE
,EMAIL=@EMAIL,MOBILENO=@MOBILENO,ISACTIVE=@ISACTIVE,EXT01=@EXT01,EXT02=@EXT02
,EXT03=@EXT03,EXT04=@EXT04,EXT05=@EXT05,EXT06=@EXT06,EXT07=@EXT07,EXT08=@EXT08,EXT11=@EXT11,EXT12=@EXT12,EXT15=@EXT15
,UPDATEDATE=@UPDATEDATE,StartWorkDate=@StartWorkDate,EntryDate=@EntryDate
WHERE USERID=@USERID

UPDATE ORG_JOB SET DEPARTMENTID=@DEPARTMENTID,SUPERVISORJOBID=@SUPERVISORJOBID
WHERE USERID=@USERID 
";
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(new SqlParameter("@USERID", u.USERID));
            p.Add(new SqlParameter("@ORDERNO", u.ORDERNO));
            p.Add(new SqlParameter("@USERCODE", u.USERCODE));
            p.Add(new SqlParameter("@LOGINNAME", u.LOGINNAME));
            p.Add(new SqlParameter("@USERNAME", u.USERNAME));
            //p.Add(new SqlParameter("@USERCODE", u.USERCODE));
            p.Add(new SqlParameter("@EMAIL", u.EMAIL));
            p.Add(new SqlParameter("@MOBILENO", u.MOBILENO));
            p.Add(new SqlParameter("@ISACTIVE", u.ISACTIVE));
            p.Add(new SqlParameter("@EXT01", u.EXT01));
            p.Add(new SqlParameter("@EXT02", u.EXT02));
            p.Add(new SqlParameter("@EXT03", u.EXT03));
            p.Add(new SqlParameter("@EXT04", u.EXT04));
            p.Add(new SqlParameter("@EXT05", u.EXT05));
            p.Add(new SqlParameter("@EXT06", u.EXT06));
            p.Add(new SqlParameter("@EXT07", u.EXT07));
            p.Add(new SqlParameter("@EXT08", u.EXT08));
            p.Add(new SqlParameter("@EXT11", u.EXT11));
            p.Add(new SqlParameter("@EXT12", u.EXT12));
            p.Add(new SqlParameter("@EXT15", u.EXT15));
            p.Add(new SqlParameter("@CREATEDATE", DateTime.Now));
            p.Add(new SqlParameter("@UPDATEDATE", DateTime.Now));
            p.Add(new SqlParameter("@EntryDate", u.EXT09));
            p.Add(new SqlParameter("@StartWorkDate", u.EXT10));

            p.Add(new SqlParameter("@DEPARTMENTID", u.OrgID));
            p.Add(new SqlParameter("@SUPERVISORJOBID", u.JobID));

            
            SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, p.ToArray());
        }

        public void Delete(int id)
        {
            string sql = @"DELETE FROM ORG_USER WHERE USERID=@USERID";
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(new SqlParameter("@USERID", id));
            SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, p.ToArray());
        }

        public UserEntityExt Get(int id)
        {
            string sql = @"
select t1.*,t3.DEPARTMENTID,t2.jobID,t3.DEPARTMENTNAME from ORG_USER t1
inner join ORG_JOB t2
	on t1.USERID=t2.USERID
inner join ORG_DEPARTMENT t3
	on t2.DEPARTMENTID=t3.DEPARTMENTID
where t1.USERID=@USERID
";
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(new SqlParameter("@USERID", id));
            DataSet ds = SqlHelper.ExecuteDataset(ConnStr, CommandType.Text, sql, p.ToArray());
            List<DepartmentEntity> list = new List<DepartmentEntity>();
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return Transfer(ds.Tables[0].Rows[0]);
        }

        public UserEntityExt Get(string loginName)
        {
            string sql = @"
select t1.*,t3.DEPARTMENTID,t2.jobID ,t3.DEPARTMENTNAME from ORG_USER t1
inner join ORG_JOB t2
	on t1.USERID=t2.USERID
inner join ORG_DEPARTMENT t3
	on t2.DEPARTMENTID=t3.DEPARTMENTID
where t1.LOGINNAME=@LOGINNAME
";
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(new SqlParameter("@LOGINNAME", loginName));
            DataSet ds = SqlHelper.ExecuteDataset(ConnStr, CommandType.Text, sql, p.ToArray());
            List<DepartmentEntity> list = new List<DepartmentEntity>();
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return Transfer(ds.Tables[0].Rows[0]);
        }

        public List<UserEntityExt> Query(string name, int? orgID, bool isSearch, string account, int pageIndex, int pageSize, out int totalPageCount)
        {
            totalPageCount = 0;
            string sql = @"
select t1.*,t3.DEPARTMENTID,t2.jobID,t3.DEPARTMENTNAME  from ORG_USER t1
inner join ORG_JOB t2
	on t1.USERID=t2.USERID
inner join ORG_DEPARTMENT t3
	on t2.DEPARTMENTID=t3.DEPARTMENTID
where 1=1
";
            List<SqlParameter> p = new List<SqlParameter>();
            if (orgID != null && orgID.HasValue)
            {
                if (orgID == -10)//所有无效用户
               sql += " and t1.ISACTIVE<>'1'";
                else
                    sql += string.Format(@" and (t3.DEPARTMENTID={0})", orgID);
//                sql += string.Format(@" and (t3.DEPARTMENTID={0} or t3.DEPARTMENTID in (
//    select DEPARTMENTID from ORG_DEPARTMENT
//        where EXT05 like (select top 1 EXT05+CONVERT(varchar,DEPARTMENTID)+'.%' from ORG_DEPARTMENT where DEPARTMENTID={0})
//))", orgID);
               
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                sql += "and t1.USERNAME like @USERNAME";
                p.Add(new SqlParameter("@USERNAME", "%" + name + "%"));
            }
            if (!string.IsNullOrWhiteSpace(account))
            {
                sql += "and t1.LOGINNAME like @LOGINNAME";
                p.Add(new SqlParameter("@LOGINNAME", "%" + account + "%"));
            }

            string pageSql = string.Format(@"
SELECT TOP {1} * 
FROM 
    (
        SELECT ROW_NUMBER() OVER (ORDER BY [ORDERNO]) AS RowNumber,* FROM 
        (
            {0}
        ) B
    ) A
WHERE RowNumber > {2}
", sql, pageSize, (pageIndex - 1) * pageSize);

            DataSet ds = SqlHelper.ExecuteDataset(ConnStr, CommandType.Text, pageSql, p.ToArray());
            DataTable dt = ds.Tables[0];
            List<UserEntityExt> list = new List<UserEntityExt>();
            foreach (DataRow row in dt.Rows)
	        {
                UserEntityExt u = Transfer(row);
		        list.Add(u);
            }

            string countSql = string.Format("Select Count(*) From ({0}) ttt1", sql);
            totalPageCount = (int)SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, countSql, p.ToArray());

            return list;
        }

        private UserEntityExt Transfer(DataRow row)
        {
            UserEntityExt u = new UserEntityExt();
            u.USERID = (int)row["USERID"];
            u.USERCODE = row["USERCODE"] as string;//员工编号
            u.ORDERNO = row["ORDERNO"] as int?;
            u.LOGINNAME = row["LOGINNAME"] as string;
            u.USERNAME = row["USERNAME"] as string;
            u.USERCODE = row["USERCODE"] as string;
            u.EMAIL = row["EMAIL"] as string;
            u.MOBILENO = row["MOBILENO"] as string;
            u.ISACTIVE = row["ISACTIVE"] as string;
            u.CREATEDATE = row["CREATEDATE"] as DateTime?;
            u.UPDATEDATE = row["UPDATEDATE"] as DateTime?;
            u.EXT01 = row["EXT01"] as string;//岗位等级
            u.EXT02 = row["EXT02"] as string;//账号
            u.EXT03 = row["EXT03"] as string;//岗位
            u.EXT04 = row["EXT04"] as string;//英文名
            u.EXT05 = row["EXT05"] as string;//成本中心
            u.EXT06 = row["EXT06"] as string;//?
            u.EXT07 = row["EXT07"] as string;//岗位英文名
            u.EXT08 = row["EXT08"] as string;//岗位英文名
            u.EXT09 = row["EXT09"] as string;//岗位英文名

            u.EXT11 = row["EXT11"] as string;
            u.EXT12 = row["EXT12"] as string;
            u.EXT15 = row["EXT15"] as string;
            u.StartWorkDate = row["StartWorkDate"] as string;
            u.EntryDate = row["EntryDate"] as string;
            u.DEPARTMENT = row["DEPARTMENTNAME"] as string;
            int orgID = 0;
            int.TryParse(row["DEPARTMENTID"] + string.Empty, out orgID);
            int JobID=0;
            int.TryParse(row["jobID"] + string.Empty, out JobID);
            u.JobID = JobID;
            u.OrgID = orgID;//部门
            return u;
        }

        public bool ContainsName(string loginName, int withoutID)
        {
            string sql = "select count(*) from ORG_USER where LOGINNAME=@LOGINNAME";
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(new SqlParameter("@LOGINNAME", loginName));
            if (withoutID != 0)
            {
                sql += " and USERID<>" + withoutID;
            }

            int count = (int)SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, sql, p.ToArray());
            return count > 0;
        }

        public bool ContainsUserCode(string userCode, int withoutID)
        {
            string sql = "select count(*) from ORG_USER where USERCODE=@USERCODE";
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(new SqlParameter("@USERCODE", userCode));
            if (withoutID != 0)
            {
                sql += " and USERID<>" + withoutID;
            }

            int count = (int)SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, sql, p.ToArray());
            return count > 0;
        }

        public int GetMaxUserCode()
        {
            string sql = "select MAX(USERCODE) from ORG_USER where ISNUMERIC(USERCODE)=1";
            object o = SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, sql, null);
            return o == DBNull.Value || o == null ? 0 : Convert.ToInt32(o);
        }
    }
}