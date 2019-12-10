using System;
using System.Collections.Generic;

namespace EntityLibrary
{
    [Serializable]
    public class PageConfig
    {
        private string _ProcessName;

        public string ProcessName
        {
            get { return _ProcessName; }
            set { _ProcessName = value; }
        }
        private string _LoGo;

        public string LoGo
        {
            get { return _LoGo; }
            set { _LoGo = value; }
        }
        private List<PageConfig_Step> _ProcessStep;

        public List<PageConfig_Step> ProcessStep
        {
            get { return _ProcessStep; }
            set { _ProcessStep = value; }
        }
    }
}
