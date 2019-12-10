using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ultimus.UWF.FunctionManager.Entity;

namespace Ultimus.UWF.FunctionManager.Interface
{
    interface IAgent
    {
        List<AgentEntity> GetAgentList();
        void addAgentEntity(AgentEntity agent);
        void updateAgent(AgentEntity agent);
        void deleteAgent(int USERID);
        AgentEntity getAgentById(int USERID);
    }
}