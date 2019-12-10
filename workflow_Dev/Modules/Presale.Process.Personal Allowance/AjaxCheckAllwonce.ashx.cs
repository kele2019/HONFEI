using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MyLib;
namespace Presale.Process.Personal_Allowance
{
    /// <summary>
    /// AjaxCheckAllwonce 的摘要说明
    /// </summary>
    public class AjaxCheckAllwonce : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string username = context.Request.Form["username"];
            string SDate = context.Request.Form["SDate"];
            string flage = context.Request.Form["flage"];
            string strsql = "select B.HouseRental,Phone,Transportation from (select FORMID from PROC_PersonalAllownce where APPLICANTACCOUNT='"+username+"' and  STATUS<>3) A";
            strsql += " left join PROC_PersonalAllownce_DT B On A.FORMID=B.FORMID where  year(B.PaymentDate)='" + SDate.Split('/')[0] + "' and MONTH(B.PaymentDate)='" + SDate.Split('/')[1] + "'";
            DataTable dtAllowance = DataAccess.Instance("BizDB").ExecuteDataTable(strsql);
            string ReturnMsg = "";
            if (dtAllowance.Rows.Count > 0)
            {
                foreach (DataRow item in dtAllowance.Rows)
	            {
		        if (flage == "1")
                {
                    string Transportation = item["Transportation"].ToString() == "" ? "0" : item["Transportation"].ToString();

                    if (float.Parse(Transportation) > 0)
                    {
                        ReturnMsg = "当前月交通费已经报销\nCurrent month Transportation  Repeater ";
                    }
                }
                if (flage == "2")
                {
                    string HouseRental = item["HouseRental"].ToString() == "" ? "0" : item["HouseRental"].ToString();
                    if (float.Parse(HouseRental) > 0)
                    {
                        ReturnMsg = "当前月房租费已经报销\n Current month HouseRental  Repeater ";
                    }
                }
                if (flage == "3")
                {
                    string phoneAmount = item["Phone"].ToString() == "" ? "0" : item["Phone"].ToString();
                    if (float.Parse(phoneAmount) > 0)
                    {
                        ReturnMsg = "当前月手机费已经报销\nCurrent month Phone  Repeater ";
                    }
                }
	            }
            }
            context.Response.Write(ReturnMsg);
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