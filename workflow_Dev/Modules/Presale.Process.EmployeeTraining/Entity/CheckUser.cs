using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.EmployeeTraining.Entity
{
	public class CheckUser
	{
		public CheckUser() { }
        public CheckUser(string userinfo, string uSERID, string eXT04)
		{
			this.userinfo = userinfo;
            this.USERID=uSERID;
            this.EXT04 = eXT04;
		}
		public string userinfo { get; set; }
        public string USERID { get; set; }
        public string EXT04 { get; set; }
	}
}