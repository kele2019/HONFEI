using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BPM.ReportDesign
{
    public partial class ReportList : System.Web.UI.Page
    {
        private DataBase db;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BingData();
            }
        }
        public void BingData()
        {
            int PageIndex = AspNetPager1.CurrentPageIndex - 1;
            int PageSize = AspNetPager1.PageSize;
            
            string StrSQL = "";

            string strWhere = "";

            if (ReportName.Text.Trim() != "")
            {
                strWhere += " and REPORTNAME like '%"+ReportName.Text.Trim()+"%' ";
            }
            if (BeginDate.Text.Trim() != "")
            {
                strWhere += " and CREATDATE >= '" + BeginDate.Text.Trim() + "' ";
            }
            if (EndDate.Text.Trim() != "")
            {
                strWhere += " and CREATDATE <= '" + EndDate.Text.Trim() + "' ";
            }
            if (UpdateBeginDate.Text.Trim() != "")
            {
                strWhere += " and UPDATEDATE >= '" + UpdateBeginDate.Text.Trim() + "' ";
            }
            if (UpdateEndDate.Text.Trim() != "")
            {
                strWhere += " and UPDATEDATE <= '" + UpdateEndDate.Text.Trim() + "' ";
            }
            StrSQL = "select count(*) from COM_REPORTDESIGN where 1=1 "+strWhere;
            clsOleDB ole = new clsOleDB();
            ole.setConn("oleDB");
            AspNetPager1.RecordCount = int.Parse( ole.getDtRtn(StrSQL).Rows[0][0].ToString().Trim());

            if (System.Configuration.ConfigurationManager.AppSettings["DataBaseType"].ToString().ToUpper() == "ORACLE")
            {
                StrSQL = @"SELECT * FROM 
                         (
                         SELECT A.*, ROWNUM RN 
                         FROM (SELECT * FROM COM_REPORTDESIGN where 1=1 "+strWhere+@" order by creatdate desc) A 
                         WHERE ROWNUM <= " + AspNetPager1.CurrentPageIndex + @"*" + PageSize + @"
                         )
                         WHERE RN >= " + PageIndex + "*" + PageSize + "";
            }
            else
            {
                StrSQL = "select top("+PageSize+") * from COM_REPORTDESIGN where guid not in ("
                       + "select top(" + PageSize + "*" + PageIndex + ") guid from COM_REPORTDESIGN where 1=1 "+strWhere+" order by creatdate desc) "+strWhere+" order by creatdate desc";
            }
            string connectionString = new clsOleDB().getConn("oleDB");//new clsOleDB().getConn("oleDB");
            ReportsList.DataSource = new DataBase(connectionString).QueryTable(StrSQL);
            ReportsList.DataBind();
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BingData();
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            BingData();
        }
        protected void DelReports_Click(object sender, EventArgs e)
        {
            string connectionString = new clsOleDB().getConn("oleDB");
            db = new DataBase(connectionString);
            string Del_Guid = Del_Guids.Value;
            string DelSQL = "delete COM_REPORTDESIGN where guid in (" + Del_Guid + ")";
            DataSet ds = db.QueryTable("select * from COM_REPORTDESIGN where guid in (" + Del_Guid + ")");
            if (db.ExecuteNonQuery(DelSQL) > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string PageFileName = row["REPORTPAGEPATH"].ToString().Trim();
                    string ClassFileName = PageFileName + ".cs";
                    string DesignerFileName = PageFileName + ".designer.cs";
                    string PageFilePath = Server.MapPath("GenerateReportFiles/" + PageFileName);
                    string ClassFilePath = Server.MapPath("GenerateReportFiles/" + ClassFileName);
                    string DesignerFilePath = Server.MapPath("GenerateReportFiles/" + DesignerFileName);
                    File.Delete(PageFilePath);
                    File.Delete(ClassFilePath);
                    File.Delete(DesignerFilePath);
                }
                BingData();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BingData();
        }
    }
}