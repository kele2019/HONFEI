using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MobileClient.PublicFunctionClass;
//using MobileClient.MobileClientBackgroundRef;
using ClientService;

namespace MobileClient
{
    public partial class NewTaskList : BasePageClass.BasePage
    {
        private WorkflowRef services = new WorkflowRef();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BingProcessCount();
                BingProcess();
            }
        }

        private void BingProcessCount()
        {
            try
            {
                string CategoryName = Request.QueryString["CategoryName"].ToString().Trim();
                //lbProcessCount.Text = services.GetCategoryCountByCategoryName(CategoryName).ToString();
            }
            catch (Exception ex)
            {
                PublicClass.ShowMessage(this.Page, Resources.Resource.NewTaskList_ErrorMessage1);
                PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

        private void BingProcess()
        {
            try
            {
                string CategoryName = Request.QueryString["CategoryName"].ToString().Trim();
                //int count = services.GetCategoryCountByCategoryName(CategoryName);
                //int num = 0;
                //if (count % 3 == 0)
                //{
                //    num = count / 3;
                //}
                //else
                //{
                //    num = count / 3 + 1;
                //}
                //string[] array = new string[num];
                //Repeater1.DataSource = array;
                //Repeater1.DataBind();
            }
            catch (Exception ex)
            {
                PublicClass.ShowMessage(this.Page, Resources.Resource.NewTaskList_ErrorMessage2);
                PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                int index = e.Item.ItemIndex;
                //MobileClient_Classification[] list = services.GetProcessInfoByIndex(index);
                Repeater p = e.Item.FindControl("Repeater2") as Repeater;
                //p.DataSource = list;
                //p.DataBind();
            }
            catch (Exception ex)
            {
                PublicClass.ShowMessage(this.Page, Resources.Resource.NewTaskList_ErrorMessage2);
                PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

    }
}