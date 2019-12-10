using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Workflow.Interface;
using Ultimus.UWF.Common.Logic;
using MyLib;
using Ultimus.UWF.Workflow.Entity;
using System.Collections;
using Ultimus.UWF.Common.Entity;
using System.Data;

namespace Ultimus.UWF.Workflow
{
    public partial class TaskList : System.Web.UI.Page
    {
        IProcessCategory _category = ServiceContainer.Instance().GetService<IProcessCategory>();
        ITask _task = ServiceContainer.Instance().GetService<ITask>();
        public string COUNT = "0";
        public string CanNotCancel = Lang.Get("TaskList_CanNotCancel");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userkey = SessionLogic.GetLoginName().Replace("\\", "/");
                string sessionid = ConvertUtil.ToString(DataAccess.Instance("BizDB").ExecuteScalar("select value from COM_USERSETTINGS where name='" + userkey + "' and SETTINGTYPE='SessionID'"));
                if (!string.IsNullOrEmpty(sessionid))
                {
                    _task.LogoutUser(sessionid);
                }

                txtType.Text = Request.QueryString["Type"];
                txtSort.Text = Request.QueryString["Sort"];
                txtPreSort.Text = Request.QueryString["Sort"];
                txtDateType.Text = Request.QueryString["DateType"];
                txtProcessCategory.Text = Request.QueryString["ProcessCategory"];
                if (txtProcessCategory.Text == Lang.Get("TaskList_All"))
                {
                    txtProcessCategory.Text = "";
                }
                txtProcessName.Text = Request.QueryString["ProcessName"];
                txtIncident.Text = Request.QueryString["Incident"];
                txtStartDate.Text = Request.QueryString["StartDate"];
                txtEndDate.Text = Request.QueryString["EndDate"];
                txtApplicant.Text = Request.QueryString["Applicant"];
                txtSummary.Text = Request.QueryString["Summary"];
                txtShowQuery.Text = Request.QueryString["ShowQuery"];
                txtStepName.Text = Request.QueryString["Stepname"];
                BindGrid();
                BindProcessCategory();

                lblProcessName.Text = Lang.Get("TaskList_ProcessName");
                lblStartTime.Text = Lang.Get("TaskList_StartTime");
                lblIncident.Text = Lang.Get("TaskList_Incident");
                lblApplicant.Text = Lang.Get("TaskList_Applicant");
                lblSummary.Text = Lang.Get("TaskList_Summary");
                lbQstep.Text = Lang.Get("TaskList_StepName");
                btnSearch.Text = Lang.Get("TaskList_Search");
                btnAssign.Text = Lang.Get("TaskList_Assign");
                btnAssignCallback.Text = Lang.Get("TaskList_AssignCallback");
                btnAbort.Text = Lang.Get("TaskList_Abort");
                btnCallback.Text = Lang.Get("TaskList_Callback");
                AspNetPager1.FirstPageText = Lang.Get("FirstPage");
                AspNetPager1.PrevPageText = Lang.Get("PrevPage");
                AspNetPager1.NextPageText = Lang.Get("NextPage");
                AspNetPager1.LastPageText = Lang.Get("LastPage");
                //if(txtType.Text.ToUpper==)
            }
        }

        void BindProcessCategory()
        {
            List<ProcessCategoryEntity> lists = _category.GetCategoryList();
            if (!lists.Exists(p => p.CATEGORYNAME == Lang.Get("NewTask_AllProcess")))
            {
                ProcessCategoryEntity pe = new ProcessCategoryEntity();
                pe.CATEGORYNAME = Lang.Get("NewTask_AllProcess");
                lists.Insert(0, pe);
            }
            rptProcessCategory.DataSource = lists;
            rptProcessCategory.DataBind();
        }

        void BindGrid()
        {
            List<ParameterEntity> table = new List<ParameterEntity>();
            string filter = GetFilter(out table);
            int skipResults = (AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize;
            int maxResults = AspNetPager1.PageSize;

            List<TaskEntity> lists = new List<TaskEntity>();
            string sort = "a.STARTTIME DESC";
            if (!string.IsNullOrEmpty(txtSort.Text))
            {
                sort = txtSort.Text;
            }
            if (txtType.Text.ToUpper().Trim() == "MYREQUEST")
            {
                lists = _task.GetMyRequestList(SessionLogic.GetLoginName(), filter, table, sort, skipResults, maxResults);
                filter = GetFilter(out table);
                AspNetPager1.RecordCount = _task.GetMyRequestCount(SessionLogic.GetLoginName(), filter, table);
                //tInitior.Visible = false;
                lblApplicant.Visible = false;
                txtApplicant.Visible = false;
                //CurrentUser.Visible = true;
                btnCallback.Visible = true;
            }
            else if (txtType.Text.ToUpper().Trim() == "MYAPPROVAL")
            {
                lists = _task.GetMyApprovalList(SessionLogic.GetLoginName(),filter, table, sort, skipResults, maxResults);
                filter = GetFilter(out table);
                AspNetPager1.RecordCount = _task.GetMyApprovalCount(SessionLogic.GetLoginName(), filter, table);
                //thsteplabel.Visible = true;
            }
            else
            {
                if (string.IsNullOrEmpty(txtSort.Text))
                {
                    sort = "a.STARTTIME";
                }
                lists = _task.GetMyTaskList(SessionLogic.GetLoginName(), filter, table, sort, skipResults, maxResults);
                filter = GetFilter(out table);
                AspNetPager1.RecordCount = _task.GetMyTaskCount(SessionLogic.GetLoginName(), filter, table);
                btnAssign.Visible = true;
                btnAssignCallback.Visible = true;
                btnAbort.Visible = true;
            }
            COUNT = AspNetPager1.RecordCount.ToString();
            rptTask.DataSource = lists;
            rptTask.DataBind();
        }

        protected void lbProcessCategory_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            string id = linkButton.CommandArgument;
            if (id == Lang.Get("TaskList_All"))
            {
                txtProcessCategory.Text = "";
            }
            else
            {
                txtProcessCategory.Text = id;
            }
            BindGrid();
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindGrid();

        }

        string GetFilter(out List<ParameterEntity> table)
        {
            SqlFilterUtil fb = new SqlFilterUtil(false);
            fb.AddLike("a.PROCESSNAME", txtProcessName.Text);
            if (!string.IsNullOrEmpty(txtProcessCategory.Text) && txtProcessCategory.Text!=Lang.Get("TaskList_AllProcess"))
            {
                List<ProcessEntity> processes = _category.GetProcessList(txtProcessCategory.Text);
                List<string> strs = new List<string>();
                foreach (ProcessEntity process in processes)
                {
                    strs.Add(process.PROCESSNAME);
                }
                fb.AddIn("a.PROCESSNAME", strs.ToArray());
            }
            fb.AddLike("b.APLICANT", txtApplicant.Text);
            //fb.AddEqual("ASSIGNEDTOUSER", Portal.Logic.SSOLogic.GetLoginName().Replace("\\","/"));
            fb.AddEqual("a.INCIDENT", txtIncident.Text);
            //fb.AddScope("a.STARTTIME", txtStartDate.Text, txtEndDate.Text);
            fb.AddLike("SUMMARY", txtSummary.Text);
            fb.AddLike("a.STEPLABEL", txtStepName.Text);
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now.AddDays(1);
            switch (txtDateType.Text)
            {
                case "1":
                    startTime = DateTime.Now.AddDays(-7);
                    endTime = DateTime.Now.AddDays(1);
                    break;
                case "2":
                    startTime = DateTime.Now.AddMonths(-1);
                    endTime = DateTime.Now.AddDays(1);
                    break;
                case "3":
                    startTime = DateTime.Now.AddMonths(-3);
                    endTime = DateTime.Now.AddDays(1);
                    break;
                default:
                    if (!string.IsNullOrEmpty(txtStartDate.Text) && string.IsNullOrEmpty(txtEndDate.Text))
                    {
                        startTime = ConvertUtil.ToDateTime(txtStartDate.Text);
                        endTime = DateTime.Now.AddDays(1);
                    }
                    if (string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
                    {
                        startTime = DateTime.Now.AddMonths(-120);
                        endTime = ConvertUtil.ToDateTime(txtEndDate.Text + " 23:59:59");
                    }
                    if (string.IsNullOrEmpty(txtStartDate.Text) && string.IsNullOrEmpty(txtEndDate.Text))
                    {
                        startTime = DateTime.Now.AddMonths(-120);
                        endTime = DateTime.Now.AddDays(1);
                    }
                    if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
                    {
                        startTime = ConvertUtil.ToDateTime(txtStartDate.Text);
                        endTime = ConvertUtil.ToDateTime(txtEndDate.Text + " 23:59:59");
                    }
                    break;
            }
            table = new List<ParameterEntity>();
            table.Add(new ParameterEntity( "STARTTIME", startTime.ToString()));
            table.Add(new ParameterEntity("ENDTIME", endTime.ToString()));
            //table.Add(new ParameterEntity("ASSIGNEDTOUSER", "'" + SessionLogic.GetLoginName().Replace("\\", "/") + "'"));

            return fb.ToString();
        }
         

        public string GetProcessCategoryCurCss(string processCategory)
        {
            if (processCategory.Trim().ToUpper() == txtProcessCategory.Text.Trim().ToUpper())
            {
                return "cur";
            }
            if (processCategory.Trim().ToUpper() == Lang.Get("TaskList_All").ToUpper() && txtProcessCategory.Text == "")
            {
                return "cur";
            }
            return "";
        }

        public string GetCount(string processCategory)
        {

            if (processCategory.Trim().ToUpper() == Lang.Get("NewTask_AllProcess").ToUpper() )
            {
                return "<img id=\"sortbtn1Icon\" class=\"msgNum\" src=\"images/msgnum.png\" /><font id=\"sortbtn1Num\" class=\"num\">"+COUNT+"</font>";
            }
            return "";
        }

        public string GetDateTypeCurCss(string dateType)
        {
            if (dateType == txtDateType.Text.Trim().ToUpper())
            {
                return "cur";
            }
            if (dateType == "0" && txtDateType.Text == "")
            {
                return "cur";
            }
            return "";
        }

        public string GetSortCurCss(string sort)
        {
            if (txtSort.Text.Trim().ToUpper().IndexOf(sort.Trim().ToUpper()) >= 0)
            {
                return "cur";
            }
            if (sort == "0" && txtSort.Text == "")
            {
                return "cur";
            }
            return "";
        }

        public string GetProcessCategoryUrl(string cat)
        {
            string url = "TaskList.aspx?type={0}&processcategory={1}&datetype={2}&sort={3}&processname={4}&incident={5}&startdate={6}&enddate={7}&applicant={8}&summary={9}&showquery={10}&stepname={11}";
            url = string.Format(url, txtType.Text, Server.UrlEncode(cat), txtDateType.Text, txtSort.Text, Server.UrlEncode(txtProcessName.Text),
                txtIncident.Text, txtStartDate.Text, txtEndDate.Text, Server.UrlEncode(txtApplicant.Text), Server.UrlEncode(txtSummary.Text), txtShowQuery.Text, txtStepName.Text);
            return url;
        }

        public string GetDateTypeUrl(string dateType)
        {
            string url = "TaskList.aspx?type={0}&processcategory={1}&datetype={2}&sort={3}&processname={4}&incident={5}&startdate={6}&enddate={7}&applicant={8}&summary={9}&showquery={10}&stepname={11}";
            url = string.Format(url, txtType.Text, Server.UrlEncode(txtProcessCategory.Text), dateType, txtSort.Text, Server.UrlEncode(txtProcessName.Text),
                txtIncident.Text, txtStartDate.Text, txtEndDate.Text, Server.UrlEncode(txtApplicant.Text), Server.UrlEncode(txtSummary.Text), txtShowQuery.Text, txtStepName.Text);
            return url;
        }

        public string GetSortUrl(string sort)
        {
            if (txtPreSort.Text == sort)
            {
                if (sort.IndexOf("DESC") >= 0)
                {
                    sort = sort.Replace("DESC", "");
                }
                else
                {
                    sort = sort + " desc";
                }
            }
            sort = Server.UrlEncode(sort);
            string url = "TaskList.aspx?type={0}&processcategory={1}&datetype={2}&sort={3}&processname={4}&incident={5}&startdate={6}&enddate={7}&applicant={8}&summary={9}&showquery={10}";
            url = string.Format(url, txtType.Text, Server.UrlEncode(txtProcessCategory.Text), txtDateType.Text, sort, Server.UrlEncode(txtProcessName.Text),
                txtIncident.Text, txtStartDate.Text, txtEndDate.Text, Server.UrlEncode(txtApplicant.Text), Server.UrlEncode(txtSummary.Text), txtShowQuery.Text);
            return url;
        }

        public string GetStatus(string status)
        {
            //if (status == "1")
            //{
            //    return Workflow.Resources.lang.TaskStatus_Active;
            //}
            //else if (status == "3")
            //{
            //    return Workflow.Resources.lang.TaskStatus_Completed;
            //}
            //else if (status == "4" || status == "7")
            //{
            //    return Workflow.Resources.lang.TaskStatus_Return;
            //}
            //else if (status == "13")
            //{
            //    return Workflow.Resources.lang.TaskStatus_Queue;
            //}
            //else if (status == "19")
            //{
            //    return Workflow.Resources.lang.TaskStatus_Failure;
            //}
            //else
            //{
            //    return Workflow.Resources.lang.TaskStatus_Unknown;
            //}

            if (status == "1")
            {
                return Lang.Get("TaskStatus_Active");
            }
            else if (status == "2")
            {
                return Lang.Get("TaskStatus_Completed");
            }
            else if (status == "4")
            {
                return Lang.Get("TaskStatus_Abort");
            }
            else if (status == "8")
            {
                return Lang.Get("TaskStatus_Suspend");
            }
            else if (status == "33")
            {
                return Lang.Get("TaskStatus_Stalled");
            }
            else
            {
                return Lang.Get("TaskStatus_Unknown");
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            txtShowQuery.Text = "1";
            string url = "TaskList.aspx?type={0}&processcategory={1}&datetype={2}&sort={3}&processname={4}&incident={5}&startdate={6}&enddate={7}&applicant={8}&summary={9}&showquery={10}&stepname={11}";
            url = string.Format(url, txtType.Text, Server.UrlEncode(txtProcessCategory.Text), txtDateType.Text, txtSort.Text, Server.UrlEncode(txtProcessName.Text),
                txtIncident.Text, txtStartDate.Text, txtEndDate.Text, Server.UrlEncode(txtApplicant.Text), Server.UrlEncode(txtSummary.Text), txtShowQuery.Text, txtStepName.Text);
            Response.Redirect(url);
        }
        public string GetCurrentUser(string strProcName, string incident)
        {
            //int iInc = ConvertUtil.ToInt32(incident);
            //ITask task = ServiceContainer.Instance().GetService<ITask>();
            //return task.GetCurrentUser(strProcName.Trim(), iInc);
            return "";
        }
        protected void btnAbort_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem ri in this.rptTask.Items)
            {
                if (((CheckBox)ri.FindControl("cbSelect")).Checked)
                {
                    string strTaskid = ((HiddenField)ri.FindControl("hfTaskid")).Value;
                    if (strTaskid.Trim() != "")
                    {
                        //Ultimus.WFServer.Task tsk = new Ultimus.WFServer.Task();
                        //tsk.InitializeFromTaskId(
                        ITask task = ServiceContainer.Instance().GetService<ITask>();
                        TaskEntity ety=  task.GetTaskEntity(strTaskid);
                        if (ety.SUBSTATUS != 4)
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pushnotify", "<script>alert('只有退回的流程才能作废！'); </script>");
                            return;
                        }
                        task.AbortIncident(ety);             
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pushnotify", "<script>alert('" + Lang.Get("TaskList_CancelSuccess") + "');window.location.href = window.location.href;</script>");
                        
                    }
                }
            }
        }

        protected void btnCallback_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem ri in this.rptTask.Items)
            {
                if (((CheckBox)ri.FindControl("cbSelect")).Checked)
                {
                    string strTaskid = ((HiddenField)ri.FindControl("hfTaskid")).Value;
                    if (strTaskid.Trim() != "")
                    {
                        DataTable dt=DataAccess.Instance("UltDB").ExecuteDataTable("select ProcessName,Incident, EndTime from TASKS where TASKID='"+strTaskid+"'");
                        try
                        {
                            if (dt.Rows.Count > 0)
                            {
                                DateTime endTime = ConvertUtil.ToDateTime(dt.Rows[0][2]);
                                string processName = ConvertUtil.ToString(dt.Rows[0][0]);
                                int incident = ConvertUtil.ToInt32(dt.Rows[0][1]);
                                DataAccess.Instance("UltDB").ExecuteNonQuery("update tasks set status=1 where taskid='" + strTaskid + "' ;delete from tasks where processname='"+processName+"' and incident="+incident+" and taskid<>'"+strTaskid+"' and endtime>='"+endTime+"'");
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pushnotify", "<script>alert('撤回成功!');window.location.href = window.location.href;</script>");
                            }
                        }
                        catch
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pushnotify", "<script>alert('撤回失败!');window.location.href = window.location.href;</script>");
                        }
                    }
                }
            }
        }

    }
    
}