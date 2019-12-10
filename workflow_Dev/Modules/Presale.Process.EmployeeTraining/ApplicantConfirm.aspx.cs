using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Data;
using Ultimus.UWF.Form.ProcessControl;

namespace Presale.Process.EmployeeTraining
{
    public partial class ApplicantConfirm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
            ((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
        }
        protected void NewRequest_BeforeSubmit(object sender, System.ComponentModel.CancelEventArgs g)
        {
            string Incident = Request.QueryString["Incident"];
            string Strsql="";
            foreach (RepeaterItem item in RPList.Items)
            {
                CheckBox CBUser=(item.FindControl("User3")) as CheckBox;
                HiddenField hdUserID = item.FindControl("hdUserID") as HiddenField;
                if(CBUser.Checked)
                {
//                 Strsql+=@" update dbo.COM_EmployeeTrainSignInfo  set SumDate=null 
//  where UserID='"+hdUserID.Value+"' and TrainDocmentNo=(select DOCUMENTNO from  PROC_EmployeeTraining where  incident='"+Incident+"')";
               
                }
                else
                {
                    Strsql += @" update dbo.COM_EmployeeTrainSignInfo  set SumDate='0',TrainDate=null
  where UserID='" +hdUserID.Value+"' and TrainDocmentNo=(select DOCUMENTNO from  PROC_EmployeeTraining where  incident='"+Incident+"')";

                    Strsql += " DELETE FROM PROC_TrainingEvaluation where FORMID=(select FORMID from  PROC_EmployeeTraining where  incident='"+Incident+"') and USERID=(SELECT LOGINNAME FROM V_ORG_USER WHERE USERID='"+hdUserID.Value+"')";
                    Strsql += " DELETE FROM PROC_TrainingAnswer WHERE FORMID=(select FORMID from  PROC_EmployeeTraining where  incident='" + Incident + "') AND UserAccount='" + hdUserID.Value + "'";
                }
            }
            DataAccess.Instance("BizDB").ExecuteNonQuery(Strsql);
        }
        protected void RPList_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Incident = Request.QueryString["Incident"];
                string Strsql = @" select B.EXT04,A.SumDate,A.USERID from (
  select UserID,SumDate from COM_EmployeeTrainSignInfo where TrainDocmentNo=( select DOCUMENTNO from  PROC_EmployeeTraining where  incident='" + Incident + "') )A left join ORG_USER B on A.UserID=B.USERID";
                DataTable dtUserInfo = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
                RPList.DataSource = dtUserInfo;
                RPList.DataBind();
            }
        }
        
    }
}