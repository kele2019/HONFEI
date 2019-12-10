using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using System.ComponentModel;
using System.Text;
using System.Data;
using MyLib;

namespace Presale.Process.PersonalExpense
{
    public partial class Approval : System.Web.UI.Page
    {
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
                    DataTable dtResevese = DataAccess.Instance("BizDB").ExecuteDataTable("select *  from PROC_ResevserData where FormID='" + FormID + "' and RetrunType='PersonalExpese'");
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
    }
}