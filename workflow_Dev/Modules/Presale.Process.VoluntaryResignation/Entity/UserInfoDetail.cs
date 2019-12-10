using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.VoluntaryResignation.Entity
{
	public class UserInfoDetail
	{
		public UserInfoDetail()
		{ }
		public UserInfoDetail(string USERNAME, string EXT04, string IDNO, string EXT03, string EntryDate, string LOGINNAME)
		{
			this.USERNAME = USERNAME;
			this.EXT04 = EXT04;
			this.IDNO = IDNO;
			this.EXT03 = EXT03;
			this.EntryDate = EntryDate;
			this.LOGINNAME = LOGINNAME;
		}
		public string USERNAME { get; set; }
		public string EXT04 { get; set; }
		public string IDNO { get; set; }
		public string EXT03 { get; set; }
		public string EntryDate { get; set; }
		public string LOGINNAME { get; set; }
	}
}