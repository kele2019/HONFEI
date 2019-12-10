using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Home2.Code;
using Ultimus.UWF.Workflow.Interface;
using Ultimus.UWF.Workflow.Entity;
using MyLib;
using Ultimus.UWF.Common.Logic;
using Ultimus.UWF.Common.Entity;

namespace Ultimus.UWF.Home2
{
    public partial class Create : BasePage
    {
        IProcessCategory _pc = ServiceContainer.Instance().GetService<IProcessCategory>();
        List<TaskEntity> _initProcessList = new List<TaskEntity>();
        Workflow.Interface.ITask _task = ServiceContainer.Instance().GetService<Workflow.Interface.ITask>();
        string _userAccount = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string user = Request.QueryString["user"];
            if (!string.IsNullOrEmpty(user))
            {
                _userAccount = user;
            }
            else
            {
                _userAccount = SessionLogic.GetLoginName().Replace("\\", "/");
            }
            //load init process
            _initProcessList = _task.GetInitTaskList(_userAccount, "");

            //bind category
            List<ProcessCategoryEntity> lists = _pc.GetCategoryList();
			List<ProcessCategoryEntity> aaa = new List<ProcessCategoryEntity>();
			aaa.AddRange(lists);
			if (!aaa.Exists(p => p.CATEGORYNAME == Lang.Get("NewTask_AllProcess")))
			{
				ProcessCategoryEntity pe = new ProcessCategoryEntity();
				pe.CATEGORYNAME = Lang.Get("NewTask_AllProcess");
				pe.CATEGORYENNAME = Lang.Get("NewTask_AllProcess");
				pe.CATEGORYID = "all";
				aaa.Insert(0, pe);
			}
			//rpProcessCategory.DataSource = aaa;
			//rpProcessCategory.DataBind();

			rpAdministrationProcess.DataSource = aaa;
			rpAdministrationProcess.DataBind();

            txtCurrentCategory.Text = "all";
            List<ParameterEntity> table = new List<ParameterEntity>();
            table.Add(new ParameterEntity("STARTTIME", GetStartTime().ToString()));
            table.Add(new ParameterEntity("ENDTIME",  GetEndTime().ToString()));
            txtType.Text= _task.GetMyTaskCount(UserID, "", table).ToString();
            

        }

        protected void rpAllProcess_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //load process by category
                Repeater rpControl = (Repeater)e.Item.FindControl("rpProcess");
                ProcessCategoryEntity pce = (ProcessCategoryEntity)e.Item.DataItem;
                List<TaskEntity> lists = new List<TaskEntity>();
                List<ProcessEntity> process = new List<ProcessEntity>();
                if (pce.CATEGORYNAME == Lang.Get("NewTask_AllProcess"))
				{
					 #region
					//foreach (TaskEntity item in _initProcessList)
					//{
					//    if (item.PROCESSNAME == "Company Seal Request Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "Company Seal Request";
					//    }
					//    if (item.PROCESSNAME == "Outgoing Document Signature Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "Outgoing Document";
					//    }
					//    if (item.PROCESSNAME == "Use of Certificate Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "Use of Certificate";
					//    }
					//    if (item.PROCESSNAME == "Badge Request Application Form Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "Badge Request";
					//    }

					//    if (item.PROCESSNAME == "Key Request Application Form Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "Key Request";
					//    }
					//    if (item.PROCESSNAME == "Badge LostReadjustment Application Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "Badge LostReadjustment";
					//    }
					//    if (item.PROCESSNAME == "HongFei JV Access Request Form Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "HongFei JV Access Request";
					//    }
					//    if (item.PROCESSNAME == "Backup Request Form Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "Backup Request";
					//    }
					//    if (item.PROCESSNAME == "Recovery Request Form Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "Recovery Request";
					//    }
					//    if (item.PROCESSNAME == "IT Change Request Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "IT Change Request";
					//    }
					//    if (item.PROCESSNAME == "Website access Request Form Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "Website access Request";
					//    }
					//    if (item.PROCESSNAME == "Asset Borrowing Request Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "Asset Borrowing Request";
					//    }
					//    if (item.PROCESSNAME == "HelpDesk Registration Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "HelpDesk Registration";
					//    }
					//    if (item.PROCESSNAME == "New Employee Onboarding Agenda Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "New Employee";
					//    }
					//    if (item.PROCESSNAME == "Voluntary Resignation Application Form Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "Voluntary Resignation";
					//    }
					//    if (item.PROCESSNAME == "DayOff Record Application Form Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "Day-Off Record";
					//    }
					//    if (item.PROCESSNAME == "Leave Application Form Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "Leave Application";
					//    }
					//    if (item.PROCESSNAME == "Employee TerminationCheck Out List Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "Employee Termination";
					//    }
					//    if (item.PROCESSNAME == "Employee Training management Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "Employee Training";
					//    }
					//    if (item.PROCESSNAME == "OT Application Form Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "OT Application";
					//    }
					//    if (item.PROCESSNAME == "Employee Performance Report Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "Employee Performance Report";
					//    }
					//    if (item.PROCESSNAME == "Process Performance Measurement Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "KPI";
					//    }
					//    if (item.PROCESSNAME == "Travel Request Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "Travel Request";
					//    }
					//    if (item.PROCESSNAME == "Travel Expense Request Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "Travel Expense";
					//    }
					//    if (item.PROCESSNAME == "Down Payment Form Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "Payment Form";
					//    }
					//    if (item.PROCESSNAME == "Purchase Request Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "Purchase Request";
					//    }
					//    if (item.PROCESSNAME == "Payment Request Form Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "Payment Request";
					//    }
					//    if (item.PROCESSNAME == "Purchase Order Process")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "Purchase Order";
					//    }
					//    if (item.PROCESSNAME == "ProcessDemo")
					//    {
					//        item.OVERDUETIME = DateTime.Now.AddDays(-4);
					//        item.SUMMARY = "ProcessDemo";
					//    }

					//}
					#endregion
					//lists = _initProcessList.OrderByDescending(p=>p.OVERDUETIME).ToList();
                }
                else
                {
                    process = _pc.GetProcessList(pce.CATEGORYNAME);
                    lists = FilterList(process);
					if (lists != null)
					{
						rpControl.DataSource = lists;
						rpControl.DataBind();
					}
                }
               // lists.Sort();
              
            }
        }

        List<TaskEntity> FilterList(List<ProcessEntity> lists)
        {
            List<TaskEntity> newlist = new List<TaskEntity>();
            for (int i = 0; i < _initProcessList.Count; i++)
            {
                ProcessEntity process = lists.Find(delegate(ProcessEntity entity)
                {
                    if (entity.PROCESSNAME.ToUpper().Trim() == _initProcessList[i].PROCESSNAME.ToUpper().Trim())
                    {
                        _initProcessList[i].ICON = entity.ICON;
                        return true;
                    }
                    return false;
                });
                if (process != null)
                {
                    newlist.Add(_initProcessList[i]);
                }
            }
            return newlist;
        }

        public string GetCategoryImage(string categoryName)
        {
            List<ProcessCategoryEntity> lists = _pc.GetCategoryList();
            ProcessCategoryEntity ety = lists.Find(p => p.CATEGORYENNAME == categoryName || p.CATEGORYNAME == categoryName);
            string pic = "";
            if (ety != null)
            {
                if (!string.IsNullOrEmpty(ety.ICON))
                {
                    pic = ety.ICON;
                }
            }


            return categoryName == Lang.Get("NewTask_AllProcess") ? "allSel.png" : pic;
        }
		public string GetDeptImgChange(string DepartmentName)
		{
			string ImgSrc = "";
			switch (DepartmentName)
			{
				case "Administration":
					ImgSrc = "icon dept_1";
					break;
				case "Purchase":
					ImgSrc = "icon dept_2";
					break;
				case "HR":
					ImgSrc = "icon dept_3";
					break;
				case "Finance":
					ImgSrc = "icon dept_4";
					break;
				case "Quality":
					ImgSrc = "icon dept_5";
					break;
				case "IT":
					ImgSrc = "icon dept_6";
					break;
				default:
					break;
			}
			return ImgSrc;
		}
        public string GetImgChange(string ProcessName)
        {
            string ImgSrc = "";
            switch (ProcessName)
            {
				case "Company Seal Request":
                    ImgSrc = "app Admin_1";// LanguageHelper.Get("Btn_Create_CCBX");
                    break;
				case "Outgoing Document Signature":
					ImgSrc = "app Admin_2";
					break;
				case "Use of Certificate":
					ImgSrc = "app Admin_3";
					break;
				case "Badge Application":
					ImgSrc = "app Admin_4";
					break;
				case "Key Application":
					ImgSrc = "app Admin_5";
					break;
				case "Badge LostReadjustment Application":
					ImgSrc = "app Admin_6";
					break;
				case "IT Access Request":
					ImgSrc = "app IT_1";
					break;
				case "IT Backup Request":
					ImgSrc = "app IT_2";
					break;
				case "IT Recovery Request":
					ImgSrc = "app IT_3";
					break;
				case "IT Change Request":
					ImgSrc = "app IT_4";
					break;
				case "Website access Request":
					ImgSrc = "app IT_6";
					break;
				case "IT Asset Borrowing Request":
					ImgSrc = "app IT_5";
					break;
				case "IT HelpDesk Registration":
					ImgSrc = "app IT_7";
					break;
				case "New Employee Onboarding Agenda":
					ImgSrc = "app HR_1";
					break;
				case "Voluntary Resignation Application":
					ImgSrc = "app HR_2";
					break;
				case "DayOff Record Application":
					ImgSrc = "app HR_7";
					break;
				case "Leave Application":
					ImgSrc = "app HR_8";
					break;
				case "Employee TerminationCheck Out List":
					ImgSrc = "app HR_3";
					break;
				case "Employee Training management":
					ImgSrc = "app HR_4";
					break;
				case "OT Application":
					ImgSrc = "app HR_6";
					break;
				case "Remind Submit Employee Performance":
					ImgSrc = "app HR_9";
					break;
				case "Employee Performance Report":
					ImgSrc = "app HR_5";
					break;
				case "Quality document management":
					ImgSrc = "app Quality_1";
					break;
				case "KPI Submit Process":
					ImgSrc = "app Quality_2";
					break;
				case "Travel Request":
					ImgSrc = "app Finance_3";
					break;
				case "Travel Expense Request":
					ImgSrc = "app Finance_2";
					break;
				case "Local Expense":
					ImgSrc = "app Finance_1";
					break;
				case "Down Payment":
					ImgSrc = "app Finance_5";
					break;
				case "Purchase Request":
					ImgSrc = "app Purchase_1";
					break;
				case "Payment Request":
					ImgSrc = "app Finance_4";
					break;
				case "Purchase Order":
					ImgSrc = "app Purchase_2";
					break;
                case "Budget Process":
                    ImgSrc = "app a_6";
                    break;//LanguageHelper.get("");
                default:
                    break;
            }
            return ImgSrc;
        }

    }
}