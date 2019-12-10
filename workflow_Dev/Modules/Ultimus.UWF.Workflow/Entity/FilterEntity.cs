using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace Ultimus.UWF.Workflow.Entity
{
    public class FilterEntity
    {
        /// <summary>
        /// 流程名
        /// </summary>
        public string ProcessName { get; set; }
        /// <summary>
        /// 实例号
        /// </summary>
        public string Incident { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 发起流程日期的开始时间，搜索范围
        /// </summary>
        public string StratTimeBegin { get; set; }
        /// <summary>
        /// 发起流程日期的结束时间，搜索范围
        /// </summary>
        public string StratTimeEnd { get; set; }
        /// <summary>
        /// 结束流程日期的开始时间，搜索范围
        /// </summary>
        public string EndTimeBegin { get; set; }
        /// <summary>
        /// 结束流程日期的结束时间，搜索范围
        /// </summary>
        public string EndTimeEnd { get; set; }

        private string _loginName;
        /// <summary>
        /// 登录名称
        /// </summary>
        public string LoginName { get { return _loginName.Replace("\\", "/"); } set { _loginName = value; } }
        
        private int _taskStatus = -10;
        /// <summary>
        /// 状态
        /// </summary>
        public int TaskStatus { 
            get{ return _taskStatus; }
            set { _taskStatus = value; }
        }
        private int _incStatus = -10;
        /// <summary>
        /// 实例状态
        /// </summary>
        public int IncStatus
        {
            get { return _incStatus; }
            set { _incStatus = value; }
        }
        /// <summary>
        /// 排序，多字段逗号分隔
        /// </summary>
        public string OrderBy { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Where Sql 预计，请使用@XXX进行站位参数
        /// </summary>
        public string SqlParamsWhere { get; set; }
        /// <summary>
        /// Where Sql 的参数，传值并进行提交
        /// </summary>
        public List<VarEntity> SqlParams { get; set; }

        /// <summary>
        /// 将带参数Sql字符串转换成字符串
        /// </summary>
        /// <returns></returns>
        public string ParseSqlParameterToString()
        {
            string str = SqlParamsWhere;
            for (int i = 0; SqlParams != null && i < SqlParams.Count; i++)
            {
                VarEntity varEntity = SqlParams[i];
                str = str.Replace("@" + varEntity.Name, "'" + varEntity.Value + "'");
            }
            return str;
        }

        /// <summary>
        /// 获取SqlParams和所有条件的综合字符串Filter
        /// </summary>
        /// <returns></returns>
        public string GetAllFilterSql()
        {
            String paramStr = ParseSqlParameterToString();
            if(!string.IsNullOrEmpty(paramStr)){
                paramStr += " and ";
            }
            paramStr += GetFilterSql();
            return paramStr;
        }

        /// <summary>
        /// 将所有条件转换成Sql语句过滤条件
        /// </summary>
        /// <returns></returns>
        public string GetFilterSql()
        {
            String filter = "1=1 ";
            if (!string.IsNullOrEmpty(this.Incident))
            {
                filter += "and t.INCIDENT ='"+ Incident +"' ";
            }
            if(!string.IsNullOrEmpty(ProcessName)){
                filter += "and t.PROCESSNAME = '"+ ProcessName +"' ";
            }
            if(!string.IsNullOrEmpty(Summary)){
                filter += "and t.SUMMARY = '"+ Summary +"' ";
            }
            if(!string.IsNullOrEmpty(StratTimeBegin)){
                filter += "and t.STARTTIME >= '"+ this.StratTimeBegin +"' ";
            }
            if(!string.IsNullOrEmpty(StratTimeEnd)){
                filter += "and t.STARTTIME <= '"+ this.StratTimeEnd +"' ";
            }
            if(!string.IsNullOrEmpty(EndTimeBegin)){
                filter += "and t.ENDTIME >= '"+ this.EndTimeBegin +"' ";
            }
            if(!string.IsNullOrEmpty(EndTimeEnd)){
                filter += "and t.ENDTIME <= '"+ this.EndTimeEnd +"' ";
            }
            if(!string.IsNullOrEmpty(LoginName)){
                filter += "and t.TASKUSER = '"+this.LoginName+"' ";
            }
            if(TaskStatus >= -5){
                filter += "and t.STATUS = '"+this.TaskStatus+"' ";
            }
            if(IncStatus >= -5){
                filter += "and i.STATUS = '"+this.IncStatus+"' ";
            }
            return filter;
        }
    }
}