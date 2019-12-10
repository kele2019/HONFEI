using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using Ultimus;
using Ultimus.OC;
using Ultimus.UWF.Common.Logic;
//using SSO;

namespace BPM.ReportDesign.AjaxPage
{
    /// <summary>
    /// GetUserInfo 的摘要说明
    /// </summary>
    public class GetUserInfo : IHttpHandler, IRequiresSessionState
    {
        //private Helper helper = new Helper();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Write( SessionLogic.GetLoginName());
        }

        public string tttt()
        {
            return  SessionLogic.GetLoginName();
        }

        public string GetDepart()
        {
            string str = "";
            OrgChart oc = new OrgChart();
            User u;
            oc.FindUser( SessionLogic.GetLoginName().Replace("\\","/"), "", "", out u);
            if (u != null)
            {
                Ultimus.OC.Department depart;
                oc.FindDepartment(u.strDepartmentName, "", out depart);
                if (depart != null)
                {
                    User[] list;
                    depart.GetDepartmentMembers(out list);
                    if (list != null)
                    {
                        foreach (User uu in list)
                        {
                            str += "'" + uu.strUserName + "',";
                        }
                        Ultimus.OC.Department[] dd;
                        depart.GetSubDepartments(out dd);
                        if (dd != null)
                        {
                            foreach (Ultimus.OC.Department d1 in dd)
                            {
                                User[] uu;
                                d1.GetDepartmentMembers(out uu);
                                if (uu != null)
                                {
                                    foreach (User uuu in uu)
                                    {
                                        str += "'" + uuu.strUserName + "',";
                                    }
                                }
                            }
                        }
                        str = str.Substring(0, str.LastIndexOf(','));
                    }
                    else
                    {
                        str = "''";
                    }
                }
                else
                {
                    str = "''";
                }
            }

            return str;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}