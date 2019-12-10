using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MyLib;
using System.Web;
using Ultimus.UWF.OrgChart.Interface;
using Ultimus.UWF.OrgChart.Entity;
using Ultimus.UWF.OrgChart.Implementation;

namespace Ultimus.UWF.V8.Implementation
{
    public class UltimusOrgChart : DatabaseOrgLogic
    {
        public override UserEntity GetUserEntity(string loginName)
        {
            loginName=loginName.Replace("\\","/");
            UserEntity user = new UserEntity();
            if (HttpContext.Current != null)
            {
                if ( HttpContext.Current.Session!=null && HttpContext.Current.Session["LoginUser"] != null )
                {
                    user = HttpContext.Current.Session["LoginUser"] as UserEntity;
                    if (user.LOGINNAME!=null && user.LOGINNAME.Equals(loginName))
                    {
                        return user;
                    }
                }
            }

            Ultimus.OC.User u = new Ultimus.OC.User();
            Ultimus.OC.OrgChart oc = new OC.OrgChart();
            oc.FindUser(loginName.Trim().Replace("\\", "/"), "", "", out u);
            if (u == null)
            {
                oc = new Ultimus.OC.OrgChart(loginName.Split('/')[0]);
                oc.FindUser(loginName.Replace("\\", "/"), "", "", out u);
            }

            if (u != null)
            {
                user.LOGINNAME = loginName;
                user.USERNAME = u.strUserFullName;
                user.USERID = ConvertUtil.ToInt32(u.strUserID);
                user.JOBFUNCTION = u.strJobFunction;
                user.DEPARTMENT = u.strDepartmentName;
                string error = "";
                user.EMAIL = oc.GetEmailAddress(loginName.Replace("\\", "/"), out error);
            }
            else
            {
                user = new UserEntity();
                user.LOGINNAME = loginName;
            }

            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session["LoginUser"] = user;
            }

            return user;
        }

    }
}
