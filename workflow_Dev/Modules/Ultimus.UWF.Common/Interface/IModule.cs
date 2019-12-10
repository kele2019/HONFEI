using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ultimus.UWF.Common.Entity;

namespace Ultimus.UWF.Common.Interface
{
    public interface IModule
    {
        void Install();
        void UnInstall();
    }
}
