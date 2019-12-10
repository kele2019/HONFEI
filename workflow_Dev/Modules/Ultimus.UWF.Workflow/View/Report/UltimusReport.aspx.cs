using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Common.Logic;
using System.Net;

namespace Ultimus.UWF.Workflow.View.Report
{
    public partial class UltimusReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //这个只是中转页面
            string rootPath = MyLib.ConfigurationManager.AppSettings["BPMRootPath"].ToString();

            string path = rootPath + "/ultweb/ultwebreports/default.aspx";

            Response.Redirect(path);
        }
    }
}