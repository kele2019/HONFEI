using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using MyLib;
using System.ComponentModel;
using System.Collections;
using Presale.Process.Common;
namespace Presale.Process.Traval
{
    public partial class AdminApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
            ((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
            if (!IsPostBack)
            {
                object objtype = Request.QueryString["type"];
                if (objtype != null)
                {
                    if (objtype.ToString() == "myapproval")
                    {
                        btnDiv.Visible = false;
                    }
                    else
                    {
                        if (objtype.ToString() == "mytask")
                        {
                            btnDiv.Visible = true;
                        }
                    }
                }
            }
        }
        private void NewRequest_AfterFormDataLoad(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
        {
        }
        protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
        {
            try
            {
                // find.Close_TASelectStatus(this.fld_TAFORMID.Text);
            }
            catch (Exception ex)
            {
                LogUtil.Error("审批阶段调用发送邮件方法失败！", ex);
            }

        }
    }
}