using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ultimus.UWF.Home2.UC
{
    public partial class UCHeader : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBindUserInfo(Page.User.Identity.Name);
            }
        }

        public void DataBindUserInfo(string UserID)
        {
            object UserInfo = MyLib.DataAccess.Instance("BizDB").ExecuteScalar("select  USERNAME+' '+(case  when USERNAME=EXT04 then '' else EXT04 end) from  ORG_USER where  LOGINNAME='" + UserID + "'");
            if (UserInfo != null)
            {
                lbUserName.Text = UserInfo.ToString();
            }
        }
    }
}