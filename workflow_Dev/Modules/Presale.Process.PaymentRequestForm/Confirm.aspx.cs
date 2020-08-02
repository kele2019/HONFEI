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

namespace Presale.Process.PaymentRequestForm
{
	public partial class Confirm : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{

			}
			((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
			((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
		}
		protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
		{

		}
		protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			string FormID = userInfo.FormId;
			ApprovalHistory approvalHistory = Page.FindControl("ApprovalHistory1") as ApprovalHistory;
			int ActiontType = approvalHistory.ActionType;
			if (ActiontType == 0)//成功
			{
				string strinsert1 = string.Format("update PROC_PaymentRequest set status='2' where formid='" + FormID + "'");
				DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert1);
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