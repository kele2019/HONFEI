using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Text;
using BPM.ReportDesign.AjaxPage;

namespace BPM.ReportDesign.GenerateReportFiles
{
    public partial class Template : System.Web.UI.Page
    {
        private string UserName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            UserName = new GetUserInfo().tttt();
            if (!IsPostBack)
            {
                BingData();
            }
        }

        public void BingData()
        {
            //$BingDataFunction$
        }

        //$PagewhetherPaging$

        //$SearchFunction$

        protected void ExportToExcel_Click(object sender, EventArgs e)
        {
            //$ExportToExcel$
        }

        //$DataBaseQueryFunction$

    }
}