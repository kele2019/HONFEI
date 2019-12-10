using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ClientService;

namespace MobileClientBackground
{
    public partial class DataSaveConfig : System.Web.UI.Page
    {
        private DALLibrary.MobileClient_StepControl StepControlDAL = new DALLibrary.MobileClient_StepControl();
        private DALLibrary.MobileClient_Step StepDAL = new DALLibrary.MobileClient_Step();
        private DALLibrary.DataBase DataBaseDAL = new DALLibrary.DataBase();
        //private UltimusEikLibrary.UltimusEikOfIncident UltimusEikOfIncidentDAL = new UltimusEikLibrary.UltimusEikOfIncident();
        WorkflowRef _ref = new WorkflowRef();
        private bool IsBeginMaster = true;
        private bool IsBeginSublist = true;
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
                string StepName = Request.QueryString["StepName"].ToString().Trim();
                string id = Request.QueryString["ID"].ToString().Trim();

                EntityLibrary.MobileClient_Step StepModel = StepDAL.GetModel("FK_ID='" + id + "' and StepName='" + StepName + "'")[0];
                List<EntityLibrary.MobileClient_StepControl> List = StepControlDAL.GetModel("FK_ID='" + StepModel.ID + "'");

                Repeater1.DataSource = List.ToArray();
                Repeater1.DataBind();

                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("ConnectionString", typeof(string));
                foreach (System.Configuration.ConnectionStringSettings css in System.Configuration.ConfigurationManager.ConnectionStrings)
                {
                    if (css.Name != "LocalSqlServer")
                    {
                        dt.Rows.Add(dt.NewRow());
                        dt.Rows[dt.Rows.Count - 1]["Name"] = css.Name.Trim();
                        dt.Rows[dt.Rows.Count - 1]["ConnectionString"] = css.ConnectionString.Trim();
                    }
                }
                _dropMasterConnectionString.DataSource = dt;
                _dropMasterConnectionString.DataTextField = "Name";
                _dropMasterConnectionString.DataValueField = "Name";
                _dropMasterConnectionString.DataBind();

                _dropSublistConnectionString.DataSource = dt;
                _dropSublistConnectionString.DataTextField = "Name";
                _dropSublistConnectionString.DataValueField = "Name";
                _dropSublistConnectionString.DataBind();

                string name = _dropMasterConnectionString.Items[0].Value;
                string str = System.Configuration.ConfigurationManager.ConnectionStrings[name].ConnectionString.Trim();
                _dropMasterTableName.DataSource = DataBaseDAL.GetAllTableName(str).ToArray();
                _dropMasterTableName.DataBind();
            }
            catch (Exception ex)
            {
                DALLibrary.PublicClass.ShowMessage(this.Page, Resources.Resource.FormConfiguration_List_ErrorMessage1);
                DALLibrary.PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                string id = (e.Item.FindControl("ItemID") as HiddenField).Value.Trim();

                EntityLibrary.MobileClient_StepControl model = StepControlDAL.GetModel(Convert.ToInt32(id));

                Label ColumnName = e.Item.FindControl("lbColumnName") as Label;
                CheckBox SaveToDataBase = e.Item.FindControl("cbSaveToDataBase") as CheckBox;
                CheckBox SaveToElectronicForm = e.Item.FindControl("cbSaveToElectronicForm") as CheckBox;
                RadioButton Master = e.Item.FindControl("radioMaster") as RadioButton;
                RadioButton Sublist = e.Item.FindControl("radioSublist") as RadioButton;
                DropDownList TableName = e.Item.FindControl("dropTableName") as DropDownList;
                DropDownList FieldName = e.Item.FindControl("dropFieldName") as DropDownList;
                DropDownList VariableName = e.Item.FindControl("dropVariableName") as DropDownList;

                ColumnName.Text = model.ColumnName;
                if (!String.IsNullOrEmpty(model.DestType))
                {
                    if (model.DestType.IndexOf("DataBase") >= 0)
                    {
                        SaveToDataBase.Checked = true;
                        if (!String.IsNullOrEmpty(model.IsMasterTable) && model.IsMasterTable.ToLower() == "true")
                        {
                            if (IsBeginMaster)
                            {
                                _dropMasterConnectionString.SelectedValue = model.DestConnectionString;
                                _dropMasterTableName.SelectedValue = model.DestTableName;
                                IsBeginMaster = false;
                            }
                            Master.Checked = true;
                        }
                        else if (!String.IsNullOrEmpty(model.IsSublist) && model.IsSublist.ToLower() == "true")
                        {
                            if (IsBeginSublist)
                            {
                                _dropSublistConnectionString.SelectedValue = model.DestConnectionString;
                                IsBeginSublist = false;
                            }
                            Sublist.Checked = true;
                        }
                        string _ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[model.DestConnectionString].ConnectionString.Trim();
                        TableName.DataSource = DataBaseDAL.GetAllTableName(_ConnectionString);
                        TableName.DataBind();
                        //TableName.Items.Insert(0, new ListItem("", ""));

                        TableName.SelectedValue = model.DestTableName;

                        FieldName.DataSource = DataBaseDAL.GetTableFieldName(_ConnectionString, model.DestTableName);
                        FieldName.DataBind();
                        //FieldName.Items.Insert(0, new ListItem("", ""));

                        FieldName.SelectedValue = model.DestColumnName;
                    }
                    if (model.DestType.IndexOf("ElectronicForm") >= 0)
                    {
                        SaveToElectronicForm.Checked = true;
                        string ProcessName = Request.QueryString["ProcessName"].ToString().Trim();
                        VariableName.DataSource = _ref.GetVariableList("", ProcessName);// UltimusEikOfIncidentDAL.GetVariable(ProcessName);
                        VariableName.DataTextField = "Name";
                        VariableName.DataValueField = "Name";
                        VariableName.DataBind();
                        //VariableName.Items.Insert(0, new ListItem("", ""));
                        VariableName.SelectedValue = model.DestVariableName;
                    }
                }
                else
                {
                    Master.Checked = Master.Enabled = false;
                    Sublist.Checked = Sublist.Enabled = false;
                    TableName.Enabled = FieldName.Enabled = VariableName.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                DALLibrary.PublicClass.ShowMessage(this.Page, Resources.Resource.FormConfiguration_List_ErrorMessage1);
                DALLibrary.PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

        protected void _dropMasterConnectionString_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = _dropMasterConnectionString.SelectedValue.Trim();
            string str = System.Configuration.ConfigurationManager.ConnectionStrings[name].ConnectionString.Trim();
            _dropMasterTableName.DataSource = DataBaseDAL.GetAllTableName(str).ToArray();
            _dropMasterTableName.DataBind();
        }

        protected void dropTableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList _dropTableName = sender as DropDownList;
            DropDownList _dropFieldName = _dropTableName.Parent.FindControl("dropFieldName") as DropDownList;
            RadioButton Master = _dropTableName.Parent.FindControl("radioMaster") as RadioButton;
            string name = Master.Checked ? _dropMasterConnectionString.SelectedValue.Trim() : _dropSublistConnectionString.SelectedValue.Trim();
            string _ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[name].ConnectionString.Trim();
            _dropFieldName.DataSource = DataBaseDAL.GetTableFieldName(_ConnectionString, _dropTableName.SelectedValue);
            _dropFieldName.DataBind();
            //_dropFieldName.Items.Insert(0, new ListItem("", ""));
        }

        protected void cbSaveToDataBase_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            CheckBox cb1 = cb.Parent.FindControl("cbSaveToElectronicForm") as CheckBox;
            RadioButton Master = cb.Parent.FindControl("radioMaster") as RadioButton;
            RadioButton Sublist = cb.Parent.FindControl("radioSublist") as RadioButton;
            DropDownList _dropTableName = cb.Parent.FindControl("dropTableName") as DropDownList;
            DropDownList _dropFieldName = cb.Parent.FindControl("dropFieldName") as DropDownList;
            DropDownList _dropVariableName = cb.Parent.FindControl("dropVariableName") as DropDownList;
            Master.Enabled = Sublist.Enabled = _dropTableName.Enabled = _dropFieldName.Enabled = cb.Checked;
            if (cb.Checked)
            {
                if (!Sublist.Checked)
                {
                    bool isCheck = cb.Checked && !cb1.Checked ? false : cb1.Checked ? true : false;
                    _dropVariableName.Enabled = isCheck;
                    Master.Checked = !isCheck;
                    if (!isCheck)
                    {
                        _dropVariableName.Items.Clear();
                    }
                }

                string _ConnectionName = _dropMasterConnectionString.SelectedValue;
                string _ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[_ConnectionName].ConnectionString.Trim();
                _dropTableName.DataSource = DataBaseDAL.GetAllTableName(_ConnectionString);
                _dropTableName.DataBind();
                _dropTableName.SelectedValue = _dropMasterTableName.SelectedValue;
                _dropFieldName.DataSource = DataBaseDAL.GetTableFieldName(_ConnectionString, _dropMasterTableName.SelectedValue);
                _dropFieldName.DataBind();
            }
            else
            {
                _dropTableName.Items.Clear();
                _dropFieldName.Items.Clear();
            }
        }

        protected void cbSaveToElectronicForm_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            CheckBox cb1 = cb.Parent.FindControl("cbSaveToDataBase") as CheckBox;
            RadioButton Master = cb.Parent.FindControl("radioMaster") as RadioButton;
            RadioButton Sublist = cb.Parent.FindControl("radioSublist") as RadioButton;
            DropDownList _dropTableName = cb.Parent.FindControl("dropTableName") as DropDownList;
            DropDownList _dropFieldName = cb.Parent.FindControl("dropFieldName") as DropDownList;
            DropDownList _dropVariableName = cb.Parent.FindControl("dropVariableName") as DropDownList;
            if (!cb1.Checked)
            {
                Master.Enabled = Sublist.Enabled = _dropTableName.Enabled = _dropFieldName.Enabled = false;
                _dropTableName.SelectedValue = _dropFieldName.SelectedValue = "";
            }
            _dropVariableName.Enabled = cb.Checked;
            _dropVariableName.DataSource = _ref.GetVariableList("",Request.QueryString["ProcessName"].ToString().Trim());
            _dropVariableName.DataTextField = "Name";
            _dropVariableName.DataValueField = "Name";
            _dropVariableName.DataBind();
            if (!cb.Checked)
            {
                _dropVariableName.Items.Clear();
            }
        }


        protected void radioMaster_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _radioMaster=sender as RadioButton;
            string _ConnectionName = _dropMasterConnectionString.SelectedValue;
            string _ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[_ConnectionName].ConnectionString.Trim();
            DropDownList _dropTableName = _radioMaster.Parent.FindControl("dropTableName") as DropDownList;
            _dropTableName.DataSource = DataBaseDAL.GetAllTableName(_ConnectionString);
            _dropTableName.DataBind();
            //_dropTableName.Items.Insert(0, new ListItem("", ""));
            _dropTableName.SelectedValue = _dropMasterTableName.SelectedValue;
            if (!String.IsNullOrEmpty(_dropTableName.SelectedValue))
            {
                DropDownList _dropFieldName = _radioMaster.Parent.FindControl("dropFieldName") as DropDownList;
                _dropFieldName.DataSource = DataBaseDAL.GetTableFieldName(_ConnectionString, _dropTableName.SelectedValue);
                _dropFieldName.DataBind();
                //_dropFieldName.Items.Insert(0, new ListItem("", ""));
            }
        }

        protected void radioSublist_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _radioSublist = sender as RadioButton;
            string _ConnectionName = _dropSublistConnectionString.SelectedValue;
            string _ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[_ConnectionName].ConnectionString.Trim();
            DropDownList _dropTableName = _radioSublist.Parent.FindControl("dropTableName") as DropDownList;
            _dropTableName.DataSource = DataBaseDAL.GetAllTableName(_ConnectionString);
            _dropTableName.DataBind();
            //_dropTableName.Items.Insert(0, new ListItem("", ""));
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                string ProcessName = Request.QueryString["ProcessName"].ToString().Trim();
                string StepName = Request.QueryString["StepName"].ToString().Trim();
                string id = Request.QueryString["ID"].ToString().Trim();
                EntityLibrary.MobileClient_Step StepModel = StepDAL.GetModel("FK_ID='" + id + "' and StepName='" + StepName + "'")[0];
                List<EntityLibrary.MobileClient_StepControl> List = StepControlDAL.GetModel("FK_ID='" + StepModel.ID + "'");
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    string _ColumnName = (item.FindControl("lbColumnName") as Label).Text;
                    string _DestType="";
                    foreach (EntityLibrary.MobileClient_StepControl model in List)
                    {
                        if (model.ColumnName == _ColumnName)
                        {
                            CheckBox _SaveToDataBase = item.FindControl("cbSaveToDataBase") as CheckBox;
                            CheckBox _SaveToElectronicForm = item.FindControl("cbSaveToElectronicForm") as CheckBox;
                            RadioButton _radioMaster = item.FindControl("radioMaster") as RadioButton;
                            RadioButton _radioSublist = item.FindControl("radioSublist") as RadioButton;
                            DropDownList _dropTableName=item.FindControl("dropTableName") as DropDownList;
                            DropDownList _dropFieldName = item.FindControl("dropFieldName") as DropDownList;
                            DropDownList _dropVariableName = item.FindControl("dropVariableName") as DropDownList;
                            if (_SaveToDataBase.Checked)
                            {
                                _DestType += "DataBase,";
                            }
                            if (_SaveToElectronicForm.Checked)
                            {
                                _DestType += "ElectronicForm,";
                            }
                            model.IsMasterTable = _radioMaster.Checked.ToString();
                            model.IsSublist = _radioSublist.Checked.ToString();
                            model.DestType = _DestType.IndexOf(",") > 0 ? _DestType.TrimEnd(',') : _DestType;
                            model.DestConnectionString = _radioMaster.Checked ? _dropMasterConnectionString.SelectedValue : _radioSublist.Checked ? _dropSublistConnectionString.SelectedValue : null;
                            model.DestTableName = String.IsNullOrEmpty(_dropTableName.SelectedValue) ? null : _dropTableName.SelectedValue.Trim();
                            model.DestColumnName = String.IsNullOrEmpty(_dropFieldName.SelectedValue) ? null : _dropFieldName.SelectedValue.Trim();
                            model.DestVariableName = String.IsNullOrEmpty(_dropVariableName.SelectedValue) ? null : _dropVariableName.SelectedValue.Trim();
                            StepControlDAL.Update(model);
                            break;
                        }
                    }
                }
                Response.Redirect("ProcessStepList.aspx?ID=" + id + "&ProcessName=" + Server.UrlEncode(ProcessName) + "");
            }
            catch (Exception ex)
            {
                DALLibrary.PublicClass.ShowMessage(this.Page, Resources.Resource.ProcessStepList_List_ErrorMessage1);
                DALLibrary.PublicClass.WriteLogOfTxt(ex.Message);
            }
        }

    }
}