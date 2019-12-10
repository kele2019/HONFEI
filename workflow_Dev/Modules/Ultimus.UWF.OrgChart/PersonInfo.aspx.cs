using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.OrgChart.Interface;
using MyLib;
using Ultimus.UWF.OrgChart.Entity;
using Ultimus.UWF.Common.Logic;
using System.DirectoryServices;

namespace Ultimus.UWF.OrgChart
{
    public partial class PersonInfo : System.Web.UI.Page
    {
        IOrg _org = ServiceContainer.Instance().GetService<IOrg>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserEntity user = _org.GetUserEntity(SessionLogic.GetLoginName());
                lblAccount.Text = user.LOGINNAME;


                lblName.Text = user.USERNAME;
                lblDepartment.Text = user.DEPARTMENT;
                lblDirectReport.Text = user.DIRECTREPORTNAME;
                lblTitle.Text = user.JOBFUNCTION;
                txtEmail.Text = user.EMAIL;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bool flag=false;
            if (!string.IsNullOrEmpty(txtEmail.Text))
            {
                Ultimus.OC.OrgChart ochart=new Ultimus.OC.OrgChart();
                string error="";
                flag = ochart.SetEmailAddress(lblAccount.Text, txtEmail.Text, out error);
                
            }

            

            if (!string.IsNullOrEmpty(txtPwd.Text))
            {
                try
                {
                    if (txtPwd.Text != txtPwd2.Text)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "aa", "alert('密码和重复密码必须一致！');", true);
                        return;
                    }
                    DirectoryEntry myDirectoryEntry;

                    myDirectoryEntry = new DirectoryEntry(ConfigurationManager.AppSettings["ADPath"] + lblAccount.Text + ",User");

                    myDirectoryEntry.Invoke("setPassword", txtPwd.Text);

                    myDirectoryEntry.CommitChanges();
                    flag = true;
                }
                catch
                {
                    throw new Exception("Password or Directory Path config error!");
                }

            }

            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "aa", "alert('保存成功');", true);
            }
        }
    }
}