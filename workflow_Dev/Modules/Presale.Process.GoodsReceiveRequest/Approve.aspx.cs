using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using System.ComponentModel;
using MyLib;
using System.Data.Common;
using System.Data;

namespace Presale.Process.GoodsReceiveRequest
{
    public partial class Approve : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
            ((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
            ((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
        }
        protected void read_PurchaseRequestNo_PreRender(object sender, EventArgs e)
        {
            string GRNO = read_PurchaseRequestNo.Text;
            string ReNo = "";
            string CANo = "";
            for (int i = 0; i < read_PurchaseRequestNo.Text.Trim().Split(',').Length; i++)
            {
                string strsql = "select INCIDENT from PROC_Purchase  where DOCUMENTNO =@DOCUMENTNO";
                DataAccess dac = new DataAccess("BizDB");
                DbCommand cmd = dac.CreateCommand();
                dac.AddInParameter(cmd, "DOCUMENTNO", DbType.String, read_PurchaseRequestNo.Text.Trim().Split(',')[i].ToString());
                cmd.CommandText = "begin " + strsql + " end;";
                object ReturnNo = dac.ExecuteScalar(cmd);
                if (ReturnNo != null)
                {
                    ReNo += "<a href='../Presale.Process.PurchaseApplication/Approval.aspx?&Incident=" + ReturnNo + "&type=MYAPPROVAL&ProcessName=" + Server.HtmlEncode("Purchase Request") + "' target='_blank'>" + read_PurchaseRequestNo.Text.Trim().Split(',')[i].ToString() + "</a>";
                }

            }
            if (ReNo != "")
            {
                read_PurchaseRequestNo.Text = ReNo;
            }
        }
        protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
        {
        }
        protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
        {
            try
            {
                UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
                string FormID = userInfo.FormId;
                ApprovalHistory approvalHistory = Page.FindControl("ApprovalHistory1") as ApprovalHistory;
                int ActiontType = approvalHistory.ActionType;
                string StepName = Request.QueryString["StepName"];
                if(StepName == "SCM approval")
                 {
                   if (ActiontType == 0 )//成功
                   {
                       string strinsert = string.Format("update PROC_GoodsReceive set status='2' where formid='" + FormID + "'");
                       DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert);
                       if (!string.IsNullOrEmpty(read_PurchaseRequestNo.Text))
                       {
                           string strinsert1 = string.Format("update PROC_Purchase set PurchaseOrdStatus = 2 where DOCUMENTNO='" + read_PurchaseRequestNo.Text + "'");
                           DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert1);
                       }
                   }
                   else
                   {
                       if (!string.IsNullOrEmpty(read_PurchaseRequestNo.Text))
                       {
                           string strinsert = string.Format("update PROC_Purchase set PurchaseOrdStatus = 0 where DOCUMENTNO='" + read_PurchaseRequestNo.Text + "'");
                           DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert);
                       }
                   }
                }
            }
            catch
            {

            }
        }
    }
}