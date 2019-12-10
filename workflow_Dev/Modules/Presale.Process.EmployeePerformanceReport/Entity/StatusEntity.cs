using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.EmployeePerformanceReport.Entity
{
	public class StatusEntity
	{
		public StatusEntity() { }
		public StatusEntity(string EPStatus) { this.EPStatus = EPStatus; }
		public string EPStatus { get; set; }
	}
}