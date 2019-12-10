using System;
using Ultimus.UWF.Common.Logic;
using MyLib;
namespace Ultimus.UWF.Security.Entity
{
	/// <summary>
	/// 1
	/// </summary>
	[Serializable]
    public partial class MenuEntity : IComparable
	{
        public MenuEntity()
		{}
		#region Model
		private int _id;
		private string _menuid;
		private string _menuname;
		private string _mappingname;
		private string _parentid;
		private string _formid;
		private string _icon;
		private string _target;
		private string _isvisible;
		private string _relatedfolder;
		private string _relatedform;
		private string _accesslevel;
		private string _remark;
		private int? _orderno;
		private string _isactive;
		private DateTime? _createdate;
		private string _createby;
		private DateTime? _updatedate;
		private string _updateby;
		private string _ext01;
		private string _ext02;
		private string _ext03;
		private string _ext04;
		private string _ext05;
		private string _ext06;
		private string _ext07;
		private string _ext08;
		private string _ext09;
		private string _ext10;
		private string _ext11;
		private string _ext12;
		private string _ext13;
		private string _ext14;
		private string _ext15;
		private string _ext16;
		private string _ext17;
		private string _ext18;
		private string _ext19;
		private string _ext20;
		private string _ext21;
		private string _ext22;
		private string _ext23;
		private string _ext24;
		private string _ext25;
		private string _ext26;
		private string _ext27;
		private string _ext28;
		private string _ext29;
		private string _ext30;

        int _height;

        public int HEIGHT
        {
            get { return _height; }
            set { _height = value; }
        }
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
		public string MENUID
		{
			set{ _menuid=value;}
			get{return _menuid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MENUNAME
		{
			set{ _menuname=value;}
			get{return _menuname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DISPLAYNAME
		{
			set{ _mappingname=value;}
			get{
                string field= Lang.Get("MenuNameField");
                switch (field.ToUpper().Trim())
                {
                    case "MENUNAME":
                        return this.MENUNAME;
                    case "DISPLAYNAME":
                        return _mappingname;
                    case "EXT01":
                        return this.EXT01;
                    case "EXT02":
                        return this.EXT02;
                    case "EXT03":
                        return this.EXT03;
                }
                return _mappingname;
            }
		}
		/// <summary>
		/// 
		/// </summary>
		public string PARENTID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FORMID
		{
			set{ _formid=value;}
			get{return _formid;}
		}

        string _url;

        public string URL
        {
            get { return _url; }
            set { _url = value; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string ICON
		{
			set{ _icon=value;}
			get{return _icon;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TARGET
		{
			set{ _target=value;}
			get{return _target;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ISVISIBLE
		{
			set{ _isvisible=value;}
			get{return _isvisible;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RELATEDFOLDER
		{
			set{ _relatedfolder=value;}
			get{return _relatedfolder;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RELATEDFORM
		{
			set{ _relatedform=value;}
			get{return _relatedform;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ACCESSLEVEL
		{
			set{ _accesslevel=value;}
			get{return _accesslevel;}
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
		public int? ORDERNO
		{
			set{ _orderno=value;}
			get{return _orderno;}
		}

        string _MENUTYPE;

        public string MENUTYPE
        {
            get { return _MENUTYPE; }
            set { _MENUTYPE = value; }
        }

        string _MODULE;

        public string MODULE
        {
            get { return _MODULE; }
            set { _MODULE = value; }
        }

		/// <summary>
		/// 
		/// </summary>
		public string ISACTIVE
		{
			set{ _isactive=value;}
			get{return _isactive;}
		}

        string _ishomepage;

        public string ISHOMEPAGE
        {
            get { return _ishomepage; }
            set { _ishomepage = value; }
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
		/// <summary>
		/// 
		/// </summary>
		public string EXT01
		{
			set{ _ext01=value;}
			get{return _ext01;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT02
		{
			set{ _ext02=value;}
			get{return _ext02;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT03
		{
			set{ _ext03=value;}
			get{return _ext03;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT04
		{
			set{ _ext04=value;}
			get{return _ext04;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT05
		{
			set{ _ext05=value;}
			get{return _ext05;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT06
		{
			set{ _ext06=value;}
			get{return _ext06;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT07
		{
			set{ _ext07=value;}
			get{return _ext07;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT08
		{
			set{ _ext08=value;}
			get{return _ext08;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT09
		{
			set{ _ext09=value;}
			get{return _ext09;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT10
		{
			set{ _ext10=value;}
			get{return _ext10;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT11
		{
			set{ _ext11=value;}
			get{return _ext11;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT12
		{
			set{ _ext12=value;}
			get{return _ext12;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT13
		{
			set{ _ext13=value;}
			get{return _ext13;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT14
		{
			set{ _ext14=value;}
			get{return _ext14;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT15
		{
			set{ _ext15=value;}
			get{return _ext15;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT16
		{
			set{ _ext16=value;}
			get{return _ext16;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT17
		{
			set{ _ext17=value;}
			get{return _ext17;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT18
		{
			set{ _ext18=value;}
			get{return _ext18;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT19
		{
			set{ _ext19=value;}
			get{return _ext19;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT20
		{
			set{ _ext20=value;}
			get{return _ext20;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT21
		{
			set{ _ext21=value;}
			get{return _ext21;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT22
		{
			set{ _ext22=value;}
			get{return _ext22;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT23
		{
			set{ _ext23=value;}
			get{return _ext23;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT24
		{
			set{ _ext24=value;}
			get{return _ext24;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT25
		{
			set{ _ext25=value;}
			get{return _ext25;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT26
		{
			set{ _ext26=value;}
			get{return _ext26;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT27
		{
			set{ _ext27=value;}
			get{return _ext27;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT28
		{
			set{ _ext28=value;}
			get{return _ext28;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT29
		{
			set{ _ext29=value;}
			get{return _ext29;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXT30
		{
			set{ _ext30=value;}
			get{return _ext30;}
		}
		#endregion Model


        public int CompareTo(object obj)
        {
            MenuEntity pe = obj as MenuEntity;
            if (this.ORDERNO > pe.ORDERNO)
            {
                return 1;
            }
            if (this.ORDERNO < pe.ORDERNO)
            {
                return -1;
            }
            if (this.ID > pe.ID)
            {
                return 1;
            }
            if (this.ID < pe.ID)
            {
                return -1;
            }
            return 0;
        }
    }
}

