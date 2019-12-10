using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Common.Logic;
using Ultimus.UWF.Security.Interface;
using MyLib;

namespace Ultimus.UWF.Home3
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
                List<string> strs1 = new List<string>();
                for (int i = strs.Count - 1; i >= 0; i--)
                {
                    strs1.Add(strs[i]);
                }
                ddlDomains.DataSource = strs1;
                ddlDomains.DataBind();

                //btnSubmit.Text = Lang.Get("Login_Login");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text;
            if (user.IndexOf("\\") < 0)
            {
                user = ddlDomains.SelectedValue + "\\" + txtUser.Text;
            }

            if (!_auth.CheckUser(user, txtPassword.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ce", "checkError();", true);
            }
            else
            {
                SessionLogic.Login(user);
                LogUtil.Info(typeof(Login), "Login, IP:" + Request.UserHostAddress);
                Response.Redirect("Default.aspx");
            }
        }
    }
}