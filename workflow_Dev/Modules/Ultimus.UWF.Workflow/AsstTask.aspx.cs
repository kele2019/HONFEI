using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Workflow.Logic;
using MyLib;
using System.IO;
using Ultimus.UWF.Common.Logic;
using Ultimus.UWF.Workflow.Entity;
using Ultimus.UWF.OrgChart.Interface;
using Ultimus.UWF.OrgChart.Entity;
using Ultimus.UWF.Workflow.Interface;
using System.Data;

namespace Ultimus.UWF.Workflow
{
    public partial class AsstTask : System.Web.UI.Page
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtTaskId.Text = Request.QueryString["TaskID"];
                txtProcessName.Text =Server.UrlDecode( Request.QueryString["ProcessName"]);
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty( txtRemark.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ccc", "<script>alert('备注不能为空!');</script>");
                return;
            }

            if (SessionLogic.GetUltimusLoginName().Split('/')[0]=="Ultimus")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ccc", "<script>alert('没有权限抄送/协办!');</script>");
                return;
            }
            string[] users = txtUserId.Text.Split(',');
            if (users.Length == 0)
            {
                return;
            }
            ITask task = ServiceContainer.Instance().GetService<ITask>();
            ServerEntity server = new ServerEntity();
            server.SERVERNAME = "UltimusV8";
            server.DBNAME = "UltDB";
            task.SetServerEntity(server);
            IOrg org = ServiceContainer.Instance().GetService<IOrg>();
            int i = 0;
            bool flag = false;
            TaskEntity oldTask= task.GetTaskEntity(txtTaskId.Text);
            foreach (string user in users)
            {
                TaskEntity entity = new TaskEntity();
                entity.SERVERNAME = "UltimusV8";
                if (oldTask == null)
                {
                    return;
                }
                entity.ASSIGNEDTOUSER = oldTask.ASSIGNEDTOUSER;
                entity.SUMMARY = txtRemark.Text;
                entity.TASKID = GetStartTaskId(txtProcessName.Text);

                List<VarEntity> vars = new List<VarEntity>();
                VarEntity var=new VarEntity();
                var.Name = "AssistTaskID";
                var.Value = txtTaskId.Text;
                vars.Add(var);

                var = new VarEntity();
                var.Name = "AssistFormUrl";
                var.Value = ConfigurationManager.AppSettings["RootPath"] + "/Modules/Ultimus.UWF.Workflow/OpenForm.aspx?type=view&TaskId="+txtTaskId.Text;// task.GetTaskUrl(txtTaskId.Text, "", oldTask.ASSIGNEDTOUSER);
                vars.Add(var);

                var = new VarEntity();
                var.Name = "AssistTaskUser";
                var.Value = oldTask.ASSIGNEDTOUSER;
                vars.Add(var);

                UserEntity u = org.GetUserEntity(oldTask.ASSIGNEDTOUSER);
                var = new VarEntity();
                var.Name = "AssistTaskUserFullName";
                var.Value = u.USERNAME;
                vars.Add(var);

                var = new VarEntity();
                var.Name = "ReceiptTaskUser";
                var.Value = "USER:org=Ultimus,user=" + user;
                //var.Value =  user;
                vars.Add(var);

                var = new VarEntity();
                var.Name = "ReceiptTaskUserFullName";
                var.Value = txtUsers.Text.Split(',')[i];
                vars.Add(var);

                var = new VarEntity();
                var.Name = "AssistDateTime";
                var.Value = DateTime.Now.ToString();
                vars.Add(var);

                var = new VarEntity();
                var.Name = "AssistProcessName";
                var.Value = oldTask.PROCESSNAME;
                vars.Add(var);

                var = new VarEntity();
                var.Name = "AssistIncident";
                var.Value = oldTask.INCIDENT.ToString();
                vars.Add(var);

                var = new VarEntity();
                var.Name = "AssistStepLabel";
                var.Value = oldTask.STEPLABEL;
                vars.Add(var);

                var = new VarEntity();
                var.Name = "AssistComments";
                var.Value = txtRemark.Text;
                vars.Add(var);

                entity.VarList = vars;
                task.SubmitTask(entity);
                i++;
                flag = true;
            }

            SaveApprovalHistroy(txtTaskId.Text, txtRemark.Text, oldTask.ASSIGNEDTOUSER);

            if (!flag)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ccc", "<script>alert('发起流程失败!');</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ccc", "<script>alert('发起"+txtProcessName.Text+"成功!');window.close();</script>");
            }

        }

        public void SaveApprovalHistroy(  string taskId, string comments,
            string loginName)
        {

            string processName = "";
            int incident = 0;
            string stepLabel = "";
            DataTable dt = DataAccess.Instance("UltDB").ExecuteDataTable("select * from tasks with(nolock) where taskid='" + taskId + "'");
            if (dt.Rows.Count > 0)
            {
                processName = ConvertUtil.ToString(dt.Rows[0]["ProcessName"]);
                incident = ConvertUtil.ToInt32(dt.Rows[0]["Incident"]);
                stepLabel = ConvertUtil.ToString(dt.Rows[0]["StepLabel"]);
            }
            ApprovalHistoryEntity approval = new ApprovalHistoryEntity();
            IOrg _org = ServiceContainer.Instance().GetService<IOrg>();
            UserEntity user = _org.GetUserEntity(loginName);
             
                approval.Action = "送给【"+txtUsers.Text+"】"+txtProcessName.Text.Replace("流程","");
             
            approval.ApproveDate = DateTime.Now;
            approval.Approver = user.USERNAME;
            approval.Comments = comments;
            approval.Ext01 = user.USERNAME;
            approval.ProcessName = processName;
            approval.Incident = incident;
            approval.StepName = stepLabel == "" ? "提交" : stepLabel;
            ApprovalHistoryLogic ahl = new ApprovalHistoryLogic();
            //if (txtProcessName.Text.IndexOf("协办") >= 0)
            //{
                ahl.AddApprovalHistory(null, approval);
            //}
        }

        string GetStartTaskId(string processName)
        {
            object obj= DataAccess.Instance("UltDB").ExecuteScalar("select INITIATEID from INITIATE where PROCESSNAME='"+processName+"' order by PROCESSVERSION desc");
            return ConvertUtil.ToString(obj);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
         
    }
}