﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using MyLib;
using System.Collections;
using Ultimus.UWF.Workflow.Logic;
using Ultimus.UWF.OrgChart.Interface;
using Ultimus.UWF.OrgChart.Entity;
using Ultimus.UWF.Common.Logic;
using Ultimus.UWF.Common.Entity;
namespace Ultimus.UWF.Form.ProcessControl
{
    public partial class UserInfo : System.Web.UI.UserControl
    {
        IOrg _org = ServiceContainer.Instance().GetService<IOrg>();
        /// <summary>
        /// 表单标题
        /// </summary>
        public string ProcessTitle
        {
            get
            {
                return lblProcessName.Text;
            }
            set
            {
                lblProcessName.Text = value;
            }
        }

        /// <summary>
        /// 流程名称
        /// </summary>
        public string ProcessName
        {
            get
            {
                return fld_PROCESSNAME.Text;
            }
            set
            {
                fld_PROCESSNAME.Text = value;
            }
        }

        public string Type
        {
            get
            {
                return txtType.Text;
            }
            set
            {
                txtType.Text = value;
            }
        }

        /// <summary>
        /// 所对应的表名
        /// </summary>
        public string TableName
        {
            get
            {
                if (string.IsNullOrEmpty(txtTableName.Text))
                {
                    txtTableName.Text = "PROC_" + ProcessName;
                }
                return txtTableName.Text;
            }
            set
            {
                txtTableName.Text = value;
            }
        }
        /// <summary>
        /// 所对应的子表表名
        /// </summary>
        public string TableNameDetail
        {
            get
            {
                return txtTableNameDetail.Text;
            }
            set
            {
                txtTableNameDetail.Text = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
		//public bool ReadOnly
		//{
		//    get
		//    {
		//        if (txtReadOnly.Text == "1")
		//        {
		//            return true;
		//        }
		//        else
		//        {
		//            return false;
		//        }
		//    }
		//    set
		//    {
		//        if (value)
		//        {
		//            txtReadOnly.Text = "1";
		//        }
		//        else
		//        {
		//            txtReadOnly.Text = "0";
		//        }
		//        fld_COMPANY.Visible = !value;
		//        fld_PROCESSSUMMARY.Visible = !value;
		//        lblCOMPANY.Visible = value;
		//        lblSummary.Visible = value;
		//    }
		//}

        /// <summary>
        /// 实例号
        /// </summary>
        public string Incident
        {
            get
            {
                return fld_INCIDENT.Text;
            }
            set
            {
                fld_INCIDENT.Text = value;
            }
        }

        /// <summary>
        /// 表单Guid，唯一键
        /// </summary>
        public string FormId
        {
            get
            {
                return fld_FORMID.Text;
            }
            set
            {
                fld_FORMID.Text = value;
            }
        }

        /// <summary>
        /// 流程前缀，一般用于流程编号
        /// </summary>
        public string ProcessPrefix
        {
            get
            {
                return txtProcessPrefix.Text;
            }
            set
            {
                txtProcessPrefix.Text = value;
            }
        }

        /// <summary>
        /// 流程摘要
        /// </summary>
        public string Summary
        {
            get
            {
                return fld_PROCESSSUMMARY.Text;
            }
            set
            {
                fld_PROCESSSUMMARY.Text = value;
            }
        }

        /// <summary>
        /// 任务号
        /// </summary>
        public string TaskId
        {
            get
            {
                return txtTaskId.Text;
            }
            set
            {
                txtTaskId.Text = value;
            }
        }

        /// <summary>
        /// 步骤名
        /// </summary>
        public string StepName
        {
            get
            {
                return txtStepName.Text;
            }
            set
            {
                txtStepName.Text = value;
            }
        }


        public string Applicant
        {
            get { return fld_APPLICANT.Text; }
        }

        public string AttachmentPath
        {
            get { return var_AttachmentPath.Text; }
            set
            {
                var_AttachmentPath.Text = value;
            }
        }
		public string UserCode
		{
			get { return fld_USERCODE.Text; }
		}

        public string AttachmentName
        {
            get { return var_AttachmentName.Text; }
            set
            {
                var_AttachmentName.Text = value;
            }
        }

        /// <summary>
        /// 表单数据
        /// </summary>
        public DataSet FormData
        {
            get
            {
                if (ViewState["FormData"] != null)
                {
                    return ViewState["FormData"] as DataSet;
                }
                return new DataSet();
            }
            set
            {
                ViewState["FormData"] = value;
            }
        }
        /// <summary>
        /// 表单明细数据
        /// </summary>
        public Hashtable DetailData
        {
            get
            {
                if (ViewState["DetailData"] != null)
                {
                    return ViewState["DetailData"] as Hashtable;
                }
                return new Hashtable();
            }
            set
            {
                ViewState["DetailData"] = value;
            }
        }

        /// <summary>
        /// 获取上级主管
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        public string GetDepartmentManager(string userAccount)
        {
            return "";
        }

        /// <summary>
        /// 获取所有与数据库绑定的控件
        /// </summary>
        /// <returns></returns>
        public List<Control> GetAllFieldControl()
        {
            List<Control> controls = new List<Control>();
            foreach (Control ctl in this.Controls)
            {
                if (ctl.ID != null && (ctl.ID.StartsWith("fld_") || ctl.ID.StartsWith("read_")))
                {
                    controls.Add(ctl);
                }
            }
            foreach (Control ctl in this.Parent.Controls)
            {
                if (ctl.ID != null && (ctl.ID.StartsWith("fld_") || ctl.ID.StartsWith("read_")))
                {
                    controls.Add(ctl);
                }
            }
            return controls;
        }

        /// <summary>
        /// 获取所有包含电子表格变量的控件
        /// </summary>
        /// <returns></returns>
        public List<Control> GetAllVarControl()
        {
            List<Control> controls = new List<Control>();
            foreach (Control ctl in this.Controls)
            {
                if (ctl.ID != null && ctl.ID.StartsWith("var_"))
                {
                    controls.Add(ctl);
                }
            }
            foreach (Control ctl in this.Parent.Controls)
            {
                if (ctl.ID != null && ctl.ID.StartsWith("var_"))
                {
                    controls.Add(ctl);
                }
            }
            return controls;
        }

        /// <summary>
        /// 获取所有的电子表格变量
        /// </summary>
        /// <returns></returns>
        public Hashtable GetFormVars(ref Hashtable vars)
        {
            List<Control> controls = GetAllVarControl();
            foreach (Control ctl in controls)
            {
                string key = ctl.ID.Replace("var_", "");
                string value = "";
                if (ctl is Label)
                {
                    value = (ctl as Label).Text;
                }
                if (ctl is TextBox)
                {
                    value = (ctl as TextBox).Text;
                }
                if (ctl is DropDownList)
                {
                    value = (ctl as DropDownList).SelectedValue;
                }
                if (ctl is CheckBox)
                {
                    value = (ctl as CheckBox).Checked == true ? "1" : "0";
                }
                if (ctl is RadioButton)
                {
                    value = (ctl as RadioButton).Checked == true ? "1" : "0";
                }
                if (ctl is HtmlInputRadioButton)
                {
                    value = (ctl as HtmlInputRadioButton).Checked == true ? "1" : "0";
                }

                vars.Add(key, value);
            }

            return vars;
        }
        /// <summary>
        /// 从表单中抓取数据，完成后存放在ViewState中
        /// </summary>
        /// <returns></returns>
        public DataSet GetFormData()
        {
            return GetFormData(false);
        }
        /// <summary>
        /// 从表单中抓取数据，完成后存放在ViewState中
        /// </summary>
        /// <param name="includeRead">是否包含readonly的控件</param>
        /// <returns></returns>
        public DataSet GetFormData(bool includeRead)
        {
            List<Control> controls = GetAllFieldControl();

            DataSet ds = new DataSet();

            DataTable dt = new DataTable("MainTable");

            DataRow dr = dt.NewRow();
            foreach (Control ctl in controls)
            {
                if (ctl.ID.StartsWith("fld_detail_") || (ctl.ID.StartsWith("read_detail_") && includeRead))//明细表
                {
                    string tableName = ctl.ID.Replace("fld_detail_", "").Replace("read_detail_", "").ToUpper();
                    DataTable dt_detail = GetDetailData(ctl);
                    if (dt_detail.Rows.Count > 0)
                    {
                        dt_detail.TableName = tableName;
                        ds.Tables.Add(dt_detail);
                    }
                }
                else //主表
                {
                    if (ctl.ID.StartsWith("fld_") || (ctl.ID.StartsWith("read_") && includeRead))
                    {
                        string columnName = ctl.ID.Replace("fld_", "").Replace("read_", "");
                        dt.Columns.Add(columnName);
                        if (ctl is Label)
                        {
                            dr[columnName] = (ctl as Label).Text;
                        }
                        if (ctl is TextBox)
                        {
                            dr[columnName] = (ctl as TextBox).Text;
                        }
                        if (ctl is DropDownList)
                        {
                            dr[columnName] = (ctl as DropDownList).SelectedValue;
                        }
                        if (ctl is CheckBox)
                        {
                            dr[columnName] = (ctl as CheckBox).Checked == true ? 1 : 0;
                        }
                        if (ctl is RadioButton)
                        {
                            dr[columnName] = (ctl as RadioButton).Checked == true ? 1 : 0;
                        }
                        if (ctl is HtmlInputRadioButton)
                        {
                            dr[columnName] = (ctl as HtmlInputRadioButton).Checked == true ? 1 : 0;
                        }
                    }
                }
            }
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);


            FormData = ds;
            return ds;
        }

        /// <summary>
        /// 从数据库中加载数据
        /// </summary>
        public void SetFormData()
        {
            List<Control> controls = GetAllFieldControl();
            //1.加载主表
            string tableName = TableName.Trim();
            DataTable dt = new DataTable();
            if (txtType.Text.Trim().ToUpper() == "DRAFT")
            {
                dt = DataAccess.Instance("BizDB").ExecuteDataTable(string.Format("select * from {0} where FORMID='{1}'", tableName, fld_FORMID.Text));
            }
            else
            {
                dt = DataAccess.Instance("BizDB").ExecuteDataTable(string.Format("select * from {0} where PROCESSNAME='{1}' AND INCIDENT='{2}'", tableName, fld_PROCESSNAME.Text, fld_INCIDENT.Text));
            }
            string FORMID = "";
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                FORMID = dr["FORMID"].ToString();
                foreach (DataColumn col in dt.Columns)
                {
                    Control ctl = controls.Find(p => p.ID == "fld_" + col.ColumnName || p.ID == "read_" + col.ColumnName);
                    if (ctl != null)
                    {
                        string columnName = ctl.ID.Replace("fld_", "").Replace("read_", "");
                        if (ctl is Label)
                        {
                            if (columnName.ToUpper().IndexOf("DATE") >= 0)
                            {
                                (ctl as Label).Text = ConvertUtil.ToDateTime(dr[columnName]).ToShortDateString().Replace("-", "/");
                            }
                            else
                            {
                                (ctl as Label).Text = dr[columnName].ToString();
                            }
                        }
                        if (ctl is TextBox)
                        {
                            (ctl as TextBox).Text = dr[columnName].ToString();
                        }
                        if (ctl is DropDownList)
                        {
                            (ctl as DropDownList).SelectedValue = dr[columnName].ToString();
                        }
                        if (ctl is CheckBox)
                        {
                            if (Convert.ToInt32(dr[columnName]) == 1)
                            {
                                (ctl as CheckBox).Checked = true;
                            }
                            else
                            {
                                (ctl as CheckBox).Checked = false;
                            }
                        }
                        if (ctl is RadioButton)
                        {
                            if (Convert.ToInt32(dr[columnName]) == 1)
                            {
                                (ctl as RadioButton).Checked = true;
                            }
                            else
                            {
                                (ctl as RadioButton).Checked = false;
                            }
                        }
                        if (ctl is HtmlInputRadioButton)
                        {
                            if (Convert.ToInt32(dr[columnName]) == 1)
                            {
                                (ctl as HtmlInputRadioButton).Checked = true;
                            }
                            else
                            {
                                (ctl as HtmlInputRadioButton).Checked = false;
                            }
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(TableNameDetail.Trim()))
            {
                //判断是否有多个明细表
                if (TableNameDetail.TrimEnd(new char[] { ',' }).Contains(","))
                {
                    foreach (string tablename in TableNameDetail.Split(new char[] { ',' }))
                    {
                        SetDetailData(tablename, controls, FormId);
                    }
                }
                else
                {
                    SetDetailData(TableNameDetail, controls, FormId);
                }
            }

            if (dt != null)
            {
                FormData = dt.DataSet;
            }
        }

        //给明细表赋值
        public void SetDetailData(string TableNameDetail, List<Control> controls, string FORMID)
        {

            DataTable dt_detail = null;
            dt_detail = DataAccess.Instance("BizDB").ExecuteDataTable(string.Format("select * from {0} where  FORMID='{1}' order by rowid ", TableNameDetail, FORMID));

            Control ctl = controls.Find(p => p.ID.ToLower() == "fld_detail_" + TableNameDetail.ToLower() || p.ID.ToLower() == "read_detail_" + TableNameDetail.ToLower());
            if (ctl != null)
            {
                if (ctl is GridView)
                {
                    (ctl as GridView).DataSource = dt_detail;
                    (ctl as GridView).DataBind();
                }
                if (ctl is Repeater)
                {
                    (ctl as Repeater).DataSource = dt_detail;
                    (ctl as Repeater).DataBind();
                }
                add2DetailData(TableNameDetail, dt_detail);
            }
        }

        public void add2DetailData(string key, object value)
        {
            Hashtable htb = DetailData;
            htb.Add(key.ToUpper(), value);
            DetailData = htb;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fld_PROCESSNAME.Text = Request.QueryString["ProcessName"];
                fld_INCIDENT.Text = Request.QueryString["Incident"];
                incident.Text = Request.QueryString["Incident"];
                txtTaskId.Text = Request.QueryString["TaskId"];
                txtStepName.Text = Request.QueryString["StepName"];
                txtType.Text = Request.QueryString["Type"];
                txtApplicantAccount.Text = Request.QueryString["UserName"];
                fld_FORMID.Text = Request.QueryString["FORMID"];
                if (string.IsNullOrEmpty(fld_FORMID.Text))
                {
                    fld_FORMID.Text = Guid.NewGuid().ToString();
                }

                if (string.IsNullOrEmpty(fld_PROCESSNAME.Text))
                {
                    return;
                }
                //加载公司
                ResourceLogic res = new ResourceLogic();
               // List<ResourceEntity> list= res.GetResourceList("Company");
				//fld_COMPANY.DataSource = list;
				//fld_COMPANY.DataBind();
                //新建流程
                if (string.IsNullOrEmpty(fld_INCIDENT.Text) || fld_INCIDENT.Text.Trim() == "0")
                {
                    UserEntity user = _org.GetUserEntity(SessionLogic.GetLoginName());
                    fld_APPLICANTACCOUNT.Text = SessionLogic.GetLoginName().Replace("\\", "/");
                    //var_DepartmentId.Text = user.DEPARTMENTID.ToString();
                  // DataTable dt_UserInfo = DataAccess.Instance("BizDB").ExecuteDataTable(string.Format("select * from ORG_USER where  LOGINNAME='{0}' ", SessionLogic.GetLoginName()));
                    Logic.CommonLogic comFunc = new Logic.CommonLogic();
                  Logic.UserInfo usermode=comFunc.GetUserDataInfo(SessionLogic.GetLoginName());
                  var_REMAIL.Text = user.EMAIL;
				  if (user.USERCODE!= null)
					  fld_USERCODE.Text = user.USERCODE;
				  if (usermode != null)
				  {
					  user.EXT04 = usermode.UserEnName;
					 fld_USERCODE.Text = usermode.USERCODE;
				  }
				  if (user.USERNAME == user.EXT04)
					  //fld_APPLICANT.Text = user.USERNAME + "(" + user.LOGINNAME + ")";
					  //fld_APPLICANT.Text = user.EXT04 + "(" + user.LOGINNAME + ")";
					  fld_APPLICANT.Text = user.EXT04;
				  else
					  //fld_APPLICANT.Text = user.USERNAME + " " +user.EXT04+ "(" + user.LOGINNAME.Split('/')[1] + ")" ;
					  //fld_APPLICANT.Text = user.EXT04 + "(" + user.LOGINNAME.Split('/')[1] + ")";
					  fld_APPLICANT.Text = user.EXT04;
					fld_REQUESTDATE.Text = DateTime.Now.ToShortDateString().Replace("-", "/");
                    fld_DEPARTMENT.Text = usermode.DeptName + " " + usermode.DeptEnName;
                   // fld_DOCUMENTNO.Visible = false;
                   // lblDocumentNo.Visible = false;
                }
                else //打开待办或已办
                {
                    lblDocumentNo.Visible = true;
					//fld_DOCUMENTNO.Visible = true;
                    //加载表单数据
                    SetFormData();

                    lblSummary.Text = fld_PROCESSSUMMARY.Text;
                    //lblCOMPANY.Text = fld_COMPANY.SelectedValue;
                    //this.barcode.Text = BarCodeToHTML.get(fld_DOCUMENTNO.Text);
                }

                //从草稿或者重发起打开
                if (txtType.Text.ToUpper().Trim() == "DRAFT" || StepSettingsLogic.GetFirstStep(ProcessName).Equals(StepName))
                {
                    UserEntity user = _org.GetUserEntity(SessionLogic.GetLoginName());
                    fld_APPLICANTACCOUNT.Text = SessionLogic.GetLoginName().Replace("\\", "/");
                    //var_DepartmentId.Text = user.DEPARTMENTID.ToString();
                }

                //this.ProcessTitle = Lang.Get("Form_Expense_Title");
            }
        }

        /// <summary>
        /// 明细行新增一行
        /// </summary>
        /// <param name="ctl"></param>
        public void AddNewRow(Control ctl)
        {
            string tablename = ctl.ID.Replace("fld_detail_", "").Replace("read_detail_", "").ToUpper();
            if (DetailData.Count == 0)
            {
                SetFormData();
            }
            DataTable dt = ((DataTable)DetailData[tablename]).Clone();
            dt = GetDetailData(ctl);
            dt.Rows.Add(dt.NewRow());
            if (ctl is GridView)
            {
                GridView gv = ctl as GridView;
                gv.DataSource = dt;
                gv.DataBind();
            }
            if (ctl is Repeater)
            {
                Repeater rp = ctl as Repeater;
                rp.DataSource = dt;
                rp.DataBind();
            }
        }

        /// <summary>
        /// 明细行删除一行
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="e"></param>
        public void DeleteRow(Control ctl, RepeaterCommandEventArgs e)
        {
            string tablename = ctl.ID.Replace("fld_detail_", "").Replace("read_detail_", "").ToUpper();
            DataTable dt = ((DataTable)DetailData[tablename]).Clone();
            if (e.CommandName == "del")
            {
                dt = GetDetailData(ctl);
                dt.Rows[e.Item.ItemIndex].Delete();
                dt.AcceptChanges();
            }
            if (ctl is GridView)
            {
                GridView gv = ctl as GridView;
                gv.DataSource = dt;
                gv.DataBind();
            }
            if (ctl is Repeater)
            {
                Repeater rp = ctl as Repeater;
                rp.DataSource = dt;
                rp.DataBind();
            }
        }

        /// <summary>
        /// 获取明细行中的数据
        /// </summary>
        /// <param name="ctl">数据绑定控件</param>
        /// <returns></returns>
        public DataTable GetDetailData(Control ctl)
        {
            string tablename = ctl.ID.Replace("fld_detail_", "").Replace("read_detail_", "").ToUpper();
            DataTable sourceDt = ((DataTable)DetailData[tablename]);
            if (sourceDt == null)
            {
                return new DataTable();
            }
            DataTable dt = ((DataTable)DetailData[tablename]).Clone();
            if (ctl is GridView)
            {
                GridView gv = ctl as GridView;
                foreach (GridViewRow item in gv.Rows)
                {
                    GetControl(item.Controls, ref dt);
                }
            }
            if (ctl is Repeater)
            {
                Repeater rp = ctl as Repeater;
                foreach (RepeaterItem item in rp.Items)
                {
                    GetControl(item.Controls, ref dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// 获取明细行控件的值，存放到DataTable中
        /// </summary>
        /// <param name="item"></param>
        /// <param name="dt"></param>
        public void GetControl(ControlCollection item, ref DataTable dt)
        {
            DataRow dr = dt.NewRow();
            foreach (Control ctl in item)
            {
                if (ctl.ID != null && (ctl.ID.StartsWith("fld_") || ctl.ID.StartsWith("read_")))
                {
                    string columnName = ctl.ID.Replace("fld_", "").Replace("read_", "");
                    if (columnName == "FORMID")
                    {
                        dr[columnName] = fld_FORMID.Text;
                        continue;
                    }
                    if (ctl is Label)
                    {
                        dr[columnName] = (ctl as Label).Text;
                    }
                    if (ctl is TextBox)
                    {
                        if ((ctl as TextBox).Text.Trim() != "")
                        {
                            dr[columnName] = (ctl as TextBox).Text;
                        }
                    }
                    if (ctl is DropDownList)
                    {
                        dr[columnName] = (ctl as DropDownList).SelectedValue;
                    }
                    if (ctl is CheckBox)
                    {
                        dr[columnName] = (ctl as CheckBox).Checked == true ? 1 : 0;
                    }
                    if (ctl is RadioButton)
                    {
                        dr[columnName] = (ctl as RadioButton).Checked == true ? 1 : 0;
                    }
                }
            }
            if (dt.Columns.Contains("PROCESSNAME"))
            {
                dr["PROCESSNAME"] = ProcessName;
            }
            if (dt.Columns.Contains("FORMID"))
            {
                dr["FORMID"] = FormId;
            }
            if (dt.Columns.Contains("INCIDENT"))
            {
                dr["INCIDENT"] = Incident;
            }
            dt.Rows.Add(dr);
        }

    }
}