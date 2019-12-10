using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;

namespace BPM.ReportDesign.Report
{
    public partial class ProcessRunningReport : System.Web.UI.Page
    {

        private static double minDays = 0;
        private static double maxDays = 0;
        private static double avgDays = 0;
        private static DataTable dt = null;

        #region -- 页面加载 --
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                BindData(0);
            }
        }
        #endregion

        #region  -- 绑定数据 --
        public void BindData(int index)
        {
            clsOleDB clsdb = new clsOleDB();
            clsdb.setConn("bpmDB");            
            int pageSize = 24;

            string strSql = "select ROW_NUMBER() over(order by count(*) desc) as num,PROCESSNAME,COUNT(*) as allIncident,MAX(DATEDIFF(MI,STARTTIME,ENDTIME)) as maxSec,MIN(DATEDIFF(MI,STARTTIME,ENDTIME))+1 as minSec,avg(DATEDIFF(MI,STARTTIME,ENDTIME)) as avgSec  from INCIDENTS with(nolock) where STATUS=2   ";
            string countSql = "select COUNT(page.num)  from( select COUNT(*) as num from INCIDENTS where STATUS=2 ";
            //string strCountSql = "select count(*) from INCIDENTS with(nolock) where STATUS=2   ";
            string strWhere = "";
            if (txtProcessName.Text.Trim() != "")
            {
                strWhere += " and PROCESSNAME like '%" + txtProcessName.Text.Trim() + "%'";
            }
            if (txtStartDate.Text.Trim() != "")
            {
                strWhere += " and ENDTIME >= '" + txtStartDate.Text.Trim() + " 00:01'";
            }
            if (txtEndDate.Text.Trim() != "")
            {
                strWhere += " and ENDTIME <= '" + txtEndDate.Text.Trim() + " 23:59'";
            }
            countSql += strWhere + "group by PROCESSNAME) as page";
            pagination.RecordCount = int.Parse(clsdb.getAString(countSql));

            strWhere += " group by PROCESSNAME ";

            strSql = "select * from (" + strSql + strWhere + ") as page where page.num <=" + (index + 1) * pagination.PageSize + " and page.num>" + index * pagination.PageSize + "";
            

            //绑定数据
            PagedDataSource page = new PagedDataSource();           
            dt = clsdb.getDtRtn(strSql);

            //int totalCount = dt.Rows.Count;
            lbTotal.Text = pagination.RecordCount.ToString();
            //DataView dv = dt.DefaultView;
            //page.DataSource = dt.DefaultView;
            //page.AllowPaging = true;
            //page.PageSize = pageSize;
            //page.CurrentPageIndex = index;

            ReportList.DataSource = dt;
            ReportList.DataBind();

            //pagination.RecordCount = totalCount;
            //pagination.PageSize = page.PageSize;
            //pagination.CurrentPageIndex = page.CurrentPageIndex + 1;
            //pagination.Visible = true;
            //pagination.AlwaysShow = true;
            //pagination.CurrentPageButtonClass = "btnPageNavCurrentInput";
            //pagination.DataBind();
        }

        #endregion

        #region  -- 事件方法 --
        protected void btSearch_Click(object sender, EventArgs e)
        {
            BindData(0);
        }
        //分页
        protected void pagination_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            BindData(e.NewPageIndex - 1);
         }
        #endregion

        #region[－－其他方法－－]

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


        public string getTimeFromMi(string mi)
        {
            string strRtn = "";
            if (!String.IsNullOrEmpty(mi))
            {
                int min = int.Parse(mi);
                if (min <= 0)
                {
                    strRtn = "0";
                }
                else
                {
                    int day = min / (60 * 24);
                    int hours = (min % (60 * 24)) / 60;
                    int mins = (min % (60 * 24)) %  60;
                    strRtn = day.ToString() + "天" + hours.ToString() + "小时" + mins.ToString() + "分钟";
                }
            }
            else
            {
                strRtn = "无";
            }
            return strRtn;
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
        #endregion

       
       

       

    }
}