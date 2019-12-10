using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Common.Logic;
using Ultimus.UWF.Security.Interface;
using MyLib;
using System.DirectoryServices;

namespace Ultimus.UWF.Home2
{
    public partial class Login : System.Web.UI.Page
    {
        IAuthentication _auth = ServiceContainer.Instance().GetService<IAuthentication>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionLogic.LogOut();
                hidReturnUrl.Value = Request.QueryString["ReturnUrl"];
                List<string> strs = _auth.GetDomains();
                //List<string> strs1 = new List<string>();
                //for (int i = strs.Count - 1; i >= 0; i--)
                //{
                //    strs1.Add(strs[i]);
                //}
                ddlDomains.DataSource = strs;
                ddlDomains.DataBind();

                //btnSubmit.Text = Lang.Get("Login_Login");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string user = txtUser.Text;
                object txtUserInfo = DataAccess.Instance("BizDB").ExecuteScalar("select LOGINNAME from ORG_USER where USERCODE='" + user + "'");
                if (txtUserInfo != null)
                {
                    if (user.IndexOf("\\") < 0)
                    {
                        user = txtUserInfo.ToString();
                    }

                    if (ConfigurationManager.AppSettings["ADSwitch"].ToString() == "true")
                    {
                        if (GetADUserEntity(txtUser.Text, txtPassword.Text))
                        {
                            SessionLogic.Login(user);
                            LogUtil.Info(typeof(Login), "Login, IP:" + Request.UserHostAddress);
                            Response.Redirect("Index.aspx");
                        }
                        else
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "ce", "checkError();", true);
                    }
                    else
                    {
                        SessionLogic.Login(user);
                        Response.Redirect("Index.aspx");
                       // btnSubmit.Text = user;
                    }

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ce", "checkError();", true);
                }
            }
            catch (Exception ex)
            { 
            
            }

           
            //string user = txtUser.Text;
            //if (user.IndexOf("\\") < 0)
            //{
            //    user = ddlDomains.SelectedValue + "\\" + txtUser.Text;
            //}

            //if (!_auth.CheckUser(user, txtPassword.Text))
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "ce", "checkError();", true);
            //}
            //else
            //{
            //    SessionLogic.Login(user);
            //    LogUtil.Info(typeof(Login), "Login, IP:" + Request.UserHostAddress);
            //    Response.Redirect("Index.aspx");
            //}
        }
        private bool GetADUserEntity(string userID, string password)
        {
            try
            {
                DirectoryEntry entry = new DirectoryEntry(System.Configuration.ConfigurationManager.AppSettings["ADRootGroupPath"]) { Username = userID, Password = password };
                DirectorySearcher searcher = new DirectorySearcher(entry);
                searcher.Filter = "(objectclass=user)";
                SearchResult sr = searcher.FindOne();
                DirectoryEntry adUserInfo = sr.GetDirectoryEntry();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}