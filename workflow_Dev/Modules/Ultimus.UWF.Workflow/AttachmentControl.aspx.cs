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
using System.Data;

namespace Ultimus.UWF.Workflow
{
    public partial class AttachmentControl : System.Web.UI.Page
    {
        /// <summary>
        /// 是否可以上传
        /// </summary>
        // private bool ReadOnly;
        public bool ReadOnly
        {
            get { return uploadrow.Visible; }
            set
            {
                uploadrow.Visible = !value;
                if (value)
                {
                    txtReadonly.Text = "1";
                }
                else
                {
                    txtReadonly.Text = "0";
                }
            }
        }

        public bool Required
        {
            get
            {
                if (this.txtMust.Text == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    txtMust.Text = "1";
                }
                else
                {
                    txtMust.Text = "0";
                }
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

        public string ProcessName
        {
            get
            {
                return txtProcessName.Text;
            }
            set
            {
                txtProcessName.Text = value;
            }
        }

        public string Incident
        {
            get
            {
                return txtIncident.Text;
            }
            set
            {
                txtIncident.Text = value;
            }
        }

        private AttachmentLogic logic = new AttachmentLogic();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string type = Request.QueryString["Type"];
                    string readOnly = Request.QueryString["ReadOnly"];
                    string required = Request.QueryString["Required"];
                    txtProcessName.Text = Request.QueryString["ProcessName"];
                    txtIncident.Text = Request.QueryString["Incident"];
                    txtFormId.Text = Request.QueryString["FormID"];
                    txtTaskUser.Text = Request.QueryString["TaskUser"];
                    txtType.Text = type;
                    txtISWord.Text = Request.QueryString["ISWord"];
                    txtHasAtt.Text = "0";
                    if (readOnly == "1")
                    {
                        ReadOnly = true;
                    }
                    if (required == "1")
                    {
                        Required = true;
                    }
                    //if (!string.IsNullOrEmpty(type))
                    //{
                    //    if (type.ToUpper() == "MYAPPROVAL" || type.ToUpper() == "MYREQUEST") //已完成，不显示上传按钮
                    //    {
                    //        this.actionRow.Visible = false;
                    //        this.uploadrow.Visible = false;
                    //    }
                    //}
                    if (Convert.ToInt32(Request.QueryString["TaskStatus"]) !=1)
                    {
                         
                            ReadOnly = true;
                            txtReadonly.Text = "1";
                            readOnly = "1";
                         
                    }
                    BindAttachments();
                    this.actionRow.Visible = ReadOnly;
                }
                catch (System.Exception ex)
                {
                    MyLib.LogUtil.Error(ex);
                    //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "message", "<script language='javascript' defer>alert('" + ex.Message.Replace("\n", " ").Replace("<br/>", " ").Replace("'", "") + "');</script>");
                }

            }
        }


        void BindAttachments()
        {
            try
            {
                //UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
                //string processName = userInfo.ProcessName;
                //int incident = ConvertUtil.ToInt32(userInfo.Incident);
                DataTable dt= logic.GetAttachmentsByFormID(txtFormId.Text, txtType.Text);
                if (dt.Rows.Count > 0)
                {
                    txtHasAtt.Text = "1";
                }
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
                
            }
            catch (System.Exception ex)
            {
                MyLib.LogUtil.Error(ex);
                //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "message", "<script language='javascript' defer>alert('" + ex.Message.Replace("\n", " ").Replace("<br/>", " ").Replace("'", "") + "');</script>");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                //UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
                string processName = txtProcessName.Text;
                int incident = ConvertUtil.ToInt32(txtIncident.Text);
                string stepName = "";//userInfo.StepName;
                SerialNoLogic sn = new SerialNoLogic();
                AttachmentEntity item = new AttachmentEntity();
                item.ID = sn.GetMaxNo("WF_Attachment", "ID").ToString();
                item.ProcessName = processName;
                item.Incident = incident;
                item.UploadStepName = stepName;
                item.FileName = FilePath.FileName;
                item.FileType = item.FileName.Substring(item.FileName.LastIndexOf("."), item.FileName.Length - item.FileName.LastIndexOf("."));
                item.NewName = Guid.NewGuid().ToString() + "~" + item.FileName.Trim().Replace(item.FileType,"");
                item.FileSize = FilePath.FileContent.Length;
                //item.Status = "正常";
                item.CreateByBadge = "";// SessionLogic.GetLoginName();
                //IOrg _org = ServiceContainer.Instance().GetService<IOrg>();
                //UserEntity user = _org.GetUserEntity(SessionLogic.GetLoginName());
                //if (user != null)
                //{
                //    item.CreateByName = user.USERNAME;
                //}
                item.Comments = FileComments.Text;
                item.CreateDate = DateTime.Now;
                item.FORMID = txtFormId.Text; //表单唯一号
                item.TYPE = txtType.Text;

                string path = "";//
                string attp = MyLib.ConfigurationManager.AppSettings["AttachmentPath"];
                if (attp.StartsWith("\\"))
                {
                    path = attp + "\\" + processName + "\\";
                }
                else
                {
                    path = Server.MapPath(attp) + "/" + processName + "/";
                }

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);

                }
                string str = path + item.NewName + item.FileType;

                if(txtISWord.Text=="1" && item.FileType.ToLower()!=".docx")
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "message", "<script language='javascript' defer>alert('后缀名必须为docx!');</script>");
                    return;
                }
                FilePath.SaveAs(str);
                txtHasAtt.Text = "1";

                if (logic.UploadFile(item))
                {
                    BindAttachments();
                }
                else
                {
                    File.Delete(path);
                }
                FileComments.Text = "";

                if (txtISWord.Text == "1")
                {
                    //System.Net.NetworkCredential nc = new System.Net.NetworkCredential("ultimus", "bpm@123", "quanyou");
                    //EipService.UltimusDocService ud = new EipService.UltimusDocService();
                    //ud.Credentials = nc;
                    // FileInfo fi = new FileInfo(str);
                    //long len = fi.Length;
                    //FileStream fs = new FileStream(str, FileMode.Open);
                    //byte [] buffer = new byte[len];
                    //fs.Read(buffer, 0, (int)len);
                    //fs.Close(); 

                }
            }
            catch (Exception ex)
            {
                MyLib.LogUtil.Error(ex);
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "message", "<script language='javascript' defer>alert('" + ex.Message.Replace("\n", " ").Replace("<br/>", " ").Replace("'", "") + "');</script>");
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Download")
                {
                    try
                    {
                        logic.DownloadByID(this.Page, e.CommandArgument.ToString());
                    }
                    catch (Exception ex)
                    {
                        LogUtil.Error(ex);
                    }
                }
                if (e.CommandName == "Delete")
                {
                    try
                    {
                        if (logic.DeleteAttachmentsByID(e.CommandArgument.ToString()))
                        {
                            BindAttachments();
                        }
                        else
                        {
                            throw new Exception("Delete Attachments Error.");
                        }
                    }
                    catch (Exception ex)
                    {
                        LogUtil.Error(ex);
                    }
                }
            }
            catch (System.Exception ex)
            {
                MyLib.LogUtil.Error(ex);
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "message", "<script language='javascript' defer>alert('" + ex.Message.Replace("\n", " ").Replace("<br/>", " ").Replace("'", "") + "');</script>");
            }
        }
    }
}