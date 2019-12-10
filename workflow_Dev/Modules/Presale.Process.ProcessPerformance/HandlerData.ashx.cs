using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using MyLib;
using System.Data.SqlClient;

namespace Presale.Process.ProcessPerformance
{
    /// <summary>
    /// HandlerData 的摘要说明
    /// </summary>
    public class HandlerData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Rep Result = new Rep();
            try
            {
                string Methodname = context.Request.QueryString["Method"];
                string Versionname = context.Request.QueryString["VersionName"];
                if (Methodname == "CheckName")
                {
                    if (CheckVersionName(Versionname))
                        Result.Code = 1;
                }
                if (Methodname == "AddVersionName")
                {
                    if (!AddVersionName(Versionname))
                    { 
                      Result.Code = 1;
                      Result.Msg = "Add  Faild";
                    }
                }
                if (Methodname == "AddTarget")
                {
                    string Flag = context.Request.QueryString["Flag"];
                    string DeptCode = context.Request.QueryString["DeptCode"];
                    string DeptText = context.Request.QueryString["DeptText"];
                    string txtTarget = context.Request.QueryString["Target"];
                    string txtStandard = context.Request.QueryString["Standard"];
                    string txtDescription = context.Request.QueryString["Description"];
                    string txtCalculation = context.Request.QueryString["Calculation"];
                    if (!AddTarget(Flag, DeptCode, DeptText, txtTarget, txtStandard, txtDescription, txtCalculation))
                    {
                        Result.Code = 1;
                        Result.Msg = "Save Faild";
                    }
                }
                if (Methodname == "DeleteTarget")
                {
                    string Flag = context.Request.QueryString["Flag"];
                    if (!DeleteTarget(Flag))
                    {
                        Result.Code = 1;
                        Result.Msg = "Delete Faild";
                    }
                }

            }
            catch (Exception ex)
            {
                Result.Code = 1;
                Result.Msg ="System Exception:"+ex.Message;
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(JsonConvert.SerializeObject(Result));
        }

        public bool CheckVersionName(string Versionname)
        {
            string Strsql = "select COUNT(1) from PROC_ProcessPerformanceVersion where PerformanceVersionName='" + Versionname + "'";
            return Convert.ToInt32(DataAccess.Instance("BizDB").ExecuteScalar(Strsql))>0;
        }
        public bool AddVersionName(string VersionName)
        {
            string Strsql = @"insert into PROC_ProcessPerformance_Thrid(DEPTMENTCODE,PROCESS,PROCESSMEA,STANDARD,Definition,Calculationmethod,ContactInfo)
                        select DEPTMENTCODE,PROCESS,PROCESSMEA,STANDARD,Definition,Calculationmethod,'" + VersionName + "' as ContactInfo from PROC_ProcessPerformance_Thrid where ContactInfo=(";
                   Strsql +=@"   select Top(1) ContactInfo from PROC_ProcessPerformanceVersion order by ID desc )  ";
                   Strsql += @"insert into PROC_ProcessPerformanceVersion(PerformanceVersionName,ContactInfo,CreateDate)values('"+VersionName+"','"+VersionName+"',GETDATE()) ";
                   return DataAccess.Instance("BizDB").ExecuteNonQuery(Strsql)>0;
        }
        public bool AddTarget(string Flag, string DeptCode, string DeptText, string txtTarget, string txtStandard, string txtDescription, string txtCalculation)
        {
            string strsql = "";
            List<SqlParameter> SqlParaList = new List<SqlParameter>();
            SqlParaList.Add(new SqlParameter("@DEPTMENTCODE", DeptCode));
            SqlParaList.Add(new SqlParameter("@PROCESS", DeptText));
            SqlParaList.Add(new SqlParameter("@PROCESSMEA", txtTarget));
            SqlParaList.Add(new SqlParameter("@STANDARD", txtStandard));
            SqlParaList.Add(new SqlParameter("@Definition", txtDescription));
            SqlParaList.Add(new SqlParameter("@Calculationmethod", txtCalculation));
            SqlParaList.Add(new SqlParameter("@ID", Flag));
            if (string.IsNullOrEmpty(Flag))
            {
                strsql = @"insert into PROC_ProcessPerformance_Thrid(DEPTMENTCODE,PROCESS,PROCESSMEA,STANDARD,Definition,Calculationmethod,ContactInfo)
                        values(@DEPTMENTCODE,@PROCESS,@PROCESSMEA,@STANDARD,@Definition,@Calculationmethod,(select top(1) ContactInfo from  PROC_ProcessPerformanceVersion order by ID Desc))";
            }
            else
            {
                strsql = "update  PROC_ProcessPerformance_Thrid set DEPTMENTCODE=@DEPTMENTCODE,PROCESS=@PROCESS,PROCESSMEA=@PROCESSMEA,STANDARD=@STANDARD,Definition=@Definition,Calculationmethod=@Calculationmethod where ID=@ID";
            }
            return DataAccess.Instance("BizDB").ExecuteNonQuery(strsql,SqlParaList.ToArray())>0;
        }

        public bool DeleteTarget(string Flag)
        {
            string Strsql = "delete from PROC_ProcessPerformance_Thrid where ID="+Flag;
            return DataAccess.Instance("BizDB").ExecuteNonQuery(Strsql) > 0;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class Rep {
        public object Data { get; set; }
        public int Code { get; set; }
        public string Msg { get; set; }
    }

}