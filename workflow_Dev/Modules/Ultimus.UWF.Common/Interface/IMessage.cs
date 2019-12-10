using System;
using System.Collections.Generic;
using System.Text;
using Ultimus.UWF.Common.Entity;

namespace Ultimus.UWF.Common.Interface
{
    public interface IMessage 
    {
        void Send(MessageEntity msg);
    }
}
