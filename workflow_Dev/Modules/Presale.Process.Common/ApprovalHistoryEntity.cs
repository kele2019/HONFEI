using System;
using System.Collections.Generic;
using System.Web;

namespace Presale.Process.Common
{
    [Serializable]
    public class ApprovalHistoryEntity
    {
        string _ID;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        string _ProcessName;

        public string ProcessName
        {
            get { return _ProcessName; }
            set { _ProcessName = value; }
        }
        int _Incident;

        public int Incident
        {
            get { return _Incident; }
            set { _Incident = value; }
        }
        string _StepName;

        public string STEPNAME
        {
            get { return _StepName; }
            set { _StepName = value; }
        }
        string _Level;

        public string Level
        {
            get { return _Level; }
            set { _Level = value; }
        }
        string _Approver;

        public string APPROVERNAME
        {
            get { return _Approver; }
            set { _Approver = value; }
        }
        string _ApproverFrom;

        public string ApproverFrom
        {
            get { return _ApproverFrom; }
            set { _ApproverFrom = value; }
        }
        string _Action;

        public string ACTION
        {
            get { return _Action; }
            set { _Action = value; }
        }
        string _Comments;

        public string COMMENTS
        {
            get { return _Comments; }
            set { _Comments = value; }
        }
        DateTime _ApproveDate;

        public DateTime CREATEDATE
        {
            get { return _ApproveDate; }
            set { _ApproveDate = value; }
        }
        string _Status;

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        string _Ext01;

        public string Ext01
        {
            get { return _Ext01; }
            set { _Ext01 = value; }
        }
        string _Ext02;

        public string Ext02
        {
            get { return _Ext02; }
            set { _Ext02 = value; }
        }
        string _Ext03;

        public string Ext03
        {
            get { return _Ext03; }
            set { _Ext03 = value; }
        }
        string _Ext04;

        public string Ext04
        {
            get { return _Ext04; }
            set { _Ext04 = value; }
        }
        string _Ext05;

        public string Ext05
        {
            get { return _Ext05; }
            set { _Ext05 = value; }
        }
        string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
    }
}