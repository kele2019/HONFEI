using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.EmployeePerformanceReport.Entity
{
	public class EmployeeLoginEntity
	{
		public EmployeeLoginEntity() { }
		public EmployeeLoginEntity(string LOGINNAME) { this.LOGINNAME = LOGINNAME; }
		public string LOGINNAME { get; set; }
	}
}