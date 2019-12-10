using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
namespace Ultimus.UWF.Common.Entity
{
	/// <summary>
	/// 类SerialNo。
	/// </summary>
	[Serializable]
	public partial class SerialNoEntity
	{
        public SerialNoEntity()
		{}
		#region Model
		private string _serialtype;
		private int? _serialno;
		/// <summary>
		/// 
		/// </summary>
		public string SERIALTYPE
		{
			set{ _serialtype=value;}
			get{return _serialtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SERIALNO
		{
			set{ _serialno=value;}
			get{return _serialno;}
		}

        int _SerialYear;

        public int SERIALYEAR
        {
            get { return _SerialYear; }
            set { _SerialYear = value; }
        }
        int _SerialMonth;

        public int SERIALMONTH
        {
            get { return _SerialMonth; }
            set { _SerialMonth = value; }
        }
        int _SerialDay;

        public int SERIALDAY
        {
            get { return _SerialDay; }
            set { _SerialDay = value; }
        }

        DateTime _UpdateDate=DateTime.Now;

        public DateTime UPDATEDATE
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }

        int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
		#endregion Model


		
	}
}

