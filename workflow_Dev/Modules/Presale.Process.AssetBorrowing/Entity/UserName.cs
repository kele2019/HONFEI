using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.AssetBorrowing.Entity
{
	public class UserName
	{
		public UserName() { }
		public UserName(string EXT04) { this.EXT04 = EXT04; }
		public string EXT04 { get; set; }
	}
}