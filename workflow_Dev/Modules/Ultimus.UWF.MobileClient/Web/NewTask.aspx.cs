using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MobileClient.PublicFunctionClass;
using ClientService;

namespace MobileClient
{
    public partial class NewTask : BasePageClass.BasePage
    {
        private WorkflowRef services = new WorkflowRef();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BingCategory();
            }
        }

        private void BingCategory()
        {
            try
            {
                //MobileClient_Classification[] list = services.GetCategoryList();
                //Repeater1.DataSource = list;
                //Repeater1.DataBind();
            }
            catch (Exception ex)
            {
                PublicClass.ShowMessage(this.Page, Resources.Resource.NewTask_ErrorMessage1);
                PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string CategoryName = (e.Item.FindControl("CategoryName") as HiddenField).Value;
            //(e.Item.FindControl("Label1") as Label).Text = services.GetCategoryCountByCategoryName(CategoryName).ToString();
        }

    }
}