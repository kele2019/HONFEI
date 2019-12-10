using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.ProcessPerformance.Entity
{
	public class DepartmentEntity
	{
		public DepartmentEntity() { }
		public DepartmentEntity(string DicCode,string DicText,string DicValue,string Comments) { }
		public string DicCode { get; set; }
		public string DicText { get; set; }
		public string DicValue { get; set; }
		public string Comments { get; set; }

	}
}