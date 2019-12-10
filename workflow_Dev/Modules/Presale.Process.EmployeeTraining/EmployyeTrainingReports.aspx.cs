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
    public partial class EmployyeTrainingReports : System.Web.UI.Page
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
                dropDept.DataValueField = "DEPARTMENTID";
                dropDept.DataBind();
                dropDept.Items.Insert(0, "--Plase Select--");
            }
            DataTable dtYear = DataAccess.Instance("BizDB").ExecuteDataTable("select  distinct  YEAR(TrainDate) TYear from COM_EmployeeTrainSignInfo where TrainDate<>'' ORDER BY TYear DESC ");
            if (dtYear.Rows.Count > 0)
            {
                dropYear.DataSource = dtYear;
                dropYear.DataTextField = "TYear";
                dropYear.DataValueField = "TYear";
                dropYear.DataBind();
              //  dropYear.Items.Insert(0, "--Plase Select--");
                int Rowcount=0;
                foreach (DataRow item in dtYear.Rows)
                {
                    if (item["TYear"].ToString() == DateTime.Now.Year.ToString())
                        Rowcount++;
                }
                if(Rowcount<=0)
                    dropYear.Items.Insert(0, DateTime.Now.Year.ToString());
            }
            else
            {
                dropYear.Items.Insert(0, DateTime.Now.Year.ToString());
            }

            for (int i =0; i <12; i++)
            {
                dropMonth.Items.Insert(i, (i+1).ToString());
            }
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();

        }
        private void GetDataBind()
        {
            //string DocmentNo = Request.QueryString["DocumentNo"];

            string strwhere = "";
            if (dropDept.SelectedIndex != 0)
                strwhere = " and DEPARTMENTID='" + dropDept.SelectedItem.Value+ "'";

            if (txtUserInfo.Text.Trim() != "")
                strwhere = " and    (D.USERNAME+D.USERCODE+D.EXT03)  like '%" + txtUserInfo.Text.Trim().Replace("'", "''") + "%'";

            string CurrentYear = dropYear.SelectedItem.Value;// DateTime.Now.Year.ToString();
            string CurrentMonth = dropMonth.SelectedItem.Value;// DateTime.Now.Month.ToString();


            string Strsql = @"select C.DEPARTMENTID,D.USERID,D.USERNAME,D.USERCODE,D.EXT03,
isnull((select SUM(SumDate) from dbo.COM_EmployeeTrainSignInfo where UserID=D.USERID and YEAR(TrainDate)='" + CurrentYear + "' and MONTH(TrainDate)='" + CurrentMonth + "' ),0) MonthCount,";
            Strsql += " isnull((select SUM(SumDate) from dbo.COM_EmployeeTrainSignInfo where UserID=D.USERID and YEAR(TrainDate)='" + CurrentYear + "'),0) YearCount from"; 
 Strsql+=@" (select b.USERID,A.DEPARTMENTID from  (select DEPARTMENTID from ORG_DEPARTMENT ) A left join ORG_JOB  B  on  A.DEPARTMENTID=B.DEPARTMENTID) C
 left join ORG_USER D on C.USERID=D.USERID where  D.ISACTIVE=1 " + strwhere;
            DataTable dtTraningData = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
              if (dtTraningData.Rows.Count > 0)
                  rpList.DataSource = dtTraningData;
                else
                    rpList.DataSource = null;
                rpList.DataBind();

                foreach (RepeaterItem item in rpList.Items)
                {
                    Repeater RPTrainingDetail=item.FindControl("RPTrainingDetail") as Repeater;
                    HiddenField hdUserID = item.FindControl("hdUserID") as HiddenField;
                    string StrsqlDetail = @"select A.*,B.TrainingPurpose from     (
    select ISNULL(SumDate,0) SumDate ,TrainDocmentNo  from COM_EmployeeTrainSignInfo where UserID='" + hdUserID.Value + "' and  SumDate is not null and  SumDate<>'0')A  left join PROC_EmployeeTraining B on A.TrainDocmentNo=B.DOCUMENTNO";
                    StrsqlDetail += "  WHERE  YEAR(B.REQUESTDATE)='"+dropYear.SelectedValue+"'";

                    DataTable dtTraningDetailData = DataAccess.Instance("BizDB").ExecuteDataTable(StrsqlDetail);
                    if (dtTraningDetailData.Rows.Count > 0)
                        RPTrainingDetail.DataSource = dtTraningDetailData;
                    else
                        RPTrainingDetail.DataSource = null;
                    RPTrainingDetail.DataBind();
                }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetDataBind();
        }
    }
}