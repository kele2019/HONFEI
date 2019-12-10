using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.OleDb;

namespace Ultimus.UWF.Office
{
    public class GridImportLogic
    {

        public DataTable GetImportData(FileUpload FileUpload1,string colName)
        {
            try
            {
                string FileName = FileUpload1.FileName;
                string NewFileName = Guid.NewGuid().ToString().Replace("-", "_") + ".xls";
                string SaveFilePath =HttpContext.Current.Server.MapPath(NewFileName);
                FileUpload1.SaveAs(SaveFilePath);
                string connectionString = "";
                //if (FileName.Substring(FileName.LastIndexOf(".")) == ".xls")
                //    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveFilePath + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1'";
                //else
                connectionString = "Provider=Microsoft.Ace.OLEDB.12.0;Data Source=" + SaveFilePath + ";Extended Properties='Excel 12.0;HDR=NO;IMEX=1'";
                OleDbConnection conn = new OleDbConnection(connectionString);
                string sql = "select * from [Sheet1$]";
                OleDbCommand ocd = new OleDbCommand(sql, conn);
                OleDbDataAdapter oda = new OleDbDataAdapter(ocd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                DataTable dt = ds.Tables[0];

                string[] sz = colName.ToUpper().Split(',');
                List<string> strs = new List<string>();
                strs.AddRange(sz);
                int i=0;
                foreach (DataColumn dc in dt.Columns)
                {
                    if (sz.Length > i)
                    {
                        dc.ColumnName = sz[i];
                        i++;
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    dt.Rows.RemoveAt(0);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}