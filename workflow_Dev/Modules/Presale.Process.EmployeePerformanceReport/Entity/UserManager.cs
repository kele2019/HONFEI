using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.EmployeePerformanceReport.Entity
{
	public class UserManager
	{
		public UserManager() { }
		public UserManager(string EXT02) { this.EXT02 = EXT02; }
		public string EXT02 { get; set; }
	}
}