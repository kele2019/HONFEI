using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MobileClient.PublicFunctionClass;
//using MobileClient.MobileClientBackgroundRef;
using System.Data;
using MyLib;
using ClientService;

namespace MobileClient
{
    public partial class ApprovalHistory : BasePageClass.BasePage
    {
        private WorkflowRef services = new WorkflowRef();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BingMyTask();
            }
        }

        private void BingMyTask()
        {
            try
            {
                //MobileClient.MobileClientBackgroundRef.Ultimus_MobileServices srv = new MobileClient.MobileClientBackgroundRef.Ultimus_MobileServices();
                DataTable ds = services.GetApprovalHistoryByProc(Request.QueryString["ProcessName"], ConvertUtil.ToInt32(Request.QueryString["Incident"]));
                if (ds!=null)
                {
                    Repeater1.DataSource = ds;
                    Repeater1.DataBind();
                }
            }
            catch (Exception ex)
            {
                PublicClass.ShowMessage(this.Page, Resources.Resource.ToDoTask_ErrorMessage1);
                PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BingMyTask();
        }

    }
}