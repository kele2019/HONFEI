using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ultimus.UWF.Form.Entity
{
    public class FieldEntity
    {
        int minOccurs;

        public int MinOccurs
        {
            get { return minOccurs; }
            set { minOccurs = value; }
        }
        int maxOccurs;

        public int MaxOccurs
        {
            get { return maxOccurs; }
            set { maxOccurs = value; }
        }
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        bool nillable;

        public bool Nillable
        {
            get { return nillable; }
            set { nillable = value; }
        }

        string description;

        public string Description
        {
            get {
                if (string.IsNullOrEmpty(description))
                {
                    description = name;
                }
                return description; }
            set { description = value; }
        }

        string complexType;

        public string ComplexType
        {
            get { return complexType; }
            set { complexType = value; }
        }
    }
}
