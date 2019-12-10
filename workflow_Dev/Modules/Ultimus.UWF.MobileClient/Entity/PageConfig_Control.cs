using System;
using System.Collections.Generic;

namespace EntityLibrary
{
    [Serializable]
    public class PageConfig_Control
    {
        private string _ColumnName;

        public string ColumnName
        {
            get { return _ColumnName; }
            set { _ColumnName = value; }
        }
        private string _ControlEName;

        public string ControlEName
        {
            get { return _ControlEName; }
            set { _ControlEName = value; }
        }
        private string _Control;

        public string Control
        {
            get { return _Control; }
            set { _Control = value; }
        }
        private string _DestColumnName;

        public string DestColumnName
        {
            get { return _DestColumnName; }
            set { _DestColumnName = value; }
        }

        string _tableName;

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        string _IsShow;

        public string IsShow
        {
            get { return _IsShow; }
            set { _IsShow = value; }
        }

        string _format;

        public string FORMAT
        {
            get { return _format; }
            set { _format = value; }
        }

        string _isWillFill;

        public string ISWILLFILL
        {
            get { return _isWillFill; }
            set { _isWillFill = value; }
        }

        string _isReadOnly;

        public string ISREADONLY
        {
            get { return _isReadOnly; }
            set { _isReadOnly = value; }
        }
    }
}
