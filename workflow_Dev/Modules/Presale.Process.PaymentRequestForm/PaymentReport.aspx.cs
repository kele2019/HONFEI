using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Data;

namespace Presale.Process.PaymentRequestForm
{
    public partial class PaymentReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDateBind();
            }
        }

        public void GetDateBind()
        {
            int PageSize=AspNetPager1.PageSize;
            int PageIndex=AspNetPager1.CurrentPageIndex;
            string strfilter = "";
            string strsqlcount = "SELECT  count(1) FROM PROC_PaymentRequest WHERE STATUS<>'3' AND INCIDENT>0";

            string strsql = @"SELECT * FROM (
SELECT ROW_NUMBER() OVER(ORDER BY  INCIDENT desc) RN,DOCUMENTNO,INCIDENT,APPLICANT,PaymentDescription,CONVERT(NVARCHAR(50),REQUESTDATE,101) REQUESTDATE,vendorcode,vendorname,Currency,DownPaymentAmount,totalamountofpayment,PO,[Contract],[Emergency],EmergencyNew,Remark FROM PROC_PaymentRequest
WHERE STATUS<>'3' AND INCIDENT>0 ";
            string StrStartDate = txtStartDate.Text.Trim();
            string StrEndDate = txtEndDate.Text.Trim();

            if (StrStartDate != "" && StrEndDate != "")
                strfilter += " and  REQUESTDATE>='" + StrStartDate + "' and REQUESTDATE<=dateadd(day,1,'" + StrEndDate + "')";
            else
            {
                if (StrStartDate != "")
                    strfilter += " and  REQUESTDATE>='" + StrStartDate + "'";
                if (StrEndDate != "")
                    strfilter += " and   REQUESTDATE<=dateadd(day,1,'" + StrEndDate + "')";
            }
            if (txtVenderinfo.Text.Trim() != "")
            {
                strfilter += " and vendorname like '%" + txtVenderinfo.Text.Trim() + "%' ";
            }
            if (dropConfidential.SelectedItem.Value != "")
            {
                strfilter += " and Emergency='" + dropConfidential.SelectedItem.Value + "'";
            }
            strsqlcount += strfilter;
            strsql += strfilter;

            strsql += " ) A  where 1=1 and RN between " + PageSize * (PageIndex - 1) + " and " + PageSize * (PageIndex);
           DataTable dtPayment=DataAccess.Instance("BizDB").ExecuteDataTable(strsql);
           int DataCount = Convert.ToInt32(DataAccess.Instance("BizDB").ExecuteScalar(strsqlcount));
           AspNetPager1.RecordCount = DataCount;
           if (dtPayment.Rows.Count > 0)
           {
               RPlist.DataSource = dtPayment;
           }
           else
               RPlist.DataSource = null;
           RPlist.DataBind();

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            GetDateBind();
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            GetDateBind();
        }
    }
}