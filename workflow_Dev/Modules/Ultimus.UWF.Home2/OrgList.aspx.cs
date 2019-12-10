using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.OrgChart.Entity;
using Ultimus.UWF.Home2.Code.DAO;

namespace Ultimus.UWF.Home2
{
    public partial class OrgList : System.Web.UI.Page
    {
        protected int ParentID { get { int i = 0; int.TryParse(Request.QueryString["ParentID"], out i); return i; } }
        protected int PageIndex { get { return AspNetPager1.CurrentPageIndex; } }
        protected int PageSize = 10;

        private Dictionary<int, string> OrgIDNameDic = new Dictionary<int, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("OrgOrUser", "org");
            Response.Cookies.Add(cookie);

            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            int totalPageCount = 0;
            List<DepartmentEntityExt> orgs = OrgMgmt.Instance.Query(txtQueryName.Text, txtQueryCBCenter.Text, ParentID, PageIndex, PageSize, out totalPageCount);
            rpt.DataSource = orgs;
            rpt.DataBind();
            AspNetPager1.RecordCount = totalPageCount;

            if (orgs == null || orgs.Count == 0)
            {
                plcEmpty.Visible = true;
                AspNetPager1.Visible = false;
            }
            else
            {
                plcEmpty.Visible = false;
                AspNetPager1.Visible = true;
            }
        }

        protected string GetName(object id)
        {
            int nid = 0;
            int.TryParse(id + string.Empty, out nid);
            if (nid != 0)
            {
                if (!OrgIDNameDic.ContainsKey(nid))
                {
                    DepartmentEntityExt org = OrgMgmt.Instance.Get(nid);
                    OrgIDNameDic.Add(nid, org == null ? "" : org.DEPARTMENTNAME);
                }
                return OrgIDNameDic[nid];
            }
            else
                return OrgMgmt.RootOrgName;
        }

        protected void btnImg_Click(object sender, ImageClickEventArgs e)
        {
            BindData();
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }
    }
}