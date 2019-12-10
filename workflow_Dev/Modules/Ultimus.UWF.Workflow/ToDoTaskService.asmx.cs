using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Ultimus.UWF.Workflow.Interface;
using MyLib;
using Ultimus.UWF.Common.Entity;

namespace Ultimus.UWF.Workflow
{
    /// <summary>
    /// ToDoTaskService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class ToDoTaskService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public string TaskCount(string UserAccount)
        {
            //ITask _task = ServiceContainer.Instance().GetService<ITask>();
            //List<ParameterEntity> table = new List<ParameterEntity>();
            //return _task.GetMyApprovalCount(UserAccount, "", table);
            try
            {
                string strsql = @" SELECT count(1) FROM TASKS a with(nolock) INNER JOIN INCIDENTS b  with(nolock) ON a.PROCESSNAME = b.PROCESSNAME AND a.INCIDENT = b.INCIDENT
      WHERE a.STATUS=1 AND a.ASSIGNEDTOUSER='" + UserAccount.Replace('\\', '/') + "'";
                return (DataAccess.Instance("UltDB").ExecuteScalar(strsql).ToString()).ToString();
            }
            catch (Exception ex)
            {

                return "0";
            }
        }
    }
}
