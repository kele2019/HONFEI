using System;
using System.Collections.Generic;
using System.Text;
using Ultimus.UWF.Common.Entity;

namespace Ultimus.UWF.Common.Interface
{
    /// <summary>
    /// 资源的接口
    /// </summary>
    public interface IResource 
    {
        /// <summary>
        /// 获取所有资源列表
        /// </summary>
        /// <returns>所有资源列表</returns>
        List<ResourceEntity> GetResourceList();

        /// <summary>
        /// 清除资源缓存
        /// </summary>
        void ClearResourceList();

        List<string> GetSecurityType();

    }
}
