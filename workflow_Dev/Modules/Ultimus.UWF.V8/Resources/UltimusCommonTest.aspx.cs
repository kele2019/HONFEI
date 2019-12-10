using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ultimus.UWF.V8.Resources
{
    public partial class UltimusCommonTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CommonClass cls = new CommonClass(); 
            string guid="";
            string documentNo="";
            DateTime requestDate = DateTime.MinValue; ;
            cls.GetNewFormInfo("PO", ref guid, ref documentNo, ref requestDate);
        }
    }
}