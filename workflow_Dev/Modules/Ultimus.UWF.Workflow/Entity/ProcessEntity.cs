using System;
using System.Collections.Generic;
using System.Text;

namespace Ultimus.UWF.Workflow.Entity
{
    public class ProcessEntity:IComparable
    {
        string _processName;

        public string PROCESSNAME
        {
            get { return _processName==null?"":_processName.Trim(); }
            set { _processName = value; }
        }

        string _categoryName;

        public string CATEGORYNAME
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }

        int _initiatorType;

        public int INITIATORTYPE
        {
            get { return _initiatorType; }
            set { _initiatorType = value; }
        }

        string _initiateID;

        public string INITIATEID
        {
            get { return _initiateID; }
            set { _initiateID = value; }
        }

        string _icon;

        public string ICON
        {
            get { return _icon; }
            set { _icon = value; }
        }

        string _recipient;

        public string RECIPIENT
        {
            get { return _recipient; }
            set { _recipient = value; }
        }

        int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        int _ORDERNO;

        public int ORDERNO
        {
            get { return _ORDERNO; }
            set { _ORDERNO = value; }
        }
        string _CATEGORYENNAME;

        public string CATEGORYENNAME
        {
            get { return _CATEGORYENNAME; }
            set { _CATEGORYENNAME = value; }
        }
        string _PROCESSENNAME;

        public string PROCESSENNAME
        {
            get { return _PROCESSENNAME; }
            set { _PROCESSENNAME = value; }
        }
        int _PROCESSNO;

        public int PROCESSNO
        {
            get { return _PROCESSNO; }
            set { _PROCESSNO = value; }
        }

        public int CompareTo(object obj)
        {
            ProcessEntity pe=obj as ProcessEntity;
            return string.Compare(this.PROCESSNAME,pe.PROCESSNAME);
        }

        string _ServiceName;
        /// <summary>
        /// 数据库表 COM_APPSETTINGS 中 Name 配置项值，对应各个版本所在服务器的名称。
        /// </summary>
        public string ServiceName
        {
            get { return _ServiceName; }
            set { _ServiceName = value; }
        }
    }
}
