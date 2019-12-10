using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Ultimus.UWF.Common.Implementation;

namespace Ultimus.UWF.Common
{
    public partial class ConnectionStringEncrypt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ServiceContainer.Instance().AddComponent("encrypt", typeof(IEncrypt), typeof(UWFEncrypt));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            IEncrypt encrypt = ServiceContainer.Instance().GetService<IEncrypt>();
            TextBox1.Text= encrypt.Encrypt(TextBox1.Text);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            IEncrypt encrypt = ServiceContainer.Instance().GetService<IEncrypt>();
            TextBox1.Text = encrypt.Decrypt(TextBox1.Text);
        }
    }
}