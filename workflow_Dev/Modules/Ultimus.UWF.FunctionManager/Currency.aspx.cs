using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.FunctionManager.Entity;
using Ultimus.UWF.FunctionManager.Logic;

namespace Ultimus.UWF.FunctionManager
{
    public partial class Currency : System.Web.UI.Page
    {
        CurrencyLogic _currency = new CurrencyLogic();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                List<CurrencyEntity> lists = _currency.GetCurrencyList();
                Console.WriteLine(lists);
                rptCurrency.DataSource = lists;
                rptCurrency.DataBind();
            }

        }

        protected void btnAddCurrency_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddCurrency.aspx");
        }
        protected string GetUpdateCurrencyUrl(string ID)
        {
            string url = "UpdateCurrency.aspx?currencyID={0}";
            url = string.Format(url, ID);
            return url;
        }

    }
}