using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Home2.Code.Rep;
using Ultimus.UWF.Home2.Code.DAO;

namespace Ultimus.UWF.Home2
{
    public partial class FillOrgPath : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnFill_Click(object sender, EventArgs e)
        {
            int totalPageCount = 0;
            List<DepartmentEntityExt> orgs = OrgMgmt.Instance.Query("", null, null, 0, int.MaxValue, out totalPageCount);
            DepartmentEntityExt rootOrg = orgs.Where(o => o.PARENTID == 0).FirstOrDefault();

            rootOrg.Path = ".";
            OrgMgmt.Instance.Update(rootOrg);
            Fill(rootOrg, orgs);
        }


        private void Fill(DepartmentEntityExt parentOrg, List<DepartmentEntityExt> allorgs)
        {
            var childOrgs = allorgs.Where(o => o.PARENTID == parentOrg.DEPARTMENTID).ToList();
            foreach (var childOrg in childOrgs)
            {
                childOrg.Path = (parentOrg.Path + string.Empty).TrimEnd('.') + "." + parentOrg.DEPARTMENTID + ".";
                OrgMgmt.Instance.Update(childOrg);
                Fill(childOrg, allorgs);
            }
        }
    }
}