using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.FunctionManager.Logic;
using Ultimus.UWF.FunctionManager.Entity;

namespace Ultimus.UWF.FunctionManager
{
    public partial class AddAgent : System.Web.UI.Page
    {
        AgentLogic _agent = new AgentLogic();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddAgent_Click(object sender, EventArgs e)
        {
            AgentEntity agent = new AgentEntity();
            agent.USERNAME = agentUserName.Text;
            agent.LOGINNAME = "FAREAST" + "\\" + agentLoginName.Text;
            agent.PASSWORD = Ultimus.UWF.Security.Logic.SecurityLogic.GetMd5(agentPwd.Text.Trim(), 16);
            _agent.addAgentEntity(agent);
            Response.Redirect("Agent.aspx");
        }
    }
}