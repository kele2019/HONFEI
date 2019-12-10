using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ultimus.UWF.Workflow.Entity
{
    public class PersonalAllownce
    {
        public PersonalAllownce()
        { 
        
        }
        public PersonalAllownce(string fORMID, string pROCESSNAME, int? iNCIDENT, string dOCUMENTNO, string aPPLICANT, string aPPLICANTCODE, string aPPLICANTACCOUNT, string rEGION, string lOCATION, string bUCODE, string bUSSINESSNUIT, string cOSTCENTER, DateTime? rEQUESTDATE, string dEPARTMENT, string cOMPANYCODE, string cOMPANY, string pROCESSSUMMARY, string sTATUS, string tRSummary, int? brrowYes, int? brrowNo, string currency, string borrowsAmount, string paymentAmout, string description, string countSub, string requestTravalNo, List<PersonalAllownceDetail> personalAllownceDetail, List<Presale.Process.Common.ApprovalHistoryEntity> approvalhistory, List<Presale.Process.Common.AttachmentEntity> attachment)
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
            this.BrrowYes = brrowYes;
            this.BrrowNo = brrowNo;
            this.Currency = currency;
            this.BorrowsAmount = borrowsAmount;
            this.PaymentAmout = paymentAmout;
            this.Description = description;
            this.CountSub = countSub;
            this.RequestTravalNo = requestTravalNo;
            this.PersonalAllownceDetail = personalAllownceDetail;
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
        /// BrrowYes
        /// </summary>
        public int? BrrowYes { get; set; }
        
        /// <summary>
        /// BrrowNo
        /// </summary>
        public int? BrrowNo { get; set; }
        
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }
        
        /// <summary>
        /// BorrowsAmount
        /// </summary>
        public string BorrowsAmount { get; set; }
        
        /// <summary>
        /// PaymentAmout
        /// </summary>
        public string PaymentAmout { get; set; }
        
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// CountSub
        /// </summary>
        public string CountSub { get; set; }
        
        /// <summary>
        /// RequestTravalNo
        /// </summary>
        public string RequestTravalNo { get; set; }
        public List<PersonalAllownceDetail> PersonalAllownceDetail { get; set; }
        public List<Presale.Process.Common.ApprovalHistoryEntity> ApprovalHistoryEntity { get; set; }
        /// <summary>
        /// AttachmentEntity
        /// </summary>
        public List<Presale.Process.Common.AttachmentEntity> AttachmentEntity { get; set; }
        #endregion
    }
    public class PersonalAllownceDetail
    {
         public PersonalAllownceDetail()
        {       
         }
         public PersonalAllownceDetail(string fORMID, string pROCESSNAME, int? iNCIDENT, int? rOWID, string currency, DateTime? paymentDate, DateTime? paymentEndDate, string costCenter, string houseRental, string phone, string transportation, string other, string gifts, string remarks, string rate, string subTotal)
        {
            this.FORMID = fORMID;
            this.PROCESSNAME = pROCESSNAME;
            this.INCIDENT = iNCIDENT;
            this.ROWID = rOWID;
            this.Currency = currency;
            this.PaymentDate = paymentDate;
            this.PaymentEndDate = paymentEndDate;
            this.CostCenter = costCenter;
            this.HouseRental = houseRental;
            this.Phone = phone;
            this.Transportation = transportation;
            this.Other = other;
            this.Gifts = gifts;
            this.Remarks = remarks;
            this.Rate = rate;
            this.SubTotal = subTotal;
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
        /// ROWID
        /// </summary>
        public int? ROWID { get; set; }
        
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }
        
        /// <summary>
        /// PaymentDate
        /// </summary>
        public DateTime? PaymentDate { get; set; }
        
        /// <summary>
        /// PaymentEndDate
        /// </summary>
        public DateTime? PaymentEndDate { get; set; }
        
        /// <summary>
        /// CostCenter
        /// </summary>
        public string CostCenter { get; set; }
        
        /// <summary>
        /// HouseRental
        /// </summary>
        public string HouseRental { get; set; }
        
        /// <summary>
        /// Phone
        /// </summary>
        public string Phone { get; set; }
        
        /// <summary>
        /// Transportation
        /// </summary>
        public string Transportation { get; set; }
        
        /// <summary>
        /// Other
        /// </summary>
        public string Other { get; set; }
        
        /// <summary>
        /// Gifts
        /// </summary>
        public string Gifts { get; set; }
        
        /// <summary>
        /// Remarks
        /// </summary>
        public string Remarks { get; set; }
        
        /// <summary>
        /// Rate
        /// </summary>
        public string Rate { get; set; }
        
        /// <summary>
        /// SubTotal
        /// </summary>
        public string SubTotal { get; set; }
        #endregion
    }
}