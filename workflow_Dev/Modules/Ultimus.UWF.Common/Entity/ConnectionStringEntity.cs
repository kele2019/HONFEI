using System;
namespace Ultimus.UWF.Common.Entity
{
    /// <summary>
    /// ConnectionStringEntity:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ConnectionStringEntity
    {
        public ConnectionStringEntity()
        { }
        #region Model
        private int _id;
        private string _module;
        private string _name;
        private string _connectionstring;
        private string _providername;
        private string _isactive;
        private string _remark;
        private int? _orderno;
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
        public string CONNECTIONSTRING
        {
            set { _connectionstring = value; }
            get { return _connectionstring; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PROVIDERNAME
        {
            set { _providername = value; }
            get { return _providername; }
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
        #endregion Model

    }
}

