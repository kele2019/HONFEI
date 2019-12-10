using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyLib;

namespace Ultimus.UWF.Common.Implementation
{
    public class UWFEncrypt:IEncrypt
    {
        string _key = "afd##43@@%$$*";
        public string Decrypt(string text)
        {
            EncryptUtil.Key = _key;
            return EncryptUtil.Decrypt(text); 
        }

        public string Encrypt(string text)
        {
            EncryptUtil.Key = _key;
            return EncryptUtil.Encrypt(text);
        }

    }
}