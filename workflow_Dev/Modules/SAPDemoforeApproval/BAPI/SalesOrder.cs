using System;
using System.Collections.Generic;
using System.Text;

namespace SAPDemoforeApproval
{
    public class SalesOrder
    {
        public decimal  NET_VAL_HD;
        public string  CURRENCY;
        public string  REQ_DATE_H;
        public string  PURCH_NO;
        public string  SOLD_TO;
        public string  PMNTTRMS;
        public string  PURCH_NO_C;
        public List<Contact> Contacts;
    }

    public class Contact
    {
        private string _PARTN_ROLE;

        public string PARTN_ROLE
        {
            get { return _PARTN_ROLE; }
            set { _PARTN_ROLE = value; }
        }
        private string _CONTACT;

        public string CONTACT
        {
            get { return _CONTACT; }
            set { _CONTACT = value; }
        }
    }
}
