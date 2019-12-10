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
    public partial class UpdateCurrency : System.Web.UI.Page
    {
        CurrencyLogic _currency = new CurrencyLogic();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ID = Request.QueryString["currencyID"];
                CurrencyEntity currency = _currency.getCurreneyByID(int.Parse(ID));
                currencyChangeId.Text = currency.ID.ToString();
                currencyChangeName.Text = currency.DicText;
                currencyChangeValue.Text = currency.DicValue;
            }
        }

        protected void btnUpdateCurrency_Click(object sender, EventArgs e)
        {
            string currencyId = currencyChangeId.Text;
            CurrencyEntity currency = _currency.getCurreneyByID(int.Parse(currencyId));
            currency.DicValue = currencyChangeValue.Text;
            _currency.updateCurrency(currency);
            Response.Redirect("Currency.aspx");
        }
    }
}