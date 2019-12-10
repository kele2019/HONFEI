using System;
namespace EntityLibrary
{
	/// <summary>
	/// MobileClient_Control:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MobileClient_Control
	{
		public MobileClient_Control()
		{}
		#region Model
		private int _id;
		private string _controlcname;
		private string _controlename;
        private string _controlname;
		private string _isaction;
		/// <summary>
		/// 自增列
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 控件中文名称
		/// </summary>
		public string ControlCName
		{
			set{ _controlcname=value;}
			get{return _controlcname;}
		}
		/// <summary>
		/// 控件英文名称
		/// </summary>
		public string ControlEName
		{
			set{ _controlename=value;}
			get{return _controlename;}
		}
        /// <summary>
        /// 控件英文名称
        /// </summary>
        public string ControlName
        {
            set { _controlname = value; }
            get { return _controlname; }
        }
		/// <summary>
		/// 是否启用
		/// </summary>
		public string IsAction
		{
			set{ _isaction=value;}
			get{return _isaction;}
		}
		#endregion Model

	}
}

