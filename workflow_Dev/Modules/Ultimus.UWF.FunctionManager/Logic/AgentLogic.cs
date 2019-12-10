using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ultimus.UWF.FunctionManager.Entity;
using Ultimus.UWF.FunctionManager.Interface;
using MyLib;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Configuration;
using System.IO;

namespace Ultimus.UWF.FunctionManager.Logic
{
    public class AgentLogic : IAgent
    {
        public List<AgentEntity> GetAgentList()
        {
            List<AgentEntity> lists = DataAccess.Instance("BizDB").ExecuteList<AgentEntity>("select USERID,USERNAME,LOGINNAME,PASSWORD FROM ORG_USER where PASSWORD is not NULL;");
            return lists;
        }
        public void addAgentEntity(AgentEntity agent)
        {
            DataAccess.Instance("BizDB").ExecuteNonQuery("update ORG_USER set PASSWORD= '" + agent.PASSWORD + "'where LOGINNAME='" + agent.LOGINNAME + "' and USERNAME='" + agent.USERNAME + "';");
        }
        public void updateAgent(AgentEntity agent)
        {
            DataAccess.Instance("BizDB").ExecuteNonQuery("update ORG_USER set PASSWORD= '" + agent.PASSWORD + "' where USERID = " + agent.USERID + ";");
        }
        public void deleteAgent(int USERID)
        {
            DataAccess.Instance("BizDB").ExecuteNonQuery("delete from ORG_USER where USERID = " + USERID + ";");
        }
        public AgentEntity getAgentById(int USERID)
        {
            DataAccess db = new DataAccess("BizDB");
            StringBuilder strsql = new StringBuilder();
            strsql.Append("select * from ORG_USER where USERID=@USERID;");
            DbCommand dbcom = db.CreateCommand(strsql.ToString());
            db.AddInParameter(dbcom, "@USERID", DbType.Int32, USERID);
            DbDataReader dr = db.ExecuteReader(dbcom);
            AgentEntity agent = new AgentEntity();
            while (dr.Read())
            {
                if (dr["USERID"] != null && !String.IsNullOrEmpty(dr["USERID"].ToString()))
                {
                    agent.USERID = int.Parse(dr["USERID"].ToString());
                }
                if (dr["USERNAME"] != null && !String.IsNullOrEmpty(dr["USERNAME"].ToString()))
                {
                    agent.USERNAME = dr["USERNAME"].ToString();
                }
                if (dr["LOGINNAME"] != null && !String.IsNullOrEmpty(dr["LOGINNAME"].ToString()))
                {
                    agent.LOGINNAME = dr["LOGINNAME"].ToString();
                }
                if (dr["PASSWORD"] != null && !String.IsNullOrEmpty(dr["PASSWORD"].ToString()))
                {
                    agent.PASSWORD = dr["PASSWORD"].ToString();
                }
            }
            dr.Close();
            return agent;
        }
    }
}