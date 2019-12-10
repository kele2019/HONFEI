using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Data;

namespace Presale.Process.Leave
{
    public partial class YearLavelManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBindDropYear();
                DataBindYearList();
            }
        }

        public void DataBindDropYear()
        {
            string strsql = "select distinct LeaveYear from  COM_LevalManager";
            DataTable dtYear = DataAccess.Instance("BizDB").ExecuteDataTable(strsql);
            if (dtYear.Rows.Count > 0)
            {
                DropYearLavel.DataSource = dtYear;
                DropYearLavel.DataTextField = "LeaveYear";
                DropYearLavel.DataValueField = "LeaveYear";
                DropYearLavel.DataBind();
                DropYearLavel.Items.Insert(dtYear.Rows.Count, new ListItem(DateTime.Now.AddYears(1).Year.ToString(), DateTime.Now.AddYears(1).Year.ToString()));
                DropYearLavel.SelectedItem.Value = DateTime.Now.Year.ToString();
            }
        }

        public void DataBindYearList()
        {
            string strsqlYear = string.Format(@"select U.EXT04,U.USERCODE,U.LOGINNAME,U.USERNAME,LY.*,'{0}' LYear from 
(select * from ORG_USER where ISACTIVE=1) U left join  (select *  from  COM_LevalManager where LeaveYear='{0}') LY
on REPLACE(U.LOGINNAME,'\','/')=LY.UserAccount",DropYearLavel.SelectedItem.Value);
            DataTable dtYearInfo = DataAccess.Instance("BizDB").ExecuteDataTable(strsqlYear);
            if (dtYearInfo.Rows.Count > 0)
            {
                RpList.DataSource = dtYearInfo;
                RpList.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataBindYearList();
        }

        protected void RpList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "btnSave")
            {
                string LoginName = e.CommandArgument.ToString().Replace("\\","/");
                string YearDay = (e.Item.FindControl("hidYear") as HiddenField).Value;
                string YearCount=(e.Item.FindControl("txtYearCount") as TextBox).Text;
                string SickLeave=(e.Item.FindControl("txtSickLeave") as TextBox).Text;
                decimal LasHour = ChangeDecimal((e.Item.FindControl("hidLastHour") as HiddenField).Value);
                decimal YearHourCountOld = ChangeDecimal((e.Item.FindControl("hidYearHourCount") as HiddenField).Value);
                decimal YearHourCount = ChangeDecimal(YearCount) * 8;
                decimal LastHourCount=YearHourCount-(YearHourCountOld-LasHour);

                string StrsqlYear = string.Format(@" if exists(select * from  COM_LevalManager where UserAccount='{0}' and LeaveYear='{1}')
 update COM_LevalManager set LeaveYearCount='{2}', LeaveYearHourCount='{3}',LeaveLastYearHourCount='{4}', FuallpaySick='{5}'
 where UserAccount='{0}' and LeaveYear='{1}'
 else
 insert into COM_LevalManager (UserAccount,LeaveYear,LeaveYearCount,LeaveYearHourCount,LeaveLastYearHourCount,FuallpaySick)
 values ('{0}','{1}','{2}','{3}','{3}','{5}')", LoginName, YearDay, YearCount, YearHourCount, LastHourCount, SickLeave);
                int RowIndx=DataAccess.Instance("BizDB").ExecuteNonQuery(StrsqlYear);
            }
            DataBindYearList();
            Presale.Process.Common.MessageBox.Show(this.Page, "Save Success");
        }
        public decimal ChangeDecimal(string Number)
        { 
            decimal ReturnData=0;
            if (Number == "")
                ReturnData = 0;
            else
                ReturnData = Convert.ToDecimal(Number);
            return ReturnData;
        }
    }
}