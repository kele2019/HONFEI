using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Data;
namespace Ultimus.UWF.Report
{
    public partial class PersonalExpenseReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDataInfo();
            }
        }
        public void LoadDataInfo()
        {
            try
            {
                int pageIndex = AspNetPager1.CurrentPageIndex;
                int pageSize = AspNetPager1.PageSize;
                string strwhere = "";
                if (txtStartTime.Text.Trim() != "" && txtEndTime.Text.Trim() != "")
                {
                    strwhere = " and   REQUESTDATE between '" + txtStartTime.Text.Trim() + "' and '" + txtEndTime.Text.Trim() + "'";
                }
                else
                {
                    if (txtStartTime.Text.Trim() != "")
                    {
                        strwhere = " and   REQUESTDATE>='" + txtStartTime.Text.Trim() + "'";
                    }
                    else
                    {
                        if (txtEndTime.Text.Trim() != "")
                        {
                            strwhere = " and   REQUESTDATE=<'" + txtEndTime.Text.Trim() + "'";
                        }
                    }
                }
                string strsqlcount = "select  COUNT(1) from  PROC_PersonalExpense WHERE STATUS=2" + strwhere;
                AspNetPager1.RecordCount = int.Parse(DataAccess.Instance("BizDB").ExecuteScalar(strsqlcount).ToString());

                string strsql = @" SELECT * FROM  (select  ROW_NUMBER() over(order by  INCIDENT) RN, * from  PROC_PersonalExpense WHERE STATUS=2)A";
                strsql += " WHERE RN BETWEEN '" + pageSize * (pageIndex - 1) + "' AND '" + pageSize * pageIndex + "'" + strwhere;
                DataTable dtInfo = DataAccess.Instance("BizDB").ExecuteDataTable(strsql);
                if (dtInfo.Rows.Count > 0)
                {
                    rpList.DataSource = dtInfo;
                    rpList.DataBind();
                }
            }
            catch (Exception ex)
            {
                MyLib.LogUtil.Error(ex.Message);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            LoadDataInfo();
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            LoadDataInfo();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string strwhere = "";
            if (txtStartTime.Text.Trim() != "" && txtEndTime.Text.Trim() != "")
            {
                strwhere = " and   REQUESTDATE between '" + txtStartTime.Text.Trim() + "' and '" + txtEndTime.Text.Trim() + "'";
            }
            else
            {
                if (txtStartTime.Text.Trim() != "")
                {
                    strwhere = " and   REQUESTDATE>='" + txtStartTime.Text.Trim() + "'";
                }
                else
                {
                    if (txtEndTime.Text.Trim() != "")
                    {
                        strwhere = " and   REQUESTDATE=<'" + txtEndTime.Text.Trim() + "'";
                    }
                }
            }
            string strsql = @" SELECT * FROM  (select  ROW_NUMBER() over(order by  INCIDENT) RN, *,";
            #region 合并后的费用
            strsql += @"(select SUM(subAmount) from dbo.PROC_PersonalExpense_DT where  ExpenseItem='招待费 Meals'
  and FORMID=A.FORMID) MealsCount,
    (select SUM(subAmount) from dbo.PROC_PersonalExpense_DT where  ExpenseItem='礼品费 Gifts'
  and FORMID=A.FORMID) GiftsCount,
  (select SUM(subAmount) from dbo.PROC_PersonalExpense_DT where  ExpenseItem='会务费 Exhibition'
  and FORMID=A.FORMID) ExhibitionCount,
    (select SUM(subAmount) from dbo.PROC_PersonalExpense_DT where  ExpenseItem='公司级团队活动 Company Team Building'
  and FORMID=A.FORMID) TeamCount,
      (select SUM(subAmount) from dbo.PROC_PersonalExpense_DT where  ExpenseItem='部门级团队活动 Dept. Team Building'
  and FORMID=A.FORMID) DeptCount,
        (select SUM(subAmount) from dbo.PROC_PersonalExpense_DT where  ExpenseItem='工作餐 Working Meals'
  and FORMID=A.FORMID) WorkingCount,
  (select SUM(subAmount) from dbo.PROC_PersonalExpense_DT where  ExpenseItem='市内交通费 Local Transportation'
  and FORMID=A.FORMID) LocalCount,
   (select SUM(subAmount) from dbo.PROC_PersonalExpense_DT where  ExpenseItem='其他 Other'
  and FORMID=A.FORMID) OtherCount,
    (select SUM(subAmount) from dbo.PROC_PersonalExpense_DT where  ExpenseItem='现金 Cash'
  and FORMID=A.FORMID) CashCount,
  (select SUM(subAmount) from dbo.PROC_PersonalExpense_DT where  ExpenseItem='信用卡 Credit Card'
  and FORMID=A.FORMID) CreditCount";
            #endregion
            strsql +=" from  PROC_PersonalExpense A WHERE STATUS=2)B";
            strsql += " WHERE 1=1 " + strwhere;
            var List = DataAccess.Instance("BizDB").ExecuteList<Entity.PersonalExpense>(strsql);
            
            string strpath = HttpContext.Current.Request.ApplicationPath;
            string RootPath = HttpContext.Current.Request.PhysicalApplicationPath + "Modules\\Ultimus.UWF.Report\\Template\\个人报销.xls";
            var extractor = new HaruP.ExcelExtractor(RootPath);
            extractor.GetSheet(0).PutData(List);

            Response.ContentType = "application/vnd.ms-excel";

            string fullName = HttpUtility.UrlEncode("Peronal Expense Report.xls", System.Text.Encoding.UTF8);
            Response.AddHeader("content-disposition", "attachment;filename=" + fullName);
            extractor.Write(Response.OutputStream);

        }
    }
}