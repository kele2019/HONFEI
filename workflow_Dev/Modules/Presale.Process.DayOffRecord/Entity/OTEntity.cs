using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.DayOffRecord.Entity
{
	public class OTEntity
	{
		public OTEntity() { }
		public OTEntity(float? totalOTHourCount) { this.totalOTHourCount = totalOTHourCount; }
		public float? totalOTHourCount { get; set; }
	}
}