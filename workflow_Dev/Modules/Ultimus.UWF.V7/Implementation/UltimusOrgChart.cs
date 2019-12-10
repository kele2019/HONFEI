using System;
using System.Collections.Generic;
using System.Text;
using Ultimus.UWF.OrgChart.Interface;
using Ultimus.UWF.OrgChart.Implementation;
using Ultimus.UWF.OrgChart.Entity;

namespace Ultimus.UWF.V7.Implementation
{
    public class UltimusOrgChart : Ultimus.UWF.OrgChart.Implementation.DatabaseOrgLogic
    {
        public override UserEntity GetUserEntity(string loginName)
        {
            UserEntity ety = new UserEntity();
            Ultimus.OC.OrgChart oc = new Ultimus.OC.OrgChart();
            Ultimus.OC.User user = new Ultimus.OC.User();
            oc.FindUser(loginName.Replace("\\", "/"), "", 0, out user);

            ety.LOGINNAME = loginName;
            ety.USERNAME = user.strUserFullName;
            ety.USERID = user.nUserID;
            ety.JOBFUNCTION = user.strJobFunction;
            ety.DEPARTMENT = user.strDepartmentName;
            //UserPreferences up = null;
            //user.GetUserPrefs(out up);
            //ety.EMAIL = up.strEmail;
            //string supervisor = "";
            //user.GetSupervisor(out supervisor);
            //ety.DIRECTREPORTNAME = supervisor;
            return ety;
        }
    }
}
