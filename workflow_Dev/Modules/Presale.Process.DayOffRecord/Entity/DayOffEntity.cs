using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.DayOffRecord.Entity
{
	public class DayOffEntity
	{
		public DayOffEntity() { }
		public DayOffEntity(float totalDayOffHourCount) { }
		public float? totalDayOffHourCount { get; set; }
	}
}