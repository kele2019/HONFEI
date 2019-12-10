using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;

namespace MobileClientBackground
{
    public partial class ProcessStepList : BasePage.BasePageClass
    {
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
                string ProcessName = Request.QueryString["ProcessName"].ToString().Trim();
                string id = Request.QueryString["ID"].ToString().Trim();

                DALLibrary.ProcessStep step = new DALLibrary.ProcessStep();

                List<EntityLibrary.ProcessStep> list = step.GetList("PROCESSNAME='" + ProcessName + "' and PROCESSVERSION=(select max(PROCESSVERSION) from PROCESSSTEPS where PROCESSNAME='" + ProcessName + "') and steptype in (2,4)");

                Repeater1.DataSource = list.ToArray();
                Repeater1.DataBind();
            }
            catch (Exception ex)
            {
                DALLibrary.PublicClass.ShowMessage(this.Page, Resources.Resource.ProcessStepList_List_ErrorMessage2);
                DALLibrary.PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "OpenFormOperation" || e.CommandName.Trim() == "OpenConfigDataSave")
            {
                string ProcessName = Request.QueryString["ProcessName"].ToString().Trim();
                string ID = Request.QueryString["ID"].ToString().Trim();
                string StepName = e.CommandArgument.ToString().Trim();
                DALLibrary.MobileClient_Step stepdal = new DALLibrary.MobileClient_Step();
                List<EntityLibrary.MobileClient_Step> list = stepdal.GetModel("FK_ID='" + ID + "' and StepName='" + StepName + "'");
                EntityLibrary.MobileClient_Step step = new EntityLibrary.MobileClient_Step();
                if (ID == "0")
                {
                    DALLibrary.MobileClient_Process processdal = new DALLibrary.MobileClient_Process();
                    EntityLibrary.MobileClient_Process process = new EntityLibrary.MobileClient_Process();
                    process.ProcessName = ProcessName;
                    process.CreateTime = DateTime.Now;
                    step.FK_ID = processdal.Add(process);
                }
                else
                {
                    step.FK_ID = Convert.ToInt32(ID);
                }
                step.StepName = StepName;
                step.StepCName = (e.Item.FindControl("txtStepCName") as TextBox).Text;
                step.StepEName = (e.Item.FindControl("txtStepEName") as TextBox).Text;
                bool flag = true;
                if (list.Count > 0)
                {
                    step.ID = list[0].ID;
                    if (!stepdal.Update(step))
                    {
                        flag = false;
                    }
                }
                else
                {
                    int result = stepdal.Add(step);
                    if (result < 1)
                    {
                        flag = false;
                    }
                    else
                    {
                        ID = result.ToString();
                    }
                }
                if (flag && e.CommandName == "OpenFormOperation")
                    Response.Redirect("FormConfiguration.aspx?ID=" + ID + "&ProcessName=" + Server.UrlEncode(ProcessName) + "&StepName=" + Server.UrlEncode(StepName) + "");
                else if (flag && e.CommandName == "OpenConfigDataSave")
                    Response.Redirect("DataSaveConfig.aspx?ID=" + ID + "&ProcessName=" + Server.UrlEncode(ProcessName) + "&StepName=" + Server.UrlEncode(StepName) + "");
                else
                    DALLibrary.PublicClass.ShowMessage(this.Page, Resources.Resource.ProcessStepList_List_ErrorMessage1);
            }
            if (e.CommandName == "OpenCopy")
            {
                string ProcessName = Request.QueryString["ProcessName"].ToString().Trim();
                string ID = Request.QueryString["ID"].ToString().Trim();
                string StepName = e.CommandArgument.ToString().Trim();
                Response.Redirect("StepConfigCopy.aspx?ID=" + ID + "&ProcessName=" + Server.UrlEncode(ProcessName) + "&StepName=" + Server.UrlEncode(StepName) + "");
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string StepName = (e.Item.FindControl("lbStepName") as Label).Text.Trim();
            try
            {
                string id = Request.QueryString["ID"].ToString().Trim();
                DALLibrary.MobileClient_Step stepdal = new DALLibrary.MobileClient_Step();
                List<EntityLibrary.MobileClient_Step> StepList = stepdal.GetModel("FK_ID='" + id + "' and StepName='" + StepName + "'");
                if (StepList.Count > 0)
                {
                    EntityLibrary.MobileClient_Step step = StepList[0];
                    (e.Item.FindControl("txtStepCName") as TextBox).Text = step.StepCName;
                    (e.Item.FindControl("txtStepEName") as TextBox).Text = step.StepEName;
                }
            }
            catch (Exception ex)
            {
                DALLibrary.PublicClass.ShowMessage(this.Page, Resources.Resource.ProcessStepList_List_ErrorMessage3.Replace("{0}", StepName));
                DALLibrary.PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                string ProcessName = Request.QueryString["ProcessName"].ToString().Trim();
                string id = Request.QueryString["ID"].ToString().Trim();
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    string StepName = (item.FindControl("lbStepName") as Label).Text.Trim();
                    DALLibrary.MobileClient_Step stepdal = new DALLibrary.MobileClient_Step();
                    List<EntityLibrary.MobileClient_Step> list = stepdal.GetModel("FK_ID='" + id + "' and StepName='" + StepName + "'");

                    EntityLibrary.MobileClient_Step model = new EntityLibrary.MobileClient_Step();

                    model.FK_ID = Convert.ToInt32(id);
                    model.StepName = StepName;
                    model.StepCName = (item.FindControl("txtStepCName") as TextBox).Text.Trim();
                    model.StepEName = (item.FindControl("txtStepEName") as TextBox).Text.Trim();

                    bool flag = true;

                    if (list.Count > 0)
                    {
                        model.ID = list[0].ID;
                        flag = stepdal.Update(model);
                    }
                    else
                    {
                        flag = stepdal.Add(model) > 0 ? true : false;
                    }
                    if (!flag)
                    {
                        throw new Exception(StepName + "数据保存失败");
                    }
                }
                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                DALLibrary.PublicClass.ShowMessage(this.Page, Resources.Resource.ProcessStepList_List_ErrorMessage1);
                DALLibrary.PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

    }
}