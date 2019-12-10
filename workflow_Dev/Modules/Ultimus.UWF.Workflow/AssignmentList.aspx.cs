using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Ultimus.UWF.Common.Logic;
using MyLib;
using System.Data;
using System.Web.UI.HtmlControls;

namespace Ultimus.UWF.Workflow
{
    public partial class AssignmentList : System.Web.UI.Page
    {
        private StringBuilder StrSQL;
        public string EnableProcessAssign = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Button1.Text = Lang.Get("btn_Search");
            Button3.Text = Lang.Get("btn_Reset");
            Button4.Text = Lang.Get("btn_RecallTask");
            Button5.Text = Lang.Get("Assign_BackButton");
            if (!IsPostBack)
            {
                BingProcess();
                BingAaaignList();
            }

            if (ConfigurationManager.AppSettings["EnableProcessAssign"] == "0")
            {
                EnableProcessAssign = "hidden";
            }
        }

        private void BingProcess()
        {
            StrSQL = new StringBuilder();
            StrSQL.AppendLine("select PROCESSNAME from INITIATE order by PROCESSNAME");
           DataTable dtProcessName=DataAccess.Instance("UltDB").ExecuteDataTable(StrSQL.ToString());
           dropProcessName.DataSource = dtProcessName;
            dropProcessName.DataTextField = "PROCESSNAME";
            dropProcessName.DataValueField = "PROCESSNAME";
            dropProcessName.DataBind();
            dropProcessName.Items.Insert(0, new ListItem("--Pls Select--", ""));


            dropProcessName1.DataSource = dtProcessName;
            dropProcessName1.DataTextField = "PROCESSNAME";
            dropProcessName1.DataValueField = "PROCESSNAME";
            dropProcessName1.DataBind();
            dropProcessName1.Items.Insert(0, new ListItem("--Pls Select--", ""));

            string StrsqlProcess = "select DISPLAYNAME,CATEGORYID from WF_PROCESSCATEGORY";
            DataTable dtDept = DataAccess.Instance("BizDB").ExecuteDataTable(StrsqlProcess.ToString());
            //cbListProcess.DataSource = dtDept;
            //cbListProcess.DataTextField = "DISPLAYNAME";
            //cbListProcess.DataValueField = "CATEGORYID";
            //cbListProcess.DataBind();
            StringBuilder StrHtml = new StringBuilder();
            StrHtml.Append("<TABLE style=\"WIDTH: 100%\"  class=\"cb\" border=\"0\" cellSpacing=\"5\" cellPadding=\"5\"><TBODY><TR>");


            for (int item = 0; item < dtDept.Rows.Count; item++)
            {
                StrHtml.Append("<TD><INPUT name=" + dtDept.Rows[item]["CATEGORYID"].ToString().Trim() + " onclick=\"ChangeDepartmentProcess('" + dtDept.Rows[item]["CATEGORYID"].ToString().Trim() + "')\"  type=\"checkbox\" ><LABEL>" + dtDept.Rows[item]["DISPLAYNAME"].ToString() + "</LABEL></TD>");
               // cbListProcess.Items[item].Attributes.Add("onclick", "alert('"+cbListProcess.Items[item].Value.Trim()+"')");
            }
            StrHtml.Append("</TR></TBODY></TABLE>");
            divDept.InnerHtml = StrHtml.ToString();
        }

        private void BingAaaignList()
        {
            try
            {
                this.GetProcessInfo();
                DataTable dtProcess;
                dtProcess = DataAccess.Instance("UltDB").ExecuteDataTable(StrSQL.ToString());
                listbox1.DataTextField = "PROCESSNAME";
                listbox1.DataValueField = "PROCESSNAME";
                listbox1.DataSource = dtProcess;
                listbox1.DataBind();


                this.GetTaskInfo();
                DataTable dtCTask;
                dtCTask = DataAccess.Instance("UltDB").ExecuteDataTable(StrSQL.ToString());
                RPCTask.DataSource = dtCTask;
                RPCTask.DataBind();

                this.GetSelectSQL();
                DataTable dt;
                if (RadioButton1.Checked)
                {
                    task.Visible = true;
                    FutureTasks.Visible = false;
                    Processes.Visible = false;
                    dt = DataAccess.Instance("UltDB").ExecuteDataTable(StrSQL.ToString());
                    TaskList.DataSource = dt;
                    TaskList.DataBind();
                }
                else if (RadioButton3.Checked)
                {
                    task.Visible = false;
                    FutureTasks.Visible = true;
                    Processes.Visible = false;
                    dt = DataAccess.Instance("UltDB").ExecuteDataTable(StrSQL.ToString());
                    FutureTasksList.DataSource = dt;
                    FutureTasksList.DataBind();
                }
                else if (RadioButton4.Checked)
                {
                    task.Visible = false;
                    FutureTasks.Visible = false;
                    Processes.Visible = true;
                    dt = DataAccess.Instance("UltDB").ExecuteDataTable(StrSQL.ToString());
                    //dt = DataAccess.Instance("BizDB").ExecuteDataTable(StrSQL.ToString());
                    ProcessesList.DataSource = dt;
                    ProcessesList.DataBind();
                }
                else
                {
                    task.Visible = false;
                    FutureTasks.Visible = false;
                    Processes.Visible = false;
                    MessageBox("alert('" + Lang.Get("AssignmentList_NotTaskMsg") + "');");
                }
            }
            catch (Exception ex)
            {
                MessageBox("alert('" + ex.Message + "');");
            }
        }

        private void MessageBox(string script)
        {
            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "javascript", "<script language='javascript' defer>" + script + "</script>");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BingAaaignList();
        }

        private void GetSelectSQL()
        {
            StrSQL = new StringBuilder();

            string UserAccount = SessionLogic.GetLoginName().Replace("\\", "/");
            string strWhere = "";
            if (dropProcessName.SelectedValue.Trim() != "")
            {
                strWhere += " and PROCESSNAME = '" + dropProcessName.SelectedValue.Trim() + "' ";
            }
            if (txtAssignUserAccount.Value.Trim() != "")
            {
                strWhere += " and ASSIGNEDTOUSER = '" + txtAssignUserAccount.Value.Trim() + "' ";
            }
            if (RadioButton1.Checked)//单个任务
            {
                StrSQL.Append(@"select A.*,B.SUMMARY from (
SELECT TASKID,PROCESSNAME,INCIDENT,STEPLABEL,ASSIGNEDTOUSER FROM TASKS WHERE STATUS=1
 and TASKUSER='" + UserAccount + "' and TASKUSER!=ASSIGNEDTOUSER) A left join  INCIDENTS B ON A.PROCESSNAME=B.PROCESSNAME AND A.INCIDENT=B.INCIDENT");
               // StrSQL.AppendLine("SELECT TASKID,PROCESSNAME,INCIDENT,STEPLABEL,ASSIGNEDTOUSER FROM TASKS WHERE STATUS=1 and TASKUSER='" + UserAccount + "' and TASKUSER!=ASSIGNEDTOUSER" + strWhere);
            }
            else if (RadioButton3.Checked)//按照时间指派或所有任务指派
            {
                StrSQL.AppendLine("SELECT PROCESSNAME,STEPLABEL,ASSIGNEDTOUSER,ASSIGNUNTIL FROM ASSIGNMENT WHERE ASSIGNUNTIL is not null and ASSIGNUNTIL>=CONVERT(nvarchar(50),GETDATE(),112)   and TASKUSER='" + UserAccount + "' " + strWhere);
            }
            else if (RadioButton4.Checked)//按照流程和时间指派
            {
                StrSQL.AppendLine("SELECT ID,ProcessName, TskAssignUser,BgDate,EdDate FROM WF_ASSIGNMENT t inner join v_employee a on a.ShortName = t.tskassignuser where IsEnable=1 AND  '" + DateTime.Now.ToString() + "' between t.bgdate and t.eddate and t.tskuser ='" + UserAccount + "'" + strWhere);
            }
        }

        private void GetTaskInfo()
        {
            StrSQL = new StringBuilder();

            string UserAccount = SessionLogic.GetLoginName().Replace("\\", "/");
            StrSQL.Append("select A.*,B.SUMMARY from (select  TASKID, PROCESSNAME,STEPLABEL,INCIDENT from TASKS  where STATUS=1 and ASSIGNEDTOUSER='" + UserAccount + "' and TASKUSER=ASSIGNEDTOUSER) A left join  INCIDENTS B on A.PROCESSNAME=B.PROCESSNAME and A.INCIDENT=B.INCIDENT");
        }
        private void GetProcessInfo()
        {
            StrSQL = new StringBuilder();
            StrSQL.Append("select distinct PROCESSNAME from  PROCESSES");
        }

        private void RecallTasks(string strWhere)
        {
            try
            {
                GetRecallTasksSQL(strWhere);
                int count = 0;
                if (RadioButton4.Checked)
                {
                    count = DataAccess.Instance("BizDB").ExecuteNonQuery(StrSQL.ToString());
                }
                else
                {
                    count = DataAccess.Instance("UltDB").ExecuteNonQuery(StrSQL.ToString());
                }
                if (count > 0)
                {
                    BingAaaignList();
                }
                MessageBox("alert('Recall Success！');");
            }
            catch (Exception ex)
            {
                MessageBox("alert('" + ex.Message + "');");
            }
        }

        private void GetRecallTasksSQL(string strWhere)
        {
            StrSQL = new StringBuilder();
            if (RadioButton1.Checked)//单个任务
            {
                StrSQL.AppendLine("UPDATE TASKS SET ASSIGNEDTOUSER=TASKUSER WHERE STATUS=1" + strWhere);
            }
            else if (RadioButton3.Checked || RadioButton4.Checked)//按照时间指派或所有任务指派
            {
                StrSQL.AppendLine("DELETE ASSIGNMENT WHERE 1=1 " + strWhere);
                //  StrSQL.AppendLine("UPDATE TASKS SET ASSIGNEDTOUSER=TASKUSER WHERE STATUS=1" 
            }
            //else if (RadioButton4.Checked)//按照流程和时间指派
            //{
            //    StrSQL.AppendLine("update WF_ASSIGNMENT set IsEnable=0 WHERE 1=1" + strWhere);
            //}
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                string strWhere = "";
                if (RadioButton1.Checked)//单个任务
                {
                    strWhere += " and TaskID in (";
                    foreach (RepeaterItem item in TaskList.Items)
                    {
                        HtmlInputCheckBox cb = item.FindControl("Task_checkbox") as HtmlInputCheckBox;
                        if (cb.Checked)
                        {
                            strWhere += "'" + cb.Value + "',";
                        }
                    }
                    strWhere = strWhere.Substring(0, strWhere.LastIndexOf(','));
                    strWhere += ")";
                }
                else if (RadioButton3.Checked)//按照时间指派或所有任务指派
                {
                    //string ProcessNames = " and isnull(PROCESSNAME,' ') in (";
                    //string StepNames = " and isnull(STEPLABEL,' ') in (";
                    //string AssignedToUsers = " and ASSIGNEDTOUSER in (";
                    //string DueTimes = " and ASSIGNUNTIL in (";
                  
                    foreach (RepeaterItem item in FutureTasksList.Items)
                    {
                        HtmlInputCheckBox cb = item.FindControl("FutureTasksList_checkbox") as HtmlInputCheckBox;
                        if (cb.Checked)
                        {
                            if (string.IsNullOrEmpty(strWhere))
                                strWhere += " and (";
                            else
                                strWhere += " or (";
                            string ProcessNames = "";
                            string ProcessNameSelected = (item.FindControl("FutureTasksList_ProcessName") as Label).Text.TrimEnd();
                            if (string.IsNullOrEmpty(ProcessNameSelected))
                                ProcessNames = " PROCESSNAME is null "; 
                            else
                                ProcessNames = "  rtrim(PROCESSNAME) in ('" + ProcessNameSelected + "',";
                            string StepNames = " and  rtrim(STEPLABEL) in (";
                            string AssignedToUsers = " and ASSIGNEDTOUSER in (";
                            string DueTimes = " and ASSIGNUNTIL in (";


                            //ProcessNames += "'" + (item.FindControl("FutureTasksList_ProcessName") as Label).Text.TrimEnd() + "',";
                            StepNames += "'" + (item.FindControl("FutureTasksList_StepName") as Label).Text.Trim() + "',";
                            AssignedToUsers += "'" + (item.FindControl("FutureTasksList_AssignedToUser") as Label).Text.Trim() + "',";
                            //DueTimes += "('" + Convert.ToDateTime((item.FindControl("FutureTasksList_Assignuntil") as Label).Text.Trim()).ToShortDateString() + "','yyyy-MM-dd'),";
                            DueTimes += "'" + Convert.ToDateTime((item.FindControl("FutureTasksList_Assignuntil") as Label).Text.Trim()).ToShortDateString() + "',";


                            if (!string.IsNullOrEmpty(ProcessNameSelected))
                            {
                                ProcessNames = ProcessNames.Substring(0, ProcessNames.LastIndexOf(','));
                                ProcessNames += ")";
                            }
                            StepNames = StepNames.Substring(0, StepNames.LastIndexOf(','));
                            StepNames += ")";
                            AssignedToUsers = AssignedToUsers.Substring(0, AssignedToUsers.LastIndexOf(','));
                            AssignedToUsers += ")";
                            DueTimes = DueTimes.Substring(0, DueTimes.LastIndexOf(','));
                            DueTimes += ")";

                            strWhere += ProcessNames + AssignedToUsers + " " + DueTimes + " and taskuser = '" + SessionLogic.GetLoginName().Replace("\\", "/") + "') ";
                        }
                    }
                   
                   // strWhere += ProcessNames + " " + StepNames + " " + AssignedToUsers + " " + DueTimes + " and taskuser = '" + SessionLogic.GetLoginName().Replace("\\", "/") + "' ";
                }
                else if (RadioButton4.Checked)//按照流程和时间指派
                {
                    //strWhere += " and ID in (";
                    //foreach (RepeaterItem item in ProcessesList.Items)
                    //{
                    //    HtmlInputCheckBox cb = item.FindControl("Processes_checkbox") as HtmlInputCheckBox;
                    //    if (cb.Checked)
                    //    {
                    //        strWhere += "'" + cb.Value + "',";
                    //    }
                    //}
                    //strWhere = strWhere.Substring(0, strWhere.LastIndexOf(','));
                    //strWhere += ")";

                    string ProcessNames = " and isnull(PROCESSNAME,' ') in (";
                    //string StepNames = " and isnull(STEPLABEL,' ') in (";
                    //string AssignedToUsers = " and ASSIGNEDTOUSER in (";
                    //string DueTimes = " and ASSIGNUNTIL in ";
                    foreach (RepeaterItem item in ProcessesList.Items)
                    {
                        HtmlInputCheckBox cb = item.FindControl("Processes_checkbox") as HtmlInputCheckBox;
                        if (cb.Checked)
                        {
                            ProcessNames += "'" + (item.FindControl("ProcessTasksList_ProcessName") as Label).Text.Trim() + "',";
                            //StepNames += "' " + (item.FindControl("FutureTasksList_StepName") as Label).Text.Trim() + "',";
                            //AssignedToUsers += "'" + (item.FindControl("FutureTasksList_AssignedToUser") as Label).Text.Trim() + "',";
                            //DueTimes += "('" + Convert.ToDateTime((item.FindControl("FutureTasksList_Assignuntil") as Label).Text.Trim()).ToShortDateString() + "','yyyy-MM-dd'),";
                        }
                    }
                    ProcessNames = ProcessNames.Substring(0, ProcessNames.LastIndexOf(','));
                    ProcessNames += ")";
                    //StepNames = StepNames.Substring(0, StepNames.LastIndexOf(','));
                    //StepNames += ")";
                    //AssignedToUsers = AssignedToUsers.Substring(0, AssignedToUsers.LastIndexOf(','));
                    //AssignedToUsers += ")";
                    //DueTimes = DueTimes.Substring(0, DueTimes.LastIndexOf(','));
                    //DueTimes += "";
                    strWhere += ProcessNames + " " + " and taskuser = '" + SessionLogic.GetLoginName().Replace("\\", "/") + "' ";// +AssignedToUsers + " ";// +DueTimes;
                }
                RecallTasks(strWhere);
            }
            catch (Exception ex)
            {
                MessageBox("alert('" + ex.Message + "');");
            }
        }




        protected void Button2_Click(object sender, EventArgs e)
        {
            Assign assign = new Assign();
            string pFromUser = SessionLogic.GetLoginName().Replace("\\", "/");
            string pToUser = AssignUserAccount.Value.Replace("|USER", "");
            // pToUser = org.GetUserEntityByID(ConvertUtil.ToInt32( pToUser)).LOGINNAME.Replace("\\", "/");
            try
            {
                pToUser = ConvertUtil.ToString(DataAccess.Instance("BizDB").ExecuteDataTable("Select loginname as shortname from V_ORG_USER where userid=" + pToUser).Rows[0][0]);
            }
            catch
            {
            }
            string pEndDate = txtFutureTaskDate.Text.Trim() == "" ? DateTime.Now.ToString() : txtFutureTaskDate.Text;

            int pMode = 0;
            if (this.RadioButton2.Checked)
            {
                pMode = 1;//仅限选定的任务
            }
            else if (this.RadioButton5.Checked)
            {
                pMode = 1;//所有现有的任务
            }
            else if (this.RadioButton6.Checked)
            {
                pMode = 3;//所有将来的任务  
                pEndDate = this.txtFutureTaskDateEnd.Text;
            }
            else if (this.RadioButton7.Checked)
            {
                pMode = 4;//按流程指派
            }
            bool result = false;
            if (pMode == 1)
            {
                string[] TaskIDArray = TaskIDs.Value.Split(',');
              
                foreach (string pTaskID in TaskIDArray)
                {
                    if (pTaskID.Trim() == "")
                        continue;
                    result = assign.SetAssign(pTaskID, pFromUser, pToUser, pMode, DateTime.Parse(pEndDate));
                }
            }
            else if (pMode == 3)
            {
                string strProcessName = "";
                //string dBegin = pEndDate;
                //string dEnd = "2099-01-01";
                string dBegin = this.txtFutureTaskDate.Text;// DateTime.Now.ToShortDateString();
                string dEnd = pEndDate;// +" 23:59:59";
                if (assign.isExistAssign(pFromUser, dBegin))
                {
                    MessageBox("alert('" + Lang.Get("Assign_Msg1") + "')");
                    return;
                }
                result = assign.SetProcAssign(strProcessName, pFromUser, pToUser, dBegin, dEnd);
            }
            else if (pMode == 4)
            {
                string strProcessName = hidProcessName.Value.TrimEnd(',');// dropProcessName1.SelectedItem.Text;
                for (int i = 0; i < strProcessName.Split(',').Length; i++)
                {
                    string ProcessName = strProcessName.Split(',')[i].ToString();
                    string dBegin = this.txtBegin.Text;
                    string dEnd = Convert.ToDateTime(txtEnd.Text).AddDays(1).ToString("yyyy-MM-dd");// +" 23:59:59";
                    if (assign.isExistAssign(ProcessName, pFromUser, dBegin))
                    {
                        MessageBox("alert('" + Lang.Get("Assign_Msg1") + "')");
                        return;
                    }
                    result = assign.SetProcAssign(ProcessName, pFromUser, pToUser, dBegin, dEnd);
                }
            }
            else
            {
                //将来指派和所有任务指派不需要循环
                result = assign.SetAssign("", pFromUser, pToUser, pMode, DateTime.Parse(pEndDate));
            }
            if (result)
            {
                MessageBox("alert('" + Lang.Get("Assign_Msg2") + "');");
                RadioButton5.Checked = false;
                RadioButton6.Checked = false;
                RadioButton7.Checked = false;

                BingAaaignList();
            }
            else
            {
                MessageBox("alert('" + Lang.Get("Assign_Msg3") + "')");

            }
        }




    }
}