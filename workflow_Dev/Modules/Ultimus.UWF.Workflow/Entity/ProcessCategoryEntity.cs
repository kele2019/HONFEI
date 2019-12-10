using System;
using System.Collections.Generic;
using System.Text;

namespace Ultimus.UWF.Workflow.Entity
{
    public class ProcessCategoryEntity:IComparable
    {
        string _categoryID="all";
        public string CATEGORYID
        {
            get
            {
                return _categoryID;
            }
            set
            {
                _categoryID = value;
            }
        }

        string _categoryName;

        public string CATEGORYNAME
        {
            get {
                 
                
                return _categoryName.Trim(); }
            set { _categoryName = value; }
        }
        string _CATEGORYENNAME;

        public string CATEGORYENNAME
        {
            get { return _CATEGORYENNAME; }
            set { _CATEGORYENNAME = value; }
        }


        string _icon;

        public string ICON
        {
            get {
                if (string.IsNullOrEmpty(_icon))
                {
                    _icon = "allSel.png";
                }
                return _icon; }
            set { _icon = value; }
        }

        int _ORDERNO;

        public int ORDERNO
        {
            get { return _ORDERNO; }
            set { _ORDERNO = value; }
        }


        public int CompareTo(object obj)
        {
            ProcessCategoryEntity ety = obj as ProcessCategoryEntity;
            return this.CATEGORYNAME.CompareTo(ety.CATEGORYNAME);
        }

        string _processCount;

        public string PROCESSCOUNT
        {
            get
            {


                return _processCount.Trim();
            }
            set { _processCount = value; }
        }
    }
}
