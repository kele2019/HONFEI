using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data;
using System.ComponentModel;
using MyLib;
using Ultimus.UWF.Form.ProcessControl;
using System.Collections;
using Presale.Process.Common;
using Presale.Process.Leave.Entity;

namespace Presale.Process.Leave
{
	public partial class NewRequest : System.Web.UI.Page
	{
        string UserAccount;
		DataTable dtFinaInfo = DataAccess.Instance("BizDB").ExecuteDataTable("select DicText,DicValue from COM_DICTIONRY where Type = 'Leave' order by DicCode");
		protected void Page_Load(object sender, EventArgs e)
		{
			object ProcessName = Request.QueryString["Processname"];
		
			string Incident = Request.QueryString["Incident"];
			object myRequest = Request.QueryString["Type"];
            UserAccount = Page.User.Identity.Name;
			if (!IsPostBack)
			{
                //当年剩余年假FuallpaySick
                fld_sumAnnualLeave.Text = DataAccess.Instance("BizDB").ExecuteScalar("select LeaveYearCount from dbo.COM_LevalManager where UserAccount='" + UserAccount.Replace("\\", "/") + "' and LeaveYear=DATEPART(YY,GETDATE()) ").ToString();
                text_FuallpaySick.Text = DataAccess.Instance("BizDB").ExecuteScalar("select FuallpaySick from dbo.COM_LevalManager where UserAccount='" + UserAccount.Replace("\\", "/") + "' and LeaveYear=DATEPART(YY,GETDATE()) ").ToString();
                fld_DGMLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='DGM'").ToString()).Replace("\\", "/");
                fld_HRLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='HRM'").ToString()).Replace("\\", "/");
                fld_DGM.Text = DataAccess.Instance("BizDB").ExecuteScalar("select EXT04 from dbo.ORG_USER where EXT03 ='DGM'").ToString();
                fld_HR.Text = DataAccess.Instance("BizDB").ExecuteScalar("select EXT04 from dbo.ORG_USER where EXT03 ='HRM'").ToString();
                if (myRequest != null)
                {
                    if (myRequest.ToString().ToUpper() == "NEWREQUEST")
                    {
                        UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
                        userInfo.AddNewRow(fld_detail_PROC_Leave_DT);
                    }
                }

                if (Incident != "0")
                {
                    hdIncident.Value = Incident;
                }
                if (Incident.ToString() != "0")
                {
                    string FlagStatus = DataAccess.Instance("BizDB").ExecuteScalar("select  status from PROC_Leave where INCIDENT='" + Incident + "' ").ToString();

                    if (FlagStatus == "1" && int.Parse(Incident.ToString()) > 0 && myRequest.ToString().ToUpper() == "MYREQUEST")
                    {
                        hdUrgeTask.Value = "Yes";
                        string strRevoke = string.Format("select status from  dbo.PROC_REVOKE with(nolock) where ProcessName='{0}' and incident='{1}'", ProcessName, Incident);
                        object FlagRevoke = DataAccess.Instance("BizDB").ExecuteScalar(strRevoke);
                        if (FlagRevoke != null)
                        {
                            if (FlagRevoke.ToString() == "1")
                            {
                                btnRevoke.Visible = true;
                            }

                        }
                        object Requestor = Request.QueryString["Requestor"];
                        if (Requestor != null)
                        {
                            string CurrentUser = ConfigurationManager.AppSettings["Domain"] + "\\" + Requestor.ToString();
                            if (Page.User.Identity.Name.ToLower() == CurrentUser.ToLower())
                            {

                                btnRevoke.Visible = false;
                            }
                        }
                    }
                    else
                    {

                    }
                }
			}
			((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
            ((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
        }
        protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
        {
            #region
            object Incident = Request.QueryString["Incident"];
            object ProcessName = Request.QueryString["Processname"];
            string strRevoke = string.Format("update PROC_REVOKE set status=1,OperatorDate=getdate() where  ProcessName='{0}' and incident='{1}'", ProcessName, Incident);
            DataAccess.Instance("BizDB").ExecuteNonQuery(strRevoke);

            Hashtable table = (Hashtable)sender;
            BusniessClass ApproveClass = new BusniessClass();
            DataTable dtuser = ApproveClass.GetUserDOA(Page.User.Identity.Name);
            if (dtuser.Rows.Count > 0)
            {
                if (dtuser.Rows[0]["DEPARTMENTTYPE"].ToString() == "Dept" && dtuser.Rows[0]["IDNO"].ToString() != "1")
                {
                    table.Add("IDNO", "0");//三级部门用户
                }
                else
                {
                    if (dtuser.Rows[0]["IDNO"].ToString() == "1")
                    {
                        //fld_DGMLogin.Text = "1";
                        table.Add("IDNO", "1");//部门经理
                    }

                }
                if (dtuser.Rows[0]["IDNO"].ToString() == "0")
                {
                    table.Remove("IDNO");
                  table.Add("IDNO", "");
                    //fld_DGMLogin.Text = "";//二级部门用户
                }

            }

            #endregion
        }
		protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)//提交后执行的操作
		{
            UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            string FormID = userInfo.FormId;
            ApprovalHistory approvalHistory = Page.FindControl("ApprovalHistory1") as ApprovalHistory;
            int ActiontType = approvalHistory.ActionType;
            #region
            if (ActiontType == 0)//同意
            {
				DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable("select  ROWID  from PROC_Leave_DT where Applying='Annual Leave' and formid='" + FormID + "'");//年假计算
                 for (int i = 0; i < dt.Rows.Count; i++)
                 {
                     string ROWID = dt.Rows[i]["ROWID"].ToString();
					 string strinsert = string.Format("exec checkALdays_new_forhongfei '" + FormID + "','" + UserAccount.Replace("\\", "/") + "','" + ROWID + "'");
                     DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert);
                    
                 }
				 DataTable newdt = DataAccess.Instance("BizDB").ExecuteDataTable("select  ROWID,NoODays  from PROC_Leave_DT where Applying='Full Pay Sick Leave' and formid='" + FormID + "'");//全薪病假计算
                 for (int i = 0; i < newdt.Rows.Count; i++)
                 {
                     //string ROWID = newdt.Rows[i]["ROWID"].ToString();
                     string leaves = newdt.Rows[i]["NoODays"].ToString();
					 string strinsert = string.Format("update COM_LevalManager set FuallpaySick=FuallpaySick-Convert(decimal(4,2),isnull('" + leaves + "',0.0)) where UserAccount='" + UserAccount.Replace("\\", "/") + "' and LeaveYear=DATEPART(YY,GETDATE())");
                     DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert);
                 }
            }
            #endregion
          
		}
		protected void btnAdd_Click(object sender, EventArgs e)//添加明细行
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.AddNewRow(fld_detail_PROC_Leave_DT);
		}
		protected void fld_detail_PROC_Leave_DT_ItemCommand(object source, RepeaterCommandEventArgs e)//删除明细行
		{
			UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
			userInfo.DeleteRow(fld_detail_PROC_Leave_DT, e);
		}
		public void fld_detail_PROC_Leave_DT_ItemDataBound(object sender, RepeaterItemEventArgs e)//数据绑定
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DropDownList dropApplying = e.Item.FindControl("dropApplying") as DropDownList;
				dropApplying.DataSource = dtFinaInfo;
				dropApplying.DataTextField = "DicText";
				dropApplying.DataValueField = "DicValue";
				dropApplying.DataBind();
				dropApplying.Items.Insert(0, new ListItem("--Select Please--", "0"));
			}
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			try
			{
				string tablename = fld_detail_PROC_Leave_DT.ID.Replace("fld_detail_", "").Replace("read_detail_", "");
				UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
				DataTable dt = userInfo.GetDetailData(fld_detail_PROC_Leave_DT);
				foreach (RepeaterItem item in fld_detail_PROC_Leave_DT.Items)
				{
					HtmlInputCheckBox cb = item.FindControl("cb_SelectValue") as HtmlInputCheckBox;
					if (cb.Checked)
					{
						foreach (DataRow row in dt.Rows)
						{
							//if (row["ROWID"].ToString() == cb.Value)
							//{
							row.Delete();
							dt.AcceptChanges();
							break;
							//}
						}
					}
				}
				fld_detail_PROC_Leave_DT.DataSource = dt;
				fld_detail_PROC_Leave_DT.DataBind();
			}
			catch (Exception ex)
			{
				MyLib.LogUtil.Error(ex);
				//Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert(\"" + ex.Message.Replace("\r\n", " ").Replace("\n", "").Replace("'", "") + "\");</script>");
			}
		}
		protected void btnRevoke_Click(object sender, EventArgs e)//撤销
		{
			object ProcessName = Request.QueryString["Processname"];
			object Incident = Request.QueryString["Incident"];
			object StepName = Request.QueryString["StepName"];
			string strRevoke = string.Format("select status from  dbo.PROC_REVOKE with(nolock) where ProcessName='{0}' and incident='{1}'", ProcessName.ToString(), Incident.ToString());
			string FlagRevoke = DataAccess.Instance("BizDB").ExecuteScalar(strRevoke).ToString();



            UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            string FormID = userInfo.FormId;
            string strinsert = string.Format("exec proc_ALdayRerejeck '" + FormID + "'");
            DataAccess.Instance("BizDB").ExecuteNonQuery(strinsert);

            DataTable newdt = DataAccess.Instance("BizDB").ExecuteDataTable("select  ROWID,NoODays  from PROC_Leave_DT where Applying='Full Pay Sick Leave' and formid='" + FormID + "'");//全薪病假计算
            for (int i = 0; i < newdt.Rows.Count; i++)
            {
                string ROWID = newdt.Rows[i]["ROWID"].ToString();
                string leaves = newdt.Rows[i]["NoODays"].ToString();
                string backSick = string.Format("update COM_LevalManager set FuallpaySick=FuallpaySick+Convert(decimal(4,2),isnull('" + leaves + "',0.0)) where LeaveYear=DATEPART(YY,GETDATE()) and  UserAccount in (select APPLICANTACCOUNT  from proc_leave where formid='" + FormID + "')");
                DataAccess.Instance("BizDB").ExecuteNonQuery(backSick);
            }

			if (FlagRevoke != "2")
			{
				if (GetOrgLevel.RevokeFunc(ProcessName.ToString(), StepName.ToString(), Incident.ToString(), Page.User.Identity.Name))
				{
					Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "RevokSuccess()", true);

				}
				else
				{
					MessageBox.Show(this.Page, "撤回失败！\\nRevoke Faile!");
				}
			}
			else
			{
				MessageBox.Show(this.Page, "任务已经被处理，无法撤回！\\n Task Already Pass, Don't Revoke!");
			}
		}
	}
}