using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data;
using ClientService;
using ClientService.WorkflowSrv;

namespace MobileClientBackground
{
    public partial class ControlSourceConfig : BasePage.BasePageClass
    {
        private DALLibrary.MobileClient_StepControl ControlSourcedal = new DALLibrary.MobileClient_StepControl();
        private DALLibrary.DataBase DataBasedal = new DALLibrary.DataBase();
        //private UltimusEikLibrary.UltimusEikOfIncident EikOfIncidentdal = new UltimusEikLibrary.UltimusEikOfIncident();
        WorkflowRef _ref = new WorkflowRef();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ProcessName"] != null)
                {
                    lbProcessName.Text = Request.QueryString["ProcessName"].ToString().Trim();
                }
                if (Request.QueryString["StepName"] != null)
                {
                    lbStepName.Text = Request.QueryString["StepName"].ToString().Trim();
                }
                if (Request.QueryString["ColumnName"] != null)
                {
                    lbColumnName.Text = Request.QueryString["ColumnName"].ToString().Trim();
                }
                BingData();
            }
        }

        private void BingData()
        {
            try
            {
                string ColumnID = "";
                if (Request.QueryString["ColumnID"] != null)
                {
                    ColumnID = Request.QueryString["ColumnID"].ToString().Trim();
                }

                List<EntityLibrary.MobileClient_StepControl> list = ControlSourcedal.GetModel("ID='" + ColumnID + "'");

                EntityLibrary.MobileClient_StepControl model = new EntityLibrary.MobileClient_StepControl();

                if (cbDataBase.Checked)
                {
                    BingDataBaseData();
                }
                else if (cbElectronicForm.Checked)
                {
                    BingElectronicFormData();   
                }

                if (list.Count > 0)
                {
                    model = list[0];
                    tableID.Value = model.ID.ToString();
                    string _connectionString="";

                    if (!String.IsNullOrEmpty(model.SourceConnectionString))
                    {
                        dropConnectionString.SelectedValue = model.SourceConnectionString;
                        _connectionString = ConfigurationManager.ConnectionStrings[model.SourceConnectionString].ToString().Trim();

                        List<string> item1 = new List<string>();
                        item1 = DataBasedal.GetAllTableName(_connectionString);
                        dropTableName.DataSource = item1.ToArray();
                        dropTableName.DataBind();
                        dropTableName.Items.Insert(0, new ListItem("", ""));
                        dropTableName.SelectedValue = model.SourceTableName;

                        List<string> item2 = new List<string>();
                        item2 = DataBasedal.GetTableFieldName(_connectionString, model.SourceTableName);
                        dropColumnName.DataSource = item2.ToArray();
                        dropColumnName.DataBind();
                        dropColumnName.Items.Insert(0, new ListItem("", ""));
                        dropColumnName.SelectedValue = model.SourceColumnName;

                        txtSearchWhere.Text = model.SourceWhere;
                    }

                    string ProcessName = "";
                    if (Request.QueryString["ProcessName"] != null)
                    {
                        ProcessName = Request.QueryString["ProcessName"].ToString().Trim();
                    }
                    //Ultimus.WFServer.Variable[] Variables = EikOfIncidentdal.GetVariable(ProcessName);

                    VarEntity[] Variables = _ref.GetVariableList("", ProcessName);
                    dropVariableName.DataSource = Variables;
                    dropVariableName.DataTextField = "Name";
                    dropVariableName.DataValueField = "Name";
                    dropVariableName.DataBind();
                    dropVariableName.Items.Insert(0, new ListItem("", ""));
                    dropVariableName.SelectedValue = model.SourceVariableName;

                    if (model.SourceType == "ElectronicForm")
                    {
                        DataBase.Visible = false;
                        ElectronicForm.Visible = true;
                        cbElectronicForm.Checked = true;
                        cbDataBase.Checked = false;
                    }
                }
                
            }
            catch (Exception ex)
            {
                DALLibrary.PublicClass.ShowMessage(this.Page, Resources.Resource.ControlSourceConfig_ErrorMessage1);
                DALLibrary.PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

        protected void cbDataBase_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cbElectronicForm.Checked = false;
                BingDataBaseData();
            }
            catch (Exception ex)
            {
                DALLibrary.PublicClass.ShowMessage(this.Page, Resources.Resource.ControlSourceConfig_ErrorMessage1);
                DALLibrary.PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

        protected void cbElectronicForm_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cbDataBase.Checked = false;
                BingElectronicFormData();
            }
            catch (Exception ex)
            {
                DALLibrary.PublicClass.ShowMessage(this.Page, Resources.Resource.ControlSourceConfig_ErrorMessage1);
                DALLibrary.PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

        private void BingDataBaseData()
        {
            DataBase.Visible = true;
            ElectronicForm.Visible = false;
            DataTable dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("ConnectionString", typeof(string));
            foreach (ConnectionStringSettings css in ConfigurationManager.ConnectionStrings)
            {
                dt.Rows.Add(dt.NewRow());
                dt.Rows[dt.Rows.Count - 1]["Name"] = css.Name.Trim();
                dt.Rows[dt.Rows.Count - 1]["ConnectionString"] = css.ConnectionString.Trim();
            }
            dropConnectionString.DataSource = dt;
            dropConnectionString.DataTextField = "Name";
            dropConnectionString.DataValueField = "Name";
            dropConnectionString.DataBind();
            dropConnectionString.Items.Insert(0, new ListItem("", ""));
        }

        private void BingElectronicFormData()
        {
            DataBase.Visible = false;
            ElectronicForm.Visible = true;
            string ProcessName = "";
            if (Request.QueryString["ProcessName"] != null)
            {
                ProcessName = Request.QueryString["ProcessName"].ToString().Trim();
            }
            //Ultimus.WFServer.Variable[] Variables = EikOfIncidentdal.GetVariable(ProcessName);
            //dropVariableName.DataSource = Variables;
            //dropVariableName.DataTextField = "strVariableName";
            //dropVariableName.DataValueField = "strVariableName";
            //dropVariableName.DataBind();

            VarEntity[] Variables = _ref.GetVariableList("", ProcessName);
            dropVariableName.DataSource = Variables;
            dropVariableName.DataTextField = "Name";
            dropVariableName.DataValueField = "Name";
            dropVariableName.DataBind();

        }

        protected void dropConnectionString_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropConnectionString.SelectedItem.Text.Trim() != "")
            {
                string connectionStringName = dropConnectionString.SelectedValue.Trim();
                string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ToString().Trim();
                List<string> list = new List<string>();
                list = DataBasedal.GetAllTableName(connectionString);
                dropTableName.DataSource = list.ToArray();
                dropTableName.DataBind();
                dropTableName.Items.Insert(0, new ListItem("", ""));
            }
        }

        protected void dropTableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropTableName.SelectedItem.Text.Trim() != "")
            {
                string connectionStringName = dropConnectionString.SelectedValue.Trim();
                string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ToString().Trim();
                string tableName = dropTableName.SelectedValue.Trim();
                List<string> list = new List<string>();
                list = DataBasedal.GetTableFieldName(connectionString, tableName);
                dropColumnName.DataSource = list.ToArray();
                dropColumnName.DataBind();
                dropColumnName.Items.Insert(0, new ListItem("", ""));
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                EntityLibrary.MobileClient_StepControl model = ControlSourcedal.GetModel(Convert.ToInt32(tableID.Value));
                model.SourceType = cbDataBase.Checked ? "DataBase" : "ElectronicForm";
                model.SourceConnectionString = dropConnectionString.SelectedValue.Trim();
                model.SourceTableName = dropTableName.SelectedValue.Trim();
                model.SourceColumnName = dropColumnName.SelectedValue.Trim();
                model.SourceWhere = txtSearchWhere.Text.Trim();
                model.SourceVariableName = dropVariableName.SelectedValue.Trim();

                string ProcessID = "";string ProcessName = "";string StepName = "";
                if (Request.QueryString["ProcessID"] != null)
                {
                    ProcessID = Request.QueryString["ProcessID"].ToString().Trim();
                }
                if (Request.QueryString["ProcessName"] != null)
                {
                    ProcessName = Request.QueryString["ProcessName"].ToString().Trim();
                }
                if (Request.QueryString["StepName"] != null)
                {
                    StepName = Request.QueryString["StepName"].ToString().Trim();
                }

                if (ControlSourcedal.Update(model))
                {
                    Response.Redirect("FormConfiguration.aspx?ID=" + ProcessID + "&ProcessName=" + Server.UrlEncode(ProcessName) + "&StepName=" + Server.UrlEncode(StepName) + "");
                }
                else
                {
                    throw new Exception(Resources.Resource.ControlSourceConfig_ErrorMessage2);
                }
            }
            catch (Exception ex)
            {
                DALLibrary.PublicClass.ShowMessage(this.Page, Resources.Resource.ControlSourceConfig_ErrorMessage2);
                DALLibrary.PublicClass.WriteLogOfTxt(ex.Message);
            }
        }
    }
}