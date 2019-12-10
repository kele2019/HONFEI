using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;

namespace Ultimus.UWF.Workflow
{
    public partial class AssginDetailInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string FORMID = Request.QueryString["FORMID"];

                string Strsql = @"select ( SELECT EXT04 FROM ORG_USER WHERE USERID=(select USERID from dbo.V_ORG_USER where LOGINNAME=TaskUser)) TaskUser, ( SELECT USERNAME FROM ORG_USER WHERE USERID=(select USERID from dbo.V_ORG_USER where LOGINNAME=TaskUser)) CNNAME, 
 ( SELECT EXT04 FROM ORG_USER WHERE USERID=(select USERID from dbo.V_ORG_USER where LOGINNAME=AssginTaskUser)) AssginTaskUser,( SELECT USERNAME FROM ORG_USER WHERE USERID=(select USERID from dbo.V_ORG_USER where LOGINNAME=AssginTaskUser)) AssginTaskUserCNNAME,
 AssginStartDate,AssginEndDate,Comments,CreateDate from (
select   * from dbo.COM_ASSGININFO where ID='" + FORMID+"') A";
                DataTable dtAssign = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
                if (dtAssign.Rows.Count > 0)
                {
                    lbFromName.Text = dtAssign.Rows[0]["TaskUser"].ToString();// "../../Modules/Ultimus.UWF.Form.ProcessControl/img/" + dtAssign.Rows[0]["TaskUser"].ToString() + ".png";
                    lbToUser.Text = dtAssign.Rows[0]["AssginTaskUser"].ToString();// "../../Modules/Ultimus.UWF.Form.ProcessControl/img/" + dtAssign.Rows[0]["AssginTaskUser"].ToString() + ".png";
                    lbComments.Text =ChangeComments(dtAssign.Rows[0]["Comments"].ToString(),"EN");
                    lbStartDare.Text = Convert.ToDateTime(dtAssign.Rows[0]["AssginStartDate"].ToString()).ToString("yyyy-MM-dd");
                    lbEndDare.Text = Convert.ToDateTime(dtAssign.Rows[0]["AssginEndDate"].ToString()).ToString("yyyy-MM-dd");

                    lbFromNameC.Text = dtAssign.Rows[0]["CNNAME"].ToString(); //"../../Modules/Ultimus.UWF.Form.ProcessControl/img/" + dtAssign.Rows[0]["TaskUser"].ToString() + ".png";
                    lbToUserC.Text = dtAssign.Rows[0]["AssginTaskUserCNNAME"].ToString();//"../../Modules/Ultimus.UWF.Form.ProcessControl/img/" + dtAssign.Rows[0]["AssginTaskUser"].ToString() + ".png";

                    lbCommentsC.Text = ChangeComments(dtAssign.Rows[0]["Comments"].ToString(), "CN");
                    lbStartDareC.Text = Convert.ToDateTime(dtAssign.Rows[0]["AssginStartDate"].ToString()).ToString("yyyy-MM-dd");
                    lbEndDareC.Text = Convert.ToDateTime(dtAssign.Rows[0]["AssginEndDate"].ToString()).ToString("yyyy-MM-dd");


                    lbTaskUser.ImageUrl = "../../Modules/Ultimus.UWF.Form.ProcessControl/img/" + dtAssign.Rows[0]["TaskUser"].ToString() + ".png";
                    lbCreateDate.Text = dtAssign.Rows[0]["CreateDate"].ToString();

                }
            }

        }

        private string ChangeComments(string CommentsData,string Lanager)
        {
            if (CommentsData == "All Process")
            {
                if(Lanager=="CN")
                    CommentsData = "工作流平台所有审批";
                if (Lanager == "EN")
                    CommentsData = "all process of workflow";
            }
            return CommentsData;
        }
    }
}