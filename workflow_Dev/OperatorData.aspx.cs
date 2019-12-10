using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;
using System.Security.Principal;
using Ultimus.UWF.Security.Interface;
 
namespace UWF
{
    public partial class OperatorData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
				IAuthentication _auth = ServiceContainer.Instance().GetService<IAuthentication>();
                
                //DataTable dtDeptInfo = DataAccess.Instance("BizDB").ExecuteDataTable("select  DEPARTMENTNAME,* from dbo.ORG_DEPARTMENT where DEPARTMENTTYPE='region'");
                //for (int i = 0; i < dtDeptInfo.Rows.Count; i++)
                //{
                //DataTable dtTabelTestInfo = DataAccess.Instance("BizDB").ExecuteDataTable("select DeptEn,CostCenter ,DeptRejion from UserTable where ParentDept='"+dtDeptInfo.Rows[i]["DEPARTMENTNAME"].ToString()+"'");
                //    string strsql="update ORG_DEPARTMENT set EXT01='"+dtTabelTestInfo.Rows[0]["DeptEn"].ToString()+"'";
                //    strsql+=" ,EXT02='"+dtTabelTestInfo.Rows[0]["CostCenter"].ToString().Split('-')[0].ToString()+"',";
                //    strsql+=" EXT03='"+dtTabelTestInfo.Rows[0]["DeptRejion"].ToString()+"'";
                //    strsql+=" where DEPARTMENTNAME='"+dtDeptInfo.Rows[i]["DEPARTMENTNAME"].ToString()+"'and DEPARTMENTTYPE='region'";
                //    DataAccess.Instance("BizDB").ExecuteNonQuery(strsql);
                //}
                //DataTable dtDeptInfo = DataAccess.Instance("BizDB").ExecuteDataTable("select  DEPARTMENTNAME from dbo.ORG_DEPARTMENT where DEPARTMENTTYPE='Department'");
                //for (int i = 0; i < dtDeptInfo.Rows.Count; i++)
                //{
                //    DataTable dtTabelTestInfo = DataAccess.Instance("BizDB").ExecuteDataTable("select DeptEn,CostCenter  from UserTable where Dept='" + dtDeptInfo.Rows[i]["DEPARTMENTNAME"].ToString() + "'");
                //    string strsql = "update ORG_DEPARTMENT  set ";
                //    strsql += "  EXT02='" + dtTabelTestInfo.Rows[0]["CostCenter"].ToString() + "',";
                //    strsql += " EXT03='" + dtTabelTestInfo.Rows[0]["DeptEn"].ToString() + "'";
                //    strsql += " where DEPARTMENTNAME='" + dtDeptInfo.Rows[i]["DEPARTMENTNAME"].ToString() + "'and DEPARTMENTTYPE='Department'";
                //    DataAccess.Instance("BizDB").ExecuteNonQuery(strsql);
                //}
                try
                {
                    //bool fl = EmailUtil.SendMail("422987191@qq.com", "test", "test");
                    //string tet = MyLib.EncryptUtil.Encrypt("tests123456");
                    //string ete = MyLib.EncryptUtil.Decrypt(tet);
                    //MyLib.LogUtil.Error("test");
                    lbPassword.Text = this.Page.User.Identity.Name;
                    //string taskid="082907a6aaa5108094ddc72d443bb6";
                    //Response.Redirect("http://10.0.2.15:8001/Modules/Ultimus.UWF.workflow/OpenForm.aspx?TaskId=" + taskid + "&Type=mytask");
                    //Response.Redirect(ConfigurationManager.AppSettings["FromPortalGotoDefaultPageUrl"]);
#if Debug
            string str="test";
#endif
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
             
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            string Md5PWD = Ultimus.UWF.Security.Logic.SecurityLogic.GetMd5(txtChangPassword.Text.Trim(), 16);
            lbPassword.Text = Md5PWD;
        }
    }
}