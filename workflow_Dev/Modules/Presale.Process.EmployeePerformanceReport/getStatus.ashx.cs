using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyLib;
using Presale.Process.Common;
using Presale.Process.EmployeePerformanceReport.Entity;
using Ultimus.UWF.Form.ProcessControl;

namespace Presale.Process.EmployeePerformanceReport
{
	/// <summary>
	/// getStatus 的摘要说明
	/// </summary>
	public class getStatus : IHttpHandler
	{

		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "text/plain";
			string year = context.Request.Form["year"];
			string applierLogin = context.Request.Form["applierLogin"];
			string reportType = context.Request.Form["reportType"];
			string sql = "select EPStatus from PROC_EmployeePerformance where Year = '" + year + "' and ApplicantUserLogin = '" + applierLogin + "' and ReportType = '" + reportType + "'";
			List<StatusEntity> lists = DataAccess.Instance("BizDB").ExecuteList<StatusEntity>(sql);
			string value = "0";
			string value1 = "0";
			string value2 = "0";
			foreach (StatusEntity status in lists)
			{
				if (status.EPStatus == "1")
				{
					value1 = "1";
				}
				if (status.EPStatus == "2")
				{
					value2 = "2";
				}
			}
			if (value1 == "1") { value = value1; }
			if (value2 == "2") { value = value2; }
			context.Response.Write(value);
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