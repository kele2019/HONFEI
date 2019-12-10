using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ultimus.UWF.Workflow.View.Maintenance
{
    public partial class CompleteTaskList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                ProcessName.Items.Insert(0, new ListItem { Text = "全部", Value = "" });
            }
        }
    }
}