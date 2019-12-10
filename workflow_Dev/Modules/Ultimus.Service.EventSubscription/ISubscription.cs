using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ultimus.Service.EventSubscription
{
    public interface ISubscription
    {
        void CompletedTaskDeleted(string strProcessName, int nIncident, string strTaskId);

        void IncidentAborted(string strProcessName, int nIncident, string strReason);

        void IncidentCompleted(string strProcessName, int nIncident);

        void IncidentInitiated(string strProcessName, int nIncident);

        void QueueTaskActivated(string strProcessName, int nIncident, string strTaskId);

        void StepAborted(string strProcessName, int nIncident, int nStepType, string strStepId, string strStepLabel);

        void TaskActivated(string strProcessName, int nIncident, int nStepType, string strTaskId);

        void TaskAssigned(string strProcessName, int nIncident, string strTaskId, string strAssignedUser);

        void TaskCompleted(string strProcessName, int nIncident, int nStepType, string strTaskId);

        void TaskConferred(string strProcessName, int nIncident, string strTaskId, string strUser);

        void TaskDelayed(string strProcessName, int nIncident, string strTaskId);

        void TaskDeletedOnMinResponseComplete(string strProcessName, int nIncident, string strTaskId);

        void TaskLate(string strProcessName, int nIncident, string strTaskId);

        void TaskResubmitted(string strProcessName, int nIncident, string strTaskId);

        void TaskReturned(string strProcessName, int nIncident, int nStepType, string strTaskId);

        void TaskSubmitFailed(string strTaskId);

        void TasksPerDayThresholdReached(long lTasksPerDayLimit, long lThreshold);

        void CheckInTask(string strTaskId);

        void CheckOutTask(string strTaskId);

        void FindReplaceIncident(string strProcessName, int nIncident);

        void FindReplaceTask(string strTaskId);
       

        void SaveTask(string strTaskId);
        
    }
}
