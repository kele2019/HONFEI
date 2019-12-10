using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Data.Common;
using System.Data;

namespace Presale.Process.TravalExpense
{
    public partial class AskApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void read_RequestTravalNo_PreRender(object sender, EventArgs e)
        {
            string ReNo = "";
            string CANo = "";
            for (int i = 0; i < read_RequestTravalNo.Text.Trim().Split(',').Length; i++)
            {
                string strsql = "select INCIDENT from PROC_TRAVEL  where DOCUMENTNO =@DOCUMENTNO";
                DataAccess dac = new DataAccess("BizDB");
                DbCommand cmd = dac.CreateCommand();
                dac.AddInParameter(cmd, "DOCUMENTNO", DbType.String, read_RequestTravalNo.Text.Trim().Split(',')[i].ToString());
                cmd.CommandText = "begin " + strsql + " end;";
                object ReturnNo = dac.ExecuteScalar(cmd);
                if (ReturnNo != null)
                {
                    ReNo += "<a href='../Presale.Process.Traval/Approval.aspx?&Incident=" + ReturnNo + "&type=MYAPPROVAL&ProcessName=" + Server.HtmlEncode("Travel Request Process") + "' target='_blank'>" + read_RequestTravalNo.Text.Trim().Split(',')[i].ToString() + "</a>";
                }

            }
            if (ReNo != "")
            {
                read_RequestTravalNo.Text = ReNo;
            }
            if (read_CashAdvanceNo.Text.Trim() != "")
            {
                string strsql = "select INCIDENT from PROC_CashAdvance  where DOCUMENTNO =@DOCUMENTNO";
                DataAccess dac = new DataAccess("BizDB");
                DbCommand cmd = dac.CreateCommand();
                dac.AddInParameter(cmd, "DOCUMENTNO", DbType.String, read_CashAdvanceNo.Text.Trim());
                cmd.CommandText = "begin " + strsql + " end;";
                object ReturnNo = dac.ExecuteScalar(cmd);
                if (ReturnNo != null)
                {
                    CANo += "<a href='../Presale.Process.Cash Advance/Approval.aspx?&Incident=" + ReturnNo + "&type=MYAPPROVAL&ProcessName=" + Server.HtmlEncode("Cash Advance Process") + "' target='_blank'>" + read_CashAdvanceNo.Text.Trim() + "</a>";
                }
                read_CashAdvanceNo.Text = CANo;
            }
        }
        public void fld_detail_PROC_TravalExpense_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //{
            //    DropDownList dropCurrency = e.Item.FindControl("dropCurrency") as DropDownList;
            //    dropCurrency.DataSource = GetCurrencyData;
            //    dropCurrency.DataTextField = "DicText";
            //    dropCurrency.DataValueField = "DicValue";
            //    dropCurrency.DataBind();
        }
    }
}