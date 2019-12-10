using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.PurchaseOrder.Entity
{
	public class UserDeptManager
	{
		public UserDeptManager() { }
		public UserDeptManager(string userinfo) 
		{
			this.userinfo = userinfo;
		}
		public string userinfo { get; set; }
	}
}