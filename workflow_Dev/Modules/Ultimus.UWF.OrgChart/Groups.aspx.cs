using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Data;
using Ultimus.UWF.Common.Logic; 

namespace Ultimus.UWF.OrgChart
{
    public partial class Groups : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.tvGroups.Nodes.Add(new TreeNode("Groups", "0"));
            }
            BingTreeNode();
        }

        private void BingTreeNode()
        {
            //throw new NotImplementedException();
             
            TreeNode tnParent = tvGroups.Nodes[0];
            tnParent.ChildNodes.Clear();

            string strSql = "select * from V_ORG_GROUP order by GROUPID";
            DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                //清空节点
                foreach (DataRow dr in dt.Rows)
                {
                    TreeNode tn = new TreeNode();
                    tn.Text = ConvertUtil.ToString(dr["GROUPNAME"]);
                    tn.Value = ConvertUtil.ToString(dr["GROUPID"]);
                    tnParent.ChildNodes.Add(tn);
                }
            }
            tnParent.ExpandAll();
        }

        protected void tvGroups_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeNode tn = tvGroups.SelectedNode;


            if (tn.Value == "0")
            {
                hdGid.Value = "";
                txtGroupName.Text = "";
            }
            else
            {
                txtGroupName.Text = tn.Text;
                hdGid.Value = tn.Value;
                BindingExclude(tn.Value);
                bindingContain(tn.Value);
            }
        }

        private void bindingContain(string p)
        {
            //throw new NotImplementedException();
            lbContain.Items.Clear();
            string strSql = "select GROUPID,MEMBERID AS GROUPMEMBERID  ,MEMBERNAME AS GROUPMEMBERNAME ,MEMBERTYPE AS GROUPMEMBERTYPE from V_ORG_GROUPMEMBER  where GROUPID='" + p + "' AND  MEMBERTYPE<>'99'";
            DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(strSql);
            foreach (DataRow dr in dt.Rows)
            {
                string strValue = ConvertUtil.ToString(dr["GROUPMEMBERID"]);
                string gType = ConvertUtil.ToString(dr["GROUPMEMBERTYPE"]);
                string name = ConvertUtil.ToString(dr["GROUPMEMBERNAME"]);
                string strText = "";
                switch (gType)
                {
                    case "1":
                        strText = name ;
                        strValue += "|1";
                        break;
                    case "2":
                    case "3":
                        strText = name ;
                        strValue += "|3";
                        break;
                    
                }
                lbContain.Items.Add(new ListItem(strText, strValue));
            }
        }

        public string GetUserNameById(string strUserid)
        {

            return ConvertUtil.ToString(DataAccess.Instance("BizDB").ExecuteScalar("select distinct a.USERNAME from V_ORG_USER a where a.USERID='" + strUserid + "'"));
        }
        public string GetDeptNameById(string deptId)
        {
            return ConvertUtil.ToString(DataAccess.Instance("BizDB").ExecuteScalar("select a.DEPARTMENTNAME from V_ORG_DEPARTMENT a where a.DEPARTMENTID='" + deptId + "'"));
        }

        private void BindingExclude(string p)
        {

            lbExclude.Items.Clear();
            string strSql = "select GROUPID,MEMBERID AS GROUPMEMBERID  ,MEMBERNAME AS GROUPMEMBERNAME ,MEMBERTYPE AS GROUPMEMBERTYPE from V_ORG_GROUPMEMBER  where GROUPID='" + p + "' AND  MEMBERTYPE='99'";
            DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(strSql);
            foreach (DataRow dr in dt.Rows)
            {
                string strValue = ConvertUtil.ToString(dr["GROUPMEMBERID"]);
                string gType = ConvertUtil.ToString(dr["GROUPMEMBERTYPE"]);
                string name = ConvertUtil.ToString(dr["GROUPMEMBERNAME"]);
                string strText = "";
                switch (gType)
                {
                    case "99":
                        strText = name ;
                        strValue += "|1";
                        break;
                    case "2":
                    case "3":
                        strText = name ;
                        strValue += "|3";
                        break;

                }
                lbExclude.Items.Add(new ListItem(strText, strValue));
            }

        }

        protected void addDept_Click(object sender, EventArgs e)
        {
            doPost(this.lbContain);
        }

        protected void doPost(ListBox lbContain)
        {
            string text = this.txtChoosedDepts.Text.Trim();
            string value = this.txtChoosedDeptids.Text.Trim();
            if (text.Contains(","))
            {
                string[] strTexts = text.Split(new char[] { ',' });
                string[] strValues = value.Split(new char[] { ',' });
                for (int i = 0; i < strTexts.Length; i++)
                {
                    addItems(strTexts[i], strValues[i], lbContain);
                }
            }
            else
            {
                addItems(text, value, lbContain);
            }
            this.txtChoosedDepts.Text = "";
            this.txtChoosedDeptids.Text = "";
        }

        protected void addItems(string text, string value, ListBox lbContain)
        {
            if (text != "" && value != "")
            {
                string[] strValues = value.Split(new char[] { '|' });
                switch (strValues[1])
                {
                    case "DEPT":
                        value = strValues[0] + "|3";
                        break;
                    case "USER":
                        value = strValues[0] + "|1";
                        break;
                }
                if (lbContain.Items.FindByValue(value) == null)
                {
                    lbContain.Items.Add(new ListItem(text, value));
                }
            }
        }

        protected void btnDelMem_Click(object sender, EventArgs e)
        {
            lbContain.Items.Remove(lbContain.SelectedItem);
        }

        protected void addExUser_Click(object sender, EventArgs e)
        {
            doPost(lbExclude);
        }

        protected void btnDelExMem_Click(object sender, EventArgs e)
        {
            lbExclude.Items.Remove(lbExclude.SelectedItem);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strSql = "";
                if (hdGid.Value.Trim() != "")
                {
                    //更新组

                    strSql = "update ORG_GROUP  set GROUPNAME='" + txtGroupName.Text.Trim() + "' where GROUPID='" + hdGid.Value.Trim() + "'";

                }
                else
                {
                    //新增组
                    SerialNoLogic sn = new SerialNoLogic();
                    hdGid.Value = sn.GetMaxNo("ORG_GROUP", "GROUPID").ToString(); 
                    strSql = "insert into ORG_GROUP (GROUPID, GROUPNAME) values ('" + hdGid.Value + "', '" + txtGroupName.Text + "')";
                }
                DataAccess.Instance("BizDB").ExecuteNonQuery(strSql);
                //组成员先删除在新增
                DataAccess.Instance("BizDB").ExecuteNonQuery("delete from ORG_GROUPMEMBER where  GROUPID='" + hdGid.Value + "'");
                foreach (ListItem li in lbContain.Items)
                {
                    string text = li.Text;
                    string[] strValus = li.Value.Split(new char[] { '|' });
                    string strGType = strValus[1];
                    string gmid = "";
                    if (strGType == "1" && strValus[0].Contains("/"))
                    {

                        gmid = strValus[0].Split(new char[] { '/' })[1];
                    }
                    else
                    {
                        gmid = strValus[0];
                    }
                    SerialNoLogic sn = new SerialNoLogic();
                    string strSql1 = " insert into ORG_GROUPMEMBER (ID, GROUPID, MEMBERTYPE, MEMBERID, MEMBERNAME) values ('" + sn.GetMaxNo("ORG_GROUPMEMBER", "ID") + "', '" + hdGid.Value + "', '" + strGType + "', '" + gmid + "', '" + text + "')";
                    DataAccess.Instance("BizDB").ExecuteNonQuery(strSql1);
                }

                foreach (ListItem li in lbExclude.Items)
                {
                    string text = li.Text;
                    string[] strValus = li.Value.Split(new char[] { '|' });
                    string strGType = strValus[1];
                    string gmid = "";
                    if (strGType == "1" && strValus[0].Contains("/"))
                    {

                        gmid = strValus[0].Split(new char[] { '/' })[1];
                    }
                    else
                    {
                        gmid = strValus[0];
                    }
                    SerialNoLogic sn = new SerialNoLogic();
                    string strSql1 = " insert into ORG_GROUPMEMBER (ID, GROUPID, MEMBERTYPE, MEMBERID, MEMBERNAME) values ('" + sn.GetMaxNo("ORG_GROUPMEMBER", "ID") + "', '" + hdGid.Value + "', '99', '" + gmid + "', '" + text + "')";
                    DataAccess.Instance("BizDB").ExecuteNonQuery(strSql1);
                }
                refresh();
                Page.ClientScript.RegisterStartupScript(this.GetType(),"Delgroup1", "<script>alert('保存成功.')</script>");
            }
            catch (Exception ex)
            {
                //// dbTransaction.Rollback();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Delgroup1", "<script>alert('保存失败:" + ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "") + "');</script>");
            }
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {

            //IDbTransaction dbTransaction = DataAccess.Instance("BizDB").BeginTransaction();
            //IDbCommand dbCmd = dbTransaction.Connection.CreateCommand();
            string gid = hdGid.Value.Trim();
            try
            {
                DelGroup(gid, null);
                //dbTransaction.Commit();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Delgroup", "<script>alert('删除成功.')</script>");
                refresh();
            }
            catch (Exception ex)
            {
                //dbTransaction.Rollback();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Delgroup", "<script>alert('删除失败:" + ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "") + "');</script>");
            }


        }

        protected void refresh()
        {
            BingTreeNode();
            //this.txtGroupName.Text = "";
            //this.hdGid.Value = "";
            //lbContain.Items.Clear();
            //lbExclude.Items.Clear();
        }
        protected void DelGroup(string gid, IDbTransaction dbTransaction)
        {
            string strSql1 = "delete from ORG_GROUP  where GROUPID='" + gid + "';";
            string strSql2 = "delete from ORG_GROUPMEMBER where GROUPID='" + gid + "'";
            DataAccess.Instance("BizDB").ExecuteScalar( strSql1+strSql2);
        }

        protected void btnAddNewGroup_Click(object sender, EventArgs e)
        {
            this.txtGroupName.Text = "";
            this.hdGid.Value = "";
            lbContain.Items.Clear();
            lbExclude.Items.Clear();
        }
    }
}