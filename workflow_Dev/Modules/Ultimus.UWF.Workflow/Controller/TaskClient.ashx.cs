using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ultimus.UWF.Workflow.Logic;
using Ultimus.UWF.Workflow.Entity;
using Ultimus.UWF.Workflow.Interface;
using MyLib;
using System.Web.SessionState;
using System.Data;

namespace Ultimus.UWF.Workflow.Controller
{
    /// <summary>
    /// TaskClient 的摘要说明
    /// </summary>
    public class TaskClient : IHttpHandler, IRequiresSessionState 
    {
        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/plain";
            string method = context.Request["method"];
            string user = context.Request["user"];
            string taskId = context.Request["taskId"];
            string stepLabel = context.Request["stepLabel"];
            string processName = context.Request["processName"];
            string incident = context.Request["incident"];
            string sessionid =ConvertUtil.ToString( DataAccess.Instance("BizDB").ExecuteScalar("select value from COM_USERSETTINGS where name='" + user + "' and SETTINGTYPE='SessionID'"));
            ITask task = ServiceContainer.Instance().GetService<ITask>();
            int success = 0;
            switch (method.Trim())
            {
                case "LogoutUser":
                    task.LogoutUser(sessionid);
                    break; 
                case "ReturnTask":
                    //删除会签
                     string strSql = string.Format("delete from tasks  where taskid<>'{0}' and steplabel='{1}' and processname='{2}' and incident={3} ",
                        taskId, stepLabel, processName, incident);
                    DataAccess.Instance("UltDB").ExecuteNonQuery(strSql);
                    strSql = string.Format("update tasks set recipienttype=0  where    steplabel='{1}' and processname='{2}' and incident={3} ",
                        taskId, stepLabel, processName, incident);
                    DataAccess.Instance("UltDB").ExecuteNonQuery(strSql);
                    //判断协办
                    string strSQL = "SELECT COUNT(1) FROM TASKS WITH (nolock) where PROCESSNAME='协办流程' AND STATUS=1 AND CHARINDEX('>" + taskId + "</AssistTaskID>',CONVERT(nvarchar(max),SCHEMADATA),1) != 0";                
                    DataTable dt = new DataTable();
                    dt = DataAccess.Instance("UltDB").ExecuteDataTable(strSQL);
                    int oAssistStatus =  ConvertUtil.ToInt32(dt.Rows[0][0]);
                    dt = null;
                    success = oAssistStatus;
                    break;
            }
            context.Response.Write( success );
            context.Response.Flush();
            context.Response.End();
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}