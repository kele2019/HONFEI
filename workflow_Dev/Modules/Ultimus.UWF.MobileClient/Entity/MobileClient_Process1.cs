using System;
namespace EntityLibrary
{
	/// <summary>
	/// MobileClient_Process:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MobileClient_Process
	{
		public MobileClient_Process()
		{}
		#region Model
		private int _id;
		private string _processname;
		private string _logo;
		private DateTime? _createtime;
		private DateTime? _updatetime;
		private string _iscreatepage;
		/// <summary>
		/// 自增列
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 流程名称
		/// </summary>
		public string ProcessName
		{
			set{ _processname=value;}
			get{return _processname;}
		}
		/// <summary>
		/// 页面Logo路径
		/// </summary>
		public string Logo
		{
			set{ _logo=value;}
			get{return _logo;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime? UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// 是否生成页面
		/// </summary>
		public string IsCreatePage
		{
			set{ _iscreatepage=value;}
			get{return _iscreatepage;}
		}
		#endregion Model

	}
}

