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
    public partial class OrgEdit : System.Web.UI.Page
    {
        protected int Query_ParentID { get { int i = 0; int.TryParse(Request.QueryString["ParentID"], out i); return i; } }
        protected int Query_ID { get { int i = 0; int.TryParse(Request.QueryString["ID"], out i); return i; } }
        protected string Query_ReturnUrl { get { return Request.QueryString["ReturnUrl"]; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            if (Query_ID != 0)
            {
                DepartmentEntityExt org = OrgMgmt.Instance.Get(Query_ID);
                if (org != null)
                {
                    txtName.Text = org.DEPARTMENTNAME;
                    txtEngName.Text = org.EngName;
                    txtCBCenter.Text = org.CBCenter;
                    txtLevel.Text = org.Level;
                    txtSort.Text = org.Sort.ToString();

                    if (org.PARENTID != 0)
                    {
                        DepartmentEntityExt parentorg = OrgMgmt.Instance.Get(org.PARENTID);
                        if (parentorg != null)
                        {
                            hidParentID.Value = parentorg.DEPARTMENTID.ToString();
                            txtParentName.Text = parentorg.DEPARTMENTNAME;
                            txtParentEngName.Text = parentorg.EngName;
                        }
                    }
                    else
                    {
                        hidParentID.Value = "0";
                        txtParentName.Text = OrgMgmt.RootOrgName;
                        txtParentEngName.Text = OrgMgmt.RootOrgEngName;
                    }
                }
            }
            else
            {
                if (Query_ParentID != 0)
                {
                    DepartmentEntityExt parentOrg = OrgMgmt.Instance.Get(Query_ParentID);
                    if (parentOrg != null)
                    {
                        hidParentID.Value = Query_ParentID.ToString();
                        txtParentName.Text = parentOrg.DEPARTMENTNAME;
                        txtParentEngName.Text = parentOrg.EngName;
                    }
                }
                else
                {
                    hidParentID.Value = "0";
                    txtParentName.Text = OrgMgmt.RootOrgName;
                    txtParentEngName.Text = OrgMgmt.RootOrgEngName;
                }
            }
            txtParentName.ReadOnly = txtParentEngName.ReadOnly = true;
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            DepartmentEntityExt parentOrg = null;
            DepartmentEntityExt org = null;
            if (Query_ID == 0)
            {
                org = new DepartmentEntityExt();
                parentOrg = OrgMgmt.Instance.Get(Query_ParentID);
            }
            else
            {
                org = OrgMgmt.Instance.Get(Query_ID);
                parentOrg = OrgMgmt.Instance.Get(org.PARENTID);
            }

            org.DEPARTMENTNAME = txtName.Text;
            org.Sort = int.Parse(txtSort.Text);
            org.EngName = txtEngName.Text;
            if (!string.IsNullOrWhiteSpace(hidParentID.Value))
                org.PARENTID = int.Parse(hidParentID.Value);
            org.CBCenter = txtCBCenter.Text;
            org.Level = txtLevel.Text;
            org.Path = (parentOrg.Path + string.Empty).TrimEnd('.') + "." + parentOrg.DEPARTMENTID + ".";

            if (Query_ID == 0)
            {
                org = OrgMgmt.Instance.Create(org);
                hidNewOrgID.Value = org.DEPARTMENTID.ToString();
                plcCreateSuccess.Visible = true;
            }
            else
            {
                OrgMgmt.Instance.Update(org);
            }
            //Response.Redirect(Query_ReturnUrl);
            Page.RegisterStartupScript("key", "<script>alert('保存成功');parent.location.reload();</script>");
        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(Query_ReturnUrl);
        }
    }
}