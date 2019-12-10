using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;

namespace Ultimus.UWF.Common
{
    public partial class SelectPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hidSql.Value = Request.QueryString["SQL"];
                hidQuery.Value = Request.QueryString["Query"];
                hidCaption.Value = Request.QueryString["Caption"];
                hidWidth.Value = Request.QueryString["Width"];
                hidOrder.Value = Request.QueryString["Order"];
                hidDBName.Value = Request.QueryString["dbName"];
                litTitle.Text = Request.QueryString["Title"];
            }
            BindGrid();
        }


        void BindGrid()
        {
            string sql = hidSql.Value;
            if (!string.IsNullOrEmpty(hidQuery.Value) && !string.IsNullOrEmpty(txtSearch.Text))
            {
                sql = "SELECT * FROM (" + hidSql.Value + ") a WHERE 1=1 ";
                string[] sz = hidQuery.Value.Split(',');
                string str = "";
                foreach (string ss in sz)
                {
                    str += " or " + ss + " like '%" + txtSearch.Text + "%'";
                }
                sql += " AND (1<>1 " + str + ")";
            }
            if (!string.IsNullOrEmpty(hidOrder.Value))
            {
                sql += " order by " + hidOrder.Value;
            }
            string dbName = "BizDB";
            if (!string.IsNullOrEmpty(hidDBName.Value))
            {
                dbName = hidDBName.Value;
            }
            DataTable dt = DataAccess.Instance(dbName).ExecuteDataTable(sql);
            //列名
            string[] strArrField = hidQuery.Value.Split(',');
            string[] strArrName = hidCaption.Value.Split(',');
            string[] strArrWidth = hidWidth.Value.Split(',');

            //清空DataGrid
            dgResult.Columns.Clear();

            TemplateColumn colT = new TemplateColumn();
            colT.ItemTemplate = new CheckBoxTemplate();
            colT.HeaderStyle.Width = 10;
            dgResult.Columns.Add(colT);

            //根据查询得到的Dataset进行DataGrid列初始化
            List<string> names = new List<string>();
            names.AddRange(strArrField);
            int count = 0;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                BoundColumn col = new BoundColumn();
                col.DataField = dt.Columns[i].ColumnName;
                if (!names.Contains(dt.Columns[i].ColumnName))
                {
                    col.Visible = false;
                }
                else
                {
                    //列名
                    if (count < strArrName.Length && !string.IsNullOrEmpty(strArrName[count]))
                    {
                        col.HeaderText = strArrName[count];
                    }
                    else
                    {
                        col.HeaderText = dt.Columns[i].Caption;
                    }
                    //列宽
                    if (count < strArrWidth.Length && !string.IsNullOrEmpty(strArrWidth[count]))
                    {
                        col.HeaderStyle.Width = Int32.Parse(strArrWidth[count].ToString());
                        if (col.HeaderStyle.Width == 0)
                        {
                            col.Visible = false;
                        }
                    }
                    else
                    {
                        col.HeaderStyle.Width = 50;
                        col.Visible = false;
                    }
                    count++;
                }
                dgResult.Columns.Add(col);
            }

            //绑定数据
            dgResult.DataSource = dt;
            AspNetPager1.RecordCount = dt.Rows.Count;
            dgResult.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
            dgResult.DataBind();

            foreach (DataGridItem dgItm in dgResult.Items)
            {
                //对每行的值进行组合
                string strReturn = "{";
                for (int i = 1; i < dgResult.Columns.Count; i++)
                {
                    strReturn += dt.Columns[i-1].ColumnName + ":'" + dgItm.Cells[i].Text.Replace("'", "").Replace("\"", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").Trim() + "',";
                }
                strReturn = strReturn.TrimEnd(',');
                strReturn += "}";
                dgItm.Attributes.Add("ondblclick", "javascript:ReturnValue(" + strReturn + ");");
            }
        }


        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindGrid();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            string str = "";
            DataTable dt = (DataTable)dgResult.DataSource;
            int count=0;
            foreach (DataGridItem dgItm in dgResult.Items)
            {
                Control ctl= dgItm.Cells[0].Controls[0];
                if (ctl is CheckBox)
                {
                    CheckBox cbx = ctl as CheckBox;
                    if (cbx.Checked)
                    {
                        //对每行的值进行组合
                        string strReturn = "{";
                        for (int i = 1; i < dgResult.Columns.Count; i++)
                        {
                            strReturn += dt.Columns[i - 1].ColumnName + ":'" + dgItm.Cells[i].Text.Replace("'", "").Replace("\"", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").Trim() + "',";
                        }
                        strReturn = strReturn.TrimEnd(',');
                        strReturn += "}";

                        str += strReturn + ",";
                        count++;
                    }
                }
            }

            str = str.TrimEnd(',');
            if (count > 1)
            {
                str = "[" + str + "]";
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(),"aaa","Ok("+str+");",true);
        }
    }

    public class CheckBoxTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            CheckBox cbx = new CheckBox();
            container.Controls.Add(cbx);
        }
    }
}