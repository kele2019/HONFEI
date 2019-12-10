using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.HongFeiJVAccess.Entity
{
	public class DepartmentManager
	{
		public DepartmentManager() { }
		public DepartmentManager(string EXT04){
			this.EXT04 = EXT04;
		}
		public string EXT04 { get; set; }
	}
}