using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MyLib; 
using System.Data.SqlClient;
 
namespace Ultimus.UWF.Workflow.EmailServices
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                Console.WriteLine("Initialization of annual leave，Please input 1,Press Enter");
                Console.WriteLine("Clear up last year Annual，Please input 2, Press Enter");
                Console.WriteLine("Initialization of OT LastYear，Please input 3,Press Enter");
                Console.WriteLine("Clear up of OT LastYear，Please input 4,Press Enter");
                Console.WriteLine("Reminder of contract expiration ，Please input 5,Press Enter");
                Console.WriteLine("Evaluation of Training，Please input 6,Press Enter");
                Console.WriteLine("Empolyee Performance Report，Please input 7,Press Enter");
                // Console.WriteLine("Reminder of Email，Please input 7,Press Enter");

                // GetEmployeeContractInfo();
                //GetEmployeeTraining();
                //var tool = new HonfeiWorkflowTranningAndContractInfo();
                //tool.Start();

                L23:
                string InputStr = Console.ReadLine();
                if (InputStr == "1")
                {
                    Console.WriteLine("Initialization annual to Begin");
                    var tool= new InitializationAnnualJob();
                    tool.Start();
                    //CreateStartYearEmployeeLevelInfo();
                    Console.WriteLine("Initialization annual to Complete");
                }
                if (InputStr == "2")
                {
                    Console.WriteLine("Clear Last Year annual to Begin ");
                    var tool = new ClearLastYearAnnualJob();
                    tool.Start();
                    // ClearPrevYearLevel();
                    Console.WriteLine("Clear Last Year annual to Complete");
                }
                if (InputStr == "3")
                {
                    Console.WriteLine("Initialization OT to Begin ");
                    var tool = new InitializationOTJob();
                    tool.Start();
                    // CreateOTLastYear();
                    Console.WriteLine("Initialization OT to Complete");
                }
                if (InputStr == "4")
                {
                    Console.WriteLine("Clear Last Year annual to Begin ");
                    var tool = new ClearLastYearOTJob();
                    tool.Start();
                    // ClearOTLastYear();
                    Console.WriteLine("Clear Last Year annual to Complete");
                }
                if (InputStr == "5")
                {
                    Console.WriteLine("Reminder of contract expiration to Begin ");
                    GetEmployeeContractInfo();
                    Console.WriteLine("Reminder of contract expiration to Complete");
                }
                if (InputStr == "6")
                {
                    Console.WriteLine("evaluation of training to Begin ");
                    GetEmployeeTraining();
                    Console.WriteLine("evaluation of training to Complete");
                }
                if (InputStr == "7")
                {
                    Console.WriteLine("Employee Performance Report to Begin ");
                    var tool = new EmployeePerformanceJob();
                    tool.Start();
                    Console.WriteLine("Employee Performance Report to Complete");
                }
                

                //if (InputStr == "7")
                //{
                //    Console.WriteLine("Reminder of Email to Begin ");
                //    var tool = new ClearLastYearAnnualJob();
                //    tool.Start();

                //    Console.WriteLine("Reminder of Email  to Complete");
                //}
                goto L23;


            }
            catch (Exception ex)
            {
                EmailHelp.AppendTextToLog("员工合同到期提醒:"+ex.Message);
            }
        }

        public static void GetEmployeeContractInfo()
        {
            string ContractDay = System.Configuration.ConfigurationManager.AppSettings["ContractDay"];
            string SendEmailDay = System.Configuration.ConfigurationManager.AppSettings["SendEmailDay"];
           // string Strsql = @" select * from ( SELECT  LOGINNAME,DATEDIFF(DAY,EntryDate,getdate()) CountDay,EMAIL FROM ORG_USER )A where CountDay in(" + ContractDay + ")";//(150,1050,2130)
            int SendEmailAndProcessDay = Convert.ToInt32(SendEmailDay);
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
                    EmailHelp.SendMail("workflow",item["EMAIL"].ToString(), "Share Point Workflow Notice - Probation Evaluate", StrBody, true);
                    }
                    else
                    {
                        StrBody = "Dear Leader<br/><br/>";
                        StrBody += item["USERNAME"].ToString() + " in you department  labor contract will expire on " + NextMonth + " Please release your concurrence if agree or not to extend labor contract.<br/><br/>Thanks for you cooperation !<br/><br/>BR/HR";
                        EmailHelp.SendMail("workflow",item["EMAIL"].ToString(), "Share Point Workflow Notice - Labor Contract", StrBody, true);
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
                 string Strsql = "select APPLICANTACCOUNT,DOCUMENTNO,EvaluationDays,HRAPPROVEDATE,ApprovalArr_TrainingPersonnel,TrainingPurpose CourseName,TrainingTeacher TrainerName,StartDate TrainingStart,EndDate TrainingEnd,TrainingDuration TrainingHours   from  dbo.PROC_EmployeeTraining where INCIDENT>0  and HRAPPROVEDATE is not null";
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
                                string TrainingUser = item["APPLICANTACCOUNT"].ToString();
                                 CreateTraningResultProcess(TrainingPersonnel.Rows[i]["LOGINNAME"].ToString(), CourseName, TrainerName, TrainingStart, TrainingEnd, TrainingHours, TrainingUser);
                             }
                         }

                     }

                 }
             }
             catch (Exception ex)
             {
                 EmailHelp.AppendTextToLog("员工培训发起任务:"+ex.Message);
             }
         }

        public static void CreateTraningResultProcess(string LoginName, string CourseName, string TrainerName, string TrainingStart, string TrainingEnd, string TrainingHours,string TrainingUser)
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

                DataTable dtVar = new DataTable();
                dtVar.TableName = "dtvar";
                dtVar.Columns.Add("Key", typeof(string));
                dtVar.Columns.Add("Value", typeof(string));
                DataRow dr=dtVar.NewRow();
                dr["Key"] = "TrainingUser";
                dr["Value"] ="USER:org=HONFEI,user ="+ TrainingUser;
                dtVar.Rows.Add(dr);

                if (CreateTrainingData(UserProcessEntity, CourseName, TrainerName, TrainingStart, TrainingEnd, TrainingHours))
                {
                    int Incident = SubmitProcess(LoginName, ProcessName, PROCESSSUMMARY, dtVar, "", "0", "Flow Start");
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
