using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ultimus.UWF.WebService.Rep
{
    public class RepResult
    {
        public int Code { get; set; }
        public string Msg { get; set; }
    }

    public class RepResult<T> : RepResult
    {
        public T Data { get; set; }
    }
}