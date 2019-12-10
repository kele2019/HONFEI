using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using System.Collections;
using System.ComponentModel;
using System.Data;
using MyLib;
namespace Presale.Process.Cash_Advance
{
    public partial class AskApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
        }
        protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
        {

          
        }
    }
}