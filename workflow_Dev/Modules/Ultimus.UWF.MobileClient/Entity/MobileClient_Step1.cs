using System;
namespace EntityLibrary
{
	/// <summary>
	/// MobileClient_Step:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MobileClient_Step
	{
		public MobileClient_Step()
		{}
		#region Model
		private int _id;
		private int? _fk_id;
		private string _stepname;
		private string _stepcname;
		private string _stepename;
		/// <summary>
		/// 自增列
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 主表外键
		/// </summary>
		public int? FK_ID
		{
			set{ _fk_id=value;}
			get{return _fk_id;}
		}
		/// <summary>
		/// 步骤名称
		/// </summary>
		public string StepName
		{
			set{ _stepname=value;}
			get{return _stepname;}
		}
		/// <summary>
		/// 步骤中文名称
		/// </summary>
		public string StepCName
		{
			set{ _stepcname=value;}
			get{return _stepcname;}
		}
		/// <summary>
		/// 步骤英文名称
		/// </summary>
		public string StepEName
		{
			set{ _stepename=value;}
			get{return _stepename;}
		}
		#endregion Model

	}
}

