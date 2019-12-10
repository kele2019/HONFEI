using System;
namespace Ultimus.UWF.Security.Entity
{
	/// <summary>
	/// 1
	/// </summary>
	[Serializable]
	public partial class DataRightsEntity
	{
        public DataRightsEntity()
		{}
		#region Model
		private int _id;
		private string _datarightsid;
		private string _rightsname;
		private string _membername;
		private string _memberid;
		private string _formid;
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
		public string DATARIGHTSID
		{
			set{ _datarightsid=value;}
			get{return _datarightsid;}
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
		public string FORMID
		{
			set{ _formid=value;}
			get{return _formid;}
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

