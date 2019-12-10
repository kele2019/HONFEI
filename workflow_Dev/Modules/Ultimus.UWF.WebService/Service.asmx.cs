using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Ultimus.UWF.WebService.Req;
using Ultimus.UWF.WebService.Rep;
using Ultimus.UWF.WebService.SSOCore;

namespace Ultimus.UWF.WebService
{
    /// <summary>
    /// Service 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {
        [WebMethod]
        public RepResult Ping()
        {
            RepResult<DateTime> result = new RepResult<DateTime>();
            result.Msg = "Server Time.";
            result.Data = DateTime.Now;
            return result;
        }

        [WebMethod]
        public RepResult AddSSOUserTicket(ReqUserTicket ticketModel)
        {
            RepResult result = new RepResult();
            try
            {
                #region 数据验证
                string errorMsg = ticketModel.Valid();
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    throw new Exception(errorMsg);
                }
                #endregion

                bool success = SsoBO.Instance.AddIfTicketNotExits(ticketModel);
                if (!success)
                {
                    throw new Exception("存在尚未失效且相同的Ticket！");
                }
            }
            catch (Exception err)
            {
                result.Code = -1;
                result.Msg = err.Message;
            }
            return result;
        }

        [WebMethod]
        public RepResult<string> GetUserAccountByTicket(string ticket)
        {
            RepResult<string> result = new RepResult<string>();
            try
            {
                if (string.IsNullOrEmpty(ticket))
                {
                    throw new Exception("参数不能为空！");
                }

                string userAccount = "";
                if (!SsoBO.Instance.GetUserAccountByTicket(ticket, out userAccount))
                {
                    throw new Exception("没有找到Ticket所关联的用户，或Ticket已过期！");
                }
                result.Data = userAccount;
            }
            catch (Exception err)
            {
                result.Code = -1;
                result.Msg = err.Message;
            }
            return result;
        }
    }
}
