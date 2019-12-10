using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
//using UltimusEikLibrary;
using EntityLibrary;
using DALLibrary;
using CreatePageLibrary;
using RazorEngine.Templating;
using System.Collections.ObjectModel;
using System.CodeDom.Compiler;
using System.Text;

namespace MobileClientBackground
{
    public partial class Default : BasePage.BasePageClass
    {
        private DALLibrary.MobileClient_Process ProcessDal = new DALLibrary.MobileClient_Process();

        private DALLibrary.Initiate Initiate = new DALLibrary.Initiate();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BingData(); 
            }
        }

        private void BingData()
        {
            try
            {
                //查询所有已经发布的流程
                List<EntityLibrary.Initiate> list = Initiate.GetList("");
                //列表集合
                List<EntityLibrary.MobileClient_Process> info = new List<EntityLibrary.MobileClient_Process>();
                //循环已经发布的流程
                foreach (EntityLibrary.Initiate i in list)
                {
                    if (i.ProcessName.Trim() == "一般经费报销流程")
                    {
                        continue;
                    }
                    //根据流程名称到手机客户端主表中查找信息
                    List<EntityLibrary.MobileClient_Process> p = ProcessDal.GetModel("ProcessName='" + i.ProcessName.Trim() + "'");
                    if (p.Count > 0)
                    {
                        //循环查找到得信息添加到列表集合中
                        foreach (EntityLibrary.MobileClient_Process m in p)
                        {
                            info.Add(m);
                        }
                    }
                    else
                    {
                        EntityLibrary.MobileClient_Process model = new EntityLibrary.MobileClient_Process();
                        model.ProcessName = i.ProcessName.Trim();
                        info.Add(model);
                    }
                }

                //条件过滤
                List<EntityLibrary.MobileClient_Process> newinfo = new List<EntityLibrary.MobileClient_Process>();
                newinfo = info.FindAll(delegate(EntityLibrary.MobileClient_Process pi)
                  {
                      if (!String.IsNullOrEmpty(pi.ProcessName) && !String.IsNullOrEmpty(txtProcessName.Text.Trim()))
                      {
                          return pi.ProcessName.Contains(txtProcessName.Text.Trim());
                      }
                      if (!String.IsNullOrEmpty(pi.IsCreatePage) && !String.IsNullOrEmpty(dropCreatePage.SelectedValue.Trim()))
                      {
                          return pi.IsCreatePage.Contains(dropCreatePage.SelectedValue.Trim());
                      }
                      return true;
                  });
                AspNetPager1.RecordCount = newinfo.Count;
                info = new List<EntityLibrary.MobileClient_Process>();
                int index = AspNetPager1.CurrentPageIndex;
                int pageIndex = AspNetPager1.CurrentPageIndex - 1;
                int pageSize = AspNetPager1.PageSize;
                for (int i = 0; i < newinfo.Count; i++)
                {
                    if (i >= pageIndex * pageSize && i < index * pageSize)
                    {
                        info.Add(newinfo[i]);
                    }
                }

                Repeater1.DataSource = info.ToArray();
                Repeater1.DataBind();
            }
            catch (Exception ex)
            {   
                PublicClass.ShowMessage(this.Page, Resources.Resource.Default_List_ErrorMessage1);
                PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BingData();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            BingData();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BingData();
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "OpenProcessConfig")
            {
                string id = e.CommandArgument.ToString();
                string Processname = (e.Item.FindControl("lbProcessName") as Label).Text;
                if (!ProcessDal.Exists(Convert.ToInt32(id)))
                {
                    EntityLibrary.MobileClient_Process model = new EntityLibrary.MobileClient_Process();
                    model.ProcessName = Processname;
                    model.CreateTime = DateTime.Now;
                    id = ProcessDal.Add(model).ToString();
                }
                Response.Redirect("ProcessStepList.aspx?ID=" + id + "&ProcessName=" + Server.UrlEncode(Processname) + "");
            }
            if (e.CommandName == "CreatePage")
            {
                try
                {
                    CreatePage page = new CreatePage();
                    page.CreatePageFile(Convert.ToInt32(e.CommandArgument));
                    PublicClass.ShowMessage(this.Page, "生成页面成功");
                }
                catch (TemplateCompilationException ex)
                {
                    ReadOnlyCollection<CompilerError> errors = ex.Errors;
                    StringBuilder sb = new StringBuilder();
                    foreach (CompilerError err in errors)
                    {
                        sb.Append(err.ErrorText);
                    }
                    PublicClass.ShowMessage(this.Page, "生成页面失败");
                    PublicClass.WriteLogOfTxt(sb.ToString());
                }
            }
        }

    }
}