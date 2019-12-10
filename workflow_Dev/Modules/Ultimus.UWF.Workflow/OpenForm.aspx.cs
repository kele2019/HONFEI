using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Workflow.Interface;
using MyLib;
using Ultimus.UWF.Common.Logic;
using Ultimus.UWF.Workflow.Logic;
using Ultimus.UWF.Workflow.Entity;

namespace Ultimus.UWF.Workflow
{
    public partial class OpenForm : System.Web.UI.Page
    {
        ITask _task = ServiceContainer.Instance().GetService<ITask>();
        protected void Page_Load(object sender, EventArgs e)
        {
            string taskID = Request.QueryString["TaskID"];
            string taskUser = Request.QueryString["TaskUser"];
            string type = Request.QueryString["Type"];
            string processName = Request.QueryString["ProcessName"];
            string incident = Request.QueryString["Incident"];
            string stepName = Request.QueryString["StepName"];
            string formId = Request.QueryString["FormID"];
            string serverName = Request.QueryString["ServerName"];
            string userName = "";
            if (string.IsNullOrEmpty(type))
            {
                type = "mytask";
            }
            if (!string.IsNullOrEmpty(serverName) && serverName.StartsWith("UltimusV7"))
            {
                ServerEntity s = ServerLogic.GetServerEntity(serverName);
                string user=ServerLogic.GetLoginName(serverName,SessionLogic.GetLoginName());
                string ss = s.WEBSERVICEURL.Replace("Service/EIKService.asmx", "OpenForm.aspx");
                ss += "?TaskId="+taskID+"&ServerName="+serverName+"&TaskUser="+user;
                Response.Redirect(ss);
                return;
            }
            string url = "";
            switch (type.ToUpper().Trim())
            {
                case "DRAFT"://从草稿打开
                    userName = SessionLogic.GetLoginName();
                    if (processName != null && processName.Trim() != "")
                    {
                        string strSql = "select c.initiateid from initiate c where c.processname = '" + processName + "'";
                        taskID = ConvertUtil.ToString(DataAccess.Instance("UltDB").ExecuteScalar(strSql)).Trim();
                    }
                    string page = StepSettingsLogic.GetDraftPage(processName);
                    if (HttpContext.Current.Request.Url.Port == 443)
                    {
                        url = "https://" + HttpContext.Current.Request.Url.Host + "/" + page + "?ProcessName=" + processName + "&StepName=" + stepName + "&Incident="
                           + incident + "&TaskID=" + taskID + "&UserName=" + HttpContext.Current.Server.UrlEncode(userName) + "&Type=" + type;
                    }
                    else
                    {
                        url = "http://" + HttpContext.Current.Request.Url.Host + ":"
                                + HttpContext.Current.Request.Url.Port + "/" + page + "?ProcessName=" + processName + "&StepName=" + stepName + "&Incident="
                               + incident + "&TaskID=" + taskID + "&UserName=" + HttpContext.Current.Server.UrlEncode(userName) + "&Type=" + type;
                    
                    }
                    break;
                case "EMAIL": //从邮件打开 例如：http://10.0.0.1/Modules/Workflow/OpenForm.aspx?Type=Email&ProcessName=%e5%8f%91%e7%a5%a8%e7%94%b3%e8%af%b7&Incident=14
                    if (!string.IsNullOrEmpty(taskUser))
                    {
                        userName = taskUser.Replace("/", "\\");
                        SessionLogic.LogOut();
                        SessionLogic.Login(userName);
                    }
                    else
                    {
                        userName = SessionLogic.GetLoginName();
                    }
					taskID = _task.GetViewTaskId("", ConvertUtil.ToInt32(incident), userName.Replace("\\", "/"));
					if (string.IsNullOrEmpty(taskID))
					{
					    throw new Exception("Current user have no rights to view it!!");
					}
                    url = _task.GetTaskUrl(taskID, type, userName.Replace("\\", "/"));
                    break;
                case "VIEW":
                    string sid=ConvertUtil.ToString( Session["Ult_SessionId"]);

                    object obj =null;
                    if (string.IsNullOrEmpty(taskID))
                    {
                        obj = DataAccess.Instance("UltDB").ExecuteScalar("select TASKID from TASKS with(nolock) where   ProcessName='" + processName + "' and incident="+incident+" and STATUS=3 and RECIPIENT not like '%flobot%' order by endtime desc");
                    }
                    else
                    {
                        obj = DataAccess.Instance("UltDB").ExecuteScalar("select TASKID from TASKS with(nolock) where   TASKID='" + taskID + "' and STATUS=3 and RECIPIENT not like '%flobot%'");
                    }
                    string str = ConvertUtil.ToString(obj).Trim();
                    if (string.IsNullOrEmpty(str))
                    {
                        url = _task.GetTaskUrl(taskID, type + sid, userName.Replace("\\", "/"));
                    }
                    else
                    {
                        url = _task.GetTaskUrl(str, type + sid, userName.Replace("\\", "/"));
                        taskID = str;
                    }
                    break;
                default:
                    userName = SessionLogic.GetLoginName();
                    if (string.IsNullOrEmpty(taskID)) //从报表打开
                    {
                        TaskEntity task= _task.GetTaskEntityByName(processName, ConvertUtil.ToInt32(incident),  userName.Replace("\\", "/"));
                        url = _task.GetTaskUrl(task.TASKID, type, userName.Replace("\\", "/"));
                        taskID = task.TASKID;
                    }
                    else //从任务列表打开
                    {
                        url = _task.GetTaskUrl(taskID, type, userName.Replace("\\", "/"));
                    }
                    break;
            }
            if (!string.IsNullOrEmpty(formId))
            {
                url += "&FORMID=" + formId;
            }
            if (!string.IsNullOrEmpty(serverName) && url.IndexOf("FormAccess")<=0)
            {
                url += "&ServerName=" + serverName;
            }
            if (!string.IsNullOrEmpty(serverName) && serverName.StartsWith("UltimusV7")) //如果是V7表单那么，打开标准表单时给Cookie赋值
            {
                string loginName= ServerLogic.GetLoginName(serverName, SessionLogic.GetLoginName().Replace("\\","/"));
                HttpContext.Current.Response.AddHeader("Ultimus Workflow1", "Ultimus Workflow");
                HttpContext.Current.Response.Cookies["TaskID"].Value = taskID;
                HttpContext.Current.Response.Cookies["TaskID"].Path = @"/";
                HttpContext.Current.Response.Cookies["UserID"].Value = loginName;
                HttpContext.Current.Response.Cookies["UserID"].Path = @"/";
            }
            TaskEntity aa=_task.GetTaskEntity(taskID);
            if (aa!=null && aa.PROCESSNAME!=null && (aa.PROCESSNAME.Trim().ToUpper() == "协办流程" || aa.PROCESSNAME.Trim().ToUpper().IndexOf("审批")>=0))
            {
                string[] sz =aa.SUMMARY.Split(',');
                string str = sz[sz.Length - 1];
                string tid=str.TrimEnd(']');
                if (aa.PROCESSNAME.Trim().ToUpper().IndexOf("审批") >= 0) //为子流程
                {
                    TaskEntity task = _task.GetTaskEntity(taskID);
                    object obj = DataAccess.Instance("UltDB").ExecuteScalar("select PTASKID from SUBPROCS where CNAME='" + task.PROCESSNAME + "' and CINCIDENT=" + task.INCIDENT);
                    tid = ConvertUtil.ToString(obj);
                    url = _task.GetTaskUrl(tid, type, userName.Replace("\\", "/"));
                    switch (task.PROCESSNAME.Trim())
                    {
                        case "专项负责人审批":
                            url = url.Replace("Approval", "ZXApproval");
                            break;
                        case "归口负责人审批":
                            url = url.Replace("Approval", "GKApproval");
                            break;
                        case "其他负责人审批":
                            url = url.Replace("Approval", "QTApproval");
                            break;
                    }
                }
                else
                {
                    url = _task.GetTaskUrl(tid, type, userName.Replace("\\", "/"));
                }
                url = url + "&xieban=1&xiebantaskid="+taskID;
            }
            if (aa != null && aa.PROCESSNAME != null && aa.PROCESSNAME.Trim().ToUpper() == "抄送流程")
            {
                string[] sz = aa.SUMMARY.Split(',');
                string str = sz[sz.Length - 1];
                url = _task.GetTaskUrl(str.TrimEnd(']'), type, userName.Replace("\\", "/"));
                url = url + "&copy=1";
                try
                {
                    if (type == "mytask")
                    {
                        //如果打开抄送流程，那么系统自动提交任务
                        TaskEntity entity = new TaskEntity();
                        entity.ASSIGNEDTOUSER = userName.Replace("\\", "/");
                        entity.TASKID = taskID;
                        entity.SUMMARY = "";
                        _task.SubmitTask(entity);
                    }
                }
                catch
                {
                }
            }
            string[] aaa = url.Split('&');
            foreach (string str in aaa) //打开标准表单时，把用户的SessionId放到Session里面
            {
                if (str.ToUpper().Trim().StartsWith("SID"))
                {
                    Session["Ult_SessionId"] = str.Replace("SID=", "").Replace("sid=", "");
                }
            }

            //保存sid到数据库
            string  sessionid = ConvertUtil.ToString(Session["Ult_SessionId"]);
            if (!string.IsNullOrEmpty(sessionid))
            {
                try
                {
                    string loginName = SessionLogic.GetLoginName();
                    loginName = loginName.Replace("\\","/");
                    SerialNoLogic sn = new SerialNoLogic();
                    int max=sn.GetMaxNo("COM_USERSETTINGS", "ID");
                    string sql = "delete from COM_USERSETTINGS where name='" + loginName + "' and SETTINGTYPE='SessionID';insert into COM_USERSETTINGS(id,userid,SETTINGTYPE,name,value,createdate) values(" + (max + 1) + ",0,'SessionID','" + loginName + "','" + sessionid + "',getdate());";
                    DataAccess.Instance("BizDB").ExecuteNonQuery(sql);
                }
                catch
                {

                }
            }
            
            Response.Redirect(url);

        }
    }
}