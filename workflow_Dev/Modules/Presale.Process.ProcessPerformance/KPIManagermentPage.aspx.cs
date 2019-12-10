using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Data;
namespace Presale.Process.ProcessPerformance
{
    public partial class KPIManagermentPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDatabind();
                GetPerformanceData();
            }
        }
        public void GetDatabind()
        {
            string Strsql = "select PerformanceVersionName,ContactInfo,CreateDate from PROC_ProcessPerformanceVersion order by  ID desc";
          DataTable dtPerVersion=DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
          if (dtPerVersion.Rows.Count > 0)
          {
              dropYear.DataSource = dtPerVersion;
              dropYear.DataTextField = "PerformanceVersionName";
              dropYear.DataValueField = "ContactInfo";
              dropYear.DataBind();
              dropYear.SelectedIndex = 0;

          }

          string StrsqlDept = "select DicText,(DicCode+'-'+DicText) DicValue from COM_DICTIONRY where Type='KPIData'";
          DataTable dtDept = DataAccess.Instance("BizDB").ExecuteDataTable(StrsqlDept);
          if (dtDept.Rows.Count > 0)
          {
            dropDepartment.DataSource = dtDept;
            dropDepartment.DataTextField = "DicValue";
            dropDepartment.DataValueField = "DicText";
            dropDepartment.DataBind();
            dropDepartment.SelectedIndex = 0;

          }

        }

        public void GetPerformanceData()
        {
            string ContactInfo = dropYear.SelectedValue;
            string Strsql = "select * from PROC_ProcessPerformance_Thrid where ContactInfo='" + ContactInfo + "' order by DEPTMENTCODE asc ";
            DataTable dtPerformacneData = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            if (dtPerformacneData.Rows.Count > 0)
                RPList.DataSource = dtPerformacneData;
            else
                RPList.DataSource = null;
            RPList.DataBind();
        }
        protected void btnSeachNew_Click(object sender, EventArgs e)
        {
            GetPerformanceData();
        }

    }
}