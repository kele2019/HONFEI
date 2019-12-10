using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace BPM.ReportDesign.Report.AjaxPage
{
    public partial class getList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string processName = Request["ProcessName"];
                string incident = Request["incident"];
                string stepName = Request["stepName"];
                string str = getTasks(processName,stepName,incident);
                Response.Write(str);
            }
        }
        private string getTasks(string processName,string stepName,string incno)
        {
            string strRtn = "";
            clsOleDB clsdb = new clsOleDB();
            clsdb.setConn("bpmDB");
            string strSql = "select PROCESSNAME,INCIDENT,DATEDIFF(SS,STARTTIME,ENDTIME) as times ,STEPLABEL,DATEDIFF(SS,STARTTIME,OVERDUETIME) as  overtime from TASKS where PROCESSNAME+convert(nvarchar(50),INCIDENT) in (select PROCESSNAME+convert(nvarchar(50),INCIDENT) from INCIDENTS where STATUS=2 and PROCESSNAME like '%" + processName + "%') ";
            if (!String.IsNullOrEmpty(stepName))
            {
                strSql += " and STEPLABEL ='"+stepName+"'";
            }
            
            if (!String.IsNullOrEmpty(incno))
            {
                strSql += " and and INCIDENT="+incno;
            }
            strSql += " order by INCIDENT";
            DataTable dt = new DataTable();
            dt = clsdb.getDtRtn(strSql);
            strRtn = "{list:[";
            string incident="";
            string substeps="";
            string listitem = "";
            int listcount = 0;
            for (int i = 0; i < dt.Rows.Count; i++) {
                string str1 = dt.Rows[i]["INCIDENT"].ToString();
                if (i == 0)
                {
                    incident = str1;
                }
                if (i == dt.Rows.Count - 1) {
                    substeps += "{stepName:'" + dt.Rows[i]["STEPLABEL"] + "',stepOverTime:'" + dt.Rows[i]["overtime"].ToString() + "',stepTime:'" + dt.Rows[i]["times"].ToString() + "'},";
                }
                if (incident != str1 || i== dt.Rows.Count-1) {
                    listitem = "{processName:'" + dt.Rows[i]["PROCESSNAME"].ToString ().Trim() + "',incident:" + incident + ",steps:[";
                    incident = str1;
                    substeps = substeps.TrimEnd(','); //获取到 steps:[{},{},{}]
                    listitem += substeps+"]},";
                    strRtn += listitem;
                    listitem = "";
                    substeps = "";
                    listcount++;
                }
                substeps += "{stepName:'" + dt.Rows[i]["STEPLABEL"].ToString().Trim() + "',stepOverTime:'" + getTime(dt.Rows[i]["overtime"].ToString()) + "',stepTime:'" + getTime(dt.Rows[i]["times"].ToString()) + "'},";
            }
            strRtn = strRtn.TrimEnd(',');
            strRtn += "],listcount:"+listcount.ToString ()+"}";
           return strRtn;
        }

        //private string getTasks(string processName, string incident) {
        //    string strRtn = "";
        //    clsOleDB clsdb = new clsOleDB();
        //    clsdb.setConn("bpmDB");
        //    string strSql = "select steplabel from tasks where status=3 and  ProcessName like '%" + processName + "%' and incident =" + incident;
            
            
        //    return strRtn;
        //}

        //private string getTasks(string processName,  string stepName,string incident) {
        //    string strRtn = "";
        //    clsOleDB clsdb = new clsOleDB();
        //    clsdb.setConn("bpmDB");
        //    string strSql = "";
        //    return strRtn;
        //}


        /// <summary>
        /// 将秒数转化为 格式  天 小时 分钟
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        public string getTime(string second)
        {
            string strRtn = "";
            if (!String.IsNullOrEmpty(second))
            {
                int sec = int.Parse(second);
                if (sec < 0)
                {
                    strRtn = "0";
                }
                else
                {
                    int day = sec / (60 * 60 * 24);
                    int hours = (sec % (60 * 60 * 24)) / (60 * 60);
                    int mins =((sec % (60 * 60 * 24)) % (60 * 60)) / 60+1;
                    
                    strRtn = day.ToString() + "天" + hours.ToString() + "小时" + mins.ToString() + "分钟";
                }
            }
            else
            {
                strRtn = "无";
            }
            return strRtn;
        }
    }
}