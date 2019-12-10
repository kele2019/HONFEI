using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLibrary
{
    [Serializable]
    public class UserInfo
    {
        private string _UserFullName;

        public string UserFullName
        {
            get { return _UserFullName; }
            set { _UserFullName = value; }
        }
        private string _UserAccount;

        public string UserAccount
        {
            get { return _UserAccount; }
            set { _UserAccount = value; }
        }
        private string _UserEmail;

        public string UserEmail
        {
            get { return _UserEmail; }
            set { _UserEmail = value; }
        }
        private string _UserDepartment;

        public string UserDepartment
        {
            get { return _UserDepartment; }
            set { _UserDepartment = value; }
        }
        private string _JobFunction;

        public string JobFunction
        {
            get { return _JobFunction; }
            set { _JobFunction = value; }
        }
    }
}
