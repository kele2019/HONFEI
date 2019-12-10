using System;
using System.Collections.Generic;
using System.Text;
using UltimusEvents;
using System.EnterpriseServices;
using System.IO;
using System.Data.SqlClient;
using UltimusSubscriber.SubscriberServiceRef;

namespace UltimusSubscriber
{
    public class UltimusSubscriberClass : ServicedComponent, IUltimusPublisher, IUltimusPublisher2
    {
        //固定为8088端口
        SubscriberService GetService()
        {
            SubscriberServiceRef.SubscriberService srv = new SubscriberServiceRef.SubscriberService();
            srv.Url = "http://localhost:8088/Service/SubscriberService.asmx";
            return srv;
        }

        public void CompletedTaskDeleted(string strProcessName, int nIncident, string strTaskId)
        {
            try
            {
                GetService().CompletedTaskDeleted(strProcessName, nIncident, strTaskId);
            }
            catch (Exception e)
            {
                GetService().LogError("UltimusSubscriber CompletedTaskDeleted:" + e.Message);
            }
        }

        public void IncidentAborted(string strProcessName, int nIncident, string strReason)
        {
            try
            {
                GetService().IncidentAborted(strProcessName, nIncident, strReason);
            }
            catch (Exception e)
            {
                GetService().LogError("UltimusSubscriber IncidentAborted:" + e.Message);
            }


        }

        public void IncidentCompleted(string strProcessName, int nIncident)
        {
            try
            {
                GetService().IncidentCompleted(strProcessName, nIncident);
            }
            catch (Exception e)
            {
                GetService().LogError("UltimusSubscriber IncidentCompleted:" + e.Message);
            }
        }

        public void IncidentInitiated(string strProcessName, int nIncident)
        {
            try
            {
                GetService().IncidentInitiated(strProcessName, nIncident);
            }
            catch (Exception e)
            {
                GetService().LogError("UltimusSubscriber IncidentInitiated:" + e.Message);
            }
        }

        public void QueueTaskActivated(string strProcessName, int nIncident, string strTaskId)
        {
            try
            {
                GetService().QueueTaskActivated(strProcessName, nIncident, strTaskId);
            }
            catch (Exception e)
            {
                GetService().LogError("UltimusSubscriber QueueTaskActivated:" + e.Message);
            }
        }

        public void StepAborted(string strProcessName, int nIncident, int nStepType, string strStepId, string strStepLabel)
        {
            try
            {
                GetService().StepAborted(strProcessName, nIncident, nStepType, strStepId, strStepLabel);
            }
            catch (Exception e)
            {
                GetService().LogError("UltimusSubscriber StepAborted:" + e.Message);
            }
        }

        public void TaskActivated(string strProcessName, int nIncident, int nStepType, string strTaskId)
        {
            try
            {
                GetService().TaskActivated(strProcessName, nIncident, nStepType, strTaskId);
            }
            catch (Exception e)
            {
                GetService().LogError("UltimusSubscriber TaskActivated:" + e.Message);
            }
        }

        public void TaskAssigned(string strProcessName, int nIncident, string strTaskId, string strAssignedUser)
        {
            try
            {
                GetService().TaskAssigned(strProcessName, nIncident, strTaskId, strAssignedUser);
            }
            catch (Exception e)
            {
                GetService().LogError("UltimusSubscriber TaskAssigned:" + e.Message);
            }
        }

        public void TaskCompleted(string strProcessName, int nIncident, int nStepType, string strTaskId)
        {
            try
            {
                GetService().TaskCompleted(strProcessName, nIncident, nStepType, strTaskId);
            }
            catch (Exception e)
            {
                GetService().LogError("UltimusSubscriber TaskCompleted:" + e.Message);
            }
        }

        public void TaskConferred(string strProcessName, int nIncident, string strTaskId, string strUser)
        {
            try
            {
                GetService().TaskConferred(strProcessName, nIncident, strTaskId, strUser);
            }
            catch (Exception e)
            {
                GetService().LogError("UltimusSubscriber TaskConferred:" + e.Message);
            }
        }

        public void TaskDelayed(string strProcessName, int nIncident, string strTaskId)
        {
            try
            {
                GetService().TaskDelayed(strProcessName, nIncident, strTaskId);
            }
            catch (Exception e)
            {
                GetService().LogError("UltimusSubscriber TaskDelayed:" + e.Message);
            }
        }

        public void TaskDeletedOnMinResponseComplete(string strProcessName, int nIncident, string strTaskId)
        {
            try
            {
                GetService().TaskDeletedOnMinResponseComplete(strProcessName, nIncident, strTaskId);
            }
            catch (Exception e)
            {
                GetService().LogError("UltimusSubscriber TaskDeletedOnMinResponseComplete:" + e.Message);
            }
        }

        public void TaskLate(string strProcessName, int nIncident, string strTaskId)
        {
            try
            {
                GetService().TaskLate(strProcessName, nIncident, strTaskId);
            }
            catch (Exception e)
            {
                GetService().LogError("UltimusSubscriber TaskLate:" + e.Message);
            }

        }

        public void TaskResubmitted(string strProcessName, int nIncident, string strTaskId)
        {
            try
            {
                GetService().TaskResubmitted(strProcessName, nIncident, strTaskId);
            }
            catch (Exception e)
            {
                GetService().LogError("UltimusSubscriber TaskResubmitted:" + e.Message);
            }
        }

        public void TaskReturned(string strProcessName, int nIncident, int nStepType, string strTaskId)
        {
            try
            {
                GetService().TaskReturned(strProcessName, nIncident, nStepType, strTaskId);
            }
            catch (Exception e)
            {
                GetService().LogError("UltimusSubscriber TaskReturned:" + e.Message);
            }

        }

        public void TaskSubmitFailed(string strTaskId)
        {
            try
            {
                GetService().TaskSubmitFailed(strTaskId);
            }
            catch (Exception e)
            {
                GetService().LogError("UltimusSubscriber TaskSubmitFailed:" + e.Message);
            }

        }

        public void TasksPerDayThresholdReached(long lTasksPerDayLimit, long lThreshold)
        {
            try
            {
                GetService().TasksPerDayThresholdReached(lTasksPerDayLimit, lThreshold);
            }
            catch (Exception e)
            {
                GetService().LogError("UltimusSubscriber TasksPerDayThresholdReached:" + e.Message);
            }

        }

        public void CheckInTask(string strTaskId)
        {
            try
            {
                GetService().CheckInTask(strTaskId);
            }
            catch (Exception e)
            {
                GetService().LogError("UltimusSubscriber CheckInTask:" + e.Message);
            }

        }

        public void CheckOutTask(string strTaskId)
        {
            try
            {
                GetService().CheckOutTask(strTaskId);
            }
            catch (Exception e)
            {
                GetService().LogError("UltimusSubscriber CheckOutTask:" + e.Message);
            }

        }

        public void FindReplaceIncident(string strProcessName, int nIncident)
        {
            try
            {
                GetService().FindReplaceIncident(strProcessName, nIncident);
            }
            catch (Exception e)
            {
                GetService().LogError("UltimusSubscriber FindReplaceIncident:" + e.Message);
            }

        }

        public void FindReplaceTask(string strTaskId)
        {
            try
            {
                GetService().FindReplaceTask(strTaskId);
            }
            catch (Exception e)
            {
                GetService().LogError("UltimusSubscriber FindReplaceTask:" + e.Message);
            }

        }

        public void SaveTask(string strTaskId)
        {
            try
            {
                GetService().SaveTask(strTaskId);
            }
            catch (Exception e)
            {
                GetService().LogError("UltimusSubscriber SaveTask:" + e.Message);
            }

        }
    }
}
