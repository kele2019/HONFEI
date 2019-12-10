using System;

namespace EntityLibrary
{
    [Serializable]
    public class MobileClient_Classification
    {

        /// <summary>
        /// 自增列
        /// </summary>		
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 类别中文名称
        /// </summary>		
        private string _categorycname;
        public string CategoryCName
        {
            get { return _categorycname; }
            set { _categorycname = value; }
        }
        /// <summary>
        /// 类别英文名称
        /// </summary>		
        private string _categoryename;
        public string CategoryEName
        {
            get { return _categoryename; }
            set { _categoryename = value; }
        }
        /// <summary>
        /// 流程名称
        /// </summary>		
        private string _processname;
        public string ProcessName
        {
            get { return _processname; }
            set { _processname = value; }
        }
        private string _beginstepname;

        public string Beginstepname
        {
            get { return _beginstepname; }
            set { _beginstepname = value; }
        }
        /// <summary>
        /// 是否启用
        /// </summary>		
        private string _isaction;
        public string IsAction
        {
            get { return _isaction; }
            set { _isaction = value; }
        }

    }
}