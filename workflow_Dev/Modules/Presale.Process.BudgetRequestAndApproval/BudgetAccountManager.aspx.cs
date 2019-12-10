﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Data;
using Presale.Process.Common;

namespace Presale.Process.BudgetRequestAndApproval
{
    public partial class BudgetAccountManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDataBind();
            }
        }

        public void GetDataBind()
        {
            int PageSize = AspNetPager1.PageSize;
            int PageIndex = AspNetPager1.CurrentPageIndex - 1;

            string Filter=StrWhere();

            string strsqlCount = "select count(1) from COM_BudgetAccount where 1=1 " + Filter;

            int PageCount = Convert.ToInt32(DataAccess.Instance("BizDB").ExecuteScalar(strsqlCount));
            AspNetPager1.RecordCount = PageCount;

            string strsqlInfo = " select * from ( select  ROW_NUMBER() over(order by AccountType,BugetAccountNo) RN, * from COM_BudgetAccount where 1=1 " + Filter + ") A WHERE RN>" + PageIndex * PageSize + " AND RN<=" + PageSize * (PageIndex + 1);
            DataTable dtBudgetInfo = DataAccess.Instance("BizDB").ExecuteDataTable(strsqlInfo);
            if (dtBudgetInfo.Rows.Count > 0)
                RPList.DataSource = dtBudgetInfo;
            else
                RPList.DataSource = null;
            RPList.DataBind();
              
        }

        public string StrWhere()
        {
            string StrFilter = "";
            if (txtAccountCode.Text.Trim() != "")
                StrFilter = " and BugetAccountNo like '%" + txtAccountCode.Text.Trim()+ "%' ";

                return StrFilter;
        }

        protected void RPList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                string Strsql = "";
                if (e.CommandName == "Disabled")
                    Strsql = "update COM_BudgetAccount set IsActive=0 where ID=" + ID;
                else
                    Strsql = "update COM_BudgetAccount set IsActive=1 where ID=" + ID;

                if (DataAccess.Instance("BizDB").ExecuteNonQuery(Strsql) > 0)
                {
                    GetDataBind();
                    MessageBox.Show(this.Page, "Operator Successfully");
                }
                else
                    MessageBox.Show(this.Page, "Operator Faild");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Page, "Operator Exception:"+ex.Message);
            
            }
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            GetDataBind();
        }

        protected void btnSerach_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            GetDataBind();
        }
    }
}