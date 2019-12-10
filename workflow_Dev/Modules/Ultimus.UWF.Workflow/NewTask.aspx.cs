using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Workflow.Interface;
using MyLib;
using Ultimus.UWF.Workflow.Entity;
using Ultimus.UWF.Common.Logic;

namespace Ultimus.UWF.Workflow
{
    public partial class NewTask : System.Web.UI.Page
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
            _initProcessList = _task.GetInitTaskList(_userAccount,"");

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
            rpProcessCategory.DataSource = aaa;
            rpProcessCategory.DataBind();

            rpAllProcess.DataSource = aaa;
            rpAllProcess.DataBind();

            txtCurrentCategory.Text = "all";
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
                    lists = _initProcessList;
                }
                else
                {
                    process = _pc.GetProcessList(pce.CATEGORYNAME);
                    lists = FilterList(process);
                }
                lists.Sort();
                rpControl.DataSource = lists;
                rpControl.DataBind();
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
    }
}