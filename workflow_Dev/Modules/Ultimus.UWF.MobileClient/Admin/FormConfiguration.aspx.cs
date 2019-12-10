using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

namespace MobileClientBackground
{
    public partial class FormConfiguration : BasePage.BasePageClass
    {
        DALLibrary.MobileClient_Process Processdal = new DALLibrary.MobileClient_Process();

        DALLibrary.MobileClient_Step Stepdal = new DALLibrary.MobileClient_Step();

        DALLibrary.MobileClient_StepControl StepControldal = new DALLibrary.MobileClient_StepControl();

        DALLibrary.MobileClient_Control Controldal = new DALLibrary.MobileClient_Control();

        protected void Page_Load(object sender, EventArgs e)
        {
            ProcessID.Value = Request.QueryString["ID"].ToString().Trim();
            ProcessName.Value = Request.QueryString["ProcessName"].ToString().Trim();
            if (!IsPostBack)
            {
                BingData();
            }
        }

        private void BingData()
        {
            try
            {
                //主表ID
                string id = Request.QueryString["ID"].ToString().Trim();
                string ProcessName = Request.QueryString["ProcessName"].ToString().Trim();
                string StepName = Request.QueryString["StepName"].ToString().Trim();
                
                EntityLibrary.MobileClient_Process ProcessEntity = Processdal.GetModel(Convert.ToInt32(id));

                List<EntityLibrary.MobileClient_Step> list = Stepdal.GetModel("FK_ID='" + id + "'");

                List<EntityLibrary.MobileClient_Step> StepListEntity = list.FindAll(delegate(EntityLibrary.MobileClient_Step item)
                {
                    if (item.StepName == StepName)
                    {
                        return true;
                    }
                    return false;
                });

                if (StepListEntity.Count > 0)
                {
                    EntityLibrary.MobileClient_Step StepEntity = StepListEntity[0];

                    ViewState["StepEntity"] = StepEntity;

                    List<EntityLibrary.MobileClient_StepControl> StepControlList = StepControldal.GetModel("FK_ID='" + StepEntity.ID + "'");

                    Repeater1.DataSource = StepControlList.ToArray();
                    Repeater1.DataBind();

                    ViewState["StepControlList"] = StepControlList;
                }
            }
            catch (Exception ex)
            {
                DALLibrary.PublicClass.ShowMessage(this.Page, Resources.Resource.FormConfiguration_List_ErrorMessage1);
                DALLibrary.PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

        protected void AddControl_Click(object sender, EventArgs e)
        {
            try
            {
                SavePageData();

                List<EntityLibrary.MobileClient_StepControl> StepControlList;

                if (ViewState["StepID"] == null)
                {
                    EntityLibrary.MobileClient_Step step = Stepdal.GetModel("StepName='" + Request.QueryString["StepName"].ToString().Trim() + "' and FK_ID=" + Request.QueryString["ID"] + "")[0];
                    ViewState["StepID"] = step.ID;
                }

                EntityLibrary.MobileClient_StepControl item = new EntityLibrary.MobileClient_StepControl();
                item.FK_ID = Convert.ToInt32(ViewState["StepID"]);
                StepControldal.Add(item);

                StepControlList = StepControldal.GetModel("FK_ID='" + ViewState["StepID"].ToString().Trim() + "'");

                Repeater1.DataSource = StepControlList.ToArray();
                Repeater1.DataBind();

                ViewState["StepControlList"] = StepControlList;
            }
            catch (Exception ex)
            {
                DALLibrary.PublicClass.ShowMessage(this.Page, Resources.Resource.FormConfiguration_List_ErrorMessage2);
                DALLibrary.PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

        private void BingDropList(DropDownList drop)
        {
            List<EntityLibrary.MobileClient_Control> ControlList = Controldal.GetModel("IsAction='1'");
            drop.DataSource = ControlList.ToArray();
            string Language = Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"].ToString().Split(',')[0];
            if (Language == "zh-CN")
                drop.DataTextField = "ControlCName";
            else
                drop.DataTextField = "ControlEName";
            drop.DataValueField = "ID";
            drop.DataBind();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                DropDownList drop = e.Item.FindControl("ItemdropControl") as DropDownList;
                BingDropList(drop);
                string id = (e.Item.FindControl("ItemControlID") as HiddenField).Value;
                drop.SelectedValue = id;
            }
            catch (Exception ex)
            {
                DALLibrary.PublicClass.ShowMessage(this.Page, Resources.Resource.FormConfiguration_List_ErrorMessage4);
                DALLibrary.PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                SavePageData();
                if (ViewState["StepControlList"] != null)
                {
                    List<EntityLibrary.MobileClient_StepControl> StepControlList = ViewState["StepControlList"] as List<EntityLibrary.MobileClient_StepControl>;
                    foreach (RepeaterItem item in Repeater1.Items)
                    {
                        HtmlInputCheckBox cb = item.FindControl("ItemCheckBox") as HtmlInputCheckBox;
                        if (cb.Checked)
                        {
                            string id = cb.Value.Trim();
                            foreach (EntityLibrary.MobileClient_StepControl model in StepControlList)
                            {
                                if (model.ID.ToString().Trim() == id)
                                {
                                    StepControlList.Remove(model);
                                    StepControldal.Delete(model.ID);
                                    break;
                                }
                            }
                        }
                    }
                    ViewState["StepControlList"] = StepControlList;
                    Repeater1.DataSource = StepControlList.ToArray();
                    Repeater1.DataBind();
                }
            }
            catch (Exception ex)
            {
                DALLibrary.PublicClass.ShowMessage(this.Page, Resources.Resource.FormConfiguration_List_ErrorMessage3);
                DALLibrary.PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

        private void SavePageData()
        {
            List<EntityLibrary.MobileClient_StepControl> StepControlList = new List<EntityLibrary.MobileClient_StepControl>();
            foreach (RepeaterItem item in Repeater1.Items)
            {
                EntityLibrary.MobileClient_StepControl model = new EntityLibrary.MobileClient_StepControl();
                model.ID = Convert.ToInt32((item.FindControl("ItemCheckBox") as HtmlInputCheckBox).Value);
                model = StepControldal.GetModel(model.ID);
                model.FK_ID = Convert.ToInt32((item.FindControl("FK_ID") as HiddenField).Value);
                model.ColumnName = (item.FindControl("ItemColumnName") as TextBox).Text;
                model.ControlID = Convert.ToInt32((item.FindControl("ItemdropControl") as DropDownList).SelectedValue);
                model.Format = (item.FindControl("ItemFormat") as TextBox).Text.Replace("\r\n","").Replace("\n","");
                model.ExternalLinks = (item.FindControl("ItemExternalLinks") as TextBox).Text.Replace("\r\n", "").Replace("\n", "");
                model.IsWillFill = (item.FindControl("ItemIsWillFill") as CheckBox).Checked.ToString();
                model.ReadOnly = (item.FindControl("ItemReadOnly") as CheckBox).Checked.ToString();
                model.IsShow = (item.FindControl("ItemIsShow") as TextBox).Text;
                model.OrderBy = Convert.ToDecimal((item.FindControl("ItemOrderBy") as TextBox).Text);
                StepControlList.Add(model);
                if (model.ID == 0)
                    StepControldal.Add(model);
                else
                    StepControldal.Update(model);
            }
            ViewState["StepControlList"] = StepControlList;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            SavePageData();
            List<EntityLibrary.MobileClient_StepControl> StepControlList = ViewState["StepControlList"] as List<EntityLibrary.MobileClient_StepControl>;
            bool flag = true;
            foreach (EntityLibrary.MobileClient_StepControl item in StepControlList)
            {
                if (item.ID > 0)
                {
                    flag = StepControldal.Update(item);
                }
                else
                {
                    flag = StepControldal.Add(item) > 0 ? true : false;
                }
                if (!flag)
                {
                    DALLibrary.PublicClass.ShowMessage(this.Page, Resources.Resource.FormConfiguration_List_ErrorMessage5);
                    DALLibrary.PublicClass.WriteLogOfTxt("保存数据出错");
                    break;
                }
            }
            if (flag)
            {
                string ID = Request.QueryString["ID"].ToString().Trim();
                string ProcessName = Request.QueryString["ProcessName"].ToString().Trim();
                Response.Redirect("ProcessStepList.aspx?ID=" + ID + "&ProcessName=" + Server.UrlEncode(ProcessName) + "");
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "OpenConfigForm")
            {
                SavePageData();
                List<EntityLibrary.MobileClient_StepControl> StepControlList = ViewState["StepControlList"] as List<EntityLibrary.MobileClient_StepControl>;
                bool flag = true;
                foreach (EntityLibrary.MobileClient_StepControl item in StepControlList)
                {
                    if (item.ID > 0)
                    {
                        flag = StepControldal.Update(item);
                    }
                    else
                    {
                        flag = StepControldal.Add(item) > 0 ? true : false;
                    }
                    if (!flag)
                    {
                        DALLibrary.PublicClass.ShowMessage(this.Page, Resources.Resource.FormConfiguration_List_ErrorMessage5);
                        DALLibrary.PublicClass.WriteLogOfTxt("保存数据出错");
                        break;
                    }
                }
                if (flag)
                {
                    string ColumnID = e.CommandArgument.ToString().Trim();
                    string ProcessName = Request.QueryString["ProcessName"].ToString().Trim();
                    string StepName = Request.QueryString["StepName"].ToString().Trim();
                    string ColumnName = (e.Item.FindControl("ItemColumnName") as TextBox).Text;
                    string ProcessID = Request.QueryString["ID"].ToString().Trim();
                    Response.Redirect("ControlSourceConfig.aspx?ProcessName=" + Server.UrlEncode(ProcessName) + "&StepName=" + Server.UrlEncode(StepName) + "&ColumnName=" + Server.UrlEncode(ColumnName) + "&ColumnID=" + ColumnID + "&ProcessID=" + ProcessID + "");
                }
            }
        }

    }
}