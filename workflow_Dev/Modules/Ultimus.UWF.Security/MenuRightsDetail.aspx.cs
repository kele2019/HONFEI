using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Threading;
using Ultimus.UWF.Security.Logic;
using Ultimus.UWF.Security.Entity;
using Ultimus.UWF.Common.Logic;

namespace Ultimus.UWF.Security
{
    public partial class MenuRightsDetail : System.Web.UI.Page
    {
        SecurityLogic _logic = new SecurityLogic();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtResourceId.Text = Request.QueryString["ResourceId"];
                if (string.IsNullOrEmpty(txtResourceId.Text))
                {
                    txtResourceId.Text = Guid.Empty.ToString();
                }

                List<MenuRightsObjectEntity> menus = new List<MenuRightsObjectEntity>();

                //根据ResourceId加载数据
                LoadData(out menus);

                LoadMenu(menus);

                btnSave.Text = "保存";
            }

            txtMember.Attributes.Add("readonly", "readonly");
        }

        void LoadData(out List<MenuRightsObjectEntity> menus)
        {
            MenuRightsEntity ety = _logic.GetEntity(txtResourceId.Text);
            if (ety == null)
            {
                menus = new List<MenuRightsObjectEntity>();
            }
            else
            {
                txtName.Text = ety.RIGHTSNAME;
                txtMember.Text = ety.MEMBERNAME;
                txtMemberId.Text = ety.MEMBERID;

                List<MenuRightsObjectEntity> list = _logic.GetObjectsList(txtResourceId.Text);
                menus = list; 
            }          
        }

        void LoadMenu(List<MenuRightsObjectEntity> menus)
        {

            List<MenuEntity> list = _logic.GetMenuList();
                
            //加第一层节点
            List<MenuEntity> rootlist = list.FindAll(p => p.PARENTID.Trim() == "0");
            foreach (MenuEntity ety in rootlist)
            {
                TreeNode tn = new TreeNode();
                if (Lang.Get("MenuFieldNameField") == "DISPLAYNAME")
                {
                    tn.Text = ety.DISPLAYNAME;
                }
                else
                {
                    tn.Text = ety.MENUNAME;
                }
                tn.Value = ety.MENUID.ToString();
                if (menus.Exists(p => p.MENUID == ety.MENUID))
                {
                    tn.Checked = true;
                }
                tn.NavigateUrl = "#";
                tvMenu.Nodes.Add(tn);

                //递归插入子节点
                AddChildNodes(menus, tn, ety.MENUID, list);
            }
            tvMenu.ExpandAll();
        }



        void AddChildNodes(List<MenuRightsObjectEntity> menus, TreeNode parent, string id, List<MenuEntity> list)
        {
            List<MenuEntity> childlist = list.FindAll(p => p.PARENTID == id);
            foreach (MenuEntity ety in childlist)
            {
                TreeNode tn = new TreeNode();
                if (Lang.Get("MenuFieldNameField") == "DISPLAYNAME")
                {
                    tn.Text = ety.DISPLAYNAME;
                }
                else
                {
                    tn.Text = ety.MENUNAME;
                }
                tn.Value = ety.MENUID.ToString(); 
                tn.NavigateUrl = "#";
                if (menus.Exists(p => p.MENUID == ety.MENUID))
                {
                    tn.Checked = true;
                }
                parent.ChildNodes.Add(tn);

                AddChildNodes(menus, tn, ety.MENUID, list);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SecurityLogic logic = new SecurityLogic();
            List<MenuRightsMemberEntity> members = new List<MenuRightsMemberEntity>();
            string[] sz = txtMemberId.Text.Split(',');
            string[] szName = txtMember.Text.Split(',');
            for (int i = 0; i < sz.Length; i++)
            {
                string str = sz[i];
                MenuRightsMemberEntity ety = new MenuRightsMemberEntity();
                string[] ss = str.Split('|');
                ety.MEMBERID = ConvertUtil.ToInt32(ss[0]);
                try
                {
                    ety.MEMBERNAME = szName[i].Split('[')[0];
                }
                catch
                {
                }
                if (ss.Length > 1)
                {
                    ety.MEMBERTYPE = ss[1];
                }
                members.Add(ety);
            }
            List<MenuRightsObjectEntity> objects = new List<MenuRightsObjectEntity>();
            List<TreeNode> tns = GetAllCheckedNodes(tvMenu);
            foreach (TreeNode tn in tns)
            {
                MenuRightsObjectEntity ety = new MenuRightsObjectEntity(); 
                ety.MENUID = tn.Value;
                objects.Add(ety);
            }
            tns = GetAllCheckedNodes(tvData);
            foreach (TreeNode tn in tns)
            {
                MenuRightsObjectEntity ety = new MenuRightsObjectEntity(); 
                ety.MENUID =tn.Value;
                objects.Add(ety);
            }
            logic.SaveSecurity(txtResourceId.Text, txtName.Text, "", txtMember.Text, txtMemberId.Text, members, ddlType.SelectedValue, objects);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + Lang.Get("SecurityDetail_SaveSuccess") + "!');location.href='MenuRightsList.aspx';", true);

        }

        List<TreeNode> GetAllCheckedNodes(TreeView tv)
        {
            List<TreeNode> nodes = new List<TreeNode>();
            foreach (TreeNode tn in tv.Nodes)
            {
                if (tn.Checked)
                {
                    nodes.Add(tn);
                }
                GetChild(nodes, tn);
            }
            return nodes;
        }

        void GetChild(List<TreeNode> tns, TreeNode tn)
        {
            foreach (TreeNode t in tn.ChildNodes)
            {
                if (t.Checked)
                {
                    tns.Add(t);
                }
                GetChild(tns, t);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("SecurityList.aspx");
        }
    }
}