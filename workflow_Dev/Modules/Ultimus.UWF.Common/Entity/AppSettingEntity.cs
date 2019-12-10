using System;
namespace Ultimus.UWF.Common.Entity
{
    /// <summary>
    /// COM_APPSETTINGS:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class AppSettingEntity
    {
        public AppSettingEntity()
        { }
        #region Model
        private int _id;
        private string _module;
        private string _name;
        private string _value;
        private string _isactive;
        private string _remark;
        private int? _orderno;
        private string _isbool;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MODULE
        {
            set { _module = value; }
            get { return _module; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NAME
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string VALUE
        {
            set { _value = value; }
            get { return _value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ISACTIVE
        {
            set { _isactive = value; }
            get { return _isactive; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string REMARK
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ORDERNO
        {
            set { _orderno = value; }
            get { return _orderno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ISBOOL
        {
            set { _isbool = value; }
            get { return _isbool; }
        }
        #endregion Model

    }
}

