using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data;
using Ultimus.UWF.Form.ProcessControl;
using MyLib;
using System.Collections;
using System.Data.Common;
using System.ComponentModel;
namespace TravalExpense
{
    public partial class Approval : System.Web.UI.Page
    {
        public DataTable GetCurrencyData = DataAccess.Instance("BizDB").ExecuteDataTable("select * from  dbo.COM_DICTIONRY where Type='Currency'");

        protected void Page_Load(object sender, EventArgs e)
        {
            ((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
        }
        protected void NewRequest_AfterSubmit(object j, CancelEventArgs g)
        {
            try
            {
                ApprovalHistory approvalHistory = Page.FindControl("ApprovalHistory1") as ApprovalHistory;
                int ActiontType = approvalHistory.ActionType;
                if (ActiontType == 1)//退回
                {
                    if (read_BrrowYes.Checked)
                    {
                        UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
                        string FormID = userInfo.FormId;
                        StringBuilder strsql = new StringBuilder();
                        DataTable dtResevese = DataAccess.Instance("BizDB").ExecuteDataTable("select *  from PROC_ResevserData where FormID='" + FormID + "' and RetrunType='TravelExpese'");
                        foreach (DataRow item in dtResevese.Rows)
                        {
                            string CashAdvanceNo = item["CANo"].ToString();
                            string Amount = item["ReturnAmount"].ToString();
                            // strsql.AppendFormat("insert into dbo.PROC_ResevserData (FormID,CANo,ReturnAmount,RetrunType,ReturnDate)  values('{0}','{1}','{2}','PersonalExpese',GETDATE()) ", FormID, CashAdvanceNo, Amount);
                            strsql.AppendFormat("update PROC_CashAdvance set ReturnAmount=ISNULL(ReturnAmount,0)-{0} where  DOCUMENTNO='{1}'", Amount, CashAdvanceNo);
                        }
                        if (strsql.ToString() != "")
                        {
                            DataAccess.Instance("BizDB").ExecuteNonQuery(strsql.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error("冲账方法失败！", ex);
            }

        }
        protected void read_RequestTravalNo_PreRender(object sender, EventArgs e)
        {
            string ReNo = "";
            string CANo = "";
            for (int i = 0; i <read_RequestTravalNo.Text.Trim().Split(',').Length; i++)
            {
                string strsql = "select INCIDENT from PROC_TRAVEL  where DOCUMENTNO =@DOCUMENTNO";
                DataAccess dac = new DataAccess("BizDB");
                DbCommand cmd = dac.CreateCommand();
                dac.AddInParameter(cmd, "DOCUMENTNO", DbType.String, read_RequestTravalNo.Text.Trim().Split(',')[i].ToString());
                cmd.CommandText = "begin " + strsql + " end;";
                object ReturnNo = dac.ExecuteScalar(cmd);
                if(ReturnNo!=null)
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
                if(ReturnNo!=null)
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
            //}
        }
    }
}