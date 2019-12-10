using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ultimus.UWF.Home2.Code.Rep
{
    public class RepOrgTree
    {
        public int id { get; set; }
        public string text { get; set; }
        public List<RepOrgTree> children { get; set; }
        public string state { get; set; }
        public string ext02 { get; set; }
    }
}