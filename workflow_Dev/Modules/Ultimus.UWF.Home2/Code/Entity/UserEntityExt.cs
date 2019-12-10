using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ultimus.UWF.OrgChart.Entity;

namespace Ultimus.UWF.Home2.Code.Entity
{
    public class UserEntityExt : UserEntity
    {
        public int OrgID { get; set; }
        public int JobID { get; set; }
        /// <summary>
        /// 岗位级别
        /// </summary>
        public string GWLevel { get { return EXT01; } set { EXT01 = value; } }

        /// <summary>
        /// 岗位
        /// </summary>
        public string GW { get { return EXT03; } set { EXT03 = value; } }

        /// <summary>
        /// 岗位英文名
        /// </summary>
        public string GWEngName { get { return EXT07; } set { EXT07 = value; } }

        /// <summary>
        /// 英文名
        /// </summary>
        public string EngName { get { return EXT04; } set { EXT04 = value; } }

        /// <summary>
        /// 成本中心
        /// </summary>
        public string CBCenter { get { return EXT05; } set { EXT05 = value; } }

        /// <summary>
        /// 上级领导账号
        /// </summary>
        public string Leader { get { return EXT02; } set { EXT02 = value; } }
    }
}