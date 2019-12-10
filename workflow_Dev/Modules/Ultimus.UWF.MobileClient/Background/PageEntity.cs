using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityLibrary;

namespace MobileClient.Background
{
    public class PageEntity
    {

        string _ProcessName;

        public string ProcessName
        {
            get { return _ProcessName; }
            set { _ProcessName = value; }
        }

        string _StepName;

        public string StepName
        {
            get { return _StepName; }
            set { _StepName = value; }
        }

        List<PageConfig_Control> _Controls;

        public List<PageConfig_Control> Controls
        {
            get { return _Controls; }
            set { _Controls = value; }
        }
        List<PageConfig_Control> _Buttons;

        public List<PageConfig_Control> Buttons
        {
            get { return _Buttons; }
            set { _Buttons = value; }
        }
        List<string> _Grids;

        public List<string> Grids
        {
            get { return _Grids; }
            set { _Grids = value; }
        }

        List<string> _Attchments;

        public List<string> Attchments
        {
            get { return _Attchments; }
            set { _Attchments = value; }
        }
    }
}