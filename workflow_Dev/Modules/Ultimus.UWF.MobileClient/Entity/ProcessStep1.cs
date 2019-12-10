using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLibrary
{
    [Serializable]
    public class ProcessStep
    {
        private string _ProcessName;

        public string ProcessName
        {
            get { return _ProcessName; }
            set { _ProcessName = value; }
        }
        private string _ProcessVersion;

        public string ProcessVersion
        {
            get { return _ProcessVersion; }
            set { _ProcessVersion = value; }
        }
        private string _StepID;

        public string StepID
        {
            get { return _StepID; }
            set { _StepID = value; }
        }
        private string _StepName;

        public string StepName
        {
            get { return _StepName; }
            set { _StepName = value; }
        }
        
    }
}
