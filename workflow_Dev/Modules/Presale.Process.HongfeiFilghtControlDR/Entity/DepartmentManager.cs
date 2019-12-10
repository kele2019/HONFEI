using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.HongfeiFilghtControlDR.Entity
{
	public class DepartmentManager
	{
		public DepartmentManager() { }
		public DepartmentManager(string EXT02) { this.EXT02 = EXT02; }
		public string EXT02 { get; set; }
	}
}