using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;
using Presale.Process.Common;

namespace Presale.Process.BudgetRequestAndApproval
{
    public partial class BudgetAccountEdit : System.Web.UI.Page
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

            string BudegetID = Request.QueryString["BudegetID"];
            if (BudegetID != "0")
            {
                hidID.Value = BudegetID;
                txtCode.Visible = false;
                string StrSql = "select * from  COM_BudgetAccount  where ID="+BudegetID;
                DataTable dtAccountInfo = DataAccess.Instance("BizDB").ExecuteDataTable(StrSql);
                if (dtAccountInfo.Rows.Count > 0)
                {
                    txtDesc.Text = dtAccountInfo.Rows[0]["BugetAccountDesc"].ToString();
                    lbCode.Text = dtAccountInfo.Rows[0]["BugetAccountNo"].ToString();
                    txtCode.Text = dtAccountInfo.Rows[0]["BugetAccountNo"].ToString();
                    dropType.SelectedIndex = dropType.Items.IndexOf(dropType.Items.FindByValue(dtAccountInfo.Rows[0]["AccountType"].ToString()));
                    cbStatus.Checked = dtAccountInfo.Rows[0]["IsActive"].ToString() == "1" ? true : false;
                }
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string Code = txtCode.Text.Trim();
                string Desc = txtDesc.Text.Trim();
                string Type = dropType.SelectedItem.Value;
                string IsActive = cbStatus.Checked == true ? "1" : "0";
                string StrSql = "";
                if (hidID.Value == "")
                {
                    string StrsqlCheck = "select COUNT(1) from  COM_BudgetAccount  where  BugetAccountNo='" + Code + "' and AccountType='" + Type + "'";
                    if (Convert.ToInt32(DataAccess.Instance("BizDB").ExecuteScalar(StrsqlCheck)) <= 0)
                    {
                        StrSql = "insert into COM_BudgetAccount(BugetAccountNo,BugetAccountDesc,IsActive,AccountType) values(";
                        StrSql += "'" + Code + "','" + Desc + "','" + IsActive + "','" + Type + "')";
                    }
                    else
                        MessageBox.Show(this.Page, "This Budeget Code Already Exist");
                }
                else
                {
                    StrSql = "update COM_BudgetAccount set BugetAccountDesc='" + Desc + "',IsActive='" + IsActive + "',AccountType='" + Type + "' where ID='" + hidID.Value + "'";
                }
                if (StrSql != "")
                {
                    if (DataAccess.Instance("BizDB").ExecuteNonQuery(StrSql) > 0)
                        MessageBox.ShowAndRedirect(this.Page, "Save Successfully", "BudgetAccountManager.aspx");
                    else
                        MessageBox.Show(this.Page, "Save Faild");
                }
            }
            catch (Exception ex)
            {
                MyLib.LogUtil.Info("异常，原因是："+ex.Message);
                MessageBox.Show(this.Page, "Save Exception"+ex.Message);
            }
        }


    }
}