
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ultimus.UWF.Report.Entity
{
	public class DocumentEntity
	{
		public DocumentEntity() { }
        public DocumentEntity(string FollowUPAction, string documentName, string documentNumber, string documentOwner, string OperMode, string DOCDescription, string MajorChange, string deptMan, string deptQM, string deptEng, string deptPM, string deptOP, string deptHR, string deptFin, string deptIT, string deptPUR, string deptAdmin, string gM, string aPPLICANTACCOUNT)
		{
			this.documentName = documentName;
			this.documentNumber = documentNumber;
			this.documentOwner = documentOwner;
			this.OperMode = OperMode;
			this.MajorChange = MajorChange;
			this.DOCDescription = DOCDescription;
			this.deptAdmin = deptAdmin;
			this.deptEng = deptEng;
			this.deptFin = deptFin;
			this.deptHR = deptHR;
			this.deptIT = deptIT;
			this.deptMan = deptMan;
			this.deptOP = deptOP;
			this.deptPM = deptPM;
			this.deptPUR = deptPUR;
			this.deptQM = deptQM;
			this.FollowUPAction = FollowUPAction;
            this.deptGM = gM;
            this.APPLICANTACCOUNT = aPPLICANTACCOUNT;
		}
		public string FollowUPAction { get; set; }
		public string documentName { get; set; }
		public string documentNumber { get; set; }
		public string documentOwner { get; set; }
		public string OperMode { get; set; }
		public string DOCDescription { get; set; }
		public string MajorChange { get; set; }
		public string deptMan { get; set; }
		public string deptQM { get; set; }
		public string deptEng { get; set; }
		public string deptPM { get; set; }
		public string deptOP { get; set; }
		public string deptFin { get; set; }
		public string deptHR { get; set; }
		public string deptIT { get; set; }
		public string deptPUR { get; set; }
		public string deptAdmin { get; set; }
        public string deptGM { get; set; }
        public string APPLICANTACCOUNT { get; set; }
	}
}