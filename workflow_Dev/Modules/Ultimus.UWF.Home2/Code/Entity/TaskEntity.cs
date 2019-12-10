using System;
using System.Collections.Generic;
using System.Text;
using MyLib;

namespace Ultimus.UWF.Home2.Code.Entity
{
    public class TaskEntity : IComparable
    {
        string taskID;

        public string TASKID
        {
            get { return trimString(taskID); }
            set { taskID = value; }
        }

        string _processName;

        public string PROCESSNAME
        {
            get { return trimString(_processName); }
            set { _processName = value; }
        }

        int _incident;

        public int INCIDENT
        {
            get { return _incident; }
            set { _incident = value; }
        }

        string _summary="";

        public string SUMMARY
        {
            get { 
               
                return trimString(_summary); }
            set { _summary = value; }
        }

        string _sourceTaskID="";

        public string SourceTaskID
        {
            get {
                string[] sz = ConvertUtil.ToString(_summary).Split(',');
                if (sz.Length == 3)
                {
                    _sourceTaskID = sz[2].Replace("]", "");
                }
                return _sourceTaskID; }
            set { _sourceTaskID = value; }
        }

        string _displaySummary="";

        public string DisplaySummary
        {
            get {
                _displaySummary = SUMMARY;
                string[] sz =ConvertUtil.ToString( _summary).Split(',');
                if (sz.Length == 3)
                {
                    _displaySummary = sz[0] + "," + sz[1] + "]";
                }
                return _displaySummary; }
            set { _displaySummary = value; }
        }

        string _HELPURL;

        public string HELPURL
        {
            get { return _HELPURL; }
            set { _HELPURL = value; }
        }

        string _stepLabel;

        public string STEPLABEL
        {
            get { return trimString(_stepLabel); }
            set { _stepLabel = value; }
        }

        bool _SYNC=true;

        public bool SYNC
        {
            get { return _SYNC; }
            set { _SYNC = value; }
        }

        string _stepID;

        public string STEPID
        {
            get { return trimString(_stepID); }
            set { _stepID = value; }
        }

        int _status;

        public int STATUS
        {
            get { return _status; }
            set { _status = value; }
        }
         
        int _processStatus;

        public int PROCESSSTATUS
        {
            get { return _processStatus; }
            set { _processStatus = value; }
        }


        int _subStatus;

        public int SUBSTATUS
        {
            get { return _subStatus; }
            set { _subStatus = value; }
        }

        DateTime _startTime;

        public DateTime STARTTIME
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        DateTime _endTime;

        public DateTime ENDTIME
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        DateTime _overdueTime;

        public DateTime OVERDUETIME
        {
            get { return _overdueTime; }
            set { _overdueTime = value; }
        }

        string _taskUser;

        public string TASKUSER
        {
            get { return trimString(_taskUser); }
            set { _taskUser = value; }
        }

        string _assignedtoUser;

        public string ASSIGNEDTOUSER
        {
            get { return trimString(_assignedtoUser); }
            set { _assignedtoUser = value; }
        }

        string _INITIATOR;

        public string INITIATOR
        {
            get { return _INITIATOR; }
            set { _INITIATOR = value; }
        }

        string _APLICANT;

        public string APLICANT
        {
            get { return trimString(_APLICANT); }
            set { _APLICANT = value; }
        }

        string _DEPARTMENT;

        public string DEPARTMENT
        {
            get { return trimString(_DEPARTMENT); }
            set { _DEPARTMENT = value; }
        }


        string _ServiceName;
        /// <summary>
        /// 数据库表 COM_APPSETTINGS 中 Name 配置项值，对应各个版本所在服务器的名称。
        /// </summary>
        public string SERVERNAME
        {
            get { return _ServiceName; }
            set { _ServiceName = value; }
        }

        private string _SERVERTASKID;

        public string SERVERTASKID
        {
            get {
                if (string.IsNullOrEmpty(_SERVERTASKID))
                {
                    _SERVERTASKID = SERVERNAME+"_" + TASKID;
                }
                return _SERVERTASKID; }
            set { _SERVERTASKID = value; }
        }

        string _FORMURL;

        public string FORMURL
        {
            get {
                if (string.IsNullOrEmpty(_FORMURL))
                {
                    _FORMURL = ConfigurationManager.AppSettings["RootPath"] + "/Modules/Ultimus.UWF.Workflow/OpenForm.aspx?taskid="+TASKID+"&ServerName="+SERVERNAME;
                }
                return _FORMURL; }
            set { _FORMURL = value; }
        }

        string _ERRORMESSAGE;

        public string ERRORMESSAGE
        {
            get { return _ERRORMESSAGE; }
            set { _ERRORMESSAGE = value; }
        }

        List<VarEntity> _vars = new List<VarEntity>();

        public List<VarEntity> VarList
        {
            get { return _vars; }
            set { _vars = value; }
        }

        string _REASON;

        public string REASON
        {
            get { return _REASON; }
            set { _REASON = value; }
        }

        string _COMMENTS;

        public string COMMENTS
        {
            get { return _COMMENTS; }
            set { _COMMENTS = value; }
        }


        string _TYPE;

        public string TYPE
        {
            get { return _TYPE; }
            set { _TYPE = value; }
        }

        private string trimString(string val)
        {
            if (val != null) return val.Trim();
            return val;
        }

        public int CompareTo(object obj)
        {
            TaskEntity pe = obj as TaskEntity;
            return string.Compare(this.PROCESSNAME, pe.PROCESSNAME);
        }
    }
}
