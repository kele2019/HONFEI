using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.WebsiteAccess.Entity
{
	public class UserName
	{
		public UserName() { }
		public UserName(string Manager) { this.Manager = Manager; }
		public string Manager { get; set; }
	}
}