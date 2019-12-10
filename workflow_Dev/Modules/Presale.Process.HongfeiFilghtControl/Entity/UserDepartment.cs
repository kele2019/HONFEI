using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.HongfeiFilghtControl.Entity
{
	public class UserDepartment
	{
		public UserDepartment() { }
		public UserDepartment(string DEPARTMENTNAME) { this.DEPARTMENTNAME = DEPARTMENTNAME; }
		public string DEPARTMENTNAME { get; set; }
	}
}