using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ultimus.UWF.OrgChart.Entity;

namespace Ultimus.UWF.Home2.Code.DAO
{
    public class DepartmentEntityExt : DepartmentEntity
    {
        /// <summary>
        /// 排序 EXT01
        /// </summary>
        public int Sort { get { int i = 0; int.TryParse(EXT01, out i); return i ; } set { EXT01 = value.ToString(); } }

        /// <summary>
        /// 成本中心 EXT02
        /// </summary>
        public string CBCenter { get { return EXT02; } set { EXT02 = value; } }

        /// <summary>
        /// 英文名称 EXT03
        /// </summary>
        public string EngName { get { return EXT03; } set { EXT03 = value; } }

        /// <summary>
        /// 部门级别 EXT04
        /// </summary>
        public string Level { get { return EXT04; } set { EXT04 = value; } }

        /// <summary>
        /// 部门完整路径 EXT05
        /// </summary>
        public string Path { get { return EXT05; } set { EXT05 = value; } }
    }
}