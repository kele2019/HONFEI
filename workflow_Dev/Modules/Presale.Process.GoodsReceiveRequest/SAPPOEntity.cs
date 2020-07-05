using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.GoodsReceiveRequest
{
    public class SAPPOEntity
    {
        public string FORMID { get; set; }
        public string ITEMNO { get; set; }
        public string QUANTITY { get; set; }
        public string UnitPrice { get; set; }
        public string TaxCode { get; set; }
        public string CostCenter { get; set; }
        public string UOMCode { get; set; }
        public string PRNo { get; set; }
        public string Requester { get; set; }
        public string DetailDescription { get; set; }
        public string RequestDate { get; set; }
        public string PONo { get; set; }
        public string POLineNo { get; set; }
        public string PRLineNo { get; set; }

    }
}