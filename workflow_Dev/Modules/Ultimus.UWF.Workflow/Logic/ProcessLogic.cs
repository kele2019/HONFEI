using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ultimus.UWF.Workflow.Entity;
using MyLib;
using System.Data;

namespace Ultimus.UWF.Workflow.Logic
{
    public class ProcessLogic
    {
        public List<ProcessEntity> GetProcessList()
        {
            return DataAccess.Instance("UltDB").ExecuteList<ProcessEntity>("SELECT DISTINCT PROCESSNAME FROM PROCESSES");
        }

        public int GetProcessMaxVersion(string processName)
        {
            return ConvertUtil.ToInt32( DataAccess.Instance("UltDB").ExecuteScalar("SELECT max(PROCESSVERSION) FROM PROCESSES WHERE PROCESSNAME='"+processName+"'"));
        }

        public DataTable GetProcessSteps(string processName)
        {
            int maxversion = GetProcessMaxVersion(processName);
            return DataAccess.Instance("UltDB").ExecuteDataTable("SELECT * FROM PROCESSSTEPS WHERE PROCESSNAME='" + processName + "' and PROCESSVERSION="+maxversion +" ORDER By STEPTYPE");
        }
    }
}
