using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyLib;
using Presale.Process.Leave.Entity;

namespace Presale.Process.Leave
{
	/// <summary>
	/// checkSumAnnualLeave 的摘要说明
	/// </summary>
	public class checkSumAnnualLeave : IHttpHandler
	{

		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "text/plain";
            string StartDate = context.Request.Form["StartDate"];
            string EndDate = context.Request.Form["EndDate"];
            string Starthour = context.Request.Form["Starthour"];
            string Endhour = context.Request.Form["Endhour"];

            //string sql = "select LevalYearHourCount from dbo.COM_LevalManager where UserAccount = '" + applierLogin + "'";
            //AnnualLeave leaveDetail = DataAccess.Instance("BizDB").ExecuteEntity<AnnualLeave>(sql);
            //context.Response.Write(leaveDetail.LeaveLastYearHourCount.ToString());
            decimal countdays = CalculateHolidayData(StartDate, EndDate, Starthour, Endhour, "", "");
            context.Response.Write(countdays.ToString());

		}
        public   decimal CalculateHolidayData(string StartDate, string EndDate, string Starthour, string Endhour, string Startminutes, string Endtminutes)
        {
            var dt = DataAccess.Instance("BizDB").ExecuteList<DictionryEntity>(" select * from  COM_DICTIONRY where Type='HolidayType'");
            DateTime start = Convert.ToDateTime(StartDate);
            DateTime end = Convert.ToDateTime(EndDate);
            TimeSpan ts = end - start;
            decimal countdays = 0;
            for (int i = 0; i < ts.Days - 1; i++)
            {
                var hoildydate = dt.FirstOrDefault(o => o.DicText == start.AddDays(i + 1).ToString("yyyy-MM-dd"));
                if (hoildydate != null)
                {
                    if (hoildydate.DicValue != "Holiday" || hoildydate.DicValue == "Work")
                        countdays++;
                }
                else
                {
                    if (start.AddDays(i + 1).DayOfWeek != DayOfWeek.Saturday && start.AddDays(i + 1).DayOfWeek != DayOfWeek.Sunday)
                    {
                        countdays++;
                    }
                }
            }
            var hoildyenddate = dt.FirstOrDefault(o => o.DicText == EndDate);
            bool endiscalculate = true;
            var hoildystartdate = dt.FirstOrDefault(o => o.DicText == StartDate);
            bool startiscalculate = true;
            string EDateType = "Holiday";
            if (hoildyenddate != null && hoildyenddate.DicValue == "Work")
            {
                EDateType = "Work";
            }
            if (((Convert.ToDateTime(EndDate).DayOfWeek == DayOfWeek.Saturday || Convert.ToDateTime(EndDate).DayOfWeek == DayOfWeek.Sunday) && EDateType == "Holiday") || (hoildyenddate != null && hoildyenddate.DicValue == "Holiday"))
            {
                endiscalculate = false;
            }
         
            string DateType = "Holiday";
            if (hoildystartdate != null && hoildystartdate.DicValue == "Work")
            {
                DateType = "Work";
            }

            if (((Convert.ToDateTime(StartDate).DayOfWeek == DayOfWeek.Saturday || Convert.ToDateTime(StartDate).DayOfWeek == DayOfWeek.Sunday) && DateType == "Holiday") || (hoildystartdate != null && hoildystartdate.DicValue == "Holiday"))
            {
                startiscalculate = false;
            }
            if (endiscalculate || startiscalculate)
            {
                if (StartDate != EndDate)
                {
                    if (startiscalculate)
                    {
                        countdays += Convert.ToDecimal((17 - Convert.ToInt32(Starthour)) * 0.125);
                        if (Convert.ToInt32(Starthour) <= 12)//中午吃饭12点排除
                        {
                            countdays += Convert.ToDecimal(-0.125);
                        }
                    }
                    if (endiscalculate)
                    {
                        countdays += Convert.ToDecimal((Convert.ToInt32(Endhour) - 8) * 0.125);
                        if (Convert.ToInt32(Endhour) > 12)//中午吃饭12点排除
                        {
                            countdays += Convert.ToDecimal(-0.125);
                        }
                    }
                }
                else//同一天
                {
                    countdays += Convert.ToDecimal((Convert.ToInt32(Endhour) - Convert.ToInt32(Starthour)) * 0.125);
                    if (Convert.ToInt32(Endhour) > 12 && Convert.ToInt32(Starthour) <= 12)//中午吃饭12点排除
                    {
                        countdays += Convert.ToDecimal(-0.125);
                    }
                }
            }
            return countdays;
        }
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}