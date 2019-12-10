using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data;
using Ultimus.UWF.Form.ProcessControl;
using MyLib;
using System.ComponentModel;
 

namespace Traval
{
    public partial class Approval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            
            //((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
            ((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
            ((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
        }

        private void NewRequest_AfterFormDataLoad(object sender, System.ComponentModel.CancelEventArgs e)
        {
             
        }

        /// <summary>
        /// 明细行添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 明细行删除
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void fld_detail_PROC_TravelExpense_DT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
          
        }


        #region 按钮方法
        DataTable data = new DataTable();
        //FindDOAAndBudget find = new FindDOAAndBudget();
        //IOrg _org = ServiceContainer.Instance().GetService<IOrg>();
        protected void NewRequest_BeforeSubmit(object j, System.ComponentModel.CancelEventArgs g)
        {

        }
        protected void NewRequest_AfterSubmit(object j, EventArgs g)
        {
            //Ultimus.UWF.V8.Implementation.UltimusTask ut = new Ultimus.UWF.V8.Implementation.UltimusTask();
            //string TaskID = Request.QueryString["TaskID"].ToString();
            //System.Collections.Hashtable hs = ut.LoadTask(TaskID);
            //foreach (System.Collections.DictionaryEntry item in hs)
            //{

            //}
        }


        #endregion

        public void fld_detail_PROC_TravelExpense_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
             
        }
        public DataTable GetVLookupDT(string LookUp_Type)
        {
            string SQL = "Select TYPEVAULESCODE,TYPEVAULES from V_LOOKUP where LOOKUP_TYPE='{0}'";
            SQL = string.Format(SQL, LookUp_Type);
            DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(SQL);
            return dt;
        }
        public DataTable GetExpenseCategory(string ProcessName)
        {
            string SQL = "Select CATEGORYCODE,CATEGORY from Mng_ExpenseCategory where ProcessName='{0}' and IsDisplay=1";
            SQL = string.Format(SQL, ProcessName);
            DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(SQL);
            return dt;
        }
        public DataTable GetCurrency()
        {
            string SQL = "Select CURRENCY_CODE from Mng_CurrencyType where Status=1";
            DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(SQL);
            return dt;
        }

        protected void fld_detail_PROC_TravelExpense_DT_PreRender(object sender, EventArgs e)
        {
            
        }
        
    }
}