using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace BPM.ReportDesign.Report
{
    public partial class ProcessHandleReport : System.Web.UI.Page
    {
        public static string strPerson = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void loadPageDate(int index) {
            int pageSize = 10;
            clsOleDB clsdb = new clsOleDB();
            clsdb.setConn("bpmDB");
            string strSql = "select  ROW_NUMBER() over(order by PROCESSNAME desc) as num,PROCESSNAME,STEPLABEL,MAX(datediff(ss,STARTTIME,ENDTIME)) as maxSecond,MIN(datediff(ss,STARTTIME,ENDTIME)) as minSecond,AVG(datediff(ss,STARTTIME,ENDTIME)) as avgSecond ,max(datediff(ss,STARTTIME,OVERDUETIME)) as oversecond,'" + strPerson + "' as userjob  from tasks with(nolock) where ASSIGNEDTOUSER like '%" + hideType.Value + "%' and status=3 ";
            string strCountSql = "select count(page.PROCESSNAME) from (select PROCESSNAME from tasks with(nolock) where ASSIGNEDTOUSER like '%" + hideType.Value + "%' and status=3 ";
            if (!String.IsNullOrEmpty(txtProcessName.Value)) {
                strSql += " and PROCESSNAME like '%"+txtProcessName.Value+"%'";
                strCountSql += " and PROCESSNAME like '%" + txtProcessName.Value + "%'";
            }
            strSql += " group by PROCESSNAME,STEPLABEL";
            strCountSql += " group by PROCESSNAME,STEPLABEL ) as page";
            //if (pagination.RecordCount == 0)
            //{
                pagination.RecordCount = int.Parse(clsdb.getAString(strCountSql));
            //}
            string strFirstSql = "select * from (";
            strSql = strFirstSql + strSql + ") as page where page.num <=" + (index + 1) * pagination.PageSize + " and page.num>" + index * pagination.PageSize + "";

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
            strPerson = txtPerson.Value;
            loadPageDate(0);
        }

        /// <summary>
        /// 将秒数转化为 格式  天 小时 分钟
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        public string getTime(string second) {
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