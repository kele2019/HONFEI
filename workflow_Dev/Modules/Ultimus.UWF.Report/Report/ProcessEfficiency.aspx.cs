using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace BPM.ReportDesign.Report
{
    public partial class ProcessEfficiency : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string view = Request["view"];
                string processName="";
                string incident = "";

                //从其他页面点击查看后显示页面将查询条件进行隐藏
                if (!String.IsNullOrEmpty(view) && view == "1")
                {
                    divCondition.Visible = false;
                    processName = Request["ProcessName"];
                    incident = Request["incident"];
                    loadGrid(processName, incident);
                }
                
            }
        }

        private void loadGrid(string processName, string incident) {

            clsOleDB clsdb = new clsOleDB();
            string strSql = "select [PROCESSNAME],[INCIDENT],[STEPLABEL],[TASKUSER],[ASSIGNEDTOUSER],[STATUS],[STARTTIME],[ENDTIME] from tasks with(nolock) where PROCESSNAME like '%" + processName + "%' and incident=" + incident + " order by endtime";
            clsdb.setConn("bpmDB");
            DataTable dt = clsdb.getDtRtn(strSql);
            
            rpSource.DataSource = dt;
            rpSource.DataBind();
        }
        /// <summary>
        /// 获取任务处理人姓名
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        public string getTaskUserName(string userAccount) {
            string returnValue = "";
            if (!String.IsNullOrEmpty(userAccount)) {
                clsOleDB clsdb = new clsOleDB();
                clsdb.setConn("W2DB");
                userAccount = userAccount.Substring(userAccount.IndexOf('/')+1);
                string strSql = "select Name from dbo.HROCPeople where loginname ='"+userAccount+"'";
                returnValue = clsdb.getAString(strSql);
            }

            return returnValue;
        }
        /// <summary>
        /// 转化任务状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public string getTaskStatus(string status) {
            string strRtn = "";
            switch (status) { 
                case "3":
                    strRtn = "已处理";
                    break;
                case "1":
                    strRtn = "处理中...";
                    break;
                default :
                    strRtn = "异常";
                    break;
            }
            return strRtn;
        }

        /// <summary>
        /// 获取两时间差 格式 HH:mm
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public string getCostTime(string startTime, string endTime,string status) {
            string strRtn = "";
            if (status != "1")
            {
                TimeSpan ts = DateTime.Parse(endTime) - DateTime.Parse(startTime);

                int hours = 0;
                if (ts.Days > 0)
                {
                    hours = ts.Days * 24;
                }
                hours += ts.Hours;
                int min = ts.Minutes;
                if (ts.Seconds <= 60 && ts.Seconds > 30)
                {
                    min += 1;
                }
                strRtn = toDouble(hours.ToString()) + " : " + toDouble(min.ToString());
            }
            return strRtn;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
           string   processName = txtProcessName.Value;
           string  incident = txtIncident.Value;
           loadGrid(processName,incident);
        }

        public string toDouble(string str)
        {
            if (str.Length == 1)
            {
                return '0' + str;
            }
            else
            {
                return str;
            }
        }

    }
}