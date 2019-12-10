using System;
namespace Ultimus.UWF.Security.Entity
{
	/// <summary>
	/// 1
	/// </summary>
	[Serializable]
	public partial class MenuRightsMemberEntity
	{
        public MenuRightsMemberEntity()
		{}
		#region Model
		private int _id;
		private string _rightsid;
		private int? _memberid;
		private string _membername;
		private string _membertype;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RIGHTSID
		{
			set{ _rightsid=value;}
			get{return _rightsid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? MEMBERID
		{
			set{ _memberid=value;}
			get{return _memberid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MEMBERNAME
		{
			set{ _membername=value;}
			get{return _membername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MEMBERTYPE
		{
			set{ _membertype=value;}
			get{return _membertype;}
		}
		#endregion Model

	}
}

