using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Data;
using Presale.Process.Common;

namespace Ultimus.UWF.FunctionManager
{
    public partial class WorkHolidayManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetDataBind();
        }

        public void GetDataBind()
        {

            

            int PageSize = AspNetPager1.PageSize;
            int PageIndex = AspNetPager1.CurrentPageIndex - 1;


            string strsqlCount = "select Count(1) from COM_DICTIONRY where Type='HolidayType'";

            int PageCount = Convert.ToInt32(DataAccess.Instance("BizDB").ExecuteScalar(strsqlCount));
            AspNetPager1.RecordCount = PageCount;

            //string strsqlInfo = " select A.*,B.BugetAccountNo from (select ROW_NUMBER() over(order by ID) RN,* from COM_BugetDeptAccount  where 1=1 " + Filter + ") A LEFT JOIN COM_BudgetAccount B  ON A.AccountID=B.ID WHERE RN>" + PageIndex * PageSize + " AND RN<=" + PageSize * (PageIndex + 1);
            string strsqlInfo = "select  * from( select  *,ROW_NUMBER() over(order by DicText Desc) RN  from  COM_DICTIONRY where Type='HolidayType' ) A  WHERE RN>" + PageIndex * PageSize + " AND RN<=" + PageSize * (PageIndex + 1);
            DataTable dtBudgetInfo = DataAccess.Instance("BizDB").ExecuteDataTable(strsqlInfo);
            if (dtBudgetInfo.Rows.Count > 0)
                RPList.DataSource = dtBudgetInfo;
            else
                RPList.DataSource = null;
            RPList.DataBind();

        }

        protected void RPList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                string Strsql = "";
                if (e.CommandName == "Delete")
                    Strsql = "Delete from  COM_DICTIONRY where ID=" + ID;
                else
                    Strsql = "update COM_BugetDeptAccount set IsActive=1 where ID=" + ID;

                if (DataAccess.Instance("BizDB").ExecuteNonQuery(Strsql) > 0)
                {
                    GetDataBind();
                    MessageBox.Show(this.Page, "Operator Successfully");
                }
                else
                    MessageBox.Show(this.Page, "Operator Faild");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Page, "Operator Exception:" + ex.Message);

            }
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            GetDataBind();
        }
    }
}