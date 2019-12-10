using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyLib;

namespace Presale.Process.EmployeeTraining
{
	public partial class Answer : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string FormId = Request.QueryString["FormID"];
			formId.Text = FormId;
			if (!IsPostBack)
			{
				dtaddrow(FormId);
			}
		}

		private void dtaddrow(string formId)
		{
            string sql = "select RowIndex,Question,answerA,answerB,answerC,answerD,answerE,answerF,answer FROM PROC_TrainingPractise where FORMID='" + formId + "'";
			DataTable dt_Item = DataAccess.Instance("BizDB").ExecuteDataSet(sql).Tables[0];
			#region
			//DataTable dt = new DataTable();
			//dt.Columns.Add("Question", typeof(string));
			//dt.Columns.Add("answerA", typeof(string));
			//dt.Columns.Add("answerB", typeof(string));
			//dt.Columns.Add("answerC", typeof(string));
			//dt.Columns.Add("answerD", typeof(string));
			//dt.Columns.Add("answerE", typeof(string));
			//dt.Columns.Add("answerF", typeof(string));
			//dt.Columns.Add("answer", typeof(string));
			////dt.Rows.Add(dt.NewRow());
			//if (dt_Item.Rows.Count > 0)
			//{
			//    int UserCount = dt_Item.Rows.Count;
			//    for (int i = 0; i < UserCount; i++)
			//    {
			//        dt.Rows.Add(dt.NewRow());
			//        DataRow DRUSER = dt.Rows[(i / 8)];
			//        if ((UserCount - 1) > i) DRUSER["Question"] = dt_Item.Rows[i]["Question"];
			//        if ((UserCount - 1) > i) DRUSER["answerA"] = dt_Item.Rows[i]["answerA"];
			//        if ((UserCount - 1) > i) DRUSER["answerB"] = dt_Item.Rows[i]["answerB"];
			//        if ((UserCount - 1) > i) DRUSER["answerC"] = dt_Item.Rows[i]["answerC"];
			//        if ((UserCount - 1) > i) DRUSER["answerD"] = dt_Item.Rows[i]["answerD"];
			//        if ((UserCount - 1) > i) DRUSER["answerE"] = dt_Item.Rows[i]["answerE"];
			//        if ((UserCount - 1) > i) DRUSER["answerF"] = dt_Item.Rows[i]["answerF"];
			//        if ((UserCount - 1) > i) DRUSER["answer"] = dt_Item.Rows[i]["answer"];
			//    }
			//}
			#endregion
			Repeater1.DataSource = dt_Item;
			Repeater1.DataBind();  
		}

		protected void btnSave_Clcik(object sender, EventArgs e)
		{
			string FormId = formId.Text;
            string UserAccount = Page.User.Identity.Name;
            object UserID=DataAccess.Instance("BizDB").ExecuteScalar("select USERID from ORG_USER where LOGINNAME='"+UserAccount+"'");
            string StrsqlAnswer = " delete from PROC_TrainingAnswer where FORMID='"+FormId+"' and UserAccount='"+UserID+"' ";
			foreach (RepeaterItem item in this.Repeater1.Items)
			{
                string answer = (item.FindControl("showUserAnswer") as TextBox).Text.ToString();
                string RowIndex = (item.FindControl("hdRowIndex") as HiddenField).Value;
                StrsqlAnswer += "insert into PROC_TrainingAnswer(FORMID,RowIndex,answer,UserAccount) values('" + FormId + "','"+RowIndex+"','" + answer + "','" + UserID + "')";
			}
            DataAccess.Instance("BizDB").ExecuteNonQuery(StrsqlAnswer);
			DataAccess.Instance("BizDB").ExecuteNonQuery("update PROC_EmployeeTraining set SuccessOrNot='" + successOrNot.Text + "' where FORMID= '" + formId + "'");
            Page.ClientScript.RegisterStartupScript(typeof(string), "key", "<script>beforeSubmit()</script>");
		}
	}
}