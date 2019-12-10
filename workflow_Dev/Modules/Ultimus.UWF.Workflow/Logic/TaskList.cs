using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using MyLib;
using Ultimus.UWF.Workflow.Interface;
using Ultimus.UWF.Workflow.Entity;

using Newtonsoft.Json;namespace Ultimus.UWF.Workflow.Logic
{
    /// <summary>
    /// 获取任务数据
    /// </summary>
    public class TaskList
    {
        /// <summary>
        /// 得到所有发起流程
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <returns>返回对象</returns>
        public List<ProcessEntity> GetTaskAllInitiate(string loginName) {
            DataTable dt = new DataTable();
            try {
                ITask task = ServiceContainer.Instance().GetService<ITask>();
                return task.GetInitProcessList(loginName);
            }
            catch (Exception ex) {
                LogUtil.Info("TaskList.GetTaskAllInitiate获取任务信息失败！错误信息：" + ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 获取任务数据集合
        /// </summary>
        /// <param name="taskInfo">任务对象</param>
        /// <returns></returns>
        public PageEntity GetTaskList(FilterEntity taskInfo) {            
            try {
                ITask task = ServiceContainer.Instance().GetService<ITask>();
                return task.GetMyTask(taskInfo);
            }
            catch (Exception ex) {
                LogUtil.Info("TaskList.GetTaskAllInitiate获取任务信息失败！错误信息：" + ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 获取任务数据集合
        /// </summary>
        /// <param name="taskInfo">任务对象</param>
        /// <returns></returns>
        public PageEntity GetMyTaskList(FilterEntity taskInfo) {            
            try {
                ITask task = ServiceContainer.Instance().GetService<ITask>();
                return task.GetMyApproval(taskInfo);
            }
            catch (Exception ex) {
                LogUtil.Info("TaskList.GetTaskAllInitiate获取任务信息失败！错误信息：" + ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 获取任务数据集合Json字符串
        /// </summary>
        /// <param name="taksInfo">任务对象</param>
        /// <returns></returns>
        public string GetTaskListStr(FilterEntity taksInfo) {
            return JsonConvert.SerializeObject(this.GetTaskList(taksInfo));
        }
        /// <summary>
        /// 获取任务数据集合Json字符串
        /// </summary>
        /// <param name="taksInfo">任务对象</param>
        /// <returns></returns>
        public string GetMyTaskListStr(FilterEntity taksInfo) {
            return JsonConvert.SerializeObject(this.GetMyTaskList(taksInfo));
        }
        /// <summary>
        /// 获取申请的流程
        /// </summary>
        /// <param name="loginName">登录名称</param>
        /// <returns></returns>
        public string GetProcessApplyStr(string loginName) {
            return JsonConvert.SerializeObject(this.GetTaskAllInitiate(loginName));
        }
        /// <summary>
        /// 获取流程步骤的URL地址
        /// </summary>
        /// <param name="taskId">任务编号</param>
        /// <param name="type">地址参数</param>
        /// <param name="userName">登录名</param>
        /// <returns></returns>
        public string GetProcessStepUrl(string taskId,string type,string userName) {
            ITask task = ServiceContainer.Instance().GetService<ITask>();
            return task.GetTaskUrl(taskId, type, userName);
        }
    }
}
