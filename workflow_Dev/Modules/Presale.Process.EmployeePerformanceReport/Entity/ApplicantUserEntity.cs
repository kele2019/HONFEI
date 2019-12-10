using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.EmployeePerformanceReport.Entity
{
	public class ApplicantUserEntity
	{
		public ApplicantUserEntity() { }
		public ApplicantUserEntity(string LOGINNAME,string EXT04) { }
		public string LOGINNAME { get; set; }
		public string EXT04 { get; set; }
	}
}