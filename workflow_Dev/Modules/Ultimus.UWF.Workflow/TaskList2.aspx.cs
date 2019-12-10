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
using Ultimus.UWF.Common.Interface;
using Ultimus.UWF.OrgChart.Interface;
using Ultimus.UWF.OrgChart.Entity;
using Ultimus.UWF.Workflow.Logic;

namespace Ultimus.UWF.Workflow
{
    public partial class TaskList2 : System.Web.UI.Page
    {
        IProcessCategory _category = ServiceContainer.Instance().GetService<IProcessCategory>();
        ITask _task = ServiceContainer.Instance().GetService<ITask>();
        IOrg _org = ServiceContainer.Instance().GetService<IOrg>();
        public string COUNT = "0";
        public string CanNotCancel = Lang.Get("TaskList_CanNotCancel");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userkey = SessionLogic.GetLoginName().Replace("\\","/");
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
                //btnAssign.Text = ;// Lang.Get("TaskList_Assign");
                //btnAssignCallback.Text = Lang.Get("TaskList_AssignCallback");
                //btnAbort.Text = Lang.Get("TaskList_Abort");
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
                //取最近4周的数据
                txtStartDate.Text = DateTime.Now.AddDays(-30).ToShortDateString();
                txtEndDate.Text = DateTime.Now.AddDays(1).ToShortDateString();

                filter = GetFilter(out table);
                lists = _task.GetMyRequestList(SessionLogic.GetLoginName(), filter, table, sort, skipResults, maxResults);
                AspNetPager1.RecordCount = _task.GetMyRequestCount(SessionLogic.GetLoginName(), filter, table);
                //tInitior.Visible = false;
                lblApplicant.Visible = false;
                txtApplicant.Visible = false;
                //CurrentUser.Visible = true;
                btnAlert.Visible = true;
                ltTitle.Text = "我的已申请";
            }
            else if (txtType.Text.ToUpper().Trim() == "MYAPPROVAL")
            {
                //取最近4周的数据
                txtStartDate.Text = DateTime.Now.AddDays(-30).ToShortDateString();
                txtEndDate.Text = DateTime.Now.AddDays(1).ToShortDateString();

                filter = GetFilter(out table);
                lists = _task.GetMyApprovalList(SessionLogic.GetLoginName(), filter, table, sort, skipResults, maxResults);
                AspNetPager1.RecordCount = _task.GetMyApprovalCount(SessionLogic.GetLoginName(), filter, table);
                //thsteplabel.Visible = true;
                ltTitle.Text = "我的已办箱";
            }
            else
            {
                if (string.IsNullOrEmpty(txtSort.Text))
                {
                    sort = "a.STARTTIME";
                }
                filter = GetFilter(out table);
                lists = _task.GetMyTaskList(SessionLogic.GetLoginName(), filter, table, sort, skipResults, maxResults);
                AspNetPager1.RecordCount = _task.GetMyTaskCount(SessionLogic.GetLoginName(), filter, table);
                btnAssign.Visible = true;
                //reorder1.Visible = true;
                btnAssignCallback.Visible = true;
                //reorder2.Visible = true;
                btnAbort.Visible = true;
                //reorder3.Visible = true;
                ltTitle.Text = "待办任务箱";
            }
            COUNT = AspNetPager1.RecordCount.ToString();
            lblCount.Text = COUNT;
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
            if (!string.IsNullOrEmpty(txtProcessCategory.Text) && txtProcessCategory.Text != Lang.Get("TaskList_AllProcess"))
            {
                List<ProcessEntity> processes = _category.GetProcessList(txtProcessCategory.Text);
                List<string> strs = new List<string>();
                foreach (ProcessEntity process in processes)
                {
                    strs.Add(process.PROCESSNAME);
                }
                fb.AddIn("a.PROCESSNAME", strs.ToArray());
            }
            if (!string.IsNullOrEmpty(txtApplicant.Text))
            {
                List<UserEntity> user = _org.GetUserInfoBySearchText(txtApplicant.Text);
                if (user.Count > 0)
                {
                    fb.AddLike("b.Initiator", user[0].LOGINNAME.Replace("\\", "/"));
                }
                else
                {
                    fb.AddLike("b.Initiator", txtApplicant.Text);
                }
            }
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
            table.Add(new ParameterEntity("STARTTIME", startTime.ToString()));
            table.Add(new ParameterEntity("ENDTIME", endTime.ToString()));
            //table.Add(new ParameterEntity("ASSIGNEDTOUSER", "'" + SessionLogic.GetLoginName().Replace("\\", "/") + "'"));

            return fb.ToString();
        }

        public string GetName(object obj, object servename)
        {
            string account = ConvertUtil.ToString(obj);
            if (servename.Equals("UltimusV7"))
            {
                account= account.Replace("Ultimus/","quanyou.com.cn/").Trim();
            }
            UserEntity user= _org.GetUserEntity(account);
            if (user != null)
            {
                return user.USERNAME;
            }
            return "";
        }

        public string GetCurrentUser(object obj,object p,object i,object servename)
        {
            try
            {
                string step = ConvertUtil.ToString(obj);
                if (txtType.Text.ToUpper().Trim() == "MYTASK" || string.IsNullOrEmpty(txtType.Text))
                {
                    return step;
                }
                string ss = ConvertUtil.ToString(servename);
                ServerEntity ser = new ServerEntity();
                ser.SERVERNAME = ss;
                _task.SetServerEntity(ser);
                string curr = _task.GetCurrentApprover(ConvertUtil.ToString(p), ConvertUtil.ToInt32(i));
                curr = ConvertUtil.ToString(curr);
                string[] sz = curr.Split(',');
                string str = "";
                if (sz.Length == 1 && string.IsNullOrEmpty(sz[0]))
                {
                    return "";
                }
                foreach (string s in sz)
                {
                    string[] aa = s.Split(':');
                    if (aa[1].Trim().ToUpper() == "SYSTEMUSER")
                    {
                        str += s + "<br>";
                    }
                    else
                    {
                        string account = aa[1];
                        if (servename.Equals("UltimusV7"))
                        {
                            account = account.Replace("Ultimus/", "quanyou.com.cn/").Trim();
                        }
                        UserEntity user = _org.GetUserEntity(account);
                        if (user != null)
                        {
                            str += aa[0] + ":" + user.USERNAME + "<br>";
                        }
                    }
                }

                return str.TrimEnd(',');
            }
            catch
            {
                return "";
            }
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

            if (processCategory.Trim().ToUpper() == Lang.Get("NewTask_AllProcess").ToUpper())
            {
                return "<img id=\"sortbtn1Icon\" class=\"msgNum\" src=\"images/msgnum.png\" /><font id=\"sortbtn1Num\" class=\"num\">" + COUNT + "</font>";
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
            string url = "TaskList2.aspx?type={0}&processcategory={1}&datetype={2}&sort={3}&processname={4}&incident={5}&startdate={6}&enddate={7}&applicant={8}&summary={9}&showquery={10}&stepname={11}";
            url = string.Format(url, txtType.Text, Server.UrlEncode(cat), txtDateType.Text, txtSort.Text, Server.UrlEncode(txtProcessName.Text),
                txtIncident.Text, txtStartDate.Text, txtEndDate.Text, Server.UrlEncode(txtApplicant.Text), Server.UrlEncode(txtSummary.Text), txtShowQuery.Text, txtStepName.Text);
            return url;
        }

        public string GetDateTypeUrl(string dateType)
        {
            string url = "TaskList2.aspx?type={0}&processcategory={1}&datetype={2}&sort={3}&processname={4}&incident={5}&startdate={6}&enddate={7}&applicant={8}&summary={9}&showquery={10}&stepname={11}";
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
            string url = "TaskList2.aspx?type={0}&processcategory={1}&datetype={2}&sort={3}&processname={4}&incident={5}&startdate={6}&enddate={7}&applicant={8}&summary={9}&showquery={10}";
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
            string url = "TaskList2.aspx?type={0}&processcategory={1}&datetype={2}&sort={3}&processname={4}&incident={5}&startdate={6}&enddate={7}&applicant={8}&summary={9}&showquery={10}&stepname={11}";
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
                        TaskEntity ety = task.GetTaskEntity(strTaskid);
                        int steptype = task.GetStepType(ety.TASKID, ety.STEPID);
                        if (steptype==2 )
                        {
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pushnotify", "<script>alert('只有退回的流程才能终止！'); </script>");
                            return;
                        }
                        task.AbortIncident(ety);
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pushnotify", "<script>alert('终止成功！');window.location.href = window.location.href;</script>");

                    }
                }
            }
        }

        protected void btnAlert_Click(object sender, EventArgs e)
        {
            bool flag = false;
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
                        TaskEntity ety = task.GetTaskEntity(strTaskid);
                        //if (ety.SUBSTATUS != 4)
                        //{
                        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pushnotify", "<script>alert('只有退回的流程才能作废！'); </script>");
                        //    return;
                        //}
                        //task.AbortIncident(ety);
                        if (ety.STATUS == 1)
                        {
                            string applicant = "";
                            string createdate = "";
                            string body = string.Format("BPM系统提醒您:【{0}】于【{1}】发起的【{2}/{3},{4}】任务，请您及时办理]"
                           , applicant, createdate, ety.PROCESSNAME, ety.INCIDENT, ety.SUMMARY);
                            IMessage _msg = ServiceContainer.Instance().GetService<IMessage>();
                            MessageEntity msg = new MessageEntity();
                            msg.Body = body;
                            msg.To = ety.ASSIGNEDTOUSER.Replace("quanyou.com.cn/", "");
                            msg.Source = "BPM";
                            msg.SendType = "RTX,SMS";
                            _msg.Send(msg);
                            LogUtil.Info("发送催办提醒:" + strTaskid);
                            flag = true;
                            //短信催办 BPM系统提醒您:【赵亚龙】于【2015/7/2 17:50:18】发起的【内部工作联系单流程/123,NGLD-201507127898】任务，请您及时办理
                        }
                    }
                }
            }
            if (flag)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pushnotify", "<script>alert('发送成功!');window.location.href = window.location.href;</script>");
            }
            //TaskEntity task = new TaskEntity();
            //task = _task.GetTaskEntity(strTaskId);

            //string body = string.Format("您好!【{0}-{1}】申请单已完成审批！[查看审批记录|http://bpm.quanyou.com.cn:8080/Moduels/Ultimus.UWF.Workflow/openform.aspx?TaskID={2}]"
            //    , strProcessName, task.SUMMARY, strTaskId);
            //MessageEntity msg = new MessageEntity();
            //msg.Body = body;
            //msg.To = task.ASSIGNEDTOUSER.Replace("quanyou.com.cn/", "");
            //msg.Source = "BPM";
            //msg.SendType = "RTX,SMS";
            //_msg.Send(msg);
            //LogUtil.Info("发送完成提醒:" + strTaskId);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string url = "TaskList2.aspx?type={0}&processcategory={1}&datetype={2}&sort={3}&processname={4}&incident={5}&startdate={6}&enddate={7}&applicant={8}&summary={9}&showquery={10}&stepname={11}";
            url = string.Format(url, txtType.Text, Server.UrlEncode(txtProcessCategory.Text), txtDateType.Text, txtSort.Text, Server.UrlEncode(txtProcessName.Text),
                txtIncident.Text, txtStartDate.Text, txtEndDate.Text, Server.UrlEncode(txtApplicant.Text), Server.UrlEncode(txtSummary.Text), txtShowQuery.Text, txtStepName.Text);
            Response.Redirect(url);
        }

    }

}