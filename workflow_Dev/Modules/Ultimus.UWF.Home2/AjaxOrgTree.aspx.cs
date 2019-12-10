using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Home2.Code.Rep;
using Ultimus.UWF.Home2.Code.DAO;
using Ultimus.UWF.OrgChart.Entity;
using Newtonsoft.Json;

namespace Ultimus.UWF.Home2
{
    public partial class AjaxOrgTree : System.Web.UI.Page
    {
        private int? Query_ParentID { get { int i = 0; if (int.TryParse(Request["ParentID"], out i)) return i; return null; } }
        private int? RootParentID { get { int i = 0; if (int.TryParse(Request["RootParentID"], out i)) return i; return null; } }
        private int? Query_Level1 { get { int i = 0; if (int.TryParse(Request["level1"], out i)) return i; return null; } }

        private bool Query_VisibleNoValidateUser { get { int i = 0; int.TryParse(Request["VisibleNoValidateUser"], out i); return i == 1; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();

            object PARENTID = Request.QueryString["ParentID"];
            int ParentIDIndex = 0;
                if(PARENTID!=null)
                    ParentIDIndex=Convert.ToInt32(PARENTID);
            RepOrgTree orgTree = new RepOrgTree();
            orgTree.id = 0;
            orgTree.text = OrgMgmt.RootOrgName;
            int totalPageCount= 0;
            List<DepartmentEntityExt> orgs = OrgMgmt.Instance.Query("", null, null, 0, int.MaxValue, out totalPageCount);
            List<DepartmentEntityExt> rootOrgs = new List<DepartmentEntityExt>();
            if(ParentIDIndex==0)
            rootOrgs=orgs.Where(o => o.PARENTID == ParentIDIndex ).ToList();
            else
            rootOrgs = orgs.Where(o =>o.DEPARTMENTID == ParentIDIndex).ToList();
            

            Fill(rootOrgs, orgTree, orgs);

          //  if (Query_VisibleNoValidateUser)
            orgTree.children.Add(new RepOrgTree() { id = -10, text = "Invalid user" });

            string json = JsonConvert.SerializeObject(orgTree.children);
            Response.Write(json);
            Response.End();
        }

        private void Fill(List<DepartmentEntityExt> orgs, RepOrgTree parentOrgTree, List<DepartmentEntityExt> allorgs)
        {
            parentOrgTree.children = new List<RepOrgTree>();
            foreach (var org in orgs)
            {
                RepOrgTree c = new RepOrgTree();
                c.id = org.DEPARTMENTID;
                c.text = org.DEPARTMENTNAME;
                c.ext02 = org.EXT02;
                //c.state = "closed";
                parentOrgTree.children.Add(c);

                List<DepartmentEntityExt> tm = allorgs.Where(o => o.PARENTID == org.DEPARTMENTID).ToList();
                if (tm.Count > 0)
                {
                    Fill(tm, c, allorgs);
                }
            }
        }
    }
}