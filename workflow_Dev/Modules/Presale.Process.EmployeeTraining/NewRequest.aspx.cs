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
using System.Text;
using Presale.Process.Common;

namespace Presale.Process.EmployeeTraining
{
	public partial class NewRequest : System.Web.UI.Page
	{
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				
				string Incident = Request.QueryString["Incident"];
                object ProcessName = Request.QueryString["Processname"];
                object myRequest = Request.QueryString["Type"];
				if (Incident != "0")
				{
					hdIncident.Value = Incident;
				}
				string domin = ConfigurationManager.AppSettings["Domain"];
				domain.Text = domin + "/";
				bindlist();
				bindTeacherList();
                if (Incident.ToString() != "0")
                {
                    string FlagStatus = DataAccess.Instance("BizDB").ExecuteScalar("select  status from PROC_EmployeeTraining where INCIDENT='" + Incident + "' ").ToString();

                    if (FlagStatus == "1" && int.Parse(Incident.ToString()) > 0 && myRequest.ToString().ToUpper() == "MYREQUEST")
                    {
                        hdUrgeTask.Value = "Yes";
                        string strRevoke = string.Format("select status from  dbo.PROC_REVOKE with(nolock) where ProcessName='{0}' and incident='{1}'", ProcessName, Incident);
                        object FlagRevoke = DataAccess.Instance("BizDB").ExecuteScalar(strRevoke);
                        if (FlagRevoke != null)
                        {
                            if (FlagRevoke.ToString() == "1")
                            {
                                btnRevoke.Visible = true;
                            }

                        }
                        object Requestor = Request.QueryString["Requestor"];
                        if (Requestor != null)
                        {
                            string CurrentUser = ConfigurationManager.AppSettings["Domain"] + "\\" + Requestor.ToString();
                            if (Page.User.Identity.Name.ToLower() == CurrentUser.ToLower())
                            {

                                btnRevoke.Visible = false;
                            }
                        }
                    }
                    else
                    {

                    }
                }
			}
			((ButtonList)ButtonList1).BeforeSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_BeforeSubmit);
			((ButtonList)ButtonList1).AfterSubmit += new System.ComponentModel.CancelEventHandler(NewRequest_AfterSubmit);
		}
		protected void NewRequest_AfterSubmit(object sender, System.ComponentModel.CancelEventArgs g)
		{
          //  string FormID=(UserInfo1.FindControl("fld_FORMID") as TextBox).Text;

            string DOCUMENTNO=DataAccess.Instance("BizDB").ExecuteScalar("select DOCUMENTNO from PROC_EmployeeTraining where FORMID='" + formId.Text + "'").ToString();
            DataAccess.Instance("BizDB").ExecuteNonQuery("delete from COM_EmployeeTrainSignInfo where TrainDocmentNo='" + DOCUMENTNO + "'");

            string strInsertTraningSignsql = "";
            for (int i = 0; i < hdUserID.Value.TrimEnd(',').Split(',').Length; i++)
            {
                strInsertTraningSignsql += " INSERT INTO  [dbo].[COM_EmployeeTrainSignInfo] ([TrainDocmentNo],[UserID],SumDate) VALUES ('" + DOCUMENTNO + "','" + hdUserID.Value.Split(',')[i] + "','0')";
            }
            DataAccess.Instance("BizDB").ExecuteNonQuery(strInsertTraningSignsql);

		}
		protected string havePapers(string formId) 
		{
			string sql = "select havePractise from PROC_EmployeeTraining where FORMID = ' " + formId + " '";
			return DataAccess.Instance("BizDB").ExecuteNonQuery(sql).ToString();
		}
		protected void NewRequest_BeforeSubmit(object sender, System.ComponentModel.CancelEventArgs g) 
		{
            #region
            object Incident = Request.QueryString["Incident"];
            object ProcessName = Request.QueryString["Processname"];
            string strRevoke = string.Format("update PROC_REVOKE set status=1,OperatorDate=getdate() where  ProcessName='{0}' and incident='{1}'", ProcessName, Incident);
            DataAccess.Instance("BizDB").ExecuteNonQuery(strRevoke);
            #endregion
			StringBuilder post = new StringBuilder();
            StringBuilder StrUserEnName = new StringBuilder();
            #region Manager
            foreach (RepeaterItem item in RepeaterManager.Items)
			{
				CheckBox User1 = ((CheckBox)item.FindControl("User1"));
				CheckBox User2 = ((CheckBox)item.FindControl("User2"));
				CheckBox User3 = ((CheckBox)item.FindControl("User3"));
				CheckBox User4 = ((CheckBox)item.FindControl("User4"));
				CheckBox User5 = ((CheckBox)item.FindControl("User5"));
				if (User1.Checked)
				{
					post.Append("'" + User1.Text.ToString() + "',");
				}
				if (User2.Checked)
				{
					post.Append("'" + User2.Text.ToString() + "',");
				}
				if (User3.Checked)
				{
					post.Append("'" + User3.Text.ToString() + "',");
				}
				if (User4.Checked)
				{
					post.Append("'" + User4.Text.ToString() + "',");
				}
				if (User5.Checked)
				{
					post.Append("'" + User5.Text.ToString() + "',");
				}
            }
            #endregion
            #region IT
            foreach (RepeaterItem item in RepeaterIT.Items)
			{
				CheckBox User1 = ((CheckBox)item.FindControl("User1"));
				CheckBox User2 = ((CheckBox)item.FindControl("User2"));
				CheckBox User3 = ((CheckBox)item.FindControl("User3"));
				CheckBox User4 = ((CheckBox)item.FindControl("User4"));
				CheckBox User5 = ((CheckBox)item.FindControl("User5"));
				if (User1.Checked)
				{
					post.Append("'" + User1.Text.ToString() + "',");
				}
				if (User2.Checked)
				{
					post.Append("'" + User2.Text.ToString() + "',");
				}
				if (User3.Checked)
				{
					post.Append("'" + User3.Text.ToString() + "',");
				}
				if (User4.Checked)
				{
					post.Append("'" + User4.Text.ToString() + "',");
				}
				if (User5.Checked)
				{
					post.Append("'" + User5.Text.ToString() + "',");
				}
            }
            #endregion
            #region PM
            foreach (RepeaterItem item in RepeaterPM.Items)
            {
                CheckBox User1 = ((CheckBox)item.FindControl("User1"));
                CheckBox User2 = ((CheckBox)item.FindControl("User2"));
                CheckBox User3 = ((CheckBox)item.FindControl("User3"));
                CheckBox User4 = ((CheckBox)item.FindControl("User4"));
                CheckBox User5 = ((CheckBox)item.FindControl("User5"));
                if (User1.Checked)
                {
                    post.Append("'" + User1.Text.ToString() + "',");
                }
                if (User2.Checked)
                {
                    post.Append("'" + User2.Text.ToString() + "',");
                }
                if (User3.Checked)
                {
                    post.Append("'" + User3.Text.ToString() + "',");
                }
                if (User4.Checked)
                {
                    post.Append("'" + User4.Text.ToString() + "',");
                }
                if (User5.Checked)
                {
                    post.Append("'" + User5.Text.ToString() + "',");
                }
            }
            #endregion
            #region HR
            foreach (RepeaterItem item in RepeaterHR.Items)
			{
				CheckBox User1 = ((CheckBox)item.FindControl("User1"));
				CheckBox User2 = ((CheckBox)item.FindControl("User2"));
				CheckBox User3 = ((CheckBox)item.FindControl("User3"));
				CheckBox User4 = ((CheckBox)item.FindControl("User4"));
				CheckBox User5 = ((CheckBox)item.FindControl("User5"));
				if (User1.Checked)
				{
					post.Append("'" + User1.Text.ToString() + "',");
				}
				if (User2.Checked)
				{
					post.Append("'" + User2.Text.ToString() + "',");
				}
				if (User3.Checked)
				{
					post.Append("'" + User3.Text.ToString() + "',");
				}
				if (User4.Checked)
				{
					post.Append("'" + User4.Text.ToString() + "',");
				}
				if (User5.Checked)
				{
					post.Append("'" + User5.Text.ToString() + "',");
				}
            }
            #endregion
            #region ADM
            foreach (RepeaterItem item in RepeaterADM.Items)
			{
				CheckBox User1 = ((CheckBox)item.FindControl("User1"));
				CheckBox User2 = ((CheckBox)item.FindControl("User2"));
				CheckBox User3 = ((CheckBox)item.FindControl("User3"));
				CheckBox User4 = ((CheckBox)item.FindControl("User4"));
				CheckBox User5 = ((CheckBox)item.FindControl("User5"));
				if (User1.Checked)
				{
					post.Append("'" + User1.Text.ToString() + "',");
				}
				if (User2.Checked)
				{
					post.Append("'" + User2.Text.ToString() + "',");
				}
				if (User3.Checked)
				{
					post.Append("'" + User3.Text.ToString() + "',");
				}
				if (User4.Checked)
				{
					post.Append("'" + User4.Text.ToString() + "',");
				}
				if (User5.Checked)
				{
					post.Append("'" + User5.Text.ToString() + "',");
				}
            }
            #endregion
            #region Fina
            foreach (RepeaterItem item in RepeaterFIN.Items)
			{
				CheckBox User1 = ((CheckBox)item.FindControl("User1"));
				CheckBox User2 = ((CheckBox)item.FindControl("User2"));
				CheckBox User3 = ((CheckBox)item.FindControl("User3"));
				CheckBox User4 = ((CheckBox)item.FindControl("User4"));
				CheckBox User5 = ((CheckBox)item.FindControl("User5"));
				if (User1.Checked)
				{
					post.Append("'" + User1.Text.ToString() + "',");
				}
				if (User2.Checked)
				{
					post.Append("'" + User2.Text.ToString() + "',");
				}
				if (User3.Checked)
				{
					post.Append("'" + User3.Text.ToString() + "',");
				}
				if (User4.Checked)
				{
					post.Append("'" + User4.Text.ToString() + "',");
				}
				if (User5.Checked)
				{
					post.Append("'" + User5.Text.ToString() + "',");
				}
            }
            #endregion
            #region QA
            foreach (RepeaterItem item in RepeaterQA.Items)
			{
				CheckBox User1 = ((CheckBox)item.FindControl("User1"));
				CheckBox User2 = ((CheckBox)item.FindControl("User2"));
				CheckBox User3 = ((CheckBox)item.FindControl("User3"));
				CheckBox User4 = ((CheckBox)item.FindControl("User4"));
				CheckBox User5 = ((CheckBox)item.FindControl("User5"));
				if (User1.Checked)
				{
					post.Append("'" + User1.Text.ToString() + "',");
				}
				if (User2.Checked)
				{
					post.Append("'" + User2.Text.ToString() + "',");
				}
				if (User3.Checked)
				{
					post.Append("'" + User3.Text.ToString() + "',");
				}
				if (User4.Checked)
				{
					post.Append("'" + User4.Text.ToString() + "',");
				}
				if (User5.Checked)
				{
					post.Append("'" + User5.Text.ToString() + "',");
				}
            }
            #endregion
            #region ENG
            foreach (RepeaterItem item in RepeaterENG.Items)
			{
				CheckBox User1 = ((CheckBox)item.FindControl("User1"));
				CheckBox User2 = ((CheckBox)item.FindControl("User2"));
				CheckBox User3 = ((CheckBox)item.FindControl("User3"));
				CheckBox User4 = ((CheckBox)item.FindControl("User4"));
				CheckBox User5 = ((CheckBox)item.FindControl("User5"));
				if (User1.Checked)
				{
					post.Append("'" + User1.Text.ToString() + "',");
				}
				if (User2.Checked)
				{
					post.Append("'" + User2.Text.ToString() + "',");
				}
				if (User3.Checked)
				{
					post.Append("'" + User3.Text.ToString() + "',");
				}
				if (User4.Checked)
				{
					post.Append("'" + User4.Text.ToString() + "',");
				}
				if (User5.Checked)
				{
					post.Append("'" + User5.Text.ToString() + "',");
				}
            }
            #endregion
            #region PUR
            foreach (RepeaterItem item in RepeaterPUR.Items)
			{
				CheckBox User1 = ((CheckBox)item.FindControl("User1"));
				CheckBox User2 = ((CheckBox)item.FindControl("User2"));
				CheckBox User3 = ((CheckBox)item.FindControl("User3"));
				CheckBox User4 = ((CheckBox)item.FindControl("User4"));
				CheckBox User5 = ((CheckBox)item.FindControl("User5"));
				if (User1.Checked)
				{
					post.Append("'" + User1.Text.ToString() + "',");
				}
				if (User2.Checked)
				{
					post.Append("'" + User2.Text.ToString() + "',");
				}
				if (User3.Checked)
				{
					post.Append("'" + User3.Text.ToString() + "',");
				}
				if (User4.Checked)
				{
					post.Append("'" + User4.Text.ToString() + "',");
				}
				if (User5.Checked)
				{
					post.Append("'" + User5.Text.ToString() + "',");
				}
            }
            #endregion
            #region HSEF
            foreach (RepeaterItem item in RepeaterHSEF.Items)
            {
                CheckBox User1 = ((CheckBox)item.FindControl("User1"));
                CheckBox User2 = ((CheckBox)item.FindControl("User2"));
                CheckBox User3 = ((CheckBox)item.FindControl("User3"));
                CheckBox User4 = ((CheckBox)item.FindControl("User4"));
                CheckBox User5 = ((CheckBox)item.FindControl("User5"));
                if (User1.Checked)
                {
                    post.Append("'" + User1.Text.ToString() + "',");
                }
                if (User2.Checked)
                {
                    post.Append("'" + User2.Text.ToString() + "',");
                }
                if (User3.Checked)
                {
                    post.Append("'" + User3.Text.ToString() + "',");
                }
                if (User4.Checked)
                {
                    post.Append("'" + User4.Text.ToString() + "',");
                }
                if (User5.Checked)
                {
                    post.Append("'" + User5.Text.ToString() + "',");
                }
            }
            #endregion

            #region Marketing
            foreach (RepeaterItem item in Marketing.Items)
            {
                CheckBox User1 = ((CheckBox)item.FindControl("User1"));
                CheckBox User2 = ((CheckBox)item.FindControl("User2"));
                CheckBox User3 = ((CheckBox)item.FindControl("User3"));
                CheckBox User4 = ((CheckBox)item.FindControl("User4"));
                CheckBox User5 = ((CheckBox)item.FindControl("User5"));
                if (User1.Checked)
                {
                    post.Append("'" + User1.Text.ToString() + "',");
                }
                if (User2.Checked)
                {
                    post.Append("'" + User2.Text.ToString() + "',");
                }
                if (User3.Checked)
                {
                    post.Append("'" + User3.Text.ToString() + "',");
                }
                if (User4.Checked)
                {
                    post.Append("'" + User4.Text.ToString() + "',");
                }
                if (User5.Checked)
                {
                    post.Append("'" + User5.Text.ToString() + "',");
                }
            }
            #endregion

            string sql = " declare @ret nvarchar(4000) ";
					sql += " set @ret = '' ";
                    sql += " select (@ret + LOGINNAME+'|USER,') as userinfo,USERID,EXT04 from  dbo.ORG_USER ";
					sql += " where EXT04 in (" + post.Remove(post.ToString().LastIndexOf(","),1) + ") ";
					sql += " print(@ret) ";
			List<CheckUser> list = DataAccess.Instance("BizDB").ExecuteList<CheckUser>(sql);
			StringBuilder personals = new StringBuilder();
			foreach (CheckUser personal in list)
			{
				personals.Append(personal.userinfo);
                StrUserEnName.Append(personal.EXT04+",");
                hdUserID.Value += personal.USERID+",";
			}
            fld_TrainingUser.Text = StrUserEnName.ToString().TrimEnd(',');
			fld_ApprovalArr_TrainingPersonnel.Text = (personals.ToString()).Replace("\\", "/");
			//string sqlstr = "update PROC_EmployeeTraining set TrainingPersonnel = '" + personals.ToString() + "' where FORMID = '" + formId.Text + "'";
			//DataAccess.Instance("BizDB").ExecuteNonQuery(sqlstr);
		}
		private void bindTeacherList()
		{
            string sql = "select EXT04 FROM ORG_USER  where ISACTIVE=1 ";
			DataTable dtFinaInfo = DataAccess.Instance("BizDB").ExecuteDataTable(sql);
			dropTrainingTeacher.DataTextField = "EXT04";
			dropTrainingTeacher.DataValueField = "EXT04";
			dropTrainingTeacher.DataSource = dtFinaInfo;
			dropTrainingTeacher.DataBind();
            dropTrainingTeacher.Items.Insert(dtFinaInfo.Rows.Count, new ListItem("outsourcing", "outsourcing"));
		}
		private void bindlist()
		{
			DataTable UserData = new DataTable();
            DataTable dtUserInfoManagement = DataAccess.Instance("BizDB").ExecuteDataSet("select u.EXT04 from ORG_USER u left join ORG_JOB j on u.USERID = j.USERID left join ORG_DEPARTMENT d on j.DEPARTMENTID = d.DEPARTMENTID where d.DEPARTMENTNAME = 'Management' and u.ISACTIVE=1").Tables[0];
            DataTable dtUserInfoIT = DataAccess.Instance("BizDB").ExecuteDataSet("select u.EXT04 from ORG_USER u left join ORG_JOB j on u.USERID = j.USERID left join ORG_DEPARTMENT d on j.DEPARTMENTID = d.DEPARTMENTID where d.DEPARTMENTNAME = 'IT'  and u.ISACTIVE=1").Tables[0];
            DataTable dtUserInfoHR = DataAccess.Instance("BizDB").ExecuteDataSet("select u.EXT04 from ORG_USER u left join ORG_JOB j on u.USERID = j.USERID left join ORG_DEPARTMENT d on j.DEPARTMENTID = d.DEPARTMENTID where d.DEPARTMENTNAME = 'HR'  and u.ISACTIVE=1").Tables[0];
            DataTable dtUserInfoADM = DataAccess.Instance("BizDB").ExecuteDataSet("select u.EXT04 from ORG_USER u left join ORG_JOB j on u.USERID = j.USERID left join ORG_DEPARTMENT d on j.DEPARTMENTID = d.DEPARTMENTID where d.DEPARTMENTNAME = 'Administration' and  u.ISACTIVE=1").Tables[0];
            DataTable dtUserInfoFIN = DataAccess.Instance("BizDB").ExecuteDataSet("select u.EXT04 from ORG_USER u left join ORG_JOB j on u.USERID = j.USERID left join ORG_DEPARTMENT d on j.DEPARTMENTID = d.DEPARTMENTID where d.DEPARTMENTNAME = 'Finance'  and u.ISACTIVE=1").Tables[0];
            DataTable dtUserInfoQA = DataAccess.Instance("BizDB").ExecuteDataSet("select u.EXT04 from ORG_USER u left join ORG_JOB j on u.USERID = j.USERID left join ORG_DEPARTMENT d on j.DEPARTMENTID = d.DEPARTMENTID where d.DEPARTMENTNAME = 'Quality'  and u.ISACTIVE=1").Tables[0];
            DataTable dtUserInfoENG = DataAccess.Instance("BizDB").ExecuteDataSet("select u.EXT04 from ORG_USER u left join ORG_JOB j on u.USERID = j.USERID left join ORG_DEPARTMENT d on j.DEPARTMENTID = d.DEPARTMENTID where d.DEPARTMENTNAME = 'Engineering'  and u.ISACTIVE=1").Tables[0];

            DataTable dtUserInfoPUR = DataAccess.Instance("BizDB").ExecuteDataSet("select u.EXT04 from ORG_USER u left join ORG_JOB j on u.USERID = j.USERID left join ORG_DEPARTMENT d on j.DEPARTMENTID = d.DEPARTMENTID where (d.DEPARTMENTNAME = 'Purchase' or d.DEPARTMENTNAME = 'Supply Chain')  and u.ISACTIVE=1").Tables[0];
            DataTable dtUserInfoPM = DataAccess.Instance("BizDB").ExecuteDataSet("select u.EXT04 from ORG_USER u left join ORG_JOB j on u.USERID = j.USERID left join ORG_DEPARTMENT d on j.DEPARTMENTID = d.DEPARTMENTID where d.DEPARTMENTNAME = 'PM'  and u.ISACTIVE=1").Tables[0];
            DataTable dtUserInfoHSEF= DataAccess.Instance("BizDB").ExecuteDataSet("select u.EXT04 from ORG_USER u left join ORG_JOB j on u.USERID = j.USERID left join ORG_DEPARTMENT d on j.DEPARTMENTID = d.DEPARTMENTID where d.DEPARTMENTNAME = 'HSEF'  and u.ISACTIVE=1").Tables[0];
            DataTable dtUserInfoMarketing = DataAccess.Instance("BizDB").ExecuteDataSet("select u.EXT04 from ORG_USER u left join ORG_JOB j on u.USERID = j.USERID left join ORG_DEPARTMENT d on j.DEPARTMENTID = d.DEPARTMENTID where d.DEPARTMENTNAME = 'Marketing'  and u.ISACTIVE=1").Tables[0];

			UserData = new DataTable();
			UserData.Columns.Add("USER1", typeof(string));
			UserData.Columns.Add("USER2", typeof(string));
			UserData.Columns.Add("USER3", typeof(string));
			UserData.Columns.Add("USER4", typeof(string));
			UserData.Columns.Add("USER5", typeof(string));
			//UserData.Columns.Add("USER6", typeof(string));
			if (dtUserInfoManagement.Rows.Count > 0)
			{
				int UserCount = dtUserInfoManagement.Rows.Count;
				for (int i = 0; i < UserCount; i++)
				{
					 UserData.Rows.Add(UserData.NewRow());
					 DataRow DRUSER = UserData.Rows[(i/5)];
					 if ((UserCount - 1) >=i) DRUSER["USER1"] = dtUserInfoManagement.Rows[i]["EXT04"];
					 if ((UserCount - 1) > i) DRUSER["USER2"] = dtUserInfoManagement.Rows[++i]["EXT04"];
					 if ((UserCount - 1) > i) DRUSER["USER3"] = dtUserInfoManagement.Rows[++i]["EXT04"];
					if ((UserCount - 1) > i) DRUSER["USER4"] = dtUserInfoManagement.Rows[++i]["EXT04"];
					if ((UserCount - 1) > i) DRUSER["USER5"] = dtUserInfoManagement.Rows[++i]["EXT04"];
					//if ((UserCount - 1) > i) DRUSER["USER6"] = dtUserInfo.Rows[++i]["EXT04"];
				}
			}
			RepeaterManager.DataSource = UserData;
			RepeaterManager.DataBind();
			
			UserData = new DataTable();
			UserData.Columns.Add("USER1", typeof(string));
			UserData.Columns.Add("USER2", typeof(string));
			UserData.Columns.Add("USER3", typeof(string));
			UserData.Columns.Add("USER4", typeof(string));
			UserData.Columns.Add("USER5", typeof(string));
			//UserData.Columns.Add("USER6", typeof(string));
			if (dtUserInfoPM.Rows.Count > 0)
			{
				int UserCount = dtUserInfoPM.Rows.Count;
				for (int i = 0; i < UserCount; i++)
				{
					UserData.Rows.Add(UserData.NewRow());
					DataRow DRUSER = UserData.Rows[(i / 5)];
					if ((UserCount - 1) > i) DRUSER["USER1"] = dtUserInfoPM.Rows[i]["EXT04"];
					if ((UserCount - 1) > i) DRUSER["USER2"] = dtUserInfoPM.Rows[++i]["EXT04"];
					if ((UserCount - 1) > i) DRUSER["USER3"] = dtUserInfoPM.Rows[++i]["EXT04"];
					if ((UserCount - 1) > i) DRUSER["USER4"] = dtUserInfoPM.Rows[++i]["EXT04"];
					if ((UserCount - 1) > i) DRUSER["USER5"] = dtUserInfoPM.Rows[++i]["EXT04"];
					//if ((UserCount - 1) > i) DRUSER["USER6"] = dtUserInfo.Rows[++i]["EXT04"];
				}
			}
			RepeaterPM.DataSource = UserData;
			RepeaterPM.DataBind();
			UserData = new DataTable();
			UserData.Columns.Add("USER1", typeof(string));
			UserData.Columns.Add("USER2", typeof(string));
			UserData.Columns.Add("USER3", typeof(string));
			UserData.Columns.Add("USER4", typeof(string));
			UserData.Columns.Add("USER5", typeof(string));
			if (dtUserInfoIT.Rows.Count > 0)
			{
				int UserCount = dtUserInfoIT.Rows.Count;
				for (int i = 0; i < UserCount; i++)
				{
					UserData.Rows.Add(UserData.NewRow());
					DataRow DRUSER = UserData.Rows[(i / 5)];
					if ((UserCount - 1) > i) DRUSER["USER1"] = dtUserInfoIT.Rows[i]["EXT04"];
					if ((UserCount - 1) > i) DRUSER["USER2"] = dtUserInfoIT.Rows[++i]["EXT04"];
					if ((UserCount - 1) > i) DRUSER["USER3"] = dtUserInfoIT.Rows[++i]["EXT04"];
					if ((UserCount - 1) > i) DRUSER["USER4"] = dtUserInfoIT.Rows[++i]["EXT04"];
					if ((UserCount - 1) > i) DRUSER["USER5"] = dtUserInfoIT.Rows[++i]["EXT04"];
					//if ((UserCount - 1) > i) DRUSER["USER6"] = dtUserInfo.Rows[++i]["EXT04"];
				}
			}
			RepeaterIT.DataSource = UserData;
			RepeaterIT.DataBind();
			UserData = new DataTable();
			UserData.Columns.Add("USER1", typeof(string));
			UserData.Columns.Add("USER2", typeof(string));
			UserData.Columns.Add("USER3", typeof(string));
			UserData.Columns.Add("USER4", typeof(string));
			UserData.Columns.Add("USER5", typeof(string));
			if (dtUserInfoHR.Rows.Count > 0)
			{
				int UserCount = dtUserInfoHR.Rows.Count;
				for (int i = 0; i < UserCount; i++)
				{
					UserData.Rows.Add(UserData.NewRow());
					DataRow DRUSER = UserData.Rows[(i / 5)];
					DRUSER[i % 5] = dtUserInfoHR.Rows[i]["EXT04"];
					//if ((UserCount - 1) >= i) DRUSER["USER1"] = dtUserInfoHR.Rows[i]["EXT04"];
					//if ((UserCount - 1) >= i) DRUSER["USER2"] = dtUserInfoHR.Rows[++i]["EXT04"];
					//if ((UserCount - 1) >= i) DRUSER["USER3"] = dtUserInfoHR.Rows[++i]["EXT04"];
					//if ((UserCount - 1) >= i) DRUSER["USER4"] = dtUserInfoHR.Rows[++i]["EXT04"];
					//if ((UserCount - 1) >= i) DRUSER["USER5"] = dtUserInfoHR.Rows[++i]["EXT04"];
					 
				}
			}
			RepeaterHR.DataSource = UserData;
			RepeaterHR.DataBind();
			UserData = new DataTable();
			UserData.Columns.Add("USER1", typeof(string));
			UserData.Columns.Add("USER2", typeof(string));
			UserData.Columns.Add("USER3", typeof(string));
			UserData.Columns.Add("USER4", typeof(string));
			UserData.Columns.Add("USER5", typeof(string));
			if (dtUserInfoADM.Rows.Count > 0)
			{
				int UserCount = dtUserInfoADM.Rows.Count;
				for (int i = 0; i < UserCount; i++)
				{
					UserData.Rows.Add(UserData.NewRow());
					DataRow DRUSER = UserData.Rows[(i / 5)];
					DRUSER[i % 5] = dtUserInfoADM.Rows[i]["EXT04"];
					//if ((UserCount - 1) >= i) DRUSER["USER1"] = dtUserInfoADM.Rows[i]["EXT04"];
					//if ((UserCount - 1) >= i) DRUSER["USER2"] = dtUserInfoADM.Rows[++i]["EXT04"];
					//if ((UserCount - 1) >= i) DRUSER["USER3"] = dtUserInfoADM.Rows[++i]["EXT04"];
					//if ((UserCount - 1) >= i) DRUSER["USER4"] = dtUserInfoADM.Rows[++i]["EXT04"];
					//if ((UserCount - 1) >= i) DRUSER["USER5"] = dtUserInfoADM.Rows[++i]["EXT04"];
					//if ((UserCount - 1) > i) DRUSER["USER6"] = dtUserInfo.Rows[++i]["EXT04"];
				}
			}
			RepeaterADM.DataSource = UserData;
			RepeaterADM.DataBind();
			UserData = new DataTable();
			UserData.Columns.Add("USER1", typeof(string));
			UserData.Columns.Add("USER2", typeof(string));
			UserData.Columns.Add("USER3", typeof(string));
			UserData.Columns.Add("USER4", typeof(string));
			UserData.Columns.Add("USER5", typeof(string));
			if (dtUserInfoFIN.Rows.Count > 0)
			{
				int UserCount = dtUserInfoFIN.Rows.Count;
				for (int i = 0; i < UserCount; i++)
				{
					UserData.Rows.Add(UserData.NewRow());
					DataRow DRUSER = UserData.Rows[(i / 5)];
					if ((UserCount - 1) > i) DRUSER["USER1"] = dtUserInfoFIN.Rows[i]["EXT04"];
					if ((UserCount - 1) > i) DRUSER["USER2"] = dtUserInfoFIN.Rows[++i]["EXT04"];
					if ((UserCount - 1) > i) DRUSER["USER3"] = dtUserInfoFIN.Rows[++i]["EXT04"];
					if ((UserCount - 1) > i) DRUSER["USER4"] = dtUserInfoFIN.Rows[++i]["EXT04"];
					if ((UserCount - 1) > i) DRUSER["USER5"] = dtUserInfoFIN.Rows[++i]["EXT04"];
					//if ((UserCount - 1) > i) DRUSER["USER6"] = dtUserInfo.Rows[++i]["EXT04"];
				}
			}
			RepeaterFIN.DataSource = UserData;
			RepeaterFIN.DataBind();
			UserData = new DataTable();
			UserData.Columns.Add("USER1", typeof(string));
			UserData.Columns.Add("USER2", typeof(string));
			UserData.Columns.Add("USER3", typeof(string));
			UserData.Columns.Add("USER4", typeof(string));
			UserData.Columns.Add("USER5", typeof(string));
			if (dtUserInfoQA.Rows.Count > 0)
			{
				int UserCount = dtUserInfoQA.Rows.Count;
				for (int i = 0; i < UserCount; i++)
				{
					UserData.Rows.Add(UserData.NewRow());
					DataRow DRUSER = UserData.Rows[(i / 5)];
                    //if ((UserCount - 1) > i) DRUSER["USER1"] = dtUserInfoQA.Rows[i]["EXT04"];
                    //if ((UserCount - 1) > i) DRUSER["USER2"] = dtUserInfoQA.Rows[++i]["EXT04"];
                    //if ((UserCount - 1) > i) DRUSER["USER3"] = dtUserInfoQA.Rows[++i]["EXT04"];
                    //if ((UserCount - 1) > i) DRUSER["USER4"] = dtUserInfoQA.Rows[++i]["EXT04"];
                    //if ((UserCount - 1) > i) DRUSER["USER5"] = dtUserInfoQA.Rows[++i]["EXT04"];
                    DRUSER[i % 5] = dtUserInfoQA.Rows[i]["EXT04"];
					//if ((UserCount - 1) > i) DRUSER["USER6"] = dtUserInfo.Rows[++i]["EXT04"];

                  
				}
			}
			RepeaterQA.DataSource = UserData;
			RepeaterQA.DataBind();
			UserData = new DataTable();
			UserData.Columns.Add("USER1", typeof(string));
			UserData.Columns.Add("USER2", typeof(string));
			UserData.Columns.Add("USER3", typeof(string));
			UserData.Columns.Add("USER4", typeof(string));
			UserData.Columns.Add("USER5", typeof(string));
			if (dtUserInfoENG.Rows.Count > 0)
			{
				int UserCount = dtUserInfoENG.Rows.Count;
				for (int i = 0; i < UserCount; i++)
				{
					UserData.Rows.Add(UserData.NewRow());
					DataRow DRUSER = UserData.Rows[(i / 5)];
					DRUSER[i % 5] = dtUserInfoENG.Rows[i]["EXT04"];
				}
			}
			RepeaterENG.DataSource = UserData;
			RepeaterENG.DataBind();
			UserData = new DataTable();
			UserData.Columns.Add("USER1", typeof(string));
			UserData.Columns.Add("USER2", typeof(string));
			UserData.Columns.Add("USER3", typeof(string));
			UserData.Columns.Add("USER4", typeof(string));
			UserData.Columns.Add("USER5", typeof(string));
			if (dtUserInfoPUR.Rows.Count > 0)
			{
				int UserCount = dtUserInfoPUR.Rows.Count;
				for (int i = 0; i < UserCount; i++)
				{
					UserData.Rows.Add(UserData.NewRow());
					DataRow DRUSER = UserData.Rows[(i / 5)];
					if ((UserCount - 1) >= i) DRUSER["USER1"] = dtUserInfoPUR.Rows[i]["EXT04"];
					if ((UserCount - 1) > i) DRUSER["USER2"] = dtUserInfoPUR.Rows[++i]["EXT04"];
					if ((UserCount - 1) > i) DRUSER["USER3"] = dtUserInfoPUR.Rows[++i]["EXT04"];
					if ((UserCount - 1) > i) DRUSER["USER4"] = dtUserInfoPUR.Rows[++i]["EXT04"];
					if ((UserCount - 1) > i) DRUSER["USER5"] = dtUserInfoPUR.Rows[++i]["EXT04"];
					//if ((UserCount - 1) > i) DRUSER["USER6"] = dtUserInfo.Rows[++i]["EXT04"];
				}
			}
			RepeaterPUR.DataSource = UserData;
			RepeaterPUR.DataBind();

            UserData = new DataTable();
            UserData.Columns.Add("USER1", typeof(string));
            UserData.Columns.Add("USER2", typeof(string));
            UserData.Columns.Add("USER3", typeof(string));
            UserData.Columns.Add("USER4", typeof(string));
            UserData.Columns.Add("USER5", typeof(string));
            if(dtUserInfoHSEF.Rows.Count > 0)
            {
                int UserCount = dtUserInfoHSEF.Rows.Count;
                for (int i = 0; i < UserCount; i++)
                {
                    UserData.Rows.Add(UserData.NewRow());
                    DataRow DRUSER = UserData.Rows[(i / 5)];
                    DRUSER[i % 5] = dtUserInfoHSEF.Rows[i]["EXT04"];
                }
            }
           RepeaterHSEF.DataSource = UserData;
           RepeaterHSEF.DataBind();




            UserData = new DataTable();
            UserData.Columns.Add("USER1", typeof(string));
            UserData.Columns.Add("USER2", typeof(string));
            UserData.Columns.Add("USER3", typeof(string));
            UserData.Columns.Add("USER4", typeof(string));
            UserData.Columns.Add("USER5", typeof(string));
            if (dtUserInfoMarketing.Rows.Count > 0)
            {
                int UserCount = dtUserInfoMarketing.Rows.Count;
                for (int i = 0; i < UserCount; i++)
                {
                    UserData.Rows.Add(UserData.NewRow());
                    DataRow DRUSER = UserData.Rows[(i / 5)];
                    DRUSER[i % 5] = dtUserInfoMarketing.Rows[i]["EXT04"];
                }
            }
            Marketing.DataSource = UserData;
            Marketing.DataBind();

            

			//adapter.Fill(set, "ntable");
		}
        protected void btnRevoke_Click(object sender, EventArgs e)//撤销
        {
            object ProcessName = Request.QueryString["Processname"];
            object Incident = Request.QueryString["Incident"];
            object StepName = Request.QueryString["StepName"];
            string strRevoke = string.Format("select status from  dbo.PROC_REVOKE with(nolock) where ProcessName='{0}' and incident='{1}'", ProcessName.ToString(), Incident.ToString());
            string FlagRevoke = DataAccess.Instance("BizDB").ExecuteScalar(strRevoke).ToString();
            if (FlagRevoke != "2")
            {
                if (GetOrgLevel.RevokeFunc(ProcessName.ToString(), StepName.ToString(), Incident.ToString(), Page.User.Identity.Name))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "RevokSuccess()", true);

                }
                else
                {
                    MessageBox.Show(this.Page, "撤回失败！\\nRevoke Faile!");
                }
            }
            else
            {
                MessageBox.Show(this.Page, "任务已经被处理，无法撤回！\\n Task Already Pass, Don't Revoke!");
            }
        }
	}
}