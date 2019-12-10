using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Ultimus.UWF.Workflow.Interface;
using MyLib;
using Ultimus.UWF.Common.Logic;
using Ultimus.UWF.Workflow.Entity;
using Ultimus.UWF.OrgChart.Entity;
using Ultimus.UWF.OrgChart.Interface;

namespace Ultimus.UWF.Workflow
{
    public class TaskStatus : System.Web.UI.Page
    {
        int iCount = 0;
        ITask _task = ServiceContainer.Instance().GetService<ITask>();
        public string HIDDEN = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            int incident = ConvertUtil.ToInt32(Request.QueryString["Incident"]);
            string processName = Server.UrlDecode(Request.QueryString["ProcessName"].Trim());
            if (incident <= 0)
            {
                HIDDEN = "hide";
                btnClose.Visible = false;
            }
            try
            {
                btnClose.Text = Lang.Get("TaskStatus_Close");
                //Incident.Status pstatus = new Incident.Status();
                //Incident pincident = new Incident();
                //pincident.LoadIncident(processName, int.Parse(incident));
                //pincident.GetIncidentStatus(out pstatus);
                List<TaskEntity> list = _task.GetTaskList(" and processname='"+processName+"' and incident="+incident,new List<Common.Entity.ParameterEntity>(),"StartTime",0,999);
                byte[] bytesGif;
                //pstatus.GetGraphicalStatus(pincident.strProcessName, pincident.nIncidentNo, pincident.nVersion, out bytesGif);
                bytesGif = _task.GetGraphicalStatus(processName, incident);
                Session["flowpic"] = bytesGif;


                DataTable pGetData = this.Blank();
                //获取流程步骤状态信息
                //if (pstatus.TaskStatuses == null)
                //{
                //    throw new Exception("frm_TaskStatus_notification");
                //}

                //Incident.Status.StepStatus[] pStepStatus = null;
                //try
                //{
                //    pStepStatus = pstatus.TaskStatuses;
                //}
                //catch
                //{
                //    throw new Exception("frm_TaskStatus_notification1");
                //}

                foreach (TaskEntity pStep in list)
                //foreach (Incident.Status.StepStatus pStep in pStepStatus)
                {
                    // 5:数据库机器人 2:发起步骤 4:用户步骤 6:子流程
                    int pStepType = _task.GetStepType(pStep.TASKID, pStep.STEPID);
                    if (pStepType != 2 && pStepType != 4 && pStepType != 6)
                    {
                        //if (pStep.STEPLABEL.Trim().ToUpper() != "COMPLETE")
                            continue;
                    }

                    DataRow pRow = pGetData.NewRow();
                    pRow["StepName"] = pStep.STEPLABEL;

                    string pStepUser = pStep.ASSIGNEDTOUSER;
                    if (pStepUser == null)
                    {
                        pStepUser = pStep.TASKUSER;
                        if (pStepUser == null)
                        {
                            pStepUser = "";
                            continue;

                        }
                    }
                    if (pStepUser.IndexOf("Ultimus/") != -1) { pStepUser = pStepUser.Replace("Ultimus/", "quanyou.com.cn/"); }

                    //获取fullname
                    string pFullName = pStepUser;
                    try
                    {
                        //Ultimus.OC.OrgChart oc = new Ultimus.OC.OrgChart();
                        //Ultimus.OC.User user = new Ultimus.OC.User();
                        ////UserEntity user = getUserEntity(pStep.ASSIGNEDTOUSER.Replace("/", "\\"));
                        //oc.FindUser(pStepUser, "", "", out user);
                        //pFullName = user.strUserFullName;
                        //if (string.IsNullOrEmpty(pFullName))
                        //{
                        //    pFullName = pStep.ASSIGNEDTOUSER;
                        //}
                        IOrg org = ServiceContainer.Instance().GetService<IOrg>();
                        UserEntity ety= org.GetUserEntity(pStepUser);
                        if (ety != null)
                        {
                            pFullName = ety.USERNAME;
                            if (pFullName == null)
                            {
                                pFullName = "";
                            }
                        }
                    }
                    catch
                    {
                    }

                    //去掉域名
                    int pIndex = pStepUser.Trim().IndexOf("/");
                    if (pIndex > 0)
                        pStepUser = pStepUser.Trim().Substring(pStepUser.Trim().IndexOf("/") + 1);



                    //去掉域名
                    int pIndex1 = pFullName.Trim().IndexOf("/");
                    if (pIndex1 > 0)
                        pFullName = pFullName.Trim().Substring(pFullName.Trim().IndexOf("/") + 1);

                    pRow["StepUser"] = pFullName + "(" + pStepUser + ")";

                    if (pStep.STATUS.ToString() == "13")
                    {
                        pRow["StartTime"] = "-";
                    }
                    else
                    {
                        pRow["StartTime"] = pStep.STARTTIME;
                    }
                    if (pStep.STATUS.ToString() == "13" || pStep.STATUS.ToString() == "1")
                    {
                        pRow["EndTime"] = "-";
                    }
                    else
                    {
                        pRow["EndTime"] = pStep.ENDTIME;
                    }

                    if (pStep.STATUS.ToString() == "1")
                    {
                        pRow["Status"] = Lang.Get("TaskStatus_Active");
                    }
                    else if (pStep.STATUS.ToString() == "3")
                    {
                        pRow["Status"] = Lang.Get("TaskStatus_Completed");
                    }
                    else if (pStep.STATUS.ToString() == "4" || pStep.STATUS.ToString() == "7")
                    {
                        pRow["Status"] = Lang.Get("TaskStatus_Return");
                    }
                    else if (pStep.STATUS.ToString() == "13")
                    {
                        pRow["Status"] = Lang.Get("TaskStatus_Queue");
                    }
                    else if (pStep.STATUS.ToString() == "19")
                    {
                        pRow["Status"] = Lang.Get("TaskStatus_Failure");
                    }
                    else
                    {
                        pRow["Status"] = Lang.Get("TaskStatus_Unknown");
                    }

                    pGetData.Rows.Add(pRow);
                }
                //绑定数据源
                if (pGetData.Rows.Count > 0)
                {
                    this.DataBind(pGetData);
                }
                else
                {
                    //this.btn_GraphicWF.Disabled=true;
                    this.DataBindDefaultData(pGetData);
                }
            }
            catch
            {
                //throw new Exception(ee.Message);
                //				msg.Value=ee.Message;
                //if (iCount < 5)
                //{
                //    iCount++;
                //    Thread.Sleep(1000);
                //    Response.Redirect("TaskStatus.aspx?ProcessName=" + processName + "&Incident=" + incident);
                //}
            }
        }

        UserEntity getUserEntity(string loginName)
        {
            IOrg org = ServiceContainer.Instance().GetService<IOrg>();
            return org.GetUserEntity(loginName);
            //DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable("SELECT * FROM V_EMPLOYEE WHERE SHORTNAME = '" + loginName.Replace("\\", "/") + "' ");
            //UserEntity user = new UserEntity();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    user.USERNAME = ConvertUtil.ToString(dr["FIRSTNAME"]);
            //    user.LOGINNAME = ConvertUtil.ToString(dr["SHORTNAME"]);
            //    user.JOBFUNCTION = ConvertUtil.ToString(dr["TITLE"]);
            //    user.USERID = ConvertUtil.ToInt32(dr["EMPLOYEEID"]);
            //    user.DIRECTREPORTID = ConvertUtil.ToInt32(dr["SUPERVISORID"]);
            //    user.EXT02 = ConvertUtil.ToString(dr["DEPARTMENTID"]);
            //}
            //return user;
        }

        /// <summary>
        /// 返回步骤拥有人的全名
        /// </summary>
        /// <param name="pTaskID"></param>
        /// <param name="pStepUser"></param>
        /// <returns></returns>
        //private string ProcessStepUser(string pTaskID, string pStepUser)
        //{
        //    string pReturn = pStepUser;

        //    Ultimus.WFServer.Task pTask = new Ultimus.WFServer.Task();
        //    bool pIsOK = pTask.InitializeFromTaskId(pTaskID);
        //    if (pIsOK)
        //    {
        //        if (pStepUser.Trim().IndexOf("_WF") != -1)
        //        {
        //            pReturn = pTask.strGroupName;
        //        }
        //        else
        //        {
        //            pReturn = pTask.strRecipientFullName;
        //        }
        //    }

        //    return pReturn;
        //}

        /// <summary>
        /// 得到缺省的数据
        /// </summary>
        /// <returns></returns>
        private DataTable Blank()
        {
            DataTable pGetData = new DataTable();

            DataColumn pCol4 = new DataColumn("StepName");
            pGetData.Columns.Add(pCol4);

            DataColumn pCol5 = new DataColumn("StepUser");
            pGetData.Columns.Add(pCol5);

            DataColumn pCol6 = new DataColumn("StartTime");
            pGetData.Columns.Add(pCol6);

            DataColumn pCol7 = new DataColumn("EndTime");
            pGetData.Columns.Add(pCol7);

            DataColumn pCol8 = new DataColumn("Status");
            pGetData.Columns.Add(pCol8);

            return pGetData;
        }


        //目的  设置网格行数
        //参数  myDataTable－数据表；intPageCount－每页行数；intCol－加空行的列号
        public static void SetTableRows(ref DataTable myDataTable, int intPageCount)
        {
            int intTemp = myDataTable.Rows.Count % intPageCount;
            if ((myDataTable.Rows.Count == 0) || (intTemp != 0))
            {
                for (int i = 0; i < (intPageCount - intTemp); i++)
                {
                    DataRow myDataRow = myDataTable.NewRow();
                    myDataTable.Rows.Add(myDataRow);
                }
            }
        }

        /// <summary>
        /// 绑定缺省的空数据
        /// </summary>
        /// <param name="pBindData"></param>
        private void DataBindDefaultData(DataTable pDataTable)
        {

            this.rptTaskList.DataSource = pDataTable.DefaultView;
            this.rptTaskList.DataBind();
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        /// <param name="pDataTable"></param>
        public void DataBind(DataTable pDataTable)
        {
            DataView dv = pDataTable.DefaultView;
            dv.Sort = "StartTime,Status";
            this.rptTaskList.DataSource = pDataTable.DefaultView;
            this.rptTaskList.DataBind();

        }



        protected global::System.Web.UI.WebControls.Repeater rptTaskList;
        protected global::System.Web.UI.WebControls.Button btnClose;

    }
}