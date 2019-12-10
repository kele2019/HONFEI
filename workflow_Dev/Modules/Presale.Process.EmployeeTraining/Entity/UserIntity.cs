using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.EmployeeTraining.Entity
{
	public class UserIntity
	{
		public UserIntity() { }
		public UserIntity(string USERNAME, string DEPARTMENTNAME) {
			this.USERNAME = USERNAME;
			this.DEPARTMENTNAME = DEPARTMENTNAME;
		}
		public string USERNAME { get; set; }
		public string DEPARTMENTNAME { get; set; }
	}
}