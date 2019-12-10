using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;

namespace Ultimus.UWF.FunctionManager
{
    public partial class HolidayEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string RequestID = Request.QueryString["ID"];
                if (!string.IsNullOrEmpty(RequestID))
                {
                    GetHolidayData(RequestID);
                }
            }
        }

        public void GetHolidayData(string ID)
        {
            string Strsql = "select * from COM_DICTIONRY where Type='HolidayType' and ID='" + ID + "'";
            DataTable dtHoliday = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            if (dtHoliday.Rows.Count > 0)
            {
                foreach (DataRow item in dtHoliday.Rows)
                {
                    txtHolidayDate.Text = item["DicText"].ToString();
                    dropWorkType.SelectedIndex = dropWorkType.Items.IndexOf(dropWorkType.Items.FindByValue(item["DicValue"].ToString()));
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string RequestID = Request.QueryString["ID"];
                string HolidayDate = txtHolidayDate.Text;
                string WorkType = dropWorkType.SelectedItem.Value;
                string DicCode = Convert.ToDateTime(HolidayDate).ToString("yyyyMMdd") + "Holiday";
                string Comments = Convert.ToDateTime(HolidayDate).Year.ToString();
                if (!string.IsNullOrEmpty(RequestID))//Update
                {
                    string Strsql = @" if exists(select * from COM_DICTIONRY where ID<>'" + RequestID + "' and DicCode='" + DicCode + "')";
                    Strsql += @" select '0' else begin
                update COM_DICTIONRY set DicCode='" + DicCode + "',DicText='" + HolidayDate + "',DicValue='" + WorkType + "',Type='HolidayType',Comments='" + Comments + "' where ID='" + RequestID + "' select '1'  end";
                    object TableID = DataAccess.Instance("BizDB").ExecuteScalar(Strsql);
                    if (TableID != null)
                    {
                        if (TableID.ToString() == "0")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Success", "CompleteData('0')", true);
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Success", "CompleteData('1')", true);
                        }
                    }

                }
                else//Add
                {
                    string Strsql = "insert into COM_DICTIONRY(DicCode,DicText,DicValue,Type,Comments)values(";
                    Strsql += "'" + DicCode + "','" + HolidayDate + "','" + WorkType + "','HolidayType','" + Comments + "')";
                    if (DataAccess.Instance("BizDB").ExecuteNonQuery(Strsql) > 0)
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Success", "CompleteData('1')", true);
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Success", "CompleteData('"+ex.Message+"')", true);
            }
        }
    }
}