using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls; 
using MyLib;
using System.Threading;
using Ultimus.UWF.OrgChart.Entity;
using Ultimus.UWF.OrgChart.Interface;
using Ultimus.UWF.Common.Logic;
using Ultimus.UWF.OrgChart.Implementation;

namespace Ultimus.UWF.OrgChart
{
    public partial class UserDetail : System.Web.UI.Page
    {  
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtJobId.Text = Request.QueryString["JOBID"];
                txtDepartmentID.Text = Request.QueryString["DepartmentID"];
                txtDepartmentName.Text = Request.QueryString["DepartmentName"];
                if (!string.IsNullOrEmpty(txtJobId.Text))
                {
                    LoadEntity(txtJobId.Text);
                }

                txtDirectReportName.Attributes.Add("readonly", "true");
                txtDepartmentName.Attributes.Add("readonly", "true");
                txtUSERID.Attributes.Add("readonly", "true");
            }

        }

        IOrg logic = ServiceContainer.Instance().GetService<IOrg>();
        void LoadEntity(string jobID)
        {
            UserEntity user = new UserEntity();
            JobEntity job = new JobEntity();
            job = logic.GetJobEntity(jobID);
            user = logic.GetUserEntityByJob(jobID);
            //user
            txtUSERCODE.Text = user.USERCODE;
            txtTel.Text = user.TEL;
            txtCNNAME.Text = user.USERNAME;
            txtQQ.Text = user.IM;
            txtOrderNo.Text = user.ORDERNO.ToString();
            txtLoginName.Text = user.LOGINNAME;
            //txtENNAME.Text = user.ENNAME;
            txtEmail.Text = user.EMAIL;
            cbxIsActive.Checked=user.ISACTIVE=="1"?true:false;
            txtExt01.Text = user.EXT01;
            txtExt02.Text = user.EXT02;
            txtExt03.Text = user.EXT03;
            txtExt04.Text = user.EXT04;
            txtExt05.Text = user.EXT05;
            txtExt06.Text = user.EXT06;
            txtUSERID.Text = user.USERID.ToString();
            //job
            ddlJobGrade.SelectedValue = job.JOBGRADE;
            txtJobFunction.Text = job.JOBFUNCTION;
            txtDirectReportID.Text = job.SUPERVISORJOBID.ToString();
            txtDepartmentID.Text = job.DEPARTMENTID.ToString();
            DepartmentEntity dept=logic.GetDepartmentEntity(job.DEPARTMENTID.Value);
            if (dept != null)
            {
                txtDepartmentName.Text = dept.DEPARTMENTNAME;
            }
            JobEntity jj = logic.GetJobEntity(job.SUPERVISORJOBID.ToString());
            if (jj != null)
            {
                UserEntity uu = logic.GetUserEntityByJob(jj.JOBID.ToString());
                if (uu != null)
                {
                    txtDirectReportName.Text = uu.USERNAME; 
                }
            }
            cbxIsPrimary.Checked = job.ISPRIMARY == "1" ? true : false;
            cbxIsManager.Checked = job.ISMANAGER == "1" ? true : false;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserManagement.aspx");
        }

        protected void lbSelectUser_Click(object sender, EventArgs e)
        {
            UserEntity user = logic.GetUserEntityByID(ConvertUtil.ToInt32( txtUSERID.Text.Replace("|USER","")));
            if (user == null)
            {
                return;
            }

            //user
            txtUSERCODE.Text = user.USERCODE;
            txtTel.Text = user.TEL;
            txtCNNAME.Text = user.USERNAME;
            txtQQ.Text = user.IM;
            txtOrderNo.Text = user.ORDERNO.ToString();
            txtLoginName.Text = user.LOGINNAME;
            //txtENNAME.Text = user.ENNAME;
            txtEmail.Text = user.EMAIL;
            cbxIsActive.Checked = user.ISACTIVE == "1" ? true : false;
            txtExt01.Text = user.EXT01;
            txtExt02.Text = user.EXT02;
            txtExt03.Text = user.EXT03;
            txtExt04.Text = user.EXT04;
            txtExt05.Text = user.EXT05;
            txtExt06.Text = user.EXT06;
            txtUSERID.Text = user.USERID.ToString();
            cbxIsPrimary.Checked =  false;
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            SerialNoLogic sn = new SerialNoLogic();
            int userid = ConvertUtil.ToInt32(txtUSERID.Text);
            bool newflag = false;
            UserEntity user = new UserEntity();
            try
            {
                DatabaseOrgLogic db = new DatabaseOrgLogic();
                user = db.GetUserEntity(txtLoginName.Text);
            }
            catch
            {
            }
            if (userid == 0)
            {
                userid=sn.GetMaxNo("ORG_USER", "USERID");
            }
            if (user == null)
            {
                user = new UserEntity();
            }
            user.USERID = userid;
            user.USERCODE=txtUSERCODE.Text ;
            user.TEL=txtTel.Text;
            user.USERNAME=txtCNNAME.Text ;
            user.IM=txtQQ.Text;
            user.ORDERNO=ConvertUtil.ToInt32(txtOrderNo.Text) ;
            user.LOGINNAME=txtLoginName.Text  ; 
            user.EMAIL=txtEmail.Text ;
            user.ISACTIVE = cbxIsActive.Checked ? "1" : "0";
            user.EXT01 = txtExt01.Text;
            user.EXT02 = txtExt02.Text;
            user.EXT03 = txtExt03.Text;
            user.EXT04 = txtExt04.Text;
            user.EXT05 = txtExt05.Text;
            user.EXT06 = txtExt06.Text;

            JobEntity job = new JobEntity();
            job.JOBGRADE = ddlJobGrade.SelectedValue;
            job.JOBFUNCTION = txtJobFunction.Text;
            JobEntity jj=logic.GetJobEntityByUserID( ConvertUtil.ToInt32(txtDirectReportID.Text.Replace("|USER", "")).ToString());
            if (jj != null)
            {
                job.SUPERVISORJOBID = jj.JOBID;
            }
            job.DEPARTMENTID = ConvertUtil.ToInt32(txtDepartmentID.Text.Replace("|DEPT", ""));
            job.JOBID = ConvertUtil.ToInt32(txtJobId.Text);
            job.ISMANAGER = cbxIsManager.Checked ? "1" : "0";
            job.ISPRIMARY = cbxIsPrimary.Checked ? "1" : "0";
            job.USERID = user.USERID;
            if (job.JOBID == 0)
            {
                job.JOBID = sn.GetMaxNo("ORG_JOB", "JOBID");
                newflag = true;
            }

            if (newflag)
            {
                logic.Insert(user);
                logic.InsertJob(job);
            }
            else
            {
                logic.Update(user);
                logic.UpdateJob(job);
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('保存成功!');self.opener=null;window.open('', '_self');self.close();", true);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            btnSave_Click(sender, e);
            Response.Redirect("UserDetail.aspx?DepartmentID="+txtDepartmentID.Text+"&DepartmentName="+txtDepartmentName.Text);
        }
    }
}