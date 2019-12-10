using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Net;
namespace Ultimus.UWF.Office
{
    public partial class GridExport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {   
        }

        public void Export(string ExportString)
        {
            Microsoft.Office.Interop.Excel.Application xapp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook wb = xapp.Workbooks.Add();
            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
            string[] rows = ExportString.TrimEnd('~').Split('~');
            DataTable dt = new DataTable();
            for (int i = 0; i < rows.Length; i++)
            {
                string[] cols = rows[i].TrimEnd('|').Split('|');
                for (int j = 0; j < cols.Length; j++)
                {
                    ws.Cells.set_Item(i+1,j+1,cols[j]);
                }
            }
            string FileName = "Temp\\" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
            string FilePath = Server.MapPath(FileName);
            ws.SaveAs(FilePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            wb.Close(false, Type.Missing, Type.Missing);
            xapp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xapp);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
            GC.Collect(0);

            Response.Redirect(FileName,false);

            
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            Export(hfExportString.Value);
            
        }

    }
}