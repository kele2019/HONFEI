using System;
using System.Collections.Generic;
using System.Text;
using MyLib;

namespace TaskQueueService
{
    class Program
    {
        static void Main(string[] args)
        {
             
                TaskQueueLogic logic = new TaskQueueLogic();
                logic.Run();
             
        }
    }
}
