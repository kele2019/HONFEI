using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MyLib;
using Presale.Process.Common;
using System.Data.SqlClient;
using Ultimus.UWF.Common.Logic;
namespace Ultimus.UWF.Workflow.EmailServices
{
    class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine("请输入内容");
            //string ReadStr = Console.ReadLine();
          
            //Console.ReadKey();
            try
            {
                //EmailHelp.SendMail("joseph.li@honfei.cn", "test", "合同到期提醒", true);
                //string StrBody = "Dear Leader<br/>";
                //StrBody += " In you department probation will expire on  ... Please evaluate employee’s performance before <br/>Thanks for you cooperation !<br/>BR/HR";
                //EmailHelp.SendMail("joseph.li@honfei.cn", "Workflow Notice", StrBody, true);
                //GetEmployeeContractInfo();
                //GetEmployeeTraining();
                Console.WriteLine("Initialization of annual leave，Please input 1,Press Enter");
                Console.WriteLine("Clear up last year Annual，Please input 2, Press Enter");
                Console.WriteLine("Initialization of OT LastYear，Please input 3,Press Enter");
                Console.WriteLine("Clear up of OT LastYear，Please input 4,Press Enter");

                L23:
                string InputStr=Console.ReadLine();
                if (InputStr == "1")
                {
                    Console.WriteLine("Initialization annual to Begin");
                    CreateStartYearEmployeeLevelInfo();
                    Console.WriteLine("Initialization annual to Complete");
                }
                if (InputStr == "2")
                {
                    Console.WriteLine("Clear Last Year annual to Begin ");
                    ClearPrevYearLevel();
                    Console.WriteLine("Clear Last Year annual to Complete");
                }
                if (InputStr == "3")
                {
                    Console.WriteLine("Initialization OT to Begin ");
                    CreateOTLastYear();
                    Console.WriteLine("Initialization OT to Complete");
                }
                if (InputStr == "4")
                {
                    Console.WriteLine("Clear Last Year annual to Begin ");
                    ClearOTLastYear();
                    Console.WriteLine("Clear Last Year annual to Complete");
                }
                goto L23;
               // Console.ReadKey();

               //CreateStartYearEmployeeLevelInfo();
                //ClearPrevYearLevel();
            }
            catch (Exception ex)
            {
                EmailHelp.AppendTextToLog(ex.Message, "C:\\workflow\\log", "员工合同到期提醒.txt");
            }
        }

        /// <summary>
        /// 生成每年初始化年假信息
        /// </summary>
        public static void CreateStartYearEmployeeLevelInfo()
        {
            //string Strsql = "select * from ORG_USER where ISACTIVE=1";
            string Strsql = @"select B.* from  COM_LevalManager A  left join ORG_USER B on A.UserAccount=REPLACE(B.LOGINNAME,'\','/')
	  where B.LOGINNAME is not null";
            DataTable dtUserInfo = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            if (dtUserInfo.Rows.Count > 0)
            {
                foreach (DataRow item in dtUserInfo.Rows)
                {
                    string TwentyYear=item["EXT15"].ToString();//工作超过20年
                    string JoinDate = item["EntryDate"].ToString();//入职日期
                    if (!string.IsNullOrEmpty(JoinDate))
                    {
                        string UserAccount=item["LOGINNAME"].ToString().Replace("\\","/");
                        int StartYear = Convert.ToDateTime(JoinDate).Year;
                        int EndYear = DateTime.Now.Year;
                        int JoinYear = EndYear - StartYear;
                        if (TwentyYear == "Y")
                        {
                            if (JoinYear <= 10)
                            {
                                GenalLevelManager(UserAccount, 15);
                            }
                            if (JoinYear > 10 && JoinYear <= 15)
                            {
                                GenalLevelManager(UserAccount, 16 + (JoinYear - 10));
                            }
                            if (JoinYear > 15)
                            {
                                GenalLevelManager(UserAccount, 20);
                            }
                           // GenalLevelManager(UserAccount, 15);
                        }
                        else
                        {
                            if (JoinYear <= 5)
                            {
                                GenalLevelManager(UserAccount, 12);
                            }
                            if (JoinYear >5 && JoinYear <=10)
                            {
                                GenalLevelManager(UserAccount, 15);
                            }
                            if (JoinYear >10 && JoinYear <=15)
                            {
                                GenalLevelManager(UserAccount, 16 + (JoinYear - 10));
                            }
                            if (JoinYear > 15)
                            {
                                GenalLevelManager(UserAccount, 20);
                            }
                        }
                    }

                }
            }
        }

        public static bool GenalLevelManager(string UserAccount,int DayCount)
        {
            string Strsql = "select * from COM_LevalManager where UserAccount='" + UserAccount + "' and LeaveYear='" + (DateTime.Now.Year - 1) + "' and LeaveYearHourCount>0";
            DataTable dtUserLevelInfo = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            decimal LastYearHour = 0;
            if (dtUserLevelInfo.Rows.Count > 0)
            {
                LastYearHour = Convert.ToDecimal(dtUserLevelInfo.Rows[0]["LeaveYearHourCount"]);
                if (LastYearHour > 40)
                    LastYearHour = 40;
            }
            string StrCreateAnnualLog = @"insert into COM_LOG(ID,MODULE,LOGTYPE,FORMNAME,FORMID,LOGCONTENT,CREATEDATE)
  values((select ISNULL(MAX(ID),0)+1 from COM_LOG),'LevalType','AnnualCreate','" + UserAccount + "','" + DayCount + "','Initialization of annual leave [" + DayCount + "] Day',GETDATE())";
            DataAccess.Instance("BizDB").ExecuteNonQuery(StrCreateAnnualLog);

            string StrInsertsql = @"insert into COM_LevalManager(UserAccount,LeaveYear,LeaveYearCount,LeaveYearHourCount,LeaveLastYearHourCount,FuallpaySick,CountLeave,CountLastLeave)
  values('" + UserAccount + "','" + DateTime.Now.Year + "','" + DayCount + "','" + (DayCount * 8) + "','" + LastYearHour + "','5','" + DayCount + "','" + (LastYearHour/8).ToString("f3")+ "')";
            return DataAccess.Instance("BizDB").ExecuteNonQuery(StrInsertsql) > 0;
        }

        public static void ClearPrevYearLevel()
        {
            string Strsql = "select * from COM_LevalManager where LeaveYear='" + DateTime.Now.Year + "' and LeaveLastYearHourCount>0";
            DataTable dtLastYear = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            if (dtLastYear.Rows.Count > 0)
            {
                foreach (DataRow item in dtLastYear.Rows)
                {
                    string UserAccount=item["UserAccount"].ToString();
                    decimal LeaveLastYearHourCount = Convert.ToDecimal(item["LeaveLastYearHourCount"]);
                    int TableID = Convert.ToInt32(item["ID"]);
                    string StrClearsql = @"insert into COM_LOG(ID,MODULE,LOGTYPE,FORMNAME,FORMID,LOGCONTENT,CREATEDATE)
  values((select MAX(ID)+1 from COM_LOG),'LevalType','AnnualClear','" + UserAccount + "','" + (LeaveLastYearHourCount/8).ToString("f2")+ "','Clear up last year Annual [" + LeaveLastYearHourCount + "] Hour',GETDATE())";
                    DataAccess.Instance("BizDB").ExecuteNonQuery(StrClearsql);
                    DataAccess.Instance("BizDB").ExecuteNonQuery("update COM_LevalManager set LeaveLastYearHourCount='0.00' where ID='" + TableID + "'");
                }
            }
        }

        public static void CreateOTLastYear()
        {
            string Strsql = @"select * from COM_OTAndDayOffManage where OTYear='"+(DateTime.Now.Year-1)+"' and OTHourCount>0";
            DataTable dtOTLY=DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            if (dtOTLY.Rows.Count > 0) {
                foreach (DataRow item in dtOTLY.Rows)
                {
                    string UserAccount=item["UserAccount"].ToString();
                    string LasYearOTHour=item["OTHourCount"].ToString();
                    string StrsqlInsert = string.Format(@"insert into COM_OTAndDayOffManage(UserAccount,LastYearHourCount,OTYear,TotalHour) values
('{0}','{1}','{2}',{3})", UserAccount, LasYearOTHour, DateTime.Now.Year, LasYearOTHour);
                    DataAccess.Instance("BizDB").ExecuteNonQuery(StrsqlInsert);
                }
            }

        }
        public static void ClearOTLastYear()
        {
            string Strsql = @"SELECT * FROM (
select  A.*,LEFT(B.USERCODE,2) USERCODE  from COM_OTAndDayOffManage A LEFT JOIN ORG_USER B
ON A.UserAccount=REPLACE(B.LOGINNAME,'\','/')) C where USERCODE IN('U1','U2') AND OTYear='" + DateTime.Now.Year + "' and LastYearHourCount>0";
            DataTable dtOTLY = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            if (dtOTLY.Rows.Count > 0)
            {
                foreach (DataRow item in dtOTLY.Rows)
                {
                    string UserAccount = item["UserAccount"].ToString();
                    string LastYearHourCount = item["LastYearHourCount"].ToString();
                    string ID = item["ID"].ToString();
                    string StrsqlClear = string.Format(@"update COM_OTAndDayOffManage set LastYearHourCount=0 where ID='"+ID+"'");
                    DataAccess.Instance("BizDB").ExecuteNonQuery(StrsqlClear);
                    string StrClearsql = @"insert into COM_LOG(ID,MODULE,LOGTYPE,FORMNAME,FORMID,LOGCONTENT,CREATEDATE)
  values((select MAX(ID)+1 from COM_LOG),'OTType','OTClear','" + UserAccount + "','" + LastYearHourCount + "','Clear up last year OT [" + LastYearHourCount + "] Hour',GETDATE())";
                    DataAccess.Instance("BizDB").ExecuteNonQuery(StrClearsql);
                }
            }
        }



        public static void GetEmployeeContractInfo()
        {
            string ContractDay = System.Configuration.ConfigurationManager.AppSettings["ContractDay"];
            string SendEmailDay = System.Configuration.ConfigurationManager.AppSettings["SendEmailDay"];
           // string Strsql = @" select * from ( SELECT  LOGINNAME,DATEDIFF(DAY,EntryDate,getdate()) CountDay,EMAIL FROM ORG_USER )A where CountDay in(" + ContractDay + ")";//(150,1050,2130)
            int SendEmailAndProcessDay = Convert.ToInt32(SendEmailDay) + 30;
            string Strsql = @"  select USERNAME, LOGINNAME,CountDay,
 (((SELECT EMAIL FROM ORG_USER WHERE USERID=(SELECT TOP(1) USERID FROM ORG_JOB WHERE JOBID =(SELECT TOP(1) SUPERVISORJOBID FROM ORG_JOB WHERE USERID=A.USERID))))+';'+A.HRMEMAIL) EMAIL
  from ( SELECT USERNAME+EXT04 USERNAME ,USERID, LOGINNAME,DATEDIFF(DAY,EntryDate,getdate()) CountDay,
 ((SELECT EMAIL FROM ORG_USER WHERE LOGINNAME=(SELECT UserAccount FROM ORG_ROLEINFO WHERE RoleName='HRM'))+';'+EMAIL) AS HRMEMAIL FROM ORG_USER  where ISACTIVE=1)A
 where CountDay in(" + ContractDay + ")";//(150,1050,2130)

            DataTable dtUserInfo=DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);
            if (dtUserInfo.Rows.Count > 0)
            {
                foreach (DataRow item in dtUserInfo.Rows)
                {
                    string  NextMonth=DateTime.Now.AddMonths(1).ToString("yyyy/MM/dd");
                    string StrBody = "";
                    if (SendEmailAndProcessDay.ToString()==item["CountDay"].ToString())
                    {
                    CreateEmployeeContract(item["LOGINNAME"].ToString());
                    StrBody = "Dear Leader<br/><br/>";
                    StrBody += item["USERNAME"].ToString() + " In you department probation will expire on " + NextMonth + " Please evaluate employee’s performance before " + NextMonth + "<br/><br/>Thanks for you cooperation !<br/><br/>BR/HR";
                    EmailHelp.SendMail(item["EMAIL"].ToString(), "Share Point Workflow Notice - Probation Evaluate", StrBody, true);
                    }
                    else
                    {
                        StrBody = "Dear Leader<br/><br/>";
                        StrBody += item["USERNAME"].ToString() + " in you department  labor contract will expire on " + NextMonth + " Please release your concurrence if agree or not to extend labor contract.<br/><br/>Thanks for you cooperation !<br/><br/>BR/HR";
                        EmailHelp.SendMail(item["EMAIL"].ToString(), "Share Point Workflow Notice - Labor Contract", StrBody, true);
                    }
                }
            }
        }
        #region 员工试用期培训流程
        public static void CreateEmployeeContract(string LoginName)
        {
            UserInfoEntity UserEntity = GetOrgLevel.GetUserInfo(LoginName);
            UserProcessEntity UserProcessEntity = new UserProcessEntity();
            if (UserEntity != null)
            {
                string ProcessName = "Probation Assessment";
                string PROCESSSUMMARY = "Probation Assessment";
                UserProcessEntity.FORMID = Guid.NewGuid().ToString();
                UserProcessEntity.PROCESSNAME = ProcessName;
                UserProcessEntity.INCIDENT = 0;
                UserProcessEntity.DOCUMENTNO = "";
                UserProcessEntity.APPLICANT = UserEntity.USERNAME;
                UserProcessEntity.APPLICANTACCOUNT = UserEntity.LOGINNAME.Replace('\\', '/');
                UserProcessEntity.REQUESTDATE = DateTime.Now.ToShortDateString();
                UserProcessEntity.DEPARTMENT = UserEntity.DEPARTMENTNAME;
                UserProcessEntity.PROCESSSUMMARY = PROCESSSUMMARY;
                UserProcessEntity.STATUS = "0";
                UserProcessEntity.TRSummary = PROCESSSUMMARY;
                UserProcessEntity.USERCODE = UserEntity.USERCODE;

                if (CreateData(UserProcessEntity))
                {
                    int Incident = SubmitProcess(LoginName, ProcessName, PROCESSSUMMARY, null, "", "0", "Flow Start");
                    if (Incident > 0)
                    {
                        UserProcessEntity.INCIDENT = Incident;
                        string processPrefix = "NC";
                        SerialNoLogic sn = new SerialNoLogic();
                        string documentNo = string.Format("{0}{1}{2}{3}{4}", processPrefix,
                           DateTime.Now.Year, DateTime.Now.Month.ToString().PadLeft(2, '0'), DateTime.Now.Day.ToString().PadLeft(2, '0'),
                           sn.GetSerialNo(processPrefix, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToString().PadLeft(5, '0'));
                        UserProcessEntity.DOCUMENTNO = documentNo;
                        UpdateData(UserProcessEntity);
                    }
                }

            }
        }
        public static int SubmitProcess(string LoginName,string ProcessName,string Summary,DataTable dtvarList,string Comments,string ActionType,string StepName)
        {

            ProcessAPI.EIKService PAPI = new ProcessAPI.EIKService();
            string TaskID=PAPI.GetInitTaskID(ProcessName);
            int Incident = PAPI.CreateTask(LoginName, TaskID, Summary, dtvarList, Comments, ActionType, ProcessName, StepName);
            return Incident;
        }
        public static bool CreateData(UserProcessEntity UserProcessEntity)
        {
            string Strsql = @"INSERT INTO PROC_ProbationEvaluation (FORMID,PROCESSNAME,INCIDENT,DOCUMENTNO,APPLICANT,APPLICANTACCOUNT,REQUESTDATE,DEPARTMENT,PROCESSSUMMARY,STATUS,TRSummary,USERCODE)
  VALUES(@FORMID,@PROCESSNAME,@INCIDENT,@DOCUMENTNO,@APPLICANT,@APPLICANTACCOUNT,@REQUESTDATE,@DEPARTMENT,@PROCESSSUMMARY,@STATUS,@TRSummary,@USERCODE)";
          List<SqlParameter> listsqlpa = new List<SqlParameter>();
          listsqlpa.Add(new SqlParameter("@FORMID", UserProcessEntity.FORMID));
          listsqlpa.Add(new SqlParameter("@PROCESSNAME", UserProcessEntity.PROCESSNAME));
          listsqlpa.Add(new SqlParameter("@INCIDENT", UserProcessEntity.INCIDENT));
          listsqlpa.Add(new SqlParameter("@DOCUMENTNO", UserProcessEntity.DOCUMENTNO));
          listsqlpa.Add(new SqlParameter("@APPLICANT", UserProcessEntity.APPLICANT));
          listsqlpa.Add(new SqlParameter("@APPLICANTACCOUNT", UserProcessEntity.APPLICANTACCOUNT));
          listsqlpa.Add(new SqlParameter("@REQUESTDATE", UserProcessEntity.REQUESTDATE));
          listsqlpa.Add(new SqlParameter("@DEPARTMENT", UserProcessEntity.DEPARTMENT));
          listsqlpa.Add(new SqlParameter("@PROCESSSUMMARY", UserProcessEntity.PROCESSSUMMARY));
          listsqlpa.Add(new SqlParameter("@STATUS", UserProcessEntity.STATUS));
          listsqlpa.Add(new SqlParameter("@TRSummary", UserProcessEntity.TRSummary));
          listsqlpa.Add(new SqlParameter("@USERCODE", UserProcessEntity.USERCODE));
          return DataAccess.Instance("BizDB").ExecuteNonQuery(Strsql, listsqlpa.ToArray()) > 0;
        }
        public static bool UpdateData(UserProcessEntity UserProcessEntity)
        {
            string Strsql = "update PROC_ProbationEvaluation set INCIDENT=@INCIDENT,STATUS=1,DOCUMENTNO=@DOCUMENTNO where FORMID=@FORMID";
            List<SqlParameter> listsqlpa = new List<SqlParameter>();
            listsqlpa.Add(new SqlParameter("@INCIDENT", UserProcessEntity.INCIDENT));
            listsqlpa.Add(new SqlParameter("@DOCUMENTNO", UserProcessEntity.DOCUMENTNO));
            listsqlpa.Add(new SqlParameter("@FORMID", UserProcessEntity.FORMID));
            return DataAccess.Instance("BizDB").ExecuteNonQuery(Strsql, listsqlpa.ToArray()) > 0;
        }
        #endregion

        #region 培训评估流程

        public static void GetEmployeeTraining()
         {

             try
             {
                 string Strsql = "select DOCUMENTNO,EvaluationDays,HRAPPROVEDATE,ApprovalArr_TrainingPersonnel,TrainingPurpose CourseName,TrainingTeacher TrainerName,StartDate TrainingStart,EndDate TrainingEnd,TrainingDuration TrainingHours   from  dbo.PROC_EmployeeTraining where INCIDENT>0  and HRAPPROVEDATE is not null";
                 DataTable dtTrainingInfo = DataAccess.Instance("BizDB").ExecuteDataTable(Strsql);

                 if (dtTrainingInfo.Rows.Count > 0)
                 {
                     foreach (DataRow item in dtTrainingInfo.Rows)
                     {
                         string Trainingperson = "SELECT LOGINNAME FROM ORG_USER WHERE  USERID IN( select UserID from COM_EmployeeTrainSignInfo where  TrainDocmentNo='" + item["DOCUMENTNO"].ToString() + "' and SumDate<>'0')";
                         DataTable TrainingPersonnel = DataAccess.Instance("BizDB").ExecuteDataTable(Trainingperson); // item["ApprovalArr_TrainingPersonnel"].ToString();
                         string EvalMonth = item["EvaluationDays"].ToString();
                         DateTime dtHRAPPROVEDATE = Convert.ToDateTime(item["HRAPPROVEDATE"].ToString());
                         bool Flag = false;
                         if (EvalMonth == "3")
                         {
                             if (dtHRAPPROVEDATE.AddMonths(3).ToShortDateString() == DateTime.Now.ToShortDateString())
                                 Flag = true;
                         }
                         if (EvalMonth == "6")
                         {
                             if (dtHRAPPROVEDATE.AddMonths(6).ToShortDateString() == DateTime.Now.ToShortDateString())
                                 Flag = true;
                         }
                         if (EvalMonth == "1")
                         {
                             if (dtHRAPPROVEDATE.AddMonths(1).ToShortDateString() == DateTime.Now.ToShortDateString())
                                 Flag = true;
                         }
                         if (Flag)
                         {

                             for (int i = 0; i < TrainingPersonnel.Rows.Count; i++)
                             {
                                 string CourseName = item["CourseName"].ToString();
                                 string TrainerName = item["TrainerName"].ToString();
                                 string TrainingStart = item["TrainingStart"].ToString();
                                 string TrainingEnd = item["TrainingEnd"].ToString();
                                 string TrainingHours = item["TrainingHours"].ToString();
                                 CreateTraningResultProcess(TrainingPersonnel.Rows[i]["LOGINNAME"].ToString(), CourseName, TrainerName, TrainingStart, TrainingEnd, TrainingHours);
                             }
                         }

                     }

                 }
             }
             catch (Exception ex)
             {
                 EmailHelp.AppendTextToLog(ex.Message, "C:\\workflow\\log", "员工培训发起任务.txt");
             }
         }

        public static void CreateTraningResultProcess(string LoginName, string CourseName, string TrainerName, string TrainingStart, string TrainingEnd, string TrainingHours)
        {
            UserInfoEntity UserEntity = GetOrgLevel.GetUserInfo(LoginName);
            UserProcessEntity UserProcessEntity = new UserProcessEntity();
            if (UserEntity != null)
            {
                string ProcessName = "Training Evaluation";
                string PROCESSSUMMARY = "Training Evaluation";
                UserProcessEntity.FORMID = Guid.NewGuid().ToString();
                UserProcessEntity.PROCESSNAME = ProcessName;
                UserProcessEntity.INCIDENT = 0;
                UserProcessEntity.DOCUMENTNO = "";
                UserProcessEntity.APPLICANT = UserEntity.USERNAME;
                UserProcessEntity.APPLICANTACCOUNT = UserEntity.LOGINNAME.Replace('\\', '/');
                UserProcessEntity.REQUESTDATE = DateTime.Now.ToShortDateString();
                UserProcessEntity.DEPARTMENT = UserEntity.DEPARTMENTNAME;
                UserProcessEntity.PROCESSSUMMARY = PROCESSSUMMARY;
                UserProcessEntity.STATUS = "0";
                UserProcessEntity.TRSummary = PROCESSSUMMARY;
                UserProcessEntity.USERCODE = UserEntity.USERCODE;

                if (CreateTrainingData(UserProcessEntity, CourseName, TrainerName, TrainingStart, TrainingEnd, TrainingHours))
                {
                    int Incident = SubmitProcess(LoginName, ProcessName, PROCESSSUMMARY, null, "", "0", "Flow Start");
                    if (Incident > 0)
                    {
                        UserProcessEntity.INCIDENT = Incident;
                        string processPrefix = "TRE";
                        SerialNoLogic sn = new SerialNoLogic();
                        string documentNo = string.Format("{0}{1}{2}{3}{4}", processPrefix,
                           DateTime.Now.Year, DateTime.Now.Month.ToString().PadLeft(2, '0'), DateTime.Now.Day.ToString().PadLeft(2, '0'),
                           sn.GetSerialNo(processPrefix, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToString().PadLeft(5, '0'));
                        UserProcessEntity.DOCUMENTNO = documentNo;
                        UpdateTraningData(UserProcessEntity);
                    }
                }

            }
        }
        public static bool CreateTrainingData(UserProcessEntity UserProcessEntity, string CourseName, string TrainerName, string TrainingStart, string TrainingEnd, string TrainingHours)
        {
            string Strsql = @"INSERT INTO PROC_TrainingResultEvaluation (FORMID,PROCESSNAME,INCIDENT,DOCUMENTNO,APPLICANT,APPLICANTACCOUNT,REQUESTDATE,DEPARTMENT,PROCESSSUMMARY,STATUS,TRSummary,USERCODE,CourseName,TrainerName,TrainingStart,TrainingEnd,TrainingHours)
  VALUES(@FORMID,@PROCESSNAME,@INCIDENT,@DOCUMENTNO,@APPLICANT,@APPLICANTACCOUNT,@REQUESTDATE,@DEPARTMENT,@PROCESSSUMMARY,@STATUS,@TRSummary,@USERCODE,@CourseName,@TrainerName,@TrainingStart,@TrainingEnd,@TrainingHours)";
            List<SqlParameter> listsqlpa = new List<SqlParameter>();
            listsqlpa.Add(new SqlParameter("@FORMID", UserProcessEntity.FORMID));
            listsqlpa.Add(new SqlParameter("@PROCESSNAME", UserProcessEntity.PROCESSNAME));
            listsqlpa.Add(new SqlParameter("@INCIDENT", UserProcessEntity.INCIDENT));
            listsqlpa.Add(new SqlParameter("@DOCUMENTNO", UserProcessEntity.DOCUMENTNO));
            listsqlpa.Add(new SqlParameter("@APPLICANT", UserProcessEntity.APPLICANT));
            listsqlpa.Add(new SqlParameter("@APPLICANTACCOUNT", UserProcessEntity.APPLICANTACCOUNT));
            listsqlpa.Add(new SqlParameter("@REQUESTDATE", UserProcessEntity.REQUESTDATE));
            listsqlpa.Add(new SqlParameter("@DEPARTMENT", UserProcessEntity.DEPARTMENT));
            listsqlpa.Add(new SqlParameter("@PROCESSSUMMARY", UserProcessEntity.PROCESSSUMMARY));
            listsqlpa.Add(new SqlParameter("@STATUS", UserProcessEntity.STATUS));
            listsqlpa.Add(new SqlParameter("@TRSummary", UserProcessEntity.TRSummary));
            listsqlpa.Add(new SqlParameter("@USERCODE", UserProcessEntity.USERCODE));

            listsqlpa.Add(new SqlParameter("@CourseName", CourseName));
            listsqlpa.Add(new SqlParameter("@TrainerName", TrainerName));
            listsqlpa.Add(new SqlParameter("@TrainingStart", TrainingStart));
            listsqlpa.Add(new SqlParameter("@TrainingEnd", TrainingEnd));
            listsqlpa.Add(new SqlParameter("@TrainingHours", TrainingHours));

            return DataAccess.Instance("BizDB").ExecuteNonQuery(Strsql, listsqlpa.ToArray()) > 0;
        }
        public static bool UpdateTraningData(UserProcessEntity UserProcessEntity)
        {
            string Strsql = "update PROC_TrainingResultEvaluation set INCIDENT=@INCIDENT,STATUS=1,DOCUMENTNO=@DOCUMENTNO where FORMID=@FORMID";
            List<SqlParameter> listsqlpa = new List<SqlParameter>();
            listsqlpa.Add(new SqlParameter("@INCIDENT", UserProcessEntity.INCIDENT));
            listsqlpa.Add(new SqlParameter("@DOCUMENTNO", UserProcessEntity.DOCUMENTNO));
            listsqlpa.Add(new SqlParameter("@FORMID", UserProcessEntity.FORMID));
            return DataAccess.Instance("BizDB").ExecuteNonQuery(Strsql, listsqlpa.ToArray()) > 0;
        }
        #endregion
    }
}
