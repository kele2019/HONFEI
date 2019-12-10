using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Home2.Code.Rep;
using Ultimus.UWF.Home2.Code.DAO;
using Newtonsoft.Json;
using Ultimus.UWF.Home2.Code.Entity;

namespace Ultimus.UWF.Home2
{
    public partial class AjaxOrgUserTree : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();

            RepTreeNode orgTree = new RepTreeNode();
            orgTree.id = "0";
            orgTree.text = OrgMgmt.RootOrgName;
            int totalPageCount = 0;
            List<DepartmentEntityExt> orgs = OrgMgmt.Instance.Query("", null, null, 0, int.MaxValue, out totalPageCount);
            List<DepartmentEntityExt> rootOrgs = orgs.Where(o => o.PARENTID == 0).ToList();

            int totalPageCount2 = 0;
            List<UserEntityExt> users = UserMgmt.Instance.Query(null, null, true, null, 0, int.MaxValue, out totalPageCount2);
            Fill(rootOrgs, orgTree, orgs, users);
            string json = JsonConvert.SerializeObject(orgTree.children);
            Response.Write(json);
            Response.End();
        }

        private void Fill(List<DepartmentEntityExt> orgs, RepTreeNode parentOrgTree, List<DepartmentEntityExt> allorgs, List<UserEntityExt> users)
        {
            parentOrgTree.children = new List<RepTreeNode>();
            foreach (var org in orgs)
            {
                RepTreeNode c = new RepTreeNode();
                c.id = org.DEPARTMENTID.ToString();
                c.text = org.DEPARTMENTNAME;
                c.type = "org";
                parentOrgTree.children.Add(c);

                List<DepartmentEntityExt> tm = allorgs.Where(o => o.PARENTID == org.DEPARTMENTID).ToList();
                if (tm.Count > 0)
                {
                    Fill(tm, c, allorgs, users);
                }

                List<UserEntityExt> us = users.Where(o => o.OrgID == org.DEPARTMENTID).ToList();
                foreach (var u in us)
                {
                    RepTreeNode unode = new RepTreeNode();
                    unode.id = u.LOGINNAME;
                    unode.text = u.USERNAME;
                    unode.EngName = u.EngName;
                    unode.type = "user";
                    parentOrgTree.children.Add(unode);
                }
            }
        }
    }
}