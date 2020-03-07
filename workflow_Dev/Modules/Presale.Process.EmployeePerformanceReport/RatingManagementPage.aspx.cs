using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;

namespace Presale.Process.EmployeePerformanceReport
{
    public partial class RatingManagementPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBindYear();
                DataBindPage();
            }
        }
        public void DataBindYear()
        { 
         string strslql="select distinct RatingYear from COM_UserRatingData ";
         DataTable dtyear = DataAccess.Instance("BizDB").ExecuteDataTable(strslql);
         if (dtyear.Rows.Count > 0)
         {
             dropYear.DataSource = dtyear;
             dropYear.DataTextField = "RatingYear";
             dropYear.DataValueField = "RatingYear";
             dropYear.DataBind();
             if(dropYear.Items.IndexOf(new ListItem(DateTime.Now.Year.ToString(),DateTime.Now.Year.ToString()))<0)
                 dropYear.Items.Insert(0, new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
         }
         else
             dropYear.Items.Insert(0, new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));

        }
        public void DataBindPage()
        {
            string SelectYear = dropYear.SelectedItem.Value;
            string strsql = @"select A.*, A.USERNAME+ '('+A.EXT04+')' UserFullName,B.RatingValue from (
select * from ORG_USER where ISACTIVE=1) A left join
 (select * from  COM_UserRatingData where RatingYear='" + SelectYear + "' ) B on A.LOGINNAME=REPLACE(B.LOGINNAME,'/','\\')";
            DataTable dtRating=DataAccess.Instance("BizDB").ExecuteDataTable(strsql);
            if (dtRating.Rows.Count > 0)
                RPList.DataSource = dtRating;
            else
                RPList.DataSource = null;
            RPList.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataBindPage();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string RatingYear = dropYear.SelectedValue;
                string Strsql = "delete  from COM_UserRatingData where RatingYear='" + RatingYear + "'";
                foreach (RepeaterItem item in RPList.Items)
                {
                    string LoginName=(item.FindControl("hdUserLoginName") as HiddenField).Value.Replace('\\','/');
                    string RatingValue = (item.FindControl("txtRating") as TextBox).Text;
                    if (RatingValue != "")
                    {
                        Strsql += " insert into COM_UserRatingData(LoginName,RatingValue,RatingYear,CreateDate) values('" + LoginName + "','" + RatingValue + "','"+RatingYear+"',getdate())";
                    }
                }
                DataAccess.Instance("BizDB").ExecuteNonQuery(Strsql);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Success", "CompleteData('1')", true);
            }
            catch (Exception ex)
            { 
            
            }
            DataBindPage();
        }
        

    }
}