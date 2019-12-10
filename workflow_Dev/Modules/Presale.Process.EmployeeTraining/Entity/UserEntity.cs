using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.EmployeeTraining.Entity
{
	public class UserEntity
	{
		public UserEntity() { }
		public UserEntity(string EXT04, string LOGINNAME) {
			this.EXT04 = EXT04;
			this.LOGINNAME = LOGINNAME;
		}
		public string EXT04 { get; set; }
		public string LOGINNAME { get; set; }
	}
}