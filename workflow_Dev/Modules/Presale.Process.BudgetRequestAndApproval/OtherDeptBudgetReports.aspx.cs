using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Data;

namespace Presale.Process.BudgetRequestAndApproval
{
    public partial class OtherDeptBudgetReports : System.Web.UI.Page
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
          


            //string strsqlCostCenter = "select distinct  CostCenter from COM_BugetDeptAccount";
            //DataTable dtBudgetCostCenter = DataAccess.Instance("BizDB").ExecuteDataTable(strsqlCostCenter);
            //if (dtBudgetCostCenter.Rows.Count > 0)
            //{
            //    dropCostCenter.DataSource = dtBudgetCostCenter;
            //    dropCostCenter.DataTextField = "CostCenter";
            //    dropCostCenter.DataValueField = "CostCenter";
            //}
            //else
            //    dropCostCenter.DataSource = null;
            //dropCostCenter.DataBind();
            object CostCenter = DataAccess.Instance("BizDB").ExecuteScalar(" select EXT14 from ORG_USER where LOGINNAME='" + Page.User.Identity.Name + "'");
            if (CostCenter.ToString() == "All")
            {
                CostCenter = dropCostCenter.SelectedItem.Value;
                hidUserOwn.Value = "1";
            }
            else
            {
                if (CostCenter.ToString().IndexOf(',') > 0)
                {
                    Dictionary<string, string> Dic = new Dictionary<string, string>();
                    for (int i = 0; i < CostCenter.ToString().Split(',').Length; i++)
                    {
                        string TempCostcenter = CostCenter.ToString().Split(',')[i];
                        Dic.Add(TempCostcenter, dropCostCenter.Items.FindByValue(TempCostcenter).Text);
                    }
                    dropCostCenter.DataValueField = "key";//Dic.Keys.ToString();
                    dropCostCenter.DataTextField = "value";//Dic.Values.ToString();
                    dropCostCenter.DataSource = Dic;
                    dropCostCenter.DataBind();
                    hidUserOwn.Value = "1";
                }
                else
                    dropCostCenter.SelectedValue = CostCenter.ToString();
            }
        }

        private void GetDataBind()
        {

            string CurrentYear = dropYear.SelectedItem.Value;
            string BudgetType = dropBudgetType.SelectedItem.Value;
            if (!string.IsNullOrEmpty(Page.User.Identity.Name))
            {
               // object CostCenter = DataAccess.Instance("BizDB").ExecuteScalar(" select EXT14 from ORG_USER where LOGINNAME='" + Page.User.Identity.Name + "'");

                //if (CostCenter.ToString() == "All")
                //{
                //    CostCenter = dropCostCenter.SelectedItem.Value;
                //    hidUserOwn.Value = "1";
                //}
                //else
                //{
                //    if (CostCenter.ToString().IndexOf(',') > 0)
                //    {
                //        Dictionary<string, string> Dic = new Dictionary<string, string>();
                //        for (int i = 0; i < CostCenter.ToString().Split(',').Length; i++)
                //        {
                //            string TempCostcenter = CostCenter.ToString().Split(',')[i];
                //            Dic.Add(TempCostcenter, dropCostCenter.Items.FindByValue(TempCostcenter).Text);
                //        }
                //        dropCostCenter.DataValueField = "key";//Dic.Keys.ToString();
                //        dropCostCenter.DataTextField = "value";//Dic.Values.ToString();
                //        dropCostCenter.DataSource = Dic;
                //        dropCostCenter.DataBind();
                //        hidUserOwn.Value = "1";
                //    }
                //    else
                //        dropCostCenter.SelectedValue = CostCenter.ToString();
                //}
                object CostCenter = dropCostCenter.SelectedValue;
                DataTable dtAccountTypeInfo = DataAccess.Instance("BizDB").ExecuteDataTable("select distinct AccountType from COM_BugetDeptAccount where CostCenter='" + CostCenter.ToString() + "' and IsActive=1");
                if (dtAccountTypeInfo.Rows.Count > 0)
                {
                    RPAcountType.DataSource = dtAccountTypeInfo;
                    RPAcountType.DataBind();
                    foreach (RepeaterItem item1 in RPAcountType.Items)
                    {
                        Repeater RPCostCenter = item1.FindControl("RPCostCenter") as Repeater;
                        Label lbAccountType = item1.FindControl("lbAccountType") as Label;
                        #region 科目信息
                        DataTable dtCostCenterInfo = DataAccess.Instance("BizDB").ExecuteDataTable("select ID AccountID,BugetAccountNo ,BugetAccountDesc from COM_BudgetAccount where ID in(select  distinct AccountID from COM_BugetDeptAccount where CostCenter='" + CostCenter.ToString() + "' and IsActive=1) and AccountType='" + lbAccountType.Text.Trim() + "'  ORDER BY BugetAccountNo");
                        if (dtCostCenterInfo.Rows.Count > 0)
                        {
                            RPCostCenter.DataSource = dtCostCenterInfo;
                            RPCostCenter.DataBind();

                            foreach (RepeaterItem item in RPCostCenter.Items)
                            {
                                Repeater RPlist = item.FindControl("RPlist") as Repeater;
                                HiddenField hidAccountID = item.FindControl("hidAccountID") as HiddenField;
                                string Strsql = "";
//                                if (BudgetType == "4+8")
//                                {
//                                    Strsql = string.Format(@"  select * from  (select BD.ID,SubAccountDesc,BA.BugetAccountNo from COM_BugetDeptAccount BD left join COM_BudgetAccount  BA on BD.AccountID=BA.ID  where AccountID='{0}'  and CostCenter='{1}') A  left join 
//  (select BD.* from  (select top(1) FORMID from PROC_Budget where (STATUS=1 or STATUS=2) and Year='{2}' and BudgetType='{3}' order by INCIDENT desc) BM
// left join  (select * from PROC_Budget_DT where CostCenter='{1}') BD  on BM.FORMID=BD.FORMID ) B on A.ID=B.ID", hidAccountID.Value, CostCenter, CurrentYear, BudgetType);
//                                }
//                                if (BudgetType == "7+5")
//                                {

//                                }
                                Strsql=string.Format(@"  select * from  (select BD.ID,SubAccountDesc,BA.BugetAccountNo from COM_BugetDeptAccount BD left join COM_BudgetAccount  BA on BD.AccountID=BA.ID  where AccountID='{0}'  and CostCenter='{1}') A  left join 
  (select BD.* from  (select top(1) FORMID from PROC_Budget where (STATUS=1 or STATUS=2) and Year='{2}' and BudgetType='{3}' order by INCIDENT desc) BM
 left join  (select * from PROC_Budget_DT where CostCenter='{1}') BD  on BM.FORMID=BD.FORMID ) B on A.ID=B.ID", hidAccountID.Value, CostCenter, CurrentYear, BudgetType);
                                //                string Strsql = string.Format(@" select  C.BugetAccountNo,C.SubAccountDesc,D.* from (
                                // select A.ID,A.AccountID,A.SubAccountDesc,B.BugetAccountNo  from (
                                // select * from COM_BugetDeptAccount where  CostCenter='{0}') A left join  COM_BudgetAccount B
                                // on A.AccountID=B.ID ) C left join (  select BD.* from 
                                // (select top(1) FORMID from PROC_Budget where (STATUS=1 or STATUS=2) and Year='{1}' order by INCIDENT desc) BM
                                // left join  (select * from PROC_Budget_DT where CostCenter='{0}') BD  on BM.FORMID=BD.FORMID ) D on C.ID=D.ID and C.AccountID=D.BugetAccountID", CostCenter, CurrentYear);

                                DataTable dtBudget = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
                                if (dtBudget.Rows.Count > 0)
                                    RPlist.DataSource = dtBudget;
                                else
                                    RPlist.DataSource = null;
                                RPlist.DataBind();
                            }
                        }
                        #endregion
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetDataBind();
        }

        protected void RPCostCenter_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HiddenField hidBugetAccountNo = e.Item.FindControl("hidBugetAccountNo") as HiddenField;
            Label lbJanMonth = e.Item.FindControl("lbJanMonth") as Label;
            Label lbFebMonth = e.Item.FindControl("lbFebMonth") as Label;
            Label lbMarMonth = e.Item.FindControl("lbMarMonth") as Label;
            Label lbAprMonth = e.Item.FindControl("lbAprMonth") as Label;
            Label lbMayMonth = e.Item.FindControl("lbMayMonth") as Label;
            Label lbJunMonth = e.Item.FindControl("lbJunMonth") as Label;
            Label lbJulyMonth = e.Item.FindControl("lbJulyMonth") as Label;
            Label lbAugMonth = e.Item.FindControl("lbAugMonth") as Label;
            Label lbSepMonth = e.Item.FindControl("lbSepMonth") as Label;
            Label lbOctMonth = e.Item.FindControl("lbOctMonth") as Label;
            Label lbNovMonth = e.Item.FindControl("lbNovMonth") as Label;
            Label lbDescMonth = e.Item.FindControl("lbDescMonth") as Label;
            Label lbSubTotal = e.Item.FindControl("lbSubTotal") as Label;


            string Strql = string.Format(@" select Amount,Period, MONTH(CAST(Period as datetime)) BMONTH,YEAR(CAST(Period as datetime)) BYEAR
  from dbo.V_BudgetActualReport where CostCenter='{0}' and AcctCode='{1}' and YEAR(CAST(Period as datetime)) ='{2}'", dropCostCenter.SelectedItem.Value, hidBugetAccountNo.Value, dropYear.SelectedItem.Value);
            DataTable dtBudgetData = DataAccess.Instance("BizDB").ExecuteDataTable(Strql);
            MonthEntity Mode = GetActualData(dtBudgetData);
            lbJanMonth.Text = Mode.Jan.ToString();
            lbFebMonth.Text = Mode.Feb.ToString();
            lbMarMonth.Text = Mode.Mar.ToString();
            lbAprMonth.Text = Mode.Apr.ToString();
            lbMayMonth.Text = Mode.May.ToString();
            lbJunMonth.Text = Mode.Jun.ToString();
            lbJulyMonth.Text = Mode.Jul.ToString();
            lbAugMonth.Text = Mode.Aug.ToString();
            lbSepMonth.Text = Mode.Sep.ToString();
            if (Mode.Sep > 0)
            {

            }
            lbOctMonth.Text = Mode.Oct.ToString();
            lbNovMonth.Text = Mode.Nov.ToString();
            lbDescMonth.Text = Mode.Dec.ToString();
            lbSubTotal.Text = Mode.SubTotal.ToString();
        }
        public MonthEntity GetActualData(DataTable dtBudegetActual)
        {
            MonthEntity ModeEntity = new MonthEntity();

            foreach (DataRow item in dtBudegetActual.Rows)
            {
                switch (int.Parse(item["BMONTH"].ToString()))
                {
                    case 1:
                        ModeEntity.Jan = decimal.Parse(item["Amount"].ToString());
                        ModeEntity.SubTotal += ModeEntity.Jan;
                        break;
                    case 2:
                        ModeEntity.Feb = decimal.Parse(item["Amount"].ToString());
                        ModeEntity.SubTotal += ModeEntity.Feb;
                        break;
                    case 3:
                        ModeEntity.Mar = decimal.Parse(item["Amount"].ToString());
                        ModeEntity.SubTotal += ModeEntity.Mar;
                        break;
                    case 4:
                        ModeEntity.Apr = decimal.Parse(item["Amount"].ToString());
                        ModeEntity.SubTotal += ModeEntity.Apr;
                        break;
                    case 5:
                        ModeEntity.May = decimal.Parse(item["Amount"].ToString());
                        ModeEntity.SubTotal += ModeEntity.May;
                        break;
                    case 6:
                        ModeEntity.Jun = decimal.Parse(item["Amount"].ToString());
                        ModeEntity.SubTotal += ModeEntity.Jun;
                        break;
                    case 7:
                        ModeEntity.Jul = decimal.Parse(item["Amount"].ToString());
                        ModeEntity.SubTotal += ModeEntity.Jul;
                        break;
                    case 8:
                        ModeEntity.Aug = decimal.Parse(item["Amount"].ToString());
                        ModeEntity.SubTotal += ModeEntity.Aug;
                        break;
                    case 9:
                        ModeEntity.Sep = decimal.Parse(item["Amount"].ToString());
                        ModeEntity.SubTotal += ModeEntity.Sep;
                        break;
                    case 10:
                        ModeEntity.Oct = decimal.Parse(item["Amount"].ToString());
                        ModeEntity.SubTotal += ModeEntity.Oct;
                        break;
                    case 11:
                        ModeEntity.Nov = decimal.Parse(item["Amount"].ToString());
                        ModeEntity.SubTotal += ModeEntity.Nov;
                        break;
                    case 12:
                        ModeEntity.Dec = decimal.Parse(item["Amount"].ToString());
                        ModeEntity.SubTotal += ModeEntity.Dec;
                        break;
                    default:
                        break;
                }
            }
            return ModeEntity;
        }


        protected void btnExport_Click(object sender, EventArgs e)
        {

            string Strsql = string.Format(@"select  C.BugetAccountNo,C.SubAccountDesc,D.* from (
   select A.ID,A.AccountID,A.SubAccountDesc,B.BugetAccountNo  from (
   select * from COM_BugetDeptAccount where  CostCenter='{0}') A left join  COM_BudgetAccount B
   on A.AccountID=B.ID ) C left join (  select BD.* from 
   (select top(1) FORMID from PROC_Budget where (STATUS=1 or STATUS=2) and Year='{1}' and BudgetType='{3}' order by INCIDENT desc) BM
   left join  (select * from PROC_Budget_DT where CostCenter='{0}') BD  on BM.FORMID=BD.FORMID ) D 
   on C.ID=D.ID and C.AccountID=D.BugetAccountID", dropCostCenter.SelectedValue, dropYear.SelectedItem.Value,dropBudgetType.SelectedItem.Value);


          

            List<Entity.BudegetDeptEntity> ListEntity = DataAccess.Instance("BizDB").ExecuteList<Entity.BudegetDeptEntity>(Strsql);
            string strpath = HttpContext.Current.Request.ApplicationPath;
            string RootPath = HttpContext.Current.Request.PhysicalApplicationPath + "Modules\\Presale.Process.BudgetRequestAndApproval\\Entity\\DeptAccountList.xls";
            var extractor = new HaruP.ExcelExtractor(RootPath);
            extractor.GetSheet(0).PutData(ListEntity);
            Response.ContentType = "application/vnd.ms-excel";
            string fullName = HttpUtility.UrlEncode("DeptAccountList.xls", System.Text.Encoding.UTF8);
            Response.AddHeader("content-disposition", "attachment;filename=" + fullName);
            extractor.Write(Response.OutputStream);

        }


    }

    //public class MonthEntity
    //{
    //    public decimal Jan
    //    { get; set; }
    //    public decimal Feb
    //    { get; set; }
    //    public decimal Mar
    //    { get; set; }
    //    public decimal Apr
    //    { get; set; }
    //    public decimal May
    //    { get; set; }
    //    public decimal Jun
    //    { get; set; }
    //    public decimal Jul
    //    { get; set; }
    //    public decimal Aug
    //    { get; set; }
    //    public decimal Sep
    //    { get; set; }
    //    public decimal Oct
    //    { get; set; }
    //    public decimal Nov
    //    { get; set; }
    //    public decimal Dec
    //    { get; set; }
    //    public decimal SubTotal
    //    { get; set; }
    //    public MonthEntity()
    //    {
    //        this.Jan = 0;
    //        this.Feb = 0;
    //        this.Mar = 0;
    //        this.Apr = 0;
    //        this.May = 0;
    //        this.Jun = 0;
    //        this.Jul = 0;
    //        this.Aug = 0;
    //        this.Sep = 0;
    //        this.Oct = 0;
    //        this.Nov = 0;
    //        this.Dec = 0;
    //        this.SubTotal = 0;
    //    }
    //}
}
   
