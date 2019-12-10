using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MyLib;
using Ultimus.UWF.Common.Interface;
using Ultimus.UWF.Workflow.Entity;
using Ultimus.UWF.Workflow.Interface;
using Ultimus.UWF.Common.Entity;

namespace Ultimus.UWF.V8.Service
{
    /// <summary>
    /// SubscriberService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class SubscriberService : System.Web.Services.WebService
    {
        IMessage _msg = ServiceContainer.Instance().GetService<IMessage>();
        ITask _task = ServiceContainer.Instance().GetService<ITask>();
        [WebMethod]
        public void CompletedTaskDeleted(string strProcessName, int nIncident, string strTaskId)
        {

        }

        [WebMethod]
        public void IncidentAborted(string strProcessName, int nIncident, string strReason)
        {

        }

        [WebMethod]
        public void IncidentCompleted(string strProcessName, int nIncident)
        {

        }

        [WebMethod]
        public void IncidentInitiated(string strProcessName, int nIncident)
        {

        }

        [WebMethod]
        public void QueueTaskActivated(string strProcessName, int nIncident, string strTaskId)
        {

        }

        [WebMethod]
        public void StepAborted(string strProcessName, int nIncident, int nStepType, string strStepId, string strStepLabel)
        {

        }

        [WebMethod]
        public void TaskActivated(string strProcessName, int nIncident, int nStepType, string strTaskId)
        {
            //杨秦,您好！现有【行管人员试用期转正审批流程 + XGSZ-20150715122,2014-09-10,,其余职级行管人员】申请单需要您的审批，请打开以下地址进行处理：[处理任务|http://10.10.0.131/bpm/popup/openform.aspx?TaskID=070217fa9eb71fb97bcb66866befc7]
            TaskEntity task = new TaskEntity();
            task = _task.GetTaskEntity(strTaskId);

            Ultimus.OC.OrgChart oc = new Ultimus.OC.OrgChart("quanyou.com.cn");
            Ultimus.OC.User u = new Ultimus.OC.User();
            oc.FindUser(task.ASSIGNEDTOUSER, "", "", out u);
            string fullName = u.strUserFullName;
            string body = string.Format("{0},您好！现有【{1} + {2},{3},{4}】申请单需要您的审批，请打开以下地址进行处理：[处理任务|http://bpm.quanyou.com.cn:8080/Moduels/Ultimus.UWF.Workflow/openform.aspx?TaskID={5}]"
                , fullName, strProcessName, task.SUMMARY, task.STARTTIME.ToShortDateString(), task.STEPLABEL, strTaskId);
            MessageEntity msg = new MessageEntity();
            msg.Body = body;
            msg.To = task.ASSIGNEDTOUSER.Replace("quanyou.com.cn/", "");
            msg.Source = "BPM";
            msg.SendType = "RTX,SMS";
            _msg.Send(msg);
            LogUtil.Info("发送待办提醒:" + strTaskId);
        }

        [WebMethod]
        public void TaskAssigned(string strProcessName, int nIncident, string strTaskId, string strAssignedUser)
        {

        }

        [WebMethod]
        public void TaskCompleted(string strProcessName, int nIncident, int nStepType, string strTaskId)
        {
            //您好!【内部转账（结算）通知书审批流程-NJZS-20150723844,总公司,销售公司,370.25,结算】申请单已完成审批！[查看审批记录|http://10.10.0.131/bpm/popup/OpenForm.aspx?TaskID=0702155e4db9808d29602a65afb445]
            TaskEntity task = new TaskEntity();
            task = _task.GetTaskEntity(strTaskId);

            string body = string.Format("您好!【{0}-{1}】申请单已完成审批！[查看审批记录|http://bpm.quanyou.com.cn:8080/Moduels/Ultimus.UWF.Workflow/openform.aspx?TaskID={2}]"
                ,  strProcessName, task.SUMMARY, strTaskId);
            MessageEntity msg = new MessageEntity();
            msg.Body = body;
            msg.To = task.ASSIGNEDTOUSER.Replace("quanyou.com.cn/", "");
            msg.Source = "BPM";
            msg.SendType = "RTX,SMS";
            _msg.Send(msg);
            LogUtil.Info("发送完成提醒:"+strTaskId);
            //短信催办 BPM系统提醒您:【赵亚龙】于【2015/7/2 17:50:18】发起的【内部工作联系单流程/123,NGLD-201507127898】任务，请您及时办理
        }

        [WebMethod]
        public void TaskConferred(string strProcessName, int nIncident, string strTaskId, string strUser)
        {

        }

        [WebMethod]
        public void TaskDelayed(string strProcessName, int nIncident, string strTaskId)
        {

        }

        [WebMethod]
        public void TaskDeletedOnMinResponseComplete(string strProcessName, int nIncident, string strTaskId)
        {

        }

        [WebMethod]
        public void TaskLate(string strProcessName, int nIncident, string strTaskId)
        {

        }

        [WebMethod]
        public void TaskResubmitted(string strProcessName, int nIncident, string strTaskId)
        {

        }

        [WebMethod]
        public void TaskReturned(string strProcessName, int nIncident, int nStepType, string strTaskId)
        {

        }

        [WebMethod]
        public void TaskSubmitFailed(string strTaskId)
        {

        }

        [WebMethod]
        public void TasksPerDayThresholdReached(long lTasksPerDayLimit, long lThreshold)
        {

        }

        [WebMethod]
        public void CheckInTask(string strTaskId)
        {

        }

        [WebMethod]
        public void CheckOutTask(string strTaskId)
        {

        }

        [WebMethod]
        public void FindReplaceIncident(string strProcessName, int nIncident)
        {

        }

        [WebMethod]
        public void FindReplaceTask(string strTaskId)
        {

        }

        [WebMethod]
        public void SaveTask(string strTaskId)
        {

        }

        [WebMethod]
        public void LogInfo(string info)
        {
            LogUtil.Info(info);
        }

        [WebMethod]
        public void LogError(string error)
        {
            LogUtil.Info("Error:"+error);
        }

        
//--===============开启订阅
//USE [UltimusServer]
//GO

///****** Object:  Trigger [dbo].[TGR_TASKS_INSERT]    Script Date: 07/16/2015 19:58:26 ******/
//SET ANSI_NULLS ON
//GO

//SET QUOTED_IDENTIFIER ON
//GO

//CREATE TRIGGER [dbo].[TGR_TASKS_INSERT]
//ON [dbo].[TASKS]
//    AFTER INSERT 
//AS
//    DECLARE @TASKID NVARCHAR(50), @PROCESSNAME NVARCHAR(50), @INCIDENT INT, 
//    @STEPLABEL NVARCHAR(50), @STEPID INT,@TASKUSER NVARCHAR(50),@ASSIGNEDTOUSER NVARCHAR(50);
    
//    SELECT @TASKID = TASKID, 
//    @PROCESSNAME = PROCESSNAME ,
//    @INCIDENT = INCIDENT ,
//    @STEPLABEL = STEPLABEL ,
//    @STEPID = STEPID ,
//    @TASKUSER = TASKUSER ,
//    @ASSIGNEDTOUSER = ASSIGNEDTOUSER 
//    FROM INSERTED;
    
//    INSERT INTO ULTIMUSBIZ..WF_SUBSCRIPTION(EVENTTYPE,TASKID,PROCESSNAME,INCIDENT,STEPLABEL,STEPID,TASKUSER,ASSIGNEDTOUSER,CREATEDATE,STATUS)
//     VALUES('TASKACTIVATED',@TASKID,@PROCESSNAME,@INCIDENT,@STEPLABEL,@STEPID,@TASKUSER,@ASSIGNEDTOUSER,GETDATE(),1);
     
//     SET NOCOUNT ON;

//GO


    }
}
