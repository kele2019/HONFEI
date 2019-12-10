using System;
namespace Ultimus.UWF.Security.Entity
{
	/// <summary>
	/// 1
	/// </summary>
	[Serializable]
	public partial class DataRightsFieldEntity
	{
        public DataRightsFieldEntity()
		{}
		#region Model
		private int _id;
		private string _rightsid;
		private string _fieldtype;
		private string _fieldname;
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
		public string FIELDTYPE
		{
			set{ _fieldtype=value;}
			get{return _fieldtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FIELDNAME
		{
			set{ _fieldname=value;}
			get{return _fieldname;}
		}
		#endregion Model

	}
}

