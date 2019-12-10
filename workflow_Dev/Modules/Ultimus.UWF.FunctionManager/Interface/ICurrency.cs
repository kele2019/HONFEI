using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ultimus.UWF.FunctionManager.Entity;

namespace Ultimus.UWF.FunctionManager.Interface
{
    interface ICurrency
    {
        List<CurrencyEntity> GetCurrencyList();
        void addCurrencyEntity(CurrencyEntity currency);
        void updateCurrency(CurrencyEntity currency);
        CurrencyEntity getCurreneyByID(string ID);
    }
}