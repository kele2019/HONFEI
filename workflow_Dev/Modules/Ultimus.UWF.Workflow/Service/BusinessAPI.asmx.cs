using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MyLib;
using Ultimus.UWF.Workflow.Entity;
using Presale.Process.TravalExpense;
using Presale.Process.Common;

namespace Ultimus.UWF.Workflow.Service
{
    /// <summary>
    /// BusinessAPI 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class BusinessAPI : System.Web.Services.WebService
    {
          Implementation.BusinessData DAO = new Implementation.BusinessData();
          [WebMethod(Description = "获取出差申请数据")]
          public TravalEntity GetTravalData(string IncidentID)
          {
              try
              {
                  TravalEntity TMode = DAO.GetTaskEntityByName(IncidentID);
                  TMode.TravalEntityDetail = DAO.GetTravalEntityDetail(IncidentID);
                  TMode.ApprovalHistoryEntity = DAO.GetApprovalHistoryList(TMode.PROCESSNAME, IncidentID);
                  TMode.AttachmentEntity = DAO.GetAttachments(TMode.FORMID);

                  return TMode;
              }
              catch (Exception e)
              {
                  return null;
              }
          }

        [WebMethod(Description = "获取出差报销主数据")]
        public TravalExpenseEntity GetTravalExpenseData(string IncidentID)
        {
            try
            {
                TravalExpenseEntity TEMode=DAO.GetTravalExpenseData(IncidentID);
                TEMode.TravalExpenseEntityDetail = DAO.GetTravalExpenseDataDetail(IncidentID);
                TEMode.ApprovalHistoryEntity = DAO.GetApprovalHistoryList(TEMode.PROCESSNAME, IncidentID);
                TEMode.AttachmentEntity = DAO.GetAttachments(TEMode.FORMID);
                return TEMode;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        [WebMethod(Description = "获取借款申请主数据")]
        public CashEntity GetCashAdvanceData(string IncidentID)
        {
            try
            {
                CashEntity TEMode = DAO.GetCashAdvanceData(IncidentID);
                TEMode.ApprovalHistoryEntity = DAO.GetApprovalHistoryList(TEMode.PROCESSNAME, IncidentID);
                TEMode.AttachmentEntity = DAO.GetAttachments(TEMode.FORMID);
                return TEMode;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [WebMethod(Description = "获取个人报销数据")]
        public PersonalExpense GetPersonalExpenseData(string IncidentID)
        {
            try
            {
                PersonalExpense TEMode = DAO.GetPersonalExpenseData(IncidentID);
                TEMode.PeronalExpenseDetail = DAO.GetPeronalExpenseDetail(IncidentID);
                TEMode.ApprovalHistoryEntity = DAO.GetApprovalHistoryList(TEMode.PROCESSNAME, IncidentID);
                TEMode.AttachmentEntity = DAO.GetAttachments(TEMode.FORMID);
                return TEMode;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        [WebMethod(Description = "获取个人补贴数据")]
        public PersonalAllownce GetPersonalAllownceData(string IncidentID)
        {
            try
            {
                PersonalAllownce TEMode = DAO.GetPersonalAllownceData(IncidentID);
                TEMode.PersonalAllownceDetail = DAO.GetPersonalAllownceDetail(IncidentID);
                TEMode.ApprovalHistoryEntity = DAO.GetApprovalHistoryList(TEMode.PROCESSNAME, IncidentID);
                TEMode.AttachmentEntity = DAO.GetAttachments(TEMode.FORMID);
                return TEMode;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        [WebMethod(Description = "报销退回数据操作数据")]
        public bool ReturnExpenseData(string ProcessName, string IncidentID)
        {
            return DAO.OffsetCashAdvanceAmount(ProcessName, IncidentID);
        }
       
    }
}
