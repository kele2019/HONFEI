using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;

namespace Presale.Process.ProcessPerformance
{
    public partial class KPIReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetBindYearInfo();
                GetDatabind();
            }
        }
        public void GetBindYearInfo()
        {

            //DataTable dtYearInfo = DataAccess.Instance("BizDB").ExecuteDataTable("select distinct YEAR from  dbo.PROC_ProcessPerformance_Forth order by YEAR DESC");
            //if (dtYearInfo.Rows.Count > 0)
            //{
            //    dropYear.DataSource = dtYearInfo;
            //    dropYear.DataTextField = "YEAR";
            //    dropYear.DataValueField = "YEAR";
            //    dropYear.DataBind();
            //}
            //else
            //    dropYear.Items.Insert(0, new ListItem("--Select--", ""));


            DataTable dtVersionInfo = DataAccess.Instance("BizDB").ExecuteDataTable(" select * from PROC_ProcessPerformanceVersion order by ID desc");
            if (dtVersionInfo.Rows.Count > 0)
            {
                DropVersion.DataSource = dtVersionInfo;
                DropVersion.DataTextField = "PerformanceVersionName";
                DropVersion.DataValueField = "PerformanceVersionName";
                DropVersion.DataBind();
            }

          
        }

        public void GetDatabind()
        {
            string strsql = "declare @Year nvarchar(50) set @Year='"+dropYear.SelectedValue+"'";
//                   strsql+= @" select *,
//                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='January'   and YEAR=@Year and ID=A.ID) Jan,
//                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='February'  and YEAR=@Year and ID=A.ID) Feb,
//                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='March'     and YEAR=@Year and ID=A.ID) Mar,
//                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='April'     and YEAR=@Year and ID=A.ID) April,
//                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='May'       and YEAR=@Year and ID=A.ID) May,
//                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='June'      and YEAR=@Year and ID=A.ID) Jun,
//                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='July'      and YEAR=@Year and ID=A.ID) July,
//                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='August'    and YEAR=@Year and ID=A.ID) Aug,
//                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='September' and YEAR=@Year and ID=A.ID) Sep,
//                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='October'   and YEAR=@Year and ID=A.ID) Oct,
//                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='November'  and YEAR=@Year and ID=A.ID) Nov,
//                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='December'  and YEAR=@Year and ID=A.ID) Dece,
//                       (select top(1) YTD from ( select YTD,dbo.MonthSort(MONTH) SMonth from PROC_ProcessPerformance_Forth where YEAR=@Year and ID=A.ID  ) AA where YTD is not null order by SMonth desc)   YTD";
            strsql += @" select *,
                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='January'    and ID=A.ID) Jan,
                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='February'   and ID=A.ID) Feb,
                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='March'      and ID=A.ID) Mar,
                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='April'      and ID=A.ID) April,
                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='May'        and ID=A.ID) May,
                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='June'       and ID=A.ID) Jun,
                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='July'       and ID=A.ID) July,
                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='August'     and ID=A.ID) Aug,
                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='September'  and ID=A.ID) Sep,
                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='October'    and ID=A.ID) Oct,
                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='November'   and ID=A.ID) Nov,
                       (select TOP(1) StatusValue from PROC_ProcessPerformance_Forth where MONTH='December'   and ID=A.ID) Dece,
                       (select top(1) YTD from ( select YTD,dbo.MonthSort(MONTH) SMonth from PROC_ProcessPerformance_Forth where  ID=A.ID  ) AA where YTD is not null order by cast(SMonth as int) desc)   YTD";


                     //  (select TOP(1) AA.YTD from PROC_ProcessPerformance_Forth AA left join PROC_ProcessPerformance B on AA.FORMID=B.FORMID where AA.YTD is not null AND AA.ID=A.ID order by REQUESTDATE  desc ) YTD ";
                       //from (select DEPTMENTCODE,PROCESS,PROCESSMEA,STANDARD,Definition,Calculationmethod,ID from PROC_ProcessPerformance_Thrid) A  order by DEPTMENTCODE";
                   strsql += @" from ( select DEPTMENTCODE,PROCESS,PROCESSMEA,STANDARD,Definition,Calculationmethod,ID from
                          PROC_ProcessPerformance_Thrid where ContactInfo=(select ContactInfo from PROC_ProcessPerformanceVersion where PerformanceVersionName='" +DropVersion.SelectedValue+"' ))  A  order by DEPTMENTCODE";
                  DataTable dtDataInfo=DataAccess.Instance("BizDB").ExecuteDataTable(strsql);
                 // Response.Write(strsql);
                  if (dtDataInfo.Rows.Count > 0)
                      rpList.DataSource = dtDataInfo;
                  else
                      rpList.DataSource = null;
                  rpList.DataBind();

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //Response.Write("start search");
            //GetDatabind();
            //Response.Write("End search");
        }

        protected void btnSeachNew_Click(object sender, EventArgs e)
        {
            GetDatabind();
        }

        protected void dropYear_SelectedIndexChanged(object sender, EventArgs e)
        {

           
        }
    }
}