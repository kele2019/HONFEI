using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ultimus.UWF.Report.Entity
{
    public class PersonalExpense
    {
        public PersonalExpense()
        { }
        public PersonalExpense(string fORMID, string pROCESSNAME, int? iNCIDENT, string dOCUMENTNO, string aPPLICANT, string aPPLICANTCODE, string aPPLICANTACCOUNT, string rEGION, string lOCATION, string bUCODE, string bUSSINESSNUIT, string cOSTCENTER, DateTime? rEQUESTDATE, string dEPARTMENT, string cOMPANYCODE, string cOMPANY, string pROCESSSUMMARY, string sTATUS, string tRSummary, int? brrowYes, int? brrowNo, string currency, string borrowsAmount, string paymentAmout, string description, string countSub, string requestTravalNo, string cashAdvanceNo, string reverseAmount, string mealsCount, string giftsCount, string exhibitionCount, string teamCount, string deptCount, string workingCount, string localCount, string otherCount, string cashCount, string creditCount, string rN)
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
            this.CashAdvanceNo = cashAdvanceNo;
            this.ReverseAmount = reverseAmount;
           this.MealsCount=mealsCount;
           this.GiftsCount = giftsCount;
           this.ExhibitionCount = exhibitionCount;
           this.TeamCount = teamCount;
           this.DeptCount = deptCount;
           this.WorkingCount = workingCount;
           this.LocalCount = localCount;
           this.OtherCount =otherCount;
           this.CashCount = cashCount;
           this.CreditCount = creditCount;
           this.RN = rN;
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

        /// <summary>
        /// CashAdvanceNo
        /// </summary>
        public string CashAdvanceNo { get; set; }

        /// <summary>
        /// ReverseAmount
        /// </summary>
        public string ReverseAmount { get; set; }

       public  string MealsCount{get;set;}
       public  string GiftsCount{get;set;}
       public string ExhibitionCount { get; set; }
       public string TeamCount { get; set; }
       public string DeptCount { get; set; }
       public string WorkingCount { set; get; }
       public string LocalCount { get; set; }
       public string OtherCount { get; set; }
       public string CashCount { get; set; }
       public string CreditCount { get; set; }
       public string RN { get; set; }
        #endregion
    }
}