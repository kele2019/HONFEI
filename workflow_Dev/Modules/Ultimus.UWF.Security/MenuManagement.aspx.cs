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
    public partial class MenuManagement : System.Web.UI.Page
    {

        MenuLogic _res = new MenuLogic();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                LoadChild();
                
            }
        }
 

        void LoadChild()
        {
            tvResource.Nodes.Clear();
            List<MenuEntity> list = _res.GetMenuList(); 
            //加第一层节点
            List<MenuEntity> rootlist = list.FindAll(p => p.PARENTID.Trim() == "0" || p.PARENTID == null);
            foreach (MenuEntity ety in rootlist)
            {
                TreeNode tn = new TreeNode();
                if (Lang.Get("MENUFIELDNAME") == "DISPLAYNAME")
                {
                    tn.Text = ety.DISPLAYNAME;
                }
                else
                {
                    tn.Text = ety.MENUNAME;
                }
                tn.Value = ety.MENUID.ToString();
                tvResource.Nodes.Add(tn);

                //递归插入子节点
                AddChildNodes(tn, ety.MENUID, list);
                if (!flag)
                {
                    tn.Expand();
                    flag = true;
                }
            }
            tvResource.ExpandDepth=0;
        }

        bool flag = false;
        void AddChildNodes(TreeNode parent, string id, List<MenuEntity> list)
        {
            List<MenuEntity> childlist = list.FindAll(p => p.PARENTID == id);
            foreach (MenuEntity ety in childlist)
            {
                TreeNode tn = new TreeNode();
                if (Lang.Get("MENUFIELDNAME") == "DISPLAYNAME")
                {
                    tn.Text = ety.DISPLAYNAME;
                }
                else
                {
                    tn.Text = ety.MENUNAME;
                }
                tn.Value = ety.MENUID.ToString();
                parent.ChildNodes.Add(tn);

                AddChildNodes(tn, ety.MENUID, list);

                
            }
        }

        protected void tvResource_SelectedNodeChanged(object sender, EventArgs e)
        {
            //加载数据
            string resourceId=tvResource.SelectedValue;
            MenuEntity ety = _res.GetEntity(resourceId);
            txtMenuId.Text = ety.MENUID.ToString();
            txtUrl.Text = ety.URL;
            txtCNName.Text = ety.MENUNAME;
            txtENName.Text = ety.DISPLAYNAME;
            txtExt01.Text = ety.EXT01;
            txtExt02.Text = ety.EXT02;
            txtExt03.Text = ety.EXT03;
            txtExt04.Text = ety.EXT04;
            txtExt05.Text = ety.EXT05;
            txtExt06.Text = ety.EXT06;
            txtExt07.Text = ety.EXT07;
            txtExt08.Text = ety.EXT08;
            txtExt09.Text = ety.EXT09;
            txtExt10.Text = ety.EXT10;
            txtExt11.Text = ety.EXT11;
            txtExt12.Text = ety.EXT12;
            txtExt13.Text = ety.EXT13;
            txtExt14.Text = ety.EXT14;
            txtExt15.Text = ety.EXT15;
            txtExt16.Text = ety.EXT16;
            txtExt17.Text = ety.EXT17;
            txtExt18.Text = ety.EXT18;
            txtExt19.Text = ety.EXT19;
            txtExt20.Text = ety.EXT20;
            txtExt21.Text = ety.EXT21;
            txtExt22.Text = ety.EXT22;
            txtExt23.Text = ety.EXT23;
            txtExt24.Text = ety.EXT24;
            txtExt25.Text = ety.EXT25;
            txtExt26.Text = ety.EXT26;
            txtExt27.Text = ety.EXT27;
            txtExt28.Text = ety.EXT28;
            txtExt29.Text = ety.EXT29;
            txtExt30.Text = ety.EXT30;
            cbxIsActive.Checked = ety.ISACTIVE == "1" ? true : false;
            txtModule.Text = ety.MODULE;
            txtOrderNo.Text = ety.ORDERNO.ToString();
            txtParentId.Text = ety.PARENTID.ToString().Trim();
            txtType.Text = ety.MENUTYPE;
            txtICON.Text = ety.ICON;
            txtTarget.Text = ety.TARGET;
            cbxIsHomePage.Checked = ety.ISHOMEPAGE == "1" ? true : false;

            //加载类型标签
            btnAdd.Visible = true;
            btnAddSame.Visible = true;
            if (txtShowExt.Text == "0")
            {
            }
            else
            {
                divExt.Visible = true;
            }
            divInfo.Visible = true;
        }

        void SetLabel(Label lbl, string val)
        {
            if (!string.IsNullOrEmpty(val))
            {
                lbl.Text = val+":";
            }
        }
        void SetLabel(Label lbl, int val)
        {
            lbl.Text = "扩展"+val.ToString()+ ":";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MenuEntity ety = _res.GetEntity(txtMenuId.Text);
            if (ety == null)
            {
                ety = new MenuEntity();
            }
            ety.MENUID = txtMenuId.Text;
            //ety.CODE = txtCode.Text;
            ety.MENUNAME = txtCNName.Text;
            ety.DISPLAYNAME = txtENName.Text;
            ety.EXT01 = txtExt01.Text;
            ety.EXT02 = txtExt02.Text;
            ety.EXT03 = txtExt03.Text;
            ety.EXT04 = txtExt04.Text;
            ety.EXT05 = txtExt05.Text;
            ety.EXT06 = txtExt06.Text;
            ety.EXT07 = txtExt07.Text;
            ety.EXT08 = txtExt08.Text;
            ety.EXT09 = txtExt09.Text;
            ety.EXT10 = txtExt10.Text;
            ety.EXT11 = txtExt11.Text;
            ety.EXT12 = txtExt12.Text;
            ety.EXT13 = txtExt13.Text;
            ety.EXT14 = txtExt14.Text;
            ety.EXT15 = txtExt15.Text;
            ety.EXT16 = txtExt16.Text;
            ety.EXT17 = txtExt17.Text;
            ety.EXT18 = txtExt18.Text;
            ety.EXT19 = txtExt19.Text;
            ety.EXT20 = txtExt20.Text;
            ety.EXT21 = txtExt21.Text;
            ety.EXT22 = txtExt22.Text;
            ety.EXT23 = txtExt23.Text;
            ety.EXT24 = txtExt24.Text;
            ety.EXT25 = txtExt25.Text;
            ety.EXT26 = txtExt26.Text;
            ety.EXT27 = txtExt27.Text;
            ety.EXT28 = txtExt28.Text;
            ety.EXT29 = txtExt29.Text;
            ety.EXT30 = txtExt30.Text;
            ety.ISACTIVE = cbxIsActive.Checked ? "1" : "0";
            ety.MODULE = txtModule.Text;
            ety.ORDERNO = ConvertUtil.ToInt32(txtOrderNo.Text);
            ety.PARENTID = txtParentId.Text;
            ety.URL = txtUrl.Text;
            ety.MENUTYPE = txtType.Text;
            ety.ISHOMEPAGE = cbxIsHomePage.Checked ? "1" : "0";
            ety.ICON = txtICON.Text;
            ety.TARGET = txtTarget.Text;
            ety.MODULE = txtModule.Text;
            ety.MENUTYPE = txtType.Text;

            _res.Save(ety);
            //_res.ClearAllResourceList();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "al", "alert('保存成功');", true);
            if (txtNew.Text == "1")
            {
                TreeNode node = new TreeNode();
                if (Lang.Get("MENUFIELDNAME") == "DISPLAYNAME")
                {
                    node.Text = ety.DISPLAYNAME;
                }
                else
                {
                    node.Text = ety.MENUNAME;
                }
                node.Value = ety.MENUID.ToString();
                node.Selected = true;
                tvResource.SelectedNode.ChildNodes.Add(node);
            }
            else if (txtNew.Text == "2")
            {
                TreeNode node = new TreeNode();
                if (Lang.Get("MENUFIELDNAME") == "DISPLAYNAME")
                {
                    node.Text = ety.DISPLAYNAME;
                }
                else
                {
                    node.Text = ety.MENUNAME;
                }
                node.Value = ety.MENUID.ToString();
                node.Selected = true;
                if (tvResource.SelectedNode.Parent == null)
                {
                    tvResource.Nodes.Add(node);
                }
                else
                {
                    tvResource.SelectedNode.Parent.ChildNodes.Add(node);
                }
            }
            else
            {
                if (Lang.Get("MENUFIELDNAME") == "DISPLAYNAME")
                {
                    tvResource.SelectedNode.Text = ety.DISPLAYNAME;
                }
                else
                {
                    tvResource.SelectedNode.Text = ety.MENUNAME;
                }
            }
            txtNew.Text = "0";
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            SerialNoLogic sn = new SerialNoLogic();
            //txtCode.Text = "";
            txtCNName.Text = "";
            txtENName.Text = "";
            txtExt01.Text = "";
            txtExt02.Text = "";
            txtExt03.Text = "";
            txtExt04.Text = "";
            txtExt05.Text = "";
            txtExt06.Text = "";
            txtExt07.Text = "";
            txtExt08.Text = "";
            txtExt09.Text = "";
            txtExt10.Text = "";
            txtExt11.Text = "";
            txtExt12.Text = "";
            txtExt13.Text = "";
            txtExt14.Text = "";
            txtExt15.Text = "";
            txtExt16.Text = "";
            txtExt17.Text = "";
            txtExt18.Text = "";
            txtExt19.Text = "";
            txtExt20.Text = "";
            txtExt21.Text = "";
            txtExt22.Text = "";
            txtExt23.Text = "";
            txtExt24.Text = "";
            txtExt25.Text = "";
            txtExt26.Text = "";
            txtExt27.Text = "";
            txtExt28.Text = "";
            txtExt29.Text = "";
            txtExt30.Text = "";
            cbxIsActive.Checked = true;
            cbxIsHomePage.Checked = false;
            txtOrderNo.Text = "";
            txtTarget.Text = "";
            txtModule.Text = "";
            txtType.Text = "";
            txtUrl.Text = "";
            txtICON.Text = "";
            txtParentId.Text = txtMenuId.Text;
            txtMenuId.Text = Guid.NewGuid().ToString().ToUpper();
            //txtRemark.Text = "";
            txtCNName.Focus();
            txtNew.Text = "1";
        }
        protected void btnAddSame_Click(object sender, EventArgs e)
        {
            SerialNoLogic sn = new SerialNoLogic();
            txtCNName.Text = txtCNName.Text+" Copy";
            txtENName.Text = txtENName.Text + " Copy";
            txtMenuId.Text = Guid.NewGuid().ToString().ToUpper();
            txtCNName.Focus();
            txtNew.Text = "2";
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (tvResource.SelectedNode.ChildNodes.Count > 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "al", "alert('含有子节点不能被删除');", true);
                return;
            }
            _res.Delete(txtMenuId.Text);
            TreeNode parent = tvResource.SelectedNode.Parent;
            if (parent != null)
            {
                parent.Selected = true;
                parent.ChildNodes.RemoveAt(parent.ChildNodes.Count - 1);
            }
            tvResource_SelectedNodeChanged(sender, e);
            
            Page.ClientScript.RegisterStartupScript(this.GetType(), "al", "alert('删除成功');", true);
        }
    }
}