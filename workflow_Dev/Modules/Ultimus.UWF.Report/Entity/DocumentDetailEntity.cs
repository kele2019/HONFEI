
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ultimus.UWF.Report.Entity
{
	public class DocumentDetailEntity
	{
		public DocumentDetailEntity() { }
        public DocumentDetailEntity(DateTime? CREATEDATE, string COMMENTS, string eXT04)
		{
			this.COMMENTS = COMMENTS;
			this.CREATEDATE = CREATEDATE;
            this.EXT04 = eXT04;
		}
		public string COMMENTS { get; set; }
		public DateTime? CREATEDATE { get; set; }
        public string EXT04 { get; set; }
	}
}