using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace BPM.popup
{
    public partial class olePage4 : System.Web.UI.Page
    {
        #region 变量声明
        string strSQL;
        string strName;
        string strWidth;
        string strConn;
        string strOrder;
        string strSQLOrder;
        string strTitle;
        string isMulti;
        DataTable dtQuery = new DataTable();
        DataTable dtTotal = new DataTable();
        clsOleDB clsDB = new clsOleDB();
        #endregion

        #region 事件定义
        protected void Page_Load(object sender, System.EventArgs e)
        {
            #region 取得传入的参数
            //取得传入的参数
            if (!Page.IsPostBack)
            {
                strSQL = Request.QueryString.Get("SQL");
                strName = Request.QueryString.Get("Name");
                strWidth = Request.QueryString.Get("Width");
                strConn = Request.QueryString.Get("Conn");
                strOrder = Request.QueryString.Get("Order");
                strTitle = Request.QueryString.Get("Title");
                isMulti = Request.QueryString.Get("isMulti");

                lblSQL.Text = strSQL;
                lblName.Text = strName;
                lblWidth.Text = strWidth;
                lblConn.Text = strConn;
                lblOrder.Text = strOrder;
                lblTitle.Text = strTitle;
                lblisMulti.Text = isMulti;
            }
            else
            {
                strSQL = lblSQL.Text;
                strName = lblName.Text;
                strWidth = lblWidth.Text;
                strConn = lblConn.Text;
                strOrder = lblOrder.Text;
                strTitle = lblTitle.Text;
                isMulti = lblisMulti.Text;
            }
            #endregion

            #region 测试数据
            //strSQL = "1001#NewProcess~12";
            //strName = "stepname~userfullname~name~signdate~remarks";
            //strWidth = "100~100~100~100~100";
            //strConn = "oleDB";
            //strOrder = "remarks";
            #endregion

            #region 数据合法性验证
            if (strName == null)
            {
                strName = "";
            }

            if (strWidth == null)
            {
                strWidth = "";
            }

            if (strConn == null || strConn == "")
            {
                strConn = "bpmDB";
            }
            #endregion

            //从配置文件中取得对应的 SQL， 并进行格式化
            string strMainSql = clsDB.getConn2(strSQL.Split('$')[0]);
            string[] strSqlPara = strSQL.Split('$')[1].Split('~');

            //将参数对应的值对号入座
            for (int i = 0; i < strSqlPara.Length; i++)
            {
                strMainSql = strMainSql.Replace("{" + i.ToString() + "}", strSqlPara[i]);
            }

            if (!Page.IsPostBack)
            {
                clsDB.setConn(strConn);
                //clsDB.wteLog("", true, "调用olePage4方法SQL：" + strMainSql);
                dtQuery = clsDB.getDtRtn(strMainSql);

                //初始化DataGrid列
                InitGrid(dgResult, dtQuery, strName, strWidth);
                if (dtQuery != null)
                {
                    BindGrid(dgResult, dtQuery);
                }
            }
            else
            {
                if (strOrder != null && strOrder != "")
                {
                    strSQLOrder = "select * from (" + strMainSql + ") T_Tmps where " + strOrder + " like '%" + this.txtSearch.Text.Replace("'", "").Trim().ToString() + "%'";
                }
                else
                {
                    strSQLOrder = strMainSql;
                }

                clsDB.setConn(strConn);
                //clsDB.wteLog("", true, "调用olePage4方法SQLOrder：" + strSQLOrder);
                dtQuery = clsDB.getDtRtn(strSQLOrder);

                //初始化DataGrid列
                InitGrid(dgResult, dtQuery, strName, strWidth);
                if (dtQuery != null)
                {
                    BindGrid(dgResult, dtQuery);
                }
            }
            //Response.Charset = "GB2312";
            //Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.Write("<script language=javascript>document.title=\"" + strTitle + "\"</script>");
        }

        #endregion

        #region 方法定义
        /// <summary>
        /// 初始化DataGrid列
        /// </summary>
        private void InitGrid(DataGrid dg, DataTable dt, string strName, string strWidth)
        {
            //列名
            string[] strArrName = strName.Split('~');
            string[] strArrWidth = strWidth.Split('~');

            //清空DataGrid
            dg.Columns.Clear();

            //根据查询得到的Dataset进行DataGrid列初始化
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                BoundColumn col = new BoundColumn();
                col.DataField = dt.Columns[i].Caption.ToString();
                //列名
                if (i < strArrName.Length && strArrWidth[i].ToString() != "")
                {
                    col.HeaderText = strArrName[i];
                }
                else
                {
                    col.HeaderText = dt.Columns[i].Caption.ToString() + "";
                }

                //列宽
                if (i < strArrWidth.Length && strArrWidth[i].ToString() != "")
                {
                    col.HeaderStyle.Width = Int32.Parse(strArrWidth[i].ToString());
                    if (col.HeaderStyle.Width == 0)
                    {
                        col.Visible = false;
                    }
                }
                else
                {
                    col.HeaderStyle.Width = 50;
                }

                dg.Columns.Add(col);
            }
        }

        /// <summary>
        /// 数据邦定
        /// </summary>
        /// <param name="dsData"></param>
        private void BindGrid(DataGrid dg, DataTable dt)
        {
            //将DataGrid与DataSet进行绑定
            if (dt != null)
            {
                dg.DataSource = dt.DefaultView;
                dg.DataBind();
                //为DataGride增加事件方法
                EditGrid(dg);
            }
        }


        /// <summary>
        /// 更新Grid上的字段列
        /// </summary>
        private void EditGrid(DataGrid dg)
        {
            foreach (DataGridItem dgItm in dg.Items)
            {
                //对每行的值进行组合
                string strReturn = "";
                for (int i = 0; i < dg.Columns.Count; i++)
                {
                    strReturn += dgItm.Cells[i].Text.Replace("'", "").Replace("\"", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").Trim().Replace("&nbsp;","") + "~";
                }

                dgItm.Attributes.Add("ondblclick", "javascript:ReturnValue('" + strReturn + "');");
                if (isMulti == "1") //多选
                {
                    dgItm.Attributes.Add("onclick", "javascript:getMulti('" + strReturn.TrimEnd('~') + "',this);");
                }
                else  //单选
                {
                    dgItm.Attributes.Add("onfocusout", "javascript:MouseEvent(this);");
                    dgItm.Attributes.Add("onfocusin", "javascript:MouseEvent(this);");
                    btnConfirm.Visible = false;
                    btnSelectAll.Visible = false;
                }
                dgItm.Attributes.Add("style", "cursor:hand;");

            }

        }
        #endregion

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion
    }
}