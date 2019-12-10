using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.Leave.Entity
{
	public class AnnualLeave
	{
		public AnnualLeave() { }
		public AnnualLeave(float? LeaveLastYearHourCount) { this.LeaveLastYearHourCount = LeaveLastYearHourCount; }
		public float? LeaveLastYearHourCount { get; set; }
	}
}