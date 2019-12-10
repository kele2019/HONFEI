using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ultimus.UWF.WebService.Req
{
    public class ReqUserTicket
    {
        public string Ticket { get; set; }
        public string UserAccount { get; set; }
        public DateTime ExpiredDate { get; set; }

        public string Valid()
        {
            string msg = "";
            if (string.IsNullOrEmpty(Ticket))
            {
                msg = "Ticket不能为空！";
            }
            if (string.IsNullOrEmpty(UserAccount))
            {
                msg = "UserAccount不能为空！";
            }
            if (UserAccount.Contains('@') || UserAccount.Contains('\\') || UserAccount.Contains('/'))
            {
                msg = "UserAccount不能包含@ / \\符号！";
            }
            //if (ExpiredDate <= DateTime.Now)
            //{
            //    msg = "ExpiredDate不能早于当前时间！";
            //}
            //TimeSpan ts = DateTime.Now - ExpiredDate;
            //if (ts.TotalSeconds > 30)
            //{
            //    msg = "连接时间不能大于" + ts.TotalSeconds + "秒，这样不够安全！" + DateTime.Now.ToString() + "ExpiredDate" + ExpiredDate.ToString();
            //}
            return msg;
        }
    }

    public static class ReqUserTicketModelExtMethod
    {
        public static string Valid(this ReqUserTicket me)
        {
            if (me == null)
            {
                return "请求参数不能为空！";
            }
            return me.Valid();
        }
    }
}