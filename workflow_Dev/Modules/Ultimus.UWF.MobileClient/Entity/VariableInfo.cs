using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLibrary
{
    [Serializable]
    public class VariableInfo
    {
        private string _Name;
        /// <summary>
        /// 获取或设置 Name 的值
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private List<string> _Value;
        /// <summary>
        /// 获取或设置 Value 的值
        /// </summary>
        public List<string> Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
    }
}
