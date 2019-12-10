using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Home2.Code;
using MyLib;
using Ultimus.UWF.Workflow.Interface;
using Ultimus.UWF.Common.Entity;
using Ultimus.UWF.Workflow.Entity;
using System.Data;
using System.Text;

namespace Ultimus.UWF.Home2
{
    public partial class DraftList : BasePage
    {
        protected string ProcessName { get { return ddlFlow.SelectedItem.Value; } }
        protected string Query_Type { get { return (Request.QueryString["Type"] + string.Empty).ToLower(); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            btnSearch.ImageUrl = LanguageHelper.Get("Btn_Toolbar_Search");

            if (!IsPostBack)
            {
                UIHelper.InitFlowDropdownList(ddlFlow, true);
                BindData();
            }
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            //int skipResults = (AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize;
            //int maxResults = AspNetPager1.PageSize;
            //List<ParameterEntity> table = new List<ParameterEntity>();
            //string filter = GetFilter(out table);
            //ITask _task = ServiceContainer.Instance().GetService<ITask>();
            //List<TaskEntity> lists = _task.GetDraftTaskList(UserID, filter);
            //filter = GetFilter(out table);
            //AspNetPager1.RecordCount = _task.GetMyTaskCount(UserID, filter, table);
            //rptList.DataSource = lists;
            //rptList.DataBind();
            //if (lists.Count == 0)
            //{
            //    plcEmpty.Visible = true;
            //    AspNetPager1.Visible = false;
            //}
            //else
            //{
            //    plcEmpty.Visible = false;
            //    AspNetPager1.Visible = true;
            //}
        
            DataTable dt = new DataTable();
            dt = DataAccess.Instance("BizDB").ExecuteDataTable("select * from WF_DRAFT where createby='" + UserID + "'");
            rptList.DataSource = dt;
            rptList.DataBind();
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "del")
            {
                string str = e.CommandArgument.ToString();
                DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable("select * from WF_DRAFT where formid='" + str + "'");
                StringBuilder sb = new StringBuilder();
                foreach (DataRow row in dt.Rows)
                {
                    string[] sz = ConvertUtil.ToString(row["TABLENAME"]).Split(',');
                    foreach (string ss in sz)
                    {
                        //sb.AppendFormat("delete from {0} where FORMID='{1}'", ss, str);
                        //sb.AppendLine();
                    }
                }
                sb.Append("delete from WF_DRAFT where formid='" + str + "'");
                DataAccess.Instance("BizDB").ExecuteNonQuery(sb.ToString());
                BindData();
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + LanguageHelper.Get("SecurityList_DeleteSuccess") + "!');", true);
            }
        }
        string GetFilter(out List<ParameterEntity> table)
        {
            SqlFilterUtil fb = new SqlFilterUtil(false);
            if (!string.IsNullOrEmpty(ProcessName))
                fb.AddLike("a.PROCESSNAME", ProcessName);
            if (!string.IsNullOrEmpty(txtTitle.Text))
                fb.AddLike("SUMMARY", txtTitle.Text);

            table = new List<ParameterEntity>();
            //DateTime startTime = DateTime.MinValue;
            //DateTime endTime = DateTime.MinValue;
            //if (!DateTime.TryParse(txtDateStart.Text, out startTime))
            //    startTime = GetStartTime();
            //if (!DateTime.TryParse(txtDateEnd.Text, out endTime))
            //    endTime = GetEndTime();

            //table.Add(new ParameterEntity("STARTTIME", startTime.ToString()));
            //table.Add(new ParameterEntity("ENDTIME", endTime.ToString()));
            return fb.ToString();
        }
    }
}