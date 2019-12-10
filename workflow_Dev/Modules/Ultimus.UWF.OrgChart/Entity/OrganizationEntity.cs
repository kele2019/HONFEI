using System;
namespace Ultimus.UWF.OrgChart.Entity
{
	/// <summary>
	/// 1
	/// </summary>
	[Serializable]
	public partial class OrganizationEntity
	{
        public OrganizationEntity()
		{}
		#region Model
		private int _id;
		private string _organization;
		private DateTime? _effectfrom;
		private DateTime? _effectto;
		private int? _orderno;
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
		public string ORGANIZATION
		{
			set{ _organization=value;}
			get{return _organization;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EFFECTFROM
		{
			set{ _effectfrom=value;}
			get{return _effectfrom;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EFFECTTO
		{
			set{ _effectto=value;}
			get{return _effectto;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ORDERNO
		{
			set{ _orderno=value;}
			get{return _orderno;}
		}
		#endregion Model

	}
}

