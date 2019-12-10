using System;
namespace EntityLibrary
{
	/// <summary>
	/// MobileClient_StepControl:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MobileClient_StepControl
	{
		public MobileClient_StepControl()
		{}
		#region Model
		private int _id;
		private int? _fk_id;
		private string _columnname;
		private int? _controlid;
		private string _format;
		private string _iswillfill;
		private string _readonly;
		private string _externallinks;
		private string _isshow;
        private decimal _orderby;
		private string _sourcetype;
		private string _sourceconnectionstring;
		private string _sourcetablename;
		private string _sourcecolumnname;
        private string _sourcewhere;
		private string _sourcevariablename;
        private string _ismastertable;
        private string _issublist;
		private string _desttype;
		private string _destconnectionstring;
		private string _desttablename;
		private string _destcolumnname;
		private string _destvariablename;
		/// <summary>
		/// 自增列
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 步骤表主键
		/// </summary>
		public int? FK_ID
		{
			set{ _fk_id=value;}
			get{return _fk_id;}
		}
		/// <summary>
		/// 字段名称
		/// </summary>
		public string ColumnName
		{
			set{ _columnname=value;}
			get{return _columnname;}
		}
		/// <summary>
		/// 页面控件
		/// </summary>
		public int? ControlID
		{
			set{ _controlid=value;}
			get{return _controlid;}
		}
		/// <summary>
		/// 显示格式
		/// </summary>
		public string Format
		{
			set{ _format=value;}
			get{return _format;}
		}
		/// <summary>
		/// 是否必填
		/// </summary>
		public string IsWillFill
		{
			set{ _iswillfill=value;}
			get{return _iswillfill;}
		}
		/// <summary>
		/// 是否只读
		/// </summary>
		public string ReadOnly
		{
			set{ _readonly=value;}
			get{return _readonly;}
		}
		/// <summary>
		/// 外部链接
		/// </summary>
		public string ExternalLinks
		{
			set{ _externallinks=value;}
			get{return _externallinks;}
		}
		/// <summary>
		/// 是否显示
		/// </summary>
		public string IsShow
		{
			set{ _isshow=value;}
			get{return _isshow;}
		}
		/// <summary>
		/// 显示顺序
		/// </summary>
		public decimal OrderBy
		{
			set{ _orderby=value;}
			get{return _orderby;}
		}
		/// <summary>
		/// 数据源类型
		/// </summary>
		public string SourceType
		{
			set{ _sourcetype=value;}
			get{return _sourcetype;}
		}
		/// <summary>
		/// 连接字符串
		/// </summary>
		public string SourceConnectionString
		{
			set{ _sourceconnectionstring=value;}
			get{return _sourceconnectionstring;}
		}
		/// <summary>
		/// 表名称
		/// </summary>
		public string SourceTableName
		{
			set{ _sourcetablename=value;}
			get{return _sourcetablename;}
		}
		/// <summary>
		/// 字段名称
		/// </summary>
		public string SourceColumnName
		{
			set{ _sourcecolumnname=value;}
			get{return _sourcecolumnname;}
		}
        /// <summary>
        /// 字段名称
        /// </summary>
        public string SourceWhere
        {
            set { _sourcewhere = value; }
            get { return _sourcewhere; }
        }
		/// <summary>
		/// 电子表格变量名称
		/// </summary>
		public string SourceVariableName
		{
			set{ _sourcevariablename=value;}
			get{return _sourcevariablename;}
		}
        /// <summary>
        /// 
        /// </summary>
        public string IsMasterTable
        {
            set { _ismastertable = value; }
            get { return _ismastertable; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsSublist
        {
            set { _issublist = value; }
            get { return _issublist; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string DestType
		{
			set{ _desttype=value;}
			get{return _desttype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DestConnectionString
		{
			set{ _destconnectionstring=value;}
			get{return _destconnectionstring;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DestTableName
		{
			set{ _desttablename=value;}
			get{return _desttablename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DestColumnName
		{
			set{ _destcolumnname=value;}
			get{return _destcolumnname;}
		}
		/// <summary>
		/// 电子表格变量名称
		/// </summary>
		public string DestVariableName
		{
			set{ _destvariablename=value;}
			get{return _destvariablename;}
		}
		#endregion Model

	}
}

