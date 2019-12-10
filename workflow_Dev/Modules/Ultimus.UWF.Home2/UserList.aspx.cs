using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ultimus.UWF.Home2.Code.DAO;
using Ultimus.UWF.OrgChart.Entity;
using Ultimus.UWF.Home2.Code.Entity;

namespace Ultimus.UWF.Home2
{
    public partial class UserList : System.Web.UI.Page
    {
        protected int ParentID { get { int i = 0; int.TryParse(Request.QueryString["ParentID"], out i); return i; } }
        protected int PageIndex { get { return AspNetPager1.CurrentPageIndex; } }
        protected int PageSize = 10;

        private Dictionary<int, DepartmentEntityExt> OrgDic = new Dictionary<int, DepartmentEntityExt>();

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("OrgOrUser", "user");
            Response.Cookies.Add(cookie);

            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            bool isSearch = hidIsSearch.Value == "1";
            int? orgID = null;
            if (isSearch)
            {
                int t = 0;
                if (int.TryParse(hidQueryOrgID.Value, out t))
                {
                    orgID = t;
                }
            }
            else 
                orgID = ParentID;


            int totalPageCount = 0;
            List<UserEntityExt> orgs = UserMgmt.Instance.Query(txtQueryName.Text, orgID, isSearch, txtQueryAccount.Text, PageIndex, PageSize, out totalPageCount);
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

        protected void btnImg_Click(object sender, ImageClickEventArgs e)
        {
            hidIsSearch.Value = "1";
            BindData();
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected string GetOrgName(int level, object orgIDObj)
        {
            int orgID = (int)orgIDObj;
            DepartmentEntityExt org = null;
            if (!OrgDic.TryGetValue(orgID, out org))
            {
                org = OrgMgmt.Instance.Get(orgID);
                OrgDic.Add(orgID, org);
            }

            if (org == null)
                return "";

            return GetOrgName(level, org.Path, org.DEPARTMENTID);
        }

        private string GetOrgName(int level, string orgPath, int currentOrgID)
        {
            List<string> ps = (orgPath + string.Empty).Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (ps.Count <= 1)
            {
                return "";
            }
            int orgIDLevel1 = int.Parse(ps[1]);
            int orgIDLevel2 = ps.Count >= 3 ? int.Parse(ps[2]) : currentOrgID;
            
            int orgID = 0;
            if (level == 1)
                orgID = orgIDLevel1;
            else if (level == 2)
                orgID = orgIDLevel2;

            if (orgID == 0)
                return "";
            if (!OrgDic.ContainsKey(orgID))
            {
                var org = OrgMgmt.Instance.Get(orgID);
                OrgDic.Add(orgID, org);
            }

            var t = OrgDic[orgID];
            return t == null ? "" : t.DEPARTMENTNAME;
        }
    }
}