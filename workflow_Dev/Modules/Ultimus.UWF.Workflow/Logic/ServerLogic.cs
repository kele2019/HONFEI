using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ultimus.UWF.Workflow.Interface;
using Ultimus.UWF.Workflow.Entity;
using MyLib;
using Ultimus.UWF.Common.Interface;

namespace Ultimus.UWF.Workflow.Logic
{
    public class ServerLogic
    {
        static List<ServerEntity> _list = null;
        static object obj = new object();
        public static List<ServerEntity> GetList()
        {
            lock (obj)
            {
                if (_list == null)
                {
                    try
                    {
                        _list = DataAccess.Instance("BizDB").ExecuteList<ServerEntity>("SELECT * FROM WF_SERVER WHERE ISACTIVE=1");
                    }
                    catch
                    {
                    }
                }

                return _list;
            }
        }

        public static List<ITask> GetAllTask()
        {
            lock (obj)
            {
                List<ITask> tasks = new List<ITask>();
                List<ServerEntity> list = GetList();
                foreach (ServerEntity s in list)
                {
                    object o = ReflectUtil.CreateInstance(Type.GetType(s.CLIENTASSEMBLY));
                    if (o is ITask)
                    {
                        ITask t=(ITask)o;
                        if (t is IClientService)
                        {
                            IClientService c = t as IClientService;
                            c.ServiceUrl = s.WEBSERVICEURL;
                        }
                        t.SetServerEntity(s);
                        tasks.Add(t);
                    }
                }

                return tasks;
            }
        }

        public static string GetLoginName(string serverName, string loginName)
        {
            List<ServerEntity> list = GetList();
            ServerEntity ety=list.Find(p => p.SERVERNAME.ToUpper() == serverName.ToUpper());
            if (!string.IsNullOrEmpty(ety.DOMAINNAME))
            {
                loginName = loginName.Replace("\\", "/");
                string[] sz = loginName.Split('/');
                if (sz.Length > 1)
                {
                    if (ety != null)
                    {
                        if (string.IsNullOrEmpty(ety.DOMAINNAME))
                        {
                            return loginName;
                        }
                        return ety.DOMAINNAME + "/" + sz[1];
                    }
                }
            }
            return loginName;
        }

        //public static ITask GetTaskClass(string taskID)
        //{
        //    List<ITask> tasks = ServerLogic.GetAllTask();
        //    foreach (ITask task in tasks)
        //    {
        //        TaskEntity entity = task.GetTaskEntity(taskID);
        //        if (entity != null)
        //        {
        //            return task;
        //        }
        //    }

        //    return null;
        //}

        public static ITask GetTaskClassByServer(string serverName)
        {
            List<ITask> tasks = ServerLogic.GetAllTask();
            ITask task= tasks.Find(p => p.GetServerEntity().SERVERNAME.Equals(serverName));
            if (task == null)
            {
                throw new Exception("Can not find "+serverName+" configuration in wf_server!");
            }
            return task;
        }

        public static ServerEntity GetServerEntity(string serverName)
        {
            List<ServerEntity> list=GetList();
            if (list == null)
            {
                return new ServerEntity();
            }
            ServerEntity ety=list.Find(p => p.SERVERNAME.Equals(serverName));
            if (ety == null)
            {
                return new ServerEntity();
            }
            return ety;
        }
    }
}