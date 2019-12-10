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
    public partial class BudgetDeptAccountEdit : System.Web.UI.Page
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
            BusniessClass Bll = new BusniessClass();
          
            CBCostCenterList.DataTextField = "CostCenterName";
            CBCostCenterList.DataValueField = "CostCenter";
          
            CBCostCenterList.DataSource = Bll.GetCostCenter();
            CBCostCenterList.DataBind();
            ListItem l1 = new ListItem("Select All", "");
            l1.Attributes.Add("onclick", "SelectAllDept(this)");
            CBCostCenterList.Items.Insert(0, l1);

            string BudegetID = Request.QueryString["BudegetID"];
            if (BudegetID != "0")
            {
                hidID.Value = BudegetID;
                txtCode.Visible = false;
                btnLoadBudegetNo.Visible = false;
                string StrSql = "select  A.*,B.BugetAccountNo from ( select  * from COM_BugetDeptAccount where ID=" + BudegetID+")  A LEFT JOIN COM_BudgetAccount B  ON A.AccountID=B.ID";
                DataTable dtAccountInfo = DataAccess.Instance("BizDB").ExecuteDataTable(StrSql);
                if (dtAccountInfo.Rows.Count > 0)
                {
                    hidBudgetID.Value = dtAccountInfo.Rows[0]["AccountID"].ToString();
                    txtDesc.Text = dtAccountInfo.Rows[0]["SubAccountDesc"].ToString();
                    lbCode.Text = dtAccountInfo.Rows[0]["BugetAccountNo"].ToString();
                    txtCode.Text = dtAccountInfo.Rows[0]["BugetAccountNo"].ToString();
                    lbType.Text = dtAccountInfo.Rows[0]["AccountType"].ToString();
                    hidType.Value = dtAccountInfo.Rows[0]["AccountType"].ToString();
                    cbStatus.Checked = dtAccountInfo.Rows[0]["IsActive"].ToString() == "1" ? true : false;
                    string CostCenter = dtAccountInfo.Rows[0]["CostCenter"].ToString();
                    CBCostCenterList.SelectedValue = CostCenter;
                }
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string Code = txtCode.Text.Trim();
                string Desc = txtDesc.Text.Trim();
                string Type = hidType.Value;
                string IsActive = cbStatus.Checked == true ? "1" : "0";
                string AccountID = hidBudgetID.Value;
                string StrSql = "";
                if (hidID.Value == "")
                {

                    foreach (ListItem item in CBCostCenterList.Items)
                    {
                        
                        if(item.Selected)
                        {
                            string CostCenter=item.Value;
                            if (CostCenter != "")
                            {
                                StrSql += "insert into dbo.COM_BugetDeptAccount(AccountID,SubAccountDesc,CostCenter,IsActive,AccountType) values(";
                                StrSql += " '" + AccountID + "','" + Desc + "','" + CostCenter + "','" + IsActive + "','" + Type + "')";
                            }
                         }
                    }

                    //string StrsqlCheck = "select COUNT(1) from  COM_BudgetAccount  where  BugetAccountNo='" + Code + "' and AccountType='" + Type + "'";
                    //if (Convert.ToInt32(DataAccess.Instance("BizDB").ExecuteScalar(StrsqlCheck)) <= 0)
                    //{
                    //    StrSql = "insert into COM_BudgetAccount(BugetAccountNo,BugetAccountDesc,IsActive,AccountType) values(";
                    //    StrSql += "'" + Code + "','" + Desc + "','" + IsActive + "','" + Type + "')";
                    //}
                    //else
                    //    MessageBox.Show(this.Page, "This Budeget Code Already Exist");
                }
                else
                {
                    StrSql = "update COM_BugetDeptAccount set SubAccountDesc='" + Desc + "',IsActive='" + IsActive + "',AccountType='" + Type + "' where ID='" + hidID.Value + "'";
                }
                if (StrSql != "")
                {
                    if (DataAccess.Instance("BizDB").ExecuteNonQuery(StrSql) > 0)
                        MessageBox.ShowAndRedirect(this.Page, "Save Successfully", "BudgetDeptAccountManager.aspx");
                    else
                        MessageBox.Show(this.Page, "Save Faild");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Page, "Save Exception" + ex.Message);
            }
        }

    }
}