using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyLib;
using System.Data;
namespace Presale.Process.Common
{
  public static  class GetOrgLevel
    {
      public static ApprovalInfo UserLevelInfo(string UserID) 
      {
          ApprovalInfo mode = new ApprovalInfo();
          string strsqlLeader = @"select (select A.LOGINNAME from  ORG_USER A where A.LOGINNAME=ORG_USER.EXT02) LeaderName ,EXT01,USERID from  dbo.ORG_USER where LOGINNAME='"+UserID+"'";
          DataTable dtUserLevel = DataAccess.Instance("BizDB").ExecuteDataTable(strsqlLeader);
          if (dtUserLevel.Rows.Count > 0)
          {
              mode.EXT01 = int.Parse(dtUserLevel.Rows[0]["EXT01"].ToString());
              mode.LeaderName = dtUserLevel.Rows[0]["LeaderName"].ToString();
              if (int.Parse(dtUserLevel.Rows[0]["EXT01"].ToString()) >= 30)
              {
                  string StrsqlDeptID = "select DEPARTMENTID from ORG_DEPARTMENT where DEPARTMENTID=(select DEPARTMENTID from ORG_JOB where USERID='" + dtUserLevel.Rows[0]["USERID"].ToString() + "')";
                  string DeptID=DataAccess.Instance("BizDB").ExecuteScalar(StrsqlDeptID).ToString();
                  mode.DirectManagerName = GetDeptFZC(DeptID);
              }
              else
              {
                  string strsqlDept = "select DEPARTMENTID from ORG_DEPARTMENT where DEPARTMENTID=(select DEPARTMENTID from ORG_JOB where USERID='" + dtUserLevel.Rows[0]["USERID"].ToString() + "')";
                  DataTable dtDept = DataAccess.Instance("BizDB").ExecuteDataTable(strsqlDept);
                  if (dtDept.Rows.Count > 0)
                  {
                      mode.LeaderSuprName = GetDeptLeaderName(dtDept.Rows[0]["DEPARTMENTID"].ToString());
                      string DeptID = dtDept.Rows[0]["DEPARTMENTID"].ToString();
                      mode.ManagerName = GetDeptZJ(ref DeptID);
                      mode.DirectManagerName = GetDeptFZC(DeptID);
                  }
              }
          }
          return mode;
      }
      //获取经理信息
      public static string GetDeptLeaderName(string DeptID)
      { 
       string strsqlDept = "select  * from ORG_DEPARTMENT where DEPARTMENTID='" + DeptID + "'";
          DataTable dtDept = DataAccess.Instance("BizDB").ExecuteDataTable(strsqlDept);
          if (dtDept.Rows.Count > 0)
          {
              string DeptType = dtDept.Rows[0]["DEPARTMENTTYPE"].ToString();
              //if (DeptType.ToLower() == "subdept")
              //{
                  string strDeptLeader = "select B.LOGINNAME from (select USERID from ORG_JOB  where DEPARTMENTID='" + DeptID + "' ) A";
                   strDeptLeader+=" left join ORG_USER B on A.USERID=B.USERID where EXT01=20";
                   DataTable dtDeptLeader = DataAccess.Instance("BizDB").ExecuteDataTable(strDeptLeader);
                   if (dtDeptLeader.Rows.Count > 0)
                   {
                       return dtDeptLeader.Rows[0]["LOGINNAME"].ToString();
                   }
                   else
                       return "";
              //}
              //else
              //    return "";
          }
          else
              return "";
      }
      //获取总监信息 
      public static string GetDeptZJ(ref string DeptID)
      {
          //string strsqlDept = "select * from ORG_DEPARTMENT where DEPARTMENTID=(select DEPARTMENTID from ORG_JOB where USERID='"+UserID+"')";
          string strsqlDept = "select  * from ORG_DEPARTMENT where DEPARTMENTID='" + DeptID + "'";
          DataTable dtDept = DataAccess.Instance("BizDB").ExecuteDataTable(strsqlDept);
          if (dtDept.Rows.Count > 0)
          {
              string DeptType = dtDept.Rows[0]["DEPARTMENTTYPE"].ToString();
              if (DeptType != "Department")
              {
                  DeptID = dtDept.Rows[0]["PARENTID"].ToString();
                   return GetDeptZJ(ref DeptID);
              }
              else
              {
                  string strsqlZJ = "select B.LOGINNAME from (select    USERID  from dbo.ORG_JOB where DEPARTMENTID='" + DeptID + "' and";
                  strsqlZJ += " ISMANAGER=1)A left join ORG_USER  B on A.USERID=B.USERID";
                  DataTable dtDeptZJ = DataAccess.Instance("BizDB").ExecuteDataTable(strsqlZJ);
                  if (dtDeptZJ.Rows.Count > 0)
                  {
                      DeptID = dtDept.Rows[0]["PARENTID"].ToString();
                      return dtDeptZJ.Rows[0]["LOGINNAME"].ToString();
                  }
                  else
                  {
                      DeptID = dtDept.Rows[0]["PARENTID"].ToString();
                      return "";
                  }
              }
          }
          else
          {
              return "";
          }
      }
	  public static bool RevokeFunc(string ProcessName, string StepName, string IncidentID, string Operator)
	  {
		  string strsql = string.Format(@"update TASKS set STATUS=1 where PROCESSNAME='{0}' and INCIDENT='{1}' and STEPLABEL='{2}' 
 delete TASKS    where PROCESSNAME='{0}' and INCIDENT='{1}' and STEPLABEL<>'{2}'", ProcessName, IncidentID, StepName);
		  DataAccess.Instance("UltDB").ExecuteNonQuery(strsql);

		  string StrUpdateAppHistory = string.Format("update WF_APPROVALHISTORY set  Action='Revoke' where PROCESSNAME='{0}' and INCIDENT='{1}'  and STEPNAME='{2}'", ProcessName, IncidentID, StepName);
		  DataAccess.Instance("BizDB").ExecuteNonQuery(StrUpdateAppHistory);
		  string strRevoke = string.Format("update PROC_REVOKE set status=3,Operator='{2}',OperatorDate=getdate() where  ProcessName='{0}' and incident='{1}'", ProcessName, IncidentID, Operator);

		  return DataAccess.Instance("BizDB").ExecuteNonQuery(strRevoke) > 0;

	  }

      //获取副总裁信息
      public static string GetDeptFZC(string DeptID)
      {
          string strDeptLeader = "select B.LOGINNAME from (select USERID from ORG_JOB  where DEPARTMENTID in (select DEPARTMENTID from ORG_DEPARTMENT where PARENTID='"+DeptID+"') ) A";
          strDeptLeader += " left join ORG_USER B on A.USERID=B.USERID where (EXT01=40 or EXT01=50) order by EXT01";
          DataTable dtDeptFZC = DataAccess.Instance("BizDB").ExecuteDataTable(strDeptLeader);
          if (dtDeptFZC.Rows.Count > 0)
          {
              return dtDeptFZC.Rows[0]["LOGINNAME"].ToString();
          }
          else
              return "";
      }
      public static string GetDeptInfo(string UserID)
      {
          string strsql = "select B.DEPARTMENTNAME subDeptName,B.DEPARTMENTTYPE subDeptType, A.DEPARTMENTNAME DeptName ,A.DEPARTMENTTYPE DeptType from ORG_DEPARTMENT A inner join ";
          strsql += " (select * from ORG_DEPARTMENT where DEPARTMENTID=(select DEPARTMENTID from ORG_JOB where USERID=(select USERID from ORG_USER where LOGINNAME='" + UserID + "'))) B on A.DEPARTMENTID=B.PARENTID";
        DataTable dtDeptInfo=DataAccess.Instance("BizDB").ExecuteDataTable(strsql);
        if (dtDeptInfo.Rows.Count > 0)
        {
            if (dtDeptInfo.Rows[0]["subDeptType"].ToString().ToLower() == "department")
                return dtDeptInfo.Rows[0]["subDeptName"].ToString();
            if (dtDeptInfo.Rows[0]["DeptType"].ToString().ToLower() == "department")
                return dtDeptInfo.Rows[0]["DeptName"].ToString();
            else
                return "";
        }
        else
            return "";
      }

      public static UserInfoEntity GetUserInfo(string LoginName)
      {
          string Strsql = @" SELECT C.*,D.EXT02 COSTCENTER,D.DEPARTMENTNAME FROM (
 select A.*,B.DEPARTMENTID from ( select   LOGINNAME,EXT04 ENNAME ,USERCODE,USERNAME,EMAIL,USERID from  ORG_USER where LOGINNAME='"+LoginName+"')A left join ORG_JOB B ON A.USERID=B.USERID)C LEFT JOIN ORG_DEPARTMENT D ON C.DEPARTMENTID=D.DEPARTMENTID";
          UserInfoEntity UserEntity=DataAccess.Instance("BizDB").ExecuteEntity<UserInfoEntity>(Strsql);
          return UserEntity;
      }
    }
  public class ApprovalInfo
  {
      public ApprovalInfo()
		{}
      private int ext01=0;
      public int EXT01
      { 
          get{ return ext01;}
          set{ ext01=value;}
      }
      private string leaderName ="";
      public string LeaderName
      {
          get { return leaderName; }
          set { leaderName = value; }
      }
      private string leaderSuprName = "";
      public string LeaderSuprName
      {
          get { return leaderSuprName; }
          set { leaderSuprName = value; }
      }
      private string managerName = "";
      public string ManagerName
      {
          get { return managerName; }
          set { managerName = value; }
      }
       private string directManagerName = "";
       public string DirectManagerName
      {
          get { return directManagerName; }
          set { directManagerName = value; }
      }
  }

  public class UserInfoEntity
  {
      public string LOGINNAME
      { get; set; }
      public string ENNAME
      { get; set; }
      public string USERCODE
      { get; set; }
      public string USERNAME
      { get; set; }
      public string EMAIL
      { get; set; }
      public string USERID
      { get; set; }
      public string COSTCENTER
      { get; set; }
      public string DEPARTMENTNAME
      { get; set; }
  }
}
