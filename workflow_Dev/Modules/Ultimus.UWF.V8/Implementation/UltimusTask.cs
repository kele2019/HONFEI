using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using MyLib;
using System.Web;
using System.Data;
using Ultimus.WFServer;
using System.Data.Common;
using Ultimus.UWF.Workflow.Entity;
using Ultimus.UWF.Workflow.Implementation;
using Ultimus.UWF.Workflow.Logic;
using System.Xml;
using System.Drawing;
using System.Threading;

namespace Ultimus.UWF.V8.Implementation
{
     public class UltimusTask: DatabaseTask
    {
         public override List<TaskEntity> GetInitTaskList(string loginName, string str)
        {
            List<TaskEntity> initProcessList = new List<TaskEntity>();
            //load init process
            Ultimus.WFServer.TasklistFilter filter = new Ultimus.WFServer.TasklistFilter();
            filter.strArrUserName = new string[1] { loginName };
            Ultimus.WFServer.Tasklist tl = new Ultimus.WFServer.Tasklist();
            filter.nFiltersMask = Ultimus.WFServer.Filters.nFilter_Initiate;
            List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(str))
            {
                string[] sz = str.Split(',');
                foreach (string ss in sz)
                {
                    list.Add(ss);
                }
            }
            string error = "";
            tl.LoadFilteredTasks(filter, out error);
            for (int i = 0; i < tl.GetTasksCount(); i++)
            {
                if (list.Count > 0)
                {
                    if (list.Exists(p => tl.GetAt(i).strProcessName.ToUpper().Trim().IndexOf(p.ToUpper().Trim()) >= 0))
                    {
                        TaskEntity task = new TaskEntity();
                        task.PROCESSNAME = tl.GetAt(i).strProcessName;
                        task.TASKID = tl.GetAt(i).strTaskId;
                        task.SUMMARY = tl.GetAt(i).strSummary;
                        task.HELPURL = tl.GetAt(i).strHelpUrl;
                        if (string.IsNullOrEmpty(task.SUMMARY))
                        {
                            initProcessList.Add(task);
                        }
                        task.SERVERNAME = GetServerEntity().SERVERNAME;
                    }
                }
                else
                {
                    TaskEntity task = new TaskEntity();
                    task.PROCESSNAME = tl.GetAt(i).strProcessName;
                    task.TASKID = tl.GetAt(i).strTaskId;
                    task.SUMMARY = tl.GetAt(i).strSummary;
                    task.HELPURL = tl.GetAt(i).strHelpUrl;
                    if (string.IsNullOrEmpty(task.SUMMARY))
                    {
                        initProcessList.Add(task);
                    }
                    task.SERVERNAME = GetServerEntity().SERVERNAME;
                }
            }
            return initProcessList;
        }

        public override List<TaskEntity> GetDraftTaskList(string loginName, string str)
        {
            List<TaskEntity> initProcessList = new List<TaskEntity>();
            //load init process
            Ultimus.WFServer.TasklistFilter filter = new Ultimus.WFServer.TasklistFilter();
            filter.strArrUserName = new string[1] { loginName };
            Ultimus.WFServer.Tasklist tl = new Ultimus.WFServer.Tasklist();
            filter.nFiltersMask = Ultimus.WFServer.Filters.nFilter_Initiate;
            string error = "";
            tl.LoadFilteredTasks(filter, out error);
            for (int i = 0; i < tl.GetTasksCount(); i++)
            {
                TaskEntity task = new TaskEntity();
                task.PROCESSNAME = tl.GetAt(i).strProcessName;
                task.TASKID = tl.GetAt(i).strTaskId;
                task.SUMMARY = tl.GetAt(i).strSummary;
                task.HELPURL = tl.GetAt(i).strHelpUrl;
                if (!string.IsNullOrEmpty(task.SUMMARY))
                {
                    initProcessList.Add(task);
                }
                task.SERVERNAME = GetServerEntity().SERVERNAME;
            }
            return initProcessList;
        }

        public override string GetTaskUrl(string taskID, string type, string loginName)
        {
            TaskEntity entity = new TaskEntity();
            if (taskID.StartsWith("S"))
            {
                entity = GetInitTaskEntity(taskID);
            }
            else
            {
                entity = GetTaskEntity(taskID);
            }
            string processName = "";
            string stepLabel = "";
            int incident;
            if (entity == null) //表里面没有该Task，从EIK中拿
            {
                Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
                task.InitializeFromTaskId(loginName.Replace("\\", "/"), taskID);
                processName = task.strProcessName.Trim();
                stepLabel = task.strStepName.Trim();
                incident = task.nIncidentNo;
            }
            else
            {
                processName = entity.PROCESSNAME.Trim();
                stepLabel = entity.STEPLABEL.Trim();
                incident = entity.INCIDENT;
                if (string.IsNullOrEmpty(loginName))
                {
                    loginName = entity.ASSIGNEDTOUSER;
                }
            }

            string page = StepSettingsLogic.GetStepPage(processName, stepLabel);
            string url = "";
            if (string.IsNullOrEmpty(page)) //Standard Form
            {
                string result = "";
                Ultimus.WFServer.Task t = new Ultimus.WFServer.Task();
                t.InitializeFromTaskId(loginName, taskID);
                t.ExtractFormURL(out result);
                if (!string.IsNullOrEmpty(result)) //EIK没有调用到该task
                {
                    if (result.StartsWith("."))
                    {
                        result = result.Replace("./", "");
                    }
                    url = GetStandardClientUrl(result);
                    Ultimus.ClientServices.Services srv = new Ultimus.ClientServices.Services();
                    string sessionid = "";
                    if (type.StartsWith("view"))
                    {
                        sessionid = type.Replace("view","").Replace("sid=","");
                    }
                    string error = "";
                    if (string.IsNullOrEmpty( sessionid))
                    {
                        srv.LoginUser(loginName.Split('/')[0], loginName.Split('/')[1], "", out sessionid, out error);
                        try
                        {
                            t.CheckOutTask(loginName, out error);
                            error = "";
                        }
                        catch
                        {
                        }
                    }

                    
                    if (!string.IsNullOrEmpty(error))
                    {
                        throw new Exception("Ultimus Login Error:" + error);
                    }
                    url += "&sid=" + sessionid;
                    //HttpContext.Current.Response.AddHeader("Ultimus Workflow1", "Ultimus Workflow");
                    //HttpContext.Current.Response.Cookies["TaskID"].Value = taskID;
                    //HttpContext.Current.Response.Cookies["TaskID"].Path = @"/";
                    //HttpContext.Current.Response.Cookies["UserID"].Value = userName;
                    //HttpContext.Current.Response.Cookies["UserID"].Path = @"/";
                }
                else
                {
                    if (entity != null) //有这个task,把该task再插入回来
                    {
                        InsertBackFromArchive(entity.PROCESSNAME, entity.INCIDENT);
                        t.InitializeFromTaskId(loginName.Replace("\\", "/"), taskID);
                        t.ExtractFormURL(out result);
                        if (!string.IsNullOrEmpty(result))
                        {
                            if (result.StartsWith("."))
                            {
                                result = result.Replace("./", "");
                            }
                            url = GetStandardClientUrl(result);
                            Ultimus.ClientServices.Services srv = new Ultimus.ClientServices.Services();
                            string sessionid = "";
                            string error = "";
                            srv.LoginUser(loginName.Split('/')[0], loginName.Split('/')[1], "", out sessionid, out error);
                            if (!string.IsNullOrEmpty(error))
                            {
                                throw new Exception("Ultimus Login Error:" + error);
                            }
                            url += "&sid=" + sessionid;
                        }
                    }
                    else
                    {
                        throw new Exception("OpenForm_CannotLoadTask");
                    }
                }
            }
            else //.net Form
            {
                if (HttpContext.Current.Request.Url.Port == 443)
                {
                    url = "https://" + HttpContext.Current.Request.Url.Host 
                    + "/" + page + "?ProcessName=" + processName.Trim() + "&StepName=" + stepLabel.Trim() + "&Incident="
                    + incident + "&TaskID=" + taskID.Trim() + "&UserName=" + HttpContext.Current.Server.UrlEncode(loginName) + "&Type=" + type;
                }
                else
                {
                    url = "http://" + HttpContext.Current.Request.Url.Host + ":"
                        + HttpContext.Current.Request.Url.Port + "/" + page + "?ProcessName=" + processName.Trim() + "&StepName=" + stepLabel.Trim() + "&Incident="
                       + incident + "&TaskID=" + taskID.Trim() + "&UserName=" + HttpContext.Current.Server.UrlEncode(loginName) + "&Type=" + type;
                }
            }
            return url;
        }

        

        public void InsertBackFromArchive(string processName, int incident)
        {
            //string archiveDBName = ConfigurationManager.AppSettings["ArchiveDBName"];
            //if (!string.IsNullOrEmpty(archiveDBName))
            //{
            //    Hashtable table = new Hashtable();
            //    table.Add("processName", processName);
            //    table.Add("incident", incident);
            //    table.Add("ArchiveDBName", archiveDBName);
            //    if (DataAccess.Instance("UltDB").SqlMapper.DataSource.DbProvider.Name.ToUpper().IndexOf("ORACLE") >= 0)
            //    {
            //        DataAccess.Instance("UltDB").Insert("TaskLogic_InsertBackFromArchiveOracle", table);
            //    }
            //    else
            //    {
            //        DataAccess.Instance("UltDB").Insert("TaskLogic_InsertBackFromArchive", table);
            //    }
            //}
        }

        string GetStandardClientUrl(string pUrl)
        {
            string url = MyLib.ConfigurationManager.AppSettings["StandardClientUrl"];
            if (string.IsNullOrEmpty(url))
            {
                string ServerName = HttpContext.Current.Request.Url.Host;

                string URL = "http://" + ServerName + "/Ultweb/" + pUrl;
                return URL;
            }
            else
            {
                return url + pUrl;
            }
        }

        public override int SubmitTask(TaskEntity entity)
        {
            Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
            bool flag = task.InitializeFromTaskId(entity.ASSIGNEDTOUSER.Replace("\\", "/"), entity.TASKID);
            string strError = "";
            int incident = 0;
            if (flag)
            {
                //Ultimus.WFServer.Variable[] vs = GetVariables(vars);
                SetVariables(task, entity.VarList);
                if (!entity.SYNC)
                {
                    incident = -1;
                }
                if (string.IsNullOrEmpty(entity.SUMMARY))
                {
                    entity.SUMMARY = task.strSummary;
                }
                if (!task.SendFrom(entity.ASSIGNEDTOUSER.Replace("\\", "/"), "", entity.SUMMARY, false, ref incident, out strError))
                {
                    if (string.IsNullOrEmpty(strError) || incident == 0)
                    {
                        strError = "请稍候从草稿箱打开，重新提交！";
                    }
                }
            }
            return incident;
        }

        string SetVariables(Task task, List<VarEntity> vars)
        {
            #region 垃圾，被安码骗了
            //string error = "";
            //string lasterror = "";
            //foreach (VarEntity ety in vars) //USER:org=Business Organization,user=WIN-O7BVH1JUCSN/Administrator
            //{
            //    if (ety.Value != null)
            //    {
            //        // modify by sky 2013/12/5 添加用户赋值
            //        string varValue;
            //        if (ety.Value.ToString().Contains("|USER"))
            //        {
            //            string org = ConvertUtil.ToString(ConfigurationManager.AppSettings["Org"]);
            //            varValue = "USER:org=" + org + ",user=" + ety.Value.ToString().Replace("|USER", "");
            //        }
            //        //else
            //        //{
            //            varValue = ety.Value.ToString();
            //        //}
            //        object val = new object[] { varValue };
                    
            //        //_----------------------------------------------
            //        string value = varValue;
            //        string[] sz = value.Split('|');

            //        string etyName = ety.Name.ToString();
            //        if(etyName.IndexOf("TaskData.") != 0 && etyName.IndexOf("IncidentData.") != 0)
            //        {
            //            etyName = "TaskData.Global." + etyName;
            //        }
            //        if (sz.Length >1)
            //        {
            //            task.SetNodeValue(etyName, sz, out error);

            //        }
            //        else
            //        {
            //            task.SetNodeValue(etyName, val, out error);

            //        }
            //        if (!string.IsNullOrEmpty(error))
            //        {
            //            lasterror = error;
            //        }
            //    }

            //}
            //return lasterror;
            #endregion
            string error = "";
            string lasterror = "";
            foreach (VarEntity ety in vars) //USER:org=Business Organization,user=WIN-O7BVH1JUCSN/Administrator
            {
                if (ety.Value != null)
                {
                    // modify by albert 2014/7/1 添加一个节点多人审批，给变量数组赋值

                    object val = null;
                    string varValue;
                    // modify by Jack 2014/7/16 添加 || ety.Key.ToString().Contains("APPROVALARR_")
					if (ety.Name.ToString().Contains("ApprovalArr_") || ety.Name.ToString().Contains("APPROVALARR_"))
                    {
                        string org = ConvertUtil.ToString(MyLib.ConfigurationManager.AppSettings["Org"]);
                        string[] ApprovalArr = ety.Value.ToString().Split(',');

                        string[] valStr = new string[ApprovalArr.Length];
                        for (int i = 0; i < ApprovalArr.Length; i++)
                        {
                            if (ApprovalArr[i].Contains("|USER"))
                            {
                                string varUserStr = "USER:org=" + org + ",user=" + ApprovalArr[i].ToString().Replace("|USER", "");
                                valStr[i] = varUserStr;
                            }
                        }
                        val = valStr;
                    }
                    else if (ety.Value.ToString().Contains("|USER"))
                    {
                        string org = ConvertUtil.ToString(ConfigurationManager.AppSettings["Org"]);
                        varValue = "USER:org=" + org + ",user=" + ety.Value.ToString().Replace("|USER", "");
                        val = new object[] { varValue };
                    }
                    else
                    {
                        varValue = ety.Value.ToString();
                        val = new object[] { varValue };
                    }
                    task.SetNodeValue("TaskData.Global." + ety.Name.ToString(), val, out error);
                    if (!string.IsNullOrEmpty(error))
                    {
                        lasterror = error;
                    }
                }

            }
            return lasterror;
        }

        public override void ReturnTask(TaskEntity entity)
        {
            Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
            bool flag = task.InitializeFromTaskId(entity.ASSIGNEDTOUSER.Replace("\\", "/"), entity.TASKID);
            string strError = "";
            if (flag)
            {
                SetVariables(task, entity.VarList);
                if (string.IsNullOrEmpty(entity.SUMMARY))
                {
                    entity.SUMMARY = task.strSummary;
                }
                task.Return(entity.REASON, entity.SUMMARY, false, out strError);

            }
        }

        public override void RejectTask(TaskEntity task)
        {
            AbortIncident(task);
        }

        public override void AbortIncident(TaskEntity entity)
        {
            Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
            bool flag = task.InitializeFromTaskId(entity.ASSIGNEDTOUSER.Replace("\\", "/"), entity.TASKID);
            string userName = entity.ASSIGNEDTOUSER.Replace("\\", "/");
            string strError = "";
            if (flag)
            {
                Ultimus.WFServer.Incident incident = new Ultimus.WFServer.Incident();
                incident.LoadIncident(task.strProcessName, task.nIncidentNo);
                incident.AbortIncident(userName, entity.REASON, out strError);
            }
        }

        public override bool DeleteTask(TaskEntity entity)
        {
            Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
            bool flag = task.InitializeFromTaskId(entity.ASSIGNEDTOUSER.Replace("\\", "/"), entity.TASKID);
            if (flag)
            {
               return  task.DeleteTask();
            }
            return false;
        }
          
        public override byte[] GetGraphicalStatus(string processName, int incident)
        {
            byte[] bytesGif;
            HistoryPlayback hpb = new HistoryPlayback();
            string error = "";
            if (incident <= 0)
            {
                incident = 1;
            }
            hpb.LoadHistoryEvents(processName, incident, out error);
            if (!string.IsNullOrEmpty(error))
            {
                Incident.Status pstatus = new Incident.Status();
                Incident pincident = new Incident();

                if (incident <= 0)
                {
                    int version = GetProcessVersion(processName);
                    pstatus.GetGraphicalStatus(processName, incident, version, out bytesGif); //取3次，防止取不到图
                    if (bytesGif == null)
                    {
                        pstatus.GetGraphicalStatus(processName, incident, version, out bytesGif);
                        Thread.Sleep(500);
                        if (bytesGif == null)
                        {
                            pstatus.GetGraphicalStatus(processName, incident, version, out bytesGif);
                            Thread.Sleep(500);
                            if (bytesGif == null)
                            {
                                pstatus.GetGraphicalStatus(processName, incident, version, out bytesGif);
                                Thread.Sleep(500);
                            }
                        }
                    }
                }
                else
                {
                    pincident.LoadIncident(processName, incident);
                    pincident.GetIncidentStatus(out pstatus);
                    pstatus.GetGraphicalStatus(pincident.strProcessName, pincident.nIncidentNo, pincident.nVersion, out bytesGif);
                }
                return bytesGif;
            }
            Bitmap bmp = hpb.GetEventImage();
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            bytesGif = stream.GetBuffer();
            stream.Close();
            stream = null;
            bmp.Dispose();
            bmp = null;
            return bytesGif;

        }
          
        public override bool AssignTask(string taskId, string toUser)
        {
            Ultimus.WFServer.Task pTask = new Ultimus.WFServer.Task();
            pTask.InitializeFromTaskId(taskId);
            return pTask.AssignTask(toUser);
        }

        public override bool AssignAllCurrentTasks(string fromUser, string toUser)
        {
            Ultimus.OC.User pUserTask = null;
            Ultimus.OC.OrgChart porg = new Ultimus.OC.OrgChart("HONFEI");
            porg.FindUser(fromUser, "", "", out pUserTask);
            return pUserTask.AssignAllCurrentTasks(toUser);
        }

        public override bool AssignAllFutureTasks(string fromUser, string toUser, DateTime toDate)
        {
            Ultimus.OC.User pUserTask = null;
            Ultimus.OC.OrgChart porg = new Ultimus.OC.OrgChart();
            //if (fromUser.StartsWith("Ultimus"))
            //{
            //    porg = new Ultimus.OC.OrgChart("Ultimus");
            //}
            if (fromUser.StartsWith("HONFEI"))
            {
                porg = new Ultimus.OC.OrgChart("HONFEI");
            }

            porg.FindUser(fromUser, "", "", out pUserTask);
            return pUserTask.AssignAllFutureTasks(toUser, toDate.ToOADate());
        }

        public override bool AssignProcessFutureTasks(string processName, string stepName, string fromUser, string toUser, DateTime toDate)
        {
            return base.AssignProcessFutureTasks(processName, stepName, fromUser, toUser, toDate);
        }

        public override void LogoutUser(string sessionId)
        {
            Ultimus.ClientServices.Services srv = new Ultimus.ClientServices.Services();
            string error="";
            srv.LogoutUser(sessionId, out error);
        }

        public   Hashtable LoadTask(string taskId)
        {
            Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
            bool flag = task.InitializeFromTaskId(taskId);
            Hashtable table = new Hashtable();
            if (flag)
            {
                string strStepSchema = "";
                string strError = "";
                //UltEIKXMLResolver UltEikXmlResolver;
                if (task.GetTaskXMLEx(taskId,out strStepSchema, out strError))
                {

                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    doc.LoadXml(strStepSchema);

                    foreach (System.Xml.XmlNode var in doc.ChildNodes.Item(1).ChildNodes.Item(0).ChildNodes)
                    {
                        if (var.Name != null)
                        {
                           
                            table.Add(var.Name, var.InnerText);
                        }
                    }
                }
            }
            return table;
        }

    }
}