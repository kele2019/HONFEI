using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Threading;
using Ultimus.UWF.Common.Logic;
using Ultimus.UWF.OrgChart.Interface;
using Ultimus.UWF.OrgChart.Entity;

namespace Ultimus.UWF.OrgChart
{
    public partial class DepartmentManagement : System.Web.UI.Page
    {

        IOrg _org = ServiceContainer.Instance().GetService<IOrg>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                txtShowExt.Text = Request.QueryString["ShowExt"];

                LoadChild();
                
            }
        }


        void LoadChild()
        {
            tvResource.Nodes.Clear();
            List<DepartmentEntity> list = _org.GetDepartmentList();

            //加第一层节点
            List<DepartmentEntity> rootlist = list.FindAll(p => p.PARENTID == 0  );
            foreach (DepartmentEntity ety in rootlist)
            {
                TreeNode tn = new TreeNode();
                tn.Text = ety.DEPARTMENTNAME;
               
                tn.Value = ety.DEPARTMENTID.ToString();
                tvResource.Nodes.Add(tn);

                //递归插入子节点
                AddChildNodes(tn, ety.DEPARTMENTID, list);
                if (!flag)
                {
                    tn.Expand();
                    flag = true;
                }
            }
            tvResource.ExpandDepth=0;
        }

        bool flag = false;
        void AddChildNodes(TreeNode parent, int id, List<DepartmentEntity> list)
        {
            List<DepartmentEntity> childlist = list.FindAll(p => p.PARENTID == id);
            foreach (DepartmentEntity ety in childlist)
            {
                TreeNode tn = new TreeNode();
                tn.Text = ety.DEPARTMENTNAME;

                tn.Value = ety.DEPARTMENTID.ToString();
                parent.ChildNodes.Add(tn);

                AddChildNodes(tn, ety.DEPARTMENTID, list);

                
            }
        }

        protected void tvResource_SelectedNodeChanged(object sender, EventArgs e)
        {
            //加载数据
            string resourceId=tvResource.SelectedValue;
            DepartmentEntity ety = _org.GetDepartmentEntity(ConvertUtil.ToInt32(resourceId));
            txtResourceId.Text = ety.DEPARTMENTID.ToString();
            txtCNName.Text = ety.DEPARTMENTNAME;
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
            cbxExt29.Checked = ety.EXT29 == "1" ? true : false ;
            txtExt30.Text = ety.EXT30;
            txtParentId.Text = ety.PARENTID.ToString();
            txtType.Text = ety.DEPARTMENTTYPE;

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
            DepartmentEntity ety = _org.GetDepartmentEntity(ConvertUtil.ToInt32(txtResourceId.Text));
            bool flag=true;
            if (ety == null)
            {
                ety = new DepartmentEntity();
                flag = false;
            }
            ety.DEPARTMENTID = ConvertUtil.ToInt32( txtResourceId.Text);
            ety.DEPARTMENTNAME = txtCNName.Text;
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
            ety.EXT29 = cbxExt29.Checked?"1":"0";
            ety.EXT30 = txtExt30.Text;
            ety.PARENTID = ConvertUtil.ToInt32(txtParentId.Text);
            ety.DEPARTMENTTYPE = txtType.Text;
            if (flag)
            {
                _org.UpdateDepartment(ety);
            }
            else{
                _org.InsertDepartment(ety);
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "al", "alert('保存成功');", true);
            if (txtNew.Text == "1")
            {
                TreeNode node = new TreeNode();
                node.Text = ety.DEPARTMENTNAME;
                node.Value = ety.DEPARTMENTID.ToString();
                node.Selected = true;
                tvResource.SelectedNode.ChildNodes.Add(node);
            }
            else if (txtNew.Text == "2")
            {
                TreeNode node = new TreeNode();
                node.Text = ety.DEPARTMENTNAME;

                node.Value = ety.DEPARTMENTID.ToString();
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
                tvResource.SelectedNode.Text = ety.DEPARTMENTNAME;
               

            }
            txtNew.Text = "0";
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            SerialNoLogic sn = new SerialNoLogic();
            txtCNName.Text = "";
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
            cbxExt29.Checked=false;
            txtExt30.Text = "";
            txtParentId.Text = txtResourceId.Text;
            txtResourceId.Text = sn.GetMaxNo("ORG_DEPARTMENT", "DEPARTMENTID").ToString();
            txtCNName.Focus();
            txtNew.Text = "1";
        }
        protected void btnAddSame_Click(object sender, EventArgs e)
        {
            SerialNoLogic sn = new SerialNoLogic();
            txtCNName.Text = txtCNName.Text+" Copy";

            txtResourceId.Text = sn.GetMaxNo("ORG_DEPARTMENT", "DEPARTMENTID").ToString();
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
            _org.DeleteDepartment(ConvertUtil.ToInt32(txtResourceId.Text));
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