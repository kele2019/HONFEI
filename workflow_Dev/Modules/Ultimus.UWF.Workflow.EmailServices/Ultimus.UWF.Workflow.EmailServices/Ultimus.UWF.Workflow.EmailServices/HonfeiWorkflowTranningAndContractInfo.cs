using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ptcent;
using System.Data;
using MyLib;
using System.IO;

namespace Ultimus.UWF.Workflow.EmailServices
{
    class HonfeiWorkflowTranningAndContractInfo : BaseJob
    {

        public string LogName { get { return "TranningAndContractInfo"; } }
        protected override void OnStart()
        {
            try
            {
                Program.GetEmployeeContractInfo();
                Program.GetEmployeeTraining();
            }
            catch (Exception err)
            {
                LogFactory.All.WriteWithError(LogName, err);
            }
        }


        protected override void OnStop()
        {

        }
    }
}
