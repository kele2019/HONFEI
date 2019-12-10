using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using System.Configuration;

namespace BPM
{
    /// <summary>
    /// OleDb 操作类
    /// Kyle.zhu  于 2008-03-27
    /// </summary>
    public class clsOleDB
    {

        #region 私有变量定义
        private OleDbConnection oleCnn;
        private OleDbCommand oleCmm;
        private OleDbTransaction oleTra;
        private string strConn;   /* 数据库联接字符串 */
        private string pthLog = "";
        private string pthCnn = "";
        private string pthSql = "";
        private string isDebug = "";
        #endregion

        #region 类构造clsOleDB
        /// <summary>
        /// 类构造clsOleDB
        /// <param name="strConn"></param>
        public clsOleDB(string strCnn, string strLog, string strSql)
        {
            this.pthCnn = strCnn;
            this.pthLog = strLog;
            this.pthSql = strSql;
            this.isDebug = getConn("isDebug");
        }

        public clsOleDB(string strCnn)
        {
            this.pthCnn = strCnn;
            this.pthLog = @"C:\logQuanYou " + System.DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            this.pthSql = ConfigurationManager.AppSettings["connConfigPath"].ToString(); //"C:\\cnnQuanYou.txt";
            this.isDebug = getConn("isDebug");
        }

        public clsOleDB()
        {
            this.pthCnn = ConfigurationManager.AppSettings["connConfigPath"].ToString();// "C:\\cnnQuanYou.txt"; //ConfigurationManager.AppSettings["connConfigPath"].ToString();
            this.pthLog = @"C:\logQuanYou " + System.DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            this.pthSql = ConfigurationManager.AppSettings["connConfigPath"].ToString();// "C:\\cnnQuanYou.txt";
            this.isDebug = getConn("isDebug");
        }
        #endregion

        #region 设置数据库连接字符串
        /// <summary>
        /// 设置数据库连接字符串
        /// <param name="strConn"></param>
        public void setConn(string strDB)
        {
            this.strConn = getConn(strDB);
        }
        #endregion

        #region 日志记录函数
        /// <summary>
        /// 日志记录函数
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="strInfo"></param>
        public void wteLog(string strPath, bool booDebug, string strInfo)
        {
            try
            {
                //初始化写是否写日志标志变量
                bool booFlg = true;

                //根据不同的情况改变是否写日志标志变量的值
                if (!booDebug)
                {
                    booFlg = true;
                }
                else
                {
                    if (this.isDebug == "Debug")
                    {
                        booFlg = true;
                    }
                    else
                    {
                        booFlg = false;
                    }
                }

                //根据是否写日志标志变量的取值,来执行日志写入
                if (booFlg)
                {
                    if (strPath.Trim() == "" || strPath == string.Empty)
                    {
                        strPath = this.pthLog;
                    }

                    FileInfo fleLog = new FileInfo(strPath);
                    StreamWriter steWrt = fleLog.AppendText();
                    steWrt.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]: " + strInfo);
                    steWrt.Close();
                }
            }
            catch
            {
            }
        }
        #endregion

        #region 取得配置相关
        /// <summary>
        /// 取得配置相关
        /// </summary>
        /// <returns></returns>
        public string getConn(string strConn)
        {
            string strLine = "";
            try
            {

                FileInfo fleInfo = new FileInfo(pthCnn);
                StreamReader steReader = fleInfo.OpenText();
                bool booExist = false;
                do
                {
                    //从文本文件读取一行
                    strLine = "";
                    strLine = steReader.ReadLine();

                    //空白行时,退出循环
                    if (strLine == null)
                    {
                        break;
                    }

                    int intLoc = strLine.IndexOf('=');
                    int intLoc2 = strLine.Length - intLoc;
                    string strName = strLine.Substring(0, intLoc);
                    if (strName.ToUpper() == strConn.ToUpper())
                    {
                        strLine = strLine.Substring(intLoc + 1, intLoc2 - 1);
                        booExist = true;
                    }
                }
                while (!booExist);

                steReader.Close();

            }
            catch (Exception ee)
            {
                this.wteLog(this.pthLog, false, "调用getConn方法出错：" + ee.Message);
            }

            return strLine;
        }
        #endregion

        #region 取得配置相关 2
        /// <summary>
        /// 取得配置相关 2
        /// </summary>
        /// <returns></returns>
        public string getConn2(string strConn)
        {
            string strLine = "";
            try
            {

                FileInfo fleInfo = new FileInfo(pthSql);
                StreamReader steReader = fleInfo.OpenText();
                bool booExist = false;
                do
                {
                    //从文本文件读取一行
                    strLine = "";
                    strLine = steReader.ReadLine();

                    //空白行时,退出循环
                    if (strLine == null)
                    {
                        break;
                    }

                    int intLoc = strLine.IndexOf('=');
                    int intLoc2 = strLine.Length - intLoc;
                    string strName = strLine.Substring(0, intLoc);
                    if (strName.ToUpper() == strConn.ToUpper())
                    {
                        strLine = strLine.Substring(intLoc + 1, intLoc2 - 1);
                        booExist = true;
                    }
                }
                while (!booExist);

                steReader.Close();

            }
            catch (Exception ee)
            {
                this.wteLog(this.pthLog, false, "调用getConn2方法出错：" + ee.Message);
            }

            return strLine;
        }
        #endregion

        #region 数据库连接打开
        /// <summary>
        /// 数据库连接打开
        /// </summary>
        private void oleOpen()
        {
            oleCnn = new OleDbConnection(strConn);
            try
            {
                oleCnn.Open();
            }
            catch (Exception e)
            {
                oleCnn.Close();
            }
        }
        #endregion

        #region 根据SQL查询返回 (ds)
        /// <summary>
        /// 根据SQL查询返回 (ds)
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet getDsRtn(string strSQL)
        {
            oleOpen();
            OleDbDataAdapter oleDa = new OleDbDataAdapter(strSQL, oleCnn);
            DataSet dsRtn = new DataSet();
            oleDa.Fill(dsRtn);
            oleClose();
            return dsRtn;
        }
        #endregion

        #region 根据SQL查询返回 (dt)
        /// <summary>
        /// 根据SQL查询返回 (dt)
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable getDtRtn(string strSQL)
        {
            oleOpen();
            OleDbDataAdapter oleDa = new OleDbDataAdapter(strSQL, oleCnn);
            DataSet dsRtn = new DataSet();
            DataTable dtRtn = new DataTable();
            oleDa.Fill(dsRtn);

            //根据查询的结果返回具体的 (dt)
            if (dsRtn != null)
            {
                if (dsRtn.Tables.Count > 0)
                {
                    dtRtn = dsRtn.Tables[0];
                }
                else
                {
                    dtRtn = null;
                }
            }
            else
            {
                dtRtn = null;
            }
            //数据库连接关闭
            oleClose();

            //具体值返回
            return dtRtn;
        }
        #endregion

        #region 执行SQL语句 (返回影响行数)
        /// <summary>
        /// 执行SQL语句 (返回影响行数)
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public int exeSql(string strSQL)
        {
            oleOpen();
            oleCmm = new OleDbCommand(strSQL, oleCnn);
            int intNum = oleCmm.ExecuteNonQuery();
            oleClose();
            return intNum;
        }
        #endregion

        #region 执行SQL语句 (得到单行单列的直)
        /// <summary>
        /// 执行SQL语句 (返回影响行数)
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public object exeScalar(string strSQL)
        {
            oleOpen();
            oleCmm = new OleDbCommand(strSQL, oleCnn);
            object intNum = oleCmm.ExecuteScalar();
            oleCmm = null;
            oleClose();
            return intNum;
        }
        #endregion

        #region 数据库事务开始
        /// <summary>
        /// 数据库事务开始
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public void beginTan()
        {
            oleOpen();
            oleTra = oleCnn.BeginTransaction();
        }
        #endregion

        #region 数据库事务提交
        /// <summary>
        /// 数据库事务提交
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public void commitTan()
        {
            oleTra.Commit();
            oleClose();
        }
        #endregion

        #region 数据库事务回滚
        /// <summary>
        /// 数据库事务回滚
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public void rollbackTan()
        {
            oleTra.Rollback();
            oleClose();
        }
        #endregion

        #region 执行SQL语句-事务 (返回影响行数)
        /// <summary>
        /// 执行SQL语句-事务 (返回影响行数)
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public int exeSqlTra(string strSQL)
        {
            //如果OleDbCommand为空是初始化
            if (oleCmm == null)
            {
                oleCmm = new OleDbCommand(strSQL, oleCnn);
                oleCmm.Transaction = oleTra;
            }
            else
            {
                oleCmm.CommandText = strSQL;
            }
            int intNum = oleCmm.ExecuteNonQuery();
            return intNum;
        }
        #endregion

        #region 执行存储过程
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public int exeProcedure(string strProcedure, string[] strName, string[] strValue)
        {
            oleOpen();
            oleCmm = new OleDbCommand(strProcedure, oleCnn);
            oleCmm.CommandType = CommandType.StoredProcedure;
            OleDbParameter[] oleDbParam = new OleDbParameter[strName.Length];

            //传递参数
            for (int i = 0; i < oleDbParam.Length; i++)
            {
                oleDbParam[i] = new OleDbParameter(strName[i], OleDbType.VarChar, 600);
                oleDbParam[i].Value = strValue[i].Trim();
                oleCmm.Parameters.Add(oleDbParam[i]);
            }

            //返回结果
            int intNum = oleCmm.ExecuteNonQuery();
            oleClose();
            return intNum;
        }
        #endregion

        #region 数据库连接关闭
        /// <summary>
        /// 数据库连接关闭
        /// </summary>
        public void oleClose()
        {
            oleCnn.Close();
            oleCnn.Dispose();
        }
        #endregion

        #region 获取值
        public string getAString(string sql)
        {
            oleOpen();
            OleDbCommand oleCmd = new OleDbCommand(sql, oleCnn);
            string strRtn = "";
            object ob = oleCmd.ExecuteScalar();
            if (ob == null)
            {
                strRtn = "";
            }
            else
            {
                strRtn = ob.ToString();
            }
            oleClose();
            return strRtn;
        }
        #endregion

    }

}