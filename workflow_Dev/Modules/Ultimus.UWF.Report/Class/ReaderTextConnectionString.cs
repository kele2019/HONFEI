using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Data;

namespace BPM.ReportDesign.Class
{
    public class ReaderTextConnectionString
    {
        public DataTable ReaderConnectionList()
        {
            string strLine = "";
            DataTable dt = new DataTable();
            dt.Columns.Add("ConnectionStringName");
            dt.Columns.Add("ConnectionStringValue");
            FileInfo fleInfo = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["connConfigPath"].ToString());
            StreamReader steReader = fleInfo.OpenText();

            while (true)
            {
                //从文本文件读取一行
                strLine = "";
                strLine = steReader.ReadLine();

                //空白行时,退出循环
                if (strLine == null)
                {
                    break;
                }

                dt.Rows.Add(dt.NewRow());

                dt.Rows[dt.Rows.Count - 1]["ConnectionStringName"] = strLine.Substring(0, strLine.IndexOf("="));
                dt.Rows[dt.Rows.Count - 1]["ConnectionStringValue"] = strLine.Substring(strLine.IndexOf("=") + 1);
            }
            steReader.Close();
            return dt;
        }
    }
}