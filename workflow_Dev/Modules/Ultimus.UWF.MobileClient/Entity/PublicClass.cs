using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Text;

namespace DALLibrary
{
    public static class PublicClass
    {
        /// <summary>
        /// 输出提示信息到页面
        /// </summary>
        /// <param name="page"></param>
        /// <param name="Message"></param>
        public static void ShowMessage(Page page,string Message)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "SystemMessage", "<script language=\"javascript\" defer>alert(\"" + Message.Replace("\r\n", " ").Replace("\n", " ").Replace("<br/>", " ").Replace("'", "") + "\");</script>");
        }

        /// <summary>
        /// 输出日志到文本
        /// </summary>
        /// <param name="Message">输出内容</param>
        public static void WriteLogOfTxt(string Message)
        {
            try
            {
                string filePath = HttpContext.Current.Server.MapPath("SystemLog/" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".txt");
                FileInfo f = new FileInfo(filePath);
                if (!f.Exists)
                {
                    f.Create();
                }
                StreamWriter sw = f.AppendText();
                sw.WriteLine(DateTime.Now.ToString() + ":" + Message.Replace("\r\n", " ").Replace("\n", " "));
                sw.Flush();
                sw.Close();
            }
            catch { }
        }

    }
}