using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.AssetBorrowing.Entity
{
	public class Manager
	{
		public Manager() { }
		public Manager(string DepartmentManager) { this.DepartmentManager = DepartmentManager; }
		public string DepartmentManager { get; set; }
	}
}