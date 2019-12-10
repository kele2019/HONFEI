using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Presale.Process.EmployeeTraining.Entity;
using Ultimus.UWF.Form.ProcessControl;
using System.Data.SqlClient;
namespace Presale.Process.EmployeeTraining
{
	public partial class Practise : System.Web.UI.Page
	{
		DataTable dt_tem = new DataTable();
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                string FormId = Request.QueryString["FormID"];
                FormID.Text = FormId;
                ReLoadExistData(FormId);
            }
		}
        public void ReLoadExistData(string FormID)
        {
            string strsql = "select * from PROC_TrainingPractise where FORMID='"+FormID+"' order by RowIndex";
            DataTable dtPrcatise=DataAccess.Instance("BizDB").ExecuteDataTable(strsql);
            if (dtPrcatise.Rows.Count > 0)
            {
                if (dtPrcatise.Rows.Count <= 5)
                {
                    test.SelectedIndex = 0;
                    for (int i = 0; i < 5 - dtPrcatise.Rows.Count; i++)
                    {
                        dtPrcatise.Rows.Add(dtPrcatise.NewRow());
                    }

                }
                if (dtPrcatise.Rows.Count > 5)
                {
                    test.SelectedIndex = 0;
                    for (int i = 0; i < 10 - dtPrcatise.Rows.Count; i++)
                    {
                        dtPrcatise.Rows.Add(dtPrcatise.NewRow());
                    }
                }
                PROC_EmployeeTraining_DT.DataSource = dtPrcatise;
                PROC_EmployeeTraining_DT.DataBind();
                ViewState["datatable"] = dtPrcatise;
                foreach (RepeaterItem item in PROC_EmployeeTraining_DT.Items)
                {
                    CheckBox answerCheckA = item.FindControl("answerCheckA") as CheckBox;
                    CheckBox answerCheckB = item.FindControl("answerCheckB") as CheckBox;
                    CheckBox answerCheckC = item.FindControl("answerCheckC") as CheckBox;
                    CheckBox answerCheckD = item.FindControl("answerCheckD") as CheckBox;
                    CheckBox answerCheckE = item.FindControl("answerCheckE") as CheckBox;
                    CheckBox answerCheckF = item.FindControl("answerCheckF") as CheckBox;
                    TextBox answer = item.FindControl("answer") as TextBox;
                    if (answer.Text.Contains("A"))
                        answerCheckA.Checked = true;
                    if (answer.Text.Contains("B"))
                        answerCheckB.Checked = true;
                    if (answer.Text.Contains("C"))
                        answerCheckC.Checked = true;
                    if (answer.Text.Contains("D"))
                        answerCheckD.Checked = true;
                    if (answer.Text.Contains("E"))
                        answerCheckE.Checked = true;
                    if (answer.Text.Contains("F"))
                        answerCheckF.Checked = true;
                }
            }
        }
		protected void SelectedIndexChanged(object sender, EventArgs e)
		{
			//ViewState["datatable"] = null;
            if (ViewState["datatable"] == null)
            {
                if (test.SelectedValue == "0")
                    for (int i = 0; i < 5; i++)
                    {
                        dtaddrow();
                    }
                if (test.SelectedValue == "1")
                    for (int i = 0; i < 10; i++)
                    {
                        dtaddrow();
                    }
            }
            else
            {
                DataTable dt = (DataTable)ViewState["datatable"];
                if (test.SelectedValue == "0")
                {
                    if (dt.Rows.Count > 5)
                    {
                        for (int i = 0; i <5; i++)
                        {
                            dt.Rows.Remove(dt.Rows[9-i]);
                           
                        }
                        ViewState["datatable"] = dt;
                        PROC_EmployeeTraining_DT.DataSource = dt;
                        PROC_EmployeeTraining_DT.DataBind();
                    }
                }
                if (test.SelectedValue == "1")
                {
                    for (int i = 0; i < 5; i++)
                    {
                        dtaddrow();
                    }
                }
            }
		}
		private void dtaddrow()
		{
			DataTable dt = (DataTable)ViewState["datatable"];
			if (dt != null)
				dt_tem = dt;
			else
			{
				dt_tem.Columns.Add("Question", typeof(string));
				dt_tem.Columns.Add("answerA", typeof(string));
				dt_tem.Columns.Add("answerB", typeof(string));
				dt_tem.Columns.Add("answerC", typeof(string));
				dt_tem.Columns.Add("answerD", typeof(string));
				dt_tem.Columns.Add("answerE", typeof(string));
				dt_tem.Columns.Add("answerF", typeof(string));
				dt_tem.Columns.Add("answer", typeof(string));

			}
			dt_tem.Rows.Add(dt_tem.NewRow());
			PROC_EmployeeTraining_DT.DataSource = dt_tem;
			PROC_EmployeeTraining_DT.DataBind();
			ViewState["datatable"] = dt_tem;
		}

		protected void btnSave_Clcik(object sender, EventArgs e)
		{
            DataAccess.Instance("BizDB").ExecuteNonQuery("delete from PROC_TrainingPractise where FORMID='"+FormID.Text+"'");

			//Response.Write("<scr" + "ipt language='javascript'>window.close();</sc" + "ript>");
			string formId = FormID.Text.ToString();
            int RowIndex = 1;
			foreach (RepeaterItem item in this.PROC_EmployeeTraining_DT.Items)
			{
				string Question = ((TextBox)item.FindControl("Question")).Text.ToString();
				string answerA = ((TextBox)item.FindControl("answerA")).Text.ToString();
				string answerB = ((TextBox)item.FindControl("answerB")).Text.ToString();
				string answerC = ((TextBox)item.FindControl("answerC")).Text.ToString();
				string answerD = ((TextBox)item.FindControl("answerD")).Text.ToString();
				string answerE = ((TextBox)item.FindControl("answerE")).Text.ToString();
				string answerF = ((TextBox)item.FindControl("answerF")).Text.ToString();
				string answer = "";
				CheckBox answerCheckA = (CheckBox)item.FindControl("answerCheckA");
				CheckBox answerCheckB = (CheckBox)item.FindControl("answerCheckB");
				CheckBox answerCheckC = (CheckBox)item.FindControl("answerCheckC");
				CheckBox answerCheckD = (CheckBox)item.FindControl("answerCheckD");
				CheckBox answerCheckE = (CheckBox)item.FindControl("answerCheckE");
				CheckBox answerCheckF = (CheckBox)item.FindControl("answerCheckF");
				if (answerCheckA.Checked) {
					answer += "A";
				}
				if (answerCheckB.Checked)
				{
					answer += "B";
				}
				if (answerCheckC.Checked)
				{
					answer += "C";
				}
				if (answerCheckD.Checked)
				{
					answer += "D";
				}
				if (answerCheckE.Checked)
				{
					answer += "E";
				}
				if (answerCheckF.Checked)
				{
					answer += "F";
				}
               
               // string sql= "insert into PROC_TrainingPractise(FORMID,RowIndex,Question,answerA,answerB,answerC,answerD,answerE,answerF,answer) values('" + formId + "','"+RowIndex+++"','" + Question + "','" + answerA + "','" + answerB + "','" + answerC + "','" + answerD + "','" + answerE + "','" + answerF + "','" + answer + "')";
                string sql = "insert into PROC_TrainingPractise(FORMID,RowIndex,Question,answerA,answerB,answerC,answerD,answerE,answerF,answer) values('" + formId + "','" + RowIndex++ + "',@Question,@answerA,@answerB,@answerC,@answerD,@answerE,@answerF,@answer)";
                List<SqlParameter> sqlpaList = new List<SqlParameter>();
                sqlpaList.Add(new SqlParameter("@Question", Question));
                sqlpaList.Add(new SqlParameter("@answerA", answerA));
                sqlpaList.Add(new SqlParameter("@answerB", answerB));
                sqlpaList.Add(new SqlParameter("@answerC", answerC));
                sqlpaList.Add(new SqlParameter("@answerD", answerD));
                sqlpaList.Add(new SqlParameter("@answerE", answerE));
                sqlpaList.Add(new SqlParameter("@answerF", answerF));
                sqlpaList.Add(new SqlParameter("@answer", answer));

                DataAccess.Instance("BizDB").ExecuteNonQuery(sql,sqlpaList.ToArray());

                //DataAccess.Instance("BizDB").ExecuteNonQuery(sql);
                 
			}
			DataAccess.Instance("BizDB").ExecuteNonQuery("update PROC_EmployeeTraining set havePapers='" + havePapers.Text + "' where FORMID= '" + formId + "'");
			Page.RegisterStartupScript("key", "<script>SinglePersonConfirm()</script>");
		}
	}
}