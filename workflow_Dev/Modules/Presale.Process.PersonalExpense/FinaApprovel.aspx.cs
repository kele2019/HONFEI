using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;

namespace Presale.Process.PersonalExpense
{
    public partial class FinaApprovel : System.Web.UI.Page
    {
        public List<Subject> GetSubjectData = DataAccess.Instance("BizDB").ExecuteList<Subject>("select * from  dbo.COM_Subject");
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void fld_detail_PROC_TravalExpense_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string TaskType = Request.QueryString["type"].ToString();
            if (TaskType == "mytask")
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    TextBox Subject = e.Item.FindControl("fld_Subject") as TextBox;
                    Label read_ExpenseItem = e.Item.FindControl("read_ExpenseItem") as Label;
                    int SubjectCount=GetSubjectData.Where(p => p.Item == read_ExpenseItem.Text).Count();
                    if (SubjectCount > 0)
                    {
                        Subject.Text = GetSubjectData.Where(p => p.Item == read_ExpenseItem.Text).First().FinaSub;
                    }
                }
            }
        }
    }
    public class Subject
    {
        string finSub = "";
        public string FinaSub
        {
            get { return finSub; }
            set { finSub = value; }
        }
        string item = "";
        public string Item
        {
            get { return item; }
            set { item = value; }
        }
    }
}