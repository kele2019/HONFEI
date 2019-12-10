using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Data;
using System.Data.SqlClient;

namespace Presale.Process.EmployeeTraining
{
	public partial class TrainingPracticeAppraisal : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                string FormId = Request.QueryString["FormID"];
                fld_FormID.Text = FormId;

                DataTable dtTraning = DataAccess.Instance("BizDB").ExecuteDataTable("select APPLICANTACCOUNT,TrainingPurpose,TrainingTeacher,StartDate,EndDate from PROC_EmployeeTraining where FORMID='" + fld_FormID.Text + "'");
              if (dtTraning.Rows.Count > 0)
              {
                  fld_CourseName.Text = dtTraning.Rows[0]["TrainingPurpose"].ToString();
                  fld_Trainer.Text=dtTraning.Rows[0]["TrainingTeacher"].ToString();
                  fld_Date.Text = Convert.ToDateTime(dtTraning.Rows[0]["StartDate"].ToString()).ToShortDateString() + " To" + Convert.ToDateTime(dtTraning.Rows[0]["EndDate"].ToString()).ToShortDateString();
                  hdUserID.Value = Page.User.Identity.Name.Replace('\\', '/');// dtTraning.Rows[0]["APPLICANTACCOUNT"].ToString();
              }
            }

		}

		protected void btnSave_Clcik(object sender, EventArgs e)
		{
             
			string CourseName = fld_CourseName.Text;
			string Trainer = fld_Trainer.Text;
			string Date = fld_Date.Text;
			string ApplicableValue = fld_ApplicableValue.Text;
			string WorkingRequirement = fld_WorkingRequirement.Text;
			string TimeArrangement = fld_TimeArrangement.Text;
			string MaterialQuality = fld_MaterialQuality.Text;
			string PowerOE = fld_PowerOE.Text;
			string Motivation = fld_Motivation.Text;
			string ResponseTQ = fld_ResponseTQ.Text;
			string AOTCourse = fld_AOTCourse.Text;
			string Opinion = fld_Opinion.Text;
			string AgreeOrNot = fld_AgreeOrNot.Text;
			string FormID = fld_FormID.Text;
            string sql = "delete from PROC_TrainingEvaluation where Formid='" + fld_FormID.Text + "' AND UserID='" + hdUserID.Value + "'";
          //  sql += "insert into PROC_TrainingEvaluation(FORMID,CourseName,Trainer,Date,ApplicableValue,WorkingRequirement,TimeArrangement,MaterialQuality,PowerOE,Motivation,ResponseTQ,AOTCourse,Opinion,AgreeOrNot,UserID) values('" + FormID + "','" + CourseName + "','" + Trainer + "','" + Date + "','" + ApplicableValue + "','" + WorkingRequirement + "','" + TimeArrangement + "','" + MaterialQuality + "','" + PowerOE + "','" + Motivation + "','" + ResponseTQ + "','" + AOTCourse + "','" + Opinion + "','" + AgreeOrNot + "','"+hdUserID.Value+"')";
			DataAccess.Instance("BizDB").ExecuteNonQuery(sql);

            string Strsql = "insert into PROC_TrainingEvaluation(FORMID,CourseName,Trainer,Date,ApplicableValue,WorkingRequirement,TimeArrangement,MaterialQuality,PowerOE,Motivation,ResponseTQ,AOTCourse,Opinion,AgreeOrNot,UserID) values(";
            Strsql += "@FORMID,@CourseName,@Trainer,@Date,@ApplicableValue,@WorkingRequirement,@TimeArrangement,@MaterialQuality,@PowerOE,@Motivation,@ResponseTQ,@AOTCourse,@Opinion,@AgreeOrNot,@UserID)";
           SqlParameter[] SqlPara=new SqlParameter[15];
           SqlPara[0] = new SqlParameter("@FORMID", FormID);
           SqlPara[1] = new SqlParameter("@CourseName", CourseName);
           SqlPara[2] = new SqlParameter("@Trainer", Trainer);
           SqlPara[3] = new SqlParameter("@Date", Date);
           SqlPara[4] = new SqlParameter("@ApplicableValue", ApplicableValue);
           SqlPara[5] = new SqlParameter("@WorkingRequirement", WorkingRequirement);
           SqlPara[6] = new SqlParameter("@TimeArrangement", TimeArrangement);
           SqlPara[7] = new SqlParameter("@MaterialQuality", MaterialQuality);
           SqlPara[8] = new SqlParameter("@PowerOE", PowerOE);
           SqlPara[9] = new SqlParameter("@Motivation", Motivation);
           SqlPara[10] = new SqlParameter("@ResponseTQ", ResponseTQ);
           SqlPara[11] = new SqlParameter("@AOTCourse", AOTCourse);
           SqlPara[12] = new SqlParameter("@Opinion", Opinion);
           SqlPara[13] = new SqlParameter("@AgreeOrNot", AgreeOrNot);
           SqlPara[14] = new SqlParameter("@UserID", hdUserID.Value);
           DataAccess.Instance("BizDB").ExecuteNonQuery(Strsql, SqlPara);
			DataAccess.Instance("BizDB").ExecuteNonQuery("update PROC_EmployeeTraining set CompleteEvaluation='" + completeEvaluation.Text + "' where FORMID= '" + FormID + "'");
			Page.RegisterStartupScript("key", "<script>SinglePersonConfirm()</script>");
		}
	}
}