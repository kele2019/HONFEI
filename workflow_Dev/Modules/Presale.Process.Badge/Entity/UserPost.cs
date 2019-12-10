using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.Badge.Entity
{
	public class UserPost
	{
		public UserPost() { }
		public UserPost(string EXT03) { this.EXT03 = EXT03; }
		public string EXT03 { get; set; }
	}
}