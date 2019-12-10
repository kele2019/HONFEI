using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using MyLib;
using System.IO;

namespace Presale.Process.Common
{
    public static class EmailHelp
    {

        public static bool SendMail(string strMailTo, string strSubject, string strBody, bool blIsHtml)
        {
            //if (ConfigHelper.GetConfigString("SendMailSwitch").ToUpper() == "OFF")
            //{
            //    return false;
            //}

            if (!string.IsNullOrEmpty(strMailTo))
            {
                using (MailMessage message = new MailMessage())
                {
                    string strSendEmailAddress = System.Configuration.ConfigurationManager.AppSettings["SendEmailAddress"];// ConfigHelper.GetConfigString("SendEmailAddress"); 
                    string strSendEmailDisplayName = System.Configuration.ConfigurationManager.AppSettings["SendEmailDisplayName"];// ConfigHelper.GetConfigString("SendEmailDisplayName");
                    message.From = new MailAddress(strSendEmailAddress, strSendEmailDisplayName, System.Text.Encoding.UTF8);

                    string[] strEmailSingle = strMailTo.Split(';');
                    for (int k = 0; k < strEmailSingle.Length; k++)
                    {
                        message.To.Add(new MailAddress(strEmailSingle[k].Trim())); //收件人邮箱
                    }

                    message.Subject = strSubject;
                    message.Body = strBody;
                    message.IsBodyHtml = blIsHtml;
                    message.Priority = MailPriority.High;   //发送邮件的优先等级

                    try
                    {
                        SmtpClient mailClient = new SmtpClient();
                        if (System.Configuration.ConfigurationManager.AppSettings["EmailFlag"].ToLower() == "true")
                            mailClient.EnableSsl = true;
                        mailClient.Send(message);
                    }
                    catch (Exception ex)
                    {
                        string _str = ex.Message;
                        EmailHelp.AppendTextToLog(ex.Message, "D:\\V2.0.5\\log", "EmailLog1.txt");
                        return false;
                        //throw ex;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        //public static bool SendMail(string strMailTo, string strSubject, string strBody, bool blIsHtml, string cc)
        //{
        //    //if (ConfigHelper.GetConfigString("SendMailSwitch").ToUpper() == "OFF")
        //    //{
        //    //    return false;
        //    //}

        //    if (!string.IsNullOrEmpty(strMailTo))
        //    {
        //        using (MailMessage message = new MailMessage())
        //        {
        //            string strSendEmailAddress = ConfigurationManager.AppSettings["SendEmailAddress"];// ConfigHelper.GetConfigString("SendEmailAddress"); 
        //            string strSendEmailDisplayName = ConfigurationManager.AppSettings["SendEmailDisplayName"];// ConfigHelper.GetConfigString("SendEmailDisplayName");
        //            message.From = new MailAddress(strSendEmailAddress, strSendEmailDisplayName, System.Text.Encoding.UTF8);
        //            if (String.IsNullOrEmpty(cc))
        //            {
        //                cc += ConfigurationManager.AppSettings["CC"].ToString();
        //            }
        //            else
        //            {
        //                cc += ";" + ConfigurationManager.AppSettings["CC"].ToString();
        //            }
        //            string[] strEmailSingle = strMailTo.Split(';');
        //            for (int k = 0; k < strEmailSingle.Length; k++)
        //            {
        //                message.To.Add(new MailAddress(strEmailSingle[k].Trim())); //收件人邮箱
        //            }
        //            string[] strEmailSingleCC = cc.Split(';');
        //            for (int j = 0; j < strEmailSingleCC.Length; j++)
        //            {
        //                message.CC.Add(new MailAddress(strEmailSingleCC[j].Trim())); //CC邮箱
        //            }
        //            message.Subject = strSubject;
        //            message.Body = strBody;
        //            message.IsBodyHtml = blIsHtml;
        //            message.Priority = MailPriority.High;   //发送邮件的优先等级

        //            try
        //            {
        //                SmtpClient mailClient = new SmtpClient();
        //                if (ConfigurationManager.AppSettings["EmailFlag"].ToLower() == "true")
        //                    mailClient.EnableSsl = true;
        //                mailClient.Send(message);
        //            }
        //            catch (Exception ex)
        //            {
        //                string _str = ex.Message;
        //                EmailHelp.AppendTextToLog(ex.Message, "D:\\V2.0.5\\log", "EmailLog1.txt");
        //                return false;
        //                //throw ex;
        //            }
        //        }
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public static bool SendMailpath(string strMailTo, string strSubject, string strBody, bool blIsHtml, string path)
        {
            if (!string.IsNullOrEmpty(strMailTo))
            {
                using (MailMessage message = new MailMessage())
                {
                    string strSendEmailAddress = ConfigurationManager.AppSettings["SendEmailAddress"];// ConfigHelper.GetConfigString("SendEmailAddress"); 
                    string strSendEmailDisplayName = ConfigurationManager.AppSettings["SendEmailDisplayName"];// ConfigHelper.GetConfigString("SendEmailDisplayName");
                    message.From = new MailAddress(strSendEmailAddress, strSendEmailDisplayName, System.Text.Encoding.UTF8);

                    string[] strEmailSingle = strMailTo.Split(';');
                    for (int k = 0; k < strEmailSingle.Length; k++)
                    {
                        if (strEmailSingle[k].Trim()!="")
                        message.To.Add(new MailAddress(strEmailSingle[k].Trim())); //收件人邮箱
                    }

                    message.Subject = strSubject;
                    message.Body = strBody;
                    message.IsBodyHtml = blIsHtml;
                    message.Priority = MailPriority.High;   //发送邮件的优先等级
                    message.Attachments.Add(new Attachment(path));//增加附件
                    try
                    {
                        SmtpClient mailClient = new SmtpClient();
                        if (ConfigurationManager.AppSettings["EmailFlag"].ToLower() == "true")
                            mailClient.EnableSsl = true;
                        mailClient.Send(message);
                    }
                    catch (Exception ex)
                    {
                        string _str = ex.Message;
                        EmailHelp.AppendTextToLog(ex.Message, "D:\\V2.0.5\\log", "EmailLog1.txt");
                        // File.Delete(path);//发送错误,删除附件
                        return false;
                        //throw ex;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 表名未定义
        /// </summary>
        public partial class TaskBoxes
        {

            ///<summary>
            ///空的描述
            ///</summary>
            public Int64 Id { get; set; }

            ///<summary>
            ///空的描述
            ///</summary>
            public String TaskLineId { get; set; }

            ///<summary>
            ///空的描述
            ///</summary>
            public String PushTitle { get; set; }

            ///<summary>
            ///空的描述
            ///</summary>
            public String PushContent { get; set; }

            ///<summary>
            ///空的描述
            ///</summary>
            public String PushEmail { get; set; }

            ///<summary>
            ///空的描述
            ///</summary>
            public Int16 PushState { get; set; }
            ///<summary>
            ///空的描述
            ///</summary>
            public string PushStateStr { get; set; }

            public Int16 TaskAway { get; set; }

            ///<summary>
            ///空的描述
            ///</summary>
            public System.DateTime? CreateDate { get; set; }

        }


//        public static bool SendMail(string strMailTo, string strSubject, string strBody, bool blIsHtml)
//        {
//            try
//            {
//                strBody = strBody.Replace("'", "&apos;");
//                string sqlCommandText = @" Insert Into TaskBoxes([TaskLineId],[PushTitle],[PushContent],[PushEmail],[PushState],[CreateDate],[TaskAway])
//Values('JOG-001-T1','" + strSubject + "','" + strBody + "','" + strMailTo + "','0','" + DateTime.Now + "','0')";
//                DataAccess.Instance("PushDB").ExecuteNonQuery(sqlCommandText);
//            }
//            catch (Exception ex)
//            {
//                string _str = ex.Message;
//                EmailHelp.AppendTextToLog(ex.Message, "D:\\V2.0.5\\log", "EmailLog1.txt");
//                return false;
//                //throw ex;
//            }
//            return true;
//        }

        public static bool SendMail(string strMailTo, string strSubject, string strBody, bool blIsHtml, string cc)
        {
            try
            {
                strBody = strBody.Replace("'", "&apos;");
                string sqlCommandText = @" Insert Into TaskBoxes([TaskLineId],[PushTitle],[PushContent],[PushEmail],[PushState],[CreateDate],[TaskAway])
Values('JOG-001-T1','" + strSubject + "','" + strBody + "','" + strMailTo + "','0','" + DateTime.Now + "','0')";
                DataAccess.Instance("PushDB").ExecuteNonQuery(sqlCommandText);
            }
            catch (Exception ex)
            {
                string _str = ex.Message;
                EmailHelp.AppendTextToLog(ex.Message, "C:\\workflow\\log", "EmailLog1.txt");
                return false;
                //throw ex;
            }
            return true;
        }
         

        public static void AppendTextToLog(string strText, string strLogFilePath, string FileInfo)
        {
            if (!Directory.Exists(strLogFilePath))
            {
                Directory.CreateDirectory(strLogFilePath);
            }
            StreamWriter logWriter;
            logWriter = File.AppendText(strLogFilePath + "\\" + FileInfo);
            logWriter.Write(DateTime.Now.ToString() + ": ");
            logWriter.WriteLine(strText);
            logWriter.Close();
            logWriter.Dispose();
        }
    }
    public class EmailArg
    {
        private string incident = "";
        public string Incident
        {
            get { return incident; }
            set { incident = value; }
        }
        private string uRLpath = "";
        public string URLpath
        {
            get { return uRLpath; }
            set { uRLpath = value; }
        }
        private string processName = "";
        public string ProcessName
        {
            get { return processName; }
            set { processName = value; }
        }
        private string email = "";
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private System.Data.DataTable dtPRocess = new System.Data.DataTable();
        public System.Data.DataTable DtPRocess
        {
            get { return dtPRocess; }
            set { dtPRocess = value; }
        }
    }



}
