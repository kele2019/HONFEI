using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls; 
using MyLib;
using System.Threading;
using Ultimus.UWF.Workflow.Logic;
using Ultimus.UWF.Workflow.Entity;
using Ultimus.UWF.Workflow.Interface;
using Ultimus.UWF.Common.Logic;
using Ultimus.UWF.Form.Logic;
using System.Data;

namespace Ultimus.UWF.Workflow
{
    public partial class ProcessCategory : System.Web.UI.Page
    {

         
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProcessLogic logic = new ProcessLogic();
                //List<ProcessEntity> list= logic.GetProcessList();
                Workflow.Interface.ITask _task = ServiceContainer.Instance().GetService<Workflow.Interface.ITask>();
                List<TaskEntity> _initProcessList = _task.GetInitTaskList(SessionLogic.GetLoginName().Replace("\\", "/"), "");
                List<ProcessEntity> list = new List<ProcessEntity>();
                foreach (TaskEntity entity in _initProcessList)
                {
                    DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable("select * from V_WF_CATEGORY where PROCESSNAME='" + entity.PROCESSNAME + "'");
                    ProcessEntity pe = new ProcessEntity();
                    pe.PROCESSNAME = entity.PROCESSNAME;
                    if (dt.Rows.Count > 0)
                    {
                        pe.CATEGORYNAME = ConvertUtil.ToString(dt.Rows[0]["CATEGORYNAME"]);
                        pe.CATEGORYENNAME = ConvertUtil.ToString(dt.Rows[0]["CATEGORYENNAME"]);
                        pe.ICON = ConvertUtil.ToString(dt.Rows[0]["ICON"]);
                    }
                    list.Add(pe);
                }

                rptTask.DataSource = list;
                rptTask.DataBind();
            }
        }

        protected void rptTask_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Label pro = e.Item.FindControl("cbSelect") as Label;
            TextBox caten = e.Item.FindControl("TextBox1") as TextBox;
            TextBox cat = e.Item.FindControl("Label1") as TextBox;
            TextBox icon = e.Item.FindControl("TextBox2") as TextBox;

            if (e.CommandName == "set")
            {
                string sql = "select CATEGORYID from WF_PROCESSCATEGORY where DISPLAYNAME='" + cat.Text + "'";
                string obj=ConvertUtil.ToString( DataAccess.Instance("BizDB").ExecuteScalar(sql));
                
                    SerialNoLogic sn = new SerialNoLogic();
                int id;
                if (string.IsNullOrEmpty(obj))
                {
                    obj=Guid.NewGuid().ToString();
                    id = sn.GetMaxNo("WF_PROCESSCATEGORY", "ID");
                    sql = "insert into WF_PROCESSCATEGORY(ID,CATEGORYID,CATEGORYNAME,DISPLAYNAME,ICON) values(" + id + ",'" + obj + "','" + caten.Text + "','" + cat.Text + "','" + icon.Text+ "')";
                    DataAccess.Instance("BizDB").ExecuteNonQuery(sql);
                }
                sql = "select PROCESSNAME from WF_PROCESS where PROCESSNAME='" + pro.Text + "'";
                string processname = ConvertUtil.ToString(DataAccess.Instance("BizDB").ExecuteScalar(sql));
                if (string.IsNullOrEmpty(processname))
                {
                    id = sn.GetMaxNo("WF_PROCESS", "ID");
                    sql = "insert into WF_PROCESS(ID,PROCESSNAME,DISPLAYNAME,CATEGORYID) values(" + id + ",'" + pro.Text + "','" + pro.Text + "','" + obj + "' )";
                    DataAccess.Instance("BizDB").ExecuteNonQuery(sql);
                }
                else
                {
                    sql = "update  WF_PROCESS set CATEGORYID='"+obj+"' where processname='"+pro.Text+"'";
                    DataAccess.Instance("BizDB").ExecuteNonQuery(sql);
                }

                IProcessCategory pc = ServiceContainer.Instance().GetService<IProcessCategory>();
                pc.Clear();

            }

            if (e.CommandName == "form")
            {
                FormLogic logic = new FormLogic();
                logic.CreateForm(pro.Text);
            }

            if (e.CommandName == "formAgain")
            {
                DataAccess.Instance("BizDB").ExecuteNonQuery("update WF_PROCESS set HASFORM='0'");
                FormLogic logic = new FormLogic();
                logic.CreateForm(pro.Text);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "aa", "alert('生成成功！');", true);
            }
        }
    }
}