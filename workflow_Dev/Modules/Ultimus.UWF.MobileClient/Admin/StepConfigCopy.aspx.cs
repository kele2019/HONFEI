using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace MobileClientBackground
{
    public partial class StepConfigCopy : System.Web.UI.Page
    {
        private DALLibrary.MobileClient_Step StepDAL = new DALLibrary.MobileClient_Step();

        private DALLibrary.MobileClient_StepControl StepControlDAL = new DALLibrary.MobileClient_StepControl();

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
                string ID = Request.QueryString["ID"].ToString().Trim();
                string ProcessName = Request.QueryString["ProcessName"].ToString().Trim();
                string StepName = Request.QueryString["StepName"].ToString().Trim();
                List<EntityLibrary.MobileClient_Step> StepList = StepDAL.GetModel("FK_ID='" + ID + "' and StepName!='" + StepName + "'");
                Repeater1.DataSource = StepList.ToArray();
                Repeater1.DataBind();
            }
            catch (Exception ex)
            {
                DALLibrary.PublicClass.ShowMessage(this.Page, Resources.Resource.ProcessStepList_List_ErrorMessage2);
                DALLibrary.PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string ID = Request.QueryString["ID"].ToString().Trim();
            string ProcessName = Request.QueryString["ProcessName"].ToString().Trim();
            Response.Redirect("ProcessStepList.aspx?ID=" + ID + "&ProcessName=" + Server.UrlEncode(ProcessName) + "");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string ID = Request.QueryString["ID"].ToString().Trim();
                string StepName = Request.QueryString["StepName"].ToString().Trim();

                EntityLibrary.MobileClient_Step StepMode = StepDAL.GetModel("FK_ID='" + ID + "' and StepName='" + StepName + "'")[0];
                List<EntityLibrary.MobileClient_StepControl> StepControlModel = StepControlDAL.GetModel("FK_ID='" + StepMode.ID + "'");

                foreach (RepeaterItem item in Repeater1.Items)
                {
                    HtmlInputCheckBox cb = item.FindControl("checkbox") as HtmlInputCheckBox;
                    if (cb.Checked)
                    {
                        int CopyToID = Convert.ToInt32(cb.Value);
                        StepControlDAL.DeleteList(CopyToID.ToString());
                        foreach (EntityLibrary.MobileClient_StepControl mode in StepControlModel)
                        {
                            mode.ID = 0;
                            mode.FK_ID = CopyToID;
                            StepControlDAL.Add(mode);
                        }
                    }
                }
                string ProcessID = Request.QueryString["ID"].ToString().Trim();
                string ProcessName = Request.QueryString["ProcessName"].ToString().Trim();
                Response.Redirect("ProcessStepList.aspx?ID=" + ProcessID + "&ProcessName=" + Server.UrlEncode(ProcessName) + "");
            }
            catch (Exception ex)
            {
                DALLibrary.PublicClass.ShowMessage(this.Page, "数据保存失败");
                DALLibrary.PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

    }
}