using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presale.Process.Common
{
	public class MessageBox
	{
		private MessageBox()
		{
		}

		/// <summary>
		/// 显示消息提示对话框
		/// </summary>
		/// <param name="page">当前页面指针，一般为this</param>
		/// <param name="msg">提示信息</param>
		public static void Show(System.Web.UI.Page page, string msg)
		{
			page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
		}

		/// <summary>
		/// 控件点击 消息确认提示框
		/// </summary>
		/// <param name="page">当前页面指针，一般为this</param>
		/// <param name="msg">提示信息</param>
		public static void ShowConfirm(System.Web.UI.WebControls.WebControl Control, string msg)
		{
			//Control.Attributes.Add("onClick","if (!window.confirm('"+msg+"')){return false;}");
			Control.Attributes.Add("onclick", "return confirm('" + msg + "');");
		}

		/// <summary>
		/// 显示消息提示对话框，并进行页面跳转
		/// </summary>
		/// <param name="page">当前页面指针，一般为this</param>
		/// <param name="msg">提示信息</param>
		/// <param name="url">跳转的目标URL</param>
		public static void ShowAndRedirect(System.Web.UI.Page page, string msg, string url)
		{
			StringBuilder Builder = new StringBuilder();
			Builder.Append("<script language='javascript' defer>");
			Builder.AppendFormat("alert('{0}');", msg);
			Builder.AppendFormat("top.location.href='{0}'", url);
			Builder.Append("</script>");
			page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());

		}

		public static void ShowAndHref(System.Web.UI.Page page, string msg, string url)
		{
			StringBuilder Builder = new StringBuilder();
			Builder.Append("<script language='javascript' defer>");
			Builder.AppendFormat("alert('{0}');", msg);
			Builder.AppendFormat("window.location.href='{0}'", url);
			Builder.Append("</script>");
			page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());
		}

		/// <summary>
		/// 输出自定义脚本信息
		/// </summary>
		/// <param name="page">当前页面指针，一般为this</param>
		/// <param name="script">输出脚本</param>
		public static void ResponseScript(System.Web.UI.Page page, string script)
		{
			page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>" + script + "</script>");

		}

		/// <summary>
		/// 显示消息提示对话框,并写入异常日志
		/// </summary>
		/// <param name="page">当前页面指针，一般为this</param>
		/// <param name="msg">提示信息</param>
		/// <param name="ex">异常对象</param>
		/// MessageBox.Show(this.Page, "操作失败"+ex.Message, ex);
		public static void Show(System.Web.UI.Page page, string msg, Exception ex)
		{
			page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
			//CommonHelper.WriteLog(ex.ToString().Trim());
		}

		/// <summary>
		/// 提示发起任务成功，并关闭当前页面
		/// </summary>
		/// <param name="page">当前页面指针，一般为this</param>
		/// <param name="msg">提示信息</param>
		public static void ShowAndClose(System.Web.UI.Page page, string msg)
		{
			page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg + "');window.opener=null; window.open('','_self');window.close();</script>");
		}
	}
}
