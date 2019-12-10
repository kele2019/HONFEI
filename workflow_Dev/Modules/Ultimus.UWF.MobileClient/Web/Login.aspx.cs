using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MobileClient.PublicFunctionClass;
using ClientService;
//using MobileClient.MobileClientBackgroundRef;

namespace MobileClient
{
    public partial class Login : System.Web.UI.Page
    {
        WorkflowRef services = new WorkflowRef();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Language"] == null)
            {
                string Language = "zh-CN";
                Session["Language"] = Language;
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(Language);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Language);
            }
            if (!IsPostBack)
            {
                if (Request.Cookies["userName"] != null)
                {
                    string str = Request.Cookies["userName"].Value; txtAccount.Text = str;
                }
                txtAccount.Focus();
                BingDomainList();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (!IsPostBack)
                Session.Clear();
            base.OnInit(e);
        }

        private void BingDomainList()
        {
            try
            {
                string[] Domains = services.GetDomainList();
                for (int i = 0; i < Domains.Length; i++)
                {
                    dropDomain.Items.Insert(i, new ListItem(Domains[i], Domains[i]));
                }
            }
            catch (Exception ex)
            {
                PublicClass.ShowMessage(this.Page, Resources.Resource.Login_ErrorMessage1);
                PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string UserAccount = txtAccount.Text;
                string PassWord = txtPassWord.Text;
                string Domain = dropDomain.SelectedValue.Trim();
                string account=Domain + "/" + UserAccount;
                bool info = services.CheckUser(account, PassWord);
                if (info)
                {
                    Session["UserInfo"] = services.GetUserEntity(account);
                    Session["Account"] = account;
                    Response.Cookies["username"].Value = UserAccount;
                    Response.Cookies["username"].Expires = DateTime.MaxValue;
                    Response.Redirect("ToDoTask.aspx");
                }
                else
                {
                    PublicClass.ShowMessage(this.Page, Resources.Resource.Login_ErrorMessage3);
                }
            }
            catch (Exception ex)
            {
                PublicClass.ShowMessage(this.Page, Resources.Resource.Login_ErrorMessage2);
                PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

        protected override void InitializeCulture()
        {
            string Language = Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"].ToString().Split(',')[0];
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(Language);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Language);
            base.InitializeCulture();
        }

        protected void btncn_Click(object sender, EventArgs e)
        {
            string Language = "zh-CN";
            Session["Language"] = Language;
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(Language);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Language);
        }

        protected void btnen_Click(object sender, EventArgs e)
        {
            string Language = "en-us";
            Session["Language"] = Language;
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(Language);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Language);
        }

    }
}