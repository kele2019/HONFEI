using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MyLib;

namespace Presale.Process.BudgetRequestAndApproval
{
    /// <summary>
    /// LoadBudegetInfo 的摘要说明
    /// </summary>
    public class LoadBudegetInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string BudegetNo=context.Request.Form["BudegetNo"].ToString().Trim();
            string YearData = context.Request.Form["YearData"].ToString().Trim();
            string CostCenter = context.Request.Form["CostCenter"].ToString().Trim();
            string Code = "";
            if (CostCenter.Contains("50805000"))
                Code = "50805000";
            if (CostCenter.Contains("50808510"))
                Code = "50808510";
            if (CostCenter.Contains("50805500"))
                Code = "50805500";
            if (CostCenter.Contains("50801000"))
                Code = "50801000";
            if (CostCenter.Contains("50801500"))
                Code = "50801500";
            if (CostCenter.Contains("50806500"))
                Code = "50806500";
            if (CostCenter.Contains("50803000"))
                Code = "50803000";

            //context.Response.Write("Hello World");
            if (Code != "")
            {
                context.Response.Write(ActualData(Code, YearData, BudegetNo));
            }
            else
            context.Response.Write("");
        }

        private string ActualData(string Code, string YearData, string BudegetNo)
        {
            string StrHTML = "<tr class=\"TRActual\" style=\"display:none\"><td>Actual Data：</td><td></td>";
         string Strql = string.Format(@" select Amount,Period, MONTH(CAST(Period as datetime)) BMONTH,YEAR(CAST(Period as datetime)) BYEAR
  from dbo.V_BudgetActualReport where CostCenter='{0}' and AcctCode='{1}' and YEAR(CAST(Period as datetime)) ='{2}'", Code, BudegetNo, YearData);
          
            DataTable dtBudgetData = DataAccess.Instance("BizDB").ExecuteDataTable(Strql);
            BudgetReportsForDept Pagemode=new BudgetReportsForDept ();
            MonthEntity Mode=Pagemode.GetActualData(dtBudgetData);
            StrHTML += "<td class=\"money\">"  + Mode.Jan.ToString() +"</td>";
            StrHTML += "<td class=\"money\">" + Mode.Feb.ToString() + "</td>";
            StrHTML += "<td class=\"money\">" + Mode.Mar.ToString() + "</td>";
            StrHTML += "<td class=\"money\">" + Mode.Apr.ToString() + "</td>";
            StrHTML += "<td class=\"money\">" + Mode.May.ToString() + "</td>";
            StrHTML += "<td class=\"money\">" + Mode.Jun.ToString() + "</td>";
            StrHTML += "<td class=\"money\">" + Mode.Jul.ToString() + "</td>";
            StrHTML += "<td class=\"money\">" + Mode.Aug.ToString() + "</td>";
            StrHTML += "<td class=\"money\">" + Mode.Sep.ToString() + "</td>";
            StrHTML += "<td class=\"money\">" + Mode.Oct.ToString() + "</td>";
            StrHTML += "<td class=\"money\">" + Mode.Nov.ToString() + "</td>";
            StrHTML += "<td class=\"money\">" + Mode.Dec.ToString() + "</td>";
            StrHTML += "<td class=\"money\">" + Mode.SubTotal.ToString() + "</td>";
            StrHTML += "</tr>";
            return StrHTML;
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