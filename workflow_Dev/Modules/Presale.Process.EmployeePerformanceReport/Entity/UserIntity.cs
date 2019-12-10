using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.EmployeePerformanceReport.Entity
{
	public class UserIntity
	{
		public UserIntity() { }
		public UserIntity(string USERNAME, string DEPARTMENTNAME,string EXT03) {
			this.USERNAME = USERNAME;
			this.DEPARTMENTNAME = DEPARTMENTNAME;
			this.EXT03 = EXT03;
		}
		public string USERNAME { get; set; }
		public string DEPARTMENTNAME { get; set; }
		public string EXT03 { get; set; }
	}
}