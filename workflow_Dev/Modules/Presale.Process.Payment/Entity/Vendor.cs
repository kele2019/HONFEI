using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.Payment.Entity
{
	public class Vendor
	{
		public Vendor() { }
		public Vendor(string VendorCode, string PONO, string BankAccount, string CostCenterNumber, string DownpaymentAmount) {
			this.VendorCode = VendorCode;
			this.PONO = PONO;
			this.BankAccount = BankAccount;
			this.CostCenterNumber = CostCenterNumber;
			this.DownpaymentAmount = DownpaymentAmount;
		}
		public string VendorCode { get; set; }
		public string PONO { get; set; }
		public string BankAccount { get; set; }
		public string CostCenterNumber { get; set; }
		public string DownpaymentAmount { get; set; }
	}
}