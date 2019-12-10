using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;

namespace Presale.Process.Personal_Allowance
{
    public partial class Approval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             
        }
        public void fld_detail_PROC_TravalExpense_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //{
            //    DropDownList dropCurrency = e.Item.FindControl("dropCurrency") as DropDownList;
            //    dropCurrency.DataSource = GetCurrencyData;
            //    dropCurrency.DataTextField = "DicText";
            //    dropCurrency.DataValueField = "DicValue";
            //    dropCurrency.DataBind();
               
            //}
        }
    }
}