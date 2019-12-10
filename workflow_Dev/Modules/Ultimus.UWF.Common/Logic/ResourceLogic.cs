using System;
using System.Collections.Generic;
using System.Text;
using MyLib;
using System.Data;
using Ultimus.UWF.Common.Interface;
using Ultimus.UWF.Common.Entity;

namespace Ultimus.UWF.Common.Logic
{
    /// <summary>
    /// 资源访问类，获取所有资源请用此类
    /// </summary>
    public class ResourceLogic:IResource
    {
        public List<ResourceEntity> GetAllResourceList()
        {
            //IResource[] resources = ServiceContainer.Instance().GetAll<IResource>();
            List<ResourceEntity> lists = new List<ResourceEntity>();
            //foreach (IResource resource in resources)
            //{
            IResource resource = new ResourceLogic();
                lists.AddRange(resource.GetResourceList());
            //}
            return lists;
        }

        public void ClearAllResourceList()
        {
            IResource[] resources = ServiceContainer.Instance().GetAll<IResource>();
            foreach (IResource resource in resources)
            {
                resource.ClearResourceList();
            }
        }

        public ResourceEntity GetEntity(int resourceId)
        {
            List<ResourceEntity> list = GetAllResourceList();
            return list.Find(p => p.ID == resourceId);
        }

        /// <summary>
        /// 获取系统配置中的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetKeyValue(string key)
        {
            List<ResourceEntity> list = GetAllResourceList();
            ResourceEntity resource= list.Find(p => p.NAME.ToUpper() == key.ToUpper());
            if (resource != null)
            {
                return resource.VALUE ;
            }

            return string.Empty;
        }

        public List<ResourceEntity> GetResourceList(string type)
        {
            ResourceLogic resource = new ResourceLogic();
            List<ResourceEntity> list = resource.GetAllResourceList();
            return list.FindAll(p => p.TYPE.ToUpper() == type.ToUpper());
        }

        public List<ResourceEntity> GetResourceList()
        {
            List<ResourceEntity> lists = DataAccess.Instance().GetList<ResourceEntity>("ResourceLogic_GetResourceList", null);
            return lists;
        }

        public void ClearResourceList()
        {
            DataAccess.Instance().GetObject("ResourceLogic_ClearResourceList", null);
        }

        public void Save(ResourceEntity ety)
        {
            int count = DataAccess.Instance("BizDB").GetEntity<int>("ResourceLogic_Exist", ety.ID);
            //保存
            if (count > 0)
            {
                DataAccess.Instance("BizDB").Update("ResourceLogic_Update", ety);
            }
            else
            {
                DataAccess.Instance("BizDB").Insert("ResourceLogic_Insert", ety);
            }

            ClearResourceList();
        }

        public void DeleteResource(int resouceId)
        {
            string strSql = "DELETE FROM COM_RESOURCE WHERE ID=" + resouceId;
            DataAccess.Instance("BizDB").ExecuteScalar(strSql);
            ClearResourceList();
        }

        public List<string> GetTopTypeList()
        {
            List<string> strs = new List<string>();

            DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable("SELECT TYPE FROM COM_RESOURCE WHERE PARENTID='0' OR PARENTID IS NULL  GROUP BY TYPE");
            foreach (DataRow row in dt.Rows)
            {
                strs.Add(ConvertUtil.ToString(row[0]));
            }
            return strs;
        }

        public List<string> GetSecurityType()
        {
            return new List<string>();
        }
    }
}
