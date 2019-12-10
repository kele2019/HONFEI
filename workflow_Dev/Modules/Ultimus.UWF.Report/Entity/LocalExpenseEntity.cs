
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ultimus.UWF.Report.Entity
{
	public class LocalExpenseEntity
	{
		public LocalExpenseEntity() { }
		public LocalExpenseEntity(string VendorName, string VendorCode, decimal? RMB, string CostCenterSubject, string Project) {
			this.VendorName = VendorName;
			this.VendorCode = VendorCode;
			this.RMB = RMB;
			this.CostCenterSubject = CostCenterSubject;
			this.Project = Project;
		}
		public string VendorName { get; set; }
		public string VendorCode { get; set; }
		public decimal? RMB { get; set; }
		public string CostCenterSubject { get; set; }
		public string Project { get; set; }
	}
}