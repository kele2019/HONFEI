using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using EntityLibrary;
using Ultimus.WFServer;
using DataBaseLibrary;
using MyLib;

namespace UltimusEikLibrary
{
    public class UltimusEikOfIncident
    {
        private PublicFunctionClass pfc = new PublicFunctionClass();

        /// <summary>
        /// 获得流程监控图
        /// </summary>
        /// <param name="ProcessName">流程名称</param>
        /// <param name="Incident">实例编号</param>
        /// <returns>byte[]</returns>
        public byte[] GetProcessMonitoring(string ProcessName, int Incident)
        {
            try
            {
                Incident objInc = new Incident();
                bool pReturnInc = objInc.LoadIncident(ProcessName, Incident);
                byte[] bytesGif;
                Incident.Status objIncStatus = new Incident.Status();
                objInc.GetIncidentStatus(out objIncStatus);
                objIncStatus.GetGraphicalStatus(objInc.strProcessName, objInc.nIncidentNo, objInc.nVersion, out bytesGif);
                return bytesGif;
            }
            catch (Exception ex)
            {
                pfc.WriteLogOfTxt(ex.Message);
                throw ex;
            }
        }

        public Variable[] GetVariable(string ProcessName)
        {
            try
            {
                Variable[] vars = null;
                string taskID = GetInitTaskID(ProcessName);
                if (!string.IsNullOrEmpty(taskID))
                {
                    Ultimus.WFServer.Task t = new Ultimus.WFServer.Task();
                    t.InitializeFromInitiateTaskId("", taskID);
                    string error = "";
                    t.GetAllTaskVariables(out vars, out error);
                }
                return vars;
            }
            catch (Exception ex)
            {
                pfc.WriteLogOfTxt(ex.Message);
                throw ex;
            }
        }

        public string GetInitTaskID(string processName)
        {
            return  DataAccess.Instance("UltDB").ExecuteScalar("select top 1 INITIATEID from INITIATE  where  ProcessName='" + processName + "' order by PROCESSVERSION desc").ToString();
        }

    }
}
