using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.ComponentModel;
using Ultimus.UWF.Form.ProcessControl;
using Presale.Process.PaymentRequestForm.Entity;
using Presale.Process.Common;
using System.Data.Common;
using System.Data;

namespace Presale.Process.PaymentRequestForm
{
	public partial class Approval : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
            ((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);

		}
		protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
		{
			string sql = "select EXT03 from dbo.org_user where loginname = '" + Page.User.Identity.Name + "'";
			ApprovalUsrePost user = DataAccess.Instance("BizDB").ExecuteEntity<ApprovalUsrePost>(sql);
			fld_ApprovalUserPost.Text = user.EXT03;
            
            string ProcessName= Request.QueryString["ProcessName"].ToString();
            string StepName = Request.QueryString["StepName"].ToString();
            string Incident = Request.QueryString["Incident"].ToString();
            ApprovalHistory approvalHistory = Page.FindControl("ApprovalHistory1") as ApprovalHistory;
            int ActiontType = approvalHistory.ActionType;
            BusniessClass ApproveClass = new BusniessClass();
            if (StepName == "GM Approve")//成功
            {
                if (ApproveClass.CheckCFOApprove(ProcessName, Incident, "CFO Approve"))
            {
                MessageBox.Show(this.Page, "CFO not approve");
                g.Cancel = true;
                return;
            }
            }
            //if (ActiontType == 1 && StepName != "GM Approve")
            //    ApproveClass.RejectProcessStep(ProcessName, Incident, "GM Approve");
		}

        protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
        {
            string StepName = Request.QueryString["StepName"].ToString();
            UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            string FormID = userInfo.FormId;
            ApprovalHistory approvalHistory = Page.FindControl("ApprovalHistory1") as ApprovalHistory;
            int ActiontType = approvalHistory.ActionType;
            string GrNo = read_GRNo.Text.Trim();
            if (ActiontType != 0)//成功
            {
                string strinsert = string.Format("update PROC_Purchase set PurchaseOrdStatus = 0 where DOCUMENTNO='" + read_PurchaseOrderNo.Text + "'");
                DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert);
            }
            else
            {
                NewRequest nr = new NewRequest();
                nr.ChangeGRPaymentStatus(GrNo);
                //string sqlfitler = "";
                //if (GrNo != "")
                //{
                //    for (int i = 0; i < GrNo.Split(',').Length; i++)
                //    {
                //        sqlfitler += "'" + GrNo.Split(',')[i] + "'";
                //    }
                //    string strinsertGR = string.Format("update PROC_GoodsReceive set PaymentStatus =0 where DOCUMENTNO in(" + sqlfitler + "");
                //    DataAccess.Instance("BizDB").ExecuteNonQuery(strinsertGR);
                //}
            }
        }
        protected void read_GRNo_PreRender(object sender, EventArgs e)
        {
            string GRNO = read_GRNo.Text;
            string ReNo = "";
            string CANo = "";
            for (int i = 0; i < read_GRNo.Text.Trim().Split(',').Length; i++)
            {
                string strsql = "select INCIDENT from PROC_GoodsReceive  where DOCUMENTNO =@DOCUMENTNO";
                DataAccess dac = new DataAccess("BizDB");
                DbCommand cmd = dac.CreateCommand();
                dac.AddInParameter(cmd, "DOCUMENTNO", DbType.String, read_GRNo.Text.Trim().Split(',')[i].ToString());
                cmd.CommandText = "begin " + strsql + " end;";
                object ReturnNo = dac.ExecuteScalar(cmd);
                if (ReturnNo != null)
                {
                    ReNo += "<a href='../Presale.Process.GoodsReceiveRequest/Approve.aspx?&Incident=" + ReturnNo + "&type=MYAPPROVAL&ProcessName=" + Server.HtmlEncode("Goods Receive Application") + "' target='_blank'>" + read_GRNo.Text.Trim().Split(',')[i].ToString() + "</a>";
                }

            }
            if (ReNo != "")
            {
                read_GRNo.Text = ReNo;
            }
        }
	}
}