using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ultimus.UWF.Home2.Code.Rep
{
    public class RepTreeNode
    {
        public string id { get; set; }
        public string text { get; set; }
        public string type { get; set; }
        public List<RepTreeNode> children { get; set; }
        public string state { get; set; }
        public string EngName { get; set; }

    }
}