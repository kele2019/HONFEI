using System;
namespace Ultimus.UWF.Common.Entity
{
    /// <summary>
    /// ServiceEntity:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ServiceEntity
    {
        public ServiceEntity()
        { }
        #region Model
        private int _id;
        private string _module;
        private string _uri;
        private string _uritype;
        private string _isactive;
        private string _description;
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
        public string URI
        {
            set { _uri = value; }
            get { return _uri; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string URITYPE
        {
            set { _uritype = value; }
            get { return _uritype; }
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
        public string DESCRIPTION
        {
            set { _description = value; }
            get { return _description; }
        }
        #endregion Model

    }
}

