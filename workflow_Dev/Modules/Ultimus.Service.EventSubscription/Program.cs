using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using MyLib;

namespace Ultimus.Service.EventSubscription
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                LogUtil.Info("未执行Subscription...");
            }
            else
            {
                EventRun er = new EventRun();
                er.Run();

                SubscriptionRun sr = new SubscriptionRun();
                sr.Run();
            }

        }
    }
}
