using System;
using System.Collections.Generic;

namespace EntityLibrary
{
    [Serializable]
    public class PageConfig_Step
    {
        private string _StepName;

        public string StepName
        {
            get { return _StepName; }
            set { _StepName = value; }
        }
        private List<PageConfig_Control> _StepControl;

        public List<PageConfig_Control> StepControl
        {
            get { return _StepControl; }
            set { _StepControl = value; }
        }

        int _stepID;

        public int StepID
        {
            get { return _stepID; }
            set { _stepID = value; }
        }
    }
}
