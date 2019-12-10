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
    public partial class UpdateAgent : System.Web.UI.Page
    {
        AgentLogic _agent = new AgentLogic();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string agentId = Request.QueryString["agentId"];
                updateAgentId.Text = agentId;
                AgentEntity agent = _agent.getAgentById(int.Parse(agentId));
                updateAgentName.Text = agent.USERNAME;
            }
        }

        protected void btnUpdatePwd_Click(object sender, EventArgs e)
        {
            string agentId = updateAgentId.Text;
            AgentEntity agent = _agent.getAgentById(int.Parse(agentId));
            agent.PASSWORD = Ultimus.UWF.Security.Logic.SecurityLogic.GetMd5(updateAgentPwd.Text.Trim(), 16);
            _agent.updateAgent(agent);
            Response.Redirect("Agent.aspx");
        }

        protected void btnReturnAgent_Click(object sender, EventArgs e)
        {
            Response.Redirect("Agent.aspx");
        }
    }
}