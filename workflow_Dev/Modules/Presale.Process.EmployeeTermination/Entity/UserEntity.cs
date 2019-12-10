using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.EmployeeTermination.Entity
{
	public class UserEntity
	{
		string value;
		string str = "Voluntary Resignation Application Form";
		public UserEntity() { }
		public UserEntity(string Applicant, string Department,DateTime ResignationDate,string FORMID) {
			this.FORMID = FORMID;
			this.Applicant = Applicant;
			this.Department = Department;
			this.ResignationDate = ResignationDate;
		}
		public string FORMID { get; set; }
		public string Applicant { get; set; }
		public string Department { get; set; }
		public DateTime ResignationDate { get; set; }

		public string Value
		{
			get { return this.value; }
			set 
			{
				this.value = this.Applicant + "_" + this.Department + "_" + this.ResignationDate.ToString("yyyyMMdd") + "_" + str;
			}
		}
	}
}