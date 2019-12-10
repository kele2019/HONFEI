using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MyLib;
using System.Web;
using Ultimus.UWF.OrgChart.Interface;
using Ultimus.UWF.OrgChart.Entity;

namespace Ultimus.UWF.OrgChart.Implementation
{
    public class DatabaseOrgLogic : IOrg
    {
        public virtual UserEntity GetUserEntity(string loginName)
        {
            UserEntity user = new UserEntity();
            try
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Session["LoginUser"] != null)
                    {
                        user = HttpContext.Current.Session["LoginUser"] as UserEntity;
                        if (user.LOGINNAME.Equals(loginName))
                        {
                            return user;
                        }
                    }
                }
            }
            catch { }
            user = DataAccess.Instance("BizDB").GetEntity<UserEntity>("OrgLogic_GetUserEntity", loginName);
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session["LoginUser"] = user;
            }
            return user;
        }

        public virtual List<DepartmentEntity> GetDepartmentList()
        {
            return DataAccess.Instance("BizDB").ExecuteList<DepartmentEntity>("SELECT * FROM ORG_DEPARTMENT");
        }

        public virtual DepartmentEntity GetDepartmentEntity(int departmentID)
        {
            return DataAccess.Instance("BizDB").ExecuteEntity<DepartmentEntity>("SELECT * FROM ORG_DEPARTMENT WHERE DEPARTMENTID=@departmentID", departmentID);
        }

        public virtual List<GroupEntity> GetUserGroups(string loginName)
        {
            List<GroupEntity> groups = new List<GroupEntity>();
            DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable("SELECT * FROM  V_ORG_GROUPMEMBER");
            List<DepartmentEntity> depts = GetUserDepartments(loginName);
            foreach (DataRow dr in dt.Rows)
            {
                string type = ConvertUtil.ToString(dr["MEMBERTYPE"]);
                int GROUPMEMBERID = ConvertUtil.ToInt32(dr["MEMBERID"]);
                int GROUPID = ConvertUtil.ToInt32(dr["GROUPID"]);
                GroupEntity group = new GroupEntity();
                group.GROUPID=GROUPID;
                UserEntity user = GetUserEntity(loginName);
                switch (type)
                {
                    //1:组成员为用户
                    case "1":
                        if (user.USERID == GROUPMEMBERID && !groups.Exists(p=>p.GROUPID==GROUPID))
                        {
                            groups.Add(group);
                        }
                        break;
                    //2:组成员为当前部门
                    case "2":
                        if (user.EXT02 == GROUPMEMBERID.ToString() && !groups.Exists(p => p.GROUPID == GROUPID))
                        {
                            groups.Add(group);
                        }
                        break;
                    //3:组成员为部门及下属所有子部门
                    case "3":
                        foreach (DepartmentEntity dept in depts)
                        {
                            if (dept.DEPARTMENTID == GROUPMEMBERID && !groups.Exists(p => p.GROUPID == GROUPID))
                            {
                                groups.Add(group);
                            }
                        }
                        break;
                    //3:排除该人
                    case "99":
                        if (user.USERID == GROUPMEMBERID && groups.Exists(p => p.GROUPID == GROUPID))
                        {
                            groups.Remove(groups.Find(p => p.GROUPID == GROUPID));
                        }
                        break;
                }
            }
            return groups;
        }

        public virtual List<DepartmentEntity> GetUserDepartments(string loginName)
        {
            object obj = DataAccess.Instance("BizDB").ExecuteScalar("SELECT USERID FROM V_ORG_USER WHERE LOGINNAME=@loginName",loginName);
            List<DepartmentEntity> depts = new List<DepartmentEntity>();
            int userId = ConvertUtil.ToInt32(obj);
            obj = DataAccess.Instance("BizDB").ExecuteScalar("SELECT DEPARTMENTID FROM V_ORG_USERDEPARTMENT WHERE USERID=@USERID", userId);
            int deptId = ConvertUtil.ToInt32(obj);

            if (deptId==0)
            {
                deptId = -1;
            }
            while (true)
            {
                DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable("SELECT * FROM V_ORG_DEPARTMENT WHERE DEPARTMENTID=" + deptId);
                if (dt.Rows.Count > 0)
                {
                    DepartmentEntity resource = new DepartmentEntity();
                    resource.DEPARTMENTID = ConvertUtil.ToInt32(dt.Rows[0]["DEPARTMENTID"]); //部门ID
                    resource.PARENTID = ConvertUtil.ToInt32(dt.Rows[0]["PARENTID"]); //PARENTID
                    resource.DEPARTMENTNAME = ConvertUtil.ToString(dt.Rows[0]["DEPARTMENTNAME"]); //DEPARTMENTNAME
                    depts.Add(resource);

                    deptId = resource.PARENTID;
                }
                else
                {
                    break;
                }

            }
            return depts;
        }

        public virtual UserEntity GetUserEntityByJob(string jobID)
        {
            return DataAccess.Instance("BizDB").GetEntity<UserEntity>("OrgLogic_GetUserEntityByJob", jobID);
        }
        public virtual void Insert(UserEntity user)
        {
            DataAccess.Instance("BizDB").GetEntity<UserEntity>("OrgLogic_InsertUser", user);
        }
        public virtual void Update(UserEntity user)
        {
            DataAccess.Instance("BizDB").GetEntity<UserEntity>("OrgLogic_UpdateUser", user);
        }
        public virtual void Delete(int userID)
        {
            DataAccess.Instance("BizDB").GetEntity<UserEntity>("OrgLogic_DeleteUser", userID);
        }

        public virtual JobEntity GetJobEntity(string jobID)
        {
            return DataAccess.Instance("BizDB").GetEntity<JobEntity>("OrgLogic_GetJobEntity", jobID);
             
        }
        public virtual JobEntity GetJobEntityByUserID(string userID)
        {
            return DataAccess.Instance("BizDB").GetEntity<JobEntity>("OrgLogic_GetJobEntityByUserID", userID);
        }

        public UserEntity GetUserEntityByID(int userID)
        {
            return DataAccess.Instance("BizDB").ExecuteEntity<UserEntity>("SELECT * FROM ORG_USER WHERE USERID=@userID", userID);
        }


        public virtual void InsertJob(JobEntity user)
        {
            DataAccess.Instance("BizDB").GetEntity<JobEntity>("OrgLogic_InsertJob", user);
        }
        public virtual void UpdateJob(JobEntity user)
        {
            DataAccess.Instance("BizDB").GetEntity<JobEntity>("OrgLogic_UpdateJob", user);
        }
        public virtual void DeleteJob(int userID)
        {
            DataAccess.Instance("BizDB").GetEntity<JobEntity>("OrgLogic_DeleteJob", userID);
        }

        public virtual void InsertDepartment(DepartmentEntity dept)
        {
            DataAccess.Instance("BizDB").GetEntity<JobEntity>("OrgLogic_InsertDepartment", dept);
        }
        public virtual void UpdateDepartment(DepartmentEntity dept)
        {
            DataAccess.Instance("BizDB").GetEntity<JobEntity>("OrgLogic_UpdateDepartment", dept);
        }
        public virtual void DeleteDepartment(int departmentID)
        {
            DataAccess.Instance("BizDB").GetEntity<JobEntity>("OrgLogic_DeleteDepartment", departmentID);
        }

        //设置top
        public virtual List<UserEntity> GetUserInfoBySearchText(string searchText)
        {
            string domain = ConfigurationManager.AppSettings["DomainList"].Split(',')[0];
            Ultimus.OC.OrgChart occ = new OC.OrgChart(domain);// Ultimus
            if (domain.ToUpper() == "BUSINESS ORGANIZATION")
            {
                occ = new OC.OrgChart();
            }
            OC.User user = null;
            occ.FindUser(searchText, "", "", out user);
            if (user == null)
            {
                occ.FindUser("", searchText, "", out user);
            }
            //OC.User[] user11 = null;
            //occ.GetAllOCMembers(out user11);

            List<UserEntity> lue = new List<UserEntity>();
            if (user != null)
            {
                //return null;
                UserEntity ue1 = new UserEntity();
                ue1.USERNAME = user.strUserFullName;// "李四";
                ue1.LOGINNAME = user.strUserName;// 
                lue.Add(ue1);
            }
            return lue;

//            string sql = @"SELECT [User].UserName,[User].DisplayName,[Organization].Name
//              FROM [dbo].[User] (nolock) left join [dbo].Organization (nolock) on
//              [Organization].[Id]=[User].[OrganizationId] where ([User].[IsEnabled]=1 and [User].[IsHidden]=0 and [User].[IsDeleted]=0) ";
//            if (searchText.IndexOf("%") != -1)
//            {
//                sql += " and ([User].UserName like '" + searchText + "' or [User].DisplayName like '" + searchText + "' or [Organization].Name like '" + searchText + "')";
//            }
//            else 
//            {
//                sql += " and [User].UserName = '" + searchText + "'";
//            }
//            DataTable dt = DataAccess.Instance("ResDB").ExecuteDataTable(sql);
//            List<UserEntity> lue = new List<UserEntity>();
//            for (int i = 0; i < dt.Rows.Count; i++) 
//            {
//                UserEntity ue1 = new UserEntity();
//                ue1.USERNAME = dt.Rows[i]["DisplayName"].ToString();
//                ue1.LOGINNAME = dt.Rows[i]["UserName"].ToString();
//                ue1.DEPARTMENT = dt.Rows[i]["Name"].ToString();
//                lue.Add(ue1);
//            }
//            return lue;
        }
        

    }
}
