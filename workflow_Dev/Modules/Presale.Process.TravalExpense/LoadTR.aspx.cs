using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Data;
namespace Presale.Process.TravalExpense
{
    public partial class LoadTR : System.Web.UI.Page
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
            
            string strsql = "select  * from PROC_TRAVEL where APPLICANTACCOUNT='"+Page.User.Identity.Name.Replace('\\','/')+"' and Status=2";
            if (txtEDate.Text.Trim() != "" && txtSDate.Text.Trim() != "")
            {
                strsql += " and  REQUESTDATE between '"+txtSDate.Text.Trim()+"' and '"+txtEDate.Text.Trim()+"'";
            }
            DataTable dtRequestInfo=DataAccess.Instance("BizDB").ExecuteDataTable(strsql);
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