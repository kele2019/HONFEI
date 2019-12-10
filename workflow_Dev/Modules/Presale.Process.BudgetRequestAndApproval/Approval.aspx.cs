using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Form.ProcessControl;
using MyLib;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Presale.Process.Common;

namespace Presale.Process.BudgetRequestAndApproval
{
	public partial class Approval : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                object StepName = Request.QueryString["StepName"];
                hidStepName.Value = StepName.ToString();
                object Incident = Request.QueryString["Incident"];
                object Type = Request.QueryString["Type"];
                if (StepName != null && Incident!=null)
                {
                    DataTable ProcessInfo = DataAccess.Instance("BizDB").ExecuteDataTable("select FORMID,BudgetType,Year from  PROC_Budget where INCIDENT='" + Incident.ToString() + "'");
                    if (ProcessInfo.Rows.Count > 0)
                    {
                        object ProcessFormID = ProcessInfo.Rows[0]["FORMID"];
                        hidBudgetType.Value = ProcessInfo.Rows[0]["BudgetType"].ToString();
                        hidBudgetYear.Value = ProcessInfo.Rows[0]["Year"].ToString();
                        if (ProcessFormID != null)
                            hidFormID.Value = ProcessFormID.ToString();
                        hidCostCenter.Value = ChangeDeptAndCostCenter(StepName.ToString());
                        // CreateViewStruct(hidCostCenter.Value, ProcessFormID.ToString(), Type.ToString());
                        LoadRPList(hidCostCenter.Value, ProcessFormID.ToString(), Type.ToString());
                        GetDataBind();
                    }
                }
            }
            ((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
            ((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
		}
        protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
        {

            if (!SaveBudget())
            {
                g.Cancel = true;
                MessageBox.Show(this.Page, "Save Data Faild");
               
            }
             

        }
        protected void NewRequest_AfterSubmit(object j, CancelEventArgs g)
        {
            //SaveBudget();
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public bool SaveBudget()
        {
            StringBuilder Strsql = new StringBuilder();
            List<SqlParameter> ListP = new List<SqlParameter>();
            int RowIndex = 0;
            Strsql.Append("delete from PROC_Budget_DT where FORMID=@FORMID and CostCenter=@CostCenter ");

            ListP.Add(new SqlParameter("@FORMID", hidFormID.Value));
            ListP.Add(new SqlParameter("@CostCenter", hidCostCenter.Value));

            foreach (RepeaterItem item1 in RPAccountType.Items)
            {
                Label lbAccountType = item1.FindControl("lbAccountType") as Label;
                Repeater RPList = item1.FindControl("RPList") as Repeater;
            foreach (RepeaterItem itemDept in RPList.Items)
            {
                Repeater fld_detail_PROC_Budget_DT = itemDept.FindControl("fld_detail_PROC_Budget_DT") as Repeater;
                foreach (RepeaterItem item in fld_detail_PROC_Budget_DT.Items)
                {
                    #region 获取数据
                    Label fld_ROWID = item.FindControl("fld_ROWID") as Label;//行号
                    TextBox fld_FORMID = item.FindControl("fld_FORMID") as TextBox;//FORMID
                    HiddenField hidBugetAccountID = item.FindControl("hidBugetAccountID") as HiddenField;//科目ID
                    HiddenField hidID = item.FindControl("hidID") as HiddenField;//详细项ID
                    Label fld_Item = item.FindControl("fld_Item") as Label;//科目
                    TextBox fld_Explain = item.FindControl("fld_Explain") as TextBox;//Explain
                    TextBox fld_Jan = item.FindControl("fld_Jan") as TextBox;//1月
                    TextBox fld_Feb = item.FindControl("fld_Feb") as TextBox;//2月
                    TextBox fld_Mar = item.FindControl("fld_Mar") as TextBox;//3月
                    TextBox fld_Apr = item.FindControl("fld_Apr") as TextBox;//4月
                    TextBox fld_May = item.FindControl("fld_May") as TextBox;//5月
                    TextBox fld_Jun = item.FindControl("fld_Jun") as TextBox;//6月
                    TextBox fld_July = item.FindControl("fld_July") as TextBox;//7月
                    TextBox fld_Aug = item.FindControl("fld_Aug") as TextBox;//8月
                    TextBox fld_Sep = item.FindControl("fld_Sep") as TextBox;//9月
                    TextBox fld_Oct = item.FindControl("fld_Oct") as TextBox;//10月
                    TextBox fld_Nov = item.FindControl("fld_Nov") as TextBox;//11月
                    TextBox fld_Dec = item.FindControl("fld_Dec") as TextBox;//12月
                    HiddenField fld_SubTotal = item.FindControl("fld_SubTotal") as HiddenField;// 小计
                    #endregion

                    #region 写入数据库

                    Strsql.Append(@" Insert into PROC_Budget_DT(FORMID,ROWID,AccountType,BugetAccountID,ID,Item,Explain,Jan,Feb,Mar,Apr,May,Jun,July,Aug,Sep,Oct,Nov,Dec,CostCenter,SubTotal)");
                    Strsql.Append(" values (@FORMID" + RowIndex + ",@ROWID" + RowIndex + ",@AccountType" + RowIndex + ",@BugetAccountID" + RowIndex + ",@ID" + RowIndex + ",@Item" + RowIndex + ",@Explain" + RowIndex + ",@Jan" + RowIndex + ",@Feb" + RowIndex + ",@Mar" + RowIndex + ",@Apr" + RowIndex + ",@May" + RowIndex + ",@Jun" + RowIndex + ",@July" + RowIndex + ",@Aug" + RowIndex + ",@Sep" + RowIndex + ",@Oct" + RowIndex + ",@Nov" + RowIndex + ",@Dec" + RowIndex + ",@CostCenter" + RowIndex + ",@SubTotal" + RowIndex + ")");
                    ListP.Add(new SqlParameter("@FORMID" + RowIndex, hidFormID.Value));
                    ListP.Add(new SqlParameter("@ROWID" + RowIndex, fld_ROWID.Text));
                    ListP.Add(new SqlParameter("@AccountType" + RowIndex, lbAccountType.Text));
                    ListP.Add(new SqlParameter("@BugetAccountID" + RowIndex, hidBugetAccountID.Value));
                    ListP.Add(new SqlParameter("@ID" + RowIndex, hidID.Value));
                    ListP.Add(new SqlParameter("@Item" + RowIndex, fld_Item.Text));
                    ListP.Add(new SqlParameter("@Explain" + RowIndex, fld_Explain.Text));
                    ListP.Add(new SqlParameter("@Jan" + RowIndex, ChangeBudgetPrice(fld_Jan.Text.Trim())));
                    ListP.Add(new SqlParameter("@Feb" + RowIndex, ChangeBudgetPrice(fld_Feb.Text)));
                    ListP.Add(new SqlParameter("@Mar" + RowIndex, ChangeBudgetPrice(fld_Mar.Text)));
                    ListP.Add(new SqlParameter("@Apr" + RowIndex, ChangeBudgetPrice(fld_Apr.Text)));
                    ListP.Add(new SqlParameter("@May" + RowIndex, ChangeBudgetPrice(fld_May.Text)));
                    ListP.Add(new SqlParameter("@Jun" + RowIndex, ChangeBudgetPrice(fld_Jun.Text)));
                    ListP.Add(new SqlParameter("@July" + RowIndex,  ChangeBudgetPrice(fld_July.Text)));
                    ListP.Add(new SqlParameter("@Aug" + RowIndex,ChangeBudgetPrice(fld_Aug.Text)));
                    ListP.Add(new SqlParameter("@Sep" + RowIndex,ChangeBudgetPrice(fld_Sep.Text)));
                    ListP.Add(new SqlParameter("@Oct" + RowIndex,ChangeBudgetPrice(fld_Oct.Text)));
                    ListP.Add(new SqlParameter("@Nov" + RowIndex,ChangeBudgetPrice(fld_Nov.Text)));
                    ListP.Add(new SqlParameter("@Dec" + RowIndex, ChangeBudgetPrice(fld_Dec.Text)));
                    ListP.Add(new SqlParameter("@CostCenter" + RowIndex, hidCostCenter.Value));
                    ListP.Add(new SqlParameter("@SubTotal" + RowIndex, ChangeBudgetPrice(fld_SubTotal.Value)));
                    RowIndex++;
                    #endregion
                }
            }
            }
           return DataAccess.Instance("BizDB").ExecuteNonQuery(Strsql.ToString(), ListP.ToArray())>0;
        }


        public void LoadRPList(string CostCenter, string ProcessFormID, string ProcessType)
        {
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(new SqlParameter("@CostCenter", CostCenter));
            string StrsqlAccountType = " select  distinct AccountType from COM_BugetDeptAccount   where  CostCenter=@CostCenter  and IsActive=1";
            DataTable dtAccountType = DataAccess.Instance("BizDB").ExecuteDataTable(StrsqlAccountType, p.ToArray());
              if (dtAccountType.Rows.Count > 0)
              {
                  RPAccountType.DataSource = dtAccountType;
                  RPAccountType.DataBind();

                  foreach (RepeaterItem item in RPAccountType.Items)
                  {
                      Repeater RPList = item.FindControl("RPList") as Repeater;
                      Label lbAccountType = item.FindControl("lbAccountType") as Label;
                 
                //  p.Add(new SqlParameter("@FORMID", ProcessFormID));

                      List<SqlParameter> p1 = new List<SqlParameter>();
                      p1.Add(new SqlParameter("@CostCenter", CostCenter));
                      p1.Add(new SqlParameter("@AccountType", lbAccountType.Text));

                      string StrSqlDeptInfo = @"select  '" + lbAccountType.Text + "' AS AccountType, B.BugetAccountNo,B.BugetAccountDesc,ID from (select  distinct AccountID  from COM_BugetDeptAccount where  CostCenter=@CostCenter  and IsActive=1  and AccountType=@AccountType) A " +
                                         "left join COM_BudgetAccount B on A.AccountID=B.ID where IsActive=1 ORDER BY BugetAccountNo";
                  DataTable dtDeptBugetDetail = DataAccess.Instance("BizDB").ExecuteDataTable(StrSqlDeptInfo, p1.ToArray());
                  if (dtDeptBugetDetail.Rows.Count > 0)
                  {
                      RPList.DataSource = dtDeptBugetDetail;
                      RPList.DataBind();
                  }
                  }
              }
        }


        public void CreateViewStruct(string CostCenter,string ProcessFormID,string ProcessType)
        {
          
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(new SqlParameter("@FORMID", ProcessFormID));
            p.Add(new SqlParameter("@CostCenter", CostCenter));

            string Strsql = "select *  from  PROC_Budget_DT where FORMID=@FORMID and CostCenter=@CostCenter ";
            DataTable dtBugetDetail = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql, p.ToArray());

            if (dtBugetDetail.Rows.Count <= 0&&ProcessType.ToLower() == "mytask")
            {
                List<SqlParameter> p1 = new List<SqlParameter>();
                p1.Add(new SqlParameter("@CostCenter", CostCenter));
                string Accountsql = "select ID,AccountID BugetAccountID ,SubAccountDesc Item,'' FORMID,'' ROWID,'' Jan,'' Feb,'' Mar,'' Apr,'' May,'' Jun,'' July,'' Aug,'' Sep,'' Oct,'' Nov,'' Dec,'' SubTotal   from COM_BugetDeptAccount where  CostCenter=@CostCenter and IsActive=1";
                dtBugetDetail = DataAccess.Instance("BizDB").ExecuteDataTable(Accountsql, p1.ToArray());
            }

            
            ViewState["Buget"] = dtBugetDetail;

        }

        


        public void GetDataBind()
        {
            //DataTable dtBugetDetail =(ViewState["Buget"]) as DataTable;
            //if (dtBugetDetail.Rows.Count > 0)
            //    fld_detail_PROC_Budget_DT.DataSource = dtBugetDetail;
            //else
            //    fld_detail_PROC_Budget_DT.DataSource = null;
            //fld_detail_PROC_Budget_DT.DataBind();

        }

        public void EditBugetDetail(string OperatorType,string RowID)
        {
            //DataTable dtBugetDetail = (ViewState["Buget"]) as DataTable;
            //dtBugetDetail.Rows.Clear();
            //foreach (RepeaterItem item in fld_detail_PROC_Budget_DT.Items)
            //{
            //    Label fld_ROWID = item.FindControl("fld_ROWID") as Label;//行号
            //    if (OperatorType=="Add"||(OperatorType == "Del"&&RowID != fld_ROWID.Text))
            //    {
            //        DataRow NewRow = dtBugetDetail.NewRow();
            //        #region 获取数据
            //        TextBox fld_FORMID = item.FindControl("fld_FORMID") as TextBox;//FORMID
            //        HiddenField hidBugetAccountID = item.FindControl("hidBugetAccountID") as HiddenField;//科目ID
            //        HiddenField hidID = item.FindControl("hidID") as HiddenField;//详细项ID
            //        TextBox fld_Item = item.FindControl("fld_Item") as TextBox;//科目
            //        TextBox fld_Jan = item.FindControl("fld_Jan") as TextBox;//1月
            //        TextBox fld_Feb = item.FindControl("fld_Feb") as TextBox;//2月
            //        TextBox fld_Mar = item.FindControl("fld_Mar") as TextBox;//3月
            //        TextBox fld_Apr = item.FindControl("fld_Apr") as TextBox;//4月
            //        TextBox fld_May = item.FindControl("fld_May") as TextBox;//5月
            //        TextBox fld_Jun = item.FindControl("fld_Jun") as TextBox;//6月
            //        TextBox fld_July = item.FindControl("fld_July") as TextBox;//7月
            //        TextBox fld_Aug = item.FindControl("fld_Aug") as TextBox;//8月
            //        TextBox fld_Sep = item.FindControl("fld_Sep") as TextBox;//9月
            //        TextBox fld_Oct = item.FindControl("fld_Oct") as TextBox;//10月
            //        TextBox fld_Nov = item.FindControl("fld_Nov") as TextBox;//11月
            //        TextBox fld_Dec = item.FindControl("fld_Dec") as TextBox;//12月
            //        HiddenField fld_SubTotal = item.FindControl("fld_SubTotal") as HiddenField;// 小计
            //        #endregion
            //        #region 赋值
            //        NewRow["FORMID"] =hidFormID.Value;
            //        NewRow["BugetAccountID"] = hidBugetAccountID.Value == "" ? "0" : hidBugetAccountID.Value;
            //        NewRow["ID"] = hidID.Value == "" ? "0" : hidID.Value;
            //        NewRow["Item"] = fld_Item.Text;
            //        NewRow["Jan"] = fld_Jan.Text;
            //        NewRow["Feb"] = fld_Feb.Text;
            //        NewRow["Mar"] = fld_Mar.Text;
            //        NewRow["Apr"] = fld_Apr.Text;
            //        NewRow["May"] = fld_May.Text;
            //        NewRow["Jun"] = fld_Jun.Text;
            //        NewRow["July"] = fld_July.Text;
            //        NewRow["Aug"] = fld_Aug.Text;
            //        NewRow["Sep"] = fld_Sep.Text;
            //        NewRow["Oct"] = fld_Oct.Text;
            //        NewRow["Nov"] = fld_Nov.Text;
            //        NewRow["Dec"] = fld_Dec.Text;
            //        NewRow["SubTotal"] = fld_SubTotal.Value;
            //        #endregion
            //        dtBugetDetail.Rows.Add(NewRow);
            //    }
            //}
            //if (OperatorType == "Add")
            //     dtBugetDetail.Rows.Add(dtBugetDetail.NewRow());
            //     ViewState["Buget"] = dtBugetDetail;
            //     GetDataBind();            
        }


        /// <summary>
        /// 明细行添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            //userInfo.AddNewRow(fld_detail_PROC_Budget_DT);
            EditBugetDetail("Add","");
        }
        /// <summary>
        /// 明细行删除
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void fld_detail_PROC_Budget_DT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            //userInfo.DeleteRow(fld_detail_PROC_Budget_DT, e);
            Label RowID = e.Item.FindControl("fld_ROWID") as Label;
            EditBugetDetail("Del", RowID.Text);
        }

        public string ChangeDeptAndCostCenter(string StepName)
        {
            string CostCenter = "";
            string Strsql = "select * from WF_PROCESSSTEP where PROCESSNAME='Budget Process' and STEPNAME='"+StepName+"'";
            DataTable dtCostCenterStepname=DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            if (dtCostCenterStepname.Rows.Count > 0)
            {
                CostCenter = dtCostCenterStepname.Rows[0]["EXT01"].ToString();
                lbDept.Text = dtCostCenterStepname.Rows[0]["EXT02"].ToString();
            }

            //switch (StepName)
            //{
            //    case "GM Approve":
            //    case "GM Submit":
            //        CostCenter = "50805000";
            //        lbDept.Text = "GM";
            //        break;
            //    case "IT Approve":
            //    case "IT Submit":
            //        CostCenter = "50801500";
            //        lbDept.Text = "IT";
            //        break;
            //    case "Admin Approve":
            //    case "Admin Submit":
            //        CostCenter = "50801010";
            //        lbDept.Text = "Admin";
            //        break;
            //    case "Quality Approve":
            //    case "Quality Submit":
            //        CostCenter = "50806500";
            //        lbDept.Text = "Quality";
            //        break;
            //    case "HR Approve":
            //    case "HR Submit":
            //        CostCenter = "50801000";
            //        lbDept.Text = "HR";
            //        break;
            //    case "Engineering Approve":
            //    case "Engineering Submit":
            //        CostCenter = "50806200";
            //        lbDept.Text = "Engineering";
            //        break;
            //    case "FIN Approve":
            //    case "FIN Submit":
            //        CostCenter = "50803000";
            //        lbDept.Text = "FIN";
            //        break;
            //    case "OP Approve":
            //    case "OP Submit":
            //        CostCenter = "50808510";
            //        lbDept.Text = "OP";
            //        break;
            //    case "CTO Approve":
            //    case "CTO Submit":
            //        CostCenter = "50805500";
            //        lbDept.Text = "CTO";
            //        break;
            //    case "PM Approve":
            //    case "PM Submit":
            //        CostCenter = "50808510";
            //        break;
              
            //    default:
            //        CostCenter="";
            //        break;
            //}
            return CostCenter;
        }

        protected void RPList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HiddenField hidAccountID = e.Item.FindControl("hidAccountID") as HiddenField;
            Repeater fld_detail_PROC_Budget_DT = e.Item.FindControl("fld_detail_PROC_Budget_DT") as Repeater;
            string TaskType = Request.QueryString["Type"];
            string CostCenter = hidCostCenter.Value;
            string AccountID = hidAccountID.Value;
            string BugdegeType = hidBudgetType.Value;
            string BudegetYear = hidBudgetYear.Value;
            HiddenField AccountType = e.Item.FindControl("hdlbAccountType") as HiddenField;
            string StrSql = "";
            List<SqlParameter> p = new List<SqlParameter>();
            //p.Add(new SqlParameter("@AccountID", AccountID));
            //p.Add(new SqlParameter("@FORMID", hidFormID.Value));
            //p.Add(new SqlParameter("@CostCenter", CostCenter));
            if (TaskType == "mytask")
            {



                if (BugdegeType == "Full Year")
                {
                    p.Add(new SqlParameter("@FORMID", hidFormID.Value));
                    p.Add(new SqlParameter("@AccountID", AccountID));
                    p.Add(new SqlParameter("@CostCenter", CostCenter));
                    p.Add(new SqlParameter("@AccountType", AccountType.Value));


                    // StrSql = "select ID,AccountID BugetAccountID ,SubAccountDesc Item,'' FORMID,'' ROWID,'' Jan,'' Feb,'' Mar,'' Apr,'' May,'' Jun,'' July,'' Aug,'' Sep,'' Oct,'' Nov,'' Dec,'' SubTotal  from COM_BugetDeptAccount where AccountID=@AccountID and IsActive=1  and CostCenter=@CostCenter";
                    StrSql = @"select  B.Explain,A.ID,AccountID BugetAccountID ,SubAccountDesc Item,B.FORMID,B.ROWID,B.Jan,B.Feb,B.Mar,B.Apr,B.May,B.Jun,B.July,B.Aug,B.Sep,B.Oct,B.Nov,B.Dec,B.SubTotal
      from COM_BugetDeptAccount A left join  (select * from  PROC_Budget_DT where FORMID=@FORMID ) B on A.ID=B.ID
      where  AccountID=@AccountID and A.CostCenter=@CostCenter and A.IsActive=1 AND A.AccountType=@AccountType";
                }
                else
                {
                    p.Add(new SqlParameter("@AccountID", AccountID));
                    p.Add(new SqlParameter("@CostCenter", CostCenter));
                    if (BugdegeType == "4+8")
                    {
                        StrSql = @"select A.ID,AccountID BugetAccountID ,SubAccountDesc Item,B.Explain,B.FORMID,B.ROWID,'0' Jan,'0' Feb,'0' Mar,'0' Apr,B.May,B.Jun,B.July,B.Aug,B.Sep,B.Oct,B.Nov,B.Dec,B.SubTotal
      from COM_BugetDeptAccount A left join  (select PBD.* from (
    select FORMID from  PROC_Budget where Year='" + BudegetYear + "' and BudgetType='Full Year' and INCIDENT>0 and STATUS=2) PB" +
        " left join  PROC_Budget_DT  PBD on PB.FORMID=PBD.FORMID ) B on A.ID=B.ID      where  AccountID=@AccountID and A.CostCenter=@CostCenter and A.IsActive=1";
                    }
                    if (BugdegeType == "7+5")
                    {
                        StrSql = @"select A.ID,AccountID BugetAccountID ,SubAccountDesc Item,B.Explain,B.FORMID,B.ROWID,'0' Jan,'0' Feb,'0' Mar,'0' Apr,'0' May,'0' Jun,'0' July,B.Aug,B.Sep,B.Oct,B.Nov,B.Dec,B.SubTotal
      from COM_BugetDeptAccount A left join  (select PBD.* from (
    select FORMID from  PROC_Budget where Year='" + BudegetYear + "' and BudgetType='4+8' and INCIDENT>0 and STATUS=2) PB" +
        " left join  PROC_Budget_DT  PBD on PB.FORMID=PBD.FORMID ) B on A.ID=B.ID      where  AccountID=@AccountID and A.CostCenter=@CostCenter and A.IsActive=1";
                    }
                }

            }
            else
            {
                p.Add(new SqlParameter("@FORMID", hidFormID.Value));
                p.Add(new SqlParameter("@CostCenter", CostCenter));
                p.Add(new SqlParameter("@BugetAccountID", AccountID));
                p.Add(new SqlParameter("@AccountType", AccountType.Value));
                StrSql = "select *  from  PROC_Budget_DT where FORMID=@FORMID and CostCenter=@CostCenter and BugetAccountID=@BugetAccountID AND  AccountType=@AccountType ";
            }
            DataTable dtBugetDetail = DataAccess.Instance("BizDB").ExecuteDataTable(StrSql, p.ToArray());
            if (dtBugetDetail.Rows.Count > 0)
            {
                fld_detail_PROC_Budget_DT.DataSource = dtBugetDetail;
                fld_detail_PROC_Budget_DT.DataBind();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //if (hidFlag.Value == "submit")
                //{
                //    if (!SaveBudget())
                //        MessageBox.Show(this.Page, "Save Faild");
                //    else
                       
                //        // MessageBox.ResponseScript(this.Page, "<script>SubimtFun();</script>");
                //        Page.RegisterStartupScript("key", "<script>SubimtFun();</script>");
                //}
                //else
                //{
                    if (SaveBudget())
                        MessageBox.Show(this.Page, "Save Successfully");
                    else
                        MessageBox.Show(this.Page, "Save Faild");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Page, "Save Exception:"+ex.Message);
            }

        }
        public string ChangeBudgetPrice(string obj)
        {
            if (obj.Trim() == "")
                return "0.00";
            else
                return obj;
        }

	}

}