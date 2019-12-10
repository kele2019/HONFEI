using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace BPM.ReportDesign.Report.AjaxPage
{
    /// <summary>
    /// getlist 的摘要说明
    /// </summary>
    public class getlist : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string processName = context.Request["ProcessName"];
            string incident = context.Request["incident"];
            string stepName = context.Request["stepName"];
            string index = context.Request["index"];
            string str = getTasks(processName, stepName, incident,index);
            context.Response.Write(str);
        }

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
                    int mins = ((sec % (60 * 60 * 24)) % (60 * 60)) / 60 + 1;

                    strRtn = day.ToString() + "天" + hours.ToString() + "小时" + mins.ToString() + "分钟";
                }
            }
            else
            {
                strRtn = "无";
            }
            return strRtn;
        }

        private string getTasks(string processName, string stepName, string incno, string index)
        {
            string strRtn = "";
            clsOleDB clsdb = new clsOleDB();
            clsdb.setConn("bpmDB");
            //string strSql = "select PROCESSNAME,INCIDENT,DATEDIFF(SS,STARTTIME,ENDTIME) as times ,STEPLABEL,DATEDIFF(SS,STARTTIME,OVERDUETIME) as  overtime from TASKS where PROCESSNAME+convert(nvarchar(50),INCIDENT) in (select PROCESSNAME+convert(nvarchar(50),INCIDENT) from INCIDENTS where STATUS=2  and PROCESSNAME like '%" + processName + "%')  and STEPLABEL <> '完成' ";
            //string strSql = "select PROCESSNAME, INCIDENT,DATEDIFF(SS,STARTTIME,ENDTIME) as times ,STEPLABEL,DATEDIFF(SS,STARTTIME,OVERDUETIME) as  overtime from TASKS where PROCESSNAME in ( select PROCESSNAME from  (select ROW_NUMBER() over(order by incident ) as rownum ,* from INCIDENTS where PROCESSNAME like '%" + processName + "%' and STATUS=2 ) as t where t.rownum >=" + ((int.Parse(index) - 1) * 10 + 1) + " and t.rownum<=" + (int.Parse(index) * 10) + ") and INCIDENT in ( select INCIDENT from  (select ROW_NUMBER() over(order by incident ) as rownum ,* from INCIDENTS where PROCESSNAME like '%" + processName + "%' and STATUS=2 ) as t where t.rownum >=" + ((int.Parse(index) - 1) * 10 + 1) + " and t.rownum<=" + (int.Parse(index) * 10) + ")  and STEPLABEL <> '完成'  ";
            string strSql = "select t.PROCESSNAME, t.INCIDENT,DATEDIFF(SS,t.STARTTIME,t.ENDTIME) as times,DATEDIFF(SS,i.STARTTIME,i.ENDTIME) as ptimes ,STEPLABEL,DATEDIFF(SS,t.STARTTIME,OVERDUETIME) as  overtime from TASKS t left join dbo.INCIDENTS i on t.INCIDENT=i.INCIDENT and t.PROCESSNAME=i.PROCESSNAME where t.PROCESSNAME in ( select PROCESSNAME from  (select ROW_NUMBER() over(order by incident ) as rownum ,* from INCIDENTS where PROCESSNAME like '%" + processName + "%' and STATUS=2 ) as t where t.rownum >=" + ((int.Parse(index) - 1) * 10 + 1) + " and t.rownum<=" + (int.Parse(index) * 10) + ") and t.INCIDENT in ( select INCIDENT from  (select ROW_NUMBER() over(order by incident ) as rownum ,* from INCIDENTS where PROCESSNAME like '%" + processName + "%' and STATUS=2 ) as t where t.rownum >=" + ((int.Parse(index) - 1) * 10 + 1) + " and t.rownum<=" + (int.Parse(index) * 10) + ")  and STEPLABEL <> '完成'  ";
            if (!String.IsNullOrEmpty(stepName))
            {
                strSql += " and STEPLABEL like '%" + stepName + "%'";
            }

            if (!String.IsNullOrEmpty(incno))
            {
                strSql += " and t.INCIDENT=" + incno;
            }
            strSql += " order by INCIDENT,STEPLABEL";
            DataTable dt = new DataTable();
            dt = clsdb.getDtRtn(strSql);
            strRtn = "{list:[";
            string incident = "";
            string substeps = "";
            string listitem = "";
            int listcount = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string str1 = dt.Rows[i]["INCIDENT"].ToString();
                if (i == 0)
                {
                    incident = str1;
                }
                if (i == dt.Rows.Count - 1)
                {
                    substeps += "{stepName:'" + dt.Rows[i]["STEPLABEL"] + "',stepOverTime:'" + getTime(dt.Rows[i]["overtime"].ToString()) + "',stepTime:'" + getTime(dt.Rows[i]["times"].ToString()) + "'},";
                }
                if (incident != str1 || i == dt.Rows.Count - 1)
                {
                    listitem = "{processName:'" + dt.Rows[i]["PROCESSNAME"].ToString().Trim() + "',incident:" + incident + ",steps:[";
                    incident = str1;
                    substeps = substeps.TrimEnd(','); //获取到 steps:[{},{},{}]
                    listitem += substeps + "]},";
                    strRtn += listitem;
                    listitem = "";
                    substeps = "";
                    listcount++;
                }
                substeps += "{stepName:'" + dt.Rows[i]["STEPLABEL"].ToString().Trim() + "',stepOverTime:'" + getTime(dt.Rows[i]["overtime"].ToString()) + "',stepTime:'" + getTime(dt.Rows[i]["times"].ToString()) + "'},";
            }
            strRtn = strRtn.TrimEnd(',');
            strRtn += "],listcount:" + getCount(processName, stepName, incno) + "}";
            return strRtn;
        }

        private string getCount(string processName,string stepName,string inco)
        {
            string strRtn = "";
            clsOleDB clsdb = new clsOleDB();
            clsdb.setConn("bpmDB");
            string strSql = "select count(*) from INCIDENTS where PROCESSNAME like '%" + processName + "%' and STATUS=2 ";
            if (!String.IsNullOrEmpty(inco)) {
                strSql += " and INCIDENT ="+inco;
            }
            if (!String.IsNullOrEmpty(stepName)) {
                strSql = "select COUNT(*) from TASKS where PROCESSNAME like '%"+processName+"%' and STEPLABEL like'%"+stepName+"%' and STATUS=3 ";
                if (!String.IsNullOrEmpty(inco))
                {
                    strSql += " and INCIDENT =" + inco;
                }
                strSql += " group by PROCESSNAME,STEPLABEL";
            }
            strRtn = clsdb.getAString(strSql);
            return strRtn;
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}