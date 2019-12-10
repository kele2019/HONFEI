using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;

namespace Presale.Process.EmployeeTraining
{
    public partial class AnswerDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string FormId=Request.QueryString["FormID"];
            GetDataBind(FormId);
        }

        public void GetDataBind(string FormId)
        {
             string DocmentNo = Request.QueryString["DocumentNo"];
             string Strsql = @"select  TrainingPurpose,TrainingTeacher,TrainingDuration,(convert(nvarchar(50),StartDate,111)) BeiginDate,(convert(nvarchar(50),EndDate,111)) EndDate,TrainingLocation from PROC_EmployeeTraining where  FORMID='" + FormId + "'";
            DataTable dtTraningData = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            if (dtTraningData.Rows.Count > 0)
            {
                lbTopic.Text = dtTraningData.Rows[0]["TrainingPurpose"].ToString();
                lbTrainer.Text = dtTraningData.Rows[0]["TrainingTeacher"].ToString();
                lbTrainingDur.Text = dtTraningData.Rows[0]["TrainingDuration"].ToString();
                lbTrainingDate.Text = dtTraningData.Rows[0]["BeiginDate"].ToString() + "-" + dtTraningData.Rows[0]["EndDate"].ToString();
                lbLocation.Text = dtTraningData.Rows[0]["TrainingLocation"].ToString();
            }
            string strsqlUserInfo = " select B.USERNAME,B.EXT04,USERID from   (select distinct UserAccount from PROC_TrainingAnswer where FORMID='"+FormId+"') A  left join ORG_USER B on A.UserAccount=B.USERID";
            DataTable dtUserinfo = DataAccess.Instance("BizDB").ExecuteDataTable(strsqlUserInfo);
            if (dtUserinfo.Rows.Count > 0)
                rpUserInfo.DataSource = dtUserinfo;
            else
                rpUserInfo.DataSource = null;
            rpUserInfo.DataBind();
            foreach (RepeaterItem item in rpUserInfo.Items)
	        {
                HiddenField hdUserAccount = item.FindControl("hdUserID") as HiddenField;
                Repeater rpQuestiongDetail = item.FindControl("rpQuestiongDetail") as Repeater;
                string StrsqlAnswer = @" select A.*,B.answer UAnswer ,B.UserAccount from   (select FORMID,RowIndex,Question,answer from PROC_TrainingPractise  where  FORMID='"+FormId+"')A";
                   StrsqlAnswer+=" left join (select * from  PROC_TrainingAnswer where UserAccount="+hdUserAccount.Value+")  B  on A.FORMID=B.FORMID  and A.RowIndex=B.RowIndex ";
                   DataTable dtAnswer = DataAccess.Instance("BizDB").ExecuteDataTable(StrsqlAnswer);
                   if (dtAnswer.Rows.Count > 0)
                       rpQuestiongDetail.DataSource = dtAnswer;
                   else
                       rpQuestiongDetail.DataSource = null;
                   rpQuestiongDetail.DataBind();
	        }

        }
    }
}