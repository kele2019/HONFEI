using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ultimus.UWF.Report.Entity
{
	public class TravelExpenseDetailEntity
	{
		public TravelExpenseDetailEntity() { }
		public TravelExpenseDetailEntity(int? ROWID, string TravelFrom, DateTime? ItemValue, string Meal, string CurrencyMeal, string payMeal, string BME, string CurrencyBME, string payBME, string Air, string CurrencyAir, string payAir, string Hotel, string CurrencyHotel, string payHotel, string TB, string CurrencyTB, string payTB, string Lau, string CurrencyLau, string payLau, string TT, string CurrencyTT, string payTT, string Others, string CurrencyOther, string payOthers)
		{
			this.ROWID = ROWID;
			this.TravelFrom = TravelFrom;
			this.ItemValue = ItemValue;
			this.Meal = Meal;
			this.CurrencyMeal = CurrencyMeal;
			this.payMeal = payMeal;
			this.BME = BME;
			this.CurrencyBME = CurrencyBME;
			this.payBME = payBME;
			this.Air = Air;
			this.CurrencyAir = CurrencyAir;
			this.payAir = payAir;
			this.Hotel = Hotel;
			this.CurrencyHotel = CurrencyHotel;
			this.payHotel = payHotel;
			this.TB = TB;
			this.CurrencyTB = CurrencyTB;
			this.payTB = payTB;
			this.Lau = Lau;
			this.CurrencyLau = CurrencyLau;
			this.payLau = payLau;
			this.TT = TT;
			this.CurrencyTT = CurrencyTT;
			this.payTT = payTT;
			this.Others = Others;
			this.CurrencyOther = CurrencyOther;
			this.payOthers = payOthers;
		}
		public int? ROWID { get; set; }
		public string TravelFrom { get; set; }
		public DateTime? ItemValue { get; set; }
		public string Meal { get; set; }
		public string CurrencyMeal { get; set; }
		public string payMeal { get; set; }
		public string BME { get; set; }
		public string CurrencyBME { get; set; }
		public string payBME { get; set; }
		public string Air { get; set; }
		public string CurrencyAir { get; set; }
		public string payAir { get; set; }
		public string Hotel { get; set; }
		public string CurrencyHotel { get; set; }
		public string payHotel { get; set; }
		public string TB { get; set; }
		public string CurrencyTB { get; set; }
		public string payTB { get; set; }
		public string Lau { get; set; }
		public string CurrencyLau { get; set; }
		public string payLau { get; set; }
		public string TT { get; set; }
		public string CurrencyTT { get; set; }
		public string payTT { get; set; }
		public string Others { get; set; }
		public string CurrencyOther { get; set; }
		public string payOthers { get; set; }
	}
}