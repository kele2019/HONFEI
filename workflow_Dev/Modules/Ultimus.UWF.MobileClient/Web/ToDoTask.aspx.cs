﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MobileClient.PublicFunctionClass;
//using MobileClient.MobileClientBackgroundRef;
using ClientService;
using ClientService.WorkflowSrv;
using EntityLibrary;
using MyLib;
using System.Data;

namespace MobileClient
{
    public partial class ToDoTask : BasePageClass.BasePage
    {
        private WorkflowRef services = new WorkflowRef();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BingMyTask();
            }
        }

        private void BingMyTask()
        {
            DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable("select * from MOBILECLIENT_STEP a inner join MOBILECLIENT_PROCESS b on a.FK_ID=b.ID where a.ISCREATEPAGE=1 and (a.STEPCNAME!='' and a.STEPCNAME is not null and a.STEPENAME!='' and a.STEPENAME is not null)");

            try
            {
                UserEntity uUserInfo = Session["UserInfo"] as UserEntity;
                List<TaskEntity> info = new List<TaskEntity>();
                info.AddRange(services.GetMyTaskList(uUserInfo.LOGINNAME));
                //foreach (TaskEntity te in info) 
                //{
                //    services.GetUserInfoBySearchText(te.
                //}
                ////    APLICANT
                //根据条件过滤
                string summary = txtSummary.Text.Trim();
                if (txtSummary.Text.Trim() == "流程摘要") summary = "";
                List<TaskEntity> newinfo = info.FindAll(delegate(TaskEntity t)
                {
                    //if (!String.IsNullOrEmpty(txtProcessName.Text.Trim()))
                    //{
                    //    return t.PROCESSNAME.Contains(txtProcessName.Text.Trim());
                    //}
                    //if (!String.IsNullOrEmpty(txtIncident.Text.Trim()))
                    //{
                    //    return t.INCIDENT.ToString().Contains(txtIncident.Text.Trim());
                    //}
                    //if (!String.IsNullOrEmpty(txtStepName.Text.Trim()))
                    //{
                    //    return t.STEPLABEL.Contains(txtStepName.Text.Trim());
                    //}
                    if (dt.Select("ProcessName='" + t.PROCESSNAME + "' and StepName='" + t.STEPLABEL + "'").Length == 0)
                    {
                        return false;
                    }
                    if (!String.IsNullOrEmpty(summary))
                    {
                        string sy = "";
                        if (t.SUMMARY != null) sy = t.SUMMARY;
                        return sy.Contains(summary);
                    }
                    return true;
                });

                foreach (TaskEntity te in newinfo)
                {
                    te.APLICANT = GetApplicant(te.INCIDENT, te.PROCESSNAME.Trim());
                }

                newinfo.Sort(delegate(TaskEntity p1, TaskEntity p2) { return Comparer<DateTime>.Default.Compare(p2.STARTTIME, p1.STARTTIME); });

                Repeater1.DataSource = newinfo.ToArray();
                Repeater1.DataBind();

            }
            catch (Exception ex)
            {
                PublicClass.ShowMessage(this.Page, Resources.Resource.ToDoTask_ErrorMessage1);
                PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            BingMyTask();
        }

        protected string GetApplicant(int incidentId, string processName)
        {
            string appAccount = "";
            try
            {
                string sql = "SELECT INITIATOR FROM  [INCIDENTS] (NOLOCK) WHERE INCIDENT=" + incidentId.ToString() + " and PROCESSNAME='" + processName + "' ";
                appAccount = DataAccess.Instance("UltDB").ExecuteScalar(sql).ToString().Trim();
                string[] appAccount_arry = appAccount.Split('/');
                if (appAccount_arry.Length > 1) { appAccount = appAccount_arry[1]; }
                appAccount = services.GetUserInfoBySearchText(appAccount)[0].USERNAME.Trim();
            }
            catch (Exception ex)
            {
                return "";
            }
            return appAccount;
        }
    }
}