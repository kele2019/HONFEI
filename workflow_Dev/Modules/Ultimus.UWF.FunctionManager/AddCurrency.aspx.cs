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
    public partial class AddCurrency : System.Web.UI.Page
    {
        CurrencyLogic _currency = new CurrencyLogic();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddCurrency_Click(object sender, EventArgs e)
        {
            CurrencyEntity currency = new CurrencyEntity();
            currency.DicCode = currencyName.Text;
            currency.DicText = currencyName.Text;
            currency.DicValue = currencyValue.Text;
            _currency.addCurrencyEntity(currency);
            Response.Redirect("Currency.aspx");
        }
    }
}