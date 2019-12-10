using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ultimus.UWF.FunctionManager.Entity
{
    public class CurrencyEntity : IComparable
    {
        int _ID;
        string _DicCode;
        string _DicText;
        string _DicValue;
        public CurrencyEntity() { }

        public int ID
        {
            get { return _ID; }
            set { _ID = value;}
        }
        public string DicCode
        {
            get { return _DicCode; }
            set { _DicCode = value; }
        }

        public string DicText
        {
            get { return _DicText; }
            set { _DicText = value; }
        }
        public string DicValue
        {
            get { return _DicValue; }
            set { _DicValue = value; }
        }
        public int CompareTo(object obj)
        {
            CurrencyEntity td = obj as CurrencyEntity;
            return string.Compare(this.DicCode, td.DicCode);
        }
    }
}