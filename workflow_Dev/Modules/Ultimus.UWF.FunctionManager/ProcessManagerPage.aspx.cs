using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;
using Presale.Process.Common;

namespace Ultimus.UWF.FunctionManager
{
    public partial class ProcessManagerPage : System.Web.UI.Page
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
            //string strsqlInfo = " select A.*,B.BugetAccountNo from (select ROW_NUMBER() over(order by ID) RN,* from COM_BugetDeptAccount  where 1=1 " + Filter + ") A LEFT JOIN COM_BudgetAccount B  ON A.AccountID=B.ID WHERE RN>" + PageIndex * PageSize + " AND RN<=" + PageSize * (PageIndex + 1);
            string strsqlInfo = "select ID,PROCESSNAME,EXT01 from WF_PROCESS ORDER BY PROCESSNAME ";
            DataTable dtBudgetInfo = DataAccess.Instance("BizDB").ExecuteDataTable(strsqlInfo);
            if (dtBudgetInfo.Rows.Count > 0)
                RPList.DataSource = dtBudgetInfo;
            else
                RPList.DataSource = null;
            RPList.DataBind();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            string Strsql = "update WF_PROCESS set EXT01='' ";

            foreach (RepeaterItem item in RPList.Items)
            {
                CheckBox CbProcessName=item.FindControl("CbProcessName") as CheckBox;
                HiddenField HidID=item.FindControl("HidID") as HiddenField;
                if (CbProcessName.Checked)
                {
                    Strsql += " update WF_PROCESS set EXT01='1' where ID='"+HidID.Value+"' ";
                }
            }
            if (DataAccess.Instance("BizDB").ExecuteNonQuery(Strsql) > 0)
                MessageBox.Show(this.Page, "Save Successfully");

        }
    }
}