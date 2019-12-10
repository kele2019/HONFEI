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
using Ultimus.UWF.Workflow.Implementation;
using System.Data;

namespace Ultimus.UWF.Workflow
{
    public partial class NewTask2 : System.Web.UI.Page
    {
        IProcessCategory _pc = ServiceContainer.Instance().GetService<IProcessCategory>();
        List<TaskEntity> _initProcessList = new List<TaskEntity>();
        Workflow.Interface.ITask _task = ServiceContainer.Instance().GetService<Workflow.Interface.ITask>();
        string _userAccount = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string user = Request.QueryString["user"];

            string userkey = SessionLogic.GetLoginName().Replace("\\", "/");
            string sessionid = ConvertUtil.ToString(DataAccess.Instance("BizDB").ExecuteScalar("select value from COM_USERSETTINGS where name='" + userkey + "' and SETTINGTYPE='SessionID'"));
            if (!string.IsNullOrEmpty(sessionid))
            {
                _task.LogoutUser(sessionid);
            }


            if (!string.IsNullOrEmpty(user))
            {
                _userAccount = user;
            }
            else
            {
                _userAccount = SessionLogic.GetLoginName().Replace("\\", "/");
            }
            //load init process
            _initProcessList = _task.GetInitTaskList(_userAccount, txtSearch.Text);

            //Process HelpUrl，GetInitTaskList取出来的是发起节点的hepl url
            string allprocessSql = "SELECT PROCESSNAME,PROCESSHELPURL AS HELPURL FROM INITIATE WITH(NOLOCK)";
            DataTable allProcessDt = DataAccess.Instance("UltDB").ExecuteDataTable(allprocessSql);
            foreach (TaskEntity te in _initProcessList) 
            {
                DataRow[] dr = allProcessDt.Select("PROCESSNAME='" + te.PROCESSNAME.Trim() + "'");
                if (dr.Length > 0) 
                {
                    te.HELPURL = dr[0][1].ToString();
                }
            }

            //bind category
            List<ProcessCategoryEntity> lists = _pc.GetCategoryList();
            List<ProcessCategoryEntity> aaa = new List<ProcessCategoryEntity>();
            int i = 0;
            foreach (ProcessCategoryEntity pce in lists)
            {
                List<ProcessEntity> l_pe = _pc.GetProcessList(pce.CATEGORYNAME);
                List<TaskEntity> lists1 = new List<TaskEntity>();
                lists1 = FilterList(l_pe);
                if (!pce.CATEGORYNAME.Equals(Lang.Get("NewTask_AllProcess")))
                {
                    pce.PROCESSCOUNT = "[" + lists1.Count.ToString() + "]";
                    aaa.Add(pce);
                }
                i++;
            }

            //aaa.AddRange(lists);
            if (!aaa.Exists(p => p.CATEGORYNAME == Lang.Get("NewTask_AllProcess")))
            {
                ProcessCategoryEntity pe = new ProcessCategoryEntity();
                pe.CATEGORYNAME = Lang.Get("NewTask_AllProcess");
                //pe.PROCESSCOUNT = "[" + _initProcessList.Count.ToString() + "]";
                pe.PROCESSCOUNT = "";
                pe.CATEGORYID = "all";
                aaa.Insert(0, pe);
            }
            lblCount.Text = _initProcessList.Count.ToString();

            //_pc.GetProcessList(pce.CATEGORYNAME);

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

                //Repeater rpCategoryControl = (Repeater)Page.FindControl("rpProcessCategory");
                //for (int i = 0; i < rpCategoryControl.Items.Count; i++) 
                //{

                //}
                //    //CheckBox cb = this.rpLink.Items[i].FindControl("cbChild") as CheckBox;
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            
        }

        protected void lbtnUp_Click(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument;
            

        }
    }
}