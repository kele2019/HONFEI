using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Text;
using System.Data;
using EntityLibrary;
using UltimusEikLibrary;
using System.Threading;
using ProcessControl.Logic;
using DataMaintenance.Logic;

namespace MobileClientBackground
{
    /// <summary>
    /// Ultimus 服务
    /// </summary>
    [WebService(Namespace = "Ultimus 服务接口")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]

    public class Ultimus_MobileServices : System.Web.Services.WebService
    {
        private UltimusEikOfTask Eik = new UltimusEikOfTask();

        private UltimusEikOfOrgOC OC = new UltimusEikOfOrgOC();

        private UltimusEikOfOther Other = new UltimusEikOfOther();

        private UltimusEikOfDataBase DataBase = new UltimusEikOfDataBase();

        private UltimusEikOfIncident iIncident = new UltimusEikOfIncident();

        private DALLibrary.PageSource Source = new DALLibrary.PageSource();

        [WebMethod(Description = "Ultimus 服务接口 获得新建任务列表")]
        public List<EntityLibrary.Tasks> GetNewTaskListOfUltimusEik(string UserAccount)
        {
            return Eik.GetNewTaskListOfUltimusEik(UserAccount);
        }

        [WebMethod(Description = "Ultimus 服务接口 获得待办任务列表")]
        public List<EntityLibrary.Tasks> GetMyTaskListOfUltimusEik(string UserAccount)
        {
            return Eik.GetMyTaskListOfUltimusEik(UserAccount);
        }

        [WebMethod(Description = "Ultimus 服务接口 获得已办任务列表")]
        public List<EntityLibrary.Tasks> GetCompleteTaskListOfUltimusEik(string UserAccount)
        {
            return Eik.GetCompleteTaskListOfUltimusEik(UserAccount);
        }

        [WebMethod(Description = "Ultimus 服务接口 发起新任务", MessageName = "流程名称 步骤名称 用户账号 发起新任务")]
        public int SendForm(string ProcessName, string StepName, string Summary, string UserAccount, List<VariableInfo> variable, DataSet ds)
        {
            return Source.SavePageDestSource(0, ProcessName, 0, StepName, Summary, UserAccount, variable, ds);
        }

        [WebMethod(Description = "Ultimus 服务接口 发起新任务", MessageName = "任务ID 发起新任务")]
        public int SendForm(string TaskID, string Summary, string UserAccount, List<VariableInfo> variable, DataSet ds)
        {
            Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
            task.InitializeFromTaskId(TaskID);
            return Source.SavePageDestSource(0, task.strProcessName, 0, task.strStepName, Summary, UserAccount, variable, ds);
        }

        [WebMethod(Description = "Ultimus 服务接口 提交任务", MessageName = "流程名称 实例编号 步骤名称 提交任务")]
        public int Send(string ProcessName, int Incident, string StepName, string UserAccount, string Summary, List<VariableInfo> variable, DataSet ds)
        {
            return Source.SavePageDestSource(0, ProcessName, Incident, StepName, Summary, UserAccount, variable, ds);
        }

        [WebMethod(Description = "Ultimus 服务接口 提交任务", MessageName = "任务ID 提交任务")]
        public int Send(string TaskID, string Summary, List<VariableInfo> variable, DataSet ds)
        {
            Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
            task.InitializeFromTaskId(TaskID);
            int i = Source.SavePageDestSource(0, task.strProcessName, task.nIncidentNo, task.strStepName, Summary, task.strUser, variable, ds);
            if (task.strProcessName.Trim() == "一般经费报销流程" && task.strStepName.Trim() == "财务")
            {
                OHCCommon ohccommon = new OHCCommon();
                ohccommon.SendMailGenerApplicant(task.strProcessName.ToString(), task.nIncidentNo.ToString(), "PROC_EXPENSE", task.strStepName + "同意");
            }
            ThreadStart starter = delegate { isComplter(task.strProcessName, task.nIncidentNo); };
            new Thread(starter).Start();

            return i;
        }

        public DataTable ExecuteDataTable(string strCon, string strsql)
        {
            try
            {
                System.Data.SqlClient.SqlConnection SqlCon = new System.Data.SqlClient.SqlConnection(strCon);
                System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter(strsql, SqlCon);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExecuteNonQuery(string strCon, string strsql)
        {
            System.Data.SqlClient.SqlConnection SqlCon = new System.Data.SqlClient.SqlConnection(strCon);
            try
            {
                System.Data.SqlClient.SqlCommand sqlcom = new System.Data.SqlClient.SqlCommand(strsql, SqlCon);
                SqlCon.Open();
                return sqlcom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SqlCon.Close();
            }

        }

        private void isComplter(string strProcessName, int nIncident)
        {
            try
            {
                Thread.Sleep(20000);
                StringBuilder sb = new StringBuilder();
                StringBuilder sblog = new StringBuilder();//jhj
                sb.Append("select * from incidents where processname = '" + strProcessName + "' and incident = '" + nIncident + "'");
                DataTable dt = ExecuteDataTable(System.Configuration.ConfigurationManager.ConnectionStrings["UltDB"].ConnectionString, sb.ToString());
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    string obj = row["PARENTTASKID"].ToString();
                    if (!string.IsNullOrEmpty(obj))
                    {
                        //sub process
                        sb = new StringBuilder();
                        sb.Append("select processname,incident from tasks where taskid = '" + obj + "'");
                        DataTable dd = ExecuteDataTable(System.Configuration.ConfigurationManager.ConnectionStrings["UltDB"].ConnectionString, sb.ToString());
                        if (dd.Rows.Count > 0)
                        {
                            DataRow dr = dd.Rows[0];
                            isComplter(dr["processname"].ToString(), Convert.ToInt32(dr["incident"].ToString()));
                        }
                    }
                    int result = Convert.ToInt32(row["status"]);
                    OHCCommon ohccommon = new OHCCommon();
                    if (result == 2)
                    {
                        sb = new StringBuilder();
                        result = 4;
                        string strTableName = DataMaintenance.Logic.ProcessMaping.GetTableName(strProcessName);
                        sb.AppendFormat("update {0} set STATUS = " + result + " where PROCESSNAME = '{1}' and INCIDENT = '{2}'", strTableName, strProcessName, nIncident);


                        if (ExecuteNonQuery(System.Configuration.ConfigurationManager.ConnectionStrings["BizDB"].ConnectionString, sb.ToString()) > 0)
                        {
                            MyLib.LogUtil.Info("Processname[" + strProcessName + "] Incident[" + nIncident + "] Tablename[" + strTableName + "] status update success");
                        }
                        else
                        {
                            MyLib.LogUtil.Info("Processname[" + strProcessName + "] Incident[" + nIncident + "] Tablename[" + strTableName + "] status update fail");
                        }
                        //邮件审批结束提醒申请人
                        ohccommon.SendMailApplicant(strProcessName, nIncident.ToString(), strTableName, 1);
                        //table money field
                        string strAmountField = "";
                        //table costcenter field
                        string strCostCenterField = "";
                        //table request date field
                        string strRequestDateField = "";
                        string strBudgetCodeField = "";
                        string strPreBillField = "";

                        ProcessMaping.ProcessField(strTableName, ref strAmountField, ref strCostCenterField, ref strRequestDateField, ref strBudgetCodeField, ref strPreBillField);

                        sb = new StringBuilder();

                        string strType = "";

                        #region 备份
                        //                       //lock
                        //                       if (ProcessMaping.LockProcess(strTableName))
                        //                       {
                        //                           sb.AppendFormat(@"UPDATE MASTER_BUDGET SET MASTER_BUDGET.YEARAMOUNTLOCKED = convert(decimal(18,2),a.YEARAMOUNTLOCKED) + convert(decimal(18,2),b." + strAmountField + @")
                        //                                           from MASTER_BUDGET a inner join PROC_TRAVELAPPLICATION b 
                        //                                           on a.YEAR = (case when MONTH(b." + strRequestDateField + @") < 4 then year(b." + strRequestDateField + @") - 1 when MONTH(b." + strRequestDateField + @") >= 4 then year(b." + strRequestDateField + @") end)
                        //                                           and a.CCCODE = b." + strCostCenterField + @"
                        //                                           and b.PROCESSNAME = '" + strProcessName + @"'
                        //                                           and b.INCIDENT = '" + nIncident + @"'");
                        //                           strType = "lock";
                        //                       }
                        //                       //unlock
                        //                       if (ProcessMaping.UnLockProcess(strTableName))
                        //                       {
                        //                           sb.AppendFormat(@"UPDATE MASTER_BUDGET SET MASTER_BUDGET.YEARAMOUNTLOCKED = convert(decimal(18,2),a.YEARAMOUNTLOCKED) - convert(decimal(18,2),b." + strAmountField + @")
                        //                                           ,MASTER_BUDGET.YEARAMOUNTUSED = a.YEARAMOUNTUSED + convert(decimal(18,2),b." + strAccountField + @")
                        //                                           from MASTER_BUDGET a inner join PROC_TRAVELAPPLICATION b 
                        //                                           on a.YEAR = (case when MONTH(b." + strRequestDateField + @") < 4 then year(b." + strRequestDateField + @") - 1 when MONTH(b." + strRequestDateField + @") >= 4 then year(b." + strRequestDateField + @") end)
                        //                                           and a.CCCODE = b." + strCostCenterField + @"
                        //                                           and b.PROCESSNAME = '" + strProcessName + @"'
                        //                                           and b.INCIDENT = '" + nIncident + @"'");
                        //                           strType = "unlock";
                        //                       }
                        #endregion

                        string strPreBill = "";//事先申请单编号
                        string strBudgetCode = "";//预算编号
                        string strCostCenter = "";//成本中心
                        DateTime dtRequestDate;//预算申请时间
                        string strYear = "";//预算年份
                        double amountRequest = 0;//事先申请金额

                        string strBudgetCodeUsed = "";//报销对应的预算编号
                        string strCostCenterUsed = "";//报销对应的成本中心
                        DateTime dtRequestDateUsed;//预算报销时间
                        string strYearUsed = "";//预算报销年份
                        double amountUsed = 0;//报销金额

                        //lock  根据事先申请单上的预算编号、成本中心、年份锁定预算
                        if (ProcessMaping.LockProcess(strTableName))//根据表名判断流程是否需要锁定
                        {
                            string sql = string.Format("select  * from {0} where INCIDENT={1}", strTableName, nIncident);
                            DataTable dtPROC = ExecuteDataTable(System.Configuration.ConfigurationManager.ConnectionStrings["BizDB"].ConnectionString, sql);// DataAccess.Instance("BizDB").ExecuteDataTable(sql);
                            if (dtPROC.Rows.Count > 0)
                            {
                                amountRequest = Convert.ToDouble(dtPROC.Rows[0][strAmountField].ToString());
                                dtRequestDate = Convert.ToDateTime(dtPROC.Rows[0][strRequestDateField].ToString());
                                strBudgetCode = dtPROC.Rows[0][strBudgetCodeField].ToString();
                                strCostCenter = dtPROC.Rows[0][strCostCenterField].ToString();
                                if (dtRequestDate.Month < 4)
                                {
                                    strYear = Convert.ToString(dtRequestDate.Year - 1);
                                }
                                else
                                {
                                    strYear = dtRequestDate.Year.ToString();
                                }
                            }
                            //增加锁定
                            sb.AppendFormat(@"UPDATE MASTER_BUDGET SET YEARAMOUNTLOCKED = ISNULL(YEARAMOUNTLOCKED,0)+" + amountRequest
                                          + @" WHERE MASTER_BUDGET.CODE='" + strBudgetCode + "'"
                                          + @"   AND MASTER_BUDGET.CCCODE='" + strCostCenter + "'"
                                          + @"   AND MASTER_BUDGET.YEAR='" + strYear + "'");
                            strType = "lock";
                        }
                        //unlock 根据事先申请单上的预算编号、成本中心、年份解锁预算；根据报销单上的预算编号、成本中心、年份扣减预算；
                        if (ProcessMaping.UnLockProcess(strTableName))
                        {
                            //获取事先申请单和报销金额（需扣减预算的金额）
                            string sql = string.Format("select  * from {0} where INCIDENT={1}", strTableName, nIncident);
                            DataTable dtEXPENSE = ExecuteDataTable(System.Configuration.ConfigurationManager.ConnectionStrings["BizDB"].ConnectionString, sql);//DataAccess.Instance("BizDB").ExecuteDataTable(sql);
                            if (dtEXPENSE.Rows.Count > 0)
                            {
                                strPreBill = dtEXPENSE.Rows[0][strPreBillField].ToString();
                                strBudgetCodeUsed = dtEXPENSE.Rows[0]["BUDGETCODE"].ToString();
                                strCostCenterUsed = dtEXPENSE.Rows[0]["LASTCOSTCENTER"].ToString();
                                amountUsed = Convert.ToDouble(dtEXPENSE.Rows[0][strAmountField].ToString());
                                dtRequestDateUsed = Convert.ToDateTime(dtEXPENSE.Rows[0]["REQUESTTIME"].ToString());
                                if (dtRequestDateUsed.Month < 4)
                                {
                                    strYearUsed = Convert.ToString(dtRequestDateUsed.Year - 1);
                                }
                                else
                                {
                                    strYearUsed = dtRequestDateUsed.Year.ToString();
                                }
                            }
                            //获取事先申请单相关信息（预算编号、成本中心、年份、申请金额)
                            sql = string.Format("select  * from V_PREBILL where DOCUMENTNO='{0}'", strPreBill);
                            DataTable dtPROC = ExecuteDataTable(System.Configuration.ConfigurationManager.ConnectionStrings["BizDB"].ConnectionString, sql);//DataAccess.Instance("BizDB").ExecuteDataTable(sql);
                            if (dtPROC.Rows.Count > 0)
                            {
                                strBudgetCode = dtPROC.Rows[0]["BUDGETCODE"].ToString();
                                strCostCenter = dtPROC.Rows[0]["LASTCOSTCENTER"].ToString();
                                amountRequest = Convert.ToDouble(dtPROC.Rows[0]["AMOUNTPRE"].ToString());
                                dtRequestDate = Convert.ToDateTime(dtPROC.Rows[0]["REQUESTTIME"].ToString());
                                if (dtRequestDate.Month < 4)
                                {
                                    strYear = Convert.ToString(dtRequestDate.Year - 1);
                                }
                                else
                                {
                                    strYear = dtRequestDate.Year.ToString();
                                }
                            }
                            //如果跨预算年度报销，按照事先申请单的时间进行预算扣减
                            if (strYear != strYearUsed)
                            {
                                strYearUsed = strYear;
                            }
                            //根据事先申请单解锁金额；
                            sb.AppendFormat(@"UPDATE MASTER_BUDGET SET YEARAMOUNTLOCKED = ISNULL(YEARAMOUNTLOCKED,0) - " + amountRequest
                                          + @" WHERE MASTER_BUDGET.CODE='" + strBudgetCode + "'"
                                          + @"   AND MASTER_BUDGET.CCCODE='" + strCostCenter + "'"
                                          + @"   AND MASTER_BUDGET.YEAR='" + strYear + "';");
                            //根据报销单扣减预算金额；
                            sb.AppendFormat(@"UPDATE MASTER_BUDGET SET YEARAMOUNTUSED=ISNULL(YEARAMOUNTUSED,0)+" + amountUsed
                                          + @" WHERE MASTER_BUDGET.CODE='" + strBudgetCodeUsed + "'"
                                          + @"   AND MASTER_BUDGET.CCCODE='" + strCostCenterUsed + "'"
                                          + @"   AND MASTER_BUDGET.YEAR='" + strYearUsed + "';");
                            strType = "unlock";
                        }

                        if (sb.ToString().Length > 0)
                        {

                            if (ExecuteNonQuery(System.Configuration.ConfigurationManager.ConnectionStrings["BizDB"].ConnectionString, sb.ToString()) > 0)
                            {
                                MyLib.LogUtil.Info("Processname[" + strProcessName + "] Incident[" + nIncident + "] Tablename[" + strTableName + "] [" + strType + "] update success");
                            }
                            else
                            {
                                MyLib.LogUtil.Info("Processname[" + strProcessName + "] Incident[" + nIncident + "] Tablename[" + strTableName + "] [" + strType + "] update fail");
                            }
                        }
                    }
                    if (result != 4)
                    {
                        //审批结束邮件提醒下一个审批人
                        string strTableName = DataMaintenance.Logic.ProcessMaping.GetTableName(strProcessName);
                        ohccommon.SendNextMail(strProcessName, nIncident.ToString(), strTableName);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Thread.CurrentThread.Abort();
                GC.Collect();
            }
        }


        [WebMethod(Description = "Ultimus 服务接口 退回任务", MessageName = "任务ID 退回任务")]
        public int Return(string TaskID, string Summary, List<VariableInfo> variable, DataSet ds)
        {
            Ultimus.WFServer.Task task = new Ultimus.WFServer.Task();
            task.InitializeFromTaskId(TaskID);
            int i = Source.SavePageDestSource(1, task.strProcessName, task.nIncidentNo, task.strStepName, Summary, task.strUser, variable, ds);
            if (task.strProcessName.Trim() == "一般经费报销流程" && task.strStepName.Trim() == "财务")
            {
                OHCCommon ohccommon = new OHCCommon();
                ohccommon.SendMailGenerApplicant(task.strProcessName.ToString(), task.nIncidentNo.ToString(), "PROC_EXPENSE", task.strStepName + "退回");
            }
            ThreadStart starter = delegate { isComplter(task.strProcessName, task.nIncidentNo); };
            new Thread(starter).Start();
            return i;
        }

        [WebMethod(Description = "Ultimus 服务接口 获得在Ultimus Administrator 中的域信息")]
        public string[] GetUltimusByAdministratorDomains()
        {
            return Other.GetDomains();
        }

        [WebMethod(Description = "Ultimus 服务接口 根据 用户账号 密码 效验用户")]
        public UserInfo CheckUserByUltimus(string UserAccount, string PassWord)
        {
            return OC.CheckUser(UserAccount, PassWord);
        }

        [WebMethod(Description = "Ultimus 服务接口 获得所有的流程分类")]
        public List<EntityLibrary.MobileClient_Classification> GetCategoryList()
        {
            return DataBase.GetCategory();
        }

        [WebMethod(Description = "Ultimus 服务接口 获得某个分类下的流程个数")]
        public int GetCategoryCountByCategoryName(string CategoryName)
        {
            return DataBase.GetCountByClassificationInfo(CategoryName);
        }

        [WebMethod(Description = "Ultimus 服务接口 根据行索引获取流程信息")]
        public List<EntityLibrary.MobileClient_Classification> GetProcessInfoByIndex(int index)
        {
            return DataBase.GetProcessInfoByIndex(index);
        }

        [WebMethod(Description = "Ultimus 服务接口 根据 流程名称 实例编号 获得流程监控图")]
        public byte[] GetProcessMonitoring(string ProcessName, int Incident)
        {
            return iIncident.GetProcessMonitoring(ProcessName, Incident);
        }

        [WebMethod(Description = "Ultimus 服务接口 根据 流程名称 实例编号 步骤名称 查询数据")]
        public DataSet GetPageSource(string ProcessName, int nIncident, string StepName)
        {
            return Source.GetPageDestSource(ProcessName, nIncident, StepName);
        }

        [WebMethod(Description = "Ultimus 服务接口 根据 流程名称 实例编号 获取步骤ID")]
        public int GetStepId(string ProcessName, string stepName)
        {
            return Source.GetStepId(ProcessName, stepName);
        }

        [WebMethod(Description = "Ultimus 服务接口 根据 流程名称 实例编号 获取审批列表")]
        public DataSet GetProcessApprovalList(string ProcessName, int Incident)
        {
            return Source.GetProcessApprovalList(ProcessName, Incident);
        }

        [WebMethod(Description = "Ultimus 服务接口 根据 流程名称 获得页面控件配置的数据源")]
        public DataSet GetPageControlSource(string ProcessName, string StepName)
        {
            return Source.GetPageControlSource(ProcessName, StepName);
        }

        [WebMethod(Description = "Ultimus 服务接口 根据 流程名称 实例编号 获取附件列表")]
        public DataSet GetProcessAttachments(string ProcessName, string Incident)
        {
            return Source.GetProcessAttachments(ProcessName, Incident);
        }
    }
}
