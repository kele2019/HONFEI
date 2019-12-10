using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.TravelExpenseReport.Entity
{
	public class RateEntity
	{
		public RateEntity() { }
		public RateEntity(string DicValue)
		{
			this.DicValue = DicValue;
		}
		public string DicValue { get; set; }
	}
}