using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyLib;
using Presale.Process.Common;
namespace Presale.Process.Traval
{
    public   class TravalEntity
    {

        public TravalEntity()
        { }
        public TravalEntity(string fORMID, string pROCESSNAME, int? iNCIDENT, string dOCUMENTNO, string aPPLICANT, string aPPLICANTCODE, string aPPLICANTACCOUNT, string rEGION, string lOCATION, string bUCODE, string bUSSINESSNUIT, string cOSTCENTER, DateTime? rEQUESTDATE, string dEPARTMENT, string cOMPANYCODE, string cOMPANY, string pROCESSSUMMARY, string sTATUS, string tRSummary, DateTime? travelStartDate, string travelCity, string travaelPurpose, string travalComments, string departmentCode, string travalType, string travalCount, string yDNo, string jDNo, string jPYDYes, string jPYDNo, string yDJDYes, string yDJDNo, string yWXY, string pX, string qT, string yDComments, string yDJDComments, List<MyDB> travalDeatil, List<ApprovalHistoryEntity> approvalhistory, List<AttachmentEntity> attachment)
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
            this.TravelStartDate = travelStartDate;
            this.TravelCity = travelCity;
            this.TravaelPurpose = travaelPurpose;
            this.TravalComments = travalComments;
            this.DepartmentCode = departmentCode;
            this.TravalType = travalType;
            this.TravalCount = travalCount;
            this.YDNo = yDNo;
            this.JDNo = jDNo;
            this.JPYDYes = jPYDYes;
            this.JPYDNo = jPYDNo;
            this.YDJDYes = yDJDYes;
            this.YDJDNo = yDJDNo;
            this.YWXY = yWXY;
            this.PX = pX;
            this.QT = qT;
            this.YDComments = yDComments;
            this.YDJDComments = yDJDComments;
            this.TravalEntityDetail = travalDeatil;
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
        /// TravelStartDate
        /// </summary>
        public DateTime? TravelStartDate { get; set; }

        /// <summary>
        /// TravelCity
        /// </summary>
        public string TravelCity { get; set; }

        /// <summary>
        /// TravaelPurpose
        /// </summary>
        public string TravaelPurpose { get; set; }

        /// <summary>
        /// TravalComments
        /// </summary>
        public string TravalComments { get; set; }

        /// <summary>
        /// DepartmentCode
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// TravalType
        /// </summary>
        public string TravalType { get; set; }

        /// <summary>
        /// TravalCount
        /// </summary>
        public string TravalCount { get; set; }

        /// <summary>
        /// YDNo
        /// </summary>
        public string YDNo { get; set; }

        /// <summary>
        /// JDNo
        /// </summary>
        public string JDNo { get; set; }

        /// <summary>
        /// JPYDYes
        /// </summary>
        public string JPYDYes { get; set; }

        /// <summary>
        /// JPYDNo
        /// </summary>
        public string JPYDNo { get; set; }

        /// <summary>
        /// YDJDYes
        /// </summary>
        public string YDJDYes { get; set; }

        /// <summary>
        /// YDJDNo
        /// </summary>
        public string YDJDNo { get; set; }

        /// <summary>
        /// YWXY
        /// </summary>
        public string YWXY { get; set; }

        /// <summary>
        /// PX
        /// </summary>
        public string PX { get; set; }

        /// <summary>
        /// QT
        /// </summary>
        public string QT { get; set; }

        /// <summary>
        /// YDComments
        /// </summary>
        public string YDComments { get; set; }

        /// <summary>
        /// YDJDComments
        /// </summary>
        public string YDJDComments { get; set; }
        /// <summary>
        /// TravalEntityDetail
        /// </summary>
        public List<MyDB> TravalEntityDetail { get; set; }
        /// <summary>
        /// ApprovalHistoryEntity
        /// </summary>
        public List<ApprovalHistoryEntity> ApprovalHistoryEntity { get; set; }
        /// <summary>
        /// AttachmentEntity
        /// </summary>
        public List<AttachmentEntity> AttachmentEntity { get; set; }
        #endregion
    }

   
}