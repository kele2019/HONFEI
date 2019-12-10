using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ultimus.UWF.Workflow.Entity
{
    public class PageEntity
    {
        private int _count;
        private int _pageCount;
        private int _pageNum;
        private List<Object> _datas;
        
        /// <summary>
        /// 总共行数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 每夜行数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 当前第几页
        /// </summary>
        public int PageNum { get; set; }

        /// <summary>
        /// Task数据
        /// </summary>
        public List<TaskEntity> TaskDatas { get; set; }

        /// <summary>
        /// Process数据
        /// </summary>
        public List<ProcessEntity> ProcessDatas { get; set; }
    }
}