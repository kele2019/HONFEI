using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using System.ComponentModel;
using MyLib;

namespace Presale.Process.Headcount
{
    public partial class HRApprove : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
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
        protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
        {
            object Incident = Request.QueryString["Incident"];
            string Strsql = "update  PROC_HeadCount set status=2 where incident='" + Incident + "'";
            DataAccess.Instance("BizDB").ExecuteNonQuery(Strsql);

        }
    }
}