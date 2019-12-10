using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.TravelExpenseReport.Entity
{
	public class ApprovalUserPost
	{
		public ApprovalUserPost() { }
		public ApprovalUserPost(string EXT03) 
		{
			this.EXT03 = EXT03;
		}
		public string EXT03 { get; set; }
	}
}