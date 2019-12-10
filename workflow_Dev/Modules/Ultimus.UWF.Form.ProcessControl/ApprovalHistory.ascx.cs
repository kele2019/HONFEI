using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Ultimus.UWF.Form.ProcessControl.Logic;
using MyLib;
using Ultimus.UWF.Workflow.Logic;

namespace Ultimus.UWF.Form.ProcessControl
{
    public partial class ApprovalHistory : System.Web.UI.UserControl
    {
        public bool ShowAction
        {
            get
            {
                if (txtShowAction.Text == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    txtShowAction.Text = "1";
                }
                else
                {
                    txtShowAction.Text = "0";
                }
                trAction.Visible = value;
                trIdear.Visible = value;
            }
        }
        public string SpecalAction
        {
            get {
                return txtSpecialAction.Text;
            }
        }
        public string Comments
        {
            get
            {
                return txtComments.Text;
            }
        }


        /// <summary>
        /// 操作类型 退回1 同意0 
        /// </summary>
        public int ActionType
        {
            get
            {
                if (rbReturn.Checked)
                {
                    return 1;
                }
                if (rbAbort.Checked)
                {
                    return 2;
                }
                return 0;
            }
        }

       
        private ApprovalHistoryLogic logic = new ApprovalHistoryLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
                int incident = ConvertUtil.ToInt32(userInfo.Incident);
                if (incident <= 0)
                {
                    this.Visible = false;
                }
                
                BingApprovalHistory();

                string type = Request.QueryString["Type"];
                if (!string.IsNullOrEmpty(type))
                {
                    if (type.ToUpper() == "MYAPPROVAL" || type.ToUpper() == "MYREQUEST") //已完成，不显示提交按钮
                    {
                        ShowAction = false;
                    }
                }

                rbApprove.Text = Resources.lang.Approve;
                rbReturn.Text = Resources.lang.Return;

                if (Request.QueryString["XieBan"] == "1")
                {
                    //trAction.Visible = false;
                    rbApprove.Checked = true;
                    //txtShowAction.Text = "0";
                }

                if (Request.QueryString["Copy"] == "1")
                {
                    trAction.Visible = false;
                    rbApprove.Checked = true;
                    txtShowAction.Text = "0";
                    txtComments.Visible = false;
                    this.Visible = false;
                }
            }
        }

        void BingApprovalHistory()
        {
            try
            {
                UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
                ApprovalHistoryList.DataSource = logic.GetApprovalHistoryByProc(userInfo.ProcessName,ConvertUtil.ToInt32( userInfo.Incident));
                ApprovalHistoryList.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}