using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using System.Configuration;
using MyLib;
using System.Collections;
using System.Threading;

namespace TaskQueueService
{
    public class TaskLogic
    {
        

        public virtual string SubmitTask(string userName, string taskId, string summary, Hashtable vars, bool sync, ref int incident)
        {
            string sql = "insert into WF_TASKQUEUE (TASKID,loginname,sync,summary,varlist,action,status,retrytime,createdate) values('{0}','{1}','{2}','{3}','{4}','{5}','1',0,getdate());";
            string s = "1";
            if (!sync)
            {
                s = "0";
            }
            VarLogic logic = new VarLogic();
            List<VarEntity> list= logic.GetVarList(vars);
            string str= SerializeUtil.XMLSerialize(list);
            DataAccess.Instance("BizDB").ExecuteNonQuery(string.Format(sql,taskId,userName,s,summary,str,"SUBMITTASK"));
            return "";
        }

        public virtual string ReturnTask(string userName, string taskId, string reason, string summary, Hashtable vars, bool sync)
        {
            string sql = "insert into WF_TASKQUEUE (TASKID,loginname,sync,summary,varlist,action,reason,status,retrytime,createdate) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','1',0,getdate());";
            string s = "1";
            if (!sync)
            {
                s = "0";
            }
            VarLogic logic = new VarLogic();
            List<VarEntity> list = logic.GetVarList(vars);
            string str = SerializeUtil.XMLSerialize(list);
            DataAccess.Instance("BizDB").ExecuteNonQuery(string.Format(sql, taskId, userName, s, summary, str, "RETURNTASK",reason));
            return "";
        }

        public virtual string RejectTask(string userName, string taskId, string reason, Hashtable vars, bool sync)
        {
            string sql = "insert into WF_TASKQUEUE (TASKID,loginname,sync,reason,varlist,action,status,retrytime,createdate) values('{0}','{1}','{2}','{3}','{4}','{5}','1',0,getdate());";
            string s="1";
            if (!sync)
            {
                s = "0";
            }
            VarLogic logic = new VarLogic();
            List<VarEntity> list = logic.GetVarList(vars);
            string str = SerializeUtil.XMLSerialize(list);
            DataAccess.Instance("BizDB").ExecuteNonQuery(string.Format(sql, taskId, userName, s, reason, str, "REJECTTASK"));
            return "";
        }
         
    }
}
