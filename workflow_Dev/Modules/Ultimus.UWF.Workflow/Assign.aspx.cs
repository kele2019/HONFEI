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
using Ultimus.UWF.Workflow.Interface;
using Ultimus.UWF.Workflow.Entity;
using Ultimus.UWF.Workflow.Logic;

namespace Ultimus.UWF.Workflow
{
    public partial class Assign : System.Web.UI.Page
    {
        private StringBuilder StrSQL;
        public string EnableProcessAssign = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Button1.Text = Lang.Get("Assign_AssignButton");
            if (Request.QueryString["TaskID"] != null)
            {
                TaskIDs.Value = Request.QueryString["TaskID"].ToString().Trim();
            }
            if (!IsPostBack)
            {
                BingProcess();
            }

            //if (ConfigurationManager.AppSettings["EnableProcessAssign"] == "0")
            //{
            //    EnableProcessAssign = "hidden";
            //}
        }

        private void BingProcess()
        {
            StrSQL = new StringBuilder();
            StrSQL.AppendLine("select PROCESSNAME from INITIATE");
            dropProcessName.DataSource = DataAccess.Instance("UltDB").ExecuteDataTable(StrSQL.ToString());
            dropProcessName.DataTextField = "PROCESSNAME";
            dropProcessName.DataValueField = "PROCESSNAME";
            dropProcessName.DataBind();
            dropProcessName.Items.Insert(0, new ListItem("", ""));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            //OrgChart.Entity.UserEntity CurrentUser = new OrgChart.Entity.UserEntity();
            //if (HttpContext.Current.Session["CurrentUser"] != null)
            //{
            //    CurrentUser = HttpContext.Current.Session["CurrentUser"] as OrgChart.Entity.UserEntity;
            //}
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
            if (this.RadioButton1.Checked)
            {
                pMode = 1;//仅限选定的任务
            }
            else if (this.RadioButton2.Checked)
            {
                pMode = 2;//所有现有的任务
            }
            else if (this.RadioButton3.Checked)
            {
                pMode = 3;//所有将来的任务  
                pEndDate = this.txtFutureTaskDate.Text;
            }
            else if (this.RadioButton4.Checked)
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
                    result = this.SetAssign(pTaskID, pFromUser, pToUser, pMode, DateTime.Parse(pEndDate));
                }
            }
            else if (pMode == 3)
            {
                string strProcessName = "";
                //string dBegin = pEndDate;
                //string dEnd = "2099-01-01";
                string dBegin = DateTime.Now.ToShortDateString();
                string dEnd = pEndDate;// +" 23:59:59";
                if (this.isExistAssign(pFromUser, dBegin))
                {
                    MessageBox("alert('" + Lang.Get("Assign_Msg1") + "')");
                    return;
                }
                result = this.SetProcAssign(strProcessName, pFromUser, pToUser, dBegin, dEnd);
            }
            else if (pMode == 4)
            {
                string strProcessName = dropProcessName.SelectedItem.Text;
                string dBegin = this.txtBegin.Text;
                string dEnd = this.txtEnd.Text;// +" 23:59:59";
                if (this.isExistAssign(strProcessName, pFromUser, dBegin))
                {
                    MessageBox("alert('" + Lang.Get("Assign_Msg1") + "')");
                    return;
                }
                result = this.SetProcAssign(strProcessName, pFromUser, pToUser, dBegin, dEnd);
            }
            else
            {
                //将来指派和所有任务指派不需要循环
                result = this.SetAssign("", pFromUser, pToUser, pMode, DateTime.Parse(pEndDate));
            }
            if (result)
            {
                MessageBox("alert('" + Lang.Get("Assign_Msg2") + "');window.close();");
            }
            else
            {
                MessageBox("alert('" + Lang.Get("Assign_Msg3") + "')");

            }
        }

        private void MessageBox(string script)
        {
            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "javascript", "<script language='javascript' defer>" + script + "</script>");
        }

        public bool isExistAssign(string pFromUser, string dBegin)
        {
            //20150306 东 用表[UltimusServer].[dbo].[ASSIGNMENT]
            string strSql = " select COUNT(*) from ASSIGNMENT " +
                                " where TASKUSER ='" + pFromUser + "' " +
                                " and ASSIGNFROM>'" + dBegin + "' and (PROCESSNAME='' or PROCESSNAME is null) ";
            DataTable dt = DataAccess.Instance("UltDB").ExecuteDataTable(strSql);
            if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                return true;
            return false;
        }

        /// <summary>
        /// 是否存在该流程的指派
        /// </summary>
        /// <param name="strProcessName"></param>
        /// <param name="pFromUser"></param>
        /// <param name="dBegin"></param>
        /// <returns></returns>
        public bool isExistAssign(string strProcessName, string pFromUser, string dBegin)
        {
            // throw new NotImplementedException();
            //string strSql = " select COUNT(id) from WF_ASSIGNMENT " +
            //                    " where IsEnable = 1 " +
            //                    " and ProcessName='" + strProcessName + "' " +
            //                    " and TskUser ='" + pFromUser + "'" +
            //                    " and EdDate>'" + dBegin + "' ";
            //DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(strSql);
            //if (Convert.ToInt32(dt.Rows[0][0]) > 0)
            //    return true;
            //return false;

            //20150306 东 用表[UltimusServer].[dbo].[ASSIGNMENT]
            //string strSql = " select COUNT(*) from ASSIGNMENT " +
            //                    " where PROCESSNAME='" + strProcessName + "' " +
            //                    " and TASKUSER ='" + pFromUser + "' ";
            string strSql = "delete from  ASSIGNMENT where TASKUSER='" + pFromUser + "'  and PROCESSNAME='" + strProcessName + "'";
            int i = DataAccess.Instance("UltDB").ExecuteNonQuery(strSql);
            if (i> 0)
                return true;
            return false;
        }

        public bool SetProcAssign(string strProcessName, string pFromUser, string pToUser, string dBegin, string dEnd)
        {
            //throw new NotImplementedException();
            //string strSql = "INSERT INTO WF_ASSIGNMENT " +
            //                                       "(ID " +
            //                                       ",ProcessName " +
            //                                      " ,TskUser " +
            //                                      " ,TskAssignUser" +
            //                                      " ,BgDate" +
            //                                      " ,EdDate" +
            //                                      " ,AssignCount" +
            //                                      " ,IsEnable)" +
            //                                      " VALUES ('" +
            //                                      Guid.NewGuid().ToString() +
            //                                      "' ,'" + strProcessName.Trim() +
            //                                      "' ,'" + pFromUser +
            //                                      "' , '" + pToUser +
            //                                      "' ,'" + dBegin +
            //                                      "','" + dEnd +
            //                                       "',0,1)";
            //int count = DataAccess.Instance("BizDB").ExecuteNonQuery(strSql);
            //if (count > 0)
            //    return true;
            //else
            //    return false;
            //20150306 东 用表[UltimusServer].[dbo].[ASSIGNMENT]
              string strSql ="";
            if(string.IsNullOrEmpty(strProcessName))
            {
             strSql= @"INSERT INTO [ASSIGNMENT]
                ([TASKUSER]
              
                ,[ASSIGNEDTOUSER]
                ,[ASSIGNUNTIL]
                ,[ASSIGNFROM]) VALUES ('" + pFromUser +
                "' , '" + pToUser +
                "' ,'" + dEnd +
                "','" + dBegin +
                "')";
            }
            else
            {
          strSql= @"INSERT INTO [ASSIGNMENT]
                ([TASKUSER]
                ,[PROCESSNAME]
                ,[ASSIGNEDTOUSER]
                ,[ASSIGNUNTIL]
                ,[ASSIGNFROM]) VALUES ('" + pFromUser +
                "' ,'" + strProcessName.Trim() +
                "' , '" + pToUser +
                "' ,'" + dEnd +
                "','" + dBegin +
                "')";
            }
            
            if (string.IsNullOrEmpty(strProcessName))
            {

               // _task.AssignAllFutureTasks(pFromUser, pToUser, ConvertUtil.ToDateTime(dEnd));
               // return true;
            }
            int count = DataAccess.Instance("UltDB").ExecuteNonQuery(strSql);
            if (count > 0)
            {
            if (string.IsNullOrEmpty(strProcessName))
                strProcessName = "All Process";

                CreateAssignDataBackup(pFromUser, pToUser, dBegin, dEnd, strProcessName);
                return true;
          
          }
            else
                return false;
        }



        ITask _task = ServiceContainer.Instance().GetService<ITask>();
        public bool SetAssign(string pTaskID, string pFromUser, string pToUser, int pMode, DateTime pDate)
        {
            bool result = false;
            try
            {
                //Ultimus.OC.User pUserTask = null;
                //Ultimus.OC.OrgChart porg = new Ultimus.OC.OrgChart();
                //result = porg.FindUser(pFromUser, "", 0, out pUserTask);
                switch (pMode)
                {
                    case 1:
                        //Ultimus.WFServer.Task pTask = new Ultimus.WFServer.Task();
                        //pTask.InitializeFromTaskId(pTaskID);
                        //result = pTask.AssignTask(pToUser);
                        //TaskEntity te = _task.GetTaskEntity(pTaskID);
                        //string v7Domain = ConfigurationManager.AppSettings["V7Domain"].ToString();
                        //if (te.SERVERNAME.IndexOf("V7") != 1) 
                        //{
                        //    pToUser = pToUser.Replace("\\", "/");
                        //    pToUser = v7Domain + "/" + pToUser.Split('/')[1].ToString();
                        //}
                        //pToUser = ServerLogic.GetLoginName("UltimusV8", pToUser);
                        result = _task.AssignTask(pTaskID, pToUser);
                        if (result)
                        {
                             object Summary=DataAccess.Instance("UltDB").ExecuteScalar("select SUMMARY from ( select PROCESSNAME,INCIDENT from TASKS where TASKID='"+pTaskID+"') A left join INCIDENTS B on A.PROCESSNAME=B.PROCESSNAME and A.INCIDENT=B.INCIDENT");
                            if(Summary!=null)
                            CreateAssignDataBackup(pFromUser, pToUser, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString(), Summary.ToString());
                        }
                        break;
                    case 2:
                        result = _task.AssignAllCurrentTasks(pFromUser, pToUser);
                        break;
                    case 3:
                        //pUserTask.AssignAllFutureTasks(pToUser, pDate.ToOADate());
                        result = _task.AssignAllFutureTasks(pFromUser, pToUser, pDate);
                        if (result)
                            CreateAssignDataBackup(pFromUser, pToUser, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString(), "All Process");
                        break;
                }
                return result;

            }
            catch
            {
                return false;
            }
        }

        public void CreateAssignDataBackup(string pFromUser, string pToUser, string dBegin, string dEnd,string Comments)
        {
            if (string.IsNullOrEmpty(Comments))
                Comments = "All Process";
            string Strsql = "insert into COM_ASSGININFO(TaskUser,AssginTaskUser,AssginStartDate,AssginEndDate,Comments,CreateDate)values(";
            Strsql += "'" + pFromUser + "',";
            Strsql += "'" + pToUser + "',";
            Strsql += "'" + dBegin + "',";
            Strsql += "'" + dEnd + "',";
            Strsql += "'" + Comments + "',";
            Strsql += "'"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"'";
            Strsql += ")";
            DataAccess.Instance("BizDB").ExecuteNonQuery(Strsql);
        }

    }
}