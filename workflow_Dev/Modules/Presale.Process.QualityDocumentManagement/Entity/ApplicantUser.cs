using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.QualityDocumentManagement.Entity
{
	public class ApplicantUser
	{
		public ApplicantUser() { }
		public ApplicantUser(string LOGINNAME,string EXT04,string EXT02) {
			this.EXT04 = EXT04;
			this.LOGINNAME = LOGINNAME;
			this.EXT02 = EXT02;
		}
		public string LOGINNAME { get; set; }
		public string EXT04 { get; set; }
		public string EXT02 { get; set; }
	}
}