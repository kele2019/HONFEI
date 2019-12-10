using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Data;

namespace Ultimus.UWF.Workflow
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

            string strSql = "select * from sys_groups order by id";
            DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                //清空节点
                tnParent.ChildNodes.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    TreeNode tn = new TreeNode();
                    tn.Text = ConvertUtil.ToString(dr["GROUPNAME"]);
                    tn.Value = ConvertUtil.ToString(dr["ID"]);
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
            string strSql = "select b.groupmemberid ||'~'||b.gmembertype gid,b.groupmembername gname from sys_groupsmember b where b.gid='" + p + "'";
            DataTable dt = DataAccess.Instance("BizDB").ExecuteDataTable(strSql);
            foreach (DataRow dr in dt.Rows)
            {
                string strValue = ConvertUtil.ToString(dr["GID"]);
                string gType = strValue.Split(new char[] { '~' })[1];
                string gmid = strValue.Split(new char[] { '~' })[0];
                string strText = "";
                switch (gType)
                {
                    case "1":
                        strText = GetUserNameById(gmid) + "[用户]";
                        break;
                    case "2":
                    case "3":
                        strText = GetDeptNameById(gmid) + "[部门]";
                        break;
                    default:
                        strText = ConvertUtil.ToString(dr["GNAME"]);
                        break;
                }
                lbContain.Items.Add(new ListItem(strText, strValue));
            }
        }

        public string GetUserNameById(string strUserid)
        {

            return ConvertUtil.ToString(DataAccess.Instance("BizDB").ExecuteScalar("select distinct a.FirstName from v_employee a where a.EmployeeID='" + strUserid + "'"));
        }
        public string GetDeptNameById(string deptId)
        {
            return ConvertUtil.ToString(DataAccess.Instance("BizDB").ExecuteScalar("select a.DepartmentName from v_department a where a.DepartmentID='" + deptId + "'"));
        }

        private void BindingExclude(string p)
        {
            lbExclude.Items.Clear();
            string strSql = "select ''''|| replace( a.excludemembersid,',',''',''')||'''' from sys_groups a where a.id='" + p + "'";
            string strIn = ConvertUtil.ToString(DataAccess.Instance("BizDB").ExecuteScalar(strSql));
            if (!string.IsNullOrEmpty(strIn))
            {
                strSql = "select distinct a.FirstName,a.ShortName from v_employee a where a.ShortName in(" + strIn + ")";
                lbExclude.DataSource = DataAccess.Instance("BizDB").ExecuteDataTable(strSql);
                lbExclude.DataTextField = "FIRSTNAME";
                lbExclude.DataValueField = "SHORTNAME";
                lbExclude.DataBind();
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
            if (text.Contains(",") && value.Contains("<+>"))
            {
                string[] strTexts = text.Split(new char[] { ',' });
                string[] strValues = value.Replace("<", "").Replace(">", "").Split(new char[] { '+' });
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
                        text = text + "[" + strValues[0] + "][部门]";
                        value = strValues[0] + "~3";
                        break;
                    case "USER":
                        text = text + "[用户]";
                        value = strValues[0] + "~1";
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
            //// IDbCommand dbcmd = DataAccess.Instance("BizDB").GetDbCommand();
            // if (dbcmd.Connection.State == ConnectionState.Closed)
            // {
            //     dbcmd.Connection.Open();
            // }
            // IDbTransaction dbTransaction = dbcmd.Connection.BeginTransaction();
            // // = dbTransaction.Connection.CreateCommand();
            // dbcmd.CommandType = CommandType.Text;
            // dbcmd.Transaction = dbTransaction;
            try
            {

                string strExclude = "";
                foreach (ListItem li in lbExclude.Items)
                {
                    strExclude += li.Value.Remove(li.Value.IndexOf("~")) + ',';
                }
                strExclude = strExclude.TrimEnd(new char[] { ',' });
                string strSql = "";
                if (hdGid.Value.Trim() != "")
                {
                    //更新组

                    strSql = "update sys_groups t set t.groupname='" + txtGroupName.Text.Trim() + "',t.grouptype='3',t.excludemembersid ='" + strExclude + "' where t.id='" + hdGid.Value.Trim() + "'";

                }
                else
                {
                    //新增组
                    hdGid.Value = ConvertUtil.ToInt32((DateTime.Now.ToOADate() * 10000)).ToString();
                    strSql = "insert into sys_groups (id, groupname, grouptype, excludemembersid) values ('" + hdGid.Value + "', '" + txtGroupName.Text + "', '3', '" + strExclude + "')";
                }
                DataAccess.Instance("BizDB").ExecuteNonQuery(strSql);
                //dbcmd.CommandText = strSql;
                //dbcmd.ExecuteNonQuery();
                //组成员先删除在新增
                DataAccess.Instance("BizDB").ExecuteNonQuery("delete from sys_groupsmember where gid='" + hdGid.Value + "'");
                //dbcmd.CommandText = "delete from sys_groupsmember where gid='" + hdGid.Value + "'";
                //dbcmd.ExecuteNonQuery();
                foreach (ListItem li in lbContain.Items)
                {
                    string text = li.Text;
                    string[] strValus = li.Value.Split(new char[] { '~' });
                    string strGType = strValus[1];
                    string gmid = "";
                    if (strGType == "1")
                    {
                        gmid = strValus[0].Split(new char[] { '/' })[1];
                    }
                    else
                    {
                        gmid = strValus[0];
                    }
                    string strSql1 = " insert into sys_groupsmember (id, gid, gmembertype, groupmemberid, groupmembername) values ('" + (DateTime.Now.ToOADate() * 10000) + "', '" + hdGid.Value + "', '" + strGType + "', '" + gmid + "', '" + text + "')";
                    DataAccess.Instance("BizDB").ExecuteNonQuery(strSql1);
                    //dbcmd.CommandText = strSql1;
                    //dbcmd.ExecuteNonQuery();
                }
                //dbTransaction.Commit();
                //dbTransaction.Connection.Close();
                //dbTransaction.Dispose();
                refresh();
                Page.RegisterStartupScript("Delgroup1", "<script>alert('保存成功.')</script>");
            }
            catch (Exception ex)
            {
                //// dbTransaction.Rollback();
                Page.RegisterStartupScript("Delgroup1", "<script>alert('保存失败:" + ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "") + "');</script>");
            }
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {

            string gid = hdGid.Value.Trim();
            try
            {
                DelGroup(gid, null);
                Page.RegisterStartupScript("Delgroup", "<script>alert('删除成功.')</script>");
                refresh();
            }
            catch (Exception ex)
            {
                Page.RegisterStartupScript("Delgroup", "<script>alert('删除失败:" + ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "") + "');</script>");
            }


        }

        protected void refresh()
        {
            BingTreeNode();
            this.txtGroupName.Text = "";
            this.hdGid.Value = "";
            lbContain.Items.Clear();
            lbExclude.Items.Clear();
        }
        protected void DelGroup(string gid, IDbTransaction dbTransaction)
        {
            string strSql1 = "delete from sys_groups where id='" + gid + "';";
            string strSql2 = "delete from sys_groupsmember where gid='" + gid + "'";
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