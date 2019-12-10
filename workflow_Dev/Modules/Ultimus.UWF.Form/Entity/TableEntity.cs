using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ultimus.UWF.Form.Entity
{
    public class TableEntity
    {
        
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string type;

        

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
         
    }
}
