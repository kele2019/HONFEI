using System;
namespace Ultimus.UWF.Security.Entity
{
	/// <summary>
	/// 1
	/// </summary>
	[Serializable]
	public partial class MenuRightsEntity
	{
        public MenuRightsEntity()
		{}
		#region Model
		private int _id;
		private string _menurightsid;
		private string _rightsname;
		private string _membername;
		private string _memberid;
		private string _remark;
		private DateTime? _createdate;
		private string _createby;
		private DateTime? _updatedate;
		private string _updateby;
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
			set{ _menurightsid=value;}
			get{return _menurightsid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RIGHTSNAME
		{
			set{ _rightsname=value;}
			get{return _rightsname;}
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
		public string MEMBERID
		{
			set{ _memberid=value;}
			get{return _memberid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string REMARK
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CREATEDATE
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CREATEBY
		{
			set{ _createby=value;}
			get{return _createby;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? UPDATEDATE
		{
			set{ _updatedate=value;}
			get{return _updatedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UPDATEBY
		{
			set{ _updateby=value;}
			get{return _updateby;}
		}
		#endregion Model

	}
}

