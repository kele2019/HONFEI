using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ultimus.UWF.Report.Entity
{
	public class TravelExpenseReportEntity
	{
		public TravelExpenseReportEntity() { }
		//
		public TravelExpenseReportEntity(string PurposeOfTrip, string EmployeeName, string Employee, string CostCenterDisplay, string RMB, string PaidByCompanyRMB, string PaidByEmployeeRMB, string Rate, int? ROWID, DateTime? ItemValue, string Meal, string CurrencyMeal, string BME, string CurrencyBME, string Air, string CurrencyAir, string Lau, string CurrencyLau, string TT, string CurrencyTT, string Others, string CurrencyOther, string TB,string CurrencyTB,string Hotel,string CurrencyHotel)
		{
			this.PurposeOfTrip = PurposeOfTrip;
			this.EmployeeName = EmployeeName;
			this.Employee = Employee;
			this.CostCenterDisplay = CostCenterDisplay;
			this.RMB = RMB;
			this.Rate = Rate;
			this.PaidByCompanyRMB = PaidByCompanyRMB;
			this.PaidByEmployeeRMB = PaidByEmployeeRMB;
			this.ROWID = ROWID;
			this.ItemValue = ItemValue;
			this.Meal = Meal;
			this.CurrencyMeal = CurrencyMeal;
			this.BME = BME;
			this.CurrencyBME = CurrencyBME;
			this.CurrencyAir = CurrencyAir;
			this.Air = Air;
			this.CurrencyLau = CurrencyLau;
			this.Lau = Lau;
			this.Others = Others;
			this.CurrencyOther = CurrencyOther;
			this.TT = TT;
			this.CurrencyTT = CurrencyTT;
			this.Hotel = Hotel;
			this.CurrencyHotel = CurrencyHotel;
			this.TB = TB;
			this.CurrencyTB = CurrencyTB;
		}
		public string PurposeOfTrip { get; set; }
		public string EmployeeName { get; set; }
		public string Employee { get; set; }
		public string CostCenterDisplay { get; set; }
		public string RMB { get; set; }
		public string PaidByCompanyRMB { get; set; }
		public string PaidByEmployeeRMB { get; set; }
		public string Rate { get; set; }
		public int? ROWID { get; set; }
		public DateTime? ItemValue { get; set; }
		public string Meal { get; set; }
		public string CurrencyMeal { get; set; }
		public string BME { get; set; }
		public string CurrencyBME { get; set; }
		public string Air { get; set; }
		public string CurrencyAir { get; set; }
		public string Lau { get; set; }
		public string CurrencyLau { get; set; }
		public string TT { get; set; }
		public string TB { get; set; }
		public string CurrencyTT { get; set; }
		public string CurrencyTB { get; set; }
		public string Others { get; set; }
		public string CurrencyOther { get; set; }
		public string Hotel { get; set; }
		public string CurrencyHotel { get; set; }
	}
}