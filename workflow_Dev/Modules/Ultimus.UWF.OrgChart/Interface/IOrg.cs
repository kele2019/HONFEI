using System;
using System.Collections.Generic;
using System.Text;
using Ultimus.UWF.OrgChart.Entity;

namespace Ultimus.UWF.OrgChart.Interface
{
    /// <summary>
    /// 组织架构接口
    /// </summary>
    public interface IOrg
    {
        UserEntity GetUserEntity(string loginName);

        List<GroupEntity> GetUserGroups(string loginName);

        List<DepartmentEntity> GetUserDepartments(string loginName);

        List<DepartmentEntity> GetDepartmentList();

        DepartmentEntity GetDepartmentEntity(int departmentID);

        UserEntity GetUserEntityByJob(string jobID);

        void Insert(UserEntity user);

        void Update(UserEntity user);

        void Delete(int userID);

        JobEntity GetJobEntity(string jobID);

        JobEntity GetJobEntityByUserID(string userID);

        void InsertJob(JobEntity user);

        void UpdateJob(JobEntity user);

        void DeleteJob(int userID);

        void InsertDepartment(DepartmentEntity dept);

        void UpdateDepartment(DepartmentEntity dept);

        void DeleteDepartment(int departmentID);

        UserEntity GetUserEntityByID(int userID);

        List<UserEntity> GetUserInfoBySearchText(string searchText);

    }
}
