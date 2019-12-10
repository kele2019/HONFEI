using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace BPM.ReportDesign.Report
{
    public partial class ProcessRunningEff : System.Web.UI.Page
    {
        public static  string processName = String.Empty;
        public static string startdate = String.Empty;
        public static string enddate = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                string view = Request["view"];
                
                if (!String.IsNullOrEmpty(view) ) {
                    divCondition.Visible = false;
                    processName = Request["ProcessName"];
                    startdate = Request["startdate"];
                    enddate = Request["enddate"];
                    loadPageDate(0);
                }
                //loadPageDate(0);
            }
        }

        private void loadPageDate(int index) {
            int pageSize = 24;
            clsOleDB clsdb = new clsOleDB();
            clsdb.setConn("bpmDB");
            string strSql = "select ROW_NUMBER() over(order by DATEDIFF(SS,STARTTIME,ENDTIME) desc) as num,PROCESSNAME,INCIDENT,DATEDIFF(SS,STARTTIME,ENDTIME) as totleSeconds,longStep=(select top 1 STEPLABEL  from TASKS where PROCESSNAME = ''+inc.PROCESSNAME+'' and INCIDENT=inc.INCIDENT order by DATEDIFF(SS,STARTTIME,ENDTIME)  desc),longSec=(select top 1 DATEDIFF(SS,STARTTIME,ENDTIME) as sec  from TASKS with(nolock) where PROCESSNAME = ''+inc.PROCESSNAME+''   and INCIDENT=inc.INCIDENT order by sec desc) from INCIDENTS   as inc with(nolock) where STATUS=2 ";
            string strCountSql = "select count(*) from INCIDENTS  with(nolock) where STATUS=2 ";
            if (!String.IsNullOrEmpty(processName))
            {
                strSql += " and PROCESSNAME = '" + processName.Trim() + "'";
                strCountSql += " and PROCESSNAME = '" + processName.Trim() + "'";
                if (!String.IsNullOrEmpty(startdate))
                {
                    strSql += " and endtime >= '" + startdate + " 00:01'";
                    strCountSql += " and endtime >= '" + startdate + " 00:01'";
                }
                if (!String.IsNullOrEmpty(enddate))
                {
                    strSql += " and endtime <= '" + enddate + " 23:59'";
                    strCountSql += " and endtime <= '" + enddate + " 23:59'";
                }
            }
            else
            {
                strSql += " and PROCESSNAME like '%" + txtProcessName.Value.Trim() + "%'";
                strCountSql += " and PROCESSNAME like '%" + txtProcessName.Value.Trim() + "%'";
                if (!String.IsNullOrEmpty(txtStartTime.Value))
                {
                    strSql += " and endtime >= '" + txtStartTime.Value + " 00:01'";
                    strCountSql += " and endtime >= '" + txtStartTime.Value + " 00:01'";
                }
                if (!String.IsNullOrEmpty(txtEndTime.Value))
                {
                    strSql += " and endtime <= '" + txtEndTime.Value + " 23:59'";
                    strCountSql += " and endtime <= '" + txtEndTime.Value + " 23:59'";
                }
            }

            //if (pagination.RecordCount == 0)
            //{
                pagination.RecordCount = int.Parse( clsdb.getAString(strCountSql));
            //}
            string strFirstSql = "select * from (";
            strSql = strFirstSql + strSql + ") as page where page.num <="+(index+1)*pagination.PageSize+" and page.num>"+index*pagination.PageSize+"";
           // strSql += " order by totleSeconds desc";

            //PagedDataSource page = new PagedDataSource();
            DataTable dt = new DataTable();
            dt = clsdb.getDtRtn(strSql);

            //int totalCount = dt.Rows.Count;
            lbCount.Text = pagination.RecordCount.ToString();
            //DataView dv = dt.DefaultView;
            //page.DataSource = dt.DefaultView;
            //page.AllowPaging = true;
            //page.PageSize = pageSize;
            //page.CurrentPageIndex = index;

            rpSource.DataSource = dt;
            rpSource.DataBind();

            //pagination.RecordCount = totalCount;
            //pagination.PageSize = page.PageSize;
            //pagination.CurrentPageIndex = page.CurrentPageIndex + 1;
            //pagination.Visible = true;
            //pagination.AlwaysShow = true;
            //pagination.CurrentPageButtonClass = "btnPageNavCurrentInput";
            //pagination.DataBind();

        }

        /// <summary>
        /// 分页选择
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void pagination_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            loadPageDate(e.NewPageIndex - 1);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadPageDate(0);
        }

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
                    int mins = ((sec % (60 * 60 * 24)) % (60 * 60)) / 60+1;
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