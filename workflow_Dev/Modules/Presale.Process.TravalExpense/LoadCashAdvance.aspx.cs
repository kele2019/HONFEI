using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;

namespace Presale.Process.TravalExpense
{
    public partial class LoadCashAdvance : System.Web.UI.Page
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

            string strsql = "select  (BorrowAmount-ISNULL(ReturnAmount,0)) UnReturn ,* from PROC_CashAdvance  where APPLICANTACCOUNT='" + Page.User.Identity.Name.Replace('\\', '/') + "' and Status=2  and (BorrowAmount-ISNULL(ReturnAmount,0))>0 ";
            if (txtEDate.Text.Trim() != "" && txtSDate.Text.Trim() != "")
            {
                strsql += " and  REQUESTDATE between '" + txtSDate.Text.Trim() + "' and '" + txtEDate.Text.Trim() + "'";
            }
            DataTable dtRequestInfo = DataAccess.Instance("BizDB").ExecuteDataTable(strsql);
            if (dtRequestInfo.Rows.Count > 0)
            {
                RPList.DataSource = dtRequestInfo;
                RPList.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataBind();
        }
    }
}