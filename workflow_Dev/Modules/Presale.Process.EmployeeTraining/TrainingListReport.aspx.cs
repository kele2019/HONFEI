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
    public partial class TrainingListReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetBindBusData();
                GetDataBind();
            }
        }
        public void GetBindBusData()
        {
            DataTable dtDept = DataAccess.Instance("BizDB").ExecuteDataTable("select DEPARTMENTID,DEPARTMENTNAME from ORG_DEPARTMENT ");
            if (dtDept.Rows.Count > 0)
            {
                dropDept.DataSource = dtDept;
                dropDept.DataTextField = "DEPARTMENTNAME";
                dropDept.DataValueField = "DEPARTMENTNAME";
                dropDept.DataBind();
                dropDept.Items.Insert(0, "--Plase Select--");
            }
            DataTable dtYear = DataAccess.Instance("BizDB").ExecuteDataTable("select *  from  ( select distinct  YEAR(REQUESTDATE) RYear  from PROC_EmployeeTraining where INCIDENT>0 and (STATUS=1 or STATUS=2) )  C order by RYear Desc");
            if (dtYear.Rows.Count > 0)
            {
                dropYear.DataSource = dtYear;
                dropYear.DataTextField = "RYear";
                dropYear.DataValueField = "RYear";
                dropYear.DataBind();
                //  dropYear.Items.Insert(0, "--Plase Select--");
                int Rowcount = 0;
                foreach (DataRow item in dtYear.Rows)
                {
                    if (item["RYear"].ToString() == DateTime.Now.Year.ToString())
                        Rowcount++;
                }
                //if (Rowcount <= 0)
                    //dropYear.Items.Insert(0, DateTime.Now.Year.ToString());
            }
            dropYear.Items.Insert(0,new ListItem("--Pls Select--",""));// DateTime.Now.Year.ToString());
            //else
            //{
            //    dropYear.Items.Insert(0, DateTime.Now.Year.ToString());
            //}

            for (int i = 0; i < 12; i++)
            {
                dropMonth.Items.Insert(i, (i + 1).ToString());
            }
           // dropMonth.SelectedValue = DateTime.Now.Month.ToString();
            dropMonth.Items.Insert(0, new ListItem("--Pls Select--", ""));
        }
        private void GetDataBind()
        {
            //string DocmentNo = Request.QueryString["DocumentNo"];

            string strwhere = "";
            if (dropDept.SelectedIndex != 0)
                strwhere = " and   DEPARTMENT='" + dropDept.SelectedItem.Value + "'";

            if (txtUserInfo.Text.Trim() != "")
                strwhere += " and   APPLICANT like  '%" + txtUserInfo.Text.Trim().Replace("'", "''") + "%'";

            string CurrentYear = dropYear.SelectedItem.Value;// DateTime.Now.Year.ToString();
            string CurrentMonth = dropMonth.SelectedItem.Value;// DateTime.Now.Month.ToString();
            if(CurrentYear!=""&&CurrentMonth!="")
            strwhere += " and YEAR(REQUESTDATE)='" + CurrentYear + "' and  month(REQUESTDATE)='" + CurrentMonth + "'";

            string StrsColumn = @"(SELECT  EXT04+',' FROM ((
  select EXT04 from org_user where userid in(( select UserID from COM_EmployeeTrainSignInfo where TrainDocmentNo=PROC_EmployeeTraining.DOCUMENTNO
   and SumDate is not null and SumDate<>'0')))) AA for xml path('') )  ActualUsers";

            string Strsql = @"select " + StrsColumn + ",TrainingUser,DOCUMENTNO,APPLICANT,DEPARTMENT,TrainingPurpose,TrainingTeacher,TrainingType,StartDate,EndDate,TrainingDuration,TrainingLocation  from PROC_EmployeeTraining where INCIDENT>0 and (STATUS=1 or STATUS=2)" + strwhere;
            Strsql += " order by REQUESTDATE desc";
            DataTable dtTraningData = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            if (dtTraningData.Rows.Count > 0)
                rpList.DataSource = dtTraningData;
            else
                rpList.DataSource = null;
            rpList.DataBind();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetDataBind();
        }
    }
}