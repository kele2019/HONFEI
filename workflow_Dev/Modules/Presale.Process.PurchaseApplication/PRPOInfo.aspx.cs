using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Data;
using Presale.Process.Common;

namespace Presale.Process.PurchaseApplication
{
    public partial class PRPOInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDataBind();
            }
        }
        public void GetDataBind()
        {
            string streFilter = Strwhere();
            int PageSize = AspNetPager1.PageSize;
            int PageIndex = AspNetPager1.CurrentPageIndex;
            string StrsqlCount = "select  COUNT(1) from PROC_Purchase A left join  V_PRPO B on A.DOCUMENTNO=B.PRNO WHERE 1=1"+streFilter;
            AspNetPager1.RecordCount=Convert.ToInt32(DataAccess.Instance("BizDB").ExecuteScalar(StrsqlCount).ToString());
            string Strsql = @"select * from (
select ROW_NUMBER() over(order by A.REQUESTDATE desc) RN, A.DOCUMENTNO,A.APPLICANT AS PRAPPLICANT,CONVERT(nvarchar(50),A.REQUESTDATE,111) as PRREQUESTDATE,A.STATUS,A.Incident,A.Remarks,A.TotalAmount,B.*,CONVERT(nvarchar(50),B.REQUESTDATE,111) as POREQUESTDATE,PurchaseOrdStatus from PROC_Purchase A left join  V_PRPO B on A.DOCUMENTNO=B.PRNO WHERE 1=1
" + streFilter + ") AA WHERE RN BETWEEN " + ((PageSize * (PageIndex - 1))+1) + " AND " +PageSize * PageIndex;
            DataTable dtPurchaseInfo = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            if (dtPurchaseInfo.Rows.Count > 0)
                RPList.DataSource = dtPurchaseInfo;
            else
                RPList.DataSource = null;
            RPList.DataBind();
        }
        public string Strwhere()
        {
            string returnvalue = "";
             
            //if (txtPRNO.Text.Trim() != "")
            //{
            //    returnvalue = " and A.DOCUMENTNO like '%" + txtPRNO.Text.Trim() + "%'";
            //}
            //if (txtPONO.Text.Trim() != "")
            //{
            //    returnvalue = " and B.PONO like '%" + txtPONO.Text.Trim() + "%'";
            //}
            if (DropPRStatus.SelectedItem.Value != "")
            {
                if (DropPRStatus.SelectedItem.Value == "0")
                {
                    returnvalue = " and A.PurchaseOrdStatus='" + DropPRStatus.SelectedItem.Value + "'";
                }
                else
                {
                    returnvalue = " and A.PurchaseOrdStatus<>'0'";
                }
            }
            returnvalue += " and Incident<>-1 and status<>3";

            return returnvalue;
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            GetDataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetDataBind();
        }

        protected void RPList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                string PRNO = e.CommandArgument.ToString();
                string StrSql = "";
                if (e.CommandName == "EnableCom")
                {
                    StrSql = "update PROC_Purchase set STATUS=2,PurchaseOrdStatus='0' where DOCUMENTNO='" + PRNO + "'";
                }
                if (e.CommandName == "DisableCom")
                {
                    StrSql = "update PROC_Purchase set STATUS=2,PurchaseOrdStatus='2' where DOCUMENTNO='" + PRNO + "'";
                }
              int Flag=DataAccess.Instance("BizDB").ExecuteNonQuery(StrSql);
              if (Flag > 0)
              {
                  MessageBox.Show(this.Page, "Operator success");
                  GetDataBind();
              }
                else
                  MessageBox.Show(this.Page, "Operator Faild");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Page, "Operator Exception:"+ex.Message);
            }
        }

        protected void RPList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}