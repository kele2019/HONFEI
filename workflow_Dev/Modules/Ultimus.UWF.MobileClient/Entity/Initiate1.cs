using System;

namespace EntityLibrary
{
    [Serializable]
    public class Initiate
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
    }
}
