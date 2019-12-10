using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ultimus.UWF.Form.Entity
{
    public class SearchEntity
    {
        private string _SearchType;
        /// <summary>
        /// 搜索模式（1：单选，2：多选）
        /// </summary>
        public string SearchType
        {
            get { return _SearchType; }
            set { _SearchType = value; }
        }
        private string _ConnectionString;
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }
        private string _TableName;
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        private List<ColumnEntity> _Columns;
        /// <summary>
        /// 字段名称
        /// </summary>
        public List<ColumnEntity> Columns
        {
            get { return _Columns; }
            set { _Columns = value; }
        }
        private string _StrWhere;
        /// <summary>
        /// 查询条件
        /// </summary>
        public string StrWhere
        {
            get { return _StrWhere; }
            set { _StrWhere = value; }
        }
        private string _OrderBy;
        /// <summary>
        /// 排序规则
        /// </summary>
        public string OrderBy
        {
            get { return _OrderBy; }
            set { _OrderBy = value; }
        }
        private int _SearchCount;
        /// <summary>
        /// 查询条数
        /// </summary>
        public int SearchCount
        {
            get { return _SearchCount; }
            set { _SearchCount = value; }
        }
    }
    public class ColumnEntity
    {
        private string _Title;

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        private string _Column;

        public string Column
        {
            get { return _Column; }
            set { _Column = value; }
        }
        private string _ColumnName;

        public string ColumnName
        {
            get { return _ColumnName; }
            set { _ColumnName = value; }
        }

        private string _Display;

        public string Display
        {
            get { return _Display; }
            set { _Display = value; }
        }

        private string _DisplayConditions;

        public string DisplayConditions
        {
            get { return _DisplayConditions; }
            set { _DisplayConditions = value; }
        }
    }
}