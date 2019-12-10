using System;
namespace Ultimus.UWF.Security.Entity
{
	/// <summary>
	/// 1
	/// </summary>
	[Serializable]
	public partial class MenuRightsObjectEntity
	{
        public MenuRightsObjectEntity()
		{}
		#region Model
		private int _id;
		private string _rightsid;
		private string _menuid;
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
		public string MENUID
		{
			set{ _menuid=value;}
			get{return _menuid;}
		}
		#endregion Model

	}
}

