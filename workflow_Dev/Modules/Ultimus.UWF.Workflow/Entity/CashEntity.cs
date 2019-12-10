using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ultimus.UWF.Workflow.Entity
{
    public class CashEntity
    {
        public CashEntity()
        { 
        }
        public CashEntity(string fORMID, string pROCESSNAME, int? iNCIDENT, string dOCUMENTNO, string aPPLICANT, string aPPLICANTCODE, string aPPLICANTACCOUNT, string rEGION, string lOCATION, string bUCODE, string bUSSINESSNUIT, string cOSTCENTER, DateTime? rEQUESTDATE, string dEPARTMENT, string cOMPANYCODE, string cOMPANY, string pROCESSSUMMARY, string sTATUS, string tRSummary, string borrowReson, double? borrowAmount, double? returnAmount, string currency, string bankNo, string bankName, List<Presale.Process.Common.ApprovalHistoryEntity> approvalhistory, List<Presale.Process.Common.AttachmentEntity> attachment)
        {
            this.FORMID = fORMID;
            this.PROCESSNAME = pROCESSNAME;
            this.INCIDENT = iNCIDENT;
            this.DOCUMENTNO = dOCUMENTNO;
            this.APPLICANT = aPPLICANT;
            this.APPLICANTCODE = aPPLICANTCODE;
            this.APPLICANTACCOUNT = aPPLICANTACCOUNT;
            this.REGION = rEGION;
            this.LOCATION = lOCATION;
            this.BUCODE = bUCODE;
            this.BUSSINESSNUIT = bUSSINESSNUIT;
            this.COSTCENTER = cOSTCENTER;
            this.REQUESTDATE = rEQUESTDATE;
            this.DEPARTMENT = dEPARTMENT;
            this.COMPANYCODE = cOMPANYCODE;
            this.COMPANY = cOMPANY;
            this.PROCESSSUMMARY = pROCESSSUMMARY;
            this.STATUS = sTATUS;
            this.TRSummary = tRSummary;
            this.BorrowReson = borrowReson;
            this.BorrowAmount = borrowAmount;
            this.ReturnAmount = returnAmount;
            this.Currency = currency;
            this.BankNo = bankNo;
            this.BankName = bankName;
            this.ApprovalHistoryEntity = approvalhistory;
            this.AttachmentEntity = attachment;
        }
        
        #region 实体属性
        /// <summary>
        /// FORMID
        /// </summary>
        public string FORMID { get; set; }
        
        /// <summary>
        /// PROCESSNAME
        /// </summary>
        public string PROCESSNAME { get; set; }
        
        /// <summary>
        /// INCIDENT
        /// </summary>
        public int? INCIDENT { get; set; }
        
        /// <summary>
        /// DOCUMENTNO
        /// </summary>
        public string DOCUMENTNO { get; set; }
        
        /// <summary>
        /// APPLICANT
        /// </summary>
        public string APPLICANT { get; set; }
        
        /// <summary>
        /// APPLICANTCODE
        /// </summary>
        public string APPLICANTCODE { get; set; }
        
        /// <summary>
        /// APPLICANTACCOUNT
        /// </summary>
        public string APPLICANTACCOUNT { get; set; }
        
        /// <summary>
        /// REGION
        /// </summary>
        public string REGION { get; set; }
        
        /// <summary>
        /// LOCATION
        /// </summary>
        public string LOCATION { get; set; }
        
        /// <summary>
        /// BUCODE
        /// </summary>
        public string BUCODE { get; set; }
        
        /// <summary>
        /// BUSSINESSNUIT
        /// </summary>
        public string BUSSINESSNUIT { get; set; }
        
        /// <summary>
        /// COSTCENTER
        /// </summary>
        public string COSTCENTER { get; set; }
        
        /// <summary>
        /// REQUESTDATE
        /// </summary>
        public DateTime? REQUESTDATE { get; set; }
        
        /// <summary>
        /// DEPARTMENT
        /// </summary>
        public string DEPARTMENT { get; set; }
        
        /// <summary>
        /// COMPANYCODE
        /// </summary>
        public string COMPANYCODE { get; set; }
        
        /// <summary>
        /// COMPANY
        /// </summary>
        public string COMPANY { get; set; }
        
        /// <summary>
        /// PROCESSSUMMARY
        /// </summary>
        public string PROCESSSUMMARY { get; set; }
        
        /// <summary>
        /// STATUS
        /// </summary>
        public string STATUS { get; set; }
        
        /// <summary>
        /// TRSummary
        /// </summary>
        public string TRSummary { get; set; }
        
        /// <summary>
        /// BorrowReson
        /// </summary>
        public string BorrowReson { get; set; }
        
        /// <summary>
        /// BorrowAmount
        /// </summary>
        public double? BorrowAmount { get; set; }
        
        /// <summary>
        /// ReturnAmount
        /// </summary>
        public double? ReturnAmount { get; set; }
        
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }
        
        /// <summary>
        /// BankNo
        /// </summary>
        public string BankNo { get; set; }
        
        /// <summary>
        /// BankName
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// ApprovalHistoryEntity
        /// </summary>
        public List<Presale.Process.Common.ApprovalHistoryEntity> ApprovalHistoryEntity { get; set; }
        /// <summary>
        /// AttachmentEntity
        /// </summary>
        public List<Presale.Process.Common.AttachmentEntity> AttachmentEntity { get; set; }
        #endregion
        
    }
}