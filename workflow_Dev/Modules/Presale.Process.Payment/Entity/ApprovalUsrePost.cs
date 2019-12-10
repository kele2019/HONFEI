using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.Payment.Entity
{
	public class ApprovalUsrePost
	{
		public ApprovalUsrePost() { }
		public ApprovalUsrePost(string EXT03) 
		{
			this.EXT03 = EXT03;
		}
		public string EXT03 { get; set; }
	}
}