using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.PaymentRequestForm
{
    public class SAPPOEntity
    {
        public string PONo { get; set; }
        public string PORemark { get; set; }
        public string GRNo { get; set; }
        public string GRRemark { get; set; }
        public string GRTotal { get; set; }
        public string FORMID { get; set; }


    }
}