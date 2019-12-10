using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using Ultimus.UWF.Report.Entity;

namespace Ultimus.UWF.Report
{
	public partial class DocumentReviewBallotingForm : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string FormId = Request.QueryString["FormID"];

			string sqlLog1 = "select INCIDENT from PROC_QualityDocumentManagement where FORMID = " + FormId;
			string incident = DataAccess.Instance("BizDB").ExecuteScalar(sqlLog1).ToString();

            string sql = "select APPLICANTACCOUNT,OperMode,MajorChange,DOCDescription,documentName,documentNumber,documentOwner,deptMan,deptQM,deptEng,deptPM,deptOP,deptAdmin,deptFin,deptHR,deptIT,deptPUR,FollowUPAction,deptGM from PROC_QualityDocumentManagement where status = '2' and FormId = " + FormId;
			DocumentEntity document = DataAccess.Instance("BizDB").ExecuteEntity<DocumentEntity>(sql);
            if (document != null)
            {
                int lastIndex=document.documentName.LastIndexOf('/')+1;
                int Length=document.documentName.Length;
                DocumentName.Text = document.documentName.Substring(lastIndex,Length-lastIndex);
                DocumentNo.Text = document.documentNumber;
                DocumentOwner.Text = document.documentOwner;
                Status.Text = document.OperMode;
                DocDescription.Text = document.DOCDescription;
                MajorChange.Text = document.MajorChange;
                deptMan.Text = document.deptMan;
                deptQM.Text = document.deptQM;
                deptEng.Text = document.deptEng;
                deptPM.Text = document.deptPM;
                deptOP.Text = document.deptOP;
                deptHR.Text = document.deptHR;
                deptFin.Text = document.deptFin;
                deptAdmin.Text = document.deptAdmin;
                deptIT.Text = document.deptIT;
                deptPUR.Text = document.deptPUR;
                deptGM.Text = document.deptGM;
                FollowUPAction.Text = document.FollowUPAction;

                int incdent = int.Parse(incident);
                if (document.deptMan == "yes")
                {
                    Image deptManImg = (Image)Page.FindControl("deptManImg");
                    Label deptManSignDate = Page.FindControl("deptManSignDate") as Label;
                    Label deptManComments = Page.FindControl("deptManComments") as Label;
                   //getLogAndImg(deptManImg, deptManSignDate, deptManComments, incdent, "QAM1");
                    getLogAndImg(deptManImg, deptManSignDate, deptManComments, incdent, "QAMLogin"); 
                }
                if (document.deptQM == "yes")
                {
                    Image deptQMImg = (Image)Page.FindControl("deptQMImg");
                    Label deptQMSignDate = Page.FindControl("deptQMSignDate") as Label;
                    Label deptQMComments = Page.FindControl("deptQMComments") as Label;
                    //getLogAndImg(deptQMImg, deptQMSignDate, deptQMComments, incdent, "QAM");
                    getLogAndImg(deptQMImg, deptQMSignDate, deptQMComments, incdent, "QAMLogin"); 
                }
                if (document.deptEng == "yes")
                {
                    Image deptEngImg = (Image)Page.FindControl("deptEngImg");
                    Label deptEngSignDate = Page.FindControl("deptEngSignDate") as Label;
                    Label deptEngComments = Page.FindControl("deptEngComments") as Label;
                   // getLogAndImg(deptEngImg, deptEngSignDate, deptEngComments, incdent, "CTO");
                    getLogAndImg(deptEngImg, deptEngSignDate, deptEngComments, incdent, "CTOLogin");
                    
                }
                if (document.deptPM == "yes")
                {
                    Image deptPMImg = (Image)Page.FindControl("deptPMImg");
                    Label deptPMSignDate = Page.FindControl("deptPMSignDate") as Label;
                    Label deptPMComments = Page.FindControl("deptPMComments") as Label;
                    //getLogAndImg(deptPMImg, deptPMSignDate, deptPMComments, incdent, "PM");
                    getLogAndImg(deptPMImg, deptPMSignDate, deptPMComments, incdent, "PMLogin");
                }
                if (document.deptOP == "yes")
                {
                    Image deptOPImg = (Image)Page.FindControl("deptOPImg");
                    Label deptOPSignDate = Page.FindControl("deptOPSignDate") as Label;
                    Label deptOPComments = Page.FindControl("deptOPComments") as Label;
                    //getLogAndImg(deptOPImg, deptOPSignDate, deptOPComments, incdent, "DGM");
                    getLogAndImg(deptOPImg, deptOPSignDate, deptOPComments, incdent, "DGMLogin");

                }
                if (document.deptHR == "yes")
                {
                    Image deptHRImg = (Image)Page.FindControl("deptHRImg");
                    Label deptAdminSignDate = Page.FindControl("deptAdminSignDate") as Label;
                    Label deptAdminComments = Page.FindControl("deptAdminComments") as Label;
                    //getLogAndImg(deptAdminImg, deptAdminSignDate, deptAdminComments, incdent, "Admin Assistant");
                    getLogAndImg(deptHRImg, deptAdminSignDate, deptAdminComments, incdent, "HRMLogin");
                }
                if (document.deptFin == "yes")
                {
                    Image detpFinImg = (Image)Page.FindControl("detpFinImg");
                    Label deptFinSignDate = Page.FindControl("deptFinSignDate") as Label;
                    Label deptFinComments = Page.FindControl("deptFinComments") as Label;
                    //getLogAndImg(detpFinImg, deptFinSignDate, deptFinComments, incdent, "CFO");
                    getLogAndImg(detpFinImg, deptFinSignDate, deptFinComments, incdent, "CFOLogin");
                }
                if (document.deptAdmin == "yes")
                {
                    Image deptHRImg = (Image)Page.FindControl("deptAdminImg");
                    Label deptHRSignDate = Page.FindControl("deptHRSignDate") as Label;
                    Label deptHRComments = Page.FindControl("deptFinComments") as Label;
                   // getLogAndImg(deptHRImg, deptHRSignDate, deptHRComments, incdent, "HRM");
                    getLogAndImg(deptHRImg, deptHRSignDate, deptHRComments, incdent, "AdimLogin");
                }
                if (document.deptIT == "yes")
                {
                    Image deptITImg = (Image)Page.FindControl("deptITImg");
                    Label deptITSignDate = Page.FindControl("deptITSignDate") as Label;
                    Label deptITComments = Page.FindControl("deptITComments") as Label;
                   // getLogAndImg(deptITImg, deptITSignDate, deptITComments, incdent, "IT Manager");
                    getLogAndImg(deptITImg, deptITSignDate, deptITComments, incdent, "ITMLogin");
                }
                if (document.deptPUR == "yes")
                {
                    Image deptPURImg = (Image)Page.FindControl("deptPURImg");
                    Label deptPURSignDate = Page.FindControl("deptPURSignDate") as Label;
                    Label deptPURComments = Page.FindControl("deptPURComments") as Label;
                  //  getLogAndImg(deptPURImg, deptPURSignDate, deptPURComments, incdent, "Supply Chain Manager");
                    getLogAndImg(deptPURImg, deptPURSignDate, deptPURComments, incdent, "SupplierMLogin");
                }
                if (document.deptGM == "yes")
                {
                    Image deptPURImg = (Image)Page.FindControl("DeptGMImg");
                    Label deptPURSignDate = Page.FindControl("deptGMSignDate") as Label;
                    Label deptPURComments = Page.FindControl("deptGMComments") as Label;
                   // getLogAndImg(deptPURImg, deptPURSignDate, deptPURComments, incdent, "GM");
                    getLogAndImg(deptPURImg, deptPURSignDate, deptPURComments, incdent, "GMLogin");
                }

                Image ApplierImg = (Image)Page.FindControl("ApplierImg");
                string sqlImg = "select EXT04 from ORG_USER where loginname = '" + document.APPLICANTACCOUNT.Replace("/","\\") + "'";
                string imgName = DataAccess.Instance("BizDB").ExecuteScalar(sqlImg).ToString();
                string applierImg = "/Modules/Ultimus.UWF.Form.ProcessControl/img/" + imgName + ".png";
                ApplierImg.ImageUrl = applierImg;
                ApplierImg.AlternateText = imgName;
                string sqlDate = "select CREATEDATE from WF_APPROVALHISTORY where STEPNAME='Applier Conifrm' and PROCESSNAME = 'Quality document management' and INCIDENT =" + incdent;
                CompleteDate.Text = Convert.ToDateTime(DataAccess.Instance("BizDB").ExecuteScalar(sqlDate)).ToString("yyyy-MM-dd");
            }
		}
        protected void getLogAndImg(Image img, Label signDateLabel, Label commentLabel, int incident, string StepName)
        {
			//img.ImageUrl = getImgURL(post);
			DocumentDetailEntity documentLogDetail = getLogDetail(incident, StepName);
            if (documentLogDetail != null)
            {
                img.ImageUrl = "/Modules/Ultimus.UWF.Form.ProcessControl/img/" + documentLogDetail.EXT04+ ".png";
                signDateLabel.Text = Convert.ToDateTime(documentLogDetail.CREATEDATE).ToString("yyyy-MM-dd");
                commentLabel.Text = documentLogDetail.COMMENTS;
            }
		}

		private DocumentDetailEntity getLogDetail(int incident, string StepName)
		{
			//string sqlLog = "select a.COMMENTS,a.CREATEDATE from WF_APPROVALHISTORY a left join ORG_USER b on a.EXT01 = b.LOGINNAME where a.PROCESSNAME = 'Quality document management' and INCIDENT = " + incident + " and b.EXT03 = '" + post + "'";
			//string sqlLog = "select COMMENTS,CREATEDATE from (select a.COMMENTS,a.CREATEDATE,b.EXT13 from WF_APPROVALHISTORY a left join ORG_USER b on a.EXT01 = b.LOGINNAME where a.PROCESSNAME = 'Quality document management' and a.INCIDENT = " + incident + ")C where EXT13 = '" + post + "'";

            //string sqlLog = "select a.*,b.EXT04  from ( select  COMMENTS, CREATEDATE,EXT01 from WF_APPROVALHISTORY  where   PROCESSNAME = 'Quality document management'   and  INCIDENT ='"+incident+"' and STEPNAME='"+StepName+"' )a  left join ORG_USER b on a.EXT01 = b.LOGINNAME";
            string sqlLog = "  SELECT D.*,B.EXT04 FROM (  select TOP(1) a.* from ( select  COMMENTS, CREATEDATE,EXT01 from WF_APPROVALHISTORY  where "+
  "PROCESSNAME = 'Quality document management' and  INCIDENT ='"+incident+"' )a   RIGHT join   (select  REPLACE((REPLACE("+StepName+",'/','\\')),'|USER','') USERNAME from  PROC_QualityDocumentManagement where INCIDENT='"+incident+"') C"+
  " ON A.EXT01=C.USERNAME   ORDER BY CREATEDATE DESC) D  left join ORG_USER b on D.EXT01 = b.LOGINNAME";
			DocumentDetailEntity documentLog = DataAccess.Instance("BizDB").ExecuteEntity<DocumentDetailEntity>(sqlLog);
			return documentLog;
		}

		private string getImgURL(string post)
		{
			string sqlImg = "select EXT04 from ORG_USER where EXT13 = '" + post + "'";
			string imgName = DataAccess.Instance("BizDB").ExecuteScalar(sqlImg).ToString();
			string imgURL = "/Modules/Ultimus.UWF.Form.ProcessControl/img/" + imgName + ".png";
			return imgURL;
		}
		
	}
}