using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls; 
using MyLib;
using System.Threading;
using Ultimus.UWF.Common.Logic;
using Ultimus.UWF.Common.Entity;

namespace Ultimus.UWF.Common
{
    public partial class ResourceManagement : System.Web.UI.Page
    {

        ResourceLogic _res = new ResourceLogic();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string type = Request.QueryString["Type"];
                txtShowExt.Text = Request.QueryString["ShowExt"];
                if (!string.IsNullOrEmpty(type))
                {
                    List<string> str = new List<string>();
                    str.Add(type);
                    ddlType.DataSource = str;
                    ddlType.DataBind();
                    
                }
                else
                {
                    ddlType.DataSource = _res.GetTopTypeList();
                    ddlType.DataBind();
                }

                ddlType_SelectedIndexChanged(sender,e);

                
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChild(ddlType.SelectedValue);
        }

        void LoadChild(string type)
        {
            tvResource.Nodes.Clear();
            List<ResourceEntity> list = _res.GetResourceList(type);
            List<ResourceEntity> allList = _res.GetAllResourceList();
            //加第一层节点
            List<ResourceEntity> rootlist = list.FindAll(p => p.PARENTID == "0" || p.PARENTID==null);
            foreach (ResourceEntity ety in rootlist)
            {
                TreeNode tn = new TreeNode();
                if (Lang.Get("ResourceNameField").ToUpper() == "CNNAME")
                {
                    tn.Text = ety.NAME;
                }
                else
                {
                    tn.Text = ety.VALUE;
                }
                tn.Value = ety.ID.ToString();
                tvResource.Nodes.Add(tn);

                //递归插入子节点
                AddChildNodes(tn, ety.ID, allList);
                if (!flag)
                {
                    tn.Expand();
                    flag = true;
                }
            }
            tvResource.ExpandDepth=0;
        }

        bool flag = false;
        void AddChildNodes(TreeNode parent, int id, List<ResourceEntity> list)
        {
            List<ResourceEntity> childlist = list.FindAll(p => p.PARENTID ==Convert.ToString( id));
            foreach (ResourceEntity ety in childlist)
            {
                TreeNode tn = new TreeNode();
                if (Lang.Get("ResourceNameField").ToUpper() == "CNNAME")
                {
                    tn.Text = ety.NAME;
                }
                else
                {
                    tn.Text = ety.VALUE;
                }
                tn.Value = ety.ID.ToString();
                parent.ChildNodes.Add(tn);

                AddChildNodes(tn, ety.ID, list);

                
            }
        }

        protected void tvResource_SelectedNodeChanged(object sender, EventArgs e)
        {
            //加载数据
            string resourceId=tvResource.SelectedValue;
            ResourceEntity ety=_res.GetEntity(ConvertUtil.ToInt32(resourceId));
            txtResourceId.Text = ety.ID.ToString();
            txtCode.Text = ety.CODE;
            txtCNName.Text = ety.NAME;
            txtENName.Text = ety.VALUE;
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
            txtParentId.Text =ConvertUtil.ToString( ety.PARENTID);
            txtRemark.Text = ety.REMARK;
            txtType.Text = ety.TYPE;

            //加载类型标签
            List<ResourceEntity> allList = _res.GetAllResourceList();
            ResourceEntity rl= allList.Find(p => p.TYPE == "ExtDescription" && p.MODULE == ety.TYPE);
            if (rl != null)
            {
                SetLabel(lblExt1, rl.EXT01);
                SetLabel(lblExt2, rl.EXT02);
                SetLabel(lblExt3, rl.EXT03);
                SetLabel(lblExt4, rl.EXT04);
                SetLabel(lblExt5, rl.EXT05);
                SetLabel(lblExt6, rl.EXT06);
                SetLabel(lblExt7, rl.EXT07);
                SetLabel(lblExt8, rl.EXT08);
                SetLabel(lblExt9, rl.EXT09);
                SetLabel(lblExt10, rl.EXT10);
                SetLabel(lblExt11, rl.EXT11);
                SetLabel(lblExt12, rl.EXT12);
                SetLabel(lblExt13, rl.EXT13);
                SetLabel(lblExt14, rl.EXT14);
                SetLabel(lblExt15, rl.EXT15);
                SetLabel(lblExt16, rl.EXT16);
                SetLabel(lblExt17, rl.EXT17);
                SetLabel(lblExt18, rl.EXT18);
                SetLabel(lblExt19, rl.EXT19);
                SetLabel(lblExt20, rl.EXT20);
                SetLabel(lblExt21, rl.EXT21);
                SetLabel(lblExt22, rl.EXT22);
                SetLabel(lblExt23, rl.EXT23);
                SetLabel(lblExt24, rl.EXT24);
                SetLabel(lblExt25, rl.EXT25);
                SetLabel(lblExt26, rl.EXT26);
                SetLabel(lblExt27, rl.EXT27);
                SetLabel(lblExt28, rl.EXT28);
                SetLabel(lblExt29, rl.EXT29);
                SetLabel(lblExt30, rl.EXT30);
            }
            else
            {
                SetLabel(lblExt1, 1);
                SetLabel(lblExt2, 2);
                SetLabel(lblExt3, 3);
                SetLabel(lblExt4, 4);
                SetLabel(lblExt5, 5);
                SetLabel(lblExt6, 6);
                SetLabel(lblExt7, 7);
                SetLabel(lblExt8, 8);
                SetLabel(lblExt9, 9);
                SetLabel(lblExt10, 10);
                SetLabel(lblExt11, 11);
                SetLabel(lblExt12, 12);
                SetLabel(lblExt13, 13);
                SetLabel(lblExt14, 14);
                SetLabel(lblExt15, 15);
                SetLabel(lblExt16, 16);
                SetLabel(lblExt17, 17);
                SetLabel(lblExt18, 18);
                SetLabel(lblExt19, 19);
                SetLabel(lblExt20, 20);
                SetLabel(lblExt21, 21);
                SetLabel(lblExt22, 22);
                SetLabel(lblExt23, 23);
                SetLabel(lblExt24, 24);
                SetLabel(lblExt25, 25);
                SetLabel(lblExt26, 26);
                SetLabel(lblExt27, 27);
                SetLabel(lblExt28, 28);
                SetLabel(lblExt29, 29);
                SetLabel(lblExt30, 30);
            }

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
            ResourceEntity ety = _res.GetEntity(ConvertUtil.ToInt32(txtResourceId.Text));
            if (ety == null)
            {
                ety = new ResourceEntity();
            }
            ety.ID = ConvertUtil.ToInt32( txtResourceId.Text);
            ety.CODE = txtCode.Text;
            ety.NAME = txtCNName.Text;
            ety.VALUE = txtENName.Text;
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
            ety.PARENTID = ConvertUtil.ToString(txtParentId.Text);
            ety.REMARK = txtRemark.Text;
            ety.TYPE = txtType.Text;
            _res.Save(ety);
            //_res.ClearAllResourceList();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "al", "alert('保存成功');", true);
            if (txtNew.Text == "1")
            {
                TreeNode node = new TreeNode();
                if (Lang.Get("ResourceNameField").ToUpper() == "CNNAME")
                {
                    node.Text = ety.NAME;
                }
                else
                {
                    node.Text = ety.VALUE;
                }
                node.Value = ety.ID.ToString();
                node.Selected = true;
                tvResource.SelectedNode.ChildNodes.Add(node);
            }
            else if (txtNew.Text == "2")
            {
                TreeNode node = new TreeNode();
                if (Lang.Get("ResourceNameField").ToUpper() == "CNNAME")
                {
                    node.Text = ety.NAME;
                }
                else
                {
                    node.Text = ety.VALUE;
                }
                node.Value = ety.ID.ToString();
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
                if (Lang.Get("ResourceNameField").ToUpper() == "CNNAME")
                {
                    tvResource.SelectedNode.Text = ety.NAME;
                }
                else
                {
                    tvResource.SelectedNode.Text = ety.VALUE;
                }
            }
            txtNew.Text = "0";
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            SerialNoLogic sn = new SerialNoLogic();
            txtCode.Text = "";
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
            txtOrderNo.Text = "";
            txtParentId.Text = txtResourceId.Text;
            txtResourceId.Text = sn.GetMaxNo("COM_RESOURCE", "ID").ToString();
            txtRemark.Text = "";
            txtCNName.Focus();
            txtNew.Text = "1";
        }
        protected void btnAddSame_Click(object sender, EventArgs e)
        {
            SerialNoLogic sn = new SerialNoLogic();
            txtCNName.Text = txtCNName.Text+" Copy";
            txtENName.Text = txtENName.Text + " Copy";
            txtResourceId.Text = sn.GetMaxNo("COM_RESOURCE", "ID").ToString();
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
            _res.DeleteResource(ConvertUtil.ToInt32(txtResourceId.Text));
            TreeNode parent = tvResource.SelectedNode.Parent;
            if (parent != null)
            {
                parent.Selected = true;
                parent.ChildNodes.RemoveAt(parent.ChildNodes.Count - 1);
                tvResource_SelectedNodeChanged(sender, e);
            }
            else
            {
                tvResource.Nodes.RemoveAt(tvResource.Nodes.Count - 1);
            }
            
            Page.ClientScript.RegisterStartupScript(this.GetType(), "al", "alert('删除成功');", true);
        }
    }
}