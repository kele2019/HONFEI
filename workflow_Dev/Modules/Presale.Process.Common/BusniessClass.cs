using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyLib;
using System.Data.SqlClient;
using System.Data;

namespace Presale.Process.Common
{
  public  class BusniessClass
    {

      /// <summary>
      /// 检查指定审批人是否审批
      /// </summary>
      /// <param name="ProcessName"></param>
      /// <param name="Incident"></param>
      /// <param name="StepName"></param>
      /// <returns></returns>
      public bool CheckCFOApprove(string ProcessName, string Incident,string StepName)
      {
          string Strsql = "select COUNT(1) from TASKS where PROCESSNAME=@PROCESSNAME and INCIDENT=@INCIDENT and STEPLABEL=@STEPLABEL AND STATUS=1";
          List<SqlParameter> listP = new List<SqlParameter>();
          listP.Add(new SqlParameter("@PROCESSNAME",ProcessName));
          listP.Add(new SqlParameter("@INCIDENT", Incident));
          listP.Add(new SqlParameter("@STEPLABEL", StepName));
         return Convert.ToInt32(DataAccess.Instance("UltDB").ExecuteScalar(Strsql, listP.ToArray()))>0;
      }
      /// <summary>
      /// Reject 指定步骤
      /// </summary>
      /// <param name="ProcessName"></param>
      /// <param name="Incident"></param>
      /// <param name="StepName"></param>
      /// <returns></returns>
      public bool RejectProcessStep(string ProcessName, string Incident, string StepName)
      {
          List<SqlParameter> listP = new List<SqlParameter>();
          listP.Add(new SqlParameter("@PROCESSNAME", ProcessName));
          listP.Add(new SqlParameter("@INCIDENT", Incident));
          listP.Add(new SqlParameter("@STEPLABEL", StepName));
         // string Strsql = "update TASKS set STATUS=4 where PROCESSNAME=@PROCESSNAME and INCIDENT=@INCIDENT and STEPLABEL=@STEPLABEL AND STATUS=1";
          string Strsql = "delete from TASKS where PROCESSNAME=@PROCESSNAME and INCIDENT=@INCIDENT and STEPLABEL=@STEPLABEL AND STATUS=1";
          return Convert.ToInt32(DataAccess.Instance("UltDB").ExecuteScalar(Strsql, listP.ToArray())) > 0;
      }
      /// <summary>
      /// 更新Review审批状态
      /// </summary>
      /// <param name="ProcessName"></param>
      /// <param name="Incident"></param>
      /// <param name="StepName"></param>
      /// <returns></returns>
      public bool UpdateReivewStatus(string ProcessName, string Incident, string StepName)
      {
          List<SqlParameter> listP = new List<SqlParameter>();
          listP.Add(new SqlParameter("@PROCESSNAME", ProcessName));
          listP.Add(new SqlParameter("@INCIDENT", Incident));
          listP.Add(new SqlParameter("@STEPLABEL", StepName));
          //string Strsql = "update TASKS set STATUS=3 where PROCESSNAME=@PROCESSNAME and INCIDENT=@INCIDENT and STEPLABEL=@STEPLABEL AND STATUS=1";

//          string AproveHistorysql = @"if exists(
//select ASSIGNEDTOUSER from TASKS where PROCESSNAME=@PROCESSNAME and INCIDENT=@INCIDENT and STEPLABEL=@STEPLABEL AND STATUS=1)
// insert into UltimusBizHF.dbo.WF_APPROVALHISTORY (PROCESSNAME,INCIDENT,STEPNAME,APPROVERNAME,ACTION,CREATEDATE,EXT01)
//select top(1) PROCESSNAME,INCIDENT,STEPNAME,APPROVERNAME,ACTION,CREATEDATE,REPLACE(LOGINNAME,'/','\\') EXT01 from 
//( select '" + ProcessName + "' PROCESSNAME,'" + Incident + "' INCIDENT,'" + StepName + "' STEPNAME, USERNAME APPROVERNAME,'Review' ACTION, GETDATE() CREATEDATE,LOGINNAME from   UltimusBizHF.dbo. V_ORG_USER)A right join (select ASSIGNEDTOUSER from TASKS where PROCESSNAME=@PROCESSNAME and INCIDENT=@INCIDENT and STEPLABEL=@STEPLABEL AND STATUS=1) B on B.ASSIGNEDTOUSER=A.LOGINNAME";

          string AproveHistorysql = "select ASSIGNEDTOUSER from TASKS where PROCESSNAME=@PROCESSNAME and INCIDENT=@INCIDENT and STEPLABEL=@STEPLABEL AND STATUS=1";
        object ASSIGNEDTOUSER=DataAccess.Instance("UltDB").ExecuteScalar(AproveHistorysql, listP.ToArray());
        if (ASSIGNEDTOUSER != null)
        {

            AproveHistorysql += " update TASKS set STATUS=3 where PROCESSNAME=@PROCESSNAME and INCIDENT=@INCIDENT and STEPLABEL=@STEPLABEL AND STATUS=1";
            DataAccess.Instance("UltDB").ExecuteScalar(AproveHistorysql, listP.ToArray());
            AproveHistorysql = @"insert into dbo.WF_APPROVALHISTORY (PROCESSNAME,INCIDENT,STEPNAME,APPROVERNAME,ACTION,CREATEDATE,EXT01)
 select top(1) '" + ProcessName + "' PROCESSNAME,'" + Incident + "' INCIDENT,'" + StepName + "' STEPNAME, USERNAME APPROVERNAME,'Review' ACTION, GETDATE() CREATEDATE,REPLACE(LOGINNAME,'/','\\') EXT01 from   dbo. V_ORG_USER where LOGINNAME='" + ASSIGNEDTOUSER.ToString().Trim() + "'";
            DataAccess.Instance("BizDB").ExecuteNonQuery(AproveHistorysql);
        }
         return true;
      }

      /// <summary>
      /// 获取用户DOA信息
      /// </summary>
      /// <param name="LOGINNAME"></param>
      /// <returns></returns>
      public  DataTable GetUserDOA(string LOGINNAME)
      {
          string strsql = @"select C.*,D.DEPARTMENTTYPE from (
 select A.*,B.DEPARTMENTID from (select USERID,IDNO from ORG_USER where LOGINNAME='"+LOGINNAME+"') A left join ORG_JOB B on A.USERID=B.USERID) C left join ORG_DEPARTMENT D on C.DEPARTMENTID=D.DEPARTMENTID";
          return DataAccess.Instance("BizDB").ExecuteDataTable(strsql);
      }
      /// <summary>
      /// 获取成本中心
      /// </summary>
      /// <returns></returns>
      public DataTable GetCostCenter()
      {
         // string StrsqlCostCenter = "select EXT02 CostCenter,(EXT02+' '+DEPARTMENTNAME) CostCenterName from ORG_DEPARTMENT where DEPARTMENTTYPE<>'Root'";
          string StrsqlCostCenter = "select cosetcenter as CostCenter,Description CostCenterName,(cosetcenter+ Description) CodeDesc from PROC_PURCHASE_COSTCENTER  where ISactive='1'";
          
          return DataAccess.Instance("BizDB").ExecuteDataTable(StrsqlCostCenter);
      }
    }
}
