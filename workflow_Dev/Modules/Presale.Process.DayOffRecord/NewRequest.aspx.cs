using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Presale.Process.DayOffRecord.Entity;
using Ultimus.UWF.Form.ProcessControl;
using System.ComponentModel;
using Presale.Process.Common;
using System.Data;


namespace Presale.Process.DayOffRecord
{
	public partial class NewRequest : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				object ProcessName = Request.QueryString["Processname"];
				object myRequest = Request.QueryString["Type"];
				string Incident = Request.QueryString["Incident"];
				if (Incident != "0")
				{
					hdIncident.Value = Incident;
				}
				if (Incident.ToString() != "0")
				{
					string FlagStatus = DataAccess.Instance("BizDB").ExecuteScalar("select  status from PROC_DayOffRecord where INCIDENT='" + Incident + "' ").ToString();

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

                if (myRequest != null)
                {
                    if (myRequest.ToString().ToUpper() == "NEWREQUEST")
                    {
                        UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
                        userInfo.AddNewRow(fld_detail_PROC_DayOffRecord_DT);
                    }
                }


				fld_ApplierLogin.Text = Page.User.Identity.Name.Replace("\\", "/");
                string sql1 = "select (ISNULL(OTHourCount,0)+isnull(LastYearHourCount,0)) as totalOTHourCount from dbo.COM_OTAndDayOffManage where OTYear='" + DateTime.Now.Year + "' and UserAccount = '" + Page.User.Identity.Name.Replace("\\", "/") + "'";
				OTEntity otEntity = DataAccess.Instance("BizDB").ExecuteEntity<OTEntity>(sql1);
				fld_sumOTHourCount.Text = otEntity.totalOTHourCount.ToString();
				fld_HRLogin.Text = (DataAccess.Instance("BizDB").ExecuteScalar("select LoginName+'|USER' from dbo.ORG_USER where EXT03 ='HRM'").ToString()).Replace("\\", "/");
				fld_HR.Text = DataAccess.Instance("BizDB").ExecuteScalar("select EXT04 from dbo.ORG_USER where EXT03 ='HRM'").ToString();
			}

			((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
			((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
		}
		protected void NewRequest_BeforeSubmit(object sender, CancelEventArgs g)
		{
            #region
            object Incident = Request.QueryString["Incident"];
            object ProcessName = Request.QueryString["Processname"];
            string strRevoke = string.Format("update PROC_REVOKE set status=1,OperatorDate=getdate() where  ProcessName='{0}' and incident='{1}'", ProcessName, Incident);
            DataAccess.Instance("BizDB").ExecuteNonQuery(strRevoke);
            #endregion
			string sql = "select EXT04 from dbo.org_user where loginname = '" + Page.User.Identity.Name + "'";
			UserName user = DataAccess.Instance("BizDB").ExecuteEntity<UserName>(sql);
			fld_ApplicantUser.Text = user.EXT04;
		}
		protected void NewRequest_AfterSubmit(object sender, CancelEventArgs g)
		{
              ApprovalHistory approvalHistory = Page.FindControl("ApprovalHistory1") as ApprovalHistory;
            int ActiontType = approvalHistory.ActionType;
            UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            string FormID = userInfo.FormId;
            if (ActiontType == 0)//同意
            {
                //string sql = "Update COM_OTAndDayOffManage set OTHourCount = OTHourCount-Convert(decimal(5,2),isnull('" + fld_SumHour.Text + "',0.0)) where UserAccount = '" + Page.User.Identity.Name.Replace("\\", "/") + "'";
                //DataAccess.Instance("BizDB").ExecuteNonQuery(sql);
                OperationDayOff(Page.User.Identity.Name.Replace("\\", "/"), FormID, "1");
            }
		}
		protected void btnRevoke_Click(object sender, EventArgs e)//撤销
		{
			object ProcessName = Request.QueryString["Processname"];
			object Incident = Request.QueryString["Incident"];
			object StepName = Request.QueryString["StepName"];
			string strRevoke = string.Format("select status from  dbo.PROC_REVOKE with(nolock) where ProcessName='{0}' and incident='{1}'", ProcessName.ToString(), Incident.ToString());


            //string sql = "Update COM_OTAndDayOffManage set OTHourCount = OTHourCount+Convert(decimal(5,2),isnull(" + fld_SumHour.Text + ",0.0)) where UserAccount = '" + Page.User.Identity.Name.Replace("\\", "/") + "'";
            //    DataAccess.Instance("BizDB").ExecuteNonQuery(sql);
            UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            string FormID = userInfo.FormId;
            OperationDayOff(Page.User.Identity.Name.Replace("\\", "/"), FormID, "2");

			string FlagRevoke = DataAccess.Instance("BizDB").ExecuteScalar(strRevoke).ToString();
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

        public void OperationDayOff(string UserAccount,string FormID,string Flag)
        {
            string Strsql = "SELECT  ID,ISNULL(OTHourCount,0) OTHourCount,ISNULL(LastYearHourCount,0) LastYearHourCount  FROM COM_OTAndDayOffManage where OTYear='" + DateTime.Now.Year + "' and UserAccount='" + UserAccount + "'";
            DataTable dtOTInfo = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);

            string StrsqlFormInfo = "select EndDate,SumHour,ISNULL(REGION,0) REGION,ISNULL(LOCATION,0) LOCATION from PROC_DayOffRecord where FORMID='" + FormID + "'";
            DataTable dtOTFormInfo = DataAccess.Instance("BizDB").ExecuteDataTable(StrsqlFormInfo);
            string StrsqlResult = "";
            if (dtOTInfo.Rows.Count > 0)
            {
                string ID = dtOTInfo.Rows[0]["ID"].ToString();
                decimal OTHourCount = Convert.ToDecimal(dtOTInfo.Rows[0]["OTHourCount"].ToString());
                decimal LastYearHourCount = Convert.ToDecimal(dtOTInfo.Rows[0]["LastYearHourCount"].ToString());

                DateTime DayoffDate = Convert.ToDateTime(dtOTFormInfo.Rows[0]["EndDate"].ToString());
                decimal sumOTHourCount = Convert.ToDecimal(dtOTFormInfo.Rows[0]["SumHour"].ToString());
                decimal REGION = Convert.ToDecimal(dtOTFormInfo.Rows[0]["REGION"].ToString());
                decimal LOCATION = Convert.ToDecimal(dtOTFormInfo.Rows[0]["LOCATION"].ToString());

                decimal LastCount = 0;//Last year
                decimal CurrentCount = 0;//This Year

                if (Flag == "1")//申请
                {
                    if (DayoffDate.Month <= 2)//优先扣减上年的
                    {
                        if (sumOTHourCount>=LastYearHourCount)
                        {
                            LastCount = LastYearHourCount;
                            CurrentCount = (sumOTHourCount-LastYearHourCount);
                        }
                        else
                        {
                            LastCount = sumOTHourCount;
                            CurrentCount = 0;
                        }
                    }
                    else//忽略上年，直接扣除当年
                    {
                        LastCount = 0;
                        CurrentCount = sumOTHourCount;
                    }
                    StrsqlResult = "Update COM_OTAndDayOffManage set LastYearHourCount=(ISNULL(LastYearHourCount,0)-" + LastCount + "), OTHourCount =(ISNULL(OTHourCount,0)-" + CurrentCount + ") where ID=" + ID;
                    DataAccess.Instance("BizDB").ExecuteNonQuery(StrsqlResult);
                    StrsqlResult = "UPDATE PROC_DayOffRecord SET REGION='" + CurrentCount + "',LOCATION='" + LastCount + "' WHERE FORMID='" + FormID + "'";
                    DataAccess.Instance("BizDB").ExecuteNonQuery(StrsqlResult);
                }
                else//退回
                {
                    StrsqlResult = "Update COM_OTAndDayOffManage set LastYearHourCount=(ISNULL(LastYearHourCount,0)+" + LOCATION + "), OTHourCount =(ISNULL(OTHourCount,0)+" + REGION + ") where ID=" + ID;
                    DataAccess.Instance("BizDB").ExecuteNonQuery(StrsqlResult);
                }
            }
        
        }

        protected void btnAdd_Click(object sender, EventArgs e)//添加明细行
        {
            UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            userInfo.AddNewRow(fld_detail_PROC_DayOffRecord_DT);
        }
        protected void fld_detail_PROC_Leave_DT_ItemCommand(object source, RepeaterCommandEventArgs e)//删除明细行
        {
            UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
            userInfo.DeleteRow(fld_detail_PROC_DayOffRecord_DT, e);
        }
       


	}
}
