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
			string applierLogin = context.Request.Form["applierLogin"];
			string sql = "select LevalYearHourCount from dbo.COM_LevalManager where UserAccount = '" + applierLogin + "'";
			AnnualLeave leaveDetail = DataAccess.Instance("BizDB").ExecuteEntity<AnnualLeave>(sql);
			context.Response.Write(leaveDetail.LeaveLastYearHourCount.ToString());
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