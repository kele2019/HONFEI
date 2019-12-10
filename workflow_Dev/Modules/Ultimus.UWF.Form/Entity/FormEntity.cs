using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ultimus.UWF.Form.Entity
{
    public class FormEntity
    {
        string processName;
        string tableName;

        public string ProcessName
        {
            get { return processName; }
            set { processName = value; }
        }

        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string tableNameDetail="";

        public string TableNameDetail
        {
            get { return tableNameDetail; }
            set { tableNameDetail = value; }
        }

        private string projectName;

        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }

        string projectFullName;

        public string ProjectFullName
        {
            get { return projectFullName; }
            set { projectFullName = value; }
        }

        List<FieldEntity> fields;

        public List<FieldEntity> Fields
        {
            get { return fields; }
            set { fields = value; }
        }

        List<TableEntity> tables;

        public List<TableEntity> Tables
        {
            get { return tables; }
            set { tables = value; }
        }
    }
}
