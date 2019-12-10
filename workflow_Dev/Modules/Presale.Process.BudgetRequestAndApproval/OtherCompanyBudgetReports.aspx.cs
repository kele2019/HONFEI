using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;
namespace Presale.Process.BudgetRequestAndApproval
{
    public partial class OtherCompanyBudgetReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDataYearBind();
                GetDataBind();
            }
        }
        private void GetDataYearBind()
        {
            string strsqlYear = "select distinct [YEAR] from PROC_Budget  where (STATUS=1 or STATUS=2) order by [YEAR] desc";
            DataTable dtBudgetYear = DataAccess.Instance("BizDB").ExecuteDataTable(strsqlYear);
            if (dtBudgetYear.Rows.Count > 0)
            {
                dropYear.DataSource = dtBudgetYear;
                dropYear.DataTextField = "YEAR";
                dropYear.DataValueField = "YEAR";
                dropYear.DataBind();
            }
            else
            {
                dropYear.DataSource = null;
                dropYear.DataBind();
                dropYear.Items.Insert(0, new ListItem("--Pls Select--", ""));
            }
          

        }

        private void GetDataBind()
        {
            string CurrentYear = dropYear.SelectedItem.Value;
            string Strsql = string.Format(@"  select A.BugetAccountNo,A.BugetAccountDesc, SUM(convert(decimal(18,2),ISNULL(B.Jan,0))) Jan
  ,SUM(convert(decimal(18,2),ISNULL(B.Feb,0))) Feb,SUM(convert(decimal(18,2),ISNULL(B.Mar,0)))Mar,SUM(convert(decimal(18,2),ISNULL(B.Apr,0))) Apr,SUM(convert(decimal(18,2),ISNULL(B.May,0)))May,
   SUM(convert(decimal(18,2),ISNULL(B.Jun,0))) Jun,SUM(convert(decimal(18,2),ISNULL(B.July,0)))July,SUM(convert(decimal(18,2),ISNULL(B.Aug,0))) Aug,SUM(convert(decimal(18,2),ISNULL(B.Sep,0)))Sep,
   SUM(convert(decimal(18,2),ISNULL(B.Oct,0))) Oct,SUM(convert(decimal(18,2),ISNULL(B.Nov,0)))Nov, SUM(convert(decimal(18,2),ISNULL(B.[Dec],0)))[Dec],SUM(convert(decimal(18,2),ISNULL(B.SubTotal,0)))SubTotal
   from (select * from  COM_BudgetAccount    where AccountType='{1}')  A  left join (   select BD.* from (select top(1) FORMID from PROC_Budget where  (STATUS=1 or STATUS=2) and Year='{0}' and BudgetType='{2}'  order by INCIDENT desc) BM left join   PROC_Budget_DT  BD     on BM.FORMID=BD.FORMID  ) B on A.ID=B.BugetAccountID  group by A.BugetAccountNo,A.BugetAccountDesc order by BugetAccountNo", CurrentYear, dropAccountType.SelectedItem.Value, dropBudgetType.SelectedItem.Value);
            DataTable dtBudget = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);


            string StrsqlActual = string.Format(@" select SUM(Convert(decimal(18,2),Amount)) ActuralData ,AcctCode,CONVERT(datetime,Period) Period from  dbo.V_BudgetActualReport where CostCenter<>''   and YEAR(CONVERT(datetime,Period))='{0}' group by AcctCode,Period ", CurrentYear);
            DataTable dtActual = DataAccess.Instance("BizDB").ExecuteDataTable(StrsqlActual);

            DataTable dtCompany = new DataTable();
            dtCompany.Columns.Add("BugetAccountNo", typeof(string));
            dtCompany.Columns.Add("BugetAccountDesc", typeof(string));
            dtCompany.Columns.Add("Jan", typeof(string));
            dtCompany.Columns.Add("Feb", typeof(string));
            dtCompany.Columns.Add("Mar", typeof(string));
            dtCompany.Columns.Add("Apr", typeof(string));
            dtCompany.Columns.Add("May", typeof(string));
            dtCompany.Columns.Add("Jun", typeof(string));
            dtCompany.Columns.Add("July", typeof(string));
            dtCompany.Columns.Add("Aug", typeof(string));
            dtCompany.Columns.Add("Sep", typeof(string));
            dtCompany.Columns.Add("Oct", typeof(string));
            dtCompany.Columns.Add("Nov", typeof(string));
            dtCompany.Columns.Add("Dec", typeof(string));
            dtCompany.Columns.Add("SubTotal", typeof(string));
            dtCompany.Columns.Add("AJan", typeof(string));
            dtCompany.Columns.Add("AFeb", typeof(string));
            dtCompany.Columns.Add("AMar", typeof(string));
            dtCompany.Columns.Add("AApr", typeof(string));
            dtCompany.Columns.Add("AMay", typeof(string));
            dtCompany.Columns.Add("AJun", typeof(string));
            dtCompany.Columns.Add("AJuly", typeof(string));
            dtCompany.Columns.Add("AAug", typeof(string));
            dtCompany.Columns.Add("ASep", typeof(string));
            dtCompany.Columns.Add("AOct", typeof(string));
            dtCompany.Columns.Add("ANov", typeof(string));
            dtCompany.Columns.Add("ADec", typeof(string));
            dtCompany.Columns.Add("ASubTotal", typeof(string));

            if (dtBudget.Rows.Count > 0)
            {

                foreach (DataRow item in dtBudget.Rows)
                {
                    DataRow drBuget = dtCompany.NewRow();
                    drBuget["BugetAccountNo"] = item["BugetAccountNo"];
                    drBuget["BugetAccountDesc"] = item["BugetAccountDesc"];
                    drBuget["Jan"] = item["Jan"];
                    drBuget["Feb"] = item["Feb"];
                    drBuget["Mar"] = item["Mar"];
                    drBuget["Apr"] = item["Apr"];
                    drBuget["May"] = item["May"];
                    drBuget["Jun"] = item["Jun"];
                    drBuget["July"] = item["July"];
                    drBuget["Aug"] = item["Aug"];
                    drBuget["Sep"] = item["Sep"];
                    drBuget["Oct"] = item["Oct"];
                    drBuget["Nov"] = item["Nov"];
                    drBuget["Dec"] = item["Dec"];
                    drBuget["SubTotal"] = item["SubTotal"];

                    decimal ActualSubTotal = 0;
                    drBuget["AJan"] = MonthActural(dtActual.Select(" AcctCode='" + item["BugetAccountNo"].ToString() + "'  and Period='" + CurrentYear + "/" + 01 + "/" + 1 + "' "));
                    ActualSubTotal += Convert.ToDecimal(drBuget["AJan"]);
                    drBuget["AFeb"] = MonthActural(dtActual.Select(" AcctCode='" + item["BugetAccountNo"].ToString() + "'  and Period='" + CurrentYear + "/" + 02 + "/" + 1 + "' "));
                    ActualSubTotal += Convert.ToDecimal(drBuget["AFeb"]);
                    drBuget["AMar"] = MonthActural(dtActual.Select(" AcctCode='" + item["BugetAccountNo"].ToString() + "'  and Period='" + CurrentYear + "/" + 03 + "/" + 1 + "' "));
                    ActualSubTotal += Convert.ToDecimal(drBuget["AMar"]);
                    drBuget["AApr"] = MonthActural(dtActual.Select(" AcctCode='" + item["BugetAccountNo"].ToString() + "'  and Period='" + CurrentYear + "/" + 04 + "/" + 1 + "' "));
                    ActualSubTotal += Convert.ToDecimal(drBuget["AApr"]);
                    drBuget["AMay"] = MonthActural(dtActual.Select(" AcctCode='" + item["BugetAccountNo"].ToString() + "'  and Period='" + CurrentYear + "/" + 05 + "/" + 1 + "' "));
                    ActualSubTotal += Convert.ToDecimal(drBuget["AMay"]);
                    drBuget["AJun"] = MonthActural(dtActual.Select(" AcctCode='" + item["BugetAccountNo"].ToString() + "'  and Period='" + CurrentYear + "/" + 06 + "/" + 1 + "' "));
                    ActualSubTotal += Convert.ToDecimal(drBuget["AJun"]);
                    drBuget["AJuly"] = MonthActural(dtActual.Select(" AcctCode='" + item["BugetAccountNo"].ToString() + "'  and Period='" + CurrentYear + "/" + 07 + "/" + 1 + "' "));
                    ActualSubTotal += Convert.ToDecimal(drBuget["AJuly"]);
                    drBuget["AAug"] = MonthActural(dtActual.Select(" AcctCode='" + item["BugetAccountNo"].ToString() + "'  and Period='" + CurrentYear + "/" + 08 + "/" + 1 + "' "));
                    ActualSubTotal += Convert.ToDecimal(drBuget["AAug"]);
                    drBuget["ASep"] = MonthActural(dtActual.Select(" AcctCode='" + item["BugetAccountNo"].ToString() + "'  and Period='" + CurrentYear + "/" + 09 + "/" + 1 + "' "));
                    ActualSubTotal += Convert.ToDecimal(drBuget["ASep"]);
                    drBuget["AOct"] = MonthActural(dtActual.Select(" AcctCode='" + item["BugetAccountNo"].ToString() + "'  and Period='" + CurrentYear + "/" + 10 + "/" + 1 + "' "));
                    ActualSubTotal += Convert.ToDecimal(drBuget["AOct"]);
                    drBuget["ANov"] = MonthActural(dtActual.Select(" AcctCode='" + item["BugetAccountNo"].ToString() + "'  and Period='" + CurrentYear + "/" + 11 + "/" + 1 + "' "));
                    ActualSubTotal += Convert.ToDecimal(drBuget["ANov"]);
                    drBuget["ADec"] = MonthActural(dtActual.Select(" AcctCode='" + item["BugetAccountNo"].ToString() + "'  and Period='" + CurrentYear + "/" + 12 + "/" + 1 + "' "));
                    ActualSubTotal += Convert.ToDecimal(drBuget["ADec"]);
                    drBuget["ASubTotal"] = ActualSubTotal;
                    dtCompany.Rows.Add(drBuget);
                }

                RPlist.DataSource = dtCompany;


            }
            else
                RPlist.DataSource = null;
            RPlist.DataBind();
        }

        public decimal MonthActural(DataRow[] drActual)
        {
            if (drActual.Length > 0)
                return Convert.ToDecimal(drActual[0]["ActuralData"]);
            else
                return 0;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetDataBind();
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {

            string CurrentYear = dropYear.SelectedItem.Value;
            string Strsql = string.Format(@"  select A.BugetAccountNo,A.BugetAccountDesc, SUM(convert(decimal(18,2),ISNULL(B.Jan,0))) Jan
  ,SUM(convert(decimal(18,2),ISNULL(B.Feb,0))) Feb,SUM(convert(decimal(18,2),ISNULL(B.Mar,0)))Mar,SUM(convert(decimal(18,2),ISNULL(B.Apr,0))) Apr,SUM(convert(decimal(18,2),ISNULL(B.May,0)))May,
   SUM(convert(decimal(18,2),ISNULL(B.Jun,0))) Jun,SUM(convert(decimal(18,2),ISNULL(B.July,0)))July,SUM(convert(decimal(18,2),ISNULL(B.Aug,0))) Aug,SUM(convert(decimal(18,2),ISNULL(B.Sep,0)))Sep,
   SUM(convert(decimal(18,2),ISNULL(B.Oct,0))) Oct,SUM(convert(decimal(18,2),ISNULL(B.Nov,0)))Nov, SUM(convert(decimal(18,2),ISNULL(B.[Dec],0)))[Dec],SUM(convert(decimal(18,2),ISNULL(B.SubTotal,0)))SubTotal
   from (select * from  COM_BudgetAccount    where AccountType='{1}')  A  left join (   select BD.* from (select top(1) FORMID from PROC_Budget where  (STATUS=1 or STATUS=2) and Year='{0}' and BudgetType='{2}'  order by INCIDENT desc) BM left join   PROC_Budget_DT  BD     on BM.FORMID=BD.FORMID  ) B on A.ID=B.BugetAccountID  group by A.BugetAccountNo,A.BugetAccountDesc", CurrentYear, dropAccountType.SelectedItem.Value, dropBudgetType.SelectedItem.Value);
            // DataTable dtBudget = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);

            List<Entity.BudegetCompanyEntity> ListEntity = DataAccess.Instance("BizDB").ExecuteList<Entity.BudegetCompanyEntity>(Strsql);
            string strpath = HttpContext.Current.Request.ApplicationPath;
            string RootPath = HttpContext.Current.Request.PhysicalApplicationPath + "Modules\\Presale.Process.BudgetRequestAndApproval\\Entity\\CompanyBudegetList.xls";
            var extractor = new HaruP.ExcelExtractor(RootPath);
            extractor.GetSheet(0).PutData(ListEntity);

            Response.ContentType = "application/vnd.ms-excel";

            string fullName = HttpUtility.UrlEncode("CompanyBudegetList.xls", System.Text.Encoding.UTF8);
            Response.AddHeader("content-disposition", "attachment;filename=" + fullName);
            extractor.Write(Response.OutputStream);

        }
    }
}