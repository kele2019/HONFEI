using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ultimus.UWF.WebService.Req;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Ultimus.UWF.WebService.SSOCore
{
    public class SsoBO
    {
        static SsoBO _Instance = new SsoBO();
        public static SsoBO Instance { get { return _Instance; } }

        private static string ConnectionString { get { return ConfigurationManager.ConnectionStrings["SSODB"].ConnectionString; } }

        /// <summary>
        /// 如果Ticket存在重复记录，则不会插入，并且会返回false
        /// </summary>
        /// <returns></returns>
        public bool AddIfTicketNotExits(ReqUserTicket ticket)
        {
            string sql = @"
if (select count(*) from Tickets where Ticket=@Ticket and ExpiredDate>@Now) = 0
begin
	insert into Tickets(Ticket, UserAccount, ExpiredDate, CreatedDate)
        values(@Ticket, @UserAccount, @ExpiredDate, @Now)
end
";
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@Ticket", ticket.Ticket));
            sqlParams.Add(new SqlParameter("@UserAccount", ticket.UserAccount));
            sqlParams.Add(new SqlParameter("@ExpiredDate", ticket.ExpiredDate));
            sqlParams.Add(new SqlParameter("@Now", DateTime.Now));

            int count = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql, sqlParams.ToArray());
            return count > 0;
        }

        public bool GetUserAccountByTicket(string ticket, out string userAccount)
        {
            userAccount = "";

            string sql =@"
select top 1 UserAccount from Tickets where Ticket=@Ticket and ExpiredDate<=@Now
";
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@Ticket", ticket));
            sqlParams.Add(new SqlParameter("@Now", DateTime.Now));

            object obj = SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sql, sqlParams.ToArray());
            if (obj == null)
                return false;
            userAccount = (string)obj;
            return true;
        }
    }
}