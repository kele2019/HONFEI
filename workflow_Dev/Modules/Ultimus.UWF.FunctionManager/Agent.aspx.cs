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
    public partial class Agent : System.Web.UI.Page
    {
       AgentLogic agent = new AgentLogic();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string agentId = Request.QueryString["agentId"];
                if(agentId != null)
                {
                    agent.deleteAgent(int.Parse(agentId));
                }
                List<AgentEntity> lists = agent.GetAgentList();
                rptAgent.DataSource = lists;
                rptAgent.DataBind();
            }
        }

        protected string GetUpdateAgentUrl(string agentId)
        {
            string url = "UpdateAgent.aspx?agentId={0}";
            url = string.Format(url,agentId);
            return url;
        }
        protected string DeleteAgent(string agentId)
        {
            string url = "Agent.aspx?agentId={0}";
            url = string.Format(url, agentId);
            return url;
        }
        protected void btnAddAgent_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddAgent.aspx");
        }
       
    }
}