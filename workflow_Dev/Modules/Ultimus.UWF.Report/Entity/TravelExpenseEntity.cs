using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ultimus.UWF.Report.Entity
{
	public class TravelExpenseEntity
	{
		public TravelExpenseEntity() { }
		//, 
		public TravelExpenseEntity(string Project,string PurposeOfTrip, string EmployeeName, string Employee, string CostCenterDisplay, string RMB, string PaidByCompanyRMB, string PaidByEmployeeRMB, string Rate)
		{
			this.PurposeOfTrip = PurposeOfTrip;
			this.EmployeeName = EmployeeName;
			this.Employee = Employee;
			this.CostCenterDisplay = CostCenterDisplay;
			this.RMB = RMB;
			this.Rate = Rate;
			this.PaidByCompanyRMB = PaidByCompanyRMB;
			this.PaidByEmployeeRMB = PaidByEmployeeRMB;
			this.Project = Project;
			//this.ROWID = ROWID;
			//this.ItemValue = ItemValue;
			//this.Meal = Meal;
			//this.CurrencyMeal = CurrencyMeal;
			//this.BME = BME;
			//this.CurrencyBME = CurrencyBME;
			//this.CurrencyAir = CurrencyAir;
			//this.Air = Air;
			//this.CurrencyLau = CurrencyLau;
			//this.Lau = Lau;
			//this.Others = Others;
			//this.CurrencyOther = CurrencyOther;
			//this.TT = TT;
			//this.CurrencyTT = CurrencyTT;
		}
		public string Project { get; set; }
		public string PurposeOfTrip { get; set; }
		public string EmployeeName { get; set; }
		public string Employee { get; set; }
		public string CostCenterDisplay { get; set; }
		public string RMB { get; set; }
		public string PaidByCompanyRMB { get; set; }
		public string PaidByEmployeeRMB { get; set; }
		public string Rate { get; set; }
		
	}
}