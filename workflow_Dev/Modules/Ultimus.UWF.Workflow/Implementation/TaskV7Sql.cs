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

namespace Ultimus.UWF.Workflow.Implementation
{
    public class TaskV7Sql : TaskV7
    {
        public override PageEntity GetMyTask(FilterEntity filter)
        {
            string sql = @"WITH CTE(        
                    ROWNO,
                    TASKID,
                    PROCESSNAME,
                    INCIDENT,
                    SUMMARY,
                    INITIATOR,
                    STEPID,
                    STEPLABEL,
                    TASKUSER,
                    ASSIGNEDTOUSER,
                    STATUS,
                    SUBSTATUS,
                    STARTTIME,
                    ENDTIME,
                    PROCESSSTATUS,
                    OVERDUETIME
                )
                AS
                (
                    SELECT ROW_NUMBER() OVER(ORDER BY @Sort) AS ROWNO,
                    TASKID,
                    a.PROCESSNAME,
                    a.INCIDENT,
                    b.SUMMARY,
                    b.INITIATOR,
                    a.STEPID,
                    a.STEPLABEL,
                    a.TASKUSER,
                    a.ASSIGNEDTOUSER,
                    a.STATUS,
                    a.SUBSTATUS,
                    a.STARTTIME,
                    a.ENDTIME,
                    b.STATUS as PROCESSSTATUS,
                    a.OVERDUETIME
                    FROM TASKS a
                    INNER JOIN INCIDENTS b  ON a.PROCESSNAME = b.PROCESSNAME AND a.INCIDENT = b.INCIDENT
                    WHERE a.STATUS=1 AND a.ASSIGNEDTOUSER='" + filter.LoginName + "' ";
            sql += filter.SqlParamsWhere + ") SELECT * FROM CTE WHERE ROWNO BETWEEN " + ConvertUtil.ToInt32(filter.PageIndex) + " AND " + ConvertUtil.ToInt32(filter.PageIndex * filter.PageSize);

            DataAccess hlp = new DataAccess("UltDB");
            sql = sql.Replace("@Sort", filter.OrderBy);
            DbCommand cmd = hlp.CreateCommand(sql);
            if (filter.SqlParams != null)
            {
                foreach (VarEntity varEnt in filter.SqlParams)
                {
                    hlp.AddInParameter(cmd, "@" + varEnt.Name, DbType.String, varEnt.Value);
                }
            }
            PageEntity pageEntity = new PageEntity();
            pageEntity.TaskDatas = hlp.ExecuteList<TaskEntity>(cmd);
            pageEntity.Count = GetMyTaskCount(filter);
            return pageEntity;
        }

        public override int GetMyTaskCount(FilterEntity filter)
        {
            string sql = @"
                    SELECT 
                    COUNT(1)
                    FROM TASKS a
                    INNER JOIN INCIDENTS b  ON a.PROCESSNAME = b.PROCESSNAME AND a.INCIDENT = b.INCIDENT
                    WHERE a.STATUS=1 AND a.ASSIGNEDTOUSER='" + filter.LoginName + "'";
            sql += filter.ParseSqlParameterToString();

            DataAccess hlp = new DataAccess("UltDB");
            DbCommand cmd = hlp.CreateCommand(sql);
            if (filter.SqlParams != null)
            {
                foreach (VarEntity varEnt in filter.SqlParams)
                {
                    hlp.AddInParameter(cmd, "@" + varEnt.Name, DbType.String, varEnt.Value);
                }
            }
            object obj = hlp.ExecuteScalar(cmd);
            return ConvertUtil.ToInt32(obj);
        }

        public override PageEntity GetMyApproval(FilterEntity filter)
        {
            string sql = @"WITH CTE(        
                    ROWNO,
                    TASKID,
                    PROCESSNAME,
                    INCIDENT,
                    SUMMARY,
                    INITIATOR,
                    STEPID,
                    STEPLABEL,
                    TASKUSER,
                    ASSIGNEDTOUSER,
                    STATUS,
                    SUBSTATUS,
                    STARTTIME,
                    ENDTIME,
                    PROCESSSTATUS,
                    OVERDUETIME
                )
                AS
                (
                    SELECT ROW_NUMBER() OVER(ORDER BY @Sort) AS ROWNO,
                    TASKID,
                  a.PROCESSNAME,
                  a.INCIDENT,
                  b.SUMMARY,
                  b.INITIATOR,
                  a.STEPID,
                  a.STEPLABEL,
                  a.TASKUSER,
                  a.ASSIGNEDTOUSER,
                  a.STATUS,
                  a.SUBSTATUS,
                  a.STARTTIME,
                  a.ENDTIME,
                  b.STATUS as PROCESSSTATUS,
                  a.OVERDUETIME
                  FROM TASKS a
                  INNER JOIN INCIDENTS b  ON a.PROCESSNAME = b.PROCESSNAME AND a.INCIDENT = b.INCIDENT
                  INNER JOIN PROCESSSTEPS c  ON a.PROCESSNAME=c.PROCESSNAME and a.PROCESSVERSION=c.PROCESSVERSION and a.STEPID=c.STEPID
                  WHERE c.STEPTYPE=4 AND a.STATUS in (3,4,7) AND ASSIGNEDTOUSER='" + filter.LoginName + "' ";
            sql += filter.ParseSqlParameterToString() + ") SELECT * FROM CTE WHERE ROWNO BETWEEN " + ConvertUtil.ToInt32(filter.PageIndex)
                + " AND " + ConvertUtil.ToInt32(filter.PageIndex * filter.PageSize);

            DataAccess hlp = new DataAccess("UltDB");
            sql = sql.Replace("@Sort", filter.OrderBy);
            DbCommand cmd = hlp.CreateCommand(sql);
            if (filter.SqlParams != null)
            {
                foreach (VarEntity varEnt in filter.SqlParams)
                {
                    hlp.AddInParameter(cmd, "@" + varEnt.Name, DbType.String, varEnt.Value);
                }
            }
            PageEntity pageEntity = new PageEntity();
            pageEntity.TaskDatas = hlp.ExecuteList<TaskEntity>(cmd);
            pageEntity.Count = GetMyTaskCount(filter);
            return pageEntity;
        }

        public override int GetMyApprovalCount(FilterEntity filter)
        {
            string sql = @"
                    SELECT 
                    COUNT(1)
                    FROM TASKS a
                  INNER JOIN INCIDENTS b  ON a.PROCESSNAME = b.PROCESSNAME AND a.INCIDENT = b.INCIDENT
                  INNER JOIN PROCESSSTEPS c  ON a.PROCESSNAME=c.PROCESSNAME and a.PROCESSVERSION=c.PROCESSVERSION and a.STEPID=c.STEPID
                  WHERE c.STEPTYPE=4 AND a.STATUS in (3,4,7) AND ASSIGNEDTOUSER='" + filter.LoginName + "' ";
            sql += filter;

            DataAccess hlp = new DataAccess("UltDB");
            DbCommand cmd = hlp.CreateCommand(sql);
            if (filter.SqlParams != null)
            {
                foreach (VarEntity varEnt in filter.SqlParams)
                {
                    hlp.AddInParameter(cmd, "@" + varEnt.Name, DbType.String, varEnt.Value);
                }
            }
            object obj = hlp.ExecuteScalar(cmd);
            return ConvertUtil.ToInt32(obj);
        }

        public override PageEntity GetMyRequest(FilterEntity filter)
        {
            string sql = @"WITH CTE(        
                    ROWNO,
                    TASKID,
                    PROCESSNAME,
                    INCIDENT,
                    SUMMARY,
                    INITIATOR,
                    STEPID,
                    STEPLABEL,
                    TASKUSER,
                    ASSIGNEDTOUSER,
                    STATUS,
                    SUBSTATUS,
                    STARTTIME,
                    ENDTIME,
                    PROCESSSTATUS,
                    OVERDUETIME
                )
                AS
                (
                    SELECT ROW_NUMBER() OVER(ORDER BY @Sort) AS ROWNO,
                    TASKID,
                  a.PROCESSNAME,
                  a.INCIDENT,
                  b.SUMMARY,
                  b.INITIATOR,
                  a.STEPID,
                  a.STEPLABEL,
                  a.TASKUSER,
                  a.ASSIGNEDTOUSER,
                  a.STATUS,
                  a.SUBSTATUS,
                  a.STARTTIME,
                  a.ENDTIME,
                  b.STATUS as PROCESSSTATUS,
                  a.OVERDUETIME
                  FROM TASKS a
                  INNER JOIN INCIDENTS b  ON a.PROCESSNAME = b.PROCESSNAME AND a.INCIDENT = b.INCIDENT
                  INNER JOIN PROCESSSTEPS c  ON a.PROCESSNAME=c.PROCESSNAME and a.PROCESSVERSION=c.PROCESSVERSION and a.STEPID=c.STEPID AND c.STEPTYPE=2
                  WHERE 1=1 AND (ASSIGNEDTOUSER='" + filter.LoginName + "') ";
            sql += filter.ParseSqlParameterToString() + ") SELECT * FROM CTE WHERE ROWNO BETWEEN " + ConvertUtil.ToInt32(filter.PageIndex) + " AND " + ConvertUtil.ToInt32(filter.PageIndex * filter.PageSize);

            DataAccess hlp = new DataAccess("UltDB");
            sql = sql.Replace("@Sort", filter.OrderBy);
            DbCommand cmd = hlp.CreateCommand(sql);
            if (filter.SqlParams != null)
            {
                foreach (VarEntity varEnt in filter.SqlParams)
                {
                    hlp.AddInParameter(cmd, "@" + varEnt.Name, DbType.String, varEnt.Value);
                }
            }
            PageEntity pageEntity = new PageEntity();
            pageEntity.TaskDatas = hlp.ExecuteList<TaskEntity>(cmd);
            pageEntity.Count = GetMyRequestCount(filter);
            return pageEntity;
        }

        public override int GetMyRequestCount(FilterEntity filter)
        {
            string sql = @"
                    SELECT 
                    COUNT(1)
                    FROM TASKS a
                  INNER JOIN INCIDENTS b  ON a.PROCESSNAME = b.PROCESSNAME AND a.INCIDENT = b.INCIDENT
                  INNER JOIN PROCESSSTEPS c  ON a.PROCESSNAME=c.PROCESSNAME and a.PROCESSVERSION=c.PROCESSVERSION and a.STEPID=c.STEPID AND c.STEPTYPE=2
                  WHERE 1=1 AND (ASSIGNEDTOUSER='" + filter.LoginName + "') ";
            sql += filter.ParseSqlParameterToString();

            DataAccess hlp = new DataAccess("UltDB");
            DbCommand cmd = hlp.CreateCommand(sql);
            if (filter.SqlParams != null)
            {
                foreach (VarEntity varEnt in filter.SqlParams)
                {
                    hlp.AddInParameter(cmd, "@" + varEnt.Name, DbType.String, varEnt.Value);
                }
            }
            object obj = hlp.ExecuteScalar(cmd);
            return ConvertUtil.ToInt32(obj);
        }
    }
}