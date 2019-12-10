using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Workflow.Interface;
using Ultimus.UWF.Common.Logic;
using Ultimus.UWF.Workflow.Entity;
using System.Data;
using MyLib;
using System.Text;

namespace Ultimus.UWF.Workflow
{
    public partial class DraftList2 : System.Web.UI.Page
    {
        IProcessCategory _category = ServiceContainer.Instance().GetService<IProcessCategory>();
        ITask _task = ServiceContainer.Instance().GetService<ITask>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtType.Text = Request.QueryString["Type"];
                txtSort.Text = Request.QueryString["Sort"];
                txtPreSort.Text = Request.QueryString["Sort"];
                txtDateType.Text = Request.QueryString["DateType"];
                txtProcessCategory.Text = Request.QueryString["ProcessCategory"];
                if (txtProcessCategory.Text == Lang.Get("TaskList_All"))
                {
                    txtProcessCategory.Text = "";
                }
                 
                txtShowQuery.Text = Request.QueryString["ShowQuery"];
                BindProcessCategory();
                BindGrid();

                 
                AspNetPager1.FirstPageText = Lang.Get("FirstPage");
                AspNetPager1.PrevPageText = Lang.Get("PrevPage");
                AspNetPager1.NextPageText = Lang.Get("NextPage");
                AspNetPager1.LastPageText = Lang.Get("LastPage");
                //btnDelete.Text=Lang.Get("DraftList_Delete");

            }
        }

        void BindProcessCategory()
        {
            List<ProcessCategoryEntity> lists = _category.GetCategoryList();
            if (!lists.Exists(p => p.CATEGORYNAME == Lang.Get("NewTask_AllProcess")))
            {
                ProcessCategoryEntity pe = new ProcessCategoryEntity();
                pe.CATEGORYNAME = Lang.Get("NewTask_AllProcess");
                lists.Insert(0, pe);
            }
        }

        void BindGrid()
        {
            List<TaskEntity> tasks = _task.GetDraftTaskList(SessionLogic.GetLoginName().Replace("\\", "/"),"");
            //DataTable dt = new DataTable();
            //dt = DataAccess.Instance("BizDB").ExecuteDataTable("select * from WF_DRAFT where createby='" + SessionLogic.GetLoginName() + "'");
            rptTask.DataSource = tasks;
            rptTask.DataBind();
        }

        protected void lbProcessCategory_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            string id = linkButton.CommandArgument;
            if (id == Lang.Get("TaskList_All"))
            {
                txtProcessCategory.Text = "";
            }
            else
            {
                txtProcessCategory.Text = id;
            }
            BindGrid();
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindGrid();

        }

        protected void rptTask_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "del")
            {
                string str = e.CommandArgument.ToString();
                //DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable("select * from WF_DRAFT where formid='" + str + "'");
                //StringBuilder sb = new StringBuilder();
                //foreach (DataRow row in dt.Rows)
                //{
                //    string[] sz = ConvertUtil.ToString(row["TABLENAME"]).Split(',');
                //    foreach (string ss in sz)
                //    {
                //        //sb.AppendFormat("delete from {0} where FORMID='{1}'", ss, str);
                //        //sb.AppendLine();
                //    }
                //}
                //sb.Append("delete from WF_DRAFT where formid='" + str + "'");
                //DataAccess.Instance("BizDB").ExecuteNonQuery(sb.ToString());
                TaskEntity ety=new TaskEntity();
                string[] sz=str.Split('_');
                ety.TASKID=sz[1];
                ety.ASSIGNEDTOUSER=SessionLogic.GetLoginName().Replace("\\","/");
                ServerEntity enty=new ServerEntity();
                enty.SERVERNAME=sz[0];
                _task.SetServerEntity(enty);
                if (_task.DeleteTask(ety))
                {
                    BindGrid();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('删除成功!');", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('删除失败!');", true);
                }
            }
        }

        public string GetUrl(object obj)
        {
            return Server.UrlEncode(obj.ToString());
        }
    }
}