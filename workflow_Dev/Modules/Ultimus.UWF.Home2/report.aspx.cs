using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Ultimus.UWF.Workflow.Interface;
using Ultimus.UWF.Common.Logic;
using System.Data;

namespace Ultimus.UWF.Home2
{
	public partial class report : System.Web.UI.Page
	{
		//IProcessCategory _pc = ServiceContainer.Instance().GetService<IProcessCategory>();
		//List<ReportEntity> _initProcessList = new List<ReportEntity>();
		//Workflow.Interface.ITask _task = ServiceContainer.Instance().GetService<Workflow.Interface.ITask>();
		//string _userAccount = "";
		protected void Page_Load(object sender, EventArgs e)
		{
			//string user = Request.QueryString["user"];
			//if (!string.IsNullOrEmpty(user))
			//{
			//    _userAccount = user;
			//}
			//else
			//{
			//    _userAccount = SessionLogic.GetLoginName().Replace("\\", "/");
			//}
			//_initProcessList = _task.GetInitTaskList(_userAccount, "");
            GetMeanuBindData();
		}
        public void GetMeanuBindData()
        {
            string MID = Request.QueryString["MID"].ToString();

            string MeanuSql = string.Format(@"select M.* from (select ICON,URL,MENUNAME,MENUID from  dbo.SEC_MENU where PARENTID=(select MENUID from  SEC_MENU where MENUNAME='{0}')) M left join 
SEC_MENURIGHTSMEMBER MM on M.MENUID=MM.RIGHTSID where MEMBERID=(select USERID from ORG_USER where LOGINNAME='{1}')", MID, Page.User.Identity.Name);
            DataTable dtMeaun = DataAccess.Instance("BizDB").ExecuteDataTable(MeanuSql);
            if (dtMeaun.Rows.Count > 0)
                RpList.DataSource = dtMeaun;
            else
                RpList.DataSource = null;
            RpList.DataBind();
        }
		//public string GetImgChange(string reportName)
		//{
		//    string ImgSrc = "";
		//    switch (reportName)
		//    {
		//        case "OTAndDayOff":
		//            ImgSrc = "app HR_6";
		//            break;
		//        default:
		//            break;
		//    }
		//    return ImgSrc;
		//}

		//protected void ReportList_ItemDataBound(object sender, RepeaterItemEventArgs e)
		//{
			//if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			//{
			//    //load process by category
			//    Repeater rpControl = (Repeater)e.Item.FindControl("rpProcess");
			//    ProcessCategoryEntity pce = (ProcessCategoryEntity)e.Item.DataItem;
			//    List<ReportEntity> lists = new List<ReportEntity>();
			//    List<ReportEntity> report = new List<ReportEntity>();
			//    if (pce.CATEGORYNAME == Lang.Get("NewTask_AllProcess"))
			//    {
			//    }
			//    else
			//    {
			//        report = _pc.GetProcessList(pce.CATEGORYNAME);
			//        lists = FilterList(report);
			//        if (lists != null)
			//        {
			//            ReportList.DataSource = lists;
			//            ReportList.DataBind();
			//        }
			//    }
			//    // lists.Sort();

			//}
		//}
		//List<ReportEntity> FilterList(List<ReportEntity> lists)
		//{
		//    List<TaskEntity> newlist = new List<TaskEntity>();
		//    for (int i = 0; i < _initProcessList.Count; i++)
		//    {
		//        ProcessEntity process = lists.Find(delegate(ProcessEntity entity)
		//        {
		//            if (entity.PROCESSNAME.ToUpper().Trim() == _initProcessList[i].PROCESSNAME.ToUpper().Trim())
		//            {
		//                return true;
		//            }
		//            return false;
		//        });
		//        if (process != null)
		//        {
		//            newlist.Add(_initProcessList[i]);
		//        }
		//    }
		//    return newlist;
		//}
	}
}