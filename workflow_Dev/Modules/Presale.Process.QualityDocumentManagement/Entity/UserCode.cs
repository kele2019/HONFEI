using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.QualityDocumentManagement.Entity
{
	public class UserCode
	{
		public UserCode() { }
		public UserCode(string USERCODE, string DEPARTMENTNAME) 
		{ 
			this.USERCODE = USERCODE;
			this.DEPARTMENTNAME = DEPARTMENTNAME;
		}
		public string USERCODE { get;set;}
		public string DEPARTMENTNAME { get; set; }
	}
}