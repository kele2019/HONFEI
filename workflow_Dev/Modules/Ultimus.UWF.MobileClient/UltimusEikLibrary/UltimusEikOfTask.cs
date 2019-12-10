using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using EntityLibrary;
using Ultimus.WFServer;
using System.Configuration;
using System.Web;

namespace UltimusEikLibrary
{
    public class UltimusEikOfTask
    {
        private PublicFunctionClass pfc = new PublicFunctionClass();

        private DALLibrary.MobileClient_Process ProessDAL = new DALLibrary.MobileClient_Process();

        private DALLibrary.MobileClient_Step StepDAL = new DALLibrary.MobileClient_Step();

        /// <summary>
        /// 根据用户账号通过UltimusEIK接口取得新建任务列表
        /// </summary>
        /// <param name="UserAccount">用户账号</param>
        /// <returns></returns>
        public List<EntityLibrary.Tasks> GetNewTaskListOfUltimusEik(string UserAccount)
        {
            List<EntityLibrary.Tasks> tasks = new List<EntityLibrary.Tasks>();
            try
            {
                Tasklist list = new Tasklist();
                TasklistFilter filter = new TasklistFilter();
                filter.nFiltersMask = Filters.nFilter_Initiate;
                filter.strArrUserName = new string[] { UserAccount };
                list.LoadFilteredTasks(filter);
                for (int i = 0; i < list.GetTasksCount(); i++)
                {
                    EntityLibrary.Tasks item = pfc.GetEntityOfTask(list.GetAt(i));
                    if (IsCreate(item))
                    {
                        tasks.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                pfc.WriteLogOfTxt(ex.Message);
            }
            return tasks;
        }

        /// <summary>
        /// 根据用户账号通过UltimusEIK接口取得待办任务列表
        /// </summary>
        /// <param name="UserAccount">用户账号</param>
        /// <returns></returns>
        public List<EntityLibrary.Tasks> GetMyTaskListOfUltimusEik(string UserAccount)
        {
            List<EntityLibrary.Tasks> tasks = new List<EntityLibrary.Tasks>();
            try
            {
                Tasklist list = new Tasklist();
                TasklistFilter filter = new TasklistFilter();
                filter.strArrUserName = new string[] { UserAccount };
                filter.SetSortOrder(1, 0);
                list.LoadFilteredTasks(filter);
                for (int i = 0; i < list.GetTasksCount(); i++)
                {
                    Task task = list.GetAt(i);
                    if (task.nTaskStatus == TaskStatuses.TASK_STATUS_ACTIVE)
                    {
                        EntityLibrary.Tasks item = pfc.GetEntityOfTask(task);
                        if (IsCreate(item))
                        {
                            tasks.Add(item);
                        }
                    }

                }
                
            }
            catch (Exception ex)
            {
                pfc.WriteLogOfTxt(ex.Message);
            }
            return tasks;
        }

        /// <summary>
        /// 根据用户账号通过UltimusEIK接口取得已办任务列表
        /// </summary>
        /// <param name="UserAccount">用户账号</param>
        /// <returns></returns>
        public List<EntityLibrary.Tasks> GetCompleteTaskListOfUltimusEik(string UserAccount)
        {
            List<EntityLibrary.Tasks> tasks = new List<EntityLibrary.Tasks>();
            try
            {
                Tasklist list = new Tasklist();
                TasklistFilter filter = new TasklistFilter();
                filter.strArrUserName = new string[] { UserAccount };
                filter.SetSortOrder(1, 1);
                list.LoadFilteredTasks(filter);
                for (int i = 0; i < list.GetTasksCount(); i++)
                {
                    Task task = list.GetAt(i);
                    if (task.nTaskStatus == TaskStatuses.TASK_STATUS_COMPLETED)
                    {
                        EntityLibrary.Tasks item = pfc.GetEntityOfTask(task);
                        if (IsCreate(item))
                        {
                            tasks.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                pfc.WriteLogOfTxt(ex.Message);
            }
            return tasks;
        }

        /// <summary>
        /// 发起流程
        /// </summary>
        /// <param name="ProcessName">流程名称</param>
        /// <param name="StepName">步骤名称</param>
        /// <param name="Summary">流程摘要</param>
        /// <param name="UserAccount">用户账号</param>
        /// <param name="variable">参数泛型集合</param>
        /// <returns>返回实例编号</returns>
        public int SendForm(string ProcessName, string StepName, string Summary, string UserAccount, List<VariableInfo> variable)
        {
            int NewIncident = 0; string SendFormError = "";
            try
            {
                Task task = pfc.InitTask(ProcessName, 0, StepName, UserAccount);
                Variable[] variables = pfc.GetVariable(variable);
                if (task.SendFrom(UserAccount, variables, "", Summary, ref NewIncident, out SendFormError))
                {
                    pfc.WriteLogOfTxt("ProcessName:[" + ProcessName + "],Incident:[" + NewIncident + "],StepName:[" + StepName + "],UserAccount:[" + UserAccount + "],Summary:[" + Summary + "] 提交任务成功!");
                }
                else
                {
                    throw new Exception("ProcessName:[" + ProcessName + "],Incident:[" + NewIncident + "],StepName:[" + StepName + "],UserAccount:[" + UserAccount + "],Summary:[" + Summary + "] " + SendFormError);
                }
            }
            catch (Exception ex)
            {
                pfc.WriteLogOfTxt(ex.Message);
            }
            return NewIncident;
        }

        /// <summary>
        /// 发起流程
        /// </summary>
        /// <param name="TaskID">TaskID</param>
        /// <param name="Summary">流程摘要</param>
        /// <param name="UserAccount">用户账号</param>
        /// <param name="variable">参数泛型集合</param>
        /// <returns>返回实例编号</returns>
        public int SendForm(string TaskID, string Summary, string UserAccount, List<VariableInfo> variable)
        {
            int NewIncident = 0; string SendFormError = "";
            try
            {
                Task task = pfc.InitTask(TaskID);
                Variable[] variables = pfc.GetVariable(variable);
                if (task.SendFrom(UserAccount, variables, "", Summary, ref NewIncident, out SendFormError))
                {
                    pfc.WriteLogOfTxt("ProcessName:[" + task.strProcessName + "],Incident:[" + NewIncident + "],StepName:[" + task.strStepName + "],UserAccount:[" + UserAccount + "],Summary:[" + Summary + "] 提交任务成功!");
                }
                else
                {
                    throw new Exception("ProcessName:[" + task.strProcessName + "],Incident:[" + NewIncident + "],StepName:[" + task.strStepName + "],UserAccount:[" + UserAccount + "],Summary:[" + Summary + "] " + SendFormError);
                }
            }
            catch (Exception ex)
            {
                pfc.WriteLogOfTxt(ex.Message);
            }
            return NewIncident;
        }

        /// <summary>
        /// 提交任务
        /// </summary>
        /// <param name="ProcessName">流程名称</param>
        /// <param name="Incident">实例编号</param>
        /// <param name="StepName">步骤名称</param>
        /// <param name="UserAccount">用户账号</param>
        /// <param name="Summary">流程摘要</param>
        /// <param name="variable">参数泛型集合</param>
        /// <returns>返回实例编号</returns>
        public int Send(string ProcessName, string Incident, string StepName, string UserAccount, string Summary, List<VariableInfo> variable)
        {
            int NewIncident = 0; string SendFormError = "";
            try
            {
                Task task = pfc.InitTask(ProcessName, 0, StepName, UserAccount);
                Variable[] variables = pfc.GetVariable(variable);
                if (task.Send(variables, "", Summary, ref NewIncident, out SendFormError))
                {
                    pfc.WriteLogOfTxt("ProcessName:[" + ProcessName + "],Incident:[" + NewIncident + "],StepName:[" + StepName + "],UserAccount:[" + UserAccount + "],Summary:[" + Summary + "] 提交任务成功!");
                }
                else
                {
                    throw new Exception("ProcessName:[" + ProcessName + "],Incident:[" + NewIncident + "],StepName:[" + StepName + "],UserAccount:[" + UserAccount + "],Summary:[" + Summary + "] " + SendFormError);
                }
            }
            catch (Exception ex)
            {
                pfc.WriteLogOfTxt(ex.Message);
            }
            return NewIncident;
        }

        /// <summary>
        /// 提交任务
        /// </summary>
        /// <param name="TaskID">TaskID</param>
        /// <param name="Summary">流程摘要</param>
        /// <param name="variable">参数泛型集合</param>
        /// <returns>返回实例编号</returns>
        public int Send(string TaskID, string Summary, List<VariableInfo> variable)
        {
            int NewIncident = 0; string SendFormError = "";
            try
            {
                Task task = pfc.InitTask(TaskID);
                Variable[] variables = pfc.GetVariable(variable);
                if (task.Send(variables, "", Summary, ref NewIncident, out SendFormError))
                {
                    pfc.WriteLogOfTxt("ProcessName:[" + task.strProcessName + "],Incident:[" + NewIncident + "],StepName:[" + task.strStepName + "],UserAccount:[" + task.strUser + "],Summary:[" + Summary + "] 提交任务成功!");
                }
                else
                {
                    throw new Exception("ProcessName:[" + task.strProcessName + "],Incident:[" + NewIncident + "],StepName:[" + task.strStepName + "],UserAccount:[" + task.strUser + "],Summary:[" + Summary + "] " + SendFormError);
                }
            }
            catch (Exception ex)
            {
                pfc.WriteLogOfTxt(ex.Message);
            }
            return NewIncident;
        }

        private bool IsCreate(EntityLibrary.Tasks item)
        {
            bool flag = false;
            List<EntityLibrary.MobileClient_Process> list = ProessDAL.GetModel("ProcessName='" + item.ProcessName.Trim() + "'");
            EntityLibrary.MobileClient_Process model = new MobileClient_Process();
            if (list.Count > 0)
            {
                model = list[0];
                List<EntityLibrary.MobileClient_Step> steplist = StepDAL.GetModel("FK_ID='" + model.ID + "'");

                foreach (EntityLibrary.MobileClient_Step s in steplist)
                {
                    if (s.StepName.Trim() == item.StepName.Trim())
                    {
                        string str = ConfigurationManager.AppSettings["CreatePagePath"].ToString().Trim();
                        str = HttpContext.Current.Server.MapPath(str);
                        str = str + @"\" + s.ID.ToString() + ".aspx";
                        if (File.Exists(str))
                            flag = true;
                    }
                }
            }
            
            return flag;
        }

    }
}
