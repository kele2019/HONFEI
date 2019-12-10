using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.IO;
using EntityLibrary;
using Ultimus;
using Ultimus.OC;
using Ultimus.WFServer;

namespace UltimusEikLibrary
{
    public class PublicFunctionClass
    {
        /// <summary>
        /// 根据Task对象得到一个实体类
        /// </summary>
        /// <param name="task">Task对象</param>
        /// <returns>TaskEntity</returns>
        public EntityLibrary.Tasks GetEntityOfTask(Task task)
        {
            EntityLibrary.Tasks item = new EntityLibrary.Tasks();
            item.TaskID = task.strTaskId;
            item.ProcessName = task.strProcessName;
            item.Incident = task.nIncidentNo;
            item.StepName = task.strStepName;
            item.Summary = task.strSummary;
            item.StartTime = task.dStartTime;
            item.EndTime = task.dEndTime;
            return item;
        }

        /// <summary>
        /// 获得传入电子表格的参数数组
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        public Variable[] GetVariable(List<VariableInfo> variable)
        {
            Variable[] variables = new Variable[variable.Count];
            for (int i=0;i<variable.Count;i++)
            {
                Variable var = new Variable();
                var.strVariableName = variable[i].Name;
                var.objVariableValue = variable[i].Value.ToArray();
                variables.SetValue(var, i);
            }
            return variables;
        }

        /// <summary>
        /// 初始化Task对象
        /// </summary>
        /// <param name="ProcessName">流程名称</param>
        /// <param name="Incident">实例编号</param>
        /// <param name="StepName">步骤名称</param>
        /// <param name="UserName">用户账号</param>
        /// <returns>Task</returns>
        public Task InitTask(string ProcessName, int Incident, string StepName, string UserName)
        {
            Tasklist list = new Tasklist();
            TasklistFilter filter = new TasklistFilter();
            filter.strProcessNameFilter = ProcessName;
            filter.strStepLabelFilter = StepName;
            filter.nIncidentNo = Incident;
            filter.strArrUserName = new string[] { UserName };
            list.LoadFilteredTasks(filter);
            return list.GetNextTask();
        }

        /// <summary>
        /// 初始化Task对象
        /// </summary>
        /// <param name="ProcessName">流程名称</param>
        /// <param name="Incident">实例编号</param>
        /// <param name="StepName">步骤名称</param>
        /// <returns>Task</returns>
        public Task InitTask(string ProcessName, int Incident, string StepName)
        {
            Tasklist list = new Tasklist();
            TasklistFilter filter = new TasklistFilter();
            filter.strProcessNameFilter = ProcessName;
            filter.strStepLabelFilter = StepName;
            filter.nIncidentNo = Incident;
            list.LoadFilteredTasks(filter);
            return list.GetNextTask();
        }

        /// <summary>
        /// 初始化Task对象
        /// </summary>
        /// <param name="TaskID">TaskID</param>
        /// <returns>Task</returns>
        public Task InitTask(string TaskID)
        {
            Task task = new Task();
            task.InitializeFromTaskId(TaskID);
            return task;
        }

        /// <summary>
        /// 输出日志到文本
        /// </summary>
        /// <param name="Message">输出内容</param>
        public void WriteLogOfTxt(string Message)
        {
            try
            {
                string filePath = HttpContext.Current.Server.MapPath("SystemLog/" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".txt");
                FileInfo f = new FileInfo(filePath);
                FileStream fs = f.Open(FileMode.OpenOrCreate);
                byte[] b = Encoding.UTF8.GetBytes(DateTime.Now.ToString().Replace("/", "-") + ":" + Message.Replace("\r\n", " ").Replace("\n", " ") + "\r\n");
                fs.Write(b, 0, b.Length);
                fs.Close();
                fs.Dispose();
            }
            catch { }
        }

    }
}
