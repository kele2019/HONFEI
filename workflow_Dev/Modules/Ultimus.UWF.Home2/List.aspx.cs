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

namespace Ultimus.UWF.Home2
{
	public partial class List : BasePage
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
			int skipResults = (AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize;
			int maxResults = AspNetPager1.PageSize;
			List<ParameterEntity> table = new List<ParameterEntity>();
			string filter = GetFilter(out table);
			string sort = "a.STARTTIME desc";
			ITask _task = ServiceContainer.Instance().GetService<ITask>();
			List<TaskEntity> lists = null;
			int count = 0;
			if (Query_Type == "mytask")//待办
			{
				lists = _task.GetMyTaskList(UserID, filter, table, sort, skipResults, maxResults);
				filter = GetFilter(out table);
				count = _task.GetMyTaskCount(UserID, filter, table);
			}
			else if (Query_Type == "myapproval")//已办
			{
				lists = _task.GetMyApprovalList(UserID, filter, table, sort, skipResults, maxResults);
				filter = GetFilter(out table);
				count = _task.GetMyApprovalCount(UserID, filter, table);
			}
			else if (Query_Type == "myrequest")//我的申请
			{
				lists = _task.GetMyRequestList(UserID, filter, table, sort, skipResults, maxResults);
				filter = GetFilter(out table);
				count = _task.GetMyRequestCount(UserID, filter, table);
			}

			rptList.DataSource = lists;
			rptList.DataBind();
			AspNetPager1.RecordCount = count;
			if (lists == null || lists.Count == 0)
			{
				plcEmpty.Visible = true;
				AspNetPager1.Visible = false;
			}
			else
			{
				plcEmpty.Visible = false;
				AspNetPager1.Visible = true;
			}
		}

		protected void AspNetPager1_PageChanged(object sender, EventArgs e)
		{
			BindData();
		}

		string GetFilter(out List<ParameterEntity> table)
		{
			SqlFilterUtil fb = new SqlFilterUtil(false);
			if (!string.IsNullOrEmpty(ProcessName))
				fb.AddLike("a.PROCESSNAME", ProcessName);
			if (!string.IsNullOrEmpty(txtTitle.Text))
				fb.AddLike("SUMMARY", txtTitle.Text);
            if (dropStatus.SelectedIndex != 0)
                fb.AddEqual("b.Status", dropStatus.SelectedItem.Value);
			table = new List<ParameterEntity>();
			DateTime startTime = DateTime.MinValue;
			DateTime endTime = DateTime.MinValue;
			if (!DateTime.TryParse(txtDateStart.Text, out startTime))
				startTime = GetStartTime();
			if (!DateTime.TryParse(txtDateEnd.Text, out endTime))
				endTime = GetEndTime();

			if (txtDateEnd.Text.Trim() != "" && txtDateStart.Text.Trim() != "")
			{
				table.Add(new ParameterEntity("STARTTIME", startTime.AddDays(-1).ToString()));
				table.Add(new ParameterEntity("ENDTIME", endTime.AddDays(1).ToString()));
			}
			else
			{
				if (txtDateStart.Text.Trim() != "")
				{
					table.Add(new ParameterEntity("STARTTIME", startTime.ToString()));
					table.Add(new ParameterEntity("ENDTIME", endTime.ToString()));
				}
				else
				{
					if (txtDateEnd.Text.Trim() != "")
					{
						table.Add(new ParameterEntity("STARTTIME", startTime.ToString()));
						table.Add(new ParameterEntity("ENDTIME", endTime.AddDays(1).ToString()));
					}
					else
					{
						table.Add(new ParameterEntity("STARTTIME", startTime.ToString()));
						table.Add(new ParameterEntity("ENDTIME", endTime.ToString()));
					}
				}

			}
			return fb.ToString();
		}
		public string CurrenctUser(string ProcessName, string IncidentID)
		{
			object objCuser = DataAccess.Instance("UltDB").ExecuteScalar(string.Format("select top 1  ASSIGNEDTOUSER from TASKS   with(nolock) where  INCIDENT='{0}' and PROCESSNAME='{1}'  and STATUS=1", IncidentID, ProcessName));
			if (objCuser != null)
			{
				return objCuser.ToString().IndexOf('/') > 0 ? objCuser.ToString().Split('/')[1] : "";
			}
			else
				return "";
		}
	}
}