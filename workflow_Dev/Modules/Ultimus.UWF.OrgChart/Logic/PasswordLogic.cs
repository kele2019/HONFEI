using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;

namespace Ultimus.UWF.OrgChart.Logic
{
    public class PasswordLogic
    {
        public void ChangePassword(string path,string newPassword)
        {
            DirectoryEntry myDirectoryEntry;

            //myDirectoryEntry = new DirectoryEntry(@"WinNT://WIN-NH12D4FTL91/user1,User");
            myDirectoryEntry = new DirectoryEntry(path);

            myDirectoryEntry.Invoke("setPassword", newPassword);

            myDirectoryEntry.CommitChanges();  
        }
    }
}
