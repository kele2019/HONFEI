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
using Ultimus.WFServer;

namespace Ultimus.UWF.V8
{
    public partial class SaveTemplate : System.Web.UI.Page
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ccc", "<script>alert('模板名称不能为空!');</script>");
                return;
            }
            Task task = new Task();
            task.InitializeFromTaskId(txtTaskId.Text);
            string str = @"<TaskData xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns='http://schema.ultimus.com/采购审批/3/45168B'>
  <Global>
    <Applicant xmlns='http://schema.ultimus.com/采购审批/3/Types' xsi:nil='true' >sfsfdsf</Applicant>
    <RequestDate xmlns='http://schema.ultimus.com/采购审批/3/Types' xsi:nil='true' />
    <Department xmlns='http://schema.ultimus.com/采购审批/3/Types' xsi:nil='true' />
    <FormID xmlns='http://schema.ultimus.com/采购审批/3/Types' xsi:nil='true' />
    <DocumentNo xmlns='http://schema.ultimus.com/采购审批/3/Types' xsi:nil='true' />
    <Category1 xmlns='http://schema.ultimus.com/采购审批/3/Types' xsi:nil='true' />
    <Category2 xmlns='http://schema.ultimus.com/采购审批/3/Types' xsi:nil='true' />
    <TotalAmount xmlns='http://schema.ultimus.com/采购审批/3/Types' xsi:nil='true' />
    <SupplierName xmlns='http://schema.ultimus.com/采购审批/3/Types' xsi:nil='true' />
    <SupplierNo xmlns='http://schema.ultimus.com/采购审批/3/Types' xsi:nil='true' />
    <Comments xmlns='http://schema.ultimus.com/采购审批/3/Types' xsi:nil='true' />
    <CGY xmlns='http://schema.ultimus.com/采购审批/3/Types' xsi:nil='true' />
    <Summary xmlns='http://schema.ultimus.com/采购审批/3/Types' xsi:nil='true' />
    <bFalse xmlns='http://schema.ultimus.com/采购审批/3/Types'>false</bFalse>
  </Global>
  <SYS_PROCESSATTACHMENTS />
</TaskData>";
            string str1="";
            string error="";
            //task.GetStepSchema2(out str, out str1, out error);
            task.GetTaskXML(out str, out error);
           // task.SaveTemplate("WIN-2MTM13RFV5E/user4", txtRemark.Text, "", out error);

        }

   
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
         
    }
}