using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace MobileClientBackground
{
    public partial class ProcessClassification : System.Web.UI.Page
    {
        private DALLibrary.MobileClient_Classification ClassificationDAL = new DALLibrary.MobileClient_Classification();
        private DALLibrary.Initiate InitiateDAL = new DALLibrary.Initiate();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BingData();
            }
        }

        private void BingData()
        {
            List<EntityLibrary.MobileClient_Classification> list = ClassificationDAL.GetList("");
            ViewState["list"] = list;
            Repeater1.DataSource = list.ToArray();
            Repeater1.DataBind();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DropDownList dropProcessName = e.Item.FindControl("DropDownList1") as DropDownList;
            HiddenField hfprocessname = e.Item.FindControl("processname") as HiddenField;
            DropDownList dropIsAction = e.Item.FindControl("DropDownList2") as DropDownList;
            HiddenField hfisaction = e.Item.FindControl("isaction") as HiddenField;
            List<EntityLibrary.Initiate> list = InitiateDAL.GetList("");
            dropProcessName.DataSource = list.ToArray();
            dropProcessName.DataTextField = "ProcessName";
            dropProcessName.DataValueField = "ProcessName";
            dropProcessName.DataBind();
            dropProcessName.Items.Insert(0, new ListItem("", ""));
            dropProcessName.SelectedItem.Text = hfprocessname.Value;
            dropIsAction.SelectedValue = hfisaction.Value.Trim();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SaveData();
            List<EntityLibrary.MobileClient_Classification> list = ViewState["list"] as List<EntityLibrary.MobileClient_Classification>;
            EntityLibrary.MobileClient_Classification model = new EntityLibrary.MobileClient_Classification();
            list.Add(model);
            ViewState["list"] = list;
            Repeater1.DataSource = list.ToArray();
            Repeater1.DataBind();
        }

        private void SaveData()
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                EntityLibrary.MobileClient_Classification model = new EntityLibrary.MobileClient_Classification();
                model.ID = Convert.ToInt32((item.FindControl("checkbox") as HtmlInputCheckBox).Value);
                model.CategoryCName = (item.FindControl("TextBox1") as TextBox).Text.Trim();
                model.CategoryEName = (item.FindControl("TextBox2") as TextBox).Text.Trim();
                model.ProcessName = (item.FindControl("DropDownList1") as DropDownList).SelectedItem.Text;
                model.IsAction = (item.FindControl("DropDownList2") as DropDownList).SelectedValue;
                if (model.ID > 0)
                    ClassificationDAL.Update(model);
                else
                    ClassificationDAL.Add(model);
            }
            List<EntityLibrary.MobileClient_Classification> list = ClassificationDAL.GetList("");
            ViewState["list"] = list;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            List<EntityLibrary.MobileClient_Classification> list = ViewState["list"] as List<EntityLibrary.MobileClient_Classification>;
            foreach (RepeaterItem item in Repeater1.Items)
            {
                HtmlInputCheckBox cb = item.FindControl("checkbox") as HtmlInputCheckBox;
                if (cb.Checked)
                {
                    foreach (EntityLibrary.MobileClient_Classification model in list)
                    {
                        if (model.ID.ToString() == cb.Value)
                        {
                            list.Remove(model);
                            ClassificationDAL.Delete(model.ID);
                            break;
                        }
                    }
                }
            }
            ViewState["list"] = list;
            Repeater1.DataSource = list.ToArray();
            Repeater1.DataBind();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            SaveData();
        }

    }
}