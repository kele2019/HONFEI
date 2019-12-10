using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Collections.Generic;
namespace Ultimus.UWF.Office
{
    public partial class GridImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtVarName.Text = Request.QueryString["varName"];
                txtColName.Text = Request.QueryString["colNames"];
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                string FileName = FileUpload1.FileName;
                string NewFileName = Guid.NewGuid().ToString().Replace("-", "_") + ".xls";
                string SaveFilePath = Server.MapPath(NewFileName);
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

                string[] sz = txtColName.Text.ToUpper().Split(',');
                List<string> strs = new List<string>();
                strs.AddRange(sz);
                string script = "";
                string varnames = "";
                string values = "";
                string lines = (dt.Rows.Count-1).ToString();
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    if (i == 1)
                    {
                        //script += "<script language='javascript' defer>ClearRow('" + txtVarName.Text + "');AddNew('" + txtVarName.Text + "');</script>";
                    }
                    else
                    {
                        //script += "<script language='javascript' defer>AddNew('" + txtVarName.Text + "');</script>";
                    }
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string dtCol =Convert.ToString( dt.Rows[0][j]);
                        if (strs.Contains(dtCol.ToUpper()))
                        {
                            int pos = dtCol.IndexOf("[");
                            if (pos > 0)
                            {
                                dtCol = dtCol.Substring(0, pos);
                            }
                            string colName = dtCol;
                            string varName = txtVarName.Text + "[" + i  + "]" + "." + colName;
                            varnames += varName + ",";
                            string value = Convert.ToString(dt.Rows[i][j]);
                            values += value + ","; ;
                            //script += "<script language='javascript' defer>ReturnValue('" +varName+ "','" + value + "');</script>";
                        }
                    }
                }
                varnames = varnames.TrimEnd(',');
                values = values.TrimEnd(',');
                script += "<script language='javascript' defer>ReturnValue('" + txtVarName.Text + "','" + varnames + "','" + values + "',"+lines+");</script>";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "ddd", script);
                File.Delete(SaveFilePath);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(\"" + ex.Message + "\");</script>");
                return;
            }
            //this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "javascript", "<script language='javascript' defer>alert('导入成功！');</script>");
        }
        
    }
}