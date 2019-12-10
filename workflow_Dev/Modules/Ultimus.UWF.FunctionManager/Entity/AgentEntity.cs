using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ultimus.UWF.FunctionManager.Entity
{
    public class AgentEntity : IComparable
    {
        int _USERID;
        string _USERNAME;
        string _LOGINNAME;
        string _PASSWORD;

        public AgentEntity() { }

        public int USERID
        {
            get { return _USERID; }
            set { _USERID = value; }
        }
        public string USERNAME
        {
            get { return _USERNAME; }
            set { _USERNAME = value; }
        }
        public string LOGINNAME
        {
            get { return _LOGINNAME; }
            set { _LOGINNAME = value; }
        }
        public string PASSWORD
        {
            get { return _PASSWORD; }
            set { _PASSWORD = value; }
        }
        public int CompareTo(object obj)
        {
            AgentEntity agent = obj as AgentEntity;
            return string.Compare(this.LOGINNAME, agent.LOGINNAME);
        }
    }
}