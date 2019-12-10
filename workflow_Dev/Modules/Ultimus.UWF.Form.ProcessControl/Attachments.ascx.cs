using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.IO;
using Ultimus.UWF.Form.ProcessControl.Logic;
using Ultimus.UWF.Form.ProcessControl.Entity;
using MyLib;
using Ultimus.UWF.OrgChart.Interface;
using Ultimus.UWF.OrgChart.Entity;
using Ultimus.UWF.Common.Logic;

namespace Ultimus.UWF.Form.ProcessControl
{
	public partial class Attachments : System.Web.UI.UserControl
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

		public string Title
		{
			get
			{
				return lblTitle.Text;
			}
			set
			{
				lblTitle.Text = value;
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

		private AttachmentLogic logic = new AttachmentLogic();
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				try
				{
					string type = Request.QueryString["Type"];
					if (!string.IsNullOrEmpty(type))
					{
						if (type.ToUpper() == "MYAPPROVAL" || type.ToUpper() == "MYREQUEST") //已完成，不显示上传按钮
						{
							this.actionRow.Visible = false;
							this.uploadrow.Visible = false;
						}
					}
					BindAttachments();
					this.actionRow.Visible = ReadOnly;
				}
				catch (System.Exception ex)
				{
					MyLib.LogUtil.Error(ex);
					this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "message", "<script language='javascript' defer>alert('" + ex.Message.Replace("\n", " ").Replace("<br/>", " ").Replace("'", "") + "');</script>");
				}

				Button1.Text = ProcessControl.Resources.lang.Upload;
				lblTitle.Text = Lang.Get("Form_Expense_Attachment");
			}
		}


		void BindAttachments()
		{
			try
			{
				UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
				string processName = userInfo.ProcessName;
				int incident = ConvertUtil.ToInt32(userInfo.Incident);
				Repeater1.DataSource = logic.GetAttachmentsByFormID(userInfo.FormId, txtType.Text);
				Repeater1.DataBind();
			}
			catch (System.Exception ex)
			{
				MyLib.LogUtil.Error(ex);
				this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "message", "<script language='javascript' defer>alert('" + ex.Message.Replace("\n", " ").Replace("<br/>", " ").Replace("'", "") + "');</script>");
			}
		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			try
			{
				UserInfo userInfo = Page.FindControl("UserInfo1") as UserInfo;
				string processName = userInfo.ProcessName;
				int incident = ConvertUtil.ToInt32(userInfo.Incident);
				string stepName = userInfo.StepName;
				SerialNoLogic sn = new SerialNoLogic();
				AttachmentEntity item = new AttachmentEntity();
				item.ID = sn.GetMaxNo("WF_Attachment", "ID").ToString();
				item.ProcessName = processName;
				item.Incident = incident;
				item.UploadStepName = stepName;
				item.FileName = FilePath.FileName;
				item.FileType = item.FileName.Substring(item.FileName.LastIndexOf("."), item.FileName.Length - item.FileName.LastIndexOf("."));
				item.NewName = Guid.NewGuid().ToString() + "~" + item.FileName.Trim().Replace(item.FileType, "");
				item.FileSize = FilePath.FileContent.Length;
				//item.Status = "正常";
				item.CreateByBadge = SessionLogic.GetLoginName();
				IOrg _org = ServiceContainer.Instance().GetService<IOrg>();
				UserEntity user = _org.GetUserEntity(SessionLogic.GetLoginName());
				if (user != null)
				{
					item.CreateByName = user.USERNAME;
				}
				item.Comments = FileComments.Text;
				item.CreateDate = DateTime.Now;
				item.FORMID = userInfo.FormId; //表单唯一号
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
					// path =attp+ "/" + processName + "/";
				}
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);

				}
				//System.Net.WebClient webClient = new System.Net.WebClient();
				// webClient.UploadFile(path, FilePath.FileName);
				string str = path + item.NewName + item.FileType;
				FilePath.SaveAs(str);
				//userInfo.AttachmentPath = str.Replace("/","\\");
				//userInfo.AttachmentName = "http://win-tv39q3k9unt:2001/ultimusdoc/"+item.FileName;

				if (logic.UploadFile(item))
				{
					BindAttachments();
				}
				else
				{
					File.Delete(path);
					//MessageBox.Show(Page, "File Upload Error.");
				}
				FileComments.Text = "";
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