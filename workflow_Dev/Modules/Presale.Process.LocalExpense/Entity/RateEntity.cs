using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.LocalExpense.Entity
{
	public class RateEntity
	{
		public RateEntity() { }
		public RateEntity(string DicText, string DicValue)
		{
			this.DicText = DicText;
			this.DicValue = DicValue;
		}
		public string DicText { get; set; }
		public string DicValue { get; set; }
	}
}