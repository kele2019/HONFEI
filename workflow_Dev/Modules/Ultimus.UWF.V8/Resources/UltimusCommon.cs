using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using Ultimus.WFServer;
using Ultimus.OC;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Ultimus
{
    public class CommonClass
    {
        public Task ContextTask;
        string _connStr = "Data Source=demo.ultimus.com.cn;Initial Catalog=UltimusBiz;User ID=sa;Password=qazwsx!23;";
        string _logPath = "c:\\ultimuslog.txt";
        public void GetNewFormInfo(string prefix, ref string formID, ref string documentNo, ref DateTime requestDate)
        {
            int intCnt = 0;
            string strSQL = "";

            try
            {
                if (formID == null || formID.Trim() == "")
                {
                    formID = Guid.NewGuid().ToString();
                }

                requestDate = DateTime.Now;

                //1.申请单号-----------------------------------------------------------------------------------------------------------------
                strSQL = "select count(*) as CNT from COM_SERIALNO where SERIALTYPE = '" + prefix + "' ";
                DataTable dt = new DataTable();

                dt = this.ExecuteDataTable(strSQL);
                intCnt = (dt.Rows[0]["CNT"].ToString() == "" ? 0 : int.Parse(dt.Rows[0]["CNT"].ToString()));
                dt = null;

                //如果存在的情况
                if (intCnt > 0)
                {
                    strSQL = "update COM_SERIALNO set serialno = (select max(serialno) + 1 from COM_SERIALNO where SERIALTYPE = '" + prefix + "')";
                    strSQL += " where prefix = '" + prefix + "' ";
                }
                else
                {
                    strSQL = "insert into COM_SERIALNO(ID,SERIALTYPE, serialno) values((select max(id)+1 from COM_SERIALNO),'" + prefix + "', 1)";
                }

                //执行数据的更新
                intCnt = this.ExecuteNonQuery(strSQL);

                //查找最大值并返回
                strSQL = "select max(serialno) as MAXNO from COM_SERIALNO where SERIALTYPE = '" + prefix + "'";
                dt = this.ExecuteDataTable(strSQL);
                string oApplyNo = dt.Rows[0]["MAXNO"].ToString().Trim();

                string pYear = DateTime.Now.Year.ToString();
                string pMonth = DateTime.Now.Month.ToString();
                string pDay = DateTime.Now.Day.ToString();

                string pNumber = oApplyNo;

                if (pMonth.Length == 1)
                    pMonth = "0" + pMonth;

                if (pDay.Length == 1)
                    pDay = "0" + pDay;

                if (pNumber.ToString().Length == 1)
                {
                    oApplyNo = prefix.Trim() + pYear + pMonth + pDay + "000" + pNumber.ToString();
                }
                else if (pNumber.ToString().Length == 2)
                {
                    oApplyNo = prefix.Trim() + pYear + pMonth + pDay + "00" + pNumber.ToString();
                }
                else if (pNumber.ToString().Length == 3)
                {
                    oApplyNo = prefix.Trim() + pYear + pMonth + pDay + "0" + pNumber.ToString();
                }
                else
                {
                    oApplyNo = prefix.Trim() + pYear + pMonth + pDay + pNumber.ToString();
                }

                documentNo = oApplyNo;


            }
            catch (Exception ee)
            {
                this.LogInfo("调用GetNewFormInfo方法出错：" + ee.Message);
            }


        }

        public void SaveApprovalHistory(string Action, string Comments)
        {
            try
            {
                SaveApprovalHistory(ContextTask.strProcessName, ContextTask.nIncidentNo, ContextTask.strStepName
                    , ContextTask.strAssignedToUserFullName, ContextTask.strAssignedToUser, Action, Comments);
            }
            catch (Exception ee)
            {
                this.LogInfo("调用SaveApprovalHistory方法出错：" + ee.Message );
            }
        }

        public void SaveApprovalHistory(string processName, int incident, string stepName, string approverName, string approverAccount, string Action, string Comments)
        {
            string strSQL = "";
            string ProcessName = processName;
            int IncidentNo = incident;
            string StepName = stepName;
            string ApproverName = approverName;
            string ApproverAccount = approverAccount;
            string CreateDate = System.DateTime.Now.ToString();

            try
            {

                strSQL = "insert into WF_APPROVALHISTORY(PROCESSNAME,INCIDENT,STEPNAME,APPROVERNAME,APPROVERACCOUNT,ACTION,COMMENTS,CREATEDATE) values(";
                strSQL += "'" + ProcessName + "',";
                strSQL += IncidentNo + ",";
                strSQL += "'" + StepName + "',";
                strSQL += "'" + ApproverName + "',";
                strSQL += "'" + ApproverAccount + "',";
                strSQL += "'" + Action + "',";
                strSQL += "'" + Comments + "',";
                strSQL += "'" + CreateDate + "')";

                int intRtn = this.ExecuteNonQuery(strSQL);
            }
            catch (Exception ee)
            {
                this.LogInfo("调用SaveApprovalHistory方法出错：" + ee.Message + ",SQL:" + strSQL);
            }
        }


        public int ExecuteNonQuery(string commandText)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(commandText);
                cmd.Connection = conn;
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                conn = null;
                return i;
            }
            catch (Exception ex)
            {
                LogInfo(ex.Message);
                return 0;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                    conn = null;
                }
            }
        }

        public DataTable ExecuteDataTable(string commandText)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(commandText);
                cmd.Connection = conn;
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                DataTable dt = new DataTable();
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
                conn.Close();
                conn = null;
                return dt;
            }
            catch (Exception ex)
            {
                LogInfo(ex.Message);
                return new DataTable();
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                    conn = null;
                }
            }
        }

        public void LogInfo(string message)
        {
            try
            {
                string strPath = _logPath;
                string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                FileInfo fleLog = new FileInfo(strPath);
                StreamWriter steWrt = fleLog.AppendText();
                steWrt.WriteLine("[" + strDateTime + "]: " + message);
                steWrt.Close();
                steWrt = null;
            }
            catch
            {
            }
        }
    }

}