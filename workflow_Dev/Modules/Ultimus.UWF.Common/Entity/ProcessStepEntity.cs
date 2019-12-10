using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ultimus.UWF.Common.Entity
{
    public class ProcessStepEntity
    {
        string _ProcessName;

        public string ProcessName
        {
            get { return _ProcessName; }
            set { _ProcessName = value; }
        }
        string _stepName;

        public string StepName
        {
            get { return _stepName; }
            set { _stepName = value; }
        }

        string _pageUrl;

        public string PageUrl
        {
            get { return _pageUrl; }
            set { _pageUrl = value; }
        }

        string _type;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
    }
}
