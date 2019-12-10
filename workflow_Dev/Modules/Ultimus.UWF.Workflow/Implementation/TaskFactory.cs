using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ultimus.UWF.Workflow.Interface;
using MyLib;
using System.Data;

namespace Ultimus.UWF.Workflow.Implementation
{
    public class TaskFactory
    {
        //TODO:Itask[] Factory 返回数组
        public static ITask[] createTask()
        {
            DataAccess hlp = new DataAccess("BizDB");
            String sql = "select NAME, VALUE from COM_APPSETTINGS c where c.MODULE = 'Ultimus.UWF.Workflow' and c.NAME like 'Ult_Service_%' and ISACTIVE = 1";
            DataTable dt = hlp.ExecuteDataTable(sql);
            ITask[] tasks = new ITask[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tasks[i] = findItask(dt.Rows[i]["NAME"] + "", dt.Rows[i]["VALUE"] + "");
            }
            return tasks;
        }

        private static ITask findItask(string name, string value)
        {
            //if (name.IndexOf("V7") >= 0)
            //{
            //    if (value.ToUpper().IndexOf("ORACLE") >= 0)
            //    {
            //        return new TaskV7Oracle();//未实现
            //    }
            //    else if (value.ToUpper().IndexOf("SQLSERVER") >= 0)
            //    {
            //        return new TaskV7Sql();//未实现
            //    }
            //}
            //else if (name.IndexOf("V8") >= 0)
            //{
            //    if (value.ToUpper().IndexOf("ORACLE") >= 0)
            //    {
            //        return new TaskV8Oracle();//未实现
            //    }
            //    else if (value.ToUpper().IndexOf("SQLSERVER") >= 0)
            //    {
            //        return new TaskV8Sql(); //已实现
            //    }
            //}
            return new TaskV8Sql();
        }
    }
}