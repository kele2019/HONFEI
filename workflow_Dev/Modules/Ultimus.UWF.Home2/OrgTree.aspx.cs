using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Home2.Code;
using Ultimus.UWF.Common.Logic;
using MyLib;

namespace Ultimus.UWF.Home2
{
    public partial class OrgTree : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            InitOrgTree();
            if (!IsPostBack)
            {
                //DataBindUserInfo(Page.User.Identity.Name);
                //object strAgentUser = HttpContext.Current.Session["AgentUser"];
                //if (strAgentUser != null)
                //{
                //    hdFlag.Value = "1";
                //}
            }
        }
        public void DataBindUserInfo(string UserID)
        {
            //object UserInfo = MyLib.DataAccess.Instance("BizDB").ExecuteScalar("select  USERNAME+' '+(case  when USERNAME=EXT04 then '' else EXT04 end) from  ORG_USER where  LOGINNAME='" + UserID + "'");
            //if (UserInfo != null)
            //{
            //    lbUserName.Text = UserInfo.ToString();
            //}
        }
        private void InitOrgTree()
        {
            try
            {

            }
            catch (Exception)
            {

            }
        }

        protected void btnChangeUserInfo_Click(object sender, EventArgs e)
        {
            string Domain = ConfigurationManager.AppSettings["Domain"];
            SessionLogic.Login(Domain + "\\" + txtUserName.Text.Trim());
            HttpContext.Current.Session["AgentUser"] = txtUserName.Text.Trim();
            hdFlag.Value = "1";
            //string strURL=Request.Url.ToString();
            //strURL= strURL.Split('#').Length>1?strURL.Split('#')[1]:"";
            Response.Redirect("Index.aspx");
        }
    }
}