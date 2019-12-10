using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;

namespace Presale.Process.PersonalExpense
{
    public partial class LoadDept : System.Web.UI.Page
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

            string strsql = @"select B.* from 
  (select DEPARTMENTID,EXT01 from ORG_DEPARTMENT  where DEPARTMENTTYPE='region' ) A
  inner join (select * from ORG_DEPARTMENT where DEPARTMENTTYPE='Department')B
  on A.DEPARTMENTID=B.PARENTID";
            if (txtDept.Text.Trim() != ""  )
            {
                strsql += " and( DEPARTMENTNAME like '%" + txtDept.Text.Trim() + "%' or EXT03 like '%" + txtDept.Text.Trim() + "%') ";
            }
            strsql += "  order by CAST(A.EXT01 as int) asc,  CAST(B.EXT01 as int)asc";
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