using MyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultimus.UWF.Workflow.EmailServices
{
  public  class EmailHelp
    {

        public static void SendMail(string AppCode,string strMailTo, string strSubject, string strBody, bool blIsHtml)
        {
            try
            {
                string StrsqlEmail = @"insert into SendEmailData(AppCode,ReciverEmail,EmailSubject,Emailbody,CreateDate,IsSend,TryCount)
values('" + AppCode + "', '" + strMailTo + "', '" + strSubject + "', '" + strBody + "', getdate(), '0', '0')";
                DataAccess.Instance("EmailDB").ExecuteNonQuery(StrsqlEmail);
            }
            catch (Exception err)
            {
                // LogFactory.All.WriteWithError(LogName, err);
                AppendTextToLog(err.Message);
            }
        }

        public static void AppendTextToLog(string strText)
        {
            string strLogFilePath = AppDomain.CurrentDomain.BaseDirectory;
            string FileInfo = "EmailLog.txt";

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
}
