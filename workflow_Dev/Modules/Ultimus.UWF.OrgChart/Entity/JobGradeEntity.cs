using System;
namespace Ultimus.UWF.OrgChart.Entity
{
	/// <summary>
	/// 1
	/// </summary>
	[Serializable]
	public partial class JobGradeEntity
	{
        public JobGradeEntity()
		{}
		#region Model
		private int _id;
		private string _jobgrade;
		private int? _orderno;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}

        string _ORGANIZATION;

        public string ORGANIZATION
        {
            get { return _ORGANIZATION; }
            set { _ORGANIZATION = value; }
        }

		/// <summary>
		/// 
		/// </summary>
		public string JOBGRADE
		{
			set{ _jobgrade=value;}
			get{return _jobgrade;}
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

