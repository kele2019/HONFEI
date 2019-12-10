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
    public partial class EmployeeTraningAnswerReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetDataBind();
        }
        private void GetDataBind()
        {
            //string DocmentNo = Request.QueryString["DocumentNo"];

            string strwhere = "";
            if (txtStartDate.Text != "" && txtEndDate.Text != "")
                strwhere += " and REQUESTDATE>='" + txtStartDate.Text + "' and  REQUESTDATE<='" + txtEndDate.Text + "'";
            else
            { 
            if(txtStartDate.Text!="")
                strwhere += "  and REQUESTDATE>='" + txtStartDate.Text + "'";
            if(txtEndDate.Text!="")
                strwhere += " and  REQUESTDATE<='" + txtEndDate.Text + "'";
            }

            string StrsqlCount = " select  COUNT(1) from  dbo.PROC_EmployeeTraining where 1=1"+ strwhere;
            AspNetPager1.RecordCount=Convert.ToInt32(DataAccess.Instance("BizDB").ExecuteScalar(StrsqlCount));

            int PageSize = AspNetPager1.PageSize;
            int PageIndex = AspNetPager1.CurrentPageIndex-1;

            string Strsql = @"SELECT * FROM (select ROW_NUMBER() over(order by INCIDENT) RN, FORMID,DOCUMENTNO,REQUESTDATE,TrainingTeacher,TrainingType,StartDate,EndDate,TrainingPurpose from  dbo.PROC_EmployeeTraining where 1=1 " + strwhere+") A   where RN BETWEEN '"+(PageIndex*PageSize+1)+"' AND '"+PageSize*(PageIndex+1)+"'";
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
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            GetDataBind();
        }
    }
}