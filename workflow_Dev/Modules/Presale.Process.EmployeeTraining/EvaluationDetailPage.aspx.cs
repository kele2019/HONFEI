using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Data;
namespace Presale.Process.EmployeeTraining
{
    public partial class EvaluationDetailPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string FormId = Request.QueryString["FormID"];
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

            string strsqlUserInfo = @"select A.*,B.USERNAME from PROC_TrainingEvaluation  A left join V_ORG_USER B
 on A.UserID=B.LOGINNAME  where A.FORMID='" + FormId + "'";
            DataTable dtUserinfo = DataAccess.Instance("BizDB").ExecuteDataTable(strsqlUserInfo);
            if (dtUserinfo.Rows.Count > 0)
                RPList.DataSource = dtUserinfo;
            else
                RPList.DataSource = null;
            RPList.DataBind();
        }
    }
}