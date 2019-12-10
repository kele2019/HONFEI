using System;

namespace EntityLibrary
{
    [Serializable]
    public class Tasks
    {
        private string _TaskID;

        public string TaskID
        {
            get { return _TaskID; }
            set { _TaskID = value; }
        }
        private string _ProcessName;

        public string ProcessName
        {
            get { return _ProcessName; }
            set { _ProcessName = value; }
        }
        private int _Incident;

        public int Incident
        {
            get { return _Incident; }
            set { _Incident = value; }
        }
        private string _StepName;

        public string StepName
        {
            get { return _StepName; }
            set { _StepName = value; }
        }
        private string _Summary;

        public string Summary
        {
            get { return _Summary; }
            set { _Summary = value; }
        }

        private double _StartTime;

        public double StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }

        private double _EndTime;

        public double EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
    }
}
