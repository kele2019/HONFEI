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
    public partial class TrainingReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDataBind();
            }
        }
        private void GetDataBind()
        {
            string DocmentNo = Request.QueryString["DocumentNo"];
            string Strsql = @"select  TrainingPurpose,TrainingTeacher,TrainingDuration,(convert(nvarchar(50),StartDate,111)) BeiginDate,(convert(nvarchar(50),EndDate,111)) EndDate,TrainingLocation from PROC_EmployeeTraining where  DOCUMENTNO='" + DocmentNo + "'";
            DataTable dtTraningData = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            if (dtTraningData.Rows.Count > 0)
            {
                lbTopic.Text = dtTraningData.Rows[0]["TrainingPurpose"].ToString();
                lbTrainer.Text = dtTraningData.Rows[0]["TrainingTeacher"].ToString();
                lbTrainingDur.Text = dtTraningData.Rows[0]["TrainingDuration"].ToString();
                lbTrainingDate.Text = dtTraningData.Rows[0]["BeiginDate"].ToString() + "-" + dtTraningData.Rows[0]["EndDate"].ToString();
                lbLocation.Text = dtTraningData.Rows[0]["TrainingLocation"].ToString();
                string domin = ConfigurationManager.AppSettings["Domain"];
                DataTable dtTraining = DataAccess.Instance("BizDB").ExecuteDataTable(@"select A.TrainDate,SumDate,B.USERNAME,B.USERCODE,B.EXT03, REPLACE(B.LOGINNAME,'"+domin+"\\','') SignleName,EXT04 from (select *  from [COM_EmployeeTrainSignInfo] where  TrainDocmentNo='"+DocmentNo+"') A left join ORG_USER B on A.UserID=B.USERID ");
                if (dtTraining.Rows.Count > 0)
                    rpList.DataSource = dtTraining;
                else
                    rpList.DataSource = null;
                rpList.DataBind();
                 
            }
        }

    }
}