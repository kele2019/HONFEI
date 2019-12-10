using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using MyLib;
using System.Web;
using Ultimus.UWF.Security.Entity;
using Ultimus.UWF.Common.Logic;
using Ultimus.UWF.OrgChart.Entity;
using Ultimus.UWF.OrgChart.Interface;

namespace Ultimus.UWF.Security.Logic
{
    /// <summary>
    /// 权限管理
    /// </summary>
    public class SecurityLogic
    {
        public List<MenuRightsEntity> GetList()
        {
            return DataAccess.Instance("BizDB").ExecuteList<MenuRightsEntity>("SELECT * FROM SEC_MENURIGHTS");
        }

        public List<MenuRightsObjectEntity> GetObjectsList(string securityID)
        {
            return DataAccess.Instance("BizDB").ExecuteList<MenuRightsObjectEntity>("select * from SEC_MENURIGHTSOBJECT where RIGHTSID=@securityID", securityID);
        }

        public List<MenuEntity> GetMenuList()
        {
            return DataAccess.Instance("BizDB").ExecuteList<MenuEntity>("select * from SEC_MENU"); 
        }

        public List<MenuRightsMemberEntity> GetMembersList(int securityID)
        {
            return DataAccess.Instance("BizDB").GetList<MenuRightsMemberEntity>("SecurityLogic_GetMembersList", securityID);
        }

        public MenuRightsEntity GetEntity(string securityID)
        {
            return DataAccess.Instance("BizDB").ExecuteEntity<MenuRightsEntity>("select * from SEC_MENURIGHTS where RIGHTSID=@securityID", securityID);
        }

        public void Delete(string securityID)
        {
            DataAccess.Instance("BizDB").GetList<MenuRightsEntity>("SecurityLogic_Delete", securityID);
        }
        
        /// <summary>
        /// 获取当前用户有权限的菜单
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public List<MenuEntity> GetMenuList(string loginName)
        {
            List<MenuEntity> list = new List<MenuEntity>();
            list = DataAccess.Instance("BizDB").ExecuteList<MenuEntity>("select * from SEC_MENU");
            if (loginName.ToUpper()==ConfigurationManager.AppSettings["AdminAccount"].ToUpper()) //超级管理员，不判断权限
            {
                return list;
            }

            List<MenuEntity> results = new List<MenuEntity>();
            results.AddRange(list.FindAll(p =>ConvertUtil.ToString(p.ACCESSLEVEL).ToUpper()=="PUBLIC"));

            IOrg org = ServiceContainer.Instance().GetService<IOrg>();

            //获取用户所在的组
            List<GroupEntity> groups = org.GetUserGroups(loginName);
            string group = "-1,";
            foreach (GroupEntity g in groups)
            {
                group += g.GROUPID.ToString() + ",";
            }
            group = group.TrimEnd(',');

            //获取用户所在的部门
            List<DepartmentEntity> depts = org.GetUserDepartments(loginName);
            string dept = "-1,";
            foreach (DepartmentEntity d in depts)
            {
                dept += d.DEPARTMENTID.ToString() + ",";
            }
            dept = dept.TrimEnd(',');
            //获取该用户有权限的菜单Id
            UserEntity entity = org.GetUserEntity(loginName);
            string userid = entity.USERID.ToString();
            Hashtable table = new Hashtable();
            table.Add("USERID", userid);
            table.Add("DEPT", dept);
            table.Add("GROUP", group);
            List<MenuRightsObjectEntity> securityObjects = DataAccess.Instance("BizDB").GetList<MenuRightsObjectEntity>("SecurityLogic_GetMenuRightsObjects", table);
            foreach (MenuRightsObjectEntity securityObject in securityObjects)
            {
                if (!results.Exists(p => p.MENUID == securityObject.MENUID))
                {
                    results.Add(list.Find(p => p.MENUID == securityObject.MENUID));
                }
            }
            results.Sort();
            return results;
        }

        /// <summary>
        /// 判断某个资源释是否有权限
        /// </summary>
        /// <returns></returns>
        public bool CheckSecurity(int resourceId,string type, string loginName)
        {
            //List<ResourceEntity> list = GetSecurityResourceList(entity);
            //return list.Exists(p => p.RESOURCEID == resourceId && p.TYPE.ToUpper()==type.ToUpper());
            return false;
        }

        public void SaveSecurity(string rightsId, string name, string remark, string strMembers,
            string strMemberId, List<MenuRightsMemberEntity> members, string securityType, List<MenuRightsObjectEntity> objects)
        {
            SerialNoLogic sn=new SerialNoLogic();
            int count = sn.GetCount("SEC_MENURIGHTS", "RIGHTSID", rightsId);
            MenuRightsEntity ety = new MenuRightsEntity();
            ety.RIGHTSNAME = name;
            ety.REMARK = remark;
            ety.MEMBERNAME = strMembers;
            ety.MEMBERID = strMemberId;

            if (rightsId == Guid.Empty.ToString())
            {
                ety.CREATEBY = SessionLogic.GetLoginName();
                ety.CREATEDATE = DateTime.Now;
                rightsId = Guid.NewGuid().ToString();
                ety.ID = sn.GetMaxNo("SEC_MENURIGHTS", "ID") ;
            }
            else
            {
                ety.UPDATEBY = SessionLogic.GetLoginName();
                ety.UPDATEDATE = DateTime.Now;
            }
            ety.RIGHTSID = rightsId;
            //保存权限名称
            if (count > 0)
            {
                DataAccess.Instance("BizDB").Update("SecurityLogic_Update", ety);
            }
            else
            {
                DataAccess.Instance("BizDB").Insert("SecurityLogic_Insert", ety);
            }

            DataAccess.Instance("BizDB").Delete("SecurityLogic_DeleteMembers", rightsId);
            //保存授权成员
            foreach (MenuRightsMemberEntity member in members)
            {
                member.RIGHTSID = rightsId;
                member.ID = sn.GetMaxNo("SEC_MENURIGHTSMEMBER", "ID");
                DataAccess.Instance("BizDB").Insert("SecurityLogic_InsertMember", member);
            }
            //保存授权对象
            foreach (MenuRightsObjectEntity resource in objects)
            {
                resource.RIGHTSID = rightsId;
                resource.ID = sn.GetMaxNo("SEC_MENURIGHTSOBJECT", "ID") ;
                DataAccess.Instance("BizDB").Insert("SecurityLogic_InsertObject", resource);
            }

        }
        public static string GetMd5(string str, int code)
        {
            if (code == 16) //16位MD5加密（取32位加密的9~25字符） 
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
            }
            if (code == 32) //32位加密 
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
            }
            return "00000000000000000000000000000000";
        }

        
    }
}
