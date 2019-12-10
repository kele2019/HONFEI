using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MobileClient.PublicFunctionClass;
using MobileClient.MobileClientBackgroundRef;

namespace MobileClient
{
    public partial class FlowChart : BasePageClass.BasePage
    {
        private Ultimus_MobileServices services = new Ultimus_MobileServices();

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
                //UserInfo uUserInfo = Session["UserInfo"] as UserInfo;
                //List<Tasks> info = new List<Tasks>();
                //info.AddRange(services.GetMyTaskListOfUltimusEik(uUserInfo.UserAccount));
                //List<Tasks> list = new List<Tasks>();
                //int index = AspNetPager1.CurrentPageIndex;
                //int pageIndex = AspNetPager1.CurrentPageIndex - 1;
                //int pageSize = AspNetPager1.PageSize;
                //for (int i = 0; i < info.Count; i++)
                //{
                //    if (i >= pageIndex * pageSize && i < index * pageSize)
                //    {
                //        list.Add(info[i]);
                //    }
                //}
                //Repeater1.DataSource = list.ToArray();
                //Repeater1.DataBind();
                //AspNetPager1.RecordCount = info.Count;
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