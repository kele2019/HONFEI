using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.ComponentModel;
using Ultimus.UWF.Form.ProcessControl;
using Presale.Process.Common;

namespace Presale.Process.BusinessGiftRequest
{
	public partial class Approval : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{

			}
            //((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
            //((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
		}
		protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
		{
			 
		}
		protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
		{
			try
			{

			}
			catch
			{

			}
		}
	}
}