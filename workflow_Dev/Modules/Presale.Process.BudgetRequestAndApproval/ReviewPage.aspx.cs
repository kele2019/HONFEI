using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Ultimus.UWF.Form.ProcessControl;
namespace Presale.Process.BudgetRequestAndApproval
{
    public partial class ReviewPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

              object StepName = Request.QueryString["StepName"];
                object Incident = Request.QueryString["Incident"];
                object Type = Request.QueryString["Type"];
                if (StepName != null && Incident != null)
                {
                    object ProcessFormID = DataAccess.Instance("BizDB").ExecuteScalar("select FORMID  from  PROC_Budget where INCIDENT='" + Incident.ToString() + "'");
                    if (ProcessFormID != null)
                        hidFormID.Value = ProcessFormID.ToString();
                    hidCostCenter.Value = ChangeDeptAndCostCenter(StepName.ToString());
                    LoadRPList(hidCostCenter.Value, ProcessFormID.ToString(), Type.ToString());
                }

            ((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
            ((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
        }
        protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
        {
        }
        protected void NewRequest_AfterSubmit(object j, CancelEventArgs g)
        {
        }
        public void LoadRPList(string CostCenter, string ProcessFormID, string ProcessType)
        {

             List<SqlParameter> p1 = new List<SqlParameter>();
            p1.Add(new SqlParameter("@CostCenter", CostCenter));
            string StrsqlAccountType = " select  distinct AccountType from COM_BugetDeptAccount   where  CostCenter=@CostCenter  and IsActive=1";
            DataTable dtAccountType = DataAccess.Instance("BizDB").ExecuteDataTable(StrsqlAccountType, p1.ToArray());
            if (dtAccountType.Rows.Count > 0)
            {
                RPAccountType.DataSource = dtAccountType;
                RPAccountType.DataBind();

                foreach (RepeaterItem item in RPAccountType.Items)
                {
                    Repeater RPList = item.FindControl("RPList") as Repeater;
                    Label lbAccountType = item.FindControl("lbAccountType") as Label;

                    List<SqlParameter> p = new List<SqlParameter>();
                    p.Add(new SqlParameter("@CostCenter", CostCenter));
                   // p.Add(new SqlParameter("@FORMID", ProcessFormID));
                    p.Add(new SqlParameter("@AccountType", lbAccountType.Text));

                    string StrSqlDeptInfo = @"select '" + lbAccountType.Text + "' as AccountType, B.BugetAccountNo,B.BugetAccountDesc,ID from (select  distinct AccountID  from COM_BugetDeptAccount where  CostCenter=@CostCenter  and IsActive=1 and  AccountType=@AccountType) A  left join COM_BudgetAccount B on A.AccountID=B.ID where IsActive=1 ORDER BY BugetAccountNo";
                    DataTable dtDeptBugetDetail = DataAccess.Instance("BizDB").ExecuteDataTable(StrSqlDeptInfo, p.ToArray());
                    if (dtDeptBugetDetail.Rows.Count > 0)
                    {
                        RPList.DataSource = dtDeptBugetDetail;
                        RPList.DataBind();
                    }


                }
            }
        }

        protected void RPList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HiddenField hidAccountID = e.Item.FindControl("hidAccountID") as HiddenField;
            Repeater fld_detail_PROC_Budget_DT = e.Item.FindControl("read_detail_PROC_Budget_DT") as Repeater;
            HiddenField lbAccountType = e.Item.FindControl("hdlbAccountType") as HiddenField;
            string TaskType = Request.QueryString["Type"];
            string CostCenter = hidCostCenter.Value;
            string AccountID = hidAccountID.Value;
             
            string StrSql = "";
            List<SqlParameter> p = new List<SqlParameter>();
            //if (TaskType == "mytask")
            //{
            //    p.Add(new SqlParameter("@AccountID", AccountID));
            //    p.Add(new SqlParameter("@CostCenter", CostCenter));
            //    StrSql = "select ID,AccountID BugetAccountID ,SubAccountDesc Item,'' FORMID,'' ROWID,'' Jan,'' Feb,'' Mar,'' Apr,'' May,'' Jun,'' July,'' Aug,'' Sep,'' Oct,'' Nov,'' Dec,'' SubTotal  from COM_BugetDeptAccount where AccountID=@AccountID and IsActive=1  and CostCenter=@CostCenter";

            //}
            //else
            //{
                p.Add(new SqlParameter("@FORMID", hidFormID.Value));
                p.Add(new SqlParameter("@CostCenter", CostCenter));
                p.Add(new SqlParameter("@BugetAccountID", AccountID));
                p.Add(new SqlParameter("@AccountType", lbAccountType.Value));


                StrSql = "select *  from  PROC_Budget_DT where FORMID=@FORMID and CostCenter=@CostCenter and BugetAccountID=@BugetAccountID AND AccountType=@AccountType";
            //}
            DataTable dtBugetDetail = DataAccess.Instance("BizDB").ExecuteDataTable(StrSql, p.ToArray());
            if (dtBugetDetail.Rows.Count > 0)
            {
                fld_detail_PROC_Budget_DT.DataSource = dtBugetDetail;
                fld_detail_PROC_Budget_DT.DataBind();
            }
        }
        public string ChangeDeptAndCostCenter(string StepName)
        {
            string CostCenter = "";
            string Strsql = "select * from WF_PROCESSSTEP where PROCESSNAME='Budget Process' and STEPNAME='" + StepName + "'";
            DataTable dtCostCenterStepname = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            if (dtCostCenterStepname.Rows.Count > 0)
            {
                CostCenter = dtCostCenterStepname.Rows[0]["EXT01"].ToString();
                lbDept.Text = dtCostCenterStepname.Rows[0]["EXT02"].ToString();
            }
            //switch (StepName)
            //{
            //    case "FIN Approve2":
            //    case "Fin Approve CTO":
            //        CostCenter = "50805500";
            //        lbDept.Text = "CTO";
            //        break;
            //    case "FIN Approve3":
            //    case "FIN Approve GM":
            //        CostCenter = "50805000";
            //        lbDept.Text = "GM";
            //        break;
            //    case "FIN Approve4":
                

            //    case "FIN Approve IT":
            //        CostCenter = "50801500";
            //        lbDept.Text = "IT";
            //        break;
            //    case "FIN Approve5":
            //    case "FIN Approve Admin":
            //        lbDept.Text = "Admin";
            //        CostCenter = "50801010";
            //        break;
            //    case "FIN Approve6":
            //    case "FIN Approve Qua":
            //        CostCenter = "50806500";
            //        lbDept.Text = "Quality";
            //        break;
            //    case "FIN Approve7":
            //    case "FIN Approve HR":
            //        CostCenter = "50801000";
            //        lbDept.Text = "HR";
            //        break;
            //    case "FIN Approve8":
            //    case "FIN Approve Engineer":
            //        CostCenter = "50806200";
            //        lbDept.Text = "Engineer";
            //        break;
            //    case "FIN Approve9":
            //    case "FIN Approve Fin":
            //        CostCenter = "50803000";
            //        lbDept.Text = "FIN";
            //        break;
            //    case "FIN Approve1":
            //    case "FIN Approve OP":
            //        CostCenter = "50808510";
            //        lbDept.Text = "OP";
            //        break;
            //    default:
            //        CostCenter = "";
            //        break;
            //}
            return CostCenter;
        }
    }
}