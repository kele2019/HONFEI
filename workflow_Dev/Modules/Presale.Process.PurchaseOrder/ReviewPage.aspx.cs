using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presale.Process.PurchaseOrder
{
    public partial class ReviewPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void fld_detail_PROC_PurchaseOrder_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                string taxcode = ((Label)e.Item.FindControl("read_TAXCODE")).Text.ToString().Trim();
                if (taxcode == "J0")
                {
                    ((Label)e.Item.FindControl("latexcde")).Text = "进项税0%";
                }

                if (taxcode == "J11")
                {
                    ((Label)e.Item.FindControl("latexcde")).Text = "进项税11%";
                }

                if (taxcode == "J2")
                {
                    ((Label)e.Item.FindControl("latexcde")).Text = "进项税17%";
                }

                if (taxcode == "J4")
                {
                    ((Label)e.Item.FindControl("latexcde")).Text = "进项税4%";
                }

                if (taxcode == "J1")
                {
                    ((Label)e.Item.FindControl("latexcde")).Text = "进项税6%";
                }
            }
        }
    }
}